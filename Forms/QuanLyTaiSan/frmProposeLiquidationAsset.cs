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
    public partial class frmProposeLiquidationAsset : _Forms
    {
        public TSAssetManagementModel asset = new TSAssetManagementModel();
        public TSLiQuidationAssetModel liquidation = new TSLiQuidationAssetModel();
        public TSAllocationEvictionAssetModel ast = new TSAllocationEvictionAssetModel();
        public frmProposeLiquidationAsset()
        {
            InitializeComponent();
        }

        private void frmProposeLiquidationAsset_Load(object sender, EventArgs e)
        {
            try
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
                    if (status != "Đang sử dụng")
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
                LoadcboNV();
                LoadData();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        void LoadData()
        {
            dtpDeNghi.Value = liquidation.DateSuggest.HasValue == true ? liquidation.DateSuggest.Value : DateTime.Now;
            txtLyDoDeNghi.Text = liquidation.Reason;
        }
        void LoadcboNV()
        {
            DataTable NV = TextUtils.Select("Select ID, Code, FullName, ChuVuID, DepartmentID from dbo.Employee");
            cboNV.Properties.DataSource = NV;
            cboNV.Properties.DisplayMember = "FullName";
            cboNV.Properties.ValueMember = "ID";
        }
        public bool ValidateForm()
        {
            if(txtLyDoDeNghi.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập lý do đề nghị thanh lý tài sản !"),TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;

            liquidation.AssetManagementID = asset.ID;
            liquidation.DateSuggest = new DateTime(dtpDeNghi.Value.Year, dtpDeNghi.Value.Month, dtpDeNghi.Value.Day);
            liquidation.EmployeeID = TextUtils.ToInt(cboNV.EditValue);
            liquidation.Reason = txtLyDoDeNghi.Text.Trim();

            if (liquidation.ID > 0)
            {
                //TSLiQuidationAssetBO.Instance.Update(liquidation);
                SQLHelper<TSLiQuidationAssetModel>.Update(liquidation);
            }
            else
            {
                //liquidation.ID = (int)TSLiQuidationAssetBO.Instance.Insert(liquidation);
                liquidation.ID = SQLHelper<TSLiQuidationAssetModel>.Insert(liquidation).ID;
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
            ast.DateAllocation = new DateTime(dtpDeNghi.Value.Year, dtpDeNghi.Value.Month, dtpDeNghi.Value.Day);
            ast.Note = txtLyDoDeNghi.Text.Trim();
            ast.Status = "Đề nghị thanh lý";

            //ast.ID = (int)TSAllocationEvictionAssetBO.Instance.Insert(ast);
            ast.ID = SQLHelper<TSAllocationEvictionAssetModel>.Insert(ast).ID;

            asset.EmployeeID = TextUtils.ToInt(cboNV.EditValue);
            if (dtphongban.Rows.Count == 0)
            {

            }
            else
            {
                asset.DepartmentID = TextUtils.ToInt(dtphongban.Rows[0]["DepartmentID"]);
            }
            asset.Note = txtLyDoDeNghi.Text.Trim();
            asset.Status = "Đề nghị thanh lý";
            asset.StatusID = 7;
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
