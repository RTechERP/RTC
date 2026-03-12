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
    public partial class frmFilmProductsDetails : _Forms
    {
        public ProductFilmModel ProductFilm = new ProductFilmModel();
        public frmFilmProductsDetails()
        {
            InitializeComponent();
        }
        private void frmPartDetail_Load(object sender, EventArgs e)
        {
            loadParentID();
            loadStock();
            loadStockLocation();
            loadManufacture();
            if (ProductFilm.ID == 0)
            {
                cboManufacture.SelectedIndex = -1;
                cboParent.SelectedIndex = -1;
                cboStock.SelectedIndex = -1;
                cboStockLocation.SelectedIndex = -1;
            }
            else
            {
                txtCode.Text = ProductFilm.Code;
                txtName.Text = ProductFilm.Name;
                txtHeight.Text = Lib.ToString(ProductFilm.Height);
                txtWidth.Text = Lib.ToString(ProductFilm.Width);
                txtArea.Text = Lib.ToString(ProductFilm.Area);
                txtPcsPerBox.Text = Lib.ToString(ProductFilm.PcsPerBox);
                txtInventoryNumber.Text = Lib.ToString(ProductFilm.InventoryNumber);
                txtDescription.Text = Lib.ToString(ProductFilm.Description);
                cboManufacture.SelectedValue = ProductFilm.ManufactureID;
                cboParent.SelectedValue = ProductFilm.ParentID;
                cboStock.SelectedValue = ProductFilm.StockID;
                cboStockLocation.SelectedValue = ProductFilm.StockLocationID;
            }
        }

        private void loadParentID()
        {
            DataTable dt2 = TextUtils.Select("SELECT ID,Name FROM dbo.ProductFilm WHERE ParentID = 0");
            DataRow r = dt2.NewRow();
            r["ID"] = 0;
            r["Name"] = "";
            dt2.Rows.InsertAt(r, 0);
            cboParent.DataSource = dt2;
            cboParent.DisplayMember = "Name";
            cboParent.ValueMember = "ID";
        }

        private void loadManufacture()
        {
            DataTable dt2 = TextUtils.Select("SELECT ID,ManufacturerName FROM dbo.Manufacturer");
            DataRow r = dt2.NewRow();
            r["ID"] = 0;
            r["ManufacturerName"] = "";
            dt2.Rows.InsertAt(r, 0);
            cboManufacture.DataSource = dt2;
            cboManufacture.DisplayMember = "ManufacturerName";
            cboManufacture.ValueMember = "ID";
        }

        private void loadStockLocation()
        {
            DataTable dt1 = TextUtils.Select("SELECT ID,StockLocationName FROM dbo.StockLocation");
            DataRow r = dt1.NewRow();
            r["ID"] = 0;
            r["StockLocationName"] = "";
            dt1.Rows.InsertAt(r, 0);
            cboStockLocation.DataSource = dt1;
            cboStockLocation.DisplayMember = "StockLocationName";
            cboStockLocation.ValueMember = "ID";
        }

        private void loadStock()
        {
            DataTable dt = TextUtils.Select("SELECT ID,StockName FROM dbo.Stock");
            DataRow r = dt.NewRow();
            r["ID"] = 0;
            r["StockName"] = "";
            dt.Rows.InsertAt(r, 0);
            cboStock.DataSource = dt;
            cboStock.DisplayMember = "StockName";
            cboStock.ValueMember = "ID";
        }


        void loadPartGroup()
        {
            //cboPartGroup.Properties.DisplayMember = "PartGroupName";
            //cboPartGroup.Properties.ValueMember = "ID";
            //cboPartGroup.Properties.DataSource = PartGroupBO.Instance.FindAll();
        }

        /// <summary>
        /// Load dữ liệu vào form
        /// </summary>
        void loadData()
        {
            
            
        }

        /// <summary>
        /// Validate trước khi cất dữ liệu
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền mã thiết bị.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                DataTable dt;
                if (ProductFilm.ID > 0)
                {
                    int strID = ProductFilm.ID;
                    dt = TextUtils.Select("select top 1 Code from ProductFilm where Code = '" + txtCode.Text.Trim() + "' and ID <> " + strID);
                }
                else
                {
                    dt = TextUtils.Select("select top 1 Code from ProductFilm where Code = '" + txtCode.Text.Trim() + "'");
                }
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Mã thiết bị này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }

                if (TextUtils.CheckExistTable(ProductFilm.ID, "PartCode", txtCode.Text.Trim(), "Part"))
                {
                    MessageBox.Show("Mã này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền tên vật tư/thiết bị.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Cất dữ liệu
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {
            if (!ValidateForm())
                return false;

            ProductFilm.Code = txtCode.Text.Trim();
            ProductFilm.Name = txtName.Text.Trim();
            ProductFilm.Height =Lib.ToDecimal( txtHeight.Text.Trim());
            ProductFilm.Width = Lib.ToDecimal(txtWidth.Text.Trim());
            ProductFilm.Area =Lib.ToDecimal(txtArea.Text.Trim());
            ProductFilm.PcsPerBox=Lib.ToInt(txtPcsPerBox.Text.Trim());
            ProductFilm.InventoryNumber = Lib.ToInt(txtInventoryNumber.Text.Trim());
            ProductFilm.Description=txtDescription.Text.Trim();
            ProductFilm.ManufactureID=Lib.ToInt(cboManufacture.SelectedValue);
            ProductFilm.ParentID = Lib.ToInt(cboParent.SelectedValue);
            ProductFilm.StockID=Lib.ToInt(cboStock.SelectedValue);
            ProductFilm.StockLocationID=Lib.ToInt(cboStockLocation.SelectedValue);
            if (ProductFilm.ID > 0)
            {
                PartBO.Instance.Update(ProductFilm);
            }
            else
            {
                ProductFilm.ID = (int)PartBO.Instance.Insert(ProductFilm);
            }
            return true;
        } 

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
                this.DialogResult = DialogResult.OK;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                ProductFilm = new ProductFilmModel();
                loadData();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPartDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnPartGroup_Click(object sender, EventArgs e)
        {
            frmPartGroup frm = new frmPartGroup();
            frm.HasDialogResult = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPartGroup();
            }
        }

        private void txtHeight_KeyDown(object sender, KeyEventArgs e)
        {
            txtArea.Text=Lib.ToString( CallArea());
        }
        int Area;
        private int CallArea()
        {
            Area = Lib.ToInt(txtHeight.Text.Trim()) * Lib.ToInt(txtWidth.Text.Trim());
            return Area;
        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtWidth_KeyDown(object sender, KeyEventArgs e)
        {
            txtArea.Text = Lib.ToString(CallArea());
        }
    }
}
