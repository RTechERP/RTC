using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectAll : _Forms
    {
        public int ID;
        public frmProjectAll()
        {
            InitializeComponent();
        }

        private void frmProjectAll_Load(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            //dtpFromDate.Value = new DateTime(DateTime.Now.Year, 01, 01, 00, 00 , 00);
            loadProject();
            cboProject.EditValue = ID;
            LoadProjectItem();

            //object value = grvMaster.GetGroupSummaryValue(GridControl.InvalidRowHandle, summaryItem);

        }
        #region Methods
        /// <summary>
        /// load projecttype
        /// </summary>
        void loadProject()
        {
            DataTable Project = TextUtils.GetTable("spGetProject_ALL", "A");

            cboProject.Properties.DataSource = Project;
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.ValueMember = "ID";
        }

        void LoadProjectItem()
        {
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            DataTable ProjectItem = TextUtils.LoadDataFromSP("spGetDailyReportTechnical_New", "A",
                new string[] { "@ProjectID", "@FilterText", "@PageSize", "@PageNumber" },
                new object[] { projectID, txtFilterText.Text.Trim(), txtPageSize.Value, TextUtils.ToInt(txtPageNumber.Text) });

            grdMaster.DataSource = ProjectItem;
            if (ProjectItem.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(ProjectItem.Rows[0]["TotalPage"]);

            GridSummaryItem summaryItem = grvMaster.Columns["TotalHours"].SummaryItem;
            double totalDay = TextUtils.ToDouble(summaryItem.SummaryValue) / 8.0;
            txtTotalDay.Text = totalDay.ToString("0.##");
            loadCong();
        }
        #endregion
        private void loadCong()
        {
            decimal vales = TextUtils.ToDecimal(grvMaster.Columns.ColumnByName("colTotalHours").SummaryItem.SummaryValue);
            decimal result = vales / 8;
            grvMaster.Columns.ColumnByName("colResults").SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Custom, $"Tổng số ngày = {result.ToString("n1")}");
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadProjectItem();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

            var rowSelected = (DataRowView)cboProject.GetSelectedDataRow();

            //var projectCode = rowSelected[0];


            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xls)|*.xls;*.xls";
            sfd.FileName = $"{TextUtils.ToString(rowSelected.Row["ProjectCode"])}_{DateTime.Now.ToString("ddMMyy")}";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptions optionsEx = new XlsExportOptions();
                //optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvMaster.OptionsPrint.AutoWidth = false;
                grvMaster.OptionsPrint.ExpandAllDetails = false;
                grvMaster.OptionsPrint.PrintDetails = true;
                grvMaster.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvMaster.ExportToXls(sfd.FileName, optionsEx);
                    Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadProjectItem();
        }

        private void grvMaster_ColumnFilterChanged(object sender, EventArgs e)
        {
            loadCong();
        }

        private void btnChangeProject_Click(object sender, EventArgs e)
        {
            int[] rowSelecteds = grvMaster.GetSelectedRows();
            frmDailyrReportTechnicalChangeProject frm = new frmDailyrReportTechnicalChangeProject();
            frm.cboProjectOld.EditValue = cboProject.EditValue;

            if (rowSelecteds.Length > 0)
            {
                foreach (int row in rowSelecteds)
                {
                    int id = TextUtils.ToInt(grvMaster.GetRowCellValue(row, "ID"));
                    if (id <= 0) continue;

                    frm.listID.Add(id);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn báo cáo!", "Thông báo"); 
                return;
                //int id = TextUtils.ToInt(grvMaster.GetRowCellValue(grvMaster.FocusedRowHandle, "ID"));
                //if (id <= 0) return;

                //frm.listID.Add(id);
            }


            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnFind_Click(null, null);
            }
        }

        private void grvMaster_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                //e.HighPriority = true;
            }
        }
    }
}
