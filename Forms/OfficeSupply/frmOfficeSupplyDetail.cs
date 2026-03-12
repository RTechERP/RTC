using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using BMS.Business;

namespace BMS
{
    public partial class frmOfficeSupplyDetail : _Forms
    {
        public OfficeSupplyModel model = new OfficeSupplyModel();
        public frmOfficeSupplyDetail()
        {
            InitializeComponent();
        }
        private string getLatestCode()
        {
            //string newCodeRTC = "VPP" + TextUtils.ToString(TextUtils.ToInt(lastCodeRTC.Rows[0][0]) + 1);
            //DataTable lastCodeRTC = TextUtils.Select("SELECT TOP 1 SUBSTRING([CodeRTC],4,10) FROM [OfficeSupply] ORDER BY ID DESC;");
            //return newCodeRTC;

            var listCode = SQLHelper<OfficeSupplyModel>.FindAll().Select(x => new { STT = TextUtils.ToInt(x.CodeRTC.Replace("VPP", "")) }).ToList();


            //int stt = listCode.Count() + 1;
            int stt = listCode.Count() > 0 ? listCode.Max(x => x.STT) + 1 : 1;
            string code = $"VPP{stt}";
            return code;
        }
        private void frmOfficeSupplyDetail_Load(object sender, EventArgs e)
        {
            cboType.DisplayMember = "Text";
            cboType.ValueMember = "Value";
            cboType.DataSource = new[]
            {
                new {Value=1,Text="Cá nhân"},
                new {Value=2,Text="Dùng chung"}
            };
            List<OfficeSupplyUnitModel> units = SQLHelper<OfficeSupplyUnitModel>.FindAll();
            cboUnit.Properties.ValueMember = "ID";
            cboUnit.Properties.DisplayMember = "Name";
            cboUnit.Properties.DataSource = units;
            if (model.ID > 0)
            {
                txtCodeRTC.Text = model.CodeRTC;
                txtCodeNCC.Text = model.CodeNCC;
                txtNameRTC.Text = model.NameRTC;
                txtNameNCC.Text = model.NameNCC;
                txtRequestLimit.Text = TextUtils.ToString(model.RequestLimit);
                txtPrice.Text = TextUtils.ToString(model.Price);

                cboType.SelectedValue = model.Type;// == null? 1 : model.Type;
                cboUnit.EditValue = model.SupplyUnitID;
            }
            else
            {
                txtCodeRTC.Text = getLatestCode();
            }
        }
        #region Validation
        private bool ValidateInputs()
        {
            return !(txtCodeNCC.Text == "" || txtNameNCC.Text == "" ||
                TextUtils.ToInt(cboUnit.EditValue) == 0 ||
                txtPrice.Text == "" || txtRequestLimit.Text == "");
        }
        private void numberBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) e.Handled = true;
        }
        #endregion
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                model.CodeNCC = txtCodeNCC.Text;
                model.NameRTC = txtNameRTC.Text == "" ? txtNameNCC.Text : txtNameRTC.Text;
                model.NameNCC = txtNameNCC.Text;
                model.RequestLimit = TextUtils.ToInt(txtRequestLimit.Text);
                model.Price = TextUtils.ToInt(txtPrice.Text);
                model.Type = TextUtils.ToInt(cboType.SelectedValue == null ? 1 : cboType.SelectedValue);
                model.SupplyUnitID = TextUtils.ToInt(cboUnit.EditValue);
                if (model.ID > 0)
                {
                    SQLHelper<OfficeSupplyModel>.Update(model);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    var existingModel = SQLHelper<OfficeSupplyModel>.FindByAttribute("CodeRTC", txtCodeRTC.Text);
                    if (existingModel.Count == 0)
                    {
                        model.CodeRTC = txtCodeRTC.Text;
                        SQLHelper<OfficeSupplyModel>.Insert(model);
                        this.DialogResult = DialogResult.OK;
                    }
                    else MessageBox.Show("Mã đã tồn tại, vui lòng thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                this.DialogResult = DialogResult.No;
            }
        }
        private void btnReloadCode_Click(object sender, EventArgs e)
        {
            if (model.ID == 0) txtCodeRTC.Text = getLatestCode();
        }
    }
}