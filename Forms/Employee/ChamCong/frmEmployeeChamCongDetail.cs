using BMS.Business;
using BMS.Model;
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
    public partial class frmEmployeeChamCongDetail : _Forms
    {

        public EmployeeChamCongMasterModel chamCongMasterModel = new EmployeeChamCongMasterModel();
        public frmEmployeeChamCongDetail()
        {
            InitializeComponent();
        }

        private void frmEmployeeChamCongDetail_Load(object sender, EventArgs e)
        {
            txtYear.Value = DateTime.Now.Year;
            txtMonth.Value = DateTime.Now.Month;

            loadData();
        }
        void loadData()
        {
            if (chamCongMasterModel.ID > 0)
            {
                //cbTime.SelectedIndex = chamCongMasterModel.TimeType;
                txtMonth.Value = TextUtils.ToInt(chamCongMasterModel._Month);
                txtYear.Value = TextUtils.ToInt(chamCongMasterModel._Year);
                //dtpStart.Value=
                txtName.Text = chamCongMasterModel.Name;
                txtNote.Text = chamCongMasterModel.Note;
            }
            else
            {
                txtName.Text = $"Bảng chấm công tháng {txtMonth.Value}/{txtYear.Value}";
            }

        }

        bool validate()
        {
            
            if (string.IsNullOrEmpty(txtMonth.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tháng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txtYear.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Năm", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên bảng chấm công", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            EmployeeChamCongMasterModel employeeChamCong = SQLHelper<EmployeeChamCongMasterModel>
                .SqlToModel($"SELECT * FROM dbo.EmployeeChamCongMaster WHERE [_Year] = {txtYear.Value} AND [_Month] = {txtMonth.Value} AND ID <> {chamCongMasterModel.ID}");

            if (employeeChamCong.ID > 0)
            {
                MessageBox.Show($"Bảng chấm công tháng {txtMonth.Value}/{txtYear.Value} đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        bool save()
        {
            if (!validate()) return false;
            
            chamCongMasterModel._Month = TextUtils.ToInt(txtMonth.Value);
            chamCongMasterModel._Year = TextUtils.ToInt(txtYear.Value);
            chamCongMasterModel.Name = TextUtils.ToString(txtName.Text);
            chamCongMasterModel.Note = TextUtils.ToString(txtNote.Text);

            if (chamCongMasterModel.ID > 0)
            {
                EmployeeChamCongMasterBO.Instance.Update(chamCongMasterModel);
            }
            else
            {
                EmployeeChamCongMasterBO.Instance.Insert(chamCongMasterModel);
            }
            
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtMonth_ValueChanged(object sender, EventArgs e)
        {
            int number = DateTime.DaysInMonth(TextUtils.ToInt(txtYear.Value), TextUtils.ToInt(txtMonth.Value));
            dtpStart.Text = new DateTime(TextUtils.ToInt(txtYear.Value), TextUtils.ToInt(txtMonth.Value), 1).ToString();
            dtpEnd.Text = new DateTime(TextUtils.ToInt(txtYear.Value), TextUtils.ToInt(txtMonth.Value), number).ToString();

            txtName.Text = $"Bảng chấm công tháng {txtMonth.Value}/{txtYear.Value}";
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            int number = DateTime.DaysInMonth(TextUtils.ToInt(txtYear.Value), TextUtils.ToInt(txtMonth.Value));
            dtpStart.Text = new DateTime(TextUtils.ToInt(txtYear.Value), TextUtils.ToInt(txtMonth.Value), 1).ToString();
            dtpEnd.Text = new DateTime(TextUtils.ToInt(txtYear.Value), TextUtils.ToInt(txtMonth.Value), number).ToString();

            txtName.Text = $"Bảng chấm công tháng {txtMonth.Value}/{txtYear.Value}";
        }
    }
}
