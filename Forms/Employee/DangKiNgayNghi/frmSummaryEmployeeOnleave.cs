using BMS;
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

namespace Forms.Employee.DangKiNgayNghi
{
    public partial class frmSummaryEmployeeOnleave : _Forms
    {
        public decimal month;
        public decimal year;
        public frmSummaryEmployeeOnleave()
        {
            InitializeComponent();
        }

        private void frmSummaryEmployeeOnleave_Load(object sender, EventArgs e)
        {
            txtMonth.Value = month;
            txtYear.Value = year;
            txtMonth.Focus();
            loadData();
        }

        void loadData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeOnleaveByMonth", "A"
                , new string[] { "@Month", "@Year", "@KeyWord" }
                , new object[] { TextUtils.ToInt(txtMonth.Value), TextUtils.ToInt(txtYear.Value), txtKeyword.Text.Trim() });
            grdData.DataSource = dt;
        }

        private void btnLoad_Click(object sender, EventArgs e)
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
                    string filepath = $"{f.SelectedPath}/BaoCaoNgayNghi_T{txtMonth.Text}_{txtYear.Value}.xls";
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
    }
}
