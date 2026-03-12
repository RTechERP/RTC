using BMS.Business;
using BMS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmOvertimeDetail : _Forms
    {
        public EmployeeOvertimeModel overtime = new EmployeeOvertimeModel();

        DataTable dtHoliday = TextUtils.Select("Select * from Holiday");
        public frmOvertimeDetail()
        {
            InitializeComponent();
        }

        void LoadOvertime()
        {
            if (overtime.ID > 0)
            {
                cboName.EditValue = TextUtils.ToInt(overtime.EmployeeID);
                dtpDate.Value = overtime.DateRegister.Value;
                dtpTimeStart.Value = overtime.TimeStart.Value;
                dtpEndTime.Value = overtime.EndTime.Value;
                txtTimeReality.Text = TextUtils.ToString(overtime.TimeReality);
                txtTotalTime.Text = TextUtils.ToString(overtime.TotalTime);
                txtCost.Text = TextUtils.ToString(overtime.CostOvertime);
                cboType.EditValue = TextUtils.ToInt(overtime.TypeID);
                cboLocation.SelectedIndex = overtime.Location;
            }
            
            //txtRatio.Text = TextUtils.ToString(overtime.Ratio);
           }

        void LoadcboName()
        {
            DataTable dt = TextUtils.Select("Select ID, Code, FullName from dbo.Employee WHERE Status <> 1");
            cboName.Properties.DataSource = dt;
            cboName.Properties.DisplayMember = "FullName";
            cboName.Properties.ValueMember = "ID";
        }

        void LoadcboType()
        {
            //DataTable dtt = TextUtils.Select("Select * from dbo.EmployeeType");
            //cboType.DataSource = dtt;
            //cboType.DisplayMember = "Type";
            //cboType.ValueMember = "ID";
            DataTable dtt = TextUtils.Select("Select ID,Type,Ratio from dbo.EmployeeTypeOverTime");
            cboType.Properties.DataSource = dtt;
            cboType.Properties.DisplayMember = "Type";
            cboType.Properties.ValueMember = "ID";
        }

        public bool ValidateForm()
        {
            if(string.IsNullOrEmpty(cboName.Text.Trim()))
            {
                MessageBox.Show(string.Format("Vui lòng nhập Họ và tên !"), TextUtils.Caption, MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return false;
            }

            if (TextUtils.ToInt(txtTimeReality.Text) <= 0)
            {
                MessageBox.Show(string.Format("Số giờ làm thêm phải lớn hơn 0.\nVui lòng kiểm tra lại Ngày bắt đầu hoặc Ngày kết thúc!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(cboType.Text.Trim()))
            {
                MessageBox.Show(string.Format("Vui lòng nhập Loại làm thêm!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (cboLocation.SelectedIndex <= 0)
            {
                MessageBox.Show(string.Format("Vui lòng nhập Địa điểm!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            //if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            //{
            //    MessageBox.Show(string.Format("Vui lòng nhập Địa điểm!"),TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}


            return true;
        }
        
        bool saveData()
        {
            if (!ValidateForm()) return false;

            overtime.EmployeeID = TextUtils.ToInt(cboName.EditValue);
            overtime.DateRegister = dtpDate.Value;
            overtime.TimeStart = dtpTimeStart.Value;
            overtime.EndTime = dtpEndTime.Value;
            overtime.Location = cboLocation.SelectedIndex;
            overtime.TypeID = TextUtils.ToInt(cboType.EditValue);
            overtime.TimeReality = TextUtils.ToDecimal(txtTimeReality.Text);
            overtime.TotalTime = TextUtils.ToDecimal(txtTotalTime.Text);
            overtime.CostOvertime = TextUtils.ToDecimal(txtCost.Text.Substring(0, txtCost.TextLength-2));
            

            if (overtime.ID > 0)
            {
                EmployeeOvertimeBO.Instance.Update(overtime);
            }
            else
            {
                overtime.ID = (int)EmployeeOvertimeBO.Instance.Insert(overtime);
            }    
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Set thời gian thực tế, thời gian thực lĩnh và chi phí OT
        /// </summary>
        void CalculatorHour()
        {
            decimal timeReal = TextUtils.ToDecimal((dtpEndTime.Value - dtpTimeStart.Value).TotalHours);
            txtTimeReality.Text = timeReal.ToString("#.##");
            if (TextUtils.ToInt(cboType.EditValue) <= 0)
            {
                return;
            }
            DataTable dt = TextUtils.Select($"SELECT Ratio, Cost FROM EmployeeTypeOvertime WHERE ID = {TextUtils.ToInt(cboType.EditValue)}");

            decimal ratio = TextUtils.ToDecimal(dt.Rows[0]["Ratio"]);

            decimal totalTime = TextUtils.ToDecimal(timeReal * (ratio / 100));
            decimal cost = TextUtils.ToDecimal(dt.Rows[0]["Cost"]);

            txtTotalTime.Text = totalTime.ToString("#.##");
            txtCost.Text = string.Format(CultureInfo.GetCultureInfo("vi-VN"),"{0:c}", cost);

        }
        private void frmOvertimeDetail_Load(object sender, EventArgs e)
        {
            LoadcboName();
            LoadcboType();
            LoadOvertime();
        }


        private void cboType_EditValueChanged(object sender, EventArgs e)
        {
            //txtRatio.Text = TextUtils.ToString(sluType.GetFocusedRowCellValue(colRatio)) + " %";
            CalculatorHour();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dtpTimeStart_ValueChanged(object sender, EventArgs e)
        {
            CalculatorHour();
        }

        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            CalculatorHour();
        }
    }
}
