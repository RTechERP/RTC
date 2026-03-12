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
    public partial class frmNoFingerprintDetail : _Forms
    {
        public EmployeeNoFingerprintModel nofinger = new EmployeeNoFingerprintModel();
        public frmNoFingerprintDetail()
        {
            InitializeComponent();
        }

        private void frmNoFingerprintDetail_Load(object sender, EventArgs e)
        {
            //loadEmployee();
            //loadData();
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
            if (nofinger.ID > 0)
            {
                cboEmployee.EditValue = nofinger.EmployeeID;
                cboEmployeeApprove.EditValue = nofinger.ApprovedTP;
                dtpDayWork.Value = nofinger.DayWork.Value;
                rdCheckin.Checked = (nofinger.Type == 1);
                rdCheckout.Checked = (nofinger.Type == 2);
                txtReasonHREdit.Text = nofinger.ReasonHREdit;
            }
            else
            {
                cboEmployee.Focus();
                cboEmployee.EditValue = 0;
                cboEmployeeApprove.EditValue = 0;
                dtpDayWork.Value = DateTime.Now;
                rdCheckin.Checked = rdCheckout.Checked = false;
                txtReasonHREdit.Clear();
            }

            cboEmployee.Enabled = cboEmployeeApprove.Enabled = !(nofinger.ID > 0);
            lblValidateHrEdit.Visible = (nofinger.ID > 0);
        }

        //void loadEmployee()
        //{
        //    DataTable dt = TextUtils.Select("Select ID,Code,FullName from Employee");
        //    cbEmployee.Properties.DataSource = dt;
        //    cbEmployee.Properties.ValueMember = "ID";
        //    cbEmployee.Properties.DisplayMember = "FullName";

        //}
        //void loadData()
        //{
        //    if(nofinger.ID>0)
        //    {
        //        cbEmployee.EditValue = nofinger.EmployeeID;
        //        dtpDayWork.Value = nofinger.DayWork.Value;
        //        txtReasonHREdit.Text = nofinger.Note;
        //    }    
        //}

        bool CheckValidate()
        {
            if (TextUtils.ToInt(cboEmployee.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Họ và tên!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (TextUtils.ToInt(cboEmployeeApprove.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Người duyệt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //if (dtpDayWork.Text=="")
            //{
            //    MessageBox.Show("Ngày không được để trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            var exp1 = new Expression("EmployeeID", TextUtils.ToInt(cboEmployee.EditValue));
            var exp2 = new Expression("DayWork", dtpDayWork.Value.ToString("yyyy-MM-dd"));
            var exp3 = new Expression("Type", rdCheckin.Checked ? 1 : (rdCheckout.Checked ? 2 : 0));
            var exp4 = new Expression("ID", nofinger.ID, "<>");
            var list = SQLHelper<EmployeeNoFingerprintModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4));
            if (list.Count() > 0)
            {
                string typeName = rdCheckin.Checked ? rdCheckin.Text : (rdCheckout.Checked ? rdCheckout.Text : "");
                MessageBox.Show(string.Format($"Nhân viên [{cboEmployee.Text}] đã khai báo quên vân tay [{typeName}]!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nofinger.ID > 0)
            {
                if (string.IsNullOrEmpty(txtReasonHREdit.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Lý do sửa! ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        bool SaveData()
        {
            if (!CheckValidate()) return false;

            nofinger.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            nofinger.ApprovedTP = TextUtils.ToInt(cboEmployeeApprove.EditValue);
            nofinger.DayWork = dtpDayWork.Value.Date;
            nofinger.Type = rdCheckin.Checked ? 1 : (rdCheckout.Checked ? 2 : 0);
            nofinger.ReasonHREdit = txtReasonHREdit.Text.Trim();

            if (nofinger.ID > 0)
            {
                nofinger.IsApprovedHR = nofinger.IsApprovedTP = false;
                EmployeeNoFingerprintBO.Instance.Update(nofinger);
            }
            else
            {
                EmployeeNoFingerprintBO.Instance.Insert(nofinger);
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
                nofinger = new EmployeeNoFingerprintModel();
                LoadData();
            }
        }
    }
}
