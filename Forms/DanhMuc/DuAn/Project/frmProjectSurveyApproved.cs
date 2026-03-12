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
    public partial class frmProjectSurveyApproved : _Forms
    {
        public ProjectSurveyDetailModel surveyDetail = new ProjectSurveyDetailModel();
        public ProjectSurveyModel survey = new ProjectSurveyModel();
        int status = 0;

        public DateTime? dateSurvey = null;
        public frmProjectSurveyApproved(int status)
        {
            InitializeComponent();
            this.status = status;
        }

        private void frmProjectSurveyApproved_Load(object sender, EventArgs e)
        {

            txtReasonCancel.Text = surveyDetail.ReasonCancel;
            radioButton1.Checked = surveyDetail.SurveySession == 1;
            radioButton2.Checked = surveyDetail.SurveySession == 2;

            dtpDateSurvey.Value = dateSurvey.HasValue ? dateSurvey.Value : DateTime.Now; //NTA B - update 091025

            if (surveyDetail.ID > 0 && surveyDetail.DateSurvey.HasValue) dtpDateSurvey.Value = surveyDetail.DateSurvey.Value;

            LoadEmployee();
        }


        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });

            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;

            cboEmployee.EditValue = surveyDetail.EmployeeID;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {

            surveyDetail.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            surveyDetail.DateSurvey = dtpDateSurvey.Value;
            surveyDetail.ReasonCancel = txtReasonCancel.Text.Trim();
            surveyDetail.SurveySession = radioButton1.Checked ? 1 : (radioButton2.Checked ? 2 : 0);

            if (status == 1)
            {
                if (surveyDetail.EmployeeID <= 0)
                {
                    MessageBox.Show("Vui lòng chọn Kỹ thuật phụ trách!", "Thông báo");
                    return;
                }

                if (survey.DateStart.HasValue && survey.DateEnd.HasValue)
                {
                    if (dtpDateSurvey.Value.Date < survey.DateStart.Value.Date || dtpDateSurvey.Value.Date > survey.DateEnd.Value.Date)
                    {
                        MessageBox.Show($"Ngày khảo sát phải trong khoảng từ [{survey.DateStart.Value.ToString("dd/MM/yyyy")}] đến [{survey.DateEnd.Value.ToString("dd/MM/yyyy")}]!", "Thông báo");
                        return;
                    }
                }

            }
            else if (string.IsNullOrWhiteSpace(txtReasonCancel.Text.Trim()))
            {

                MessageBox.Show("Vui lòng nhập Lý do hủy!", "Thông báo");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmProjectSurveyApproved_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void frmProjectSurveyApproved_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
