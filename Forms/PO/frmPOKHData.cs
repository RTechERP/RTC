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
using System.Collections;
using static BMS.frmRequestBuyDetail;
using Forms.Classes;
using static BMS.frmBillExportDetailNew;

namespace BMS
{
    public partial class frmPOKHData : _Forms
    {
        DataTable dtProduct = new DataTable();
        public ListID _listID; // tạo ra ListID để chuyền giữa 2 form    
        public delegate void SendListID(List<int> ID, string group);
        public delegate void SendListPONCC(List<int> ID, List<int> Qty, DataTable dt);
        public SendListID sendListID;
        public SendListPONCC send;
        public ListBillExportDetail _listBillExportDetail;

        int warehouseId = 0;

        public frmPOKHData(int warehouseId)
        {
            InitializeComponent();
            this.warehouseId = warehouseId;
        }

        private void frmTargetVisit_Load(object sender, EventArgs e)
        {
            try
            {
                cGlobVar.LockEvents = true;
                cbIsPO.SelectedIndex = 0;
                loadcbPO();
                loadCustomer();
                loadUser();
                txtPageNumber.Text = "1";
                loadPOKHData();
                this.cbProduct.EditValueChanged += new System.EventHandler(cbProduct_EditValueChanged);
            }
            finally
            {
                cGlobVar.LockEvents = false;
            }

        }
        private void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Customer where IsDeleted <> 1 ORDER BY CreatedDate DESC");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }
        private void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName,Code+'-'+FullName AS UserInfo FROM dbo.Users");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;
        }

        #region Methods

        void loadcbPO()
        {
            DataTable dt = TextUtils.Select($"Select p.*,c.CustomerName From POKH p Inner Join Customer c on c.ID=p.CustomerID where IsExport = 0");
            cbPO.Properties.DisplayMember = "CustomerName";
            cbPO.Properties.ValueMember = "ID";
            cbPO.Properties.DataSource = dt;

        }
        DataSet oDataSet;
        /// <summary>
        /// load TargetVisit
        /// </summary>
        private void loadPOKHData()
        {
            group = "";
            oDataSet = TextUtils.LoadDataSetFromSP("spGetPOKHData"
                , new string[] { "@PageNumber", "@PageSize", "@FilterText", "@POKHID", "@IsOder", "@WarehouseID" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), txtFilterText.Text.Trim(), TextUtils.ToInt(cbPO.EditValue), cbIsPO.SelectedIndex, warehouseId });
            grdData.DataSource = oDataSet.Tables[0];
            for (int i = 0; i < oDataSet.Tables[2].Rows.Count; i++)
            {
                group = group + TextUtils.ToString(oDataSet.Tables[2].Rows[i][0]);
                if (i < oDataSet.Tables[2].Rows.Count - 1)
                    group = group + ",";
            }

            if (oDataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
        }
        #endregion

        #region Button Events

        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadPOKHData();
        }
        #endregion

        ////hàm khi chọn cboName -> tự động sinh ra tên,ĐVT
        private void cbProduct_EditValueChanged(object sender, EventArgs e)
        {
            grvData.Focus();
            //txtFilterText.Focus();
            DataTable dtProduct = TextUtils.Select("SELECT ID,ProductCode,ProductName,Unit,Maker FROM ProductSale");
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductCode));
            DataRow[] rows = dtProduct.Select("ID = " + ID);
            if (rows.Length > 0)
            {
                string productName = TextUtils.ToString(rows[0]["ProductName"]);
                string unit = TextUtils.ToString(rows[0]["Unit"]);
                string maker = TextUtils.ToString(rows[0]["Maker"]);
                grvData.SetFocusedRowCellValue(colProductName, productName);
                grvData.SetFocusedRowCellValue(colUnit, unit);
                grvData.SetFocusedRowCellValue(colMaker, maker);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadPOKHData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadPOKHData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadPOKHData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadPOKHData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadPOKHData();
        }
        List<int> lstProductID = new List<int>();
        List<int> lstID = new List<int>();
        string group;
        int ID;
        private void btnSave_Click(object sender, EventArgs e)
        {
            lstProductID.Clear();
            int ProductID;
            int[] selectedRowHandles = grvData.GetSelectedRows();

            foreach (int item in selectedRowHandles)
            {
                if (item >= 0)
                {
                    ProductID = TextUtils.ToInt(grvData.GetRowCellValue(item, colProductID));
                    ID = TextUtils.ToInt(grvData.GetRowCellValue(item, colID));
                    lstProductID.Add(ProductID);
                    lstID.Add(ID);
                }
            }
            if (lstID.Count == 0) return;
            string listID = string.Join(",", lstID);
            DataRow[] dtr = oDataSet.Tables[0].Select($"ID in ({listID})");
            if (dtr.Length == 0) return;
            DataTable dt = dtr.CopyToDataTable();
            this.send?.Invoke(lstProductID, lstID, dt);
            _listBillExportDetail?.Invoke(group, dt);
            this.DialogResult = DialogResult.OK;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadPOKHData();
        }

        private void cbPO_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadPOKHData();
        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadPOKHData();
        }

        private void cbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadPOKHData();
        }

        private void cbIsPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadPOKHData();
        }
    }
}


