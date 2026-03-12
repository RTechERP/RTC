using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMS.Utils;
using BMS.Business;
using BMS.Model;
using DevExpress.Utils;

namespace BMS
{
    public partial class frmManufacturer : _Forms
    {
        private bool _isAdd;
        public bool HasDialogResult = false;

        public frmManufacturer()
        {
            InitializeComponent();
        }

        private void frmManufacturer_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                TextUtils.ShowError(ex);
            }
          //  DocUtils.InitFTPTK();
        }

        #region Methods
        private void SetInterface(bool isEdit)
        {
            txtCode.ReadOnly = !isEdit;

            grdData.Enabled = !isEdit;

            btnSave.Visible = isEdit;
            btnCancel.Visible = isEdit;

            btnNew.Visible = !isEdit;
            btnEdit.Visible = !isEdit;
            btnDelete.Visible = !isEdit;
        }

        private void ClearInterface()
        {
            txtName.Text = "";
            txtCode.Text = "";
            txtNote.Text = "";
        }

        private bool checkValid()
        {
            if (txtCode.Text == "")
            {
                MessageBox.Show("Xin hãy điền mã của hãng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                DataTable dt;
                if (!_isAdd)
                {
                    int strID = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, "ID").ToString());
                    dt = TextUtils.Select("select top 1 Manufacturer from Manufacturer where Manufacturer = '" + txtCode.Text.Trim() + "' and ID <> " + strID);
                }
                else
                {
                    dt = TextUtils.Select("select top 1 Manufacturer from Manufacturer where Manufacturer = '" + txtCode.Text.Trim() + "'");
                }
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Mã hãng này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("Xin hãy điền tên của hãng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
           

            return true;
        }
       

        private void LoadData()
        {
            try
            {
                DataTable tbl = TextUtils.Select("Select a.* from Manufacturer a with(nolock)");
                grdData.DataSource = tbl;
            }
            catch (Exception)
            {
            }

        }
        #endregion

        #region Buttons Events
        private void btnNew_Click(object sender, EventArgs e)
        {
            SetInterface(true);
            _isAdd = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!grvData.IsDataRow(grvData.FocusedRowHandle))
                return;
            SetInterface(true);
            _isAdd = false;

            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            ManufacturerModel dModel = (ManufacturerModel)ManufacturerBO.Instance.FindByPK(ID);

            txtName.Text = dModel.ManufacturerName;
            txtCode.Text = dModel.ManufacturerCode;
            txtNote.Text = dModel.Note;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!grvData.IsDataRow(grvData.FocusedRowHandle))
                return;

            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID).ToString());

            string strName = grvData.GetFocusedRowCellValue(colName).ToString();

            if (PartBO.Instance.CheckExist("ManufacturerID", strID))
            {
                MessageBox.Show("Khách hàng này đang được sử dụng nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa [{0}] không?", strName), TextUtils.Caption, 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    ManufacturerBO.Instance.Delete(strID);
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
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang hãng vật tư!"))
            {
                ProcessTransaction pt = new ProcessTransaction();
                pt.OpenConnection();
                pt.BeginTransaction();
                try
                {
                    if (checkValid())
                    {
                        ManufacturerModel dModel;
                        if (_isAdd)
                        {
                            dModel = new ManufacturerModel();
                        }
                        else
                        {
                            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                            dModel = (ManufacturerModel)ManufacturerBO.Instance.FindByPK(ID);
                        }

                        dModel.ManufacturerCode = txtCode.Text.Trim().ToUpper();
                        dModel.ManufacturerName = txtName.Text.Trim().ToUpper();
                        dModel.Note = txtNote.Text;

                        if (_isAdd)
                        {
                            pt.Insert(dModel);                           
                        }
                        else
                        {
                            pt.Update(dModel);                            
                        }
                        pt.CommitTransaction();
                        LoadData();
                        SetInterface(false);
                        ClearInterface();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    pt.CloseConnection();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetInterface(false);
            ClearInterface();
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

        private void frmCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (HasDialogResult)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

    }
}
