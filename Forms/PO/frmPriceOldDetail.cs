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
    public partial class frmPriceOldDetail : _Forms
    {
        public string code = "";
        public string suplier = "";
        public RequestBuySaleDetailModel detail = new RequestBuySaleDetailModel();
       
        public frmPriceOldDetail()
        {
            InitializeComponent();
        }

        private void frmChiTietSanPhamSale_Load(object sender, EventArgs e)
        {

            loadCbPartCode();
            loadGrdData();
            
        }
        void loadCbPartCode()
        {
            DataTable dt = TextUtils.Select("select ProductCode,ID from ProductSale");
            cbProductCode.Properties.DisplayMember = "ProductCode";
            cbProductCode.Properties.ValueMember = "ID";
            cbProductCode.Properties.DataSource = dt;
        }
     
        void loadGrdData()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spLoadHistoryPrice", "A", new string[] { "@ProductID" }, new object[] { detail.ProductID });
            DataColumn data = new DataColumn("Location", typeof(Byte[]));
            dt.Columns.Add(data);
            grdData.DataSource = dt;

        }
      
        private void frmBillImportDetailsUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void cbPartCode_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
