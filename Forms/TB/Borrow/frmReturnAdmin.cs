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
    public partial class frmReturnAdmin : _Forms
    {
        DataTable dtAll = new DataTable();
        List<string> Listqrcode = new List<string>();
        public int warehouseID;
        public frmReturnAdmin()
        {
            InitializeComponent();
        }
        public frmReturnAdmin(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }

        private void frmReturnAdmin_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            txtQrCode.SelectAll();
            txtQrCode.Focus();
            dtAll = TextUtils.LoadDataFromSP(StoreProcedures.spGetProductQrCode, "A", new string[] { }, new object[] { });//spGetProductQrCode
            grdData.DataSource = dtAll;
        }
        void loadQRCode()
        {

            DataTable dt = TextUtils.LoadDataFromSP("spGetHistoryProductByQRCode", "A", new string[] { "@ProductRTCQRCode", "@WarehouseID" }, new object[] { txtQrCode.Text.Trim(),warehouseID });
            if (dt.Rows.Count == 0 || dt == null)
            {
                //txtQrCode.SelectAll();
                return;
            }
            grvData.BeginDataUpdate();
            DataRow dr = dtAll.NewRow();
            dr.BeginEdit();
            dr["ID"] = dt.Rows[0]["ID"];//ID của ProductRTCQRCode
            dr["ProductRTCID"] = dt.Rows[0]["ProductRTCID"];
            dr["ProductQRCode"] = dt.Rows[0]["ProductQRCode"];
            dr["ProductCode"] = dt.Rows[0]["ProductCode"];
            dr["ProductName"] = dt.Rows[0]["ProductName"];
            dr["ProductCodeRTC"] = dt.Rows[0]["ProductCodeRTC"];
            dr["AddressBox"] = dt.Rows[0]["AddressBox"];
            dr["Note"] = dt.Rows[0]["Note"];
            dr["Soluong"] = 1;
            dr["HistoryProductRTCID"] = dt.Rows[0]["HistoryProductRTCID"];
            dr.EndEdit();
            if (Listqrcode.Contains(TextUtils.ToString(dt.Rows[0]["ProductQRCode"])))
            {
                grdData.DataSource = dtAll;
                grvData.EndDataUpdate();
            }
            else
            {
                dtAll.Rows.Add(dr);
                grdData.DataSource = dtAll;
                grvData.EndDataUpdate();
            }
            Listqrcode.Add(TextUtils.ToString(dt.Rows[0]["ProductQRCode"]).Trim());

            txtQrCode.SelectAll();
        }

        private void txtQrCode_TextChanged(object sender, EventArgs e)
        {
            loadQRCode();
        }

        bool save()
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                int HistoryProductRTCID = TextUtils.ToInt(grvData.GetRowCellValue(i, colHistoryProductRTCID));
                HistoryProductRTCModel historyProductRTCModel = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(HistoryProductRTCID);
                historyProductRTCModel.Status = 0;
                historyProductRTCModel.DateReturn = dtpReturnDate.Value;
                historyProductRTCModel.WarehouseID = warehouseID;
                HistoryProductRTCBO.Instance.Update(historyProductRTCModel);
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn trả các thiết bị này hay không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (save())
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }
    }
}
