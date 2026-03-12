using DevExpress.Utils;
using DevExpress.XtraEditors;
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
    public partial class frmProductExportAndBorrow : _Forms
    {
        int warehouseID = 1;
        public frmProductExportAndBorrow()
        {
            InitializeComponent();
        }

        private void frmProductExportDate_Load(object sender, EventArgs e)
        {
            //DateTime datenow = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, 1, 0, 0, 0);
            dtpEndDate.Value = DateTime.Now;
            dtpStartDate.Value = dtpStartDate.Value.AddYears(-1);
            //txtPageNumber.Text = "1";
            //txtTotalPage.Text = "1";
            //txtPageSize.Value = 500;
            LoadData();
        }

        private void LoadData()
        {
            DateTime dateTimeS = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", "Đang load dữ liệu"))
            {
                DataTable dt = TextUtils.LoadDataFromSP("spGetProductByExportAndBorrowDate", "A",
                            new string[] { "@DateStart", "@DateEnd", "@PageNumber", "@PageSize", "@FilterText", "@WarehouseID" },
                            new object[] { dateTimeS, dateTimeE, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), txtFilterText.Text, warehouseID });
                grdData.DataSource = dt;
                if (dt.Rows.Count <= 0) return;
                if (string.IsNullOrEmpty(TextUtils.ToString(dt.Rows[0]["TotalPage"])))
                {
                    txtTotalPage.Text = "1";
                }
                else txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
            }

        }


        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadData();

        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadData();

        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadData();

        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"DanhSachSPKhongDungDen_{dtpStartDate.Value.ToString("ddMMyy")}_{dtpEndDate.Value.ToString("ddMMyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"DanhSachSPKhongDungDen_{dtpStartDate.Value.ToString("ddMMyy")}_{dtpEndDate.Value.ToString("ddMMyy")}.xlsx");
                string filepath = f.FileName;

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

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            dtpStartDate.Value = dtpEndDate.Value.AddDays(-TextUtils.ToInt(txtTotalDays.Value));
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            txtTotalDays.Value = TextUtils.ToDecimal((dtpEndDate.Value.Date - dtpStartDate.Value.Date).TotalDays);
        }
    }
}