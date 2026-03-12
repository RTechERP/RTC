using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.CodeParser;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.VisualBasic;
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
using Color = System.Drawing.Color;
using Excel = Microsoft.Office.Interop.Excel;

namespace BMS
{
    public partial class frmBillImportSynthetic : _Forms
    {
        public string WarehouseCode;

        //List<BillImportDetailModel> billImportDetails = new List<BillImportDetailModel>();


        DataTable dtSynthetic = new DataTable();
        public frmBillImportSynthetic()
        {
            InitializeComponent();
        }

        private void frmBillImportSynthetic_Load(object sender, EventArgs e)
        {
            this.Text += " - " + WarehouseCode;
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";

            loadProductGroup();
            cbProductGroup.CheckAll();
            LoadBillType();
            LoadBillImportSynthetic();

            grdData.ContextMenuStrip = contextMenuStrip1;

            //LogActions();
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
            DataTable dt = TextUtils.Select($"Select * from DocumentSale where BillID = {ID}  AND BillType = 1");
            grdData.DataSource = dt;
        }

        void LoadBillType()
        {
            List<object> list = new List<object>()
            {
                new {ID = -1, Name = "--Tất cả--"},
                new {ID = 0, Name = "Phiếu nhập kho"},
                new {ID = 1, Name = "Phiếu trả"},
                //new {ID = 2, Name = "PTNB"},
                new {ID = 3, Name = "Phiếu mượn NCC"}
            };
            cboBillTypeNew.Properties.DataSource = list;
            cboBillTypeNew.Properties.ValueMember = "ID";
            cboBillTypeNew.Properties.DisplayMember = "Name";
            cboBillTypeNew.EditValue = -1;
        }

        void LoadBillImportSynthetic()
        {
            try
            {
                grdData.DataSource = null;
                DateTime dateTimeS = new DateTime();
                if (!chkAllBillImport.Checked)
                {
                    dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
                    dtpFromDate.Enabled = dtpEndDate.Enabled = true;
                }
                else
                {
                    DataTable dtMinCreateDate = TextUtils.Select("SELECT MIN(CreatDate) as MinCreatDate  FROM [dbo].[BillImport]");
                    string[] minCreate = dtMinCreateDate.Rows[0]["MinCreatDate"].ToString().Split('/', ' ');
                    dateTimeS = new DateTime(TextUtils.ToInt(minCreate[2]), TextUtils.ToInt(minCreate[1]), TextUtils.ToInt(minCreate[0]), 0, 0, 0);
                    dtpFromDate.Enabled = dtpEndDate.Enabled = false;
                }

                DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
                int billType = TextUtils.ToInt(cboBillTypeNew.EditValue);


                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu..."))
                {

                    dtSynthetic = TextUtils.LoadDataFromSP("spGetBillImportSynthetic", "A"
                                               , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@KhoType", "@FilterText", "@WarehouseCode" }
                                               , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value),
                   dateTimeS, dateTimeE, billType, cbProductGroup.EditValue, txtFilterText.Text,WarehouseCode });

                    LoadDocument();
                    grdData.DataSource = dtSynthetic;

                    if (dtSynthetic.Rows.Count == 0) return;
                    txtTotalPage.Text = TextUtils.ToString(dtSynthetic.Rows[0]["TotalPage"]);
                }
            }
            catch (Exception ex) { MessageBox.Show($"{ex.ToString()}"); }
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
                col.ColumnEdit = repositoryItemMemoEdit5;

                gridBand2.Columns.Add(col);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

            //string fileName = $"TONG_HOP_CHI_TIET_PHIEU_NHAP_{WarehouseCode}_{DateTime.Now.ToString("ddMMyy")}";
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
                string filepath = Path.Combine(f.SelectedPath, $"TONG_HOP_CHI_TIET_PHIEU_NHAP_{WarehouseCode}_{DateTime.Now.ToString("ddMMyy")}.xlsx");

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

        private void cboBillTypeNew_EditValueChanged(object sender, EventArgs e)
        {
            //btnFind_Click(null, null);
        }

        private void cbProductGroup_EditValueChanged(object sender, EventArgs e)
        {
            //btnFind_Click(null, null);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadBillImportSynthetic();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadBillImportSynthetic();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadBillImportSynthetic();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadBillImportSynthetic();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadBillImportSynthetic();
        }

        bool isRecallCellValueChanged = false;
        Dictionary<int, string> dataDateSomeBill = new Dictionary<int, string>();
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //grvData.CloseEditor();
            //if (e.Column == colSomeBill || e.Column == colDateSomeBill)
            //{
            //    grvData.SetFocusedRowCellValue(colIsUpdated, 1);
            //}

            if (isRecallCellValueChanged == true) return;
            try
            {

                using (WaitDialogForm fWait = new WaitDialogForm())
                {
                    isRecallCellValueChanged = true;
                    grvData.CloseEditor();

                    if (e.Column.FieldName == colSomeBill.FieldName || e.Column.FieldName == colDateSomeBill.FieldName || e.Column.FieldName == colDPO.FieldName)
                    {
                        if (e.Value == null) return;

                        int[] selectedRows = grvData.GetSelectedRows();
                        if (selectedRows.Length > 0)
                        {
                            foreach (int row in selectedRows)
                            {
                                grvData.SetRowCellValue(row, grvData.Columns[e.Column.FieldName], e.Value);

                                if (e.Column.FieldName == colDateSomeBill.FieldName)
                                {
                                    string dateText = grvData.GetRowCellDisplayText(row, colDateSomeBill);
                                    grvData.SetRowCellValue(row, colDateSomeBillText, dateText);
                                }


                                int dpo = TextUtils.ToInt(grvData.GetRowCellValue(row, colDPO));
                                DateTime? dateSomeBill = TextUtils.ToDate4(grvData.GetRowCellDisplayText(row, colDateSomeBill));
                                if (dateSomeBill.HasValue)
                                {
                                    DateTime dueDate = dateSomeBill.Value.AddDays(dpo);
                                    grvData.SetRowCellValue(row, colDueDate, dueDate);
                                }

                                grvData.SetRowCellValue(row, colIsUpdated, 1);
                            }
                        }
                        else
                        {
                            int row = grvData.FocusedRowHandle;
                            int dpo = TextUtils.ToInt(grvData.GetRowCellValue(row, colDPO));
                            DateTime? dateSomeBill = TextUtils.ToDate4(grvData.GetRowCellDisplayText(row, colDateSomeBill));
                            if (dateSomeBill.HasValue)
                            {
                                DateTime dueDate = dateSomeBill.Value.AddDays(dpo);
                                grvData.SetRowCellValue(row, colDueDate, dueDate);
                            }
                        }

                        if (e.Column.FieldName == colDateSomeBill.FieldName)
                        {
                            string dateText = grvData.GetRowCellDisplayText(e.RowHandle, colDateSomeBill);
                            grvData.SetRowCellValue(e.RowHandle, colDateSomeBillText, dateText);
                        }
                        grvData.SetRowCellValue(e.RowHandle, colIsUpdated, 1);
                    }

                }
            }
            finally
            {
                isRecallCellValueChanged = false;
            }


        }
        bool isAdmin = (Global.IsAdmin && Global.EmployeeID <= 0);//|| Global.EmployeeID  == 178; //tbp mua
        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            try
            {
                grvData.CloseEditor();
                grvData.FocusedRowHandle = -1;
                DataTable dtChange = dtSynthetic.GetChanges();
                if (dtChange == null) return;
                foreach (DataRow row in dtChange.Rows)
                {
                    //bool isUpdated = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsUpdated));
                    //if (!isUpdated) continue;
                    int id = TextUtils.ToInt(row[colIDDetail.FieldName]);
                    if (id <= 0) continue;
                    int deliverID = TextUtils.ToInt(row[colDeliverID.FieldName]);
                    if (deliverID != Global.UserID && !isAdmin) continue;

                    //BillImportDetailModel model = SQLHelper<BillImportDetailModel>.FindByID(id);
                    //model.SomeBill = TextUtils.ToString(row[colSomeBill.FieldName]); ;
                    //model.DateSomeBill = TextUtils.ToDate4(row[colDateSomeBill.FieldName]); ;
                    //model.UpdatedBy = Global.AppUserName;
                    //model.UpdatedDate = DateTime.Now;

                    int dpo = TextUtils.ToInt(row[colDPO.FieldName]);
                    DateTime? dateSomeBill = TextUtils.ToDate4(row[colDateSomeBillText.FieldName]);
                    DateTime? dueDate = null;
                    if (dateSomeBill.HasValue) dueDate = dateSomeBill.Value.AddDays(dpo);
                    
                    
                    
                    var myDict = new Dictionary<string, object>()
                    {
                        { BillImportDetailModel_Enum.SomeBill.ToString(),TextUtils.ToString(row[colSomeBill.FieldName])},
                        //{ BillImportDetailModel_Enum.DateSomeBill.ToString(),TextUtils.ToDate4(row[colDateSomeBill.FieldName])},
                        { BillImportDetailModel_Enum.DateSomeBill.ToString(),dateSomeBill},
                        { BillImportDetailModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                        { BillImportDetailModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                        { BillImportDetailModel_Enum.DPO.ToString(),dpo},
                        { BillImportDetailModel_Enum.DueDate.ToString(),dueDate},
                        //{ BillImportDetailModel_Enum.TaxReduction.ToString(),dueDate},
                        { BillImportDetailModel_Enum.TaxReduction.ToString(),TextUtils.ToDecimal(row[colTaxReduction.FieldName])},
                        { BillImportDetailModel_Enum.COFormE.ToString(),TextUtils.ToDecimal(row[colCOFormE.FieldName])},
                    };

                    //DateTime? dateSomeBill = TextUtils.ToDate4(row[colDateSomeBill.FieldName]);
                    //if (dateSomeBill.HasValue) myDict.Add(BillImportDetailModel_Enum.DateSomeBill.ToString(), dateSomeBill.Value);

                    SQLHelper<BillImportDetailModel>.UpdateFieldsByID(myDict, id);
                }

                //for (int i = 0; i < grvData.RowCount; i++)
                //{
                //    bool isUpdated = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsUpdated));
                //    if (!isUpdated) continue;
                //    int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colIDDetail));
                //    if (id <= 0) continue;
                //    int deliverID = TextUtils.ToInt(grvData.GetRowCellValue(i, colDeliverID));

                //    if (deliverID != Global.UserID && !isAdmin) continue;


                //    BillImportDetailModel model = SQLHelper<BillImportDetailModel>.FindByID(id);
                //    model.SomeBill = TextUtils.ToString(grvData.GetRowCellValue(i, colSomeBill)); ;
                //    model.DateSomeBill = TextUtils.ToDate4(grvData.GetRowCellValue(i, colDateSomeBill)); ;
                //    model.UpdatedBy = Global.AppUserName;
                //    model.UpdatedDate = DateTime.Now;
                //    SQLHelper<BillImportDetailModel>.Update(model);
                //}


                //if (billImportDetails.Count <= 0) return;
                //foreach (var item in billImportDetails)
                //{
                //    if (item.ID <= 0) continue;
                //    BillImportDetailModel model = SQLHelper<BillImportDetailModel>.FindByID(item.ID);
                //    model.SomeBill = item.SomeBill;
                //    model.DateSomeBill = item.DateSomeBill;
                //    model.UpdatedBy = Global.AppUserName;
                //    model.UpdatedDate = DateTime.Now;
                //    SQLHelper<BillImportDetailModel>.Update(model);
                //}

                MessageBox.Show("Lưu thành công!", "Thông báo");
                //billImportDetails.Clear();

                dtSynthetic.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void grvData_ShowingEditor(object sender, CancelEventArgs e)
        {
            int deliverID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colDeliverID));
            if (deliverID != Global.UserID && !isAdmin)
            {
                e.Cancel = true;
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

        private void grvData_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.ForeColor = Color.Black;
                //e.HighPriority = true;
            }
        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void btnDocumentAdditional_DropDownOpening(object sender, EventArgs e)
        {
            if (btnDocumentAdditional.DropDownItems.Count > 0) btnDocumentAdditional.DropDownItems.Clear();
            List<DocumentImportModel> documents = SQLHelper<DocumentImportModel>.FindByAttribute("IsDeleted", 0);
            foreach (DocumentImportModel document in documents)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = $"btnProjectStatus{document.ID}";
                item.Text = $"{document.DocumentImportName}";
                item.Tag = $"{document.ID}";
                item.Click += Item_Click;
                btnDocumentAdditional.DropDownItems.Add(item);
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem toolStrip = (ToolStripMenuItem)sender;
                int documentImportID = TextUtils.ToInt(toolStrip.Tag);

                int[] selectedRows = grvData.GetSelectedRows();

                DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn bổ sung chứng từ ${toolStrip.Text} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog != DialogResult.Yes) return;
                foreach (int row in selectedRows)
                {
                    int ponccID = TextUtils.ToInt(grvData.GetRowCellValue(row, colPONCCID));
                    if (ponccID <= 0) continue;

                    int deliverID = TextUtils.ToInt(grvData.GetRowCellValue(row, colDeliverID));
                    if (deliverID != Global.UserID && !isAdmin) continue;

                    var exp1 = new Utils.Expression(DocumentImportPONCCModel_Enum.PONCCID, ponccID);
                    var exp2 = new Utils.Expression(DocumentImportPONCCModel_Enum.DocumentImportID, documentImportID);
                    DocumentImportPONCCModel documentImport = SQLHelper<DocumentImportPONCCModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault() ?? new DocumentImportPONCCModel();

                    //documentImport.PONCCID = ponccID;
                    documentImport.DocumentImportID = documentImportID;
                    documentImport.IsAdditional = documentImport.ID > 0;
                    documentImport.EmployeeAdditionalID = Global.EmployeeID;
                    documentImport.DateAdditional = DateTime.Now;

                    int billImportID = TextUtils.ToInt(grvData.GetRowCellValue(row, colIDMaster));
                    documentImport.BillImportID = billImportID;

                    if (documentImport.ID <= 0) SQLHelper<DocumentImportPONCCModel>.Insert(documentImport);
                    else if (documentImport.Status != 1)
                    {
                        SQLHelper<DocumentImportPONCCModel>.Update(documentImport);
                    }
                }


                MessageBox.Show("Bổ sung chứng từ thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void grvData_RowStyle_1(object sender, RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                //e.HighPriority = true;
            }
        }


        #region Logs
        private void LogActions()
        {
            try
            {
                var controls = new Component[] { btnSaveBill, grdData };
                var actions = Enumerable.Repeat<string>("Update", controls.Length).ToArray();
                var logDatas = Enumerable.Repeat<Func<dynamic, dynamic>>(GetDataChange, controls.Length).ToArray();
                var initialData = GetCurrentData();
                var logger = new Logger(controls, actions, logDatas, this, initialData);
                logger.Start();
            }
            catch
            {

            }
        }

        private BillImportLog GetCurrentData()
        {
            var data = new BillImportLog();
            //var billImportLog = new BillImportModel();
            //billImportLog.ID = billImport.ID;

            //billImportLog.BillImportCode = txtBilllNumber.Text.Trim();
            //if (dtpCreatDate.EditValue != null)
            //{
            //    billImportLog.CreatDate = (DateTime)dtpCreatDate.EditValue;
            //}
            //billImportLog.Deliver = cboDeliver.Text.Trim();
            //billImportLog.Reciver = cboReciver.Text.Trim();
            //billImportLog.Suplier = cboSupplier.Text.Trim();
            //billImportLog.KhoType = cbKhoType.Text.Trim();
            //billImportLog.DeliverID = TextUtils.ToInt(cboDeliver.EditValue);
            //billImportLog.ReciverID = TextUtils.ToInt(cboReciver.EditValue);
            //billImportLog.SupplierID = TextUtils.ToInt(cboSupplier.EditValue);
            //billImportLog.GroupID = TextUtils.ToString(cboGroup.EditValue);
            //billImportLog.KhoTypeID = TextUtils.ToInt(cbKhoType.EditValue);
            //billImportLog.WarehouseID = TextUtils.ToInt(cboWarehouse.EditValue); ;
            //billImportLog.BillTypeNew = TextUtils.ToInt(cboBillTypeNew.EditValue);
            //if (billImportLog.BillTypeNew == 4)
            //{
            //    billImportLog.DateRequestImport = dtpDateRequestImport.Value;
            //}
            //billImportLog.UpdatedDate = DateTime.Now;
            //billImportLog.UpdatedBy = Global.AppUserName;
            //billImportLog.Status = false;
            var detailsLog = new List<BillImportDetailModel>();
            var invProjectLog = new List<InventoryProjectModel>();

            grvData.CloseEditForm();
            grvData.FocusedRowHandle = -1;

            DataTable dtChange = dtSynthetic.GetChanges();
            if (dtChange != null)
            {
                foreach (DataRow row in dtChange.Rows)
                {
                    BillImportDetailModel detail = new BillImportDetailModel
                    {
                        ID = TextUtils.ToInt(row[colIDDetail.FieldName]),
                        //BillImportID = TextUtils.ToInt(grvData.GetRowCellValue(i, colIDDetail)),
                        //ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID)),
                        //Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty)),
                        //Price = TextUtils.ToInt(grvData.GetRowCellValue(i, colPrice)),
                        //ProjectCode = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectCode)),
                        SomeBill = TextUtils.ToString(row[colSomeBill.FieldName]),
                        //Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote)),
                        //STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT)),
                        //TotalQty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalQty)),
                        //ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID)),
                        //PONCCDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colPONCCDetailID)),
                        //SerialNumber = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumber)),
                        //CodeMaPhieuMuon = TextUtils.ToString(grvData.GetRowCellValue(i, colPM)),
                        //BillExportDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colBorrowID)),
                        //QtyRequest = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQtyRequest)),
                        //BillCodePO = TextUtils.ToString(grvData.GetRowCellValue(i, colBillCodePO)),
                        //ProjectPartListID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectPartListID)),
                        //IsKeepProject = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsKeepProject)),
                        //ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectNameText)),
                        DateSomeBill = TextUtils.ToDate4(row[colDateSomeBillText.FieldName])
                    };
                    detailsLog.Add(detail);
                    string projectNameText = TextUtils.ToString(row[colProjectNameText.FieldName]);
                }

                //for (int i = 0; i < dtChange.Rows.Count; i++)
                //{
                //    BillImportDetailModel detail = new BillImportDetailModel
                //    {
                //        ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colIDDetail)),
                //        //BillImportID = TextUtils.ToInt(grvData.GetRowCellValue(i, colIDDetail)),
                //        //ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID)),
                //        //Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty)),
                //        //Price = TextUtils.ToInt(grvData.GetRowCellValue(i, colPrice)),
                //        //ProjectCode = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectCode)),
                //        SomeBill = TextUtils.ToString(grvData.GetRowCellValue(i, colSomeBill)),
                //        //Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote)),
                //        //STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT)),
                //        //TotalQty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalQty)),
                //        //ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID)),
                //        //PONCCDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colPONCCDetailID)),
                //        //SerialNumber = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumber)),
                //        //CodeMaPhieuMuon = TextUtils.ToString(grvData.GetRowCellValue(i, colPM)),
                //        //BillExportDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colBorrowID)),
                //        //QtyRequest = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQtyRequest)),
                //        //BillCodePO = TextUtils.ToString(grvData.GetRowCellValue(i, colBillCodePO)),
                //        //ProjectPartListID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectPartListID)),
                //        //IsKeepProject = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsKeepProject)),
                //        //ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectNameText)),
                //        DateSomeBill = TextUtils.ToDate4(grvData.GetRowCellDisplayText(i, colDateSomeBill))
                //    };
                //    detailsLog.Add(detail);
                //    string projectNameText = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectNameText));
                //    //if (detail.ProjectID > 0 || string.IsNullOrWhiteSpace(projectNameText))
                //    //{
                //    //    InventoryProjectModel inventoryProject = new InventoryProjectModel();
                //    //    inventoryProject.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colInventoryProjectID));
                //    //    inventoryProject.ProjectID = detail.ProjectID;
                //    //    inventoryProject.ProductSaleID = detail.ProductID;
                //    //    inventoryProject.WarehouseID = billImport.WarehouseID;
                //    //    inventoryProject.Quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colProjectPartListQuantity));
                //    //    inventoryProject.Note = projectNameText;
                //    //    invProjectLog.Add(inventoryProject);
                //    //}
                //    //else
                //    //{
                //    //    invProjectLog.Add(null);
                //    //}
                //}
            }
            //data.BillImport = billImportLog;
            data.Details = detailsLog;
            //data.InvPrj = invProjectLog;
            return data;
        }

        private dynamic GetDataChange(dynamic oldData)
        {
            var oldDataLog = (BillImportLog)oldData;
            var newDataLog = GetCurrentData();
            return new
            {
                BillIport = new
                {
                    Old = oldDataLog.BillImport,
                    New = newDataLog.BillImport
                },
                Details = new
                {
                    Old = oldDataLog.Details,
                    New = newDataLog.Details
                },
                InvPrj = new
                {
                    Old = oldDataLog.InvPrj,
                    New = newDataLog.InvPrj
                }
            };
        }

        public class BillImportLog
        {
            public BillImportModel BillImport;
            public List<BillImportDetailModel> Details;
            public List<InventoryProjectModel> InvPrj;
        }
        #endregion
    }
}
