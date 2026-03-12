using BMS.Business;
using BMS.Model;
using BMS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmTaxDepartmentDetail : _Forms
    {
        public TaxDepartmentModel taxModel = new TaxDepartmentModel();
        public frmTaxDepartmentDetail()
        {
            InitializeComponent();
        }
        private void frmDepartmentTaxDetail_Load(object sender, EventArgs e)
        {
            cboStatus.SelectedIndex = -1;
            loadCombo();
            loadData();
        }

        void loadData()
        {
            if (taxModel.ID > 0)
            {
                txtCode.Text = taxModel.Code;
                txtName.Text = taxModel.Name;
                txtHotline.Text = taxModel.Hotline;
                txtEmail.Text = taxModel.Email;
                cboStatus.SelectedIndex = taxModel.Status;
                leHead.EditValue = taxModel.HeadofDepartment;
                txtSTT.Text = TextUtils.ToString(taxModel.STT);
            }
            else
            {
                loadNumberSTT();
            }
        }

        void loadCombo()
        {
            DataTable tblPerson = TextUtils.Select("Select ID, Code, FullName from Employee a with(nolock)");
            leHead.Properties.DataSource = tblPerson;
            leHead.Properties.DisplayMember = "FullName";
            leHead.Properties.ValueMember = "ID";
        }


        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        void loadNumberSTT()
        {
            object maxSTT = TextUtils.ExcuteScalar("SELECT MAX(STT) AS MaxSTT FROM TaxDepartment");
            int stt = maxSTT != DBNull.Value ? TextUtils.ToInt(maxSTT) + 1 : 1;
            txtSTT.Text = TextUtils.ToString(stt);
        }


        bool checkValidate()
        {
            string input = txtSTT.Text;
            int result;

            if (!int.TryParse(input, out result))
            {
                MessageBox.Show("Số thứ tự bạn nhập vào không hợp lệ! Vui lòng kiểm tra lại.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                var checkSTT = SQLHelper<TaxDepartmentModel>.FindByExpression(new Expression("STT", txtSTT.Text.Trim()).And(new Expression("ID", taxModel.ID, "<>")));
                if (checkSTT.Count > 0)
                {
                    loadNumberSTT();
                    MessageBox.Show($"Số thứ tự này đã tồn tại! STT [{input}] đã được đổi thành [{txtSTT.Text.Trim()}]", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtCode.Text.Trim()))
            {
                MessageBox.Show("Xin hãy điền mã của phòng thuế.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                var checkCode = SQLHelper<TaxDepartmentModel>.FindByExpression(new Expression("Code", txtCode.Text.Trim()).And(new Expression("ID", taxModel.ID, "<>")));
                if (checkCode.Count > 0)
                {
                    MessageBox.Show($"Mã phòng [{txtCode.Text.Trim()}] đã tồn tại.\n Vui lòng nhập mã phòng khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }


            if (string.IsNullOrWhiteSpace(txtName.Text.Trim()))
            {
                MessageBox.Show("Xin hãy điền tên của phòng ban.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (Regex.IsMatch(txtEmail.Text.Trim(), @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                MessageBox.Show("Gmail không đúng định dạng! Vui lòng nhập đúng định dạng Gmail.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (cboStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Xin hãy chọn trạng thái của phòng ban.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (leHead.EditValue == null || (int)leHead.EditValue == 0)
            {
                MessageBox.Show("Xin hãy chọn trưởng phòng ban.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        bool save()
        {
            if (!checkValidate())
            {
                return false;
            }
            taxModel.Code = TextUtils.ToString(txtCode.Text);
            taxModel.STT = TextUtils.ToInt(txtSTT.Text);
            taxModel.Name = TextUtils.ToString(txtName.Text);
            taxModel.Status = TextUtils.ToInt(cboStatus.SelectedIndex);
            taxModel.Email = TextUtils.ToString(txtEmail.Text);
            taxModel.Hotline = TextUtils.ToString(txtHotline.Text);
            taxModel.HeadofDepartment = TextUtils.ToInt(leHead.EditValue);

            if (taxModel.ID > 0)
            {
                TaxDepartmentBO.Instance.Update(taxModel);
            }
            else
            {
                TaxDepartmentBO.Instance.Insert(taxModel);
            }

            return true;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            save();
            txtCode.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtHotline.Text = "";
            cboStatus.SelectedIndex = -1;
            leHead.EditValue = 0;
            loadNumberSTT();
            taxModel = new TaxDepartmentModel();
        }

        private void frmTaxDepartmentDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
