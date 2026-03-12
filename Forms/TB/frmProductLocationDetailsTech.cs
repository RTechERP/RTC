using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;

namespace BMS
{
    public partial class frmProductLocationDetailTech : _Forms
    {
        public ProductLocationModel location = new ProductLocationModel();
        public int warehouseID;
        public frmProductLocationDetailTech(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }

        private void frmProductLocationDetails_Load(object sender, EventArgs e)
        {
            if(location.ID > 0)
            {
                txtLocationCode.Text = location.LocationCode;
                txtlocationName.Text = location.LocationName;
                txtSTT.Value = TextUtils.ToInt(location.STT);
                cboLocationType.SelectedIndex = TextUtils.ToInt(location.LocationType);
            }
            else
            {
                var listLocations = SQLHelper<ProductLocationModel>.FindByAttribute(ProductLocationModel_Enum.WarehouseID.ToString(), warehouseID);
                txtSTT.Value = TextUtils.ToInt(listLocations.Max(x => x.STT)) + 1;
            }


            label4.Visible = warehouseID == 5;
            cboLocationType.Visible = warehouseID == 5;
        }
        bool ValidateForm()
        {
            string code = txtLocationCode.Text.Trim();
            if (txtLocationCode.Text.Trim() == "" || txtlocationName.Text.Trim() == "" )
            {
                MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            var exp1 = new Expression(ProductLocationModel_Enum.LocationType, cboLocationType.SelectedIndex);
            var exp2 = new Expression(ProductLocationModel_Enum.WarehouseID, warehouseID);
            var exp3 = new Expression(ProductLocationModel_Enum.ID, location.ID,"<>");
            var exp4 = new Expression(ProductLocationModel_Enum.LocationCode, code);
            var listLocation = SQLHelper<ProductLocationModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp3).And(exp4));

            if (listLocation.Count > 0)
            {
                MessageBox.Show($"Mã [{txtLocationCode.Text.Trim()}] đã tồn tại, vui lòng kiểm tra lại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            //DataTable dt;
            //if (location.ID > 0)
            //{
            //    dt = TextUtils.Select("select top 1 ID from ProductLocation where LocationCode = '" + txtLocationCode.Text.Trim() + "' and ID <> " + location.ID + $" AND WarehouseID = {warehouseID}");
            //}
            //else
            //{
            //    dt = TextUtils.Select("select top 1 ID from ProductLocation where LocationCode = '" + txtLocationCode.Text.Trim() + "'" + $" AND WarehouseID = {warehouseID}");
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
        bool save()
        {
            if (!ValidateForm()) return false;

            location.LocationCode = txtLocationCode.Text.Trim();
            location.LocationName = txtlocationName.Text.Trim();
            location.WarehouseID = warehouseID;
            location.STT = TextUtils.ToInt(txtSTT.Value);

            location.LocationType = cboLocationType.SelectedIndex;
            if(location.ID > 0)
            {
                //ProductLocationBO.Instance.Update(location);
                SQLHelper<ProductLocationModel>.Update(location);
            }
            else
            {
                //ProductLocationBO.Instance.Insert(location);
                SQLHelper<ProductLocationModel>.Insert(location);
            }

            return true;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!save()) return;  
            this.DialogResult = DialogResult.OK;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!save()) return;


            txtLocationCode.Clear();
            txtlocationName.Clear();

            var listLocations = SQLHelper<ProductLocationModel>.FindByAttribute(ProductLocationModel_Enum.WarehouseID.ToString(), warehouseID);
            txtSTT.Value = TextUtils.ToInt(listLocations.Max(x => x.STT)) + 1;
        }

        private void frmProductLocationDetailTech_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
