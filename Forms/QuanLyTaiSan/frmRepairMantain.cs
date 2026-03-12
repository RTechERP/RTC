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
    public partial class frmRepairMantain : _Forms
    {
        public TSAssetManagementModel asset = new TSAssetManagementModel();
        public TSRepairAssetModel repair = new TSRepairAssetModel();
        public TSAllocationEvictionAssetModel ast = new TSAllocationEvictionAssetModel();
        public frmRepairMantain()
        {
            InitializeComponent();
        }

        private void frmRepairMantain_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable loaits = TextUtils.Select($"Select TOP 1 AssetType from dbo.TSAsset Where ID = '{asset.TSAssetID}'");
                txtMaTS.Text = asset.TSAssetCode;
                txtTenTS.Text = asset.TSAssetName;
                txtMaNCC.Text = asset.TSCodeNCC;
                txtLoaiTS.Text = loaits.Rows.Count <= 0 ? "" : TextUtils.ToString(loaits.Rows[0]["AssetType"]);
                txtQuyCach.Text = asset.SpecificationsAsset;
                txtSeri.Text = asset.Seri;
                dtpDateBuy.Value = asset.DateBuy.HasValue == true ? asset.DateBuy.Value : DateTime.Now;
                txtStatus.Text = asset.Status;
                LoadData();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        void LoadData()
        {
            if(repair.ID > 0)
            {
                dtpRepairDate.Value = repair.DateRepair.HasValue == true ? repair.DateRepair.Value : DateTime.Now;
                txtDonViSua.Text = repair.Name;
                txtChiPhi.Text = TextUtils.ToString(repair.ExpectedCost);
                txtLyDo.Text = repair.Reason;
            }    
        }
        public bool ValidateForm()
        {
            if(txtDonViSua.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập tên đơn vị sửa chữa !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtLyDo.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập lý do sửa chữa !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;

            repair.AssetManagementID = asset.ID;
            repair.DateRepair = new DateTime(dtpRepairDate.Value.Year, dtpRepairDate.Value.Month, dtpRepairDate.Value.Day);
            repair.Name = txtDonViSua.Text.Trim();
            repair.ExpectedCost = TextUtils.ToDecimal(txtChiPhi.Text.Trim());
            repair.Reason = txtLyDo.Text.Trim();
            if (repair.ID > 0)
            {
                TSRepairAssetBO.Instance.Update(repair);
            }
            else
            {
                repair.ID = (int)TSRepairAssetBO.Instance.Insert(repair);
            }

            ast.AssetManagementID = asset.ID;
            ast.EmployeeID = TextUtils.ToInt(asset.EmployeeID);
            ast.DepartmentID = TextUtils.ToInt(asset.DepartmentID);
            DataTable dtchucvu = TextUtils.Select($"Select ChuVuID From dbo.Employee where ID = '{asset.EmployeeID}' ORDER BY ID DESC");
            if (dtchucvu.Rows.Count == 0)
            {

            }
            else
            {
                ast.ChucVuID = TextUtils.ToInt(dtchucvu.Rows[0]["ChuVuID"]);
            }
            ast.DateAllocation = new DateTime(dtpRepairDate.Value.Year, dtpRepairDate.Value.Month, dtpRepairDate.Value.Day);
            ast.Note = txtLyDo.Text.Trim();
            ast.Status = "Sửa chữa, Bảo dưỡng";

            ast.ID = (int)TSAllocationEvictionAssetBO.Instance.Insert(ast);

            asset.Note = txtLyDo.Text.Trim();
            asset.Status = "Sửa chữa, Bảo dưỡng";
            asset.StatusID = 3;
            if (asset.ID > 0)
            {
                TSAssetManagementBO.Instance.Update(asset);
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
