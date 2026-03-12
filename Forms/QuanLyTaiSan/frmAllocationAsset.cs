using BMS.Business;
using BMS.Model;
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
    public partial class frmAllocationAsset : _Forms
    {
        public TSAssetManagementModel asset = new TSAssetManagementModel();
        public TSAllocationEvictionAssetModel allocation = new TSAllocationEvictionAssetModel();
        public frmAllocationAsset()
        {
            InitializeComponent();
        }
        #region Load Data
        private void frmAllocationAsset_Load(object sender, EventArgs e)
        {
            try
            {
                txtMaTS.Text = asset.TSAssetCode;
                txtTenTS.Text = asset.TSAssetName;
                txtMaNCC.Text = asset.TSCodeNCC;
                //DataTable dt = TextUtils.Select($"Select TOP 1 AssetType from dbo.TSAsset Where ID = '{asset.TSAssetID}'");
                //txtLoaiTS.Text = TextUtils.ToString(dt.Rows[0]["AssetType"]);

                TSAssetModel TSAsset = SQLHelper<TSAssetModel>.SqlToModel($"Select TOP 1 AssetType from dbo.TSAsset Where ID = '{asset.TSAssetID}'");

                txtLoaiTS.Text = TSAsset.AssetType;

                txtQuyCach.Text = asset.SpecificationsAsset;
                txtSeri.Text = asset.Seri;
                dtpDateBuy.Value = asset.DateBuy.HasValue == true ? asset.DateBuy.Value : DateTime.Now;
                txtStatus.Text = asset.Status;
                txtLyDo.Text = "Cấp phát thiết bị mới cho nhân viên";
                LoadAllocationAsset();
                LoadcboNV();
                LoadcboViTri();
                LoadcboPhongCT();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        void LoadAllocationAsset()
        {
            if(allocation.ID > 0)
            {
                cboNV.EditValue = allocation.EmployeeID;
                cboViTri.EditValue = allocation.ChucVuID;
                cboPhongCT.EditValue = allocation.DepartmentID;
                dtpNgayCapPhat.Value = allocation.DateAllocation.HasValue == true ? allocation.DateAllocation.Value : DateTime.Now;
                txtLyDo.Text = allocation.Note;
            }
        }
        void LoadcboNV()
        {
            DataTable NV = TextUtils.Select("Select ID, Code, FullName, ChuVuID, DepartmentID from dbo.Employee");
            cboNV.Properties.DataSource = NV;
            cboNV.Properties.DisplayMember = "FullName";
            cboNV.Properties.ValueMember = "ID";
        }
        void LoadcboViTri()
        {
            DataTable ViTri = TextUtils.Select("Select ID, Code, Name from dbo.EmployeeChucVu");
            cboViTri.Properties.DataSource = ViTri;
            cboViTri.Properties.DisplayMember = "Name";
            cboViTri.Properties.ValueMember = "ID";
        }
        void LoadcboPhongCT()
        {
            DataTable PhongCT = TextUtils.Select("Select ID, Code, Name from dbo.Department");
            cboPhongCT.Properties.DataSource = PhongCT;
            cboPhongCT.Properties.DisplayMember = "Name";
            cboPhongCT.Properties.ValueMember = "ID";
        }
        #endregion

        public bool ValidateForm()
        {
            if (cboNV.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào tên người được cấp phát !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboViTri.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào vị trí công tác !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboPhongCT.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào tên phòng công tác !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;

            allocation.AssetManagementID = asset.ID;
            allocation.EmployeeID = TextUtils.ToInt(cboNV.EditValue);
            allocation.ChucVuID = TextUtils.ToInt(cboViTri.EditValue);
            allocation.DepartmentID = TextUtils.ToInt(cboPhongCT.EditValue);
            allocation.DateAllocation = new DateTime(dtpNgayCapPhat.Value.Year, dtpNgayCapPhat.Value.Month, dtpNgayCapPhat.Value.Day);
            allocation.Note = txtLyDo.Text.Trim();
            allocation.Status = "Đang sử dụng";
            if (allocation.ID > 0)
            {
                TSAllocationEvictionAssetBO.Instance.Update(allocation);
            }
            else
            {
                allocation.ID = (int)TSAllocationEvictionAssetBO.Instance.Insert(allocation);
            }

            asset.EmployeeID = allocation.EmployeeID;
            asset.DepartmentID = allocation.DepartmentID;
            asset.Note = txtLyDo.Text.Trim();
            asset.Status = "Đang sử dụng";
            asset.StatusID = 2;
            if (asset.ID > 0)
            {
                TSAssetManagementBO.Instance.Update(asset);
            }
            return true;
        }

        #region Button 
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(saveData())
            {
                this.DialogResult = DialogResult.OK;
            }    
        }

        private void cboNV_EditValueChanged(object sender, EventArgs e)
        {
            cboViTri.EditValue = TextUtils.ToInt(grdData.GetFocusedRowCellValue(colChuVuID));
            cboPhongCT.EditValue = TextUtils.ToInt(grdData.GetFocusedRowCellValue(colDepartmentID));
        }
        #endregion
    }
}