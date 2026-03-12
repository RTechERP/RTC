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


namespace BMS
{
    public partial class frmCustomerBaseDetail : _Forms
    {
        public CustomerBaseModel customerBase = new CustomerBaseModel();

        public frmCustomerBaseDetail()
        {
            InitializeComponent();
        }
        private void frmCustomerBaseDetail_Load(object sender, EventArgs e)
        {
            cboUser.EditValue = Global.UserID;
            loadUsers();
            loadCustomerBaseDetail();
        }

        /// <summary>
        /// load nhân viên, người giao
        /// </summary>
        public void loadUsers()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM Users");
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = dt;
        }

        /// <summary>
        /// load Data
        /// </summary>
        private void loadCustomerBaseDetail()
        {
            if (customerBase.ID > 0 )
            {
                cboUser.EditValue = TextUtils.ToInt(customerBase.UserID);
                txtCustomerCode.Text = customerBase.CustomerCode;
                txtCustomerName.Text = customerBase.CustomerName;
                txtAddress.Text = customerBase.Address;
                cbCustomerType.SelectedIndex = customerBase.CustomerType;
                txtProductName.Text = customerBase.ProductName;
                txtProvince.Text = customerBase.Province;
                txtKCN.Text = customerBase.KCN;
                txtNote.Text = customerBase.Note;
                txtCreatedDate.Value = TextUtils.ToDate3(customerBase.CreatedDate);
            }
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.CustomerBaseContact WHERE CustomerID = " + customerBase.ID);
            grdData.DataSource = dt;
        }


        /// <summary>
        /// click button save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// hàm save
        /// </summary> 
        bool saveData()
        {
            if (!ValidateForm()) return false;
            customerBase.UserID = TextUtils.ToInt(cboUser.EditValue);
            customerBase.CustomerCode = txtCustomerCode.Text.Trim();
            customerBase.CustomerName = txtCustomerName.Text.Trim();
            customerBase.Address = txtAddress.Text.Trim();
            customerBase.CreatedDate = TextUtils.ToDate3(txtCreatedDate.Value);
            customerBase.CustomerType = TextUtils.ToInt(cbCustomerType.SelectedIndex);
            customerBase.ProductName = txtProductName.Text.Trim();
            customerBase.Province = txtProvince.Text.Trim();
            customerBase.KCN = txtKCN.Text.Trim();
            customerBase.Note = txtNote.Text.Trim();
            if (customerBase.ID > 0)
            {
                CustomerBaseBO.Instance.Update(customerBase);
            }
            else
            {
                customerBase.ID = (int)CustomerBaseBO.Instance.Insert(customerBase);
            }
            for (int i = 0; i < grvData.RowCount; i++)
            {
                CustomerBaseContactModel detail = new CustomerBaseContactModel();
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                if (ID > 0)
                {
                    detail = (CustomerBaseContactModel)CustomerBaseContactBO.Instance.FindByPK(ID);
                }
                detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                detail.CustomerID = customerBase.ID;
                detail.ContactName = TextUtils.ToString(grvData.GetRowCellValue(i, colContactName));
                detail.CustomerPosition = TextUtils.ToString(grvData.GetRowCellValue(i, colCustomerPosition));
                detail.ContactPhone = TextUtils.ToString(grvData.GetRowCellValue(i, colContactPhone));
                detail.ContactEmail = TextUtils.ToString(grvData.GetRowCellValue(i, colContactEmail));
                if (ID == 0)
                {
                    CustomerContactBO.Instance.Insert(detail);
                }
                else
                {
                    CustomerContactBO.Instance.Update(detail);
                }
            }
            return true;
        }

        /// <summary>
        /// click button lưu và thêm mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            saveData();
            cboUser.Text = "";
            txtCustomerCode.Clear();
            txtCustomerName.Clear();
            txtAddress.Clear();
            txtProvince.Clear();
            txtProductName.Clear();
            txtKCN.Clear();
            cbCustomerType.Items.Clear();
            txtNote.Clear();
            for (int i = grvData.RowCount - 1; i >= 0; i--)
            {
                grvData.DeleteRow(i);
            }
            customerBase = new CustomerBaseModel();
            loadCustomerBaseDetail();
        }

        /// <summary>
        /// tắt form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProductKHACHHANG_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// click chuột phải để save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveData();
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// check lỗi
        /// </summary>
        /// <returns></returns>
        /// 
        bool ValidateForm()
        {
            DataTable dt;
            if (customerBase.ID > 0)
            {
                dt = TextUtils.Select("select top 1 CustomerCode from Customer where CustomerCode = '" + txtCustomerCode.Text.Trim() + "' and ID <> " + customerBase.ID);
            }
            else
            {
                dt = TextUtils.Select("select top 1 CustomerCode from Customer where CustomerCode = '" + txtCustomerCode.Text.Trim() + "'");

            }
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Mã khách hàng đã tồn tại, vui lòng kiểm tra lại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtCustomerCode.Text == "")
            {
                MessageBox.Show("Xin vui lòng nhập mã khách hàng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtProductName.Text == "")
            {
                MessageBox.Show("Xin vui lòng nhập sản phẩm!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtCustomerName.Text == "")
            {
                MessageBox.Show("Xin vui lòng nhập tên khách hàng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtAddress.Text == "")
            {
                MessageBox.Show("Xin vui lòng nhập địa chỉ khách hàng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void txtCustomerShortName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)Keys.Space;
        }
    }
}
