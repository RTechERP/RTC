using BMS.Model;
using BMS.Utils;
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
    public partial class frmKPISumarizeRule : _Forms
    {
        public string deName;
        public frmKPISumarizeRule()
        {
            InitializeComponent();
        }

        private void frmKPISumarizeRule_Load(object sender, EventArgs e)
        {
            this.Text += " - " + deName;
            LoadPosition();
            LoadDepartMent();
            LoadStatus();
            LoadUserTeam();
            LoadKPISession();
            LoadEmployee();

        }
        private void LoadStatus()
        {
            List<object> lst = new List<object>()
            {
                new {ID = -1, Status = "---Tất cả---"},
                new {ID = 0, Status = "Chưa đánh giá"},
                new {ID = 1, Status = "Đã đánh giá"}
            };
            cboStatus.Properties.DataSource = lst;
            cboStatus.Properties.ValueMember = "ID";
            cboStatus.Properties.DisplayMember = "Status";
            cboStatus.EditValue = -1;
        }
        private void LoadDepartMent()
        {
            List<DepartmentModel> lst = SQLHelper<DepartmentModel>.FindByAttribute("Status", 1);
            lst.Insert(0, new DepartmentModel()
            {
                ID = 0,
                Name = "---Tất cả---"
            });
            cboDepartMent.Properties.DataSource = lst;
            cboDepartMent.Properties.ValueMember = "ID";
            cboDepartMent.Properties.DisplayMember = "Name";
            cboDepartMent.EditValue = 2;
        }
        private void LoadUserTeam()
        {
            int departMent = TextUtils.ToInt(cboDepartMent.EditValue);
            List<UserTeamModel> lst = SQLHelper<UserTeamModel>.ProcedureToList("spGetUserTeam", new string[] { "@DepartmentID" }, new object[] { departMent });
            lst.Insert(0, new UserTeamModel()
            {
                ID = 0,
                Name = "---Tất cả---"
            });

            cboUserTeam.Properties.DataSource = lst;
            cboUserTeam.Properties.ValueMember = "ID";
            cboUserTeam.Properties.DisplayMember = "Name";
            cboUserTeam.EditValue = Global.UserTeamID;
        }
        private void LoadKPISession()
        {
            int year = TextUtils.ToInt(txtYear.Value);
            int quarter = (int)((DateTime.Now.Month + 2) / 3);
            Expression ex1 = new Expression("IsDeleted", 0);
            Expression ex2 = new Expression("YearEvaluation", year);
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByExpression(ex1.And(ex2)).OrderByDescending(p => p.ID).ToList();
            cboKPISession.Properties.DataSource = lst;
            cboKPISession.Properties.DisplayMember = "Code";
            cboKPISession.Properties.ValueMember = "ID";
            KPISessionModel currentSession = lst.FirstOrDefault(p => p.QuarterEvaluation == quarter) ?? new KPISessionModel();
            cboKPISession.EditValue = currentSession.ID;
            LoadKpiRule();
        }
        private void LoadKpiRule()
        {
            int kpiSessionId = TextUtils.ToInt(cboKPISession.EditValue);
            Expression ex1 = new Expression("KPISessionID", kpiSessionId);
            Expression ex2 = new Expression("IsDeleted", 0);
            List<KPIEvaluationRuleModel> lst = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2));
            grdRuleData.DataSource = lst;
            LoadEmployee();
        }

        private void LoadPosition()
        {
            List<KPIPositionModel> lst = SQLHelper<KPIPositionModel>.FindByAttribute(KPIPositionModel_Enum.IsDeleted.ToString(), 0);
            cboPositionGrvRule.DataSource = lst;
            cboPositionGrvRule.ValueMember = "ID";
            cboPositionGrvRule.DisplayMember = "PositionCode";
        }

        private void cboKPISession_EditValueChanged(object sender, EventArgs e)
        {
            LoadKpiRule();
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            LoadKPISession();
        }

        private void LoadEmployee()
        {
            int kpiRuleID = TextUtils.ToInt(grvRuleData.GetFocusedRowCellValue(colKPIRuleID));
            int departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            int userTeamID = TextUtils.ToInt(cboUserTeam.EditValue);
            int status = TextUtils.ToInt(cboStatus.EditValue);
            string keyWords = txtKeywords.Text.Trim();
            DataTable dt = TextUtils.LoadDataFromSP("spGetKPIEmployeePoint", "LMKTable",
                                            new string[] { "@KPIRuleID", "@DepartmentID", "@UserTeamID", "@Status", "@Keywords" },
                                            new object[] { kpiRuleID, departmentID, userTeamID, status, keyWords });
            grdEmpData.DataSource = dt;
            LoadDataEmployeeTeam();
        }

        private void grvRuleData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadEmployee();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadKpiRule();
        }
        private void grvEmpData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataEmployeeTeam();


        }
        private void LoadDataEmployeeTeam()
        {
            int empPointID = TextUtils.ToInt(grvEmpData.GetFocusedRowCellValue(colKPIEmployeePointID));
            //DataTable dt = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeam", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
            DataTable dt = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeamNew", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
            grdTeam.DataSource = dt;

            LoadEvaluationRuleDetails(empPointID);
        }
        private void LoadEvaluationRuleDetails(int rulePointID)
        {
            //int rulePointID = TextUtils.ToInt(grvEmpData.GetFocusedRowCellValue(colKPIEmployeePointID));
            //DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointID", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { rulePointID });
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointIDNew", "spGetEmployeeRulePointByKPIEmpPointIDNew",
                                                new string[] { "@KPIEmployeePointID" },
                                                new object[] { rulePointID });
            treeData.DataSource = dt;
            treeData.ExpandAll();
            CalculatorTotalPoint();
        }

        private void btnEvaluated_Click(object sender, EventArgs e)
        {
            int rulePointID = GetKPIEmployeePointID();
            if (rulePointID <= 0) return;

            int sessionID = TextUtils.ToInt(cboKPISession.EditValue);
            int kpiRuleID = TextUtils.ToInt(grvRuleData.GetFocusedRowCellValue("ID"));
            int empID = TextUtils.ToInt(grvEmpData.GetFocusedRowCellValue("EmployeeID"));
            string empName = TextUtils.ToString(grvEmpData.GetFocusedRowCellValue(colEmployeeName));

            KPIEmployeePointModel model = SQLHelper<KPIEmployeePointModel>.FindByID(rulePointID);
            if (model.ID <= 0)
            {
                MessageBox.Show($"Không tìm thấy Tổng hợp đánh giá của nhân viên [{empName}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmKPISumarizeRuleDetails frm = new frmKPISumarizeRuleDetails();
            frm.kpiEmpPoint = model;
            frm.kpiSessionID = sessionID;
            frm.kpiRuleID = kpiRuleID;
            frm.empID = empID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                int rowHandle = grvEmpData.FocusedRowHandle;
                LoadEmployee();
                grvEmpData.FocusedRowHandle = rowHandle;
            }
        }
        private int GetKPIEmployeePointID()
        {
            int rulePointID = TextUtils.ToInt(grvEmpData.GetFocusedRowCellValue(colKPIEmployeePointID));
            if (rulePointID > 0) return rulePointID;
            int empID = TextUtils.ToInt(grvEmpData.GetFocusedRowCellValue(colEmployeeID));
            string empName = TextUtils.ToString(grvEmpData.GetFocusedRowCellValue(colEmployeeName));

            int ruleID = TextUtils.ToInt(grvRuleData.GetFocusedRowCellValue(colKPIRuleID));
            if (empID <= 0)
            {
                MessageBox.Show($"Không tìm thấy ID của nhân viên [{empName}]", "Thông báo");
                return -1;
            }
            if (ruleID <= 0)
            {
                MessageBox.Show($"Không tìm thấy ID của rule đánh giá! Vui lòng kiểm tra lại", "Thông báo");
                return -1;
            }
            Expression ex1 = new Expression("EmployeeID", empID);
            Expression ex2 = new Expression("KPIEvaluationRuleID", ruleID);
            Expression ex3 = new Expression("IsDelete", 0);
            KPIEmployeePointModel model = SQLHelper<KPIEmployeePointModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPIEmployeePointModel();
            model.EmployeeID = empID;
            model.KPIEvaluationRuleID = ruleID;
            model.Status = 1;
            return model.ID > 0 ? model.ID : SQLHelper<KPIEmployeePointModel>.Insert(model).ID;
        }

        private void treeData_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                return;
            }
            //else
            //{
            //    bool isStyle = e.Column == colTBPPoint || e.Column == colEmployeePoint || e.Column == colBGDPoint
            //                    || e.Column == colTree2TBPPoint || e.Column == colTree2EmployeePoint || e.Column == colTree2BGDPoint;
            //    if (isStyle)
            //    {
            //        e.Appearance.BackColor = Color.LightYellow;
            //    }
            //}
        }

        private void treeData_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            bool isStyle = e.Column == colMaxPercent || e.Column == colPercentageAdjustment || e.Column == colMaxPercentageAdjustment || e.Column == colFirstMonth || e.Column == colSecondMonth ||
                           e.Column == colThirdMonth || e.Column == colTotalError || e.Column == colPercentBonus || e.Column == colPercentRemaining;
            if (isStyle)
            {
                if (TextUtils.ToDecimal(e.Value) == 0)
                {
                    e.DisplayText = "";
                }
            }
        }

        private void cboUserTeam_EditValueChanged(object sender, EventArgs e)
        {
            LoadKpiRule();
        }

        private void grvEmpData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int status = TextUtils.ToInt(grvEmpData.GetRowCellValue(e.RowHandle, colStatus));
            if (status == 1)
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
            else if (status == 2)
            {
                e.Appearance.BackColor = Color.LightGreen;

            }
        }

        private void cboDepartMent_EditValueChanged(object sender, EventArgs e)
        {
            LoadKpiRule();
        }

        private void cboStatus_EditValueChanged(object sender, EventArgs e)
        {
            LoadKpiRule();
        }

        private void CalculatorTotalPoint()
        {
            decimal totalPercent = TextUtils.ToDecimal(treeData.GetSummaryValue(colPercentRemaining));
            txtTotalPercent.Text = TextUtils.ToString(totalPercent);

            if (totalPercent < 60) txtKPIType.Text = "D";
            else if (totalPercent >= 60 && totalPercent < 65) txtKPIType.Text = "C-";
            else if (totalPercent >= 65 && totalPercent < 70) txtKPIType.Text = "C";
            else if (totalPercent >= 70 && totalPercent < 75) txtKPIType.Text = "C+";
            else if (totalPercent >= 75 && totalPercent < 80) txtKPIType.Text = "B-";
            else if (totalPercent >= 80 && totalPercent < 85) txtKPIType.Text = "B";
            else if (totalPercent >= 85 && totalPercent < 90) txtKPIType.Text = "B+";
            else if (totalPercent >= 90 && totalPercent < 95) txtKPIType.Text = "A-";
            else if (totalPercent >= 95 && totalPercent < 100) txtKPIType.Text = "A";
            else if (totalPercent >= 100) txtKPIType.Text = "A+";
        }

        private void btnRefreshData_Click(object sender, EventArgs e)
        {

        }
    }
}
