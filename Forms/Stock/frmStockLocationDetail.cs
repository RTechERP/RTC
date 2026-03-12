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
    public partial class frmStockLocationDetail : _Forms
    {
        public int check = 0;
        public int ID = 0;
        public StockLocationModel StockLocation = new StockLocationModel();
        public frmStockLocationDetail()
        {
            InitializeComponent();
        }

        private void frmSupplierDetail_Load(object sender, EventArgs e)
        {
            loadUsers();

            if (StockLocation.ID == 0)
            {
                cboManagers.SelectedIndex = -1;
            }
            else
            {
                txtCode.Text = StockLocation.StockLocationCode;
                txtName.Text = StockLocation.StockLocationName;
                cboManagers.SelectedValue = StockLocation.StockCode;
            }
        }
        public void loadUsers()
        {
            DataTable dt1 = TextUtils.Select("SELECT ID,StockName FROM dbo.Stock");
            cboManagers.DataSource = dt1;
            cboManagers.DisplayMember = "StockName";
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
            //txtPhoneNumber.Text = Lib.ToString(dt.Rows[0]["PhoneNumber"]);



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
                MessageBox.Show("Xin hãy điền mã vị trí.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                DataTable dt;
                if (StockLocation.ID > 0)
                {
                    int strID = StockLocation.ID;
                    dt = TextUtils.Select("select top 1 StockLocationCode from StockLocation where StockLocationCode = '" + txtCode.Text.Trim() + "' and ID <> " + strID);
                }
                else
                {
                    dt = TextUtils.Select("select top 1 StockLocationCode from StockLocation where StockLocationCode = '" + txtCode.Text.Trim() + "'");
                }
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Mã vị trí này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
            }
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền tên vị trí.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            StockLocation.StockLocationCode = txtCode.Text.Trim();
            StockLocation.StockLocationName = txtName.Text.Trim();
            StockLocation.StockCode = Lib.ToInt(cboManagers.SelectedValue);
            
            if (StockLocation.ID > 0)
            {
                StockBO.Instance.Update(StockLocation);
            }
            else
            {
                StockLocation.ID = (int)StockLocationBO.Instance.Insert(StockLocation);
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

            StockLocation = new StockLocationModel();
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
