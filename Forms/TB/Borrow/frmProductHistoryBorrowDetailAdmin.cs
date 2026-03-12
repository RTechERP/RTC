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
    public partial class frmProductHistoryBorrowDetailAdmin : Form
    {
        int warehouseID;

        public frmProductHistoryBorrowDetailAdmin(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }
        //public int _ProductRTCID;
        //public int _PeopleID;
        public int _id;
        public HistoryProductRTCModel historyProductRTC = new HistoryProductRTCModel();
       
        private void frmProductHistoryBorrowDetailAdmin_Load(object sender, EventArgs e)
        {
            load();
        }
        void load()
        {
            DataTable dt = dt = TextUtils.Select($"SELECT top 1 g.ProductName , g.ProductCode,g.Serial,g.SerialNumber,g.PartNumber, g.Maker,p.NumberBorrow, g.AddressBox,p.PeopleID,h.FullName, p.* ,DATEDIFF(DAY, GETDATE(), p.DateReturnExpected) AS CountDate FROM HistoryProductRTC p LEFT JOIN ProductRTC g ON p.ProductRTCID = g.ID LEFT JOIN Users h ON p.PeopleID = h.ID  WHERE   p.ID ={_id} and p.WarehouseID = {warehouseID}");
            if (dt.Rows.Count <= 0)
            {
                return;
            }
            txtTenSanPham.Text = Lib.ToString(dt.Rows[0]["ProductName"]);
            txtMaSanPham.Text = Lib.ToString(dt.Rows[0]["ProductCode"]);
            txtSerial.Text = Lib.ToString(dt.Rows[0]["SerialNumber"]);
            txtPartNumber.Text = Lib.ToString(dt.Rows[0]["PartNumber"]);
            txtCode.Text = Lib.ToString(dt.Rows[0]["Serial"]);
            txtHang.Text = Lib.ToString(dt.Rows[0]["Maker"]);
            txtSoLuongMuon.Text = Lib.ToString(dt.Rows[0]["NumberBorrow"]);
            txtViTriHop.Text = Lib.ToString(dt.Rows[0]["AddressBox"]);
            User.Text = Lib.ToString(dt.Rows[0]["FullName"]);
            dtpNgayMuon.Text = TextUtils.ToString(dt.Rows[0]["DateBorrow"]);
            dtpNgayDuKienTra.Text = Lib.ToString(dt.Rows[0]["DateReturnExpected"]);
            txtDuAn.Text = Lib.ToString(dt.Rows[0]["Project"]);
            txtNote.Text = Lib.ToString(dt.Rows[0]["Note"]);
        }
        bool save()
        {
            historyProductRTC.DateBorrow = dtpNgayMuon.Value;
            historyProductRTC.DateReturnExpected = dtpNgayDuKienTra.Value;
            historyProductRTC.Project = txtDuAn.Text.Trim();
            historyProductRTC.Note = txtNote.Text.Trim();
            historyProductRTC.WarehouseID = warehouseID;

            HistoryProductRTCBO.Instance.Update(historyProductRTC);
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmProductHistoryBorrowDetailAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
