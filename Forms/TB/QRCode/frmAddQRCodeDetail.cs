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
    public partial class frmAddQRCodeDetail : _Forms
    {
        public int id;
        public bool Edit;
        private int warehouseID;
        public ProductRTCQRCodeModel modelrtc = new ProductRTCQRCodeModel();
        public frmAddQRCodeDetail()
        {
            InitializeComponent();
        }
        public frmAddQRCodeDetail(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }
        private void frmAddQRCodeDetail_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            loadProduct();
            LoadModulaLocation();
            loadData();
        }
        void loadProduct()
        {
            //DataTable dt = TextUtils.Select($"Select * from ProductRTC Where WarehouseID = {warehouseID}");
            List<ProductRTCModel> listProducts = SQLHelper<ProductRTCModel>.FindByAttribute("IsDelete", 0);

            cboProduct.Properties.DataSource = listProducts;
            cboProduct.Properties.ValueMember = "ID";
            cboProduct.Properties.DisplayMember = "ProductCode";

        }

        void LoadModulaLocation()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetModulaLocation", "A", new string[] { "@ModulaLocationID", "@Keyword", "@IsDeleted" }, new object[] { 0, "", 0 });
            cboModulaLocationDetail.Properties.ValueMember = "ModulaLocationDetailID";
            cboModulaLocationDetail.Properties.DisplayMember = "LocationName";
            cboModulaLocationDetail.Properties.DataSource = dt;
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
                txtSerialNumber.Text = modelrtc.SerialNumber;
                cboModulaLocationDetail.EditValue = modelrtc.ModulaLocationDetailID;
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
            if (TextUtils.ToInt(cboProduct.EditValue) <= 0)
            {
                MessageBox.Show("Thiết bị không được để trống !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtQrCode.Text))
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

            if (!string.IsNullOrWhiteSpace(txtSerialNumber.Text))
            {
                var exp1 = new Expression("SerialNumber", txtSerialNumber.Text.Trim());
                var exp2 = new Expression("WarehouseID", warehouseID);
                var exp3 = new Expression("ID", modelrtc.ID, "<>");
                var qrCodes = SQLHelper<ProductRTCQRCodeModel>.FindByExpression(exp1.And(exp2).And(exp3));
                if (qrCodes.Count > 0)
                {
                    MessageBox.Show($"Mã Serial Number [{txtSerialNumber.Text}] đã được sử dụng vui lòng kiểm tra lại !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }


            //if (modelrtc.ID > 0)
            //{
            //    DataTable dt = TextUtils.Select($"Select top 1 * from ProductRTCQRCode where ProductQRCode='{txtQrCode.Text.Trim()}' and ID <> {modelrtc.ID}");

            //    if (dt.Rows.Count > 0)
            //    {
            //        MessageBox.Show("Mã QrCode đã được sử dụng vui lòng kiểm tra lại !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        txtQrCode.Text = modelrtc.ProductQRCode;
            //        return false;
            //    }
            //}
            //else
            //{
            //    DataTable dt = TextUtils.Select($"Select top 1 * from ProductRTCQRCode where ProductQRCode='{txtQrCode.Text.Trim()}'");
            //    if (dt.Rows.Count > 0)
            //    {
            //        MessageBox.Show("Mã QrCode đã được sử dụng vui lòng kiểm tra lại !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return false;
            //    }
            //}

            return true;
        }
        bool save()
        {
            if (!validate()) return false;

            modelrtc.ProductQRCode = txtQrCode.Text;
            modelrtc.ProductRTCID = TextUtils.ToInt(cboProduct.EditValue);
            modelrtc.Status = comboBox1.SelectedIndex + 1;
            modelrtc.WarehouseID = warehouseID;
            modelrtc.SerialNumber = txtSerialNumber.Text.Trim();
            modelrtc.ModulaLocationDetailID = TextUtils.ToInt(cboModulaLocationDetail.EditValue);
            if (modelrtc.ID > 0)
            {
                ProductRTCQRCodeBO.Instance.Update(modelrtc);
            }
            else
            {
                ProductRTCQRCodeBO.Instance.Insert(modelrtc);
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
            {
                this.DialogResult = DialogResult.OK;

            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (save())
            {
                cboProduct.EditValue = -1;
                txtQrCode.Text = "";

            }
        }

        private void cboProduct_EditValueChanged(object sender, EventArgs e)
        {
            //int id = TextUtils.ToInt(cboProduct.EditValue);
            //if (id>0)
            //{
            //    ProductRTCQRCodeModel codeModel = (ProductRTCQRCodeModel)ProductRTCQRCodeBO.Instance.FindByCode("ProductRTCID", $"{id}");
            //    if(codeModel!=null)
            //    {
            //        txtQrCode.Text = codeModel.ProductQRCode; 
            //    }
            //    else
            //    {
            //        txtQrCode.Text = "";
            //    }
            //}    
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmAddQRCodeDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmAddQRCodeDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
