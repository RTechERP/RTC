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

namespace BMS
{
    public partial class frmRequestBuy : _Forms
    {
        public frmRequestBuy()
        {
            InitializeComponent();
        }

        private void frmRequestBuy_Load(object sender, EventArgs e)
        {
            // ngày bắt đầu khi load form bằng ngày hiện tại trừ đi 1 tháng
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);

            grvMaster.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            loadCustomer();
            loadUser();
            loadRequestBuy();
        }

        #region Methods
        /// <summary>
        /// Lấy danh sách khách hàng
        /// </summary>
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT ID,CustomerName FROM dbo.Customer where IsDeleted <> 1 Order By CreatedDate DESC");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }
        /// <summary>
        /// Lấy danh sách người phụ trách lên combo
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,FullName FROM dbo.Users");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;
        }

        private void loadRequestBuy()
        {
            try
            {
                txtPageNumber.Text = "1";
                txtTotalPage.Text = "1";

                DataSet oDataSet = loadRequestBuySet();

                txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0][0]);
            }
            catch (Exception)
            {
            }
        }

        DataSet loadRequestBuySet()
        {
            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetRequestBuy"
                    , new string[] { "@PageNumber", "@PageSize", "@Status", "@UserID", "@FilterText" }
                    , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt( txtPageSize.Value)
                        ,cboStatus.SelectedIndex,TextUtils.ToInt(cbUser.EditValue), txtFilterText.Text.Trim() });

            grdMaster.DataSource = oDataSet.Tables[0];
            return oDataSet;
        }

        /// <summary>
        /// load bảng Detail
        /// </summary>
        private void loadRequestBuyDetail()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetRequestBuyDetail", "A", new string[] { "@ID" }, new object[] { TextUtils.ToInt64(grvMaster.GetFocusedRowCellValue(colIDMaster)) });
            grdData.DataSource = dt;
        }
        #endregion

        #region Buttons Events
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmRequestBuyDetail frm = new frmRequestBuyDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadRequestBuy();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;

            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (id == 0) return;
            RequestBuyModel model = (RequestBuyModel)RequestBuyBO.Instance.FindByPK(id);

            frmRequestBuyDetail frm = new frmRequestBuyDetail();
            frm.requestBuy = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadRequestBuy();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!grvMaster.IsDataRow(grvMaster.FocusedRowHandle))
                return;

            int strID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));

            int status = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colNameNCC));
            if (status > 0)
            {
                MessageBox.Show("Yêu cầu này đang được thực hiện nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string productCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProductCode));
            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa [{0}] không?", productCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    RequestBuyBO.Instance.Delete(strID);
                    RequestBuyDetailBO.Instance.DeleteByAttribute("RequestBuyID", strID);
                    grvMaster.DeleteSelectedRows();
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
                }
            }
        }

        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadRequestBuy();
        }
        #endregion

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            if (grvMaster.RowCount > 0 && btnEdit.Enabled == true)
                btnEdit_Click(null, null);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadRequestBuySet();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadRequestBuySet();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadRequestBuySet();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadRequestBuySet();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            loadRequestBuy();
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadRequestBuyDetail();
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}
