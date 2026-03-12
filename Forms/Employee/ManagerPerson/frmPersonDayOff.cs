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
    public partial class frmPersonDayOff : _Forms
    {
        int StatusApprove, IDApproved, departmentId;
        DataSet dts;
        string[] arrParamName = null;
        object[] arrParamValue = null;
        public frmPersonDayOff()
        {
            InitializeComponent();
        }

        private void frmPersonDayOff_Load(object sender, EventArgs e)
        {
            //dtpStart.Value = DateTime.Now.AddDays(-1);
            loadDepartment();
            departmentId = Global.DepartmentID;
            cbDepartment.EditValue = Global.DepartmentID;
            cbApprovedStatusTBP.SelectedIndex = 0;
            loadData();
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

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.White;
            e.Appearance.ForeColor = Color.Black;

            if (e.RowHandle >= 0)
            {
                bool isCancel = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsCancelRegister));
                if (isCancel)
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                    e.Appearance.ForeColor = Color.White;
                    e.HighPriority = true;
                }
            }
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
                    string filepath = $"{f.SelectedPath}/TongHopQuanLyNghi_{dtpStart.Value.ToString("ddMMyyyy")}_{dtpEnd.Value.ToString("ddMMyyyy")}.xls";
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

        void loadData()
        {
            if (cbApprovedStatusTBP.SelectedIndex == 1)
                StatusApprove = 1;
            else if (cbApprovedStatusTBP.SelectedIndex == 2)
                StatusApprove = 0;
            departmentId = TextUtils.ToInt(cbDepartment.EditValue);
            DateTime dateTimeS = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day, 00, 00, 00).AddSeconds(-1);
            DateTime dateTimeE = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day, 23, 59, 59).AddSeconds(+1);
            if (cbApprovedStatusTBP.SelectedIndex == 0) ///Tất cả
            {
                arrParamName = new string[] { "@PageNumber", "@PageSize", "@Keyword", "@DateStart", "@DateEnd", "@IDApprovedTP", "@DepartmentID" };
                arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), txtFilterText.Text, dateTimeS,
                    dateTimeE, IDApproved, departmentId };
            }
            else
            {
                arrParamName = new string[] { "@PageNumber", "@PageSize", "@Keyword", "@DateStart", "@DateEnd", "@IDApprovedTP", "@Status", "@DepartmentID" };
                arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), txtFilterText.Text, dateTimeS,
                    dateTimeE, IDApproved, StatusApprove, departmentId };
            }

            dts = TextUtils.LoadDataSetFromSP("spGetDayOff_New", arrParamName, arrParamValue);
            grdData.DataSource = dts.Tables[0];
            if (dts.Tables[0].Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dts.Tables[1].Rows[0]["TotalPage"]);
        }
        void loadDepartment()
        {
            DataTable dt = TextUtils.Select($"Select ID, Code, Name From Department");
            cbDepartment.Properties.DataSource = dt;
            cbDepartment.Properties.DisplayMember = "Name";
            cbDepartment.Properties.ValueMember = "ID";
        }
    }
}
