using BMS;
using BMS.Model;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Data.Filtering.Helpers.SubExprHelper.ThreadHoppingFiltering;

namespace Forms.Employee.TeamPhongBan
{
    public partial class frmEmployeeTeamDetail : _Forms
    {
        public EmployeeTeamModel team = new EmployeeTeamModel();
        public Func<EmployeeTeamModel, Task> SaveEvent;
        public frmEmployeeTeamDetail()
        {
            InitializeComponent();
            loadDepartment();

        }
        void loadData()
        {
            txtName.EditValue = team.Name;
            txtCode.Text = team.Code;
            txtSTT.Text = team.STT.ToString();

            if (team.ID <= 0)
            {
                var listTeam = SQLHelper<EmployeeTeamModel>.FindByAttribute(EmployeeTeamModel_Enum.IsDeleted.ToString(), 0);
                int maxSTT = listTeam.Count <= 0 ? 0 : listTeam.Max(x => x.STT ?? 0);
                txtSTT.EditValue = maxSTT + 1;

                txtCode.Text = $"T{maxSTT + 1}";
            }
            else
            {
                cbDepartment.EditValue = team.DepartmentID;
            }
        }

        private void frmTeamPhongBanDetail_Load(object sender, EventArgs e)
        {
            loadData();
            loadDepartment();
        }
        void loadDepartment()
        {
            List<DepartmentModel> departments = SQLHelper<DepartmentModel>.FindAll();
            cbDepartment.Properties.DataSource = departments;
            cbDepartment.Properties.DisplayMember = "Name";
            cbDepartment.Properties.ValueMember = "ID";
        }
        bool validate()
        {
            if (txtName.Text.Trim() == "")
            {
                TextUtils.ShowError("Xin hãy điền tên nhóm!");
                return false;
            }
            if (cbDepartment.EditValue == null)
            {
                TextUtils.ShowError("Xin hãy chọn phòng ban!");
                return false;
            }
            if (txtSTT.Text.Trim() == "")
            {
                TextUtils.ShowError("Xin hãy điền số thứ tự!");
                return false;
            }
            if (txtCode.Text.Trim() == "")
            {
                TextUtils.ShowError("Xin hãy điền mã nhóm!");
                return false;
            }
            return true;
        }
        bool save()
        {
            if (!validate()) return false;
            team.Name = txtName.Text.Trim();
            team.Code = txtCode.Text.Trim();
            team.DepartmentID = TextUtils.ToInt(cbDepartment.EditValue);
            team.STT = TextUtils.ToInt(txtSTT.Text);

            if (team.ID > 0)
            {
                team.UpdatedBy = Global.AppCodeName;
                team.UpdatedDate = DateTime.Now;
                SQLHelper<EmployeeTeamModel>.Update(team);
            }
            else
            {
                team.CreatedBy = Global.AppUserName;
                team.CreatedDate = DateTime.Now;
                team.ID = SQLHelper<EmployeeTeamModel>.Insert(team).ID;
            }
            SaveEvent(team);
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
            {
                this.Close();
            }
        }

        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (save())
            {
                team = new EmployeeTeamModel();
                loadData();
            }
        }

        private void frmTeamPhongBanDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}