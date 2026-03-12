using BMS.Business;
using BMS.Model;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmEvictionAsset : _Forms
    {
        public TSAllocationEvictionAssetModel eviction = new TSAllocationEvictionAssetModel();
        public TSAssetManagementModel asset = new TSAssetManagementModel();
        public frmEvictionAsset()
        {
            InitializeComponent();
        }

        #region Load Data
        private void frmEvictionAsset_Load(object sender, EventArgs e)
        {
            try
            {
                LoadcboNV();
                LoadcboViTri();
                LoadcboPhongCT();
                cboNguoiThuHoi.EditValue = Global.EmployeeID;
                DataTable dtViTri = TextUtils.Select($"Select ChuVuID, DepartmentID from dbo.Employee Where ID = '{cboNguoiThuHoi.EditValue}'");
                if (dtViTri.Rows.Count > 0)
                {
                    cboViTriCuaNTH.EditValue = TextUtils.ToInt(dtViTri.Rows[0]["ChuVuID"]);
                    cboPhongBanCuaNTH.EditValue = TextUtils.ToInt(dtViTri.Rows[0]["DepartmentID"]);
                }
                
                //DataTable loaits = TextUtils.Select($"Select TOP 1 AssetType from dbo.TSAsset Where ID = '{asset.TSAssetID}'");
                
                DataTable NV = TextUtils.LoadDataFromSP("spLoadTSLostBrokenReportAS", "A", new string[] { "@ID" }, new object[] { asset.ID });
                if (NV.Rows.Count <= 0)
                {
                    cboNguoiBiThuHoi.EditValue = 0;
                    cboViTriTH.EditValue = 0;
                    cboPhongCTTH.EditValue = 0;
                }
                else
                {
                    cboNguoiBiThuHoi.EditValue = TextUtils.ToInt(NV.Rows[0]["EmployeeID"]);
                    cboViTriTH.EditValue = TextUtils.ToInt(NV.Rows[0]["ChucVuID"]);
                    cboPhongCTTH.EditValue = TextUtils.ToInt(NV.Rows[0]["DepartmentID"]);
                }
                txtMaTS.Text = asset.TSAssetCode;
                txtTenTS.Text = asset.TSAssetName;
                txtMaNCC.Text = asset.TSCodeNCC;
                //txtLoaiTS.Text = TextUtils.ToString(loaits.Rows[0]["AssetType"]);
                txtLoaiTS.Text = TextUtils.ToString(TextUtils.ExcuteScalar($"Select TOP 1 AssetType from dbo.TSAsset Where ID = '{asset.TSAssetID}'"));
                txtQuyCach.Text = asset.SpecificationsAsset;
                txtSeri.Text = asset.Seri;
                dtpDateBuy.Value = asset.DateBuy.HasValue == true ? asset.DateBuy.Value : DateTime.Now;
                txtStatus.Text = asset.Status;
                LoadAllocationAsset();
            }
            catch (Exception ex )
            {

                MessageBox.Show("Error: " + ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        void LoadAllocationAsset()
        {
            if (eviction.ID > 0)
            {
                dtpNgayThuHoi.Value = eviction.DateAllocation.HasValue == true ? eviction.DateAllocation.Value : DateTime.Now;
                txtLyDo.Text = eviction.Note;
            }
        }
        void LoadcboNV()
        {
            DataTable NV = TextUtils.Select("Select ID, Code, FullName, ChuVuID, DepartmentID from dbo.Employee");

            //Load cboNguoiBiThuHoi
            cboNguoiBiThuHoi.Properties.DataSource = NV;
            cboNguoiBiThuHoi.Properties.DisplayMember = "FullName";
            cboNguoiBiThuHoi.Properties.ValueMember = "ID";

            //Load cboNguoiThuHoi
            cboNguoiThuHoi.Properties.DataSource = NV;
            cboNguoiThuHoi.Properties.DisplayMember = "FullName";
            cboNguoiThuHoi.Properties.ValueMember = "ID";
        }
        void LoadcboViTri()
        {
            DataTable ViTri = TextUtils.Select("Select ID, Code, Name from dbo.EmployeeChucVu");

            //Load cboViTriTH
            cboViTriTH.Properties.DataSource = ViTri;
            cboViTriTH.Properties.DisplayMember = "Name";
            cboViTriTH.Properties.ValueMember = "ID";

            //LoadcboViTriNTH
            cboViTriCuaNTH.Properties.DataSource = ViTri;
            cboViTriCuaNTH.Properties.DisplayMember = "Name";
            cboViTriCuaNTH.Properties.ValueMember = "ID";
        }
        void LoadcboPhongCT()
        {
            DataTable PhongCT = TextUtils.Select("Select ID, Code, Name from dbo.Department");

            //Load cboPhongCTTH
            cboPhongCTTH.Properties.DataSource = PhongCT;
            cboPhongCTTH.Properties.DisplayMember = "Name";
            cboPhongCTTH.Properties.ValueMember = "ID";

            //Load cboPhongBanCuaNTH
            cboPhongBanCuaNTH.Properties.DataSource = PhongCT;
            cboPhongBanCuaNTH.Properties.DisplayMember = "Name";
            cboPhongBanCuaNTH.Properties.ValueMember = "ID";
        }
        #endregion
        public bool ValidateForm()
        {
            if (cboNguoiThuHoi.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào tên người thu hồi sản phẩm !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboViTriCuaNTH.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào vị trí của người thu hồi sản phẩm !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboPhongBanCuaNTH.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào phòng ban của người thu hồi sản phẩm !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;

            eviction.AssetManagementID = asset.ID;
            eviction.EmployeeID = TextUtils.ToInt(cboNguoiThuHoi.EditValue);
            eviction.ChucVuID = TextUtils.ToInt(cboViTriCuaNTH.EditValue);
            eviction.DepartmentID = TextUtils.ToInt(cboPhongBanCuaNTH.EditValue);
            eviction.DateAllocation = new DateTime(dtpNgayThuHoi.Value.Year, dtpNgayThuHoi.Value.Month, dtpNgayThuHoi.Value.Day);
            eviction.Note = txtLyDo.Text.Trim();
            eviction.Status = "Đã thu hồi";

            if (eviction.ID > 0)
            {
                TSAllocationEvictionAssetBO.Instance.Update(eviction);
            }
            else
            {
                eviction.ID = (int)TSAllocationEvictionAssetBO.Instance.Insert(eviction);
            } 
            asset.EmployeeID = TextUtils.ToInt(cboNguoiThuHoi.EditValue);
            asset.DepartmentID = TextUtils.ToInt(cboPhongBanCuaNTH.EditValue);
            asset.Note = txtLyDo.Text.Trim();
            if(asset.StatusID == 7 )
            {

            }    
            else
            {
                asset.Status = "Chưa sử dụng";
                asset.StatusID = 1;
            }    
            if (asset.ID > 0)
            {
                TSAssetManagementBO.Instance.Update(asset);
            }
            return true;
        }
        #region Button
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void cboNguoiThuHoi_EditValueChanged(object sender, EventArgs e)
        {
            cboPhongBanCuaNTH.EditValue = TextUtils.ToInt(grdData.GetFocusedRowCellValue(colDepartmentID));
            cboViTriCuaNTH.EditValue = TextUtils.ToInt(grdData.GetFocusedRowCellValue(colChuVuID));
        }

        private void cboNguoiBiThuHoi_EditValueChanged(object sender, EventArgs e)
        {
            cboPhongCTTH.EditValue = TextUtils.ToInt(grdData.GetFocusedRowCellValue(colDepartmentID));
            cboViTriTH.EditValue = TextUtils.ToInt(grdData.GetFocusedRowCellValue(colChuVuID));
        }
        #endregion
    }
}
