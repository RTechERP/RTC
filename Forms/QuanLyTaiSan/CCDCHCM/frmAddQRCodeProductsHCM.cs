using BMS.Business;
using BMS.Model;
using BMS.Utils;
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
	public partial class frmAddQRCodeProductsHCM : _Forms
	{
		public int id;
		public bool Edit;
		private int warehouseID;
		public ProductRTCQRCodeModel modelrtc = new ProductRTCQRCodeModel();

		public frmAddQRCodeProductsHCM(int WarehouseID)
		{
			InitializeComponent();
			warehouseID = WarehouseID;
		}

		private void frmAddQRCodeProductsHCM_Load(object sender, EventArgs e)
		{
			loadProduct();
			loadData();
		}

        void loadProduct()
        {
            List<ProductRTCModel> listProducts = SQLHelper<ProductRTCModel>.FindByAttribute("IsDelete", 0);
            cboProduct.Properties.DataSource = listProducts;
            cboProduct.Properties.ValueMember = "ID";
            cboProduct.Properties.DisplayMember = "ProductCode";

        }
        void loadData()
        {
            if (Edit)
            {
                cboProduct.Enabled = false;
            }
            if (modelrtc.ID > 0)
            {
                cboProduct.EditValue = modelrtc.ProductRTCID;
                txtQrCode.Text = modelrtc.ProductQRCode;
                comboBox1.SelectedIndex = modelrtc.Status - 1;
            }
            else
            {
                if (id > 0)
                {
                    cboProduct.EditValue = id;

                }
            }

        }
        bool validate()
        {
            if (cboProduct.Text == "")
            {
                MessageBox.Show("Thiết bị không được để trống !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtQrCode.Text == "")
            {
                MessageBox.Show("Mã QrCode không được để trống !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                var exp1 = new Expression("ProductQRCode", txtQrCode.Text.Trim());
                var exp2 = new Expression("WarehouseID", warehouseID);
                var exp3 = new Expression("ID", modelrtc.ID, "<>");
                var qrCodes = SQLHelper<ProductRTCQRCodeModel>.FindByExpression(exp1.And(exp2).And(exp3));
                if (qrCodes.Count > 0)
                {
                    MessageBox.Show($"Mã QrCode [{txtQrCode.Text}] đã được sử dụng vui lòng kiểm tra lại !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        bool save()
        {
            if (!validate()) return false;

            modelrtc.ProductQRCode = txtQrCode.Text;
            modelrtc.ProductRTCID = TextUtils.ToInt(cboProduct.EditValue);
            modelrtc.Status = comboBox1.SelectedIndex + 1;
            modelrtc.WarehouseID = warehouseID;
            if (modelrtc.ID > 0)
            {
                SQLHelper<ProductRTCQRCodeModel>.Update(modelrtc);
            }
            else
            {
                SQLHelper<ProductRTCQRCodeModel>.Insert(modelrtc);
            }

            return true;
        }

		private void btnAddAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
            if (save())
            {
                this.DialogResult = DialogResult.OK;

            }
        }

		private void btnAddAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
            if (save())
            {
                cboProduct.EditValue = -1;
                txtQrCode.Text = "";

            }
        }

		private void frmAddQRCodeProductsHCM_KeyDown(object sender, KeyEventArgs e)
		{
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

		private void frmAddQRCodeProductsHCM_FormClosed(object sender, FormClosedEventArgs e)
		{
            this.DialogResult = DialogResult.OK;
        }
	}
}
