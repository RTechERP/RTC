using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Forms.Employee.TeamPhongBan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmEmployee : _Forms
    {
        DataTable dt = new DataTable();
        public frmEmployee()
        {
            InitializeComponent();
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            LoadDepartment();
            cboEmployeeStatus.SelectedIndex = 1;
            LoadData();
            var dataExpireContract = dt.Select("IsExpireContract = 1");

            var exp1 = new Expression("UserID", Global.UserID);
            var exp2 = new Expression("Code", "N2");
            var vUsers = SQLHelper<vUserGroupLinkModel>.FindByExpression(exp1.And(exp2));

            if (dataExpireContract.Length > 0 && vUsers.Count > 0)
            {
                MessageBox.Show("Có nhân viên sắp hết hợp đồng!", "Thông báo");
            }
        }

        void LoadData()
        {
            int status = cboEmployeeStatus.SelectedIndex - 1;
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            dt = TextUtils.LoadDataFromSP("spGetEmployee", "A",
                                                    new string[] { "@Status", "@Keyword", "@DepartmentID" },
                                                    new object[] { status, txtKeyword.Text.Trim(), departmentID });
            grdData.DataSource = dt;

            bandSalary.Visible = Global.LoginName != "NV0014";
            colMucDongBHXHHienTai.Visible = Global.LoginName != "NV0014";

            bandSalary.OptionsBand.ShowInCustomizationForm = Global.LoginName != "NV0014";
            colMucDongBHXHHienTai.OptionsColumn.ShowInCustomizationForm = Global.LoginName != "NV0014";
        }


        void LoadDepartment()
        {
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();

            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = list;

            //cboDepartment.EditValue = Global.DepartmentID;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmShowStaff frm = new frmShowStaff();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int row = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id == 0) return;
            //_rownIndex = grvData.FocusedRowHandle;
            //EmployeeModel model = (EmployeeModel)EmployeeBO.Instance.FindByPK(id);
            EmployeeModel model = SQLHelper<EmployeeModel>.FindByID(id);
            frmShowStaff frm = new frmShowStaff();
            frm.Model = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = row;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int rowhandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id == 0) return;
            string name = grvData.GetFocusedRowCellValue(colFullName).ToString();
            //EmployeeModel model = (EmployeeModel)EmployeeBO.Instance.FindByPK(id);
            EmployeeModel model = SQLHelper<EmployeeModel>.FindByID(id);
            frmEmployeeDelete frm = new frmEmployeeDelete();
            frm.employee = model;
            frm.lblMessage.Text = $"Bạn có thực sự muốn chuyển trạng thái của nhân viên [{name}] thành Ngừng hoạt động?";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = rowhandle;
            }


            //DialogResult result = MessageBox.Show("", TextUtils.Caption,
            //    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result == DialogResult.Yes)
            //{
            //    EmployeeModel model = (EmployeeModel)EmployeeBO.Instance.FindByPK(id);
            //    if (model != null)
            //    {
            //        model.Status = 1;//ngừng hoạt động
            //        model.EndWorking = DateTime.Now;
            //        EmployeeBO.Instance.Update(model);

            //        UsersModel user = (UsersModel)UsersBO.Instance.FindByPK(model.UserID);
            //        if (user != null)
            //        {
            //            user.Status = 1;
            //            UsersBO.Instance.Update(user);
            //        }
            //    }

            //    LoadData();
            //}
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            int row = grvData.FocusedRowHandle;
            frmImportExcel frm = new frmImportExcel();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = row;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"DanhSachNhanVien_{DateTime.Now.ToString("ddMMyy")}.xls");

                XlsExportOptions optionsEx = new XlsExportOptions();
                optionsEx.ExportMode = XlsExportMode.SingleFilePageByPage;
                //optionsEx.CustomizeCell += OptionsEx_CustomizeCell;
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;
                grvData.OptionsPrint.AutoWidth = false;
                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXls(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



        private void OptionsEx_CustomizeCell(DevExpress.Export.CustomizeCellEventArgs e)
        {
            try
            {
                foreach (BandedGridColumn item in bandChecklist.Columns)
                {
                    bool isChecked = TextUtils.ToBoolean(e.Value);
                    if (isChecked)
                    {
                        e.Value = "ok";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
            finally
            {
                e.Handled = true;
            }
        }


        private void btnEmployeeApprove_Click(object sender, EventArgs e)
        {
            frmEmployeeApprove frm = new frmEmployeeApprove();
            frm.Show();
        }

        private void btnLoginManager_Click(object sender, EventArgs e)
        {
            int row = grvData.FocusedRowHandle;
            int IDEmlpoyee = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (IDEmlpoyee == 0) return;
            frmLoginManager frm = new frmLoginManager();
            frm.ID = IDEmlpoyee;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = row;
            }
        }

        private void btnEmployeeContract_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;

            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (employeeID <= 0)
            {
                return;
            }

            frmEmployeeContract frm = new frmEmployeeContract();
            frm.employeeID = employeeID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void grvData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {

        }

        private void grvData_RowStyle(object sender, RowStyleEventArgs e)
        {

            if (e.RowHandle >= 0)
            {
                int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus));
                decimal isExpireContract = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colIsExpireContract));
                if (status == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 231, 187);
                    e.Appearance.ForeColor = Color.Black;
                    e.HighPriority = true;
                }
                else if (isExpireContract == 1)
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.Appearance.ForeColor = Color.Black;
                    e.HighPriority = true;
                }
                else if (isExpireContract == 0.5m)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                    e.HighPriority = true;
                }
                else
                {
                    var view = sender as GridView;
                    if (view.FocusedRowHandle == e.RowHandle)
                    {
                        e.Appearance.BackColor = Color.LightYellow;
                        e.Appearance.ForeColor = Color.Black;
                        //e.HighPriority = true;
                    }
                }

            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            btnDelete_Click(null, null);
        }

        private void btnManagerLoginItem_Click(object sender, EventArgs e)
        {
            btnLoginManager_Click(null, null);
        }

        private void btnContractItem_Click(object sender, EventArgs e)
        {
            btnEmployeeContract_Click(null, null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            int row = grvData.FocusedRowHandle;
            LoadData();
            grvData.FocusedRowHandle = row;
        }

        private void cboEmployeeStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int row = grvData.FocusedRowHandle;
            LoadData();
            grvData.FocusedRowHandle = row;
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Space)
            {
                grvData.ClearColumnsFilter();
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            bool isColumnChecklist = bandChecklist.Columns.Contains((BandedGridColumn)e.Column);
            if (isColumnChecklist || e.Column == colTinhTrangCapDongPhuc)
            {
                bool isChecked = TextUtils.ToBoolean(e.Value);
                e.DisplayText = isChecked ? "x" : "";
            }
        }

        private void chkIsExpireContract_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsExpireContract.Checked)
            {
                string filterString = $"([IsExpireContract] = 1)";
                grvData.Columns["IsExpireContract"].FilterInfo = new ColumnFilterInfo(filterString);
            }
            else
            {
                grvData.ClearColumnsFilter();
            }

        }

        private void btnAddProjectType_Click(object sender, EventArgs e)
        {

        }

        private void btnAddTeam_DropDownOpening(object sender, EventArgs e)
        {
            if (btnAddTeam.DropDownItems.Count > 0) btnAddTeam.DropDownItems.Clear();

            //List<ProjectTypeModel> listStatus = SQLHelper<ProjectTypeModel>.FindAll().Where(x => x.ParentID != 0).ToList();
            List<ProjectTypeModel> listStatus = SQLHelper<ProjectTypeModel>.FindAll();
            foreach (var item in listStatus)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem();
                menuItem.Name = $"btnProjectType{item.ID}";
                menuItem.Text = $"{item.ProjectTypeName}";
                menuItem.Tag = $"{item.ID}";
                menuItem.Click += MenuItem_Click; ;
                btnAddTeam.DropDownItems.Add(menuItem);
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            //MessageBox.Show(item.Text, item.Name);


            int projectTypeID = TextUtils.ToInt(item.Tag);
            var rowHandle = grvData.FocusedRowHandle;
            int[] selectedRows = grvData.GetSelectedRows();
            List<int> lstIDs = new List<int>();
            foreach (int row in selectedRows)
            {
                int empID = TextUtils.ToInt(grvData.GetRowCellValue(row, "ID"));
                if (empID <= 0) continue;
                lstIDs.Add(empID);
            }
            if (lstIDs.Count <= 0) return;

            string strIDs = string.Join(",", lstIDs);
            Dictionary<string, object> newDict = new Dictionary<string, object>()
            {
                {"ProjectTypeID", projectTypeID},
                {"UpdatedDate", DateTime.Now},
                {"UpdatedBy", Global.AppCodeName}
            };
            Expression ex1 = new Expression("ID", strIDs, "IN");
            SQLHelper<EmployeeModel>.UpdateFields(newDict, ex1);
            LoadData();
        }

        private void btnEmployeeTeam_Click(object sender, EventArgs e)
        {
            frmEmployeeTeam frm = new frmEmployeeTeam();
            frm.Show();
        }
    }
}
