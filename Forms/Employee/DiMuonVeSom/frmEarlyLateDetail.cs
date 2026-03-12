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
    public partial class frmEarlyLateDetail : _Forms
    {
        public EmployeeEarlyLateModel earlylate = new EmployeeEarlyLateModel();
        public frmEarlyLateDetail()
        {
            InitializeComponent();
        }

        private void frmEarlyLateDetail_Load(object sender, EventArgs e)
        {
            //cboType.SelectedIndex = 1;
            //LoadcboName();
            //LoadEditData();

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
            if (earlylate.ID > 0)
            {
                cboEmployee.EditValue = earlylate.EmployeeID;
                cboEmployeeApprove.EditValue = earlylate.ApprovedTP;
                dtpDate.Value = earlylate.DateRegister.Value;
                dtpDateStart.Value = earlylate.DateStart.Value;
                dtpDateEnd.Value = earlylate.DateEnd.Value;
                cboType.SelectedIndex = earlylate.Type;
                txtReason.Text = earlylate.Reason;
                txtReasonHREdit.Text = earlylate.ReasonHREdit;
            }
            else
            {
                cboEmployee.Focus();
                cboEmployee.EditValue = 0;
                cboEmployeeApprove.EditValue = 0;
                dtpDate.Value = DateTime.Now;
                cboType.SelectedIndex = 0;
                txtReason.Clear();
                txtReasonHREdit.Clear();
            }

            cboEmployee.Enabled = cboEmployeeApprove.Enabled = !(earlylate.ID > 0);
            lblValidateHrEdit.Visible = (earlylate.ID > 0);
        }

        void LoadEditData()
        {
            if (earlylate.ID > 0)
            {
                cboEmployee.EditValue = TextUtils.ToInt(earlylate.EmployeeID);
                txtReason.Text = earlylate.Reason;
                dtpDate.Value = earlylate.DateRegister.Value;
                cboType.SelectedIndex = earlylate.Type;
            }
        }

        void LoadcboName()
        {
            DataTable dt = TextUtils.Select("Select ID,Code,FullName from dbo.Employee WHERE Status <> 1");
            cboEmployee.Properties.DataSource = dt;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
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

            if (cboType.SelectedIndex == 0)
            {
                MessageBox.Show(string.Format("Vui lòng nhập Loại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtReason.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập Lý do!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (earlylate.ID > 0)
            {
                if (txtReasonHREdit.Text.Trim() == "")
                {
                    MessageBox.Show(string.Format("Vui lòng nhập Lý do sửa!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            var exp1 = new Expression("EmployeeID", TextUtils.ToInt(cboEmployee.EditValue));
            var exp2 = new Expression("DateRegister", dtpDate.Value.ToString("yyyy-MM-dd"));
            var exp3 = new Expression("Type", cboType.SelectedIndex);
            var exp4 = new Expression("ID", earlylate.ID, "<>");
            var list = SQLHelper<EmployeeEarlyLateModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4));
            //int id = TextUtils.ToInt(TextUtils.ExcuteScalar($"EXEC spGetEmployeeEarlyLateByDateAndType '{dtpDate.Value.ToString("yyyy-MM-dd")}', {TextUtils.ToInt(cboEmployee.EditValue)},{cboType.SelectedIndex}"));
            if (list.Count() > 0)
            {
                MessageBox.Show(string.Format($"Nhân viên [{cboEmployee.Text}] đã khai báo [{cboType.Text}]!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;

            }
            return true;
        }

        bool SaveData()
        {
            if (!CheckValidate()) return false;
            earlylate.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            earlylate.ApprovedTP = TextUtils.ToInt(cboEmployeeApprove.EditValue);
            earlylate.DateRegister = dtpDate.Value;
            earlylate.DateStart = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, dtpDateStart.Value.Hour, dtpDateStart.Value.Minute,0);
            earlylate.DateEnd = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, dtpDateEnd.Value.Hour, dtpDateEnd.Value.Minute, 0); ;
            earlylate.TimeRegister = TextUtils.ToDecimal((earlylate.DateEnd.Value - earlylate.DateStart.Value).TotalHours);
            earlylate.Type = cboType.SelectedIndex;
            earlylate.Reason = txtReason.Text.Trim();
            earlylate.ReasonHREdit = txtReasonHREdit.Text.Trim();

            if (earlylate.ID > 0)
            {
                earlylate.IsApproved = earlylate.IsApprovedTP = false;
                EmployeeEarlyLateBO.Instance.Update(earlylate);
            }
            else
            {
                earlylate.ID = (int)EmployeeEarlyLateBO.Instance.Insert(earlylate);
            }

            return true;
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
                earlylate = new EmployeeEarlyLateModel();
                LoadData();
            }
        }


        void UpdateEmployeeSendEmail(int employeeID, int employeeApproveID, string employeeName, string approverName)
        {
            EmployeeSendEmailModel email = new EmployeeSendEmailModel();
            email.Subject = "CẬP NHẬT THÔNG TIN ĐI MUỘN - VỀ SỚM";
            EmployeeModel employee = SQLHelper<EmployeeModel>.FindByID(employeeID);
            EmployeeModel employeeApp = SQLHelper<EmployeeModel>.FindByID(employeeApproveID);

            if (employeeApp == null)
            {
                MessageBox.Show($"Không tồn tại nhân viên [{approverName}]!", "Thông báo");
                return;
            }
            else if (!string.IsNullOrEmpty(employeeApp.EmailCongTy.Trim()))
            {
                email.EmailTo = employeeApp.EmailCongTy.Trim();
            }
            else if (!string.IsNullOrEmpty(employeeApp.EmailCaNhan.Trim()))
            {
                email.EmailTo = employeeApp.EmailCaNhan.Trim();
            }
            else
            {
                MessageBox.Show($"Nhân viên [{approverName}] chưa có email.\n Không thể gửi thông báo sửa này cho TBP!", "Thông báo");
                return;
            }

            email.EmailCC = "";
            if (employee != null)
            {
                if (!string.IsNullOrEmpty(employee.EmailCongTy.Trim()))
                {
                    email.EmailCC = employeeApp.EmailCongTy.Trim();
                }
                else if (!string.IsNullOrEmpty(employee.EmailCaNhan.Trim()))
                {
                    email.EmailCC = employeeApp.EmailCaNhan.Trim();
                }
            }

            email.Body = "";
        }
    }
}
