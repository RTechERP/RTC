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
    public partial class frmChangeProjectReport : _Forms
    {
        public int projectIdOld;
        public frmChangeProjectReport()
        {
            InitializeComponent();
        }

        private void frmChangeProjectReport_Load(object sender, EventArgs e)
        {
            cboProjectOld.EditValue = projectIdOld;
            loadProject();
        }

        /// <summary>
        /// Load Dự án lên combobox
        /// </summary>
        void loadProject()
        {
            DataTable dt = TextUtils.Select("select ID, ProjectCode, ProjectName, ProjectCode + ' - ' + ProjectName as ProjectNameDisplay from Project order by ID desc");

            cboProjectOld.Properties.ValueMember = cboProjectNew.Properties.ValueMember = "ID";
            cboProjectOld.Properties.DisplayMember = cboProjectNew.Properties.DisplayMember = "ProjectNameDisplay";
            cboProjectOld.Properties.DataSource = cboProjectNew.Properties.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Update mã dự án bảng DailyReportTechnical
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {
            int projectIdOld = TextUtils.ToInt(cboProjectOld.EditValue);
            int projectIdNew = TextUtils.ToInt(cboProjectNew.EditValue);

            if (projectIdOld <= 0 || projectIdNew <= 0)
            {
                MessageBox.Show("Bạn phải chọn dự án!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            TextUtils.ExcuteProcedure("spUpdateProjectIDInDailyReportTechnical_ByNewProjectID", new string[] { "@OldProjectID", "@NewProjectID" }, new object[] { projectIdOld, projectIdNew });
            return true;
        }
    }
}
