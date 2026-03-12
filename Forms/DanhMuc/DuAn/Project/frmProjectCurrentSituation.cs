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
    public partial class frmProjectCurrentSituation : _Forms
    {
        public int projectID { get; set; }
        public frmProjectCurrentSituation()
        {
            InitializeComponent();
        }

        private void frmProjectCurrentSituation_Load(object sender, EventArgs e)
        {
            LoadProject();
            LoadEmployee();
        }
        void LoadProject()
        {
            List<ProjectModel> listProjects = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.CreatedDate).ToList();
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.DataSource = listProjects;
            cboProject.EditValue = projectID;
        }
        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
            cboEmployee.EditValue = Global.EmployeeID;
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadProjectCurrentSituation();
        }
        private void LoadProjectCurrentSituation()
        {
            int ID = TextUtils.ToInt(cboProject.EditValue);
            DataTable dt = TextUtils.GetDataTableFromSP("spGetProjectCurrentSituation", new string[] { "@ProjectID" }, new object[] { ID });
            grdData.DataSource = dt;

        }
        bool CheckValidate()
        {
            if (TextUtils.ToInt(cboProject.EditValue) == 0)
            {
                MessageBox.Show("Vui lòng nhập Dự án!", "Thông báo");
                return false;
            }
            if (string.IsNullOrEmpty(txtContentSituation.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Nội dung!", "Thông báo");
                return false;
            }
            return true;
        }
        bool Save()
        {
            if (!CheckValidate()) return false;

            int id = TextUtils.ToInt(txtID.Text);
            ProjectCurrentSituationModel model = SQLHelper<ProjectCurrentSituationModel>.FindByID(id);
            model.ProjectID = TextUtils.ToInt(cboProject.EditValue);
            model.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            model.ContentSituation = txtContentSituation.Text.Trim();
            model.DateSituation = DateTime.Now;

            //ProjectCurrentSituationModel model = new ProjectCurrentSituationModel()
            //{

            //};

            if (model.ID > 0)
            {
                SQLHelper<ProjectCurrentSituationModel>.Update(model);
            }
            else
            {

                SQLHelper<ProjectCurrentSituationModel>.Insert(model);
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Save()) return;
            this.DialogResult = DialogResult.OK;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (!Save()) return;
            txtContentSituation.Clear();
            LoadProjectCurrentSituation();
        }

        private void frmProjectCurrentSituation_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            //if (employeeID == 0) return;
            if (Global.EmployeeID == employeeID)
            {
                cboEmployee.EditValue = employeeID;
                txtContentSituation.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("ContentSituation"));
                txtID.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue("ID"));
            }
        }
    }
}