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
using DevExpress.XtraEditors.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace BMS
{
    public partial class frmQuotationNCC : _Forms
    {
        public frmQuotationNCC()
        {
            InitializeComponent();
        }

        private void frmQuotationNCC_Load(object sender, EventArgs e)
        {
            // ngày bắt đầu khi load form bằng ngày hiện tại trừ đi 1 tháng
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);

            txtPageNumber.Text = "1";
            loadSupplier();
            loadUser();
            loadQuotationNCC();
        }

        #region Methods
        /// <summary>
        /// load khách hàng
        /// </summary>
        void loadSupplier()
        {
            DataTable dt = TextUtils.Select("SELECT ID, NameNCC FROM SupplierSale ");
            cboSupplier.Properties.DisplayMember = "NameNCC";
            cboSupplier.Properties.ValueMember = "ID";
            cboSupplier.Properties.DataSource = dt;
        }

        /// <summary>
        /// load ng phụ trách
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName FROM dbo.Users");
            cbUser.Properties.DisplayMember = "FullName";

            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;

        }
        private void loadQuotationNCC()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetQuotationNCC"
                , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@FilterText", "@Status", "@SupplierID", "@UserID" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtFilterText.Text.Trim()
                            , cbStatus.SelectedIndex,TextUtils.ToInt(cboSupplier.EditValue),TextUtils.ToInt(cbUser.EditValue),});
            grdMaster.DataSource = oDataSet.Tables[0];

            if (oDataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
        }

        void loadQuotationNCCDetail()
        {
            if (grvMaster.RowCount <= 0) return;
            int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            DataTable dt = TextUtils.LoadDataFromSP("spGetQuotationNCCDetail", "A", new string[] { "@ID" }, new object[] { IDMaster });
            grdData.DataSource = dt;

        }
        #endregion

        #region Buttons Events
        /// <summary>
        /// click button tạo báo giá
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmQuotationNCCDetail frm = new frmQuotationNCCDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadQuotationNCC();
                grvMaster_FocusedRowChanged(null,null);
            }
        }

        /// <summary>
        /// click button xóa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            if (grvMaster.RowCount <= 0) return;
            string quoteCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colQuoteCode));
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Bạn không thể xóa phiếu [{0}] này? Xin vui lòng kiểm tra lại.", quoteCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            if (grvMaster.RowCount > 0)
            {
                int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                if (ID == 0) return;

                int strID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
                if (MessageBox.Show(string.Format("Bạn có muốn xóa báo giá [{0}] hay không ?", quoteCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    QuotationNCCBO.Instance.Delete(strID);
                    QuotationNCCDetailBO.Instance.DeleteByAttribute("QuotationNCCID", strID);
                    grvMaster.DeleteSelectedRows();
                }
                if (grvMaster.RowCount == 0)
                {
                    grvMaster.FocusedRowHandle = focusedRowHandle;
                }
                else
                {
                    grvMaster.FocusedRowHandle = focusedRowHandle + 1;
                }
            }
        }

        /// <summary>
        /// click button duyệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            if (grvMaster.RowCount <= 0) return;
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == true)
            {
                string quoteCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colQuoteCode));
                MessageBox.Show(String.Format("Báo giá [{0}] đã được duyệt. Xin vui lòng kiểm tra lại!", quoteCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            approved(true);
        }

        /// <summary>
        /// click button hủy duyệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == false)
            {
                string quoteCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colQuoteCode));
                MessageBox.Show(String.Format("Báo giá [{0}] chưa được duyệt. Xin vui lòng kiểm tra lại!", quoteCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            approved(false);
        }

        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadQuotationNCC();
        }

        /// <summary>
        /// click button sửa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditData();
        }
        #endregion

        /// <summary>
        /// hàm duyệt
        /// </summary>
        /// <param name="isApproved"></param>
        void approved(bool isApproved)
        {
            if (grvMaster.RowCount > 0)
            {
                QuotationNCCModel quoteNCC = new QuotationNCCModel();
                if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt phiếu này?", isApproved ? "" : "bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                int groupID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                string sql = string.Format("UPDATE dbo.QuotationNCC SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, groupID);
                TextUtils.ExcuteSQL(sql);
                if (isApproved == true)
                {
                    grvMaster.SetFocusedRowCellValue(colIsApproved, 1);
                    grvMaster.SetFocusedRowCellValue(colStatusText, "Đã duyệt");
                }    
                else
                {
                    grvMaster.SetFocusedRowCellValue(colIsApproved, 0);
                    grvMaster.SetFocusedRowCellValue(colStatusText, "Chưa duyệt");
                }
            }
        }

        /// <summary>
        /// hàm txtFilterText khi enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadQuotationNCC();
            }
        }

        /// <summary>
        /// edit data
        /// </summary>
        private void EditData()
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;

            if (grvMaster.RowCount <= 0) return;
            int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (IDMaster == 0) return;
            QuotationNCCModel model = (QuotationNCCModel)QuotationNCCBO.Instance.FindByPK(IDMaster);
            frmQuotationNCCDetail frm = new frmQuotationNCCDetail();
            frm.quotationNCC = model;
            frm.IDDetail = IDMaster;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadQuotationNCC();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void grdMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadQuotationNCCDetail();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadQuotationNCC();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadQuotationNCC();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadQuotationNCC();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadQuotationNCC();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadQuotationNCC();
        }
    }
}


