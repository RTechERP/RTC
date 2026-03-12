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
    public partial class frmCustomer_Truoc : _Forms
    {
        private bool _isAdd;
        public frmCustomer_Truoc()
        {
            InitializeComponent();
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            txtCode.ReadOnly = true;
            loadData();            
        }

        #region Methods
        private void setInterface(bool isEdit)
        {
            //txtCode.ReadOnly = !isEdit;

            grdData.Enabled = !isEdit;

            btnSave.Visible = isEdit;
            btnCancel.Visible = isEdit;

            btnNew.Visible = !isEdit;
            btnEdit.Visible = !isEdit;
            btnDelete.Visible = !isEdit;
        }

        private void clearInterface()
        {
            txtName.Text = "";
            txtShortName.Text = "";
            txtCode.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtPhoneNumber.Text = "";
            txtContactPhone.Text = "";
            txtContactName.Text = "";
            txtContactEmail.Text = "";
        }

        private bool checkValid()
        {
            if (txtCode.Text == "")
            {
                MessageBox.Show("Xin hãy điền mã của khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                int strID = _isAdd ? 0 : TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, "ID"));
                if (TextUtils.CheckExistTable(strID, "CustomerCode", txtCode.Text.Trim(), "Customer"))
                {
                    MessageBox.Show("Mã này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("Xin hãy điền tên của khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        private void loadData()
        {
            try
            {
                txtPageNumber.Text = "1";
                txtTotalPage.Text = "1";

                DataSet oDataSet = loadDataSet();
                
                txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0][0]);
            }
            catch (Exception)
            {
            }
        }

        DataSet loadDataSet()
        {
            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetCustomerPaging"
                    , new string[] { "@PageNumber", "@PageSize", "@FilterText" }
                    , new object[] { int.Parse(txtPageNumber.Text), (int)txtPageSize.Value, txtFilterText.Text.Trim() });

            grdData.DataSource = oDataSet.Tables[0];

            return oDataSet;
        }
        
        #endregion

        #region Buttons Events
        private void btnNew_Click(object sender, EventArgs e)
        {
            setInterface(true);
            _isAdd = true;
            
            txtCode.Text = TextUtils.CreateNewCode("Customer", "CustomerCode", "KH");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!grvData.IsDataRow(grvData.FocusedRowHandle))
                return;
            setInterface(true);
            _isAdd = false;
            txtName.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("CustomerName"));
            txtShortName.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("CustomerShortName"));
            txtCode.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("CustomerCode"));
            txtPhoneNumber.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("Phone"));
            txtAddress.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("Address"));
            txtEmail.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("Email"));
            txtContactEmail.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("ContactEmail"));
            txtContactName.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("ContactName"));
            txtContactPhone.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("ContactPhone"));
            txtContactNote.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("ContactNote"));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!grvData.IsDataRow(grvData.FocusedRowHandle))
                return;

            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));

            string strName = TextUtils.ToString(grvData.GetFocusedRowCellValue("CustomerName"));

            if (ProjectBO.Instance.CheckExist("CustomerID", strID))
            {
                MessageBox.Show("Khách hàng này đang được sử dụng bên Dự án nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa [{0}] không?", strName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    //CustomerBO.Instance.Delete(strID);
                    TextUtils.ExcuteSQL("UPDATE dbo.Customer SET IsDeleted = 1 WHERE ID = " + strID);
                    grvData.DeleteSelectedRows();
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkValid()) return;

                CustomerModel dModel;
                if (_isAdd)
                {
                    dModel = new CustomerModel();
                }
                else
                {
                    int ID = Convert.ToInt32(grvData.GetFocusedRowCellValue("ID"));
                    dModel = (CustomerModel)CustomerBO.Instance.FindByPK(ID);
                }

                dModel.CustomerCode = txtCode.Text.Trim();
                dModel.CustomerName = txtName.Text.Trim();
                dModel.CustomerShortName = txtShortName.Text.Trim();
                dModel.Address = txtAddress.Text.Trim();
                dModel.Phone = txtPhoneNumber.Text.Trim();
                dModel.Email = txtEmail.Text.Trim();
                dModel.ContactEmail = txtContactEmail.Text.Trim();
                dModel.ContactName = txtContactName.Text.Trim();
                dModel.ContactPhone = txtContactPhone.Text.Trim();
                dModel.ContactNote = txtContactNote.Text.Trim();

                if (_isAdd)
                {
                    CustomerBO.Instance.Insert(dModel);
                }
                else
                    CustomerBO.Instance.Update(dModel);

                loadData();
                setInterface(false);
                clearInterface();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            setInterface(false);
            clearInterface();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            if (grvData.RowCount > 0 && btnEdit.Enabled == true)
                btnEdit_Click(null, null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadDataSet();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadDataSet();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadDataSet();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadDataSet();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtCode_DoubleClick(object sender, EventArgs e)
        {
            if (_isAdd)
            {
                txtCode.Text = TextUtils.CreateNewCode("Customer", "CustomerCode", "KH");
            }
        }
    }
}
