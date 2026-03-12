using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
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
    public partial class frmHistoryPrice : _Forms
    {
        public frmHistoryPrice()
        {
            InitializeComponent();
        }

        private void frmHistoryPrice_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            string keyword = txtKeyword.Text.Trim();
            DataTable dt = TextUtils.LoadDataFromSP("spGetHistoryPricePartlist", "A",new string[] { "@Keyword" },new object[] { keyword});
            grdData.DataSource = dt;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            LoadData();
            grvData.FocusedRowHandle = rowHandle;
        }

        private void frmHistoryPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"LichSuHoiGia_{DateTime.Now.ToString("ddMMyy")}.xlsx");

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
