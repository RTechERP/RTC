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
    public partial class frmReuseAsset : _Forms
    {
        public TSAssetManagementModel asset = new TSAssetManagementModel();
        public TSRepairAssetModel repairr = new TSRepairAssetModel();
        public TSAllocationEvictionAssetModel ast = new TSAllocationEvictionAssetModel();
        public frmReuseAsset()
        {
            InitializeComponent();
        }

        private void frmReuseAsset_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable loaits = TextUtils.Select($"Select TOP 1 AssetType from dbo.TSAsset Where ID = '{asset.TSAssetID}'");
                DataTable donvi = TextUtils.Select($"Select TOP 1 Name from dbo.TSRepairAsset Where AssetManagementID = '{asset.ID}' order by ID DESC");
                if (asset.StatusID != 3)
                {
                    grpThongTinSua.Visible = false;
                    txtDonViSua.Text = " ";
                    dtpRepairDateEnd.Value = asset.DateBuy.HasValue == true ? asset.DateBuy.Value : new DateTime(0, 0, 0, 0, 0, 0);
                    txtChiPhiThucTe.Text = " 0 ";
                    txtNoiDungSua.Text = " ";
                }
                else
                {
                    if (donvi.Rows.Count == 0)
                    {
                        txtDonViSua.Text = "";
                    }
                    else
                    {
                        txtDonViSua.Text = TextUtils.ToString(donvi.Rows[0]["Name"]);
                    }
                }
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
            if(repairr.ID > 0)
            {
                dtpSuDungLai.Value = repairr.DateReuse.HasValue == true ? repairr.DateReuse.Value : DateTime.Now;
                txtDonViSua.Text = repairr.Name;
                dtpRepairDateEnd.Value = repairr.DateEndRepair.HasValue == true ? repairr.DateEndRepair.Value : DateTime.Now;
                txtChiPhiThucTe.Text = repairr.ActualCosts.ToString();
                txtNoiDungSua.Text = repairr.ContentRepair;
            }    
        }
        public bool ValidateForm()
        {
            if (txtLydo.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào lý do sử dụng lại !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (asset.StatusID == 4 || asset.StatusID == 6)
            {
            }
            else
            {
                if (txtChiPhiThucTe.Text == "")
                {
                    MessageBox.Show(string.Format("Vui lòng nhập vào chi phí thực tế để sửa chữa !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (txtNoiDungSua.Text == "")
                {
                    MessageBox.Show(string.Format("Vui lòng nhập vào nội dung sửa chữa !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;

            DataTable dt = TextUtils.Select($"Select TOP 1 ID from dbo.TSRepairAsset Where AssetManagementID = '{asset.ID}' order by ID DESC");
            if(dt.Rows.Count == 0)
            {
            
            } 
            else
            {
                int ID = TextUtils.ToInt(dt.Rows[0]["ID"]);
                //repairr = (TSRepairAssetModel)TSRepairAssetBO.Instance.FindByPK(ID);
                repairr = SQLHelper< TSRepairAssetModel>. FindByID(ID);
                if(repairr == null)
                {
                }
                else
                {
                    repairr.DateReuse = new DateTime(dtpSuDungLai.Value.Year, dtpSuDungLai.Value.Month, dtpSuDungLai.Value.Day);
                    repairr.Name = txtDonViSua.Text.Trim();
                    repairr.DateEndRepair = new DateTime(dtpRepairDateEnd.Value.Year, dtpRepairDateEnd.Value.Month, dtpRepairDateEnd.Value.Day);
                    repairr.ActualCosts = TextUtils.ToDecimal(txtChiPhiThucTe.Text.Trim());
                    repairr.ContentRepair = txtNoiDungSua.Text.Trim();

                    if (repairr.ID > 0)
                    {
                        //TSRepairAssetBO.Instance.Update(repairr);
                        SQLHelper<TSRepairAssetModel>.Update(repairr);
                    }
                }
            }

            ast.AssetManagementID = asset.ID;
            ast.EmployeeID = TextUtils.ToInt(asset.EmployeeID) ;
            ast.DepartmentID = TextUtils.ToInt(asset.DepartmentID);
            DataTable dtchucvu = TextUtils.Select($"Select ChuVuID From dbo.Employee where ID = '{asset.EmployeeID}' ORDER BY ID DESC");
            if (dtchucvu.Rows.Count == 0)
            {

            }
            else
            {
                ast.ChucVuID = TextUtils.ToInt(dtchucvu.Rows[0]["ChuVuID"]);
            }
            ast.DateAllocation = new DateTime(dtpSuDungLai.Value.Year, dtpSuDungLai.Value.Month, dtpSuDungLai.Value.Day);
            ast.Note = txtLydo.Text.Trim();
            ast.Status = "Đang sử dụng";

            //ast.ID = (int)TSAllocationEvictionAssetBO.Instance.Insert(ast);
            ast.ID = SQLHelper<TSAllocationEvictionAssetModel>.Insert(ast).ID;

            asset.Note = txtLydo.Text.Trim();
            asset.Status = "Đang sử dụng ";
            asset.StatusID = 2;
            if (asset.ID > 0)
            {
                //TSAssetManagementBO.Instance.Update(asset);
                SQLHelper<TSAssetManagementModel>.Update(asset);
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
