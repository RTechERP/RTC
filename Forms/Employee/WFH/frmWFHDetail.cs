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
    public partial class frmWFHDetail : _Forms
    {
        public EmployeeWFHModel wfh = new EmployeeWFHModel();
        //DataTable dt = TextUtils.Select($"Select * from Employee WHERE Status <> 1");
        public frmWFHDetail()
        {
            InitializeComponent();
        }

        private void frmWFHDetail_Load(object sender, EventArgs e)
        {
            //loadEmployee();
            //loadApproved();
            //loadDetail();
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
            if (wfh.ID > 0)
            {
                cboEmployee.EditValue = wfh.EmployeeID;
                cboEmployeeApprove.EditValue = wfh.ApprovedID;
                dtpRegisterDate.Value = wfh.DateWFH.Value;
                cboTimeWFH.SelectedIndex = TextUtils.ToInt(wfh.TimeWFH);
                txtReason.Text = wfh.Reason;
                txtReasonHREdit.Text = wfh.ReasonHREdit;
                txtContentWork.Text = wfh.ContentWork;
            }
            else
            {
                cboEmployee.Focus();
                cboEmployee.EditValue = 0;
                cboEmployeeApprove.EditValue = 0;
                dtpRegisterDate.Value = DateTime.Now;
                cboTimeWFH.SelectedIndex = 0;
                txtReason.Clear();
                txtReasonHREdit.Clear();
                txtContentWork.Clear();
            }

            cboEmployee.Enabled = cboEmployeeApprove.Enabled = !(wfh.ID > 0);
            lblValidateHrEdit.Visible = (wfh.ID > 0);
        }

        //private void loadDetail()
        //{
        //    //dtpRegisterDate.Value = TextUtils.ToDate(wfh.DateWFH.ToString());
        //    dtpRegisterDate.Value = wfh.DateWFH.HasValue == true ? wfh.DateWFH.Value : DateTime.Now; ;
        //    cbEmployee.EditValue = wfh.EmployeeID;
        //    cbApproved.EditValue = wfh.ApprovedID;
        //    txtReason.Text = wfh.Reason;
        //}
        //void loadEmployee()
        //{
        //    cbEmployee.Properties.DataSource = dt;
        //    cbEmployee.Properties.DisplayMember = "FullName";
        //    cbEmployee.Properties.ValueMember = "ID";
        //}
        //void loadApproved()
        //{
        //    cbApproved.Properties.DataSource = dt;
        //    cbApproved.Properties.DisplayMember = "FullName";
        //    cbApproved.Properties.ValueMember = "ID";
        //}

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
            
            wfh.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            wfh.ApprovedID = TextUtils.ToInt(cboEmployeeApprove.EditValue);
            wfh.DateWFH = dtpRegisterDate.Value.Date;
            wfh.TimeWFH = cboTimeWFH.SelectedIndex;
            wfh.TotalDay = wfh.TimeWFH == 3 ? 1 : 0.5m;
            wfh.Reason = txtReason.Text.Trim();
            wfh.ReasonHREdit = txtReasonHREdit.Text.Trim();
            wfh.ContentWork = txtContentWork.Text.Trim();
            if (wfh.ID > 0)
            {
                wfh.IsApproved = wfh.IsApprovedHR = false;
                //EmployeeWFHBO.Instance.Update(wfh);
                SQLHelper<EmployeeWFHModel>.Update(wfh);
            }
            else
            {
                //EmployeeWFHBO.Instance.Insert(wfh);
                SQLHelper<EmployeeWFHModel>.Insert(wfh);
            }  
            return true;
        }
        bool ValidateForm()
        {
            if (TextUtils.ToInt(cboEmployee.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Người đăng ký! ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (TextUtils.ToInt(cboEmployeeApprove.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Người duyệt! ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboTimeWFH.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng nhập Thời gian! ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //else
            //{
            //    int id = TextUtils.ToInt(TextUtils.ExcuteScalar($"EXEC spGetEmployeeWFHByDate '{dtpRegisterDate.Value.ToString("yyyy-MM-dd")}',{TextUtils.ToInt(cbEmployee.EditValue)}"));
            //    if (id > 0)
            //    {
            //        MessageBox.Show($"Nhân viên [{cbEmployee.Text}] đã đăng ký WFH cho ngày {dtpRegisterDate.Value.ToString("dd/MM/yyyy")}! ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return false;
            //    }
            //}

            var exp1 = new Expression("EmployeeID", TextUtils.ToInt(cboEmployee.EditValue));
            var exp2 = new Expression("DateWFH", dtpRegisterDate.Value.ToString("yyyy-MM-dd"));
            var exp3 = new Expression("TimeWFH", cboTimeWFH.SelectedIndex);
            var exp4 = new Expression("ID", wfh.ID, "<>");
            var list = SQLHelper<EmployeeWFHModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4));
            if (list.Count() > 0)
            {
                MessageBox.Show(string.Format($"Nhân viên [{cboEmployee.Text}] đã khai báo WFH [{cboTimeWFH.Text}]!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;

            }

            if (string.IsNullOrEmpty(txtReason.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Lý do! ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (wfh.ID > 0)
            {
                if (string.IsNullOrEmpty(txtReasonHREdit.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Lý do sửa! ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                wfh = new EmployeeWFHModel();
                LoadData();
            }
        }
    }
}
