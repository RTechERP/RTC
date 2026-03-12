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
    public partial class frmBillImportTechQR : _Forms
    {
        public string WarehouseCode;
        public int warehouseID;
        DataTable dt = new DataTable();
        public frmBillImportTechQR()
        {
            InitializeComponent();
        }

        private void frmBillImportTechQR_Load(object sender, EventArgs e)
        {
            this.Text += " - " + WarehouseCode;
            txtBillCode.Focus();
        }

        private void btnIsApprovedAll_Click(object sender, EventArgs e)
        {
            if (grvBillImportTech.RowCount <= 0)
            {
                MessageBox.Show("Không có dòng nào để duyệt!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            approved(true);
        }

        private void btnCancelApprovedAll_Click(object sender, EventArgs e)
        {
            if (grvBillImportTech.RowCount <= 0)
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
                    BillImportTechnicalModel billImport = SQLHelper<BillImportTechnicalModel>.FindByID(rowID);
                    billImport.Status = isApproved;
                    SQLHelper<BillImportTechnicalModel>.Update(billImport);
                    row["Status"] = isApproved;
                    row["DateStatus"] = DateTime.Now;
                    UpdateLog(rowID, isApproved);
                }
                grdBillImportTech.DataSource = null;
                grdBillImportTech.DataSource = dt;
            }
        }
        void UpdateLog(int billImportID, bool status)
        {
            BillImportTechnicalLogModel log = new BillImportTechnicalLogModel();
            log.BillImportTechnicalID = billImportID;
            log.StatusBill = status;
            log.DateStatus = DateTime.Now;
            BillImportTechnicalLogBO.Instance.Insert(log);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtBillCode.Text != null || txtBillCode.Text != "")
            {
                try
                {
                    SQLHelper<BillImportTechnicalModel>.AddNewDataQR("BillCode", txtBillCode.Text.Trim(), "spGetBillImportTechScan", warehouseID, dt, grdBillImportTech);
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
            grdBillImportTech.DataSource = null;
            txtBillCode.Clear();
            txtBillCode.Focus();
        }

        private void grvBillImportTech_DoubleClick(object sender, EventArgs e)
        {
            int billID = TextUtils.ToInt(grvBillImportTech.GetFocusedRowCellValue(colID));
            if (billID <= 0)
            {
                return;
            }

            frmBillLog frm = new frmBillLog();
            frm.billType = 2;
            frm.billImportID = billID;
            frm.Show();
        }

        private void frmBillImportTechQR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmBillImportTechQR_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }



        //void loadBillImportDetail()
        //{
        //    int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
        //    DataTable dtDetail = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { ID });
        //    grdData.DataSource = dtDetail;
        //}


    }
}
