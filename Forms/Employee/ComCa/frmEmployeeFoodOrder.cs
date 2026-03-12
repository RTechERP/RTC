using DevExpress.XtraGrid.Columns;
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
    public partial class frmEmployeeFoodOrder : _Forms
    {
        public frmEmployeeFoodOrder()
        {
            InitializeComponent();
        }
        private void frmEmployeeFoodOrder_Load(object sender, EventArgs e)
        {
            txtMonth.Value = DateTime.Now.Month;
            txtYear.Value = DateTime.Now.Year;
            loadData();
        }
        private void loadData()
        {
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetFoodOrderByMonth"
                , new string[] { "@Month", "@Year" }
                , new object[] { TextUtils.ToInt(txtMonth.Value), TextUtils.ToInt(txtYear.Value) });
            grdData.DataSource = ds.Tables[0];
            if (ds.Tables.Count == 0) return;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }


        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            //loadData();
        }

        private void txtMonth_ValueChanged(object sender, EventArgs e)
        {
            //loadData();
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xls";
            f.FileName = $"BaoCaoComCa_{txtMonth.Text}_{txtYear.Value}.xls";
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    //string filepath = $"{f.SelectedPath}/BaoCaoComCa_{txtMonth.Text}_{txtYear.Value}.xls";
                    string filepath = f.FileName;
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
