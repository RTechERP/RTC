using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace BMS
{
    public partial class frmBillExportSynthetic : _Forms
    {
        public string WarehouseCode;

        public frmBillExportSynthetic()
        {
            InitializeComponent();
        }

        private void frmBillExportSynthetic_Load(object sender, EventArgs e)
        {
            this.Text += " - " + WarehouseCode;
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";
            loadProductGroup();
            cbProductGroup.CheckAll();
            LoadStatus();
            loadDocSale();
            loadBillExportSynthetic();
        }

        private void LoadStatus()
        {
            List<object> list = new List<object>() {
                new {ID = -1,Name = "--Tất cả--"},
                new {ID = 0,Name = "Mượn"},
                new {ID = 1,Name = "Tồn Kho"},
                new {ID = 2,Name = "Đã Xuất Kho"},
                //new {ID = 3,Name = "Chia Trước"},
                //new {ID = 4,Name = "Phiếu mượn nội bộ"},
                new {ID = 5,Name = "Xuất trả NCC"},
                new {ID = 6,Name = "Yêu cầu xuất kho"},
            };
            cboStatusNew.Properties.DataSource = list;
            cboStatusNew.Properties.ValueMember = "ID";
            cboStatusNew.Properties.DisplayMember = "Name";
            cboStatusNew.EditValue = -1;
        }

        void loadProductGroup()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM ProductGroup");
            cbProductGroup.Properties.DisplayMember = "ProductGroupName";
            cbProductGroup.Properties.ValueMember = "ID";
            cbProductGroup.Properties.DataSource = dt;
        }

        void loadDocSale()
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDMaster));
            DataTable dt = TextUtils.Select($"Select * from DocumentSale where BillID = {ID}  AND BillType = 2");
            grdData.DataSource = dt;
        }

        private void loadBillExportSynthetic()
        {
            try
            {
                grdData.DataSource = null;

                DateTime? dateStart = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
                DateTime? dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

                if (chkAllBillExport.Checked) dateStart = dateEnd = null;
                dtpFromDate.Enabled = dtpEndDate.Enabled = !chkAllBillExport.Checked;

                int status = TextUtils.ToInt(cboStatusNew.EditValue);
                int pageNumber = TextUtils.ToInt(txtPageNumber.Text);
                int pageSize = TextUtils.ToInt(txtPageSize.Text);
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu..."))
                {


                    DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportSynthetic", "A"
                                            , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@KhoType", "@FilterText", "@WarehouseCode" }
                                            , new object[] { pageNumber, pageSize, dateStart, dateEnd, status, cbProductGroup.EditValue, txtFilterText.Text, WarehouseCode });

                    LoadDocument();
                    grdData.DataSource = dt;
                    if (dt.Rows.Count == 0) return;
                    txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}");
            }
        }


        void LoadDocument()
        {
            if (gridBand2.Columns.Count > 0) gridBand2.Columns.Clear();

            BandedGridColumn colSucces = new BandedGridColumn();
            colSucces.Visible = true;
            colSucces.FieldName = $"IsSuccessText";
            colSucces.Caption = "Trạng thái chứng từ";
            colSucces.OptionsColumn.AllowEdit = false;
            colSucces.OptionsColumn.ReadOnly = true;

            gridBand2.Columns.Add(colSucces);

            List<DocumentImportModel> documents = SQLHelper<DocumentImportModel>.FindByAttribute("IsDeleted", 0);
            foreach (var item in documents)
            {
                BandedGridColumn col = new BandedGridColumn();
                col.Visible = true;
                col.FieldName = $"D{item.ID}";
                col.Caption = item.DocumentImportName;
                col.OptionsColumn.AllowEdit = false;
                col.OptionsColumn.ReadOnly = true;
                col.ColumnEdit = repositoryItemMemoEdit1;

                gridBand2.Columns.Add(col);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {


            //string fileName = $"TONG_HOP_CHI_TIET_PHIEU_XUAT_{WarehouseCode}_{DateTime.Now.ToString("ddMMyy")}";

            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.FileName = fileName;
            //sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            //    {
            //        grvMaster.OptionsSelection.MultiSelect = false;
            //        grvMaster.OptionsPrint.AutoWidth = false;
            //        grvMaster.OptionsPrint.ExpandAllDetails = false;
            //        grvMaster.OptionsPrint.PrintDetails = true;
            //        grvMaster.OptionsPrint.UsePrintStyles = true;
            //        try
            //        {
            //            grvMaster.ExportToXls(sfd.FileName);
            //            Process.Start(sfd.FileName);
            //        }
            //        catch (Exception)
            //        {
            //        }
            //        grvMaster.OptionsSelection.MultiSelect = true;
            //    }
            //}

            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"TONG_HOP_CHI_TIET_PHIEU_XUAT_{WarehouseCode}_{DateTime.Now.ToString("ddMMyy")}.xlsx");

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                //PrintableComponentLink printableComponentLink2 = new PrintableComponentLink(printingSystem);
                //printableComponentLink2.Component = grdDataMisa;

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);
                        //compositeLink.Links.Add(printableComponentLink2);
                        //compositeLink.Links.Add(printableComponentLink3);

                        //compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);
                        Process.Start(filepath);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadBillExportSynthetic();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadBillExportSynthetic();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadBillExportSynthetic();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadBillExportSynthetic();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadBillExportSynthetic();
        }

        private void cbProductGroup_EditValueChanged(object sender, EventArgs e)
        {
            loadBillExportSynthetic();
        }

        private void cboStatusNew_EditValueChanged(object sender, EventArgs e)
        {
            loadBillExportSynthetic();
        }
    }
}
