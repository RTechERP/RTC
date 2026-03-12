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
    public partial class frmBillLog : _Forms
    {
        public int billType = 0;
        public int billImportID = 0;
        public int billExportID = 0;
        public frmBillLog()
        {
            InitializeComponent();
        }

        private void frmBillLog_Load(object sender, EventArgs e)
        {
            if (billType == 1)
            {
                this.Text += " - PHIẾU NHẬP";
            }
            else if (billType == 2)
            {
                this.Text += " - PHIẾU NHẬP DEMO";
            }
            else if (billType == 3)
            {
                this.Text += " - PHIẾU XUẤT DEMO";
            }
            else
            {
                this.Text += " - PHIẾU XUẤT";
            }
           // this.Text += billType == 1 ? " - PHIẾU NHẬP" : " - PHIẾU XUẤT";
            LoadData();
        }


        void LoadData()
        {
            DataTable dt = new DataTable();
            if (billType == 1) // Phiếu nhập
            {
                dt = TextUtils.LoadDataFromSP("spGetBillImportLog", "A", new string[] { "@BillImportID" }, new object[] { billImportID });
            }
            else if (billType == 2)
            {
                dt = TextUtils.LoadDataFromSP("spGetBillImportTechLog", "A", new string[] { "@BillImportTechID" }, new object[] { billImportID });
            }
            else if (billType == 3)
            {
                dt = TextUtils.LoadDataFromSP("spGetBillExportTechLog", "A", new string[] { "@BillExportTechID" }, new object[] { billExportID });
            }
            else
            {
                dt = TextUtils.LoadDataFromSP("spGetBillExportLog", "A", new string[] { "@BillExportID" }, new object[] { billExportID });
            }

            grdData.DataSource = dt;
        }
    }
}
