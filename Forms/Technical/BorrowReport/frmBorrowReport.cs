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

namespace Forms
{
    public partial class frmBorrowReport : _Forms
    {
        int warehouseID;
        public frmBorrowReport()
        {
            InitializeComponent();
        }
        public frmBorrowReport(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }


        private void frmBorrowReport_Load(object sender, EventArgs e)
        {
            loadGrdData();
            this.Text += warehouseID == 1 ? " - HN" : (warehouseID == 2 ? " - HCM" : " - BN");
        }
        void loadGrdData()
        {
           DataTable dt = TextUtils.GetDataTableFromSP("spGetRecentTimeAndNumberUse",new string[] { "@WarehouseID" },new object[] { warehouseID });
           grdData.DataSource = dt;

        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            sfd.FileName = $"BaoCaoMuonThietBi_{DateTime.Now.ToString("ddMMyyyy")}.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                try
                {
                    grvData.ExportToXls(sfd.FileName, optionsEx);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void btnResetData_Click(object sender, EventArgs e)
        {
            loadGrdData();
        }
    }
}
