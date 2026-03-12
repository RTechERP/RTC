using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Forms;
using QRCoder;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using Forms.Classes;
using static BMS.frmBillExportAcountantDetail;

namespace BMS
{
    public partial class frmSearchBillExport : _Forms
    {
        #region Variables
        DataSet oDataSet;
        //public ListSearchBillExport _listBillExport;
        DataTable DtSelect = new DataTable();
        List<int> lstSelect = new List<int>();
        DataTable dtCustomer = new DataTable();
        #endregion

        public frmSearchBillExport()
        {
            InitializeComponent();
        }

        private void frmSearchBillExport_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
                dtpFromDate.Value = datenow.AddMonths(-3);
                cGlobVar.LockEvents = true;
                txtPageNumber.Text = "1";
                loadSearchBillExport();
                loadDataSelect();
            }
            finally
            {
                cGlobVar.LockEvents = false;
            }
        }
        /// <summary>
        /// load data select
        /// </summary>
        private void loadDataSelect()
        {
            int idTest = 0;
            DtSelect = TextUtils.LoadDataFromSP("spGetSelectBillExport", "A", new string[] { "@ID" }, new object[] { idTest });
            grdSelect.DataSource = DtSelect;
        }
        #region Methods
        /// <summary>
        /// load SearchBillExport
        /// </summary>
        private void loadSearchBillExport()
        {
            //DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            //DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            //oDataSet = TextUtils.LoadDataSetFromSP("spGetSeachBillExport"
            //    , new string[] { "@DateS", "@DateE", "@PageNumber", "@PageSize", "@FilterText", "@PO" }
            //    , new object[] { dateTimeS, dateTimeE, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), txtFilterText.Text.Trim(), txtPO.Text.Trim() });
            //grdData.DataSource = oDataSet.Tables[0];
            //if (oDataSet.Tables.Count == 0) return;
            //txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
        }

        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (dtCustomer.Rows.Count <= 0) return;
            if (cboCustomer.Text.Trim() == "") return;
            DataRow[] dr = dtCustomer.Select($"ID={cboCustomer.EditValue}");
            txtAddress.Text = TextUtils.ToString(dr[0]["Address"]);
        }

        #endregion

        #region Button Events
        /// <summary>
        /// click button lựa chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            //int[] selectedRowHandles = grvData.GetSelectedRows();
            //for (int i = 0; i < selectedRowHandles.Length; i++)
            //{
            //    int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandles[i], colID));
            //    if (id == 0) continue;
            //    DataTable dt = TextUtils.LoadDataFromSP("spGetSelectBillExport", "A", new string[] { "@ID" }, new object[] { id });
            //    if (dt.Rows.Count == 0) return;
            //    if (!lstSelect.Contains(id) || lstSelect.Count == 0)
            //    {
            //        DtSelect.ImportRow(dt.Rows[0]);
            //        lstSelect.Add(id); 
            //    }
            //}
        }

        /// <summary>
        /// button save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (lstSelect.Count == 0 || grvSelect.RowCount == 0)
            //{
            //    MessageBox.Show("Xin vui lòng lựa chọn sản phẩm", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            //    return;
            //}
            //string listID = string.Join(",", lstSelect);
            //DataRow[] dtr = oDataSet.Tables[0].Select($"ID in ({listID})");
            //if (dtr.Length == 0) return;
            //DataTable dt = dtr.CopyToDataTable();
            //_listBillExport?.Invoke(dt);
            //lstSelect.Clear();
            //this.DialogResult = DialogResult.OK;
        }



        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadSearchBillExport();
        }

        /// <summary>
        /// click button xóa dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //if (grvData.RowCount == 0) return;
            //int id = TextUtils.ToInt(grvSelect.GetFocusedRowCellValue(colIDSelect));
            //string strName = TextUtils.ToString(grvSelect.GetFocusedRowCellDisplayText(colProductNewCodeSelect));
            //if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa '{strName}' không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    lstSelect.Remove(id);
            //    grvSelect.DeleteSelectedRows();
            //}
        }
        #endregion

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadSearchBillExport();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadSearchBillExport();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadSearchBillExport();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadSearchBillExport();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadSearchBillExport();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadSearchBillExport();
        }

        private void txtPO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }
    }
}


