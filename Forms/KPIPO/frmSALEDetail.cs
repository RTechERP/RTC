using BMS.Business;
using BMS.Model;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;


namespace BMS
{
    public partial class frmSALEDetail : _Forms
    {
        int warehouseID = 1;
        public SALEModel saleModel = new SALEModel();
        public static int ckeck = 0;

        public frmSALEDetail()
        {
            InitializeComponent();

        }
        /// <summary>
        /// load dữ liệu lên khi load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProductTestDetail_Load(object sender, EventArgs e)
        {
            loadUsers();
            loadMainIndex();
            loadProductTestDetail();
            loadCustomer();
        }

        #region Methods
        /// <summary>
        /// load ProductTestDetail
        /// </summary>
        private void loadProductTestDetail()
        {
            if (saleModel.ID > 0)
            {
                txtPOCustomer.Focus();
                txtPOCustomer.Text = TextUtils.ToString(saleModel.POCustomer);
                cbCustomer.EditValue = saleModel.CustomerID;
                txtSale.Text = TextUtils.ToString(saleModel.Sale);
                txtCreateDate.Value = TextUtils.ToDate3(saleModel.SaleDate);
                cbUsers.EditValue = saleModel.UserID;
                //cboType.SelectedIndex = saleModel.Type;
                cbType.EditValue = saleModel.Type;
            }
        }

        /// <summary>
        /// load khách hàng
        /// </summary>
        DataTable dtCustomer;
        private void loadCustomer()
        {
            dtCustomer = TextUtils.Select("SELECT * FROM Customer where IsDeleted <> 1");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dtCustomer;
        }

        /// <summary>
        /// load người phụ trách
        /// </summary>
        public void loadUsers()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM Users");
            cbUsers.Properties.DisplayMember = "FullName";
            cbUsers.Properties.ValueMember = "ID";
            cbUsers.Properties.DataSource = dt;
        }

        /// <summary>
        /// load type
        /// </summary>
        public void loadMainIndex()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM MainIndex where ID < 7");
            cbType.Properties.DisplayMember = "MainIndex";
            cbType.Properties.ValueMember = "ID";
            cbType.Properties.DataSource = dt;
        }
        #endregion

        #region Buttons Events
        /// <summary>
        /// click button save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData()) this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// click button sve new
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            saveData();
            txtPOCustomer.Clear();
            cbCustomer.Text = "";
            cbUsers.Text = "";
            cbType.Text = "";
            txtSale.Text = "";
            saleModel = new SALEModel();
        }
        #endregion

        /// <summary>
        /// hàm save
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {
            if (!ValidateForm()) return false;
            try
            {
                saleModel.POCustomer = TextUtils.ToString(txtPOCustomer.Text.Trim());
                saleModel.CustomerID = TextUtils.ToInt(cbCustomer.EditValue);
                saleModel.UserID = TextUtils.ToInt(cbUsers.EditValue);
                saleModel.Sale = TextUtils.ToDecimal(txtSale.Text.Trim());
                saleModel.SaleDate = TextUtils.ToDate3(txtCreateDate.Value);
                saleModel.Type = TextUtils.ToInt(cbType.EditValue);
                saleModel.Month = DateTime.Now.Month;
                saleModel.Year = DateTime.Now.Year;
                if (saleModel.ID > 0)
                {
                    SALEBO.Instance.Update(saleModel);
                }
                else
                {
                    SALEBO.Instance.Insert(saleModel);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi Update", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }

        private void frmProductTestDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// check lỗi
        /// </summary>
        /// <returns></returns>
        bool ValidateForm()
        {
            if (txtPOCustomer.Text.Trim() == "")
            {
                MessageBox.Show("Xin vui lòng điền khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txtSale.Text.Trim() == "")
            {
                MessageBox.Show("Xin vui lòng điền sale.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cbUsers.Text == "")
            {
                MessageBox.Show("Xin vui lòng chọn người phụ trách.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            DataTable dt;
            if (saleModel.ID > 0)
            {
                dt = TextUtils.Select("select top 1 POCustomer from SALE where POCustomer = '" + txtPOCustomer.Text + "' and ID <> " + saleModel.ID);
            }
            else
            {
                dt = TextUtils.Select("select top 1 POCustomer from ProductTest where POCustomer = '" + txtPOCustomer.Text + "'");
            }
            return true;
        }


        /// <summary>
        /// định dạng txtSale được ngăn cách dấu phẩy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSale_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSale.Text))
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                int valueBefore = Int32.Parse(txtSale.Text, System.Globalization.NumberStyles.AllowThousands);
                txtSale.Text = String.Format(culture, "{0:N0}", valueBefore);
                txtSale.Select(txtSale.Text.Length, 0);
            }
        }

        /// <summary>
        /// định dạng txtSale chỉ điền được số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// enter txtPOCustomer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPOCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getCustomer();
                ckeck = 1;
            }
        }

        private void txtSale_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSale.Text))
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                int valueBefore = Int32.Parse(txtSale.Text, System.Globalization.NumberStyles.AllowThousands);
                txtSale.Text = String.Format(culture, "{0:N0}", valueBefore);
                txtSale.Select(txtSale.Text.Length, 0);
            }
        }

        private void cbCustomer_Click(object sender, EventArgs e)
        {
            if (ckeck == 0)
            {
                getCustomer();
            }
            if (ckeck == 1)
            {
                cbCustomer.ReadOnly = cbUsers.ReadOnly = false;
            }
            else if (ckeck == 2)
            {
                return;
            }
            else if (ckeck == 3)
            {
                cbCustomer.ReadOnly = cbUsers.ReadOnly = true;
            }
        }

        /// <summary>
        /// load khách hàng
        /// </summary>
        void getCustomer()
        {
            if (txtPOCustomer.Text.Trim() == "") return;
            DataTable dt = TextUtils.Select("SELECT ID,POCode,CustomerID,UserID FROM POKH");
            DataRow[] dr = dt.Select($"POCode='{txtPOCustomer.Text}'");
            if (dr.Length > 0)
            {
                cbCustomer.EditValue = TextUtils.ToString(dr[0]["CustomerID"]);
                cbUsers.EditValue = TextUtils.ToString(dr[0]["UserID"]);
                ckeck = 3;
            }
            else
            {
                if (MessageBox.Show(string.Format("PO này không tồn tại! Vui lòng kiểm tra lại?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    cbCustomer.ReadOnly = cbUsers.ReadOnly = true;
                    cbCustomer.Text = cbUsers.Text = "";
                    ckeck = 2;
                }
                else
                {
                    cbCustomer.ReadOnly = cbUsers.ReadOnly = false;
                    ckeck = 1;
                }
            }
        }

        private void txtPOCustomer_Click(object sender, EventArgs e)
        {
            ckeck = 0;
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            //frmCustomerDetail frm = new frmCustomerDetail();
            frmCustomerDetailNew frm = new frmCustomerDetailNew(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCustomer();
            }
        }
    }
}
