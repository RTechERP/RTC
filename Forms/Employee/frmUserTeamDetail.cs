using BMS;
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

namespace Forms.Employee
{
    public partial class frmUserTeamDetail : _Forms
    {
        public UserTeamModel ut = new UserTeamModel();
        public int ID = 0, DepartmentID = 0;
        public frmUserTeamDetail()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Hàm lưu thông tin
        /// </summary>
        /// <returns></returns>
        bool SaveData()
        {
            if (!validate())
            {
                return false;
            }
            ut.Name = txtName.Text.Trim();
            ut.LeaderID = TextUtils.ToInt(cboLeader.EditValue);
            ut.DepartmentID = TextUtils.ToInt(cboDepartment.SelectedValue);
            ut.ParentID = TextUtils.ToInt(cboNhom.EditValue);
            ut.ProjectTypeID = TextUtils.ToInt(cboTeamType.EditValue);

            if (ut.ID > 0)
            {
                UserTeamBO.Instance.Update(ut);
            }
            else
            {
                UserTeamBO.Instance.Insert(ut);
            }
            return true;
        }
        bool validate()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên team !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (TextUtils.ToInt(cboDepartment.SelectedValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng ban !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (TextUtils.ToInt(cboLeader.EditValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn leader !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (TextUtils.ToInt(cboNhom.EditValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn nhóm !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        private void frmUserTeamDetail_Load(object sender, EventArgs e)
        {
            loadTeamType();
            loadDepartment();
            loadLeader();
            loadUserTeamDetail();
            LoadDepartmentGroup();
            //cboDepartment.SelectedIndex = 0;
        }

        void loadTeamType()
        {
            List<ProjectTypeModel> list = SQLHelper<ProjectTypeModel>.FindAll();

            cboTeamType.Properties.DataSource = list;
            cboTeamType.Properties.ValueMember = "ID";
            cboTeamType.Properties.DisplayMember = "ProjectTypeName";
        }
        void LoadDepartmentGroup()
        {
            DataTable dt = TextUtils.Select("EXEC spGetTreeUserTeamData");
            cboNhom.Properties.DisplayMember = "Name";
            cboNhom.Properties.ValueMember = "ID";
            cboNhom.Properties.DataSource = dt;
        }
        /// <summary>
        /// Load Phòng ban
        /// </summary>
        void loadDepartment()
        {
            DataTable dt = TextUtils.Select("select ID, Name from Department");
            TextUtils.PopulateCombo(cboDepartment, dt, "Name", "ID", "-- Chọn phòng ban --");
        }

        /// <summary>
        /// Load Leader
        /// </summary>
        void loadLeader()
        {
            DataTable dt = TextUtils.Select("select ID, Code, FullName from Employee");
            cboLeader.Properties.DisplayMember = "FullName";
            cboLeader.Properties.ValueMember = "ID";
            cboLeader.Properties.DataSource = dt;
        }

        /// <summary>
        /// Load thông tin UserTeam lên form
        /// </summary>
        void loadUserTeamDetail()
        {
            if (ut.ID > 0)
            {
                txtName.Text = ut.Name;
                cboDepartment.SelectedValue = ut.DepartmentID;
                cboLeader.EditValue = ut.LeaderID;
                cboNhom.EditValue = ut.ParentID;
            }
            else
            {
                cboDepartment.SelectedValue = DepartmentID;
                cboNhom.EditValue = ID;
            }
        }
    }
}
