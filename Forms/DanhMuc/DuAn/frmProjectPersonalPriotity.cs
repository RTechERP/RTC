using BMS;
using BMS.Business;
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

namespace BMS
{
    public partial class frmProjectPersonalPriotity : _Forms
    {
        public int ProjectID;
        public frmProjectPersonalPriotity()
        {
            InitializeComponent();
        }

        private void frmProjectPersonalPriotity_Load(object sender, EventArgs e)
        {
            cboProject.EditValue = ProjectID;
            LoadProject();
            LoadPriotity();
        }
        void LoadProject()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.Select($"SELECT * FROM dbo.Project");
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DataSource = dt;
        }
        void LoadPriotity()
        {
            int priotity = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 Priotity FROM dbo.ProjectPersonalPriotity WHERE UserID = {Global.UserID} AND ProjectID = {TextUtils.ToInt(cboProject.EditValue)}"));
            cboPriotity.SelectedIndex = priotity - 1;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(cboPriotity.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Mức độ ưu tiên!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (string.IsNullOrEmpty(TextUtils.ToString(cboProject.EditValue)))
            {
                MessageBox.Show("Vui lòng chọn Dự án!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                ProjectPersonalPriotityModel model = new ProjectPersonalPriotityModel();
                int id = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 ID FROM dbo.ProjectPersonalPriotity WHERE UserID = {Global.UserID} AND ProjectID = {TextUtils.ToInt(cboProject.EditValue)}"));
                
                model.ProjectID = TextUtils.ToInt(cboProject.EditValue);
                model.UserID = Global.UserID;
                model.Priotity =TextUtils.ToInt(cboPriotity.Text.Trim());
                if (id > 0)
                {
                    model.ID = id;
                    ProjectPersonalPriotityBO.Instance.Update(model);
                }
                else
                {
                    ProjectPersonalPriotityBO.Instance.Insert(model);
                }
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}