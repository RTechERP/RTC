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
using DevExpress.XtraPrinting;

namespace Forms.Personal
{
    public partial class frmPersonEarlyLate : _Forms
    {
        int StatusApprove, IDApproved;
        string[] arrParamName = null;
        object[] arrParamValue = null;
        public frmPersonEarlyLate()
        {
            InitializeComponent();
        }

        private void frmPersonEarlyLate_Load(object sender, EventArgs e)
        {
            //dtpStart.Value = DateTime.Now.AddDays(-1);
            txtPageNumber.Text = "1";
            loadDepartment();
            cboDepartment.EditValue = Global.DepartmentID;
            cbApprovedStatusTBP.SelectedIndex = 0;
            LoadEmployeeEarlyLate();
        }
        void loadDepartment()
        {
            DataTable dt = TextUtils.Select($"Select ID, Code, Name From Department");
            cboDepartment.Properties.DataSource = dt;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadEmployeeEarlyLate();
        }

        private void cbApprovedStatusTBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEmployeeEarlyLate();
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            LoadEmployeeEarlyLate();
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
                    string filepath = $"{f.SelectedPath}/TongHopDiMuonVeSom_{dtpStart.Value.ToString("ddMMyyyy")}_{dtpEnd.Value.ToString("ddMMyyyy")}.xls";
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

        void LoadEmployeeEarlyLate()
        {
            if (cbApprovedStatusTBP.SelectedIndex == 1)
                StatusApprove = 1;
            else if (cbApprovedStatusTBP.SelectedIndex == 2)
                StatusApprove = 0;

            int departmentId = TextUtils.ToInt(cboDepartment.EditValue);
            DateTime dateTimeS = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day, 00, 00, 00).AddSeconds(-1);
            DateTime dateTimeE = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day, 23, 59, 59).AddSeconds(+1);
            if (cbApprovedStatusTBP.SelectedIndex == 0)
            {
                arrParamName = new string[] { "@FilterText", "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@DepartmentID", "@IDApprovedTP" };
                arrParamValue = new object[] { txtKeyword.Text, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, departmentId, IDApproved };
            }
            else
            {
                arrParamName = new string[] { "@FilterText", "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@DepartmentID", "@IDApprovedTP", "@Status" };
                arrParamValue = new object[] { txtKeyword.Text, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, departmentId, IDApproved, StatusApprove };
            }
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeEarlyLate_New", "A", arrParamName, arrParamValue);
            grdData.DataSource = dt;
            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
        }
    }
}
