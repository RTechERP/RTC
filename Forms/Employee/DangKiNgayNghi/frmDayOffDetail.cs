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
    public partial class frmDayOffDetail : _Forms
    {

        public EmployeeOnLeaveModel onleave = new EmployeeOnLeaveModel();
        public frmDayOffDetail()
        {
            InitializeComponent();
        }

        private void frmDayOffDetail_Load(object sender, EventArgs e)
        {
            //loadEmlpoyee();
            //cbThoiGian.SelectedIndex = 3;
            //cbLoaiNghi.SelectedIndex = 2;
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
            if (onleave.ID > 0)
            {
                cboEmployee.EditValue = onleave.EmployeeID;
                cboEmployeeApprove.EditValue = onleave.ApprovedTP;
                dtpDateRegiter.Value = onleave.StartDate.Value.Date;
                cbThoiGian.SelectedIndex = onleave.TimeOnLeave;
                cboTypeIsReal.SelectedIndex = onleave.TypeIsReal;
                txtReason.Text = onleave.Reason;
                txtReasonHREdit.Text = onleave.ReasonHREdit;
            }
            else
            {
                cboEmployee.Focus();
                cboEmployee.EditValue = 0;
                cboEmployeeApprove.EditValue = 0;
                dtpDateRegiter.Value = DateTime.Now;
                cbThoiGian.SelectedIndex = 0;
                cboTypeIsReal.SelectedIndex = 0;
                txtReason.Clear();
                txtReasonHREdit.Clear();
            }

            cboEmployee.Enabled = cboEmployeeApprove.Enabled = !(onleave.ID > 0);
            lblValidateHrEdit.Visible = (onleave.ID > 0);
        }

        //void loadEmlpoyee()
        //{
        //    DataTable dt = TextUtils.Select("select ID,Code,FullName from Employee ");
        //    cboEmployee.Properties.DataSource = dt;
        //    cboEmployee.Properties.DisplayMember = "FullName";
        //    cboEmployee.Properties.ValueMember = "ID";
        //}
        //void loadData()
        //{
        //    if (Model.ID > 0)
        //    {
        //        cboEmployee.EditValue = Model.EmployeeID;
        //        dtpDateStart.Value = Model.StartDate.Value;
        //        dtpDateEnd.Value = Model.EndDate.Value;
        //        dtpDateRegiter.Value = Model.StartDate.Value;
        //        cbThoiGian.SelectedIndex = Model.TimeOnLeave;
        //        cbLoaiNghi.SelectedIndex = Model.Type;
        //        cboTypeIsReal.SelectedIndex = Model.TypeIsReal;
        //        //txtTotalTime.Text = Model.TotalTime.ToString();
        //        txtReason.Text = Model.Reason;
        //        txtReasonHREdit.Text = Model.Note;
        //    }    
        //}

        bool SaveData()
        {
            if (!CheckValidate()) return false;


            onleave.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            onleave.ApprovedTP = TextUtils.ToInt(cboEmployeeApprove.EditValue);
            onleave.TimeOnLeave = cbThoiGian.SelectedIndex;
            onleave.StartDate = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 8, 0, 0);
            onleave.EndDate = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 17, 30, 0);
            if (onleave.TimeOnLeave == 1)
            {
                onleave.StartDate = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 8, 0, 0);
                onleave.EndDate = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 12, 0, 0);
            }
            else if (onleave.TimeOnLeave == 2)
            {
                onleave.StartDate = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 13, 30, 0);
                onleave.EndDate = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 17, 30, 0);
            }

            onleave.TypeIsReal = cboTypeIsReal.SelectedIndex;
            onleave.Reason = txtReason.Text.Trim();
            onleave.ReasonHREdit = txtReasonHREdit.Text.Trim();
            //onleave.Note = txtReasonHREdit.Text.Trim();
            onleave.TotalTime = cbThoiGian.SelectedIndex == 3 ? 8 : 4;
            onleave.TotalDay = cbThoiGian.SelectedIndex == 3 ? 1 : 0.5m;
            //if (cbThoiGian.SelectedIndex == 3)
            //{
            //    onleave.TotalTime = 8;
            //    onleave.TotalDay = 1;
            //}
            //else
            //{
            //    onleave.TotalTime = 4;
            //    onleave.TotalDay = 0.5m;
            //}


            if (onleave.ID > 0)
            {
                onleave.IsApprovedHR = onleave.IsApprovedTP = false;
                //EmployeeOnLeaveBO.Instance.Update(onleave);

                SQLHelper<EmployeeOnLeaveModel>.Update(onleave);
            }
            else
            {
                //EmployeeOnLeaveBO.Instance.Insert(onleave);
                SQLHelper<EmployeeOnLeaveModel>.Insert(onleave);
            }
            return true;
        }

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

            //else
            //{
            //    int id = TextUtils.ToInt(TextUtils.ExcuteScalar($"EXEC spGetEmployeeOnleaveByDate '{dtpDateRegiter.Value.ToString("yyyy-MM-dd")}', {TextUtils.ToInt(cboEmployee.EditValue)}"));
            //    if (id > 0)
            //    {
            //        MessageBox.Show($"Nhân viên [{cboEmployee.Text}] đã đăng ký nghỉ cho ngày {dtpDateRegiter.Value.ToString("dd/MM/yyyy")}!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return false;
            //    }
            //}

            if (cbThoiGian.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng nhập Thời gian nghỉ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboTypeIsReal.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng nhập Loại nghỉ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(txtReason.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Lý do!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var exp1 = new Expression("EmployeeID", TextUtils.ToInt(cboEmployee.EditValue));
            var exp2 = new Expression("TypeIsReal", cboTypeIsReal.SelectedIndex);
            var exp3 = new Expression("ID", onleave.ID, "<>");
            var exp4 = new Expression("DeleteFlag", 0);
            var list = SQLHelper<EmployeeOnLeaveModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4)).Where(x => x.StartDate.Value.Year == dtpDateRegiter.Value.Year &&
                                                                                                        x.StartDate.Value.Month == dtpDateRegiter.Value.Month &&
                                                                                                        x.StartDate.Value.Day == dtpDateRegiter.Value.Day).ToList();
            if (list.Count() > 0)
            {
                MessageBox.Show(string.Format($"Nhân viên [{cboEmployee.Text}] đã đăng ký nghỉ ngày {dtpDateRegiter.Value.ToString("dd/MM/yyyy")} [{cboTypeIsReal.Text}]!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (onleave.ID > 0)
            {
                if (string.IsNullOrEmpty(txtReasonHREdit.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Lý do sửa! ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;

        }

        private void cbThoiGian_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbThoiGian.SelectedIndex == 1)
            //{
            //    dtpDateStart.Value = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 8, 0, 0);
            //    dtpDateEnd.Value = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 12, 0, 0);
            //    //txtTotalTime.Text = "4";
            //}
            //else if (cbThoiGian.SelectedIndex == 2)
            //{
            //    dtpDateStart.Value = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 13, 30, 0);
            //    dtpDateEnd.Value = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 17, 30, 0);
            //    //txtTotalTime.Text = "4";
            //}
            //else if (cbThoiGian.SelectedIndex == 3)
            //{
            //    dtpDateStart.Value = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 8, 0, 0);
            //    dtpDateEnd.Value = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 17, 30, 0);
            //    //txtTotalTime.Text = "8";
            //}
        }

        private void dtpDateRegiter_ValueChanged(object sender, EventArgs e)
        {
            //if (cbThoiGian.SelectedIndex == 1)
            //{
            //    dtpDateStart.Value = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 8, 0, 0);
            //    dtpDateEnd.Value = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 12, 0, 0);
            //    //txtTotalTime.Text = "4";
            //}
            //else if (cbThoiGian.SelectedIndex == 2)
            //{
            //    dtpDateStart.Value = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 13, 30, 0);
            //    dtpDateEnd.Value = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 17, 30, 0);
            //    //txtTotalTime.Text = "4";
            //}
            //else if (cbThoiGian.SelectedIndex == 3)
            //{
            //    dtpDateStart.Value = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 8, 0, 0);
            //    dtpDateEnd.Value = new DateTime(dtpDateRegiter.Value.Year, dtpDateRegiter.Value.Month, dtpDateRegiter.Value.Day, 17, 30, 0);
            //    //txtTotalTime.Text = "8";
            //}
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
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
                onleave = new EmployeeOnLeaveModel();
                LoadData();
            }
        }
    }
}
