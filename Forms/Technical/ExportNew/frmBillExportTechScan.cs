using BMS.Business;
using BMS.Model;
using Forms.Classes;
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
    public partial class frmBillExportTechScan : _Forms
    {
        public string WarehouseCode;
        public int warehouseID;
        DataTable dt = new DataTable();
        public frmBillExportTechScan()
        {
            InitializeComponent();
        }
        private void frmBillExportTechScan_Load(object sender, EventArgs e)
        {
            this.Text += " - " + WarehouseCode;
            txtBillCode.Focus();
        }

        private void grvBillExportTech_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadBillImportDetail();
        }

        void loadBillImportDetail()
        {
            int ID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP(StoreProcedures.spGetBillExportTechDetail_New, "A", new string[] { "@Id" }, new object[] { ID });
            grdDetail.DataSource = dt;
        }

        private void btnIsApprovedAll_Click(object sender, EventArgs e)
        {
            if (grvBillExportTech.RowCount <= 0)
            {
                MessageBox.Show("Không có dòng nào để hủy duyệt!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            approved(true);
        }

        private void btnCancelApprovedAll_Click(object sender, EventArgs e)
        {
            if (grvBillExportTech.RowCount <= 0)
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
                    BillExportTechnicalModel billExport = SQLHelper<BillExportTechnicalModel>.FindByID(rowID);
                    billExport.Status = isApproved ? 1 : 0;
                    SQLHelper<BillExportTechnicalModel>.Update(billExport);
                    row["StatusBit"] = isApproved;
                    row["DateStatus"] = DateTime.Now;
                    UpdateLog(rowID, isApproved);
                }
                grdBillExportTech.DataSource = null;
                grdBillExportTech.DataSource = dt;
            }
        }
        void UpdateLog(int billImportID, bool status)
        {
            BillExportTechnicalLogModel log = new BillExportTechnicalLogModel();
            log.BillExportTechnicalID = billImportID;
            log.StatusBill = status;
            log.DateStatus = DateTime.Now;
            BillExportTechnicalLogBO.Instance.Insert(log);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtBillCode.Text != null || txtBillCode.Text != "")
            {
                try
                {
                    SQLHelper<BillExportTechnicalModel>.AddNewDataQR("Code", txtBillCode.Text.Trim(), "spGetBillExportTechScan", warehouseID, dt, grdBillExportTech);
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

        private void btnClearData_Click(object sender, EventArgs e)
        {
            dt.Clear();
            grdBillExportTech.DataSource = null;
            txtBillCode.Clear();
            txtBillCode.Focus();
        }

        private void grdBillExportTech_DoubleClick(object sender, EventArgs e)
        {
            int billID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colID));
            if (billID <= 0)
            {
                return;
            }
            frmBillLog frm = new frmBillLog();
            frm.billType = 3;
            frm.billExportID = billID;
            frm.Show();
        }

        private void frmBillExportTechScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmBillExportTechScan_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
