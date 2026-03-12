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
    public partial class ucPriceCheck : UserControl
    {
        public ucPriceCheck()
        {
            InitializeComponent();
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void ucPriceCheck_Load(object sender, EventArgs e)
        {
            loadProduct();
        }
        void loadProduct()
        {
            DataTable dtProduct = TextUtils.Select("SELECT ID,ProductCode,ProductName,ItemType,Unit,Maker FROM ProductSale");
            cbProduct.DisplayMember = "ProductCode";
            cbProduct.ValueMember = "ID";
            cbProduct.DataSource = dtProduct;
        }
    }
}
