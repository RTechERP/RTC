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
    public partial class frmProject : _Forms
    {
        private bool _isAdd;
        public frmProject()
        {
            InitializeComponent();
        }

        private void frmProject_Load(object sender, EventArgs e)
        {
            loadUser();
            loadCustomer();
            loadData();

            cboUser.EditValue = Global.UserID;
        }

        #region Methods
        /// <summary>
        /// Lấy danh sách người phụ trách lên combo
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName,Code+'-'+FullName AS UserInfo FROM dbo.Users");
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = dt;
        }
        /// <summary>
        /// Lấy danh sách khách hàng lên combo chọn
        /// </summary>
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Customer ORDER BY CreatedDate DESC");
            cboCustomer.Properties.DisplayMember = "CustomerShortName";
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DataSource = dt;
        }

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
            txtCode.Text = "";
            cboCustomer.EditValue = null;
            dteDateFinish.EditValue = null;
            dteStartDate.EditValue = null;
            dteDateSingingContract.EditValue = null;
            txtShortName.Text = "";
            txtNote.Text = "";
            dteOrderDate.EditValue = null;
            dteOrderDateDeadLine.EditValue = null;
            txtQtyDay.EditValue = 0;
        }

        private bool checkValid()
        {
            if (cboCustomer.EditValue == null)
            {
                MessageBox.Show("Xin hãy chọn một khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCode.Text ))
            {
                MessageBox.Show("Xin hãy điền mã.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                int strID = _isAdd ? 0 : TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, "ID"));
                if (TextUtils.CheckExistTable(strID, "ProjectCode", txtCode.Text.Trim(), "Project"))
                {
                    MessageBox.Show("Mã này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Xin hãy điền tên.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (cboProjectStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Xin hãy chọn trạng thái.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (cboProjectType.SelectedIndex < 0)
            {
                MessageBox.Show("Xin hãy chọn loại dự án.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (cboUser.EditValue == null)
            {
                MessageBox.Show("Xin hãy chọn người phụ trách.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetProjectPaging"
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

            txtCode.Text = TextUtils.CreateNewCode("Project", "ProjectCode", "DA");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!grvData.IsDataRow(grvData.FocusedRowHandle))
                return;
            setInterface(true);
            _isAdd = false;

            txtName.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("ProjectName"));
            txtCode.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("ProjectCode"));

            cboCustomer.EditValue = TextUtils.ToInt(grvData.GetFocusedRowCellValue("CustomerID"));
            cboUser.EditValue = TextUtils.ToInt(grvData.GetFocusedRowCellValue("UserID"));
            cboProjectStatus.SelectedIndex = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ProjectStatus"));
            cboProjectType.SelectedIndex = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ProjectType"));

            dteDateSingingContract.EditValue = TextUtils.ToDate2(grvData.GetFocusedRowCellValue("DateSingingContract"));
            dteStartDate.EditValue = TextUtils.ToDate2(grvData.GetFocusedRowCellValue("StartDate"));
            dteDateFinish.EditValue = TextUtils.ToDate2(grvData.GetFocusedRowCellValue("DateFinishF"));

            dteOrderDate.EditValue = TextUtils.ToDate2(grvData.GetFocusedRowCellValue("DateOrder"));
            dteOrderDateDeadLine.EditValue = TextUtils.ToDate2(grvData.GetFocusedRowCellValue("OrderDateDeadLine"));
            txtQtyDay.EditValue = TextUtils.ToInt(grvData.GetFocusedRowCellValue("QtyOrderDay"));
            txtNote.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("Note"));
            txtShortName.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("ProjectShortName"));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!grvData.IsDataRow(grvData.FocusedRowHandle))
                return;

            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));

            string strName = TextUtils.ToString(grvData.GetFocusedRowCellValue("ProjectName"));

            if (QuotationBO.Instance.CheckExist("ProjectID", strID))
            {
                MessageBox.Show("Dự án này phát sinh ở những nghiệp vụ khác nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (RequestPriceBO.Instance.CheckExist("ProjectID", strID))
            {
                MessageBox.Show("Dự án này phát sinh ở những nghiệp vụ khác nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (PurchaseOrderBO.Instance.CheckExist("ProjectID", strID))
            {
                MessageBox.Show("Dự án này phát sinh ở những nghiệp vụ khác nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa [{0}] không?", strName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    //ProjectBO.Instance.Delete(strID);
                    TextUtils.ExcuteSQL("UPDATE dbo.Project SET IsDeleted = 1 WHERE ID = " + strID);
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

                ProjectModel dModel;
                if (_isAdd)
                {
                    dModel = new ProjectModel();
                }
                else
                {
                    int ID = Convert.ToInt32(grvData.GetFocusedRowCellValue("ID"));
                    dModel = (ProjectModel)ProjectBO.Instance.FindByPK(ID);
                }

                dModel.ProjectCode = txtCode.Text.Trim();
                dModel.ProjectName = txtName.Text.Trim();

                dModel.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
                dModel.UserID = TextUtils.ToInt(cboUser.EditValue);
                dModel.ProjectType = cboProjectType.SelectedIndex;
                dModel.ProjectStatus = cboProjectStatus.SelectedIndex;

                dModel.DateSingingContract = TextUtils.ToDate2(dteDateSingingContract.EditValue);
                dModel.StartDate = TextUtils.ToDate2(dteStartDate.EditValue);
                dModel.DateFinishF= TextUtils.ToDate2(dteDateFinish.EditValue);

                dModel.ProjectShortName = txtShortName.Text.Trim();
                dModel.Note = txtNote.Text.Trim();
                dModel.DateOrder = TextUtils.ToDate2(dteOrderDate.EditValue);
                dModel.OrderDateDeadLine = TextUtils.ToDate2(dteOrderDateDeadLine.EditValue);
                dModel.QtyOrderDay = TextUtils.ToInt(txtQtyDay.EditValue);

                if (_isAdd)
                {
                    ProjectBO.Instance.Insert(dModel);
                }
                else
                    ProjectBO.Instance.Update(dModel);

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
                txtCode.Text = TextUtils.CreateNewCode("Project", "ProjectCode", "DA");
            }
        }

        void loadOrderDate()
        {
            if (dteOrderDate.EditValue != null)
            {
                DateTime orderDate = TextUtils.ToDate3(dteOrderDate.EditValue);
                dteOrderDateDeadLine.EditValue = orderDate.AddDays(TextUtils.ToInt(txtQtyDay.EditValue));
            }
        }

        private void txtQtyDay_EditValueChanged(object sender, EventArgs e)
        {
            loadOrderDate();
        }

        private void dteOrderDate_EditValueChanged(object sender, EventArgs e)
        {
            loadOrderDate();
        }
    }
}
