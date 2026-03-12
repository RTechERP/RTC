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
    public partial class frmLiquidation : _Forms
    {
        public TSAssetManagementModel asset = new TSAssetManagementModel();
        public TSLiQuidationAssetModel liquidation = new TSLiQuidationAssetModel();
        public TSAllocationEvictionAssetModel ast = new TSAllocationEvictionAssetModel();
        public frmLiquidation()
        {
            InitializeComponent();
        }

        #region Load Data
        private void frmLiquidation_Load(object sender, EventArgs e)
        {
            DataTable loaits = TextUtils.Select($"Select TOP 1 AssetType from dbo.TSAsset Where ID = '{asset.TSAssetID}'");
            DataTable NV = TextUtils.LoadDataFromSP("spLoadTSLostBrokenReportAS", "A", new string[] { "@ID" }, new object[] { asset.ID });
            if (NV.Rows.Count == 0)
            {
                txtnv.Text = " ";
                txtViTri.Text = " ";
                txtPhongBan.Text = " ";
            }
            else
            {
                string status = TextUtils.ToString(NV.Rows[0]["Status"]);
                if(status != "Đang sử dụng")
                {
                    txtnv.Text = " ";
                    txtViTri.Text = " ";
                    txtPhongBan.Text = " ";
                }   
                else
                {
                    txtnv.Text = TextUtils.ToString(NV.Rows[0]["FullName"]);
                    txtViTri.Text = TextUtils.ToString(NV.Rows[0]["CVName"]);
                    txtPhongBan.Text = TextUtils.ToString(NV.Rows[0]["dpmName"]);
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
            LoadcboNguoiQuyetDinh();
        }
        void LoadcboNguoiQuyetDinh()
        {
            DataTable dt = TextUtils.Select("Select ID, FullName from dbo.Employee");
            cboNguoiQuyetDinh.Properties.DataSource = dt;
            cboNguoiQuyetDinh.Properties.DisplayMember = "FullName";
            cboNguoiQuyetDinh.Properties.ValueMember = "ID";
        }
        #endregion
        public bool ValidateForm()
        {
            if (txtLyDoThanhLy.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập lý do thanh lý tài sản !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboNguoiQuyetDinh.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập người quyết định thanh lý tài sản !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;

            DataTable dtLiquidation = TextUtils.Select($"Select TOP 1 ID from dbo.TSLiQuidationAsset Where AssetManagementID = '{asset.ID}' order by ID DESC");
            if(dtLiquidation.Rows.Count == 0)
            {

            }
            else
            {
                int IDliquidation = TextUtils.ToInt(dtLiquidation.Rows[0]["ID"]);
                liquidation = (TSLiQuidationAssetModel)TSLiQuidationAssetBO.Instance.FindByPK(IDliquidation);
                if(liquidation == null)
                {

                }    
                else
                {
                    liquidation.IsApproved = true;
                    liquidation.EmployeeID = TextUtils.ToInt(cboNguoiQuyetDinh.EditValue);
                    liquidation.DateLiquidation = new DateTime(dtpNgayThanhLy.Value.Year, dtpNgayThanhLy.Value.Month, dtpNgayThanhLy.Value.Day);
                    liquidation.Reason = txtLyDoThanhLy.Text.Trim();
                    if (liquidation.ID > 0)
                    {
                        TSLiQuidationAssetBO.Instance.Update(liquidation);
                    }
                    else
                    {
                        liquidation.ID = (int)TSLiQuidationAssetBO.Instance.Insert(liquidation);
                    }
                }     
            }

            ast.AssetManagementID = asset.ID;
            ast.EmployeeID = liquidation.EmployeeID;
            DataTable dtphongban = TextUtils.Select($"Select DepartmentID From dbo.Employee where ID = '{liquidation.EmployeeID}' ORDER BY ID DESC");
            if (dtphongban.Rows.Count == 0)
            {

            }
            else
            {
                ast.DepartmentID = TextUtils.ToInt(dtphongban.Rows[0]["DepartmentID"]);
            }
            DataTable dtchucvu = TextUtils.Select($"Select ChuVuID From dbo.Employee where ID = '{liquidation.EmployeeID}' ORDER BY ID DESC");
            if (dtchucvu.Rows.Count == 0)
            {

            }
            else
            {
                ast.ChucVuID = TextUtils.ToInt(dtchucvu.Rows[0]["ChuVuID"]);
            }
            ast.DateAllocation = new DateTime(dtpNgayThanhLy.Value.Year, dtpNgayThanhLy.Value.Month, dtpNgayThanhLy.Value.Day);
            ast.Note = txtLyDoThanhLy.Text.Trim();
            ast.Status = "Đã thanh lý";

            ast.ID = (int)TSAllocationEvictionAssetBO.Instance.Insert(ast);

            asset.EmployeeID = liquidation.EmployeeID;
            if (dtphongban.Rows.Count == 0)
            {

            }
            else
            {
                ast.ChucVuID = TextUtils.ToInt(dtphongban.Rows[0]["DepartmentID"]);
            }
            asset.Note = txtLyDoThanhLy.Text.Trim();
            asset.Status = "Đã thanh lý ";
            asset.StatusID = 6;
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
