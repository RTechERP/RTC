using BMS.Business;
using BMS.Model;
using System;
using System.Data;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmBillExportAccountant : _Forms
    {
        public frmBillExportAccountant()
        {
            InitializeComponent();
        }

        private void frmBillExportAccountant_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);

            txtPageNumber.Text = "1";
            loadBillExportAcountant();
        }

        #region Methods
        /// <summary>
        /// load bảng Master
        /// </summary>
        private void loadBillExportAcountant()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportAcountant", "A"
                , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@FilterText" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtFilterText.Text });
            grdMaster.DataSource = dt;
            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
        }

        /// <summary>
        /// load bảng Detail
        /// </summary>
        void loadBillExportAcountantDetail()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportAcountantDetail", "A", new string[] { "@BillExportID" }, new object[] { TextUtils.ToInt64(grvMaster.GetFocusedRowCellValue(colIDMaster)) });
            grdData.DataSource = dt;
        }
        #endregion
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmBillExportAcountantDetail frm = new frmBillExportAcountantDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillExportAcountant();
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (ID == 0) return;
            BillExportAcountantModel model = (BillExportAcountantModel)BillExportAcountantBO.Instance.FindByPK(ID);
            frmBillExportAcountantDetail frm = new frmBillExportAcountantDetail();
            frm.billExportAcountant = model;
            frm.IDDetail = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillExportAcountant();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Bạn không thể xóa phiếu xuất [{0}] này?", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            if (!grvMaster.IsDataRow(grvMaster.FocusedRowHandle)) return;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (ID == 0) return;
            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa phiếu xuất [{0}] không?", code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BillExportAcountantBO.Instance.Delete(ID);
                BillExportAcountantDetailBO.Instance.DeleteByAttribute("BillExportID", ID);
                grvMaster.DeleteSelectedRows();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
            //thêm lịch sử người xóa phiếu
            TextUtils.ExcuteSQL($"Insert into HistoryDeleteBill(BillID,UserID,DeleteDate,Name,TypeBill) values ({ID},{Global.UserID},'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{Global.AppUserName}','{code}') ");
        }

        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == true)
            {
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
                MessageBox.Show(String.Format("Phiếu xuất [{0}] đã được duyệt.", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            approved(true);
        }

        private void btnCancelApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == false)
            {
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
                MessageBox.Show(String.Format("Phiếu xuất [{0}] chưa được duyệt.", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            approved(false);
        }

        /// <summary>
        /// hàm duyệt
        /// </summary>
        /// <param name="isApproved"></param>
        void approved(bool isApproved)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt phiếu này?", isApproved ? "" : "bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int iD = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                string sql = string.Format(@"UPDATE dbo.BillExportAcountant SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, iD);
                TextUtils.ExcuteSQL(sql);
                if (isApproved == true)
                    grvMaster.SetFocusedRowCellValue(colIsApproved, 1);
                else
                    grvMaster.SetFocusedRowCellValue(colIsApproved, 0);
            }
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadBillExportAcountantDetail();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grvMaster.FocusedRowHandle;
            loadBillExportAcountant();
            grvMaster.FocusedRowHandle = focusedRowHandle - 1;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadBillExportAcountant();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadBillExportAcountant();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadBillExportAcountant();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadBillExportAcountant();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadBillExportAcountant();
        }

        private void grdMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}
