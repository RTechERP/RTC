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
    public partial class frmQuotationDetailHistory : _Forms
    {
        public string PartCode;
        public string PartCodeRTC;
        public int CustomerID;

        public decimal Value = 0;
	    
        public frmQuotationDetailHistory()
        {
            InitializeComponent();
        }

        private void frmQuotationDetailHistory_Load(object sender, EventArgs e)
        {
            this.Text += " - " + PartCode;
            DataTable dt = TextUtils.GetDataTableFromSP("spGetQuotationPartPrice"
                , new string[] { "PartCode", "PartCodeRTC", "CustomerID" }
                , new object[] { PartCode, PartCodeRTC, CustomerID });
            grdData.DataSource = dt;
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            Value = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colPrice));
            DialogResult = DialogResult.OK;
        }
    }
}
