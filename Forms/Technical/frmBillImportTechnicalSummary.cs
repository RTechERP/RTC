using BMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Utils;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.Diagnostics;
using System.IO;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace Forms.Technical
{
    public partial class frmBillImportTechnicalSummary: _Forms
    {
        //public string WarehouseCode;
        public int WarehouseId;
        
        DataTable dtSummary = new DataTable();

        public frmBillImportTechnicalSummary(int warehouseId)
        {
            WarehouseId = warehouseId;
            InitializeComponent();
        }

        private void frmBillImportTechnicalSummary_Load(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            LoadBillType();
            LoadBillImportTechnicalSummary();

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

        void LoadBillImportTechnicalSummary()
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
                    DataTable dtMinCreateDate = TextUtils.Select("SELECT MIN(CreatDate) as MinCreatDate  FROM [dbo].[BillImportTechnical]");
                    string[] minCreate = dtMinCreateDate.Rows[0]["MinCreatDate"].ToString().Split('/', ' ');
                    dateTimeS = new DateTime(TextUtils.ToInt(minCreate[2]), TextUtils.ToInt(minCreate[1]), TextUtils.ToInt(minCreate[0]), 0, 0, 0);
                    dtpFromDate.Enabled = dtpEndDate.Enabled = false;
                }

                DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
                int billType = TextUtils.ToInt(cboBillTypeNew.EditValue);


                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu..."))
                {

                    dtSummary = TextUtils.LoadDataFromSP("spGetBillImportTechnicalSummary", "A"
                                               , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd"
                                               , "@Status", "@FilterText", "@WarehouseId" }
                                               , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value),
                   dateTimeS, dateTimeE, billType,  txtFilterText.Text, WarehouseId });

                    LoadDocument();
                    grdData.DataSource = dtSummary;

                    if (dtSummary.Rows.Count == 0) return;
                    txtTotalPage.Text = TextUtils.ToString(dtSummary.Rows[0]["TotalPage"]);
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadBillImportTechnicalSummary();
        }

        bool isAdmin = (Global.IsAdmin && Global.EmployeeID <= 0);//|| Global.EmployeeID  == 178; //tbp mua
        private void btnSaveBill_Click_1(object sender, EventArgs e)
        {
            try
            {
                grvData.CloseEditor();
                grvData.FocusedRowHandle = -1;
                DataTable dtChange = dtSummary.GetChanges();
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
                    DateTime? dateSomeBill = TextUtils.ToDate4(row[colDateSomeBill.FieldName]);
                    DateTime? dueDate = null;
                    if (dateSomeBill.HasValue) dueDate = dateSomeBill.Value.AddDays(dpo);



                    var myDict = new Dictionary<string, object>()
                    {
                        { BillImportDetailTechnicalModel_Enum.SomeBill.ToString(),TextUtils.ToString(row[colSomeBill.FieldName])},
                        //{ BillImportDetailTechnicalModel_Enum.DateSomeBill.ToString(),TextUtils.ToDate4(row[colDateSomeBill.FieldName])},
                        { BillImportDetailTechnicalModel_Enum.DateSomeBill.ToString(),dateSomeBill},
                        { BillImportDetailTechnicalModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                        { BillImportDetailTechnicalModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                        { BillImportDetailTechnicalModel_Enum.DPO.ToString(),dpo},
                        { BillImportDetailTechnicalModel_Enum.DueDate.ToString(),dueDate},
                        //{ BillImportDetailTechnicalModel_Enum.TaxReduction.ToString(),dueDate},
                        { BillImportDetailTechnicalModel_Enum.TaxReduction.ToString(),TextUtils.ToDecimal(row[colTaxReduction.FieldName])},
                        { BillImportDetailTechnicalModel_Enum.COFormE.ToString(),TextUtils.ToDecimal(row[colCOFormE.FieldName])},
                    };

                    //DateTime? dateSomeBill = TextUtils.ToDate4(row[colDateSomeBill.FieldName]);
                    //if (dateSomeBill.HasValue) myDict.Add(BillImportDetailModel_Enum.DateSomeBill.ToString(), dateSomeBill.Value);

                    SQLHelper<BillImportDetailTechnicalModel>.UpdateFieldsByID(myDict, id);
                }

                //for (int i = 0; i < grvData.RowCount; i++)
                //{
                //    bool isUpdated = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsUpdated));
                //    if (!isUpdated) continue;
                //    int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colIDDetail));
                //    if (id <= 0) continue;
                //    int deliverID = TextUtils.ToInt(grvData.GetRowCellValue(i, colDeliverID));

                //    if (deliverID != Global.UserID && !isAdmin) continue;


                //    BillImportDetailTechnicalModel model = SQLHelper<BillImportDetailTechnicalModel>.FindByID(id);
                //    model.SomeBill = TextUtils.ToString(grvData.GetRowCellValue(i, colSomeBill)); ;
                //    model.DateSomeBill = TextUtils.ToDate4(grvData.GetRowCellValue(i, colDateSomeBill)); ;
                //    model.UpdatedBy = Global.AppUserName;
                //    model.UpdatedDate = DateTime.Now;
                //    SQLHelper<BillImportDetailTechnicalModel>.Update(model);
                //}


                //if (billImportDetails.Count <= 0) return;
                //foreach (var item in billImportDetails)
                //{
                //    if (item.ID <= 0) continue;
                //    BillImportDetailModel model = SQLHelper<BillImportDetailTechnicalModel>.FindByID(item.ID);
                //    model.SomeBill = item.SomeBill;
                //    model.DateSomeBill = item.DateSomeBill;
                //    model.UpdatedBy = Global.AppUserName;
                //    model.UpdatedDate = DateTime.Now;
                //    SQLHelper<BillImportDetailTechnicalModel>.Update(model);
                //}

                MessageBox.Show("Lưu thành công!", "Thông báo");
                //billImportDetails.Clear();

                dtSummary.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void btnExcel_Click_1(object sender, EventArgs e) // NTA B -- update 05/08/2025
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {

                var WarehouseCode = TextUtils.Select("SELECT WarehouseCode FROM Warehouse WHERE Id = " + WarehouseId);
                string filepath = Path.Combine(f.SelectedPath, $"TONG_HOP_CHI_TIET_PHIEU_NHAP_P.KT_{WarehouseCode}_{DateTime.Now.ToString("ddMMyy")}.xlsx");
        

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

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //Nguyễn Tuấn Anh B 01/08/2025
            if (e.Column == colDPO || e.Column == colDPO)
            {
                int row = grvData.FocusedRowHandle;
                int DPO = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colDPO));
                DateTime? dateSomeBill = TextUtils.ToDate4(grvData.GetRowCellDisplayText(row, colDateSomeBill));
                if (dateSomeBill.HasValue)
                {
                    DateTime dueDate = dateSomeBill.Value.AddDays(DPO);
                    grvData.SetRowCellValue(row, colDueDate, dueDate);
                }
            }
        }

        private void chkAllBillImport_CheckedChanged(object sender, EventArgs e)
        {
            LoadBillImportTechnicalSummary();
        }


        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadBillImportTechnicalSummary();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadBillImportTechnicalSummary();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadBillImportTechnicalSummary();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadBillImportTechnicalSummary();
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column != colBillCode) return;
            int isSuccess = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colIsSuccess));
            if (isSuccess == 0)
            {
                e.Appearance.BackColor = Color.Orange;
            }
        }
    }
}
