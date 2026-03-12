using BMS;
using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS;
using Forms.Employee;
using System.Collections;
//using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;

namespace Forms.DanhMuc.DuAn
{
    public partial class frmProjectPartList : _Forms
    {
        public int ProjectID;
        public string projectCode;
        public string projectName;
        public frmProjectPartList()
        {
            InitializeComponent();
        }
        private void frmProjectPartList_Load(object sender, EventArgs e)
        {
            this.Text = $"TIẾN ĐỘ VẬT TƯ DỰ ÁN: {projectCode} - {projectName}";
            cbStatus.SelectedIndex = 0;
            loadPersonManager();
            loadProjectPartList();
        }
        void loadPersonManager()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Employee");
            cbPersonManager.Properties.DataSource = dt;
            cbPersonManager.Properties.DisplayMember = "FullName";
            cbPersonManager.Properties.ValueMember = "ID";
        }
        void loadProjectPartList()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartList_Khanh", "A",new string[] { "@ProjectID" },new object[] { ProjectID });
            grdData.DataSource = dt;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmProjectPartListDetail frm = new frmProjectPartListDetail();
            frm.ProjectID = ProjectID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectPartList();
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id == 0) return;
            ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
            frmProjectPartListDetail frm = new frmProjectPartListDetail();
            frm.ProjectID = ProjectID;
            frm.model = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectPartList();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int[] listId = grvData.GetSelectedRows();
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (listId.Count() == 0) return;
            if(MessageBox.Show("Bạn có chắc muốn xoá danh sách đã chọn không?",TextUtils.Caption,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var item in listId)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(item, colID));
                    ProjectPartListModel partList = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
                    partList.IsDeleted = true;
                    ProjectPartListBO.Instance.Update(partList);


                    //EmailSender.setEmail(projectCode, partList, false);
                }
                
                loadProjectPartList();
            }
        }
        private void btlExcel_Click(object sender, EventArgs e)
        {
            frmInputByExcel frm = new frmInputByExcel();
            frm.ProjectID = ProjectID;
            if(frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectPartList();
            }
        }
        private void btnSet_Click(object sender, EventArgs e)
        {
            ArrayList arr = new ArrayList(grvData.GetSelectedRows());

            if (arr.Count <= 0) 
            {
                MessageBox.Show("Bạn chưa chọn vật tư. Xin vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn set người phụ trách này cho tất cả các vật \ntư được chọn?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(TextUtils.ToInt(arr[i]),colID));
                    if (id == 0) continue;
                    ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);
                    ProjectPartListJoiner.ProjectManagerID = TextUtils.ToInt(cbPersonManager.EditValue);
                    model.EmployeeID = TextUtils.ToInt(cbPersonManager.EditValue);
                    ProjectPartListBO.Instance.Update(model);
                }  
            }
            loadProjectPartList();
            arr.Clear();
        }
        private void btnStatus_Click(object sender, EventArgs e)
        {
            ArrayList arr = new ArrayList(grvData.GetSelectedRows());

            if (arr.Count <= 0)
            {
                MessageBox.Show("Bạn chưa chọn vật tư. Xin vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (MessageBox.Show("Bạn có chắc muốn set trạng thái này cho tất cả các vật \ntư được chọn?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(TextUtils.ToInt(arr[i]), colID));
                    if (id == 1) continue;
                    ProjectPartListModel model = (ProjectPartListModel)ProjectPartListBO.Instance.FindByPK(id);

                    switch (cbStatus.SelectedIndex)
                    {
                        case 1:
                            {
                                model.Status = 2;
                                break;
                            }
                        case 2:
                            {
                                model.Status = 1;
                                break;
                            }
                        case 3:
                            {
                                model.Status = 0;
                                break;
                            }
                        default:
                            {
                                model.Status = -1;
                                break;
                            }
                    }

                    ProjectPartListBO.Instance.Update(model);
                }
            }
            loadProjectPartList();
            arr.Clear();
        }
        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
        }
        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle,colStatus));
            if (status == 2)
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
        }
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptions optionsEx = new XlsExportOptions();
                //optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                //optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/TienDoVatTuDuAn_{projectCode}.xls";
                    grvData.ExportToXls(filepath, optionsEx);

                    Process.Start(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    //grvData.ExportToExcelOld($"{f.SelectedPath}/KeHoachDuAn_{cboProject.Text}.xls");
                }
                grvData.ClearSelection();
            }
        }
        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var gridvew = sender as GridView;

            //if (e.RowHandle < 0)
            //    return;

            bool value = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsDeleted));
            if (value)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
                e.Appearance.ForeColor = Color.White;
            }
                
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}
