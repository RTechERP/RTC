using BMS.Model;
using BMS.Utils;
using DevExpress.XtraTreeList.Nodes;
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
    public partial class frmProjectTypeLink : _Forms
    {
        public frmProjectTypeLink()
        {
            InitializeComponent();
        }

        private void frmProjectTypeLink_Load(object sender, EventArgs e)
        {
            LoadProject();
            LoadProjectStatus();
            LoadProjectCurrentSituation();
            LoadLeader();
            LoadProjectTypeLink();
        }


        void LoadProject()
        {
            List<ProjectModel> listProjects = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.CreatedDate).ToList();
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.DataSource = listProjects;
        }

        void LoadProjectStatus()
        {
            //cboStatus.Properties.DataSource = TextUtils.Select("Select * from ProjectStatus");
            List<ProjectStatusModel> listStatus = SQLHelper<ProjectStatusModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboProjectStatus.Properties.DataSource = listStatus;
            cboProjectStatus.Properties.DisplayMember = "StatusName";
            cboProjectStatus.Properties.ValueMember = "ID";

            int projectID = TextUtils.ToInt(cboProject.EditValue);
            ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectID);
            cboProjectStatus.EditValue = project.ProjectStatus;
        }

        void LoadProjectCurrentSituation()
        {
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            var exp1 = new Expression("ProjectID", projectID);
            var exp2 = new Expression("EmployeeID", Global.EmployeeID);
            ProjectCurrentSituationModel situation = SQLHelper<ProjectCurrentSituationModel>.FindByExpression(exp1.And(exp2))
                                                                                            .OrderByDescending(x => x.DateSituation)
                                                                                            .FirstOrDefault();
            situation = situation ?? new ProjectCurrentSituationModel();
            txtContentSituation.Text = situation.ContentSituation;

        }
        void LoadProjectTypeLink()
        {
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectTypeLink", "A", new string[] { "@ProjectID" }, new object[] { projectID });
            tlProjectType.DataSource = dt;
            tlProjectType.ExpandAll();
        }
        void LoadLeader()
        {
            //ProjectModel project = (ProjectModel)cboProject.GetSelectedDataRow() ?? new ProjectModel();
            //DateTime dateCreated = project.CreatedDate.HasValue ? project.CreatedDate.Value : DateTime.Now;

            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetUserTeam", new string[] { "@DepartmentID" }, new object[] { 0 });
            cboLeaderID.DisplayMember = "FullName";
            cboLeaderID.ValueMember = "EmployeeID";
            cboLeaderID.DataSource = dataSet.Tables[1];
        }



        bool SaveData()
        {
            tlProjectType.CloseEditor();
            if (!CheckValidate()) return false;

            int projectID = TextUtils.ToInt(cboProject.EditValue);
            int projectStatus = TextUtils.ToInt(cboProjectStatus.EditValue);


            ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectID);
            if (project.ID <= 0) return false;

            //Update trạng thái dự án
            project.ProjectStatus = projectStatus;
            SQLHelper<ProjectModel>.Update(project);

            //Update leader kiểu dự án
            var listNodes = tlProjectType.GetNodeList();
            foreach (TreeListNode node in listNodes)
            {

                int id = TextUtils.ToInt(tlProjectType.GetRowCellValue(node, colProjectTypeLinkID));
                ProjectTypeLinkModel projectType = SQLHelper<ProjectTypeLinkModel>.FindByID(id);
                projectType.ProjectTypeID = TextUtils.ToInt(tlProjectType.GetRowCellValue(node, colID));
                projectType.ProjectID = projectID;
                projectType.LeaderID = TextUtils.ToInt(tlProjectType.GetRowCellValue(node, colLeaderID));
                projectType.Selected = TextUtils.ToBoolean(tlProjectType.GetRowCellValue(node, colSelected));
                if (projectType.ID <= 0)
                {
                    SQLHelper<ProjectTypeLinkModel>.Insert(projectType);
                }
                else
                {
                    SQLHelper<ProjectTypeLinkModel>.Update(projectType);
                }
            }

            //Update hiện trạng dự án
            if (!string.IsNullOrWhiteSpace(txtContentSituation.Text.Trim()))
            {
                ProjectCurrentSituationModel situation = new ProjectCurrentSituationModel();
                situation.ProjectID = project.ID;
                situation.EmployeeID = Global.EmployeeID;
                situation.DateSituation = DateTime.Now;
                situation.ContentSituation = txtContentSituation.Text.Trim();
                SQLHelper<ProjectCurrentSituationModel>.Insert(situation);
            }

            //Thêm dữ liệu vào bảng người tham gia
            foreach (TreeListNode node in tlProjectType.GetNodeList())
            {
                bool isSelected = TextUtils.ToBoolean(tlProjectType.GetRowCellValue(node, colSelected));
                int userID = TextUtils.ToInt(tlProjectType.GetRowCellValue(node, colLeaderID));
                var rowData = (DataRowView)cboLeaderID.GetRowByKeyValue(userID);
                if (!isSelected || userID <= 0 || rowData == null) continue;


                ProjectEmployeeModel model = new ProjectEmployeeModel();


                int projectTypeID = TextUtils.ToInt(tlProjectType.GetRowCellValue(node, colProjectTypeID));
                int employeeID = TextUtils.ToInt(rowData["EmployeeID"]);

                var exp1 = new Expression("ProjectID", project.ID);
                var exp2 = new Expression("EmployeeID", employeeID);
                var exp3 = new Expression("ProjectTypeID", projectTypeID);
                var exp4 = new Expression("IsDeleted", 1, "<>");
                var projectEmployee = SQLHelper<ProjectEmployeeModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4)).ToList();
                if (projectEmployee.Count > 0)
                {
                    model = projectEmployee.FirstOrDefault();
                }

                model.ProjectID = project.ID;
                model.EmployeeID = employeeID;
                model.ProjectTypeID = projectTypeID;
                //model.ProjectTypeID = projectTypeID;
                model.IsLeader = true;

                if (model.ID > 0)
                {
                    SQLHelper<ProjectEmployeeModel>.Update(model);
                }
                else
                {
                    var list = SQLHelper<ProjectEmployeeModel>.FindByExpression(exp1.And(exp4)).ToList();
                    model.STT = list.Count + 1;
                    SQLHelper<ProjectEmployeeModel>.Insert(model);
                }
            }

            return true;
        }


        bool CheckValidate()
        {
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            int projectStatus = TextUtils.ToInt(cboProjectStatus.EditValue);
            if (projectID <= 0)
            {
                MessageBox.Show("Vui lòng nhập Dự án!", "Thông báo");
                return false;
            }

            if (projectStatus <= 0)
            {

                MessageBox.Show("Vui lòng nhập Trạng thái!", "Thông báo");
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
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
                //this.DialogResult = DialogResult.OK;
            }
        }

        private void frmProjectTypeLink_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void frmProjectTypeLink_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadProjectStatus();
            LoadProjectTypeLink();
            LoadProjectCurrentSituation();
        }
    }
}
