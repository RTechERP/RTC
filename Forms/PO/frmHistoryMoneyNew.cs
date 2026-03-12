using BMS;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Export;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmHistoryMoneyNew : _Forms
    {
        private int _initialValue;
        public frmHistoryMoneyNew(int pokhid)
        {
            InitializeComponent();
            cboPOCode.EditValue = pokhid;
        }

        private void frmHistoryMoneyNew_Load(object sender, EventArgs e)
        {
            loadPOKH();
            loadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            string billcode = txtBillCode.Text;
            int? pokhid = TextUtils.ToInt(cboPOCode.EditValue);

            grdProductDetail.DataSource = null;
            grdProductDetail.Refresh();

            DataTable dt;
            if (billcode == null || billcode == "")
            {
                dt = TextUtils.GetDataTableFromSP("spGetProductMoneyDetailByPOCode", new string[] { "@POId" }, new object[] { pokhid });
                
            } else
            {
                dt = TextUtils.GetDataTableFromSP("spGetProductMoneyDetailByBillCodeAndPOCode", new string[] { "@BillCode", "@POId" }, new object[] { billcode, pokhid });
                
            }
            grdProductDetail.DataSource = dt;
            grdProductDetail.RefreshDataSource();
        }

        void loadPOKH()
        {
            DataTable dt = TextUtils.Select("SELECT ID, POCode FROM dbo.POKH");

            cboPOCode.Properties.DataSource = dt;
            cboPOCode.Properties.DisplayMember = "POCode";
            cboPOCode.Properties.ValueMember = "ID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtMoneyPaid_TextChanged(object sender, EventArgs e)
        {
            decimal moneynotpaid = TextUtils.ToDecimal(grvProductDetail.GetFocusedRowCellValue("MoneyNotPaid"));
            decimal moneyreceive = TextUtils.ToDecimal(txtMoneyPaid.Text);
            if(moneynotpaid != 0)
            {
                txtMoneyNotPaid.Text = TextUtils.ToString(moneynotpaid - moneyreceive);
            } else
            {
                decimal totalmoney = TextUtils.ToDecimal(grvProductDetail.GetFocusedRowCellValue("IntoMoney"));
                txtMoneyNotPaid.Text = TextUtils.ToString(totalmoney - moneyreceive);
            }
        }
        private void grvProductDetail_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            txtMoneyNotPaid.Text = TextUtils.ToString(grvProductDetail.GetFocusedRowCellValue("MoneyNotPaid"));
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtBillCode.Text == "")
            {
                var rowData = grvProductDetail.GetFocusedRow();
                if (rowData != null)
                {
                    var i = grvProductDetail.FocusedRowHandle;
                    int pokhid = TextUtils.ToInt(cboPOCode.EditValue);
                    decimal money = TextUtils.ToDecimal(txtMoneyPaid.Text);
                    decimal vat = TextUtils.ToDecimal(grvProductDetail.GetRowCellValue(i, "VAT"));
                    decimal moneyvat = money / (vat + 1);
                    HistoryMoneyPOModel historyMoneyPO = new HistoryMoneyPOModel
                    {
                        Number = 0,
                        Money = money,
                        MoneyDate = dtMoneyDate.DateTime,
                        POKHID = pokhid,
                        Note = TextUtils.ToString(grvProductDetail.GetRowCellValue(i, "Note")),
                        ProductID = TextUtils.ToInt(grvProductDetail.GetRowCellValue(i, "ProductID")),
                        VAT = vat,
                        BankName = TextUtils.ToString(grvProductDetail.GetRowCellValue(i, "BankName")),
                        MoneyVAT = moneyvat,
                        POKHDetailID = TextUtils.ToInt(grvProductDetail.GetRowCellValue(i, "POKHDetailID")),
                        ProjectID = 0,
                        InvoiceNo = txtBillCode.Text,
                        IsFilm = false,
                        IsMergePO = false,
                        //CreatedDate = DateTime.Now,
                        //UpdateDate = DateTime.Now,
                        MoneyNotPaid = TextUtils.ToDecimal(txtMoneyNotPaid.Text)
                    };

                    SQLHelper<HistoryMoneyPOModel>.Insert(historyMoneyPO);
                }
            }
            else
            {
                var i = grvProductDetail.FocusedRowHandle;
                decimal money = TextUtils.ToDecimal(txtMoneyPaid.Text);
                decimal vat = TextUtils.ToDecimal(grvProductDetail.GetRowCellValue(i, "VAT"));
                decimal moneyvat = money / (vat + 1);

                HistoryMoneyPOModel historyMoneyPO = new HistoryMoneyPOModel
                {
                    Number = 0,
                    Money = money,
                    MoneyDate = dtMoneyDate.DateTime,
                    POKHID = TextUtils.ToInt(cboPOCode.EditValue),
                    Note = TextUtils.ToString(grvProductDetail.GetRowCellValue(i, "Note")),
                    ProductID = TextUtils.ToInt(grvProductDetail.GetRowCellValue(i, "ProductID")),
                    VAT = TextUtils.ToDecimal(grvProductDetail.GetRowCellValue(i, "VAT")),
                    BankName = TextUtils.ToString(grvProductDetail.GetRowCellValue(i, "BankName")),
                    MoneyVAT = moneyvat,
                    POKHDetailID = TextUtils.ToInt(grvProductDetail.GetRowCellValue(i, "POKHDetailID")),
                    ProjectID = 0,
                    InvoiceNo = txtBillCode.Text,
                    IsFilm = false,
                    IsMergePO = false,
                    //CreatedDate = DateTime.Now,
                    //UpdateDate = DateTime.Now,
                    MoneyNotPaid = TextUtils.ToDecimal(txtMoneyNotPaid.Text)
                };

                SQLHelper<HistoryMoneyPOModel>.Insert(historyMoneyPO);
            }
            MessageBox.Show("Đã update tiền về thành công");
            loadData();
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int pokhid = TextUtils.ToInt(cboPOCode.EditValue);

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel Files|*.xlsx";
            dialog.FileName = $"MoneyDetail_{pokhid}_{DateTime.Now.ToString("ddMMyyyy_HHmm")}.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {
                    string filepath = dialog.FileName;

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    printableComponentLink1.Component = grdProductDetail;

                    try
                    {
                        foreach (DevExpress.XtraGrid.Columns.GridColumn column in grvProductDetail.Columns)
                        {
                            column.Visible = true;
                        }

                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);

                        // Restore hidden columns after export
                        foreach (DevExpress.XtraGrid.Columns.GridColumn column in grvProductDetail.Columns)
                        {
                            column.Visible = false;
                        }

                        Process.Start(filepath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
}
