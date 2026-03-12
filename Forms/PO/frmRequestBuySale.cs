using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmRequestBuySale : _Forms
    {
        public frmRequestBuySale()
        {
            InitializeComponent();
        }

        private void frmRequestBuySale_Load(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            grvMaster.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            cboStatus.SelectedIndex = 0;
            loadUser();
            loadMaster();
        }

        #region Methods


        /// <summary>
        /// Lấy danh sách người phụ trách lên combo
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName,DepartmentName FROM dbo.vUser");
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = dt;
        }
        void loadMaster()
        {
            DataTable dt = TextUtils.LoadDataFromSP("sploadRequestBuySale", "A", new string[] { "@Filter", "@PageSize", "@PageNumber" }, new object[] { txtFilterText.Text, txtPageSize.Value, txtPageNumber.Text });
            grdMaster.DataSource = dt;
            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
            loadGrvData();
        }
        private void loadGrvData()
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spLoadRequestBuySaleDetail", "A", new string[] { "@ID" }, new object[] { ID });
            grdData.DataSource = dt;
        }
        #endregion

        #region Buttons Events
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmRequestBuySaleDetail frm = new frmRequestBuySaleDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadMaster();
                loadGrvData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
                if (id == 0) return;
                RequestBuySaleModel model = (RequestBuySaleModel)RequestBuySaleBO.Instance.FindByPK(id);
                frmRequestBuySaleDetail frm = new frmRequestBuySaleDetail();
                frm.dModel = model;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    loadMaster();
                    loadGrvData();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!grvMaster.IsDataRow(grvMaster.FocusedRowHandle))
                return;
            int strID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));         
            string strName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colRequestBuyCode));
            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa [{0}] không?", strName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    RequestBuySaleBO.Instance.Delete(strID);
                    RequestBuySaleDetailBO.Instance.DeleteByAttribute("RequestBuySaleID", strID);
                    grvMaster.DeleteSelectedRows();
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadMaster();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadMaster();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadMaster();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadMaster();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadMaster();
        }

        #endregion

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            loadMaster();
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadGrvData();
        }
      
        void approved(bool isApproved)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn{0} duyệt báo giá này?", isApproved ? "" : " bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No) return;
            int iD = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            string sql = string.Format(@"UPDATE dbo.RequestBuySale SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, iD);
            TextUtils.ExcuteSQL(sql);
            loadMaster();
        }
        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            approved(true);
        }

        private void btnCancelApprove_Click(object sender, EventArgs e)
        {
            approved(false);
        }

        private void grvData_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void giáPoCũToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grvData.RowCount > 0)
            {
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                RequestBuySaleDetailModel requestPriceDetailModel = (RequestBuySaleDetailModel)RequestBuySaleDetailBO.Instance.FindByPK(ID);
                frmPriceOldDetail frm = new frmPriceOldDetail();
                frm.detail = requestPriceDetailModel;
                frm.ShowDialog();
            }
        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            if (grvMaster.RowCount > 0 && btnEdit.Enabled == true)
                btnEdit_Click(null, null);
        }
    }
}

