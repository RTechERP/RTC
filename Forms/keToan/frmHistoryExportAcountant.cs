using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using Forms.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace BMS
{
    public partial class frmHistoryExportAcountant : _Forms
    {
        public frmHistoryExportAcountant()
        {
            InitializeComponent();
        }

        private void frmHistoryExportAcountant_Load(object sender, EventArgs e)
        {

            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";
            cboStatus.SelectedIndex = 0;
            loadHistoryExportAcountant();
        }

        private void loadHistoryExportAcountant()
        {

            if (cboStatus.SelectedIndex == 0)
            {
                colInvoiceNumberAcountant.Visible = colCreatedDateAcountant.Visible = colUnitPrice.Visible = colIntoMoney.Visible = colIntoMoneyWithoutVat.Visible = colTotalIntoMoney.Visible = colVAT.Visible = false;
                grvData.OptionsView.ColumnAutoWidth = true;
            }
            else if (cboStatus.SelectedIndex == 1)
            {
                colInvoiceNumberAcountant.Visible = colCreatedDateAcountant.Visible = colUnitPrice.Visible = colIntoMoney.Visible = colIntoMoneyWithoutVat.Visible = colTotalIntoMoney.Visible = colVAT.Visible = true;
                colCreatedDateAcountant.VisibleIndex = 14; colUnitPrice.VisibleIndex = 15; colIntoMoneyWithoutVat.VisibleIndex = 16; colVAT.VisibleIndex = 17; colIntoMoney.VisibleIndex = 18; colTotalIntoMoney.VisibleIndex = 19;
                grvData.OptionsView.ColumnAutoWidth = false;
            }

            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetHistoryExportAcountant"
                   , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@FilterText", "@Status" }
                   , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtFilterText.Text.Trim(), cboStatus.SelectedIndex });
            grdData.DataSource = oDataSet.Tables[0];
            if (oDataSet.Tables[0].Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[0].Rows[0]["TotalPage"]);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadHistoryExportAcountant();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvData.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadHistoryExportAcountant();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadHistoryExportAcountant();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadHistoryExportAcountant();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            loadHistoryExportAcountant();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadHistoryExportAcountant();
        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colQty || e.Column == colUnitPrice || e.Column == colVAT)
            {
                if (cboStatus.SelectedIndex == 1)
                {
                    int qty = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQty));
                    decimal unitPrice = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colUnitPrice));
                    if (unitPrice > 0 && qty > 0)
                    {
                        decimal intoMoneyWithoutVat = TextUtils.ToDecimal(qty) * TextUtils.ToDecimal(unitPrice);
                        grvData.SetFocusedRowCellValue(colIntoMoneyWithoutVat, intoMoneyWithoutVat);
                    }
                }
            }
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }
    }
}
