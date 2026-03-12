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
    public partial class frmProductCheck : _Forms
    {
        DataTable _dtList = new DataTable();
        public ProductRTCModel oProductRTCModel = new ProductRTCModel();
        public HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();

        public frmProductCheck()
        {
            InitializeComponent();
        }

        private void frmProductCheck_Load(object sender, EventArgs e)
        {
            loadDataListProduct();
        }
        /// <summary>
        /// load list data
        /// </summary>
        private void loadDataListProduct()
        {
            _dtList = TextUtils.Select("spDataProductBorrow");            
            grdData.DataSource = _dtList;
        }
        public string PN;
        public string AddressBox;
        public string Code;
        private void btnCheck_Click(object sender, EventArgs e)
        {
            string test = txtQRCode.Text.Trim();
            if (string.IsNullOrEmpty(test)) return;
            string[] productText = test.Split(';');
            PN = productText[0];
            if (test.Length == PN.Length) return;
            AddressBox = productText[1];
            if (test.Length == PN.Length + AddressBox.Length) return;
            Code = productText[2];
            for (int i = 0; i < grvData.RowCount; i++)
            {
                string grv_PN = TextUtils.ToString(grvData.GetRowCellValue(i, colProductCode));
                string grv_AddressBox = TextUtils.ToString(grvData.GetRowCellValue(i, colAddress));
                string grv_Code = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                // so sánh ko phân biệt chữ hoa hay chữ thường
                if (string.Compare(grv_PN, PN, true) == 0 && string.Compare(grv_AddressBox, AddressBox, true) == 0 && string.Compare(grv_Code, Code, true) == 0)
                {
                    grvData.SetRowCellValue(i, colCheck, 1);
                    grvData.FocusedRowHandle = i;
                    return;
                }
            }
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colCheck)) == 1)
            {
                e.Appearance.BackColor = Color.Green;
            }
            
        }

    }
}
