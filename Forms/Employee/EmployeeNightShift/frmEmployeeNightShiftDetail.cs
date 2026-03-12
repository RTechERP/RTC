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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmEmployeeNightShiftDetail : _Forms
    {
        public EmployeeNighShiftModel nighShift = new EmployeeNighShiftModel();
        public frmEmployeeNightShiftDetail()
        {
            InitializeComponent();
        }

        private void frmEmployeeNightShiftDetail_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            LoadData();
        }

        void LoadEmployee()
        {
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetEmployeeAndEmployeeApprover", new string[] { }, new object[] { });
            cboEmployee.Properties.DataSource = dataSet.Tables[0];
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";

            cboEmployeeApprove.Properties.DataSource = dataSet.Tables[1];
            cboEmployeeApprove.Properties.DisplayMember = "FullName";
            cboEmployeeApprove.Properties.ValueMember = "EmployeeID";
        }

        void LoadData()
        {
            if (nighShift.ID > 0)
            {
                cboEmployee.EditValue = nighShift.EmployeeID;
                cboEmployeeApprove.EditValue = nighShift.ApprovedTBP;
                dtpDateRegister.Value = nighShift.DateRegister.Value;
                dtpDateStart.Value = nighShift.DateStart.Value;
                dtpDateEnd.Value = nighShift.DateEnd.Value;
                txtLocation.Text = nighShift.Location;
                txtNote.Text = nighShift.Note;
                txtReasonHREdit.Text = nighShift.ReasonHREdit;
            }
            else
            {
                cboEmployee.Focus();
                cboEmployee.EditValue = 0;
                cboEmployeeApprove.EditValue = 0;
                dtpDateRegister.Value = DateTime.Now;
                txtLocation.Clear();
                txtNote.Clear();
                txtReasonHREdit.Clear();
            }

            cboEmployee.Enabled = cboEmployeeApprove.Enabled = !(nighShift.ID > 0);
            lblValidateHrEdit.Visible = (nighShift.ID > 0);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                nighShift = new EmployeeNighShiftModel();
                LoadData();
            }
        }


        bool CheckValidate()
        {
            if (TextUtils.ToInt(cboEmployee.EditValue) <= 0)
            {
                MessageBox.Show(string.Format("Vui lòng nhập Họ tên! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (TextUtils.ToInt(cboEmployeeApprove.EditValue) <= 0)
            {
                MessageBox.Show(string.Format("Vui lòng nhập Người duyệt! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            

            if (txtLocation.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập Lý do!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nighShift.ID > 0)
            {
                if (txtReasonHREdit.Text.Trim() == "")
                {
                    MessageBox.Show(string.Format("Vui lòng nhập Lý do sửa!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            var exp1 = new Expression("EmployeeID", TextUtils.ToInt(cboEmployee.EditValue));
            var exp2 = new Expression("DateRegister", dtpDateRegister.Value.ToString("yyyy-MM-dd"));
            var exp3 = new Expression("ID", nighShift.ID, "<>");
            var list = SQLHelper<EmployeeEarlyLateModel>.FindByExpression(exp1.And(exp2).And(exp3));
            if (list.Count() > 0)
            {
                MessageBox.Show(string.Format($"Nhân viên [{cboEmployee.Text}] đã khai báo làm đêm ngày [{dtpDateRegister.Value.ToString("dd/MM/yyyy")}]!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;

            }
            return true;
        }

        bool SaveData()
        {
            if (!CheckValidate())
            {
                return false;
            }
            nighShift.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            nighShift.ApprovedTBP = TextUtils.ToInt(cboEmployeeApprove.EditValue);
            nighShift.DateRegister = dtpDateRegister.Value.Date;
            nighShift.DateStart = dtpDateStart.Value;
            nighShift.DateEnd = dtpDateEnd.Value;
            nighShift.Location = txtLocation.Text.Trim();
            nighShift.Note = txtNote.Text.Trim();
            nighShift.ReasonHREdit = txtReasonHREdit.Text.Trim();

            nighShift.TotalHours = (decimal)(nighShift.DateEnd.Value - nighShift.DateStart.Value).TotalHours;
            if (nighShift.ID > 0)
            {
                nighShift.IsApprovedHR = nighShift.IsApprovedTBP = 0;
                EmployeeNighShiftBO.Instance.Update(nighShift);
            }
            else
            {
                EmployeeNighShiftBO.Instance.Insert(nighShift);
            }

            return true;
        }
    }
}
