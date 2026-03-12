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
    public partial class frmUnitCountDetail : _Forms
    {
        public UnitCountModel oUnitCount = new UnitCountModel();

        public frmUnitCountDetail()
        {
            InitializeComponent();
        }
        /// <summary>
        /// load dữ liệu lên khi load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUnitCountDetail_Load(object sender, EventArgs e)
        {
            loadUnitCountDetail();
        }

        private void loadUnitCountDetail()
        {
            txtUnitCode.Text = oUnitCount.UnitCode;
            txtUnitName.Text = oUnitCount.UnitName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }

        }
        bool SaveData()
        {
            if (!ValidateForm()) return false;
            try
            {
                oUnitCount.UnitName = txtUnitName.Text.Trim();
                oUnitCount.UnitCode = txtUnitCode.Text.Trim();

                if (oUnitCount.ID > 0)
                {
                    UnitCountBO.Instance.Update(oUnitCount);
                }
                else
                {
                    UnitCountBO.Instance.Insert(oUnitCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi Update vị trí\n{ex.ToString()}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            SaveData();
            txtUnitCode.Clear();
            txtUnitName.Clear();
            oUnitCount = new UnitCountModel();
        }

        private void frmUnitCountDetail_FormClosing(object sender, FormClosingEventArgs e)
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
            if (txtUnitName.Text == "" || txtUnitCode.Text == "")
            {
                MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            var exp2 = new Expression("ID", oUnitCount.ID, "<>");
            var exp3 = new Expression("IsDeleted", 0);


            var units = SQLHelper<UnitCountModel>.FindByExpression(exp2.And(exp3));
            if (units.Any(x => x.UnitCode.Trim().ToLower() == txtUnitCode.Text.Trim().ToLower()))
            {
                MessageBox.Show($"Mã đơn vị [{txtUnitCode.Text}] đã tồn tại!", TextUtils.Caption);
                return false;
            }

            if (units.Any(x => x.UnitName.Trim().ToLower() == txtUnitName.Text.Trim().ToLower()))
            {
                MessageBox.Show($"Tên đơn vị [{txtUnitName.Text}] đã tồn tại!", TextUtils.Caption);
                return false;
            }



            //DataTable dt;
            //if (oUnitCount.ID > 0)
            //{
            //	dt = TextUtils.Select("select top 1 UnitCode from UnitCount where UnitCode = '" + txtUnitCode.Text.Trim() + "' and ID <> " + oUnitCount.ID);
            //}
            //else
            //{
            //	dt = TextUtils.Select("select top 1 UnitCode from UnitCount where UnitCode = '" + txtUnitCode.Text.Trim() + "'");

            //}
            //if (dt != null)
            //{
            //	if (dt.Rows.Count > 0)
            //	{
            //		MessageBox.Show("Mã đã tồn tại, vui lòng kiểm tra lại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //		return false;
            //	}
            //}
            return true;
        }
    }
}
