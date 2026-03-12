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
    public partial class frmPersonNoFingerprint : _Forms
    {
        int StatusApprove, IDApproved;
        string[] arrParamName = null;
        object[] arrParamValue = null;
        public frmPersonNoFingerprint()
        {
            InitializeComponent();
        }

        private void frmPersonNoFingerprint_Load(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Now.AddDays(-1);
            LoadDepartment();
            cbDepartment.EditValue = Global.DepartmentID;
            cbApprovedStatusTBP.SelectedIndex = 0;
            loadData();
        }
        void loadData()
        {
            if (cbApprovedStatusTBP.SelectedIndex == 1)
                StatusApprove = 1;
            else if (cbApprovedStatusTBP.SelectedIndex == 2)
                StatusApprove = 0;
            int departmentId = TextUtils.ToInt(cbDepartment.EditValue);
            DateTime dateTimeS = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day, 00, 00, 00).AddSeconds(-1);
            DateTime dateTimeE = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day, 23, 59, 59).AddSeconds(+1);
            if (cbApprovedStatusTBP.SelectedIndex == 0)
            {
                arrParamName = new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Keyword", "@DepartmentID", "@IDApprovedTP" };
                arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE,
                    txtFilterText.Text, departmentId, IDApproved };
            }
            else
            {
                arrParamName = new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Keyword", "@DepartmentID", "@IDApprovedTP", "@Status" };
                arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE,
                    txtFilterText.Text, departmentId, IDApproved,StatusApprove };
            }
            DataSet dts = TextUtils.LoadDataSetFromSP("spGetEmployeeNoFingerprint", arrParamName, arrParamValue);
            grdData.DataSource = dts.Tables[0];
            if (dts.Tables[0].Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dts.Tables[1].Rows[0]["TotalPage"]);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void cbApprovedStatusTBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cbDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
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
                    string filepath = $"{f.SelectedPath}/TongHopQuenVanTay_{dtpStart.Value.ToString("ddMMyyyy")}_{dtpEnd.Value.ToString("ddMMyyyy")}.xls";
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

        void LoadDepartment()
        {
            DataTable dtDepartment = TextUtils.Select($"SELECT ID, Code, Name FROM Department");
            cbDepartment.Properties.DataSource = dtDepartment;
            cbDepartment.Properties.DisplayMember = "Name";
            cbDepartment.Properties.ValueMember = "ID";
        }
    }
}
