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
    public partial class frmDailyrReportTechnicalChangeProject : _Forms
    {

        public List<int> listID = new List<int>();
        public frmDailyrReportTechnicalChangeProject()
        {
            InitializeComponent();
        }

        private void frmDailyrReportTechnicalChangeProject_Load(object sender, EventArgs e)
        {
            LoadProject();
        }


        void LoadProject()
        {
            List<ProjectModel> listProject = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.CreatedDate).ToList();

            cboProjectOld.Properties.ValueMember = cboProjectNew.Properties.ValueMember = "ID";
            cboProjectOld.Properties.DisplayMember = cboProjectNew.Properties.DisplayMember = "ProjectCode";
            cboProjectOld.Properties.DataSource = cboProjectNew.Properties.DataSource = listProject;
        }


        bool CheckValidate()
        {

            if (TextUtils.ToInt(cboProjectNew.EditValue) <=0)
            {
                MessageBox.Show("Vui lòng nhập Đến dự án!", "Thông báo");
                return false;
            }
            return true;
        }


        bool SaveData()
        {
            if (!CheckValidate()) return false;

            foreach (int id in listID)
            {
                var myDict = new Dictionary<string, object>()
                {
                    {"ProjectID", TextUtils.ToInt(cboProjectNew.EditValue)},
                    {"UpdatedDate", DateTime.Now.ToString("yyyy-MM-dd")},
                    {"UpdatedBy", Global.LoginName},
                };

                SQLHelper<DailyReportTechnicalModel>.UpdateFieldsByID(myDict, id);
            }

            return true;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmDailyrReportTechnicalChangeProject_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void frmDailyrReportTechnicalChangeProject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
