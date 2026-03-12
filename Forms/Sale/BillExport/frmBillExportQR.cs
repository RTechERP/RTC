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
    public partial class frmBillExportQR : _Forms
    {
        public string WarehouseCode;
        public int warehouseID;
        DataTable dt = new DataTable();
        public frmBillExportQR()
        {
            InitializeComponent();
        }
        private void frmBillExportQR_Load(object sender, EventArgs e)
        {
            this.Text += " - " + WarehouseCode;
            txtTotalName.Focus();
        }


        void loadBillExportDetail()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportDetail", "A", new string[] { "@BillID" }, new object[] { TextUtils.ToInt64(grvMaster.GetFocusedRowCellValue(colIDMaster)) });
            grdData.DataSource = dt;
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            grvData.Appearance.FocusedRow.BackColor = Color.FromArgb(192, 192, 255);
            loadBillExportDetail();
        }

        #region Hủy nhận tất cả chứng từ
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
                    int rowId = TextUtils.ToInt(row["ID"]);
                    BillExportModel billExport = SQLHelper<BillExportModel>.FindByID(rowId);
                    billExport.IsApproved = isApproved;
                    SQLHelper<BillExportModel>.Update(billExport);
                    row["IsApproved"] = isApproved;
                    row["DateStatus"] = DateTime.Now;
                    UpdateLog(rowId, isApproved);
                }
                grdMaster.DataSource = null;
                grdMaster.DataSource = dt;
            }

        }

        void UpdateLog(int billExportID, bool status)
        {
            BillExportLogModel log = new BillExportLogModel();
            log.BillExportID = billExportID;
            log.StatusBill = status;
            log.DateStatus = DateTime.Now;

            BillExportLogBO.Instance.Insert(log);
        }
        #endregion

        #region Thêm mới dữ liệu vào bảng

        private void txtTotalName_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtTotalName.Text != null || txtTotalName.Text != "")
            {
                try
                {
                    SQLHelper<BillExportModel>.AddNewDataQR("Code", txtTotalName.Text.Trim(), "spGetBillExportScanQR", warehouseID, dt, grdMaster);
                    txtTotalName.Clear();
                    txtTotalName.Focus();
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
            txtTotalName.Clear();
            txtTotalName.Focus();
        }

        private void grdMaster_DoubleClick(object sender, EventArgs e)
        {
            int billID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (billID <= 0)
            {
                return;
            }

            frmBillLog frm = new frmBillLog();
            frm.billType = 0;
            frm.billExportID = billID;
            frm.Show();
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void frmBillExportQR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmBillExportQR_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
