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
    public partial class frmSearchProductTechSerialNumber : _Forms
    {
        int warehouseID;
        //public frmSearchProductTechSerialNumber()
        //{
        //    InitializeComponent();
        //}
        public frmSearchProductTechSerialNumber(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }
        private void LoadData()
        {
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetSearchProductTechSerial", new string[] { "@SerialNumber", "@WarehouseID" }, new object[] { txtSerialNumber.Text.Trim(), warehouseID });
            grdImport.DataSource = ds.Tables[1];
            grdExport.DataSource = ds.Tables[0];
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void frmSearchProductTechSerialNumber_Load(object sender, EventArgs e)
        {
            LoadData();
            this.Text += warehouseID == 1 ? " - HN" : (warehouseID == 2 ? " - HCM" : " - BN");
        }
    }
}
