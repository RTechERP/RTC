using BMS.Business;
using BMS.Model;
using BMS.Utils;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BMS
{
    public partial class frmProductLocationDetail : _Forms
    {
        public LocationModel oLocationModel = new LocationModel();

        public frmProductLocationDetail()
        {
            InitializeComponent();
        }
        /// <summary>
        /// load dữ liệu lên khi load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProductLocationDetail_Load(object sender, EventArgs e)
        {
            LoadGroup();
            loadData();
        }


        private void LoadGroup()
        {
            //DataTable dt = TextUtils.Select("select ProductGroupName,ID from ProductGroup ");
            List<ProductGroupModel> list = SQLHelper<ProductGroupModel>.FindAll();
            cboGroup.Properties.DisplayMember = "ProductGroupName";
            cboGroup.Properties.ValueMember = "ID";
            cboGroup.Properties.DataSource = list;
        }

        private void loadData()
        {
            txtMavitri.Text = oLocationModel.LocationCode;
            txtTenvitri.Text = oLocationModel.LocationName;

            cboGroup.EditValue = oLocationModel.ProductGroupID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
            this.DialogResult = DialogResult.OK;
        }
        bool SaveData()
        {
            if (!ValidateForm()) return false;
            try
            {
                oLocationModel.LocationName = txtTenvitri.Text.Trim();
                oLocationModel.LocationCode = txtMavitri.Text.Trim();
                oLocationModel.ProductGroupID = TextUtils.ToInt(cboGroup.EditValue);

                if (oLocationModel.ID > 0)
                {
                    LocationBO.Instance.Update(oLocationModel);
                }
                else
                {
                    LocationBO.Instance.Insert(oLocationModel);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi Update vị trí", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;

        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            SaveData();
            txtMavitri.Clear();
            txtTenvitri.Clear();
        }

        private void frmProductLocationDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
            this.DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// check lỗi
        /// </summary>
        /// <returns></returns>
        /// 

        bool ValidateForm()
        {
            string code = txtMavitri.Text.Trim();
            //if (txtTenvitri.Text == "" || txtMavitri.Text == "")
            //{
            //    MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            if (TextUtils.ToInt(cboGroup.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Kho!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Vui lòng nhập Mã vị trí!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenvitri.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên vị trí!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            var exp1 = new Expression("LocationCode", code);
            var exp2 = new Expression("ID", oLocationModel.ID,"<>");

            var listLocations = SQLHelper<LocationModel>.FindByExpression(exp1.And(exp2));
            if (listLocations.Count>0)
            {
                MessageBox.Show($"Mã vị trí [{code}] đã tồn tại.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }



            //DataTable dt;
            //if (oLocationModel.ID > 0)
            //{
            //    dt = TextUtils.Select("select top 1 LocationCode from ProductLocation where LocationCode = '" + txtMavitri.Text.Trim() + "' and ID <> " + oLocationModel.ID);
            //}
            //else
            //{
            //    dt = TextUtils.Select("select top 1 LocationCode from ProductLocation where LocationCode = '" + txtMavitri.Text.Trim() + "'");

            //}
            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        MessageBox.Show("Mã đã tồn tại, vui lòng kiểm tra lại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return false;
            //    }
            //}
            return true;
        }
    }
}
