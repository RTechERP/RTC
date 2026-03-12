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
    public partial class frmPONCCDetails : _Forms
    {
        public int supplierSaleId = 0;
        public List<object> listDetails = new List<object>();
        public frmPONCCDetails()
        {
            InitializeComponent();
        }

        private void frmPONCCDetails_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+1).AddDays(-1);
            cboStatus.SelectedIndex = 1;

            LoadData();
        }


        void LoadData()
        {
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            int status = cboStatus.SelectedIndex - 1;
            string keyword = txtKeyword.Text.Trim();

            DataTable dt = TextUtils.LoadDataFromSP("spGetPONCCForImport", "A",
                                    new string[] { "@DateStart", "@DateEnd", "@SupplierSaleID", "@Status", "@Keyword" },
                                    new object[] { dateStart, dateEnd,supplierSaleId, status, keyword });

            grdData.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int[] rowSelecteds = grvData.GetSelectedRows();
                if (rowSelecteds.Length <= 0)
                {
                    MessageBox.Show("Vui lòng chọn sản phầm muốn nhập kho!", "Thông báo");
                    return;
                }

                foreach (int row in rowSelecteds)
                {
                    int productSaleId = TextUtils.ToInt(grvData.GetRowCellValue(row, colProductSaleID));
                    int poNCCDetailId = TextUtils.ToInt(grvData.GetRowCellValue(row, colPONCCDetailID));

                    if (productSaleId <= 0 || poNCCDetailId <= 0) continue;

                    var importDetai = new
                    {
                        ProductNewCode = TextUtils.ToString(grvData.GetRowCellValue(row, colProductNewCode)),
                        ProductID = productSaleId,
                        ProductName = TextUtils.ToString(grvData.GetRowCellValue(row, colProductName)),
                        PONCCDetailID = poNCCDetailId,
                        Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colQtyRequest)),
                        Unit = TextUtils.ToString(grvData.GetRowCellValue(row, colUnit)),
                    };

                    listDetails.Add(importDetai);
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void frmPONCCDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
