using BMS.Business;
using BMS.Model;
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
    public partial class frmFirmDetail : _Forms
    {
        public FirmModel oFirmModel = new FirmModel();

        public frmFirmDetail()
        {
            InitializeComponent();
        }
        /// <summary>
        /// load dữ liệu lên khi load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFirmDetail_Load(object sender, EventArgs e)
        {
            loadFirmDetail();
        }
        private void loadFirmDetail()
        {
            txtFirmCode.Text = oFirmModel.FirmCode;
            txtFirmName.Text = oFirmModel.FirmName;

            if (oFirmModel.ID > 0)
            {
                cboFirmType.SelectedIndex = TextUtils.ToInt(oFirmModel.FirmType);
            }
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
                oFirmModel.FirmName = txtFirmName.Text.Trim().ToUpper();
                oFirmModel.FirmCode = txtFirmCode.Text.Trim();
                oFirmModel.FirmType = cboFirmType.SelectedIndex;

                if (oFirmModel.ID > 0)
                {
                    //FirmBO.Instance.Update(oFirmModel);
                    SQLHelper<FirmModel>.Update(oFirmModel);
                }
                else
                {
                    //FirmBO.Instance.Insert(oFirmModel);
                    SQLHelper<FirmModel>.Insert(oFirmModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}r\n${ex.ToString()}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            SaveData();
            txtFirmCode.Clear();
            txtFirmName.Clear();
            oFirmModel = new FirmModel();
        }

        private void frmFirmDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// check lỗi
        /// </summary>
        /// <returns></returns>
        /// 
        bool ValidateForm()
        {
            if (txtFirmName.Text == "" || txtFirmCode.Text == "")
            {
                MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            DataTable dt;
            if (oFirmModel.ID > 0)
            {
                dt = TextUtils.Select("select top 1 FirmCode from Firm where FirmCode = '" + txtFirmCode.Text.Trim() + "' and ID <> " + oFirmModel.ID);
            }
            else
            {
                dt = TextUtils.Select("select top 1 FirmCode from Firm where FirmCode = '" + txtFirmCode.Text.Trim() + "'");

            }
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Mã đã tồn tại, vui lòng kiểm tra lại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (cboFirmType.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn Loại!", "Thông báo");
                return false;
            }
            return true;
        }
    }
}
