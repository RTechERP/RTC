using BMS.Model;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmSummaryOfExamResults : _Forms
    {
        private bool lockItem = false;
        DataTable dtEmployeeMaster = new DataTable();

        public frmSummaryOfExamResults()
        {
            InitializeComponent();
        }

        private void frmSummaryOfExamResults_Load(object sender, EventArgs e)
        {
            lockItem = true;

            loadDepartment();
            loadTeam();
            loadEmployee();

            loadEmployeeMaster();
            loadExamResult();

            lockItem = false;
        }

        private void loadExamResult()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int empoyeeID = TextUtils.ToInt(grvEmployeeMaster.GetFocusedRowCellValue(colID));
            //if (empoyeeID == 0) return;
            //DataTable dt = TextUtils.LoadDataFromSP("spGetSummaryOfExamResults", "A",
            //                                     new string[] { "@EmployeeID" },
            //                                     new object[] { empoyeeID });


            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseNew", "A",
                                                    new string[] { "@DepartmentID", "@EmployeeID", "@Status" },
                                                    new object[] { departmentID, empoyeeID, -1 });
            grdExamResults.DataSource = dt;


            var summarys = grvExamResults.Columns[colEvaluateText.FieldName].Summary;
            if (summarys.Count > 0)
            {
                grvExamResults.Columns[colEvaluateText.FieldName].Summary.Clear();
            }

            var dataPass = dt.Select("Evaluate = 1");
            var dataNotPass = dt.Select("Evaluate = 0");

            grvExamResults.Columns[colEvaluateText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colEvaluateText.FieldName, $"Đạt = {dataPass.Length.ToString()}"));
            grvExamResults.Columns[colEvaluateText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colEvaluateText.FieldName, $"Không đạt = {dataNotPass.Length.ToString()}"));
        }

        private void loadEmployeeMaster()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int userTeamID = TextUtils.ToInt(cbTeam.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);

            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@DepartmentID", "@Status", "@ID" }, new object[] { departmentID, 0, employeeID });
            if (userTeamID > 0 && employeeID <= 0)
            {
                dt = TextUtils.LoadDataFromSP("spGetEmployeeByTeamID", "A", new string[] { "@TeamID" }, new object[] { userTeamID });
            }
            grdEmployeeMaster.DataSource = dt;

            grvEmployeeMaster.FocusedRowHandle = 0;
        }

        private void loadTeam()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            List<UserTeamModel> list = SQLHelper<UserTeamModel>.FindByAttribute($"DepartmentID ", departmentID);

            cbTeam.Properties.DataSource = list;
            cbTeam.Properties.DisplayMember = "Name";
            cbTeam.Properties.ValueMember = "ID";

            UserTeamModel team = SQLHelper<UserTeamModel>.FindByAttribute("LeaderID", Global.EmployeeID).FirstOrDefault();
            if (team == null) return;
            cbTeam.EditValue = team.ID;
        }

        private void loadEmployee()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeID = 0;

            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@DepartmentID", "@Status", "@ID" }, new object[] { departmentID, 0, employeeID });
            cboEmployee.Properties.DataSource = dt;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
        }

        private void loadDepartment()
        {
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll();

            cboDepartment.Properties.DataSource = list;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";

            cboDepartment.EditValue = Global.EmployeeID == 54 ? 2 : Global.DepartmentID;
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            if (lockItem) return;
            loadTeam();
            loadEmployee();
            loadEmployeeMaster();

        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            if (lockItem) return;
            loadEmployeeMaster();
            //loadEmployee();
        }

        private void cbTeam_EditValueChanged(object sender, EventArgs e)
        {
            if (lockItem) return;
            loadEmployee();
            loadEmployeeMaster();

            int teamId = TextUtils.ToInt(cbTeam.EditValue);
            if (teamId == 0)
            {
                loadEmployee();
            }
            else
            {
                //DataTable dt = TextUtils.Select($"Select e.* from Employee As e Join Users As u on u.ID = e.UserID Join UserTeamLink as utl on utl.UserID = u.ID Join UserTeam as ut on ut.ID = utl.UserTeamID WHERE ut.ID = {teamId}");

                List<EmployeeModel> list = SQLHelper<EmployeeModel>.ProcedureToList("spGetEmployeeByTeamID", new string[] { "@TeamID" }, new object[] { teamId });
                cboEmployee.Properties.DataSource = list;
                cboEmployee.Properties.DisplayMember = "FullName";
                cboEmployee.Properties.ValueMember = "ID";
            }
        }

        private void grvEmployeeMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadExamResult();
        }

        private void grvExamResults_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (e.Column == colEvaluateText)
            {
                int evaluate = TextUtils.ToInt(grvExamResults.GetRowCellValue(e.RowHandle, colEvaluate));
                if (evaluate > 0)
                {
                    e.Appearance.BackColor = Color.Lime;
                }
                else
                {
                    e.Appearance.BackColor = Color.Orange;
                }
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //loadEmployeeMaster();

            loadEmployee();
            loadExamResult();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvExamResults.OptionsPrint.AutoWidth = false;
                //TreeData.OptionsPrint.ExpandAllDetails = false;
                grvExamResults.OptionsPrint.ExpandAllGroups = true;
                grvExamResults.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvExamResults.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void grvExamResults_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //bool isMultiChoice = TextUtils.ToBoolean(grvExamResults.GetRowCellValue(e.ListSourceRowIndex, colIsMultiChoice));
            //bool isPractice = TextUtils.ToBoolean(grvExamResults.GetRowCellValue(e.ListSourceRowIndex, colIsPractice));
            //bool isExercise = TextUtils.ToBoolean(grvExamResults.GetRowCellValue(e.ListSourceRowIndex, colIsExercise));

            ////bool isStyle = 
            //if (e.Column == colQuizPoints && !isMultiChoice)
            //{
            //    e.DisplayText = "-";
            //}

            //if (e.Column == colPracticePoints && !isPractice)
            //{
            //    e.DisplayText = "-";
            //}

            //if (e.Column == colIsExercise && !isExercise)
            //{
            //    e.DisplayText = "-";
            //}
        }
    }
}
