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
    public partial class frmHistoryImportExport : _Forms
    {
        string _warehouseCode = "";
        public frmHistoryImportExport()
        {
            InitializeComponent();
        }

        public frmHistoryImportExport(string warehouseCode)
        {
            InitializeComponent();
            _warehouseCode = warehouseCode;
        }

        private void frmHistoryImportExport_Load(object sender, EventArgs e)
        {
            cboStatus.SelectedIndex = 0;
            this.Text += " - " + _warehouseCode;
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            //dtpFromDate.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";
            loadHistoryImportExport();
        }

        private void loadHistoryImportExport()
        {
            if (cboStatus.SelectedIndex == 0)
            {
                colBillImportCode.Visible = colSuplier.Visible = colCreatDateImport.Visible = colSomeBill.Visible = true;
                colCode.Visible = colCustomer.Visible = colAddress.Visible = colCreatDateExport.Visible = false;
                colReciver.Visible = true;
                colFullName.Visible = false;
                colDeliver.Visible = true;
                colReceiver.Visible = false;
            }
            else if (cboStatus.SelectedIndex == 1)
            {
                colBillImportCode.Visible = colSuplier.Visible = colCreatDateImport.Visible = colSomeBill.Visible = false;
                colCode.Visible = colCustomer.Visible = colAddress.Visible = colCreatDateExport.Visible = true;
                colCode.VisibleIndex = 0; colCustomer.VisibleIndex = 1; colAddress.VisibleIndex = 2; colCreatDateExport.VisibleIndex = 3;
                colReciver.Visible = false;
                colFullName.Visible = true;
                colDeliver.Visible = false;
                colReceiver.Visible = true;
            }

            //DateTime dateTimeS = new DateTime();
            //if (!chkAllHistoryImportExport.Checked)
            //{
            //    dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            //    dtpFromDate.Enabled = dtpEndDate.Enabled = true;
            //}
            //else
            //{
            //    DataTable dtMinCreateDate = new DataTable();
            //    if (cboStatus.SelectedIndex == 0)
            //    {
            //        dtMinCreateDate = TextUtils.Select("SELECT MIN(CreatDate) as MinCreatDate FROM [dbo].[BillImport]");
            //    }
            //    else if (cboStatus.SelectedIndex == 1)
            //    {
            //        dtMinCreateDate = TextUtils.Select("SELECT MIN(CreatDate) as MinCreatDate FROM [dbo].[BillExport]");
            //    }
            //    string[] minCreate = dtMinCreateDate.Rows[0]["MinCreatDate"].ToString().Split('/', ' ');
            //    dateTimeS = new DateTime(TextUtils.ToInt(minCreate[2]), TextUtils.ToInt(minCreate[1]), TextUtils.ToInt(minCreate[0]), 0, 0, 0);
            //    dtpFromDate.Enabled = dtpEndDate.Enabled = false;
            //}

            //DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            DateTime? dateStart = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime? dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            if (chkAllHistoryImportExport.Checked) dateStart = dateEnd = null;
            dtpFromDate.Enabled = dtpEndDate.Enabled = !chkAllHistoryImportExport.Checked;

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu..."))
            {
                DataSet oDataSet = TextUtils.LoadDataSetFromSP("[spGetHistoryImportExport_New]"
                   , new string[] { "@PageNumber", "@PageSize", "@FilterText", "@DateStart", "@DateEnd", "@Status", "@WarehouseCode" }
                   , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), txtFilterText.Text.Trim(), dateStart, dateEnd, cboStatus.SelectedIndex, _warehouseCode });
                txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
                grdData.DataSource = oDataSet.Tables[0];
            }

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
                BillExportModel model = (BillExportModel)BillExportBO.Instance.FindByPK(ID);
                frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
                frm.billExport = model;
                frm.IDDetail = ID;
                if (frm.ShowDialog() == DialogResult.OK)
                {

                }
            }
            else
            {
                BillImportModel model = (BillImportModel)BillImportBO.Instance.FindByPK(ID);
                frmBillImportDetail frm = new frmBillImportDetail();
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

        private void grdData_Click(object sender, EventArgs e)
        {

        }
    }
}
