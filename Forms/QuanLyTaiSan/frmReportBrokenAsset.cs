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

    public partial class frmReportBrokenAsset : _Forms
    {
        public TSReportBrokenAssetModel broken = new TSReportBrokenAssetModel();
        public TSAssetManagementModel asset = new TSAssetManagementModel();
        public TSAllocationEvictionAssetModel ast = new TSAllocationEvictionAssetModel();
        public frmReportBrokenAsset()
        {
            InitializeComponent();
        }

        private void frmReportBrokenAsset_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable loaits = TextUtils.Select($"Select TOP 1 AssetType from dbo.TSAsset Where ID = '{asset.TSAssetID}'");
                DataTable NV = TextUtils.LoadDataFromSP("spLoadTSLostBrokenReportAS", "A", new string[] { "@ID" }, new object[] { asset.ID });

                if (loaits.Rows.Count > 0)
                {
                    txtLoaiTS.Text = TextUtils.ToString(loaits.Rows[0]["AssetType"]);
                }

                if (NV.Rows.Count == 0)
                {
                    txtnv.EditValue = "";
                    txtViTri.EditValue = "";
                    txtPhongBan.EditValue = "";
                }
                else
                {
                    string status = TextUtils.ToString(NV.Rows[0]["Status"]);
                    if (status == "Đã thu hồi")
                    {
                        txtnv.EditValue = "";
                        txtViTri.EditValue = "";
                        txtPhongBan.EditValue = "";
                    }
                    else
                    {
                        txtnv.EditValue = TextUtils.ToString(NV.Rows[0]["FullName"]);
                        txtViTri.EditValue = TextUtils.ToString(NV.Rows[0]["CVName"]);
                        txtPhongBan.EditValue = TextUtils.ToString(NV.Rows[0]["dpmName"]);
                    }
                }
                txtMaTS.Text = asset.TSAssetCode;
                txtTenTS.Text = asset.TSAssetName;
                txtMaNCC.Text = asset.TSCodeNCC;
                //txtLoaiTS.Text = TextUtils.ToString(loaits.Rows[0]["AssetType"]);
                txtQuyCach.Text = asset.SpecificationsAsset;
                txtSeri.Text = asset.Seri;
                dtpDateBuy.Value = asset.DateBuy.HasValue == true ? asset.DateBuy.Value : DateTime.Now;
                txtStatus.Text = asset.Status;
                LoadReportBroken();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        void LoadReportBroken()
        {
            dtpHong.Value = broken.DateReportBroken.HasValue == true ? broken.DateReportBroken.Value : DateTime.Now;
            txtLyDo.Text = broken.Reason;
        }
        public bool ValidateForm()
        {
            if (txtLyDo.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào lý do tài sản bị hỏng !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;

            broken.AssetManagementID = asset.ID;
            broken.DateReportBroken = new DateTime(dtpHong.Value.Year, dtpHong.Value.Month, dtpHong.Value.Day);
            broken.Reason = txtLyDo.Text.Trim();

            if (broken.ID > 0)
            {
                TSReportBrokenAssetBO.Instance.Update(broken);
            }
            else
            {
                broken.ID = (int)TSReportBrokenAssetBO.Instance.Insert(broken);
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
            ast.DateAllocation = new DateTime(dtpHong.Value.Year, dtpHong.Value.Month, dtpHong.Value.Day);
            ast.Note = txtLyDo.Text.Trim();
            ast.Status = "Hỏng";

            ast.ID = (int)TSAllocationEvictionAssetBO.Instance.Insert(ast);

            asset.Note = txtLyDo.Text.Trim();
            asset.Status = "Hỏng";
            asset.StatusID = 5;
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
