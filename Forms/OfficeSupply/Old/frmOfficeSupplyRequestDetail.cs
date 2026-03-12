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
namespace BMS
{
    public partial class frmOfficeSupplyRequestDetail : _Forms
    {
        public OfficeSupplyRequestModel model = new OfficeSupplyRequestModel();
        public frmOfficeSupplyRequestDetail()
        {
            InitializeComponent();
        }
        #region Validate
        private bool ValidateInputs()
        {
            if (TextUtils.ToInt(cboUserName.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Người đăng ký!", "Thông báo");
                return false;
            }

            if (TextUtils.ToInt(cboOfficeSupply.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Văn phòng phẩm!", "Thông báo");
                return false;
            }

            if (TextUtils.ToInt(txtQuantity.Text) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Số lượng!", "Thông báo");
                return false;
            }
            //return !(cboUserName.EditValue == null || cboOfficeSupply.EditValue == null || txtQuantity.Text == "");

            return true;
        }
        #endregion
        #region Load Data
        private void frmOfficeSupplyRequestDetail_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime startOfPreviousMonth = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
            dtpDateRequest.MinDate = startOfPreviousMonth;

            chkExceedsLimit.Checked = false;

            LoadEmployee();
            LoadOfficeSupply();
            if (model.ID > 0)
            {
                cboUserName.EditValue = model.UserID;
                cboOfficeSupply.EditValue = model.OfficeSupplyID;
                txtQuantity.Text = TextUtils.ToString(model.Quantity);
                txtQuantityReceive.Text = TextUtils.ToString(model.QuantityReceived);
                txtNote.Text = model.Note;
                dtpDateRequest.Value = model.DateRequest.HasValue ? model.DateRequest.Value : DateTime.Now;
                chkExceedsLimit.Checked = model.ExceedsLimit;
                txtReason.Text = model.Reason;
                btnSaveAndReset.Visible = false;
            }
        }


        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboUserName.Properties.ValueMember = "UserID";
            cboUserName.Properties.DisplayMember = "FullName";
            cboUserName.Properties.DataSource = dt;
        }


        void LoadOfficeSupply()
        {
            List<OfficeSupplyModel> supply = SQLHelper<OfficeSupplyModel>.FindAll();
            cboOfficeSupply.Properties.ValueMember = "ID";
            cboOfficeSupply.Properties.DisplayMember = "NameNCC";
            cboOfficeSupply.Properties.DataSource = supply;
        }

        #endregion
        #region Button Click/ Value Change
        private void numberBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) e.Handled = true;
        }
        private bool save()
        {
            if (!ValidateInputs()) return false;
            //{
            //    MessageBox.Show("Vui lòng điền đầy đủ thông tin", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            model.UserID = TextUtils.ToInt(cboUserName.EditValue);
            model.OfficeSupplyID = TextUtils.ToInt(cboOfficeSupply.EditValue);
            model.Quantity = TextUtils.ToInt(txtQuantity.Text);
            model.QuantityReceived = TextUtils.ToInt(txtQuantityReceive.Text);
            model.Note = txtNote.Text;
            model.DateRequest = dtpDateRequest.Value;
            model.ExceedsLimit = chkExceedsLimit.Checked;
            model.Reason = txtReason.Text;

            DateTime currentRequest = (DateTime)model.DateRequest;
            OfficeSupplyRequestModel latestRequest = SQLHelper<OfficeSupplyRequestModel>.SqlToModel($"SELECT MAX(DateRequest) as DateRequest FROM OfficeSupplyRequest Where UserID = {model.UserID} and OfficeSupplyID = {model.OfficeSupplyID}");
            if ((latestRequest != null && latestRequest.DateRequest.HasValue) && model.ID == 0)
            {
                DateTime latestRequestDate = (DateTime)latestRequest.DateRequest;
                OfficeSupplyModel os = SQLHelper<OfficeSupplyModel>.FindByAttribute("ID", model.OfficeSupplyID)[0];
                int monthLimit = (int)os.RequestLimit / 30;
                int osType = (int)os.Type; //1 = cá nhân, 2 = dùng chung
                int monthsBetweenDates = Math
                    .Abs((currentRequest.Month - latestRequestDate.Month) + 12 * (currentRequest.Year - latestRequestDate.Year));
                if ((osType == 1 && monthsBetweenDates < monthLimit && model.ExceedsLimit == false) ||
                    (osType == 2 && monthsBetweenDates == 0 && model.ExceedsLimit == false))
                {
                    if (MessageBox.Show("Bạn có muốn đăng ký vượt định mức?", "Đã vượt quá định mức", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        chkExceedsLimit.Checked = true;
                        model.ExceedsLimit = true;
                    }
                    return false;
                }
            }
            if (model.ID > 0)
                SQLHelper<OfficeSupplyRequestModel>.Update(model);
            else
                SQLHelper<OfficeSupplyRequestModel>.Insert(model);
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
                this.DialogResult = DialogResult.OK;
        }
        private void btnSaveAndReset_Click(object sender, EventArgs e)
        {
            if (save())
            {
                cboUserName.EditValue = 0;
                cboOfficeSupply.EditValue = 0;
                txtQuantity.Text = "";
                txtQuantityReceive.Text = "";
                txtNote.Text = "";
                dtpDateRequest.Value = DateTime.Now;
                chkExceedsLimit.Checked = false;
                txtReason.Text = "";
            }
        }
        private void chkExceedsLimit_CheckedChanged(object sender, EventArgs e)
        {
            txtReason.ReadOnly = !chkExceedsLimit.Checked;
            if (!chkExceedsLimit.Checked) txtReason.Text = "";
        }

        #endregion
    }
}