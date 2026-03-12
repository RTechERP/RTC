using BMS;
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

namespace Forms.Sale
{
    
    public partial class frmPhieuTra : _Forms
    {
        public BillImportDetailModel billImportDetailModel;
        public int Type;
        public int productID;
        public string maphieu;
        public frmPhieuTra()
        {
            InitializeComponent();
        }

        private void frmPhieuTra_Load(object sender, EventArgs e)
        {
            maphieu = "";
            loadPhieuTra();
        }
        void loadPhieuTra()
        {
            DataTable dt = TextUtils.LoadDataFromSP($"spGetBillReturn", "a", new string[] { "@ProductID" }
              , new object[] { productID });
            grdData.DataSource = dt;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {

            maphieu = TextUtils.ToString(grvData.GetFocusedRowCellValue(colMaPhieuMuon));
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int remain = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colRemain));
                if (remain != 0)
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            maphieu = TextUtils.ToString(grvData.GetFocusedRowCellValue(colMaPhieuMuon));

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
