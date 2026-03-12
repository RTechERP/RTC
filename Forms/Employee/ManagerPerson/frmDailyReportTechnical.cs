using BMS.Model;
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
    public partial class frmDailyReportTechnical : _Forms
    {
        public frmDailyReportTechnical()
        {
            InitializeComponent();
        }

        private void frmDailyReportTechnical_Load(object sender, EventArgs e)
        {
            //dtpFromDate.Value = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToShortDateString());
            dtpFromDate.Value = DateTime.Now.AddDays(-1);
            dtpEndDate.Value = DateTime.Now;

            LoadDepartment();
            LoadTeam();
            LoadEmployee();
            LoadData();
        }

        void LoadData()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int teamID = TextUtils.ToInt(cboTeam.EditValue);
            int userID = TextUtils.ToInt(cboEmployee.EditValue);

            DateTime ds = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day).AddSeconds(-1);
            DateTime de = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23,59,59).AddSeconds(+1);

            DataTable dt = TextUtils.GetDataTableFromSP("spGetDailyReportTechnical",
                new string[] { "@DateStart", "@DateEnd","@TeamID", "@Keyword", "@UserID", "@DepartmentID" },
                new object[] {ds, de, teamID ,txtFilterText.Text.Trim(), userID, departmentID});

            grdData.DataSource = dt;
        }

        void LoadDepartment()
        {
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.SqlToList("SELECT * FROM Department");

            cboDepartment.Properties.DataSource = list;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";

            cboDepartment.EditValue = Global.DepartmentID;
        }


        void LoadTeam()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            List<UserTeamModel> list = SQLHelper<UserTeamModel>.SqlToList($"SELECT * FROM dbo.UserTeam WHERE DepartmentID = {departmentID}");

            cboTeam.Properties.DataSource = list;
            cboTeam.Properties.DisplayMember = "Name";
            cboTeam.Properties.ValueMember = "ID";
        }

        void LoadEmployee()
        {
            List<EmployeeModel> list = SQLHelper<EmployeeModel>.SqlToList($"SELECT ID,UserID,Code,FullName FROM dbo.Employee");

            cboEmployee.Properties.DataSource = list;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "UserID";

            cboEmployee.EditValue = Global.UserID;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                //optionsEx.CustomizeCell += OptionsEx_CustomizeCell;
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/BaoCaoCongViec_{dtpFromDate.Value.ToString("ddMMyy")}_{dtpEndDate.Value.ToString("ddMMyy")}.xls";
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

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            LoadTeam();
            LoadData();
        }

        private void cboTeam_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void grvData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            try
            {
                if (e.Column.FieldName == "UserTeamID")
                {
                    object val1 = view.GetListSourceRowCellValue(e.ListSourceRowIndex1, "UserTeamID");
                    object val2 = view.GetListSourceRowCellValue(e.ListSourceRowIndex2, "UserTeamID");
                    e.Handled = true;
                    e.Result = System.Collections.Comparer.Default.Compare(val1, val2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
