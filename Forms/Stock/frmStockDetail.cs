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
    public partial class frmStockDetail : _Forms
    {
        public int check = 0;
        public int ID = 0;
        public StockModel Stock = new StockModel();
        public frmStockDetail()
        {
            InitializeComponent();
        }

        private void frmSupplierDetail_Load(object sender, EventArgs e)
        {
            loadUsers();

            if (Stock.ID == 0)
            {
                cboManagers.SelectedIndex = -1;
            }
            else
            {
                txtCode.Text = Stock.StockCode;
                txtName.Text = Stock.StockName;
                txtPhoneNumber.Text = Stock.PhoneNumber;
                cboManagers.SelectedValue = Stock.StockManager;
            }
        }
        public void loadUsers()
        {
            DataTable dt1 = TextUtils.Select("SELECT ID,FullName FROM dbo.Users");
            cboManagers.DataSource = dt1;
            cboManagers.DisplayMember = "FullName";
            cboManagers.ValueMember = "ID";
        }
        /// <summary>
        /// Load dữ liệu vào form
        /// </summary>
        public void loadData()
        {
            //if (ID == 0)
            //{
            //    txtCode.Text = TextUtils.CreateNewCode("Supplier", "SupplierCode", "S");
            //}
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Stock WHERE ID = " + ID);
            DataTable dt1 = TextUtils.Select("SELECT ID,FullName FROM dbo.Users");

            txtCode.Text = Lib.ToString(dt.Rows[0]["StockCode"]);
            txtName.Text = Lib.ToString(dt.Rows[0]["StockName"]);
            cboManagers.DataSource = dt1;
            cboManagers.DisplayMember = "FullName";
            cboManagers.ValueMember = "ID";
            txtPhoneNumber.Text = Lib.ToString(dt.Rows[0]["PhoneNumber"]);



            //grdData.DataSource = dt;
        }

        /// <summary>
        /// Validate trước khi cất dữ liệu
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền mã kho.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                DataTable dt;
                if (Stock.ID > 0)
                {
                    int strID = Stock.ID;
                    dt = TextUtils.Select("select top 1 StockCode from Stock where StockCode = '" + txtCode.Text.Trim() + "' and ID <> " + strID);
                }
                else
                {
                    dt = TextUtils.Select("select top 1 StockCode from Stock where StockCode = '" + txtCode.Text.Trim() + "'");
                }
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Mã kho này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
            }
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền tên kho.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            Stock.StockCode = txtCode.Text.Trim();
            Stock.StockName = txtName.Text.Trim();
            Stock.StockManager = Lib.ToInt(cboManagers.SelectedValue);
            Stock.PhoneNumber = txtPhoneNumber.Text.Trim();
            if (Stock.ID > 0)
            {
                StockBO.Instance.Update(Stock);
            }
            else
            {
                Stock.ID = (int)StockBO.Instance.Insert(Stock);
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
            if (!saveData()) return;

            Stock = new StockModel();
            loadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSupplierDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }
    }
}
