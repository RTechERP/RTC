using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using Forms.Enums;
using Forms.Technical;
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
    public partial class frmProductReportNew : _Forms
    {
        private int warehouseID;
        //public frmProductReportNew()
        //{
        //    InitializeComponent();
        //}
        public frmProductReportNew(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }

        private void frmProductReportNew_Load(object sender, EventArgs e)
        {
            cboStatus.SelectedIndex = 1;
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";
            cboBillType.SelectedIndex = 0;
            loadHistoryImportExport();
            //this.Text += warehouseID == 1 ? " - HN" : (warehouseID == 2 ? " - HCM" : " - BN");
            this.Text += " - " + this.Tag;
        }

        private void loadHistoryImportExport()
        {

            if (cboStatus.SelectedIndex == 0)
            {
                colBillCode.Visible = colCreatDateImport.Visible = true;
                colCode.Visible = colCreatDateExport.Visible = false;
                colReciver_Import.Visible = true;
                colDeliver_import.Visible = true;
                colFullName_Export.Visible = false;
                colReceiver_Export.Visible = false;
                colProjectName.Visible = false;
            }
            else if (cboStatus.SelectedIndex == 1)
            {
                colBillCode.Visible = colCreatDateImport.Visible = false;
                colCode.Visible = colCreatDateExport.Visible = true;
                colCode.VisibleIndex = 2;
                colReciver_Import.Visible = false;
                colDeliver_import.Visible = false;
                colFullName_Export.Visible = true;
                colReceiver_Export.Visible = true;
                colProjectName.Visible = true;
            }
            else
            {
                return;
            }

            DateTime dateTimeS = new DateTime();
            if (!chkAllHistoryImportExport.Checked)
            {
                dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
                dtpFromDate.Enabled = dtpEndDate.Enabled = true;
            }
            else
            {
                DataTable dtMinCreateDate = new DataTable();
                if (cboStatus.SelectedIndex == 0)
                {
                    dtMinCreateDate = TextUtils.Select($"SELECT MIN(CreatDate) as MinCreatDate FROM [dbo].[BillImportTechnical] WHERE WarehouseID = {warehouseID}");
                }
                else if (cboStatus.SelectedIndex == 1)
                {
                    dtMinCreateDate = TextUtils.Select($"SELECT MIN(CreatedDate) as MinCreatDate FROM [dbo].[BillExportTechnical] WHERE WarehouseID = {warehouseID}");
                }
                string[] minCreate = dtMinCreateDate.Rows[0]["MinCreatDate"].ToString().Split('/', ' ');
                dateTimeS = new DateTime(TextUtils.ToInt(minCreate[2]), TextUtils.ToInt(minCreate[1]), TextUtils.ToInt(minCreate[0]), 0, 0, 0);
                dtpFromDate.Enabled = dtpEndDate.Enabled = false;
            }

            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);


            int billtype = cboBillType.SelectedIndex - 1;
            int receiverid = billtype == 3 ? 104 : 0;

            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetExportImportTechnical"
                   , new string[] { "@PageNumber", "@PageSize", "@FilterText", "@DateStart", "@DateEnd", "@Status", "@WarehouseID", "@BillType", "@ReceiverID" }
                   , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), txtFilterText.Text.Trim(), dateTimeS, dateTimeE, cboStatus.SelectedIndex, warehouseID, billtype, receiverid });
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
            grdData.DataSource = oDataSet.Tables[0];
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadHistoryImportExport();
        }

        private void cboStatus_TextChanged(object sender, EventArgs e)
        {
            //loadLocPhieu();

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
            loadHistoryImportExport();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadHistoryImportExport();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadHistoryImportExport();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            loadHistoryImportExport();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadHistoryImportExport();
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {

        }

        private void chiTiếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            if (cboStatus.SelectedIndex == 1)
            {
                BillExportTechnicalModel model = (BillExportTechnicalModel)BillExportTechnicalBO.Instance.FindByPK(ID);
                frmBillExportTechDetail_New frm = new frmBillExportTechDetail_New(warehouseID);
                frm.billExport = model;
                frm.IDDetail = ID;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                }
            }
            else
            {
                BillImportTechnicalModel model = (BillImportTechnicalModel)BillImportTechnicalBO.Instance.FindByPK(ID);
                frmBillImportTechDetail_New frm = new frmBillImportTechDetail_New(warehouseID);
                frm.billImport = model;
                frm.IDDetail = ID;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadHistoryImportExport();

        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void chkAllHistoryImportExport_CheckedChanged(object sender, EventArgs e)
        {
            loadHistoryImportExport();
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void cboBillType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadHistoryImportExport();
        }
    }
}
