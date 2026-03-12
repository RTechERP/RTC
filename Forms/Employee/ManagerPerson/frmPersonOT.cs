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
using BMS;
using DevExpress.UIAutomation;
using DevExpress.XtraPrinting;

namespace Forms.Personal
{
    public partial class frmPersonOT : _Forms
    {
        int StatusApprove, IDApproved;
        string[] arrParamName = null;
        object[] arrParamValue = null;
        public frmPersonOT()
        {
            InitializeComponent();
        }
        void LoadOvertime()
        {
            grdData.DataSource = null;
            grdSummary.DataSource = null;

            if (cbApprovedStatusTBP.SelectedIndex == 1)
                StatusApprove = 1;
            else if (cbApprovedStatusTBP.SelectedIndex == 2)
                StatusApprove = 0;
            DateTime datetimeS = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 00, 00, 00).AddSeconds(-1);
            DateTime datetimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59).AddSeconds(1);
            int departmentId = TextUtils.ToInt(cbDepartment.EditValue);
            int employeeId = TextUtils.ToInt(cboEmployee.EditValue); // VTN update 14725
            int team = TextUtils.ToInt(cboTeam.EditValue); // NTA B update 080925
            if (cbApprovedStatusTBP.SelectedIndex == 0) //Tất cả
            {
                arrParamName = new string[] { "@FilterText", "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@DepartmentID", "@IDApprovedTP", "@EmployeeID", "@TeamID" };
                arrParamValue = new object[] { txtKeyword.Text, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), datetimeS, datetimeE, departmentId, IDApproved, employeeId, team };
            }// VTN update 14725
            else
            {
                arrParamName = new string[] { "@FilterText", "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@DepartmentID", "@IDApprovedTP", "@Status", "@EmployeeID", "@TeamID" };
                arrParamValue = new object[] { txtKeyword.Text, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), datetimeS, datetimeE, departmentId, IDApproved, StatusApprove, employeeId, team };
            }// VTN update 14725

            //DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeOvertime", "A", arrParamName, arrParamValue);
            //grdData.DataSource = dt;

            //if (dt.Rows.Count == 0) return;
            //txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
            ////LN Hải update thêm tổng hợp 11/06/2025
            //var summary = dt.AsEnumerable()
            //    .GroupBy(row => row["FullName"])
            //    .Select(g => new
            //    {
            //        FullName = g.Key,
            //        HourSummary = g.Sum(r => TextUtils.ToDecimal(r["TimeReality"])),
            //        TotalBenefitPeriod = g.Sum(r => TextUtils.ToDecimal(r["TotalTime"])),
            //    })
            //    .ToArray();
            //grdSummary.DataSource = summary;

            DataSet data = TextUtils.LoadDataSetFromSP("spGetEmployeeOvertime", arrParamName, arrParamValue);
            DataTable dt = data.Tables[0];
            grdData.DataSource = dt;

            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
            grdSummary.DataSource = data.Tables[1];
        }
        void LoadDepartment()
        {
            DataTable dtDepartment = TextUtils.Select($"SELECT ID, Code, Name FROM Department");
            cbDepartment.Properties.DataSource = dtDepartment;
            cbDepartment.Properties.DisplayMember = "Name";
            cbDepartment.Properties.ValueMember = "ID";
        }
        void loadUserTeam() //NTA B - update 08/09/25
        {
            int departMent = TextUtils.ToInt(cbDepartment.EditValue);
            //var listDept = TextUtils.LoadDataFromSP("spGetUserTeam", "A", new string[] { "@DepartmentID" }, new object[] { 0 });
            //cboTeam.Properties.ValueMember = "ID";
            //cboTeam.Properties.DisplayMember = "Name";
            //cboTeam.Properties.DataSource = listDept;
            //cboTeam.EditValue = Global.UserTeamID;

            int currentYear = DateTime.Now.Year;
            int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;
            DataTable dt = TextUtils.LoadDataFromSP("spGetALLKPIEmployeeTeam", "A",
                                                        new string[] { "@YearValue", "@QuarterValue", "@DepartmentID" },
                                                        new object[] { currentYear, currentQuarter, departMent });
            //var filteredRows = dt.AsEnumerable().Where(r => typeID == 3 || Global.IsAdmin || TextUtils.ToInt(r["LeaderID"]) == Global.EmployeeID).CopyToDataTable();
            cboTeam.Properties.DataSource = dt;
            cboTeam.Properties.ValueMember = "ID";
            cboTeam.Properties.DisplayMember = "Name";
            cboTeam.EditValue = Global.UserTeamID;
        }
        private void frmPersonOT_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Now.AddDays(-1);
            txtPageNumber.Text = "1";
            LoadDepartment();
            loadUserTeam(); // NTA B update 080925
            LoadEmployee(); // VTN update 14725
            IDApproved = 0;
            cbDepartment.EditValue = Global.DepartmentID;//phòng ban
            cbApprovedStatusTBP.SelectedIndex = 0; //tất cả
            LoadOvertime();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadOvertime();
        }

        private void grdData_Enter(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void cbApprovedStatusTBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOvertime();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/TongHopLamThem_{dtpStartDate.Value.ToString("ddMMyyyy")}_{dtpEndDate.Value.ToString("ddMMyyyy")}.xls";
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

        private void cbDepartment_EditValueChanged(object sender, EventArgs e)
        {
            LoadOvertime();
            loadUserTeam(); // NTA B update 080925
        }

        #region VTN update 14725
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadOvertime();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (txtTotalPage.Text == "" || int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadOvertime();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (txtTotalPage.Text == "" || int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadOvertime();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (txtTotalPage.Text == "" || int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadOvertime();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            LoadOvertime();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            LoadOvertime();
        }

        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DataSource = dt;
        }
        #endregion
    }
}
