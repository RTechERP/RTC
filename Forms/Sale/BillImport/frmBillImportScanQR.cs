using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS;
using BMS.Business;
using BMS.Model;
using System.IO.Ports;
using System.Linq.Expressions;
using System.Threading;
using static Forms.Classes.cGlobVar;

namespace BMS
{
    public partial class frmBillInforScanQR : _Forms
    {
        public string WarehouseCode;
        public int warehouseID;
        DataTable dt = new DataTable();
        public frmBillInforScanQR()
        {
            InitializeComponent();
        }

        private void frmBillInforScanQR_Load(object sender, EventArgs e)
        {
            this.Text += " - " + WarehouseCode;
            txtBillCode.Focus();
        }
        void loadBillImportDetail()
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            DataTable dtDetail = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { ID });
            grdData.DataSource = dtDetail;
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadBillImportDetail();
        }

        private void frmBillInforScanQR_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        #region Hủy/nhận tất cả chứng từ
        private void btnIsApprovedAll_Click(object sender, EventArgs e)
        {
            if (grvMaster.RowCount <= 0)
            {
                MessageBox.Show("Không có dòng nào để duyệt!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            approved(true);
        }

        private void btnCancelApprovedAll_Click(object sender, EventArgs e)
        {
            if (grvMaster.RowCount <= 0)
            {
                MessageBox.Show("Không có dòng nào để hủy duyệt!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            approved(false);
        }

        void approved(bool isApproved)
        {
            string message = isApproved ? "nhận chứng từ" : "huỷ nhận chứng từ";

            if (MessageBox.Show($"Bạn có chắc muốn {message} tất cả các phiếu ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int rowID = TextUtils.ToInt(row["ID"]);
                    BillImportModel billImport = SQLHelper<BillImportModel>.FindByID(rowID);
                    billImport.Status = isApproved;
                    SQLHelper<BillImportModel>.Update(billImport);
                    row["Status"] = isApproved;
                    row["DateStatus"] = DateTime.Now;
                    UpdateLog(rowID, isApproved);
                }
                grdMaster.DataSource = null;
                grdMaster.DataSource = dt;
            }
        }

        void UpdateLog(int billImportID, bool status)
        {
            BillImportLogModel log = new BillImportLogModel();
            log.BillImportID = billImportID;
            log.StatusBill = status;
            log.DateStatus = DateTime.Now;

            BillImportLogBO.Instance.Insert(log);
        }
        #endregion

        #region Thêm dữ liệu mới vào bảng 
        private void txtBillCode_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtBillCode.Text != null || txtBillCode.Text != "")
            {
                try
                {
                    SQLHelper<BillImportModel>.AddNewDataQR("BillImportCode", txtBillCode.Text.Trim(), "spGetBillImportScanQR", warehouseID , dt, grdMaster);
                    txtBillCode.Clear();
                    txtBillCode.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK);
                   
                }
            }
            return;
        }
        #endregion

        private void btnClearData_Click(object sender, EventArgs e)
        {
            dt.Clear();
            grdMaster.DataSource = null;
            txtBillCode.Clear();
            txtBillCode.Focus();
        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            int billID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (billID <= 0)
            {
                return;
            }

            frmBillLog frm = new frmBillLog();
            frm.billType = 1;
            frm.billImportID = billID;
            frm.Show();
        }

        private void frmBillInforScanQR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
