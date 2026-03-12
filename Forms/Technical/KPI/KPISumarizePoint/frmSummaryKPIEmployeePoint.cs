using BaseBusiness.DTO;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DocumentFormat.OpenXml.Bibliography;
using Forms.Employee;
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
using static DevExpress.Utils.Svg.CommonSvgImages;
using DevExpress.Charts.Native;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;

namespace BMS
{
    public partial class frmSummaryKPIEmployeePoint : _Forms
    {
        public int departmentID = 0;
        public string deName;
        int[] _departmentCoKhiLRs = new int[] { 10, 23 };

        DataTable dtSummary = new DataTable();
        public frmSummaryKPIEmployeePoint()
        {
            InitializeComponent();
        }

        private void frmSummaryKPIEmployeePoint_Load(object sender, EventArgs e)
        {
            this.Text += " - " + deName;
            txtYear.Value = DateTime.Now.Year;
            txtYear.Minimum = DateTime.MinValue.Year;
            txtQuarter.Value = (DateTime.Now.Month - 1) / 3 + 1;


            if (_departmentCoKhiLRs.Contains(departmentID))
            {
                foreach (BarItem item in barManager1.Items)
                {
                    if (item == btnExportExcel) continue;
                    item.Visibility = BarItemVisibility.Never;
                }
            }

            LoadDepartment();
            LoadEmployee();
            LoadData();
        }

        void LoadData()
        {
            int year = TextUtils.ToInt(txtYear.Value);
            int quarter = TextUtils.ToInt(txtQuarter.Value);
            departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            grdData.DataSource = null;

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
            {

                if (_departmentCoKhiLRs.Contains(departmentID))
                {
                    dtSummary = TextUtils.LoadDataFromSP("spGetKPISumaryEvaluation", "A",
                                                   new string[] { "@Year", "@Quarter", "@DepartmentID", "@EmployeeID", "@Keyword" },
                                                   new object[] { year, quarter, departmentID, employeeID, txtKeyword.Text.Trim() });
                }
                else
                {
                    dtSummary = TextUtils.LoadDataFromSP("spGetSummaryKPIEmployeePoint", "A",
                                                   new string[] { "@Year", "@Quarter", "@DepartmentID", "@EmployeeID", "@Keyword" },
                                                   new object[] { year, quarter, departmentID, employeeID, txtKeyword.Text.Trim() });

                    //LoadKPIRule(dtSummary);
                }

                grdData.DataSource = dtSummary;

            }
        }

        private void LoadKPIRule(DataTable dt)
        {
            int empPointID = 0;
            try
            {
                int year = TextUtils.ToInt(txtYear.Value);
                int quarter = TextUtils.ToInt(txtQuarter.Value);

                var exp1 = new Expression(KPISessionModel_Enum.YearEvaluation, year);
                var exp2 = new Expression(KPISessionModel_Enum.QuarterEvaluation, quarter);

                KPISessionModel session = SQLHelper<KPISessionModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault() ?? new KPISessionModel();

                foreach (DataRow row in dt.Rows)
                {
                    int employeeID = TextUtils.ToInt(row[colID.FieldName]);
                    int departmentID = TextUtils.ToInt(row["DepartmentID"]);
                    //if (departmentID == departmentIDCoKhi) continue;
                    if (_departmentCoKhiLRs.Contains(departmentID)) continue;
                    //if (employeeID != 130) continue;


                    //int kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue);
                    //int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
                    //empID = 55;
                    KPIPositionEmployeeModel positionEmp = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", employeeID).FirstOrDefault() ?? new KPIPositionEmployeeModel();
                    //if (positionEmp == null)
                    //{
                    //    DataTable dt = new DataTable();
                    //    grdTeam.DataSource = dt;
                    //    treeList3.DataSource = dt;
                    //    return;
                    //}


                    Expression ex1 = new Expression("KPISessionID", session.ID);
                    Expression ex2 = new Expression("KPIPositionID", TextUtils.ToInt(positionEmp.KPIPosiotionID) > 0 ? TextUtils.ToInt(positionEmp.KPIPosiotionID) : 1);
                    Expression ex3 = new Expression("IsDeleted", 0);
                    KPIEvaluationRuleModel kpiRule = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPIEvaluationRuleModel();
                    //if (kpiRule == null)
                    //{
                    //    DataTable dt = new DataTable();
                    //    grdTeam.DataSource = dt;
                    //    treeList3.DataSource = dt;
                    //    return;
                    //}

                    //int empPointID = GetKPIEmployeePointID(kpiRule.ID);
                    empPointID = TextUtils.ToInt(row[colKPIEmployeePointID.FieldName]);

                    if (empPointID <= 0) continue;
                    //if (positionEmp.KPIPosiotionID > 4)
                    //{
                    //    DataTable dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeam", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
                    //    //DataTable dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeam_test", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
                    //    grdTeam.DataSource = dtTeam;
                    //}


                    //DataTable dtKpiRule = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointID", "LMKTable",
                    //                                                new string[] { "@KPIEmployeePointID" },
                    //                                                new object[] { empPointID });

                    DataTable dtKpiRule = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointIDNew", "spGetEmployeeRulePointByKPIEmpPointIDNew",
                                    new string[] { "@KPIEmployeePointID" },
                                    new object[] { empPointID });
                    //treeList3.DataSource = dtKpiRule;
                    //treeList3.ExpandAll();
                    // ========================= 09/12/2024 =======================================================
                    List<KPIEmployeePointDetailModel> lst = SQLHelper<KPIEmployeePointDetailModel>.FindByAttribute("KPIEmployeePointID", empPointID);
                    bool isAdminConfirm = TextUtils.ToBoolean(row[colIsAdminConfirm.FieldName]);
                    if (lst.Count <= 0)
                    {
                        //using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
                        {
                            LoadPointRule(empPointID, dtKpiRule);
                        }
                    }
                    else if (!isAdminConfirm)
                    {
                        using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
                        {
                            LoadPointRuleLastMonth(empPointID, dtKpiRule);
                        }
                    }
                    CalculatorPoint(empPointID, dtKpiRule);
                    //treeList3.RefreshDataSource();

                    var dataRow = dtKpiRule.Select($"ParentID = 0").AsEnumerable().Select(x => x.Field<decimal?>("PercentRemaining")).ToArray();
                    decimal totalPercent = TextUtils.ToDecimal(row[colTotalPercent.FieldName]);
                    totalPercent = totalPercent <= 0 ? TextUtils.ToDecimal(dataRow.Sum()) : totalPercent;
                    totalPercent = TextUtils.ToDecimal(dataRow.Sum());
                    row[colTotalPercent.FieldName] = totalPercent;

                    if (totalPercent < 60) row[colTotalPercentText.FieldName] = "D";
                    else if (totalPercent >= 60 && totalPercent < 65) row[colTotalPercentText.FieldName] = "C-";
                    else if (totalPercent >= 65 && totalPercent < 70) row[colTotalPercentText.FieldName] = "C";
                    else if (totalPercent >= 70 && totalPercent < 75) row[colTotalPercentText.FieldName] = "C+";
                    else if (totalPercent >= 75 && totalPercent < 80) row[colTotalPercentText.FieldName] = "B-";
                    else if (totalPercent >= 80 && totalPercent < 85) row[colTotalPercentText.FieldName] = "B";
                    else if (totalPercent >= 85 && totalPercent < 90) row[colTotalPercentText.FieldName] = "B+";
                    else if (totalPercent >= 90 && totalPercent < 95) row[colTotalPercentText.FieldName] = "A-";
                    else if (totalPercent >= 95 && totalPercent < 100) row[colTotalPercentText.FieldName] = "A";
                    else if (totalPercent >= 100) row[colTotalPercentText.FieldName] = "A+";

                    dtKpiRule.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{empPointID} {ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }

        }


        private void LoadPointRuleLastMonth(int empPointID, DataTable dt)
        {
            try
            {
                //List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointID",
                //                                                       new string[] { "@KPIEmployeePointID" },
                //                                                        new object[] { empPointID });

                List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointIDNew",
                                                                       new string[] { "@KPIEmployeePointID" },
                                                                       new object[] { empPointID });

                //if (empPointID != 2553) return;
                //var dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeam","A", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID});
                var dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeamNew", "A", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });

                //var d = dtTeam.AsEnumerable().Where(x => !x.IsNull("KPIKyNang"));

                //decimal timeWork = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable().Where(x => !x.IsNull("TimeWork")).Average(x => x.Field<decimal>("TimeWork"));// TextUtils.ToDecimal(grvTeam.Columns["TimeWork"].SummaryItem.SummaryValue);
                decimal timeWork = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable()
                                                                            .Where(x => !x.IsNull("TimeWork"))
                                                                            .Select(x => x.Field<decimal>("TimeWork"))
                                                                            .DefaultIfEmpty(0)
                                                                            .Average();

                //decimal fiveS = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable().Where(x => !x.IsNull("FiveS")).Average(x => x.Field<decimal>("FiveS"));//TextUtils.ToDecimal(grvTeam.Columns["FiveS"].SummaryItem.SummaryValue);
                decimal fiveS = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable()
                                                                            .Where(x => !x.IsNull("FiveS"))
                                                                            .Select(x => x.Field<decimal>("FiveS"))
                                                                            .DefaultIfEmpty(0)
                                                                            .Average();

                //decimal reportWork = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable().Where(x => !x.IsNull("ReportWork")).Average(x => x.Field<decimal>("ReportWork"));//TextUtils.ToDecimal(grvTeam.Columns["ReportWork"].SummaryItem.SummaryValue);
                decimal reportWork = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable()
                                                                            .Where(x => !x.IsNull("ReportWork"))
                                                                            .Select(x => x.Field<decimal>("ReportWork"))
                                                                            .DefaultIfEmpty(0)
                                                                            .Average();

                //decimal customerComplaint = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable().Where(x => !x.IsNull("CustomerComplaint")).Average(x => x.Field<decimal>("CustomerComplaint"));// TextUtils.ToDecimal(grvTeam.Columns["CustomerComplaint"].SummaryItem.SummaryValue);
                //decimal customerComplaint = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable().Where(x => !x.IsNull("ComplaneAndMissing")).Average(x => x.Field<decimal>("ComplaneAndMissing"));// TextUtils.ToDecimal(grvTeam.Columns["CustomerComplaint"].SummaryItem.SummaryValue);
                decimal customerComplaint = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable()
                                                                            .Where(x => !x.IsNull("ComplaneAndMissing"))
                                                                            .Select(x => x.Field<decimal>("ComplaneAndMissing"))
                                                                            .DefaultIfEmpty(0)
                                                                            .Average();

                //decimal deadlineDelay = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable().Where(x => !x.IsNull("DeadlineDelay")).Average(x => x.Field<decimal>("DeadlineDelay"));// TextUtils.ToDecimal(grvTeam.Columns["DeadlineDelay"].SummaryItem.SummaryValue);
                decimal deadlineDelay = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable()
                                                                            .Where(x => !x.IsNull("DeadlineDelay"))
                                                                            .Select(x => x.Field<decimal>("DeadlineDelay"))
                                                                            .DefaultIfEmpty(0)
                                                                            .Average();

                //decimal teamKPIKyNang = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable().Where(x => !x.IsNull("KPIKyNang")).Average(x => x.Field<decimal>("KPIKyNang"));// TextUtils.ToDecimal(grvTeam.Columns["KPIKyNang"].SummaryItem.SummaryValue);
                decimal teamKPIKyNang = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable()
                                                                            .Where(x => !x.IsNull("KPIKyNang"))
                                                                            .Select(x => x.Field<decimal>("KPIKyNang"))
                                                                            .DefaultIfEmpty(0)
                                                                            .Average();// TextUtils.ToDecimal(grvTeam.Columns["KPIKyNang"].SummaryItem.SummaryValue);

                //decimal teanKPIChung = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable().Where(x => !x.IsNull("KPIChung")).Average(x => x.Field<decimal>("KPIChung"));// TextUtils.ToDecimal(grvTeam.Columns["KPIChung"].SummaryItem.SummaryValue);
                decimal teanKPIChung = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable()
                                                                            .Where(x => !x.IsNull("KPIChung"))
                                                                            .Select(x => x.Field<decimal>("KPIChung"))
                                                                            .DefaultIfEmpty(0)
                                                                            .Average();

                //decimal teamKPIPLC = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable().Where(x => !x.IsNull("KPIPLC")).Average(x => x.Field<decimal>("KPIPLC"));// TextUtils.ToDecimal(grvTeam.Columns["KPIPLC"].SummaryItem.SummaryValue);
                decimal teamKPIPLC = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable()
                                                                            .Where(x => !x.IsNull("KPIPLC"))
                                                                            .Select(x => x.Field<decimal>("KPIPLC"))
                                                                            .DefaultIfEmpty(0)
                                                                            .Average();

                //decimal teamKPIVISION = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable().Where(x => !x.IsNull("KPIVision")).Average(x => x.Field<decimal>("KPIVision"));// TextUtils.ToDecimal(grvTeam.Columns["KPIVision"].SummaryItem.SummaryValue);
                decimal teamKPIVISION = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable()
                                                                            .Where(x => !x.IsNull("KPIVision"))
                                                                            .Select(x => x.Field<decimal>("KPIVision"))
                                                                            .DefaultIfEmpty(0)
                                                                            .Average();

                //decimal teamKPISOFTWARE = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable().Where(x => !x.IsNull("KPISoftware")).Average(x => x.Field<decimal>("KPISoftware"));//TextUtils.ToDecimal(grvTeam.Columns["KPISoftware"].SummaryItem.SummaryValue);
                decimal teamKPISOFTWARE = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable()
                                                                            .Where(x => !x.IsNull("KPISoftware"))
                                                                            .Select(x => x.Field<decimal>("KPISoftware"))
                                                                            .DefaultIfEmpty(0)
                                                                            .Average();

                //decimal missingTool = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable().Where(x => !x.IsNull("MissingTool")).Average(x => x.Field<decimal>("MissingTool"));// TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);  //làm mất mát hỏng thiết bị 12/12/2024
                decimal missingTool = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable()
                                                                            .Where(x => !x.IsNull("MissingTool"))
                                                                            .Select(x => x.Field<decimal>("MissingTool"))
                                                                            .DefaultIfEmpty(0)
                                                                            .Average();

                //decimal teamKPIChuyenMon = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable().Where(x => !x.IsNull("KPIChuyenMon")).Average(x => x.Field<decimal>("KPIChuyenMon"));// TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);  Chuyên môn
                decimal teamKPIChuyenMon = dtTeam.Rows.Count <= 0 ? 0 : dtTeam.AsEnumerable()
                                                                            .Where(x => !x.IsNull("KPIChuyenMon"))
                                                                            .Select(x => x.Field<decimal>("KPIChuyenMon"))
                                                                            .DefaultIfEmpty(0)
                                                                            .Average();


                //================================== update 13/12/2024 ================================== 
                List<string> lstCodeTBP = new List<string>() { "MA03", "MA04", "NotWorking", "WorkLate" }; // MA011 Tổng số liệu thời gian đi làm ko đúng giờ + đi làm ko đủ công + L4 + L5
                var ltsMA11 = lstResult.Where(p => lstCodeTBP.Contains(p.EvaluationCode.Trim())).ToList();
                //decimal totalErrorTBP = lstResult.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);
                decimal totalErrorTBP = ltsMA11.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);
                //==========================================  END ==========================================
                lstResult.AddRange(new List<KPISumarizeDTO>
                {
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM01", ThirdMonth = timeWork},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM02", ThirdMonth = fiveS},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM03", ThirdMonth = reportWork},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM04", ThirdMonth = customerComplaint + missingTool + deadlineDelay},//update  12/12/2024
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM05", ThirdMonth = customerComplaint}, //update  12/12/2024
                    //new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = missingTool},//update  12/12/2024
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = deadlineDelay},//update  12/12/2024
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIKYNANG", ThirdMonth = teamKPIKyNang},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIChung", ThirdMonth = teanKPIChung},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIPLC", ThirdMonth = teamKPIPLC},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIVISION", ThirdMonth = teamKPIVISION},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPISOFTWARE", ThirdMonth = teamKPISOFTWARE},
                    new KPISumarizeDTO(){ EvaluationCode = "MA11", ThirdMonth = totalErrorTBP}, // update 13/12/2024
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPICHUYENMON", ThirdMonth = teamKPIChuyenMon},
                });


                Lib.LockEvents = true;
                foreach (KPISumarizeDTO item in lstResult)
                //foreach (DataRow row in dt.Rows)
                {
                    //TreeListNode node = treeList3.GetNodeList().FirstOrDefault(x => item.EvaluationCode == TextUtils.ToString(x.GetValue(colEvaluationCode)));

                    //if (node == null) continue;
                    //node.SetValue(colFirstMonth, item.FirstMonth);
                    //node.SetValue(colSecondMonth, item.SecondMonth);
                    //node.SetValue(colThirdMonth, item.ThirdMonth);

                    var row = dt.AsEnumerable().FirstOrDefault(x => x.Field<string>("EvaluationCode") == item.EvaluationCode);
                    if (row == null) continue;
                    row["ThirdMonth"] = item.ThirdMonth;
                    //row["ThirdMonth"] = 5;


                }

                Lib.LockEvents = false;

                //CalculatorPoint(empPointID);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void LoadPointRule(int empPointID, DataTable dataTable)
        {
            try
            {
                //List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointID",
                //                                                       new string[] { "@KPIEmployeePointID" },
                //                                                       new object[] { empPointID });


                List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointIDNew",
                                                                       new string[] { "@KPIEmployeePointID" },
                                                                       new object[] { empPointID });

                //decimal timeWork = TextUtils.ToDecimal(grvTeam.Columns["TimeWork"].SummaryItem.SummaryValue);
                //decimal fiveS = TextUtils.ToDecimal(grvTeam.Columns["FiveS"].SummaryItem.SummaryValue);
                //decimal reportWork = TextUtils.ToDecimal(grvTeam.Columns["ReportWork"].SummaryItem.SummaryValue);
                //decimal customerComplaint = TextUtils.ToDecimal(grvTeam.Columns["CustomerComplaint"].SummaryItem.SummaryValue);
                //decimal deadlineDelay = TextUtils.ToDecimal(grvTeam.Columns["DeadlineDelay"].SummaryItem.SummaryValue);
                //decimal teamKPIKyNang = TextUtils.ToDecimal(grvTeam.Columns["KPIKyNang"].SummaryItem.SummaryValue);
                //decimal teanKPIChung = TextUtils.ToDecimal(grvTeam.Columns["KPIChung"].SummaryItem.SummaryValue);
                //decimal teamKPIPLC = TextUtils.ToDecimal(grvTeam.Columns["KPIPLC"].SummaryItem.SummaryValue);
                //decimal teamKPIVISION = TextUtils.ToDecimal(grvTeam.Columns["KPIVision"].SummaryItem.SummaryValue);
                //decimal teamKPISOFTWARE = TextUtils.ToDecimal(grvTeam.Columns["KPISoftware"].SummaryItem.SummaryValue);
                //decimal missingTool = TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);  //làm mất mát hỏng thiết bị 12/12/2024


                //DataTable dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeam", "A", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
                DataTable dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeamNew", "A", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
                decimal timeWork = 0;
                decimal fiveS = 0;
                decimal reportWork = 0;
                decimal customerComplaint = 0;
                decimal deadlineDelay = 0;
                decimal teamKPIKyNang = 0;
                decimal teanKPIChung = 0;
                decimal teamKPIPLC = 0;
                decimal teamKPIVISION = 0;
                decimal teamKPISOFTWARE = 0;
                decimal missingTool = 0;
                if (dtTeam != null)
                {
                    var timeWorks = dtTeam.AsEnumerable().Select(x => x.Field<decimal?>("TimeWork")).ToList();
                    var fiveSs = dtTeam.AsEnumerable().Select(x => x.Field<decimal?>("FiveS")).ToList();
                    var reportWorks = dtTeam.AsEnumerable().Select(x => x.Field<decimal?>("ReportWork")).ToList();
                    var customerComplaints = dtTeam.AsEnumerable().Select(x => x.Field<decimal?>("CustomerComplaint")).ToList();
                    var deadlineDelays = dtTeam.AsEnumerable().Select(x => x.Field<decimal?>("DeadlineDelay")).ToList();
                    var teamKPIKyNangs = dtTeam.AsEnumerable().Select(x => x.Field<decimal?>("KPIKyNang")).ToList();
                    var teanKPIChungs = dtTeam.AsEnumerable().Select(x => x.Field<decimal?>("KPIChung")).ToList();
                    var teamKPIPLCs = dtTeam.AsEnumerable().Select(x => x.Field<decimal?>("KPIPLC")).ToList();
                    var teamKPIVISIONs = dtTeam.AsEnumerable().Select(x => x.Field<decimal?>("KPIVision")).ToList();
                    var teamKPISOFTWAREs = dtTeam.AsEnumerable().Select(x => x.Field<decimal?>("KPISoftware")).ToList();
                    var missingTools = dtTeam.AsEnumerable().Select(x => x.Field<decimal?>("MissingTool")).ToList();

                    timeWork = TextUtils.ToDecimal(timeWorks.Average());
                    fiveS = TextUtils.ToDecimal(fiveSs.Average());
                    reportWork = TextUtils.ToDecimal(reportWorks.Average());
                    customerComplaint = TextUtils.ToDecimal(customerComplaints.Average());
                    deadlineDelay = TextUtils.ToDecimal(deadlineDelays.Average());
                    teamKPIKyNang = TextUtils.ToDecimal(teamKPIKyNangs.Average());
                    teanKPIChung = TextUtils.ToDecimal(teanKPIChungs.Average());
                    teamKPIPLC = TextUtils.ToDecimal(teamKPIPLCs.Average());
                    teamKPIVISION = TextUtils.ToDecimal(teamKPIVISIONs.Average());
                    teamKPISOFTWARE = TextUtils.ToDecimal(teamKPISOFTWAREs.Average());
                    missingTool = TextUtils.ToDecimal(missingTools.Average());
                }





                //================================== update 13/12/2024 ================================== 
                List<string> lstCodeTBP = new List<string>() { "MA03", "MA04", "NotWorking", "WorkLate" }; // MA011 Tổng số liệu thời gian đi làm ko đúng giờ + đi làm ko đủ công + L4 + L5
                var ltsMA11 = lstResult.Where(p => lstCodeTBP.Contains(p.EvaluationCode.Trim())).ToList();
                //decimal totalErrorTBP = lstResult.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);
                decimal totalErrorTBP = ltsMA11.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);
                //==========================================  END ==========================================
                lstResult.AddRange(new List<KPISumarizeDTO>
                {
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM01", ThirdMonth = timeWork},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM02", ThirdMonth = fiveS},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM03", ThirdMonth = reportWork},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM04", ThirdMonth = customerComplaint + missingTool + deadlineDelay},//update  12/12/2024
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM05", ThirdMonth = customerComplaint}, //update  12/12/2024
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = missingTool},//update  12/12/2024
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIKYNANG", ThirdMonth = teamKPIKyNang},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIChung", ThirdMonth = teanKPIChung},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIPLC", ThirdMonth = teamKPIPLC},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIVISION", ThirdMonth = teamKPIVISION},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPISOFTWARE", ThirdMonth = teamKPISOFTWARE},
                    new KPISumarizeDTO(){ EvaluationCode = "MA11", ThirdMonth = totalErrorTBP}, // update 13/12/2024
                });


                Lib.LockEvents = true;
                foreach (KPISumarizeDTO item in lstResult)
                {
                    var rows = dataTable.Select($"EvaluationCode = '{item.EvaluationCode}'");
                    if (rows.Length <= 0) continue;
                    DataRow row = rows[0];
                    //if (node == null) continue;
                    row["FirstMonth"] = item.FirstMonth;
                    row["SecondMonth"] = item.SecondMonth;
                    row["ThirdMonth"] = item.ThirdMonth;
                }

                Lib.LockEvents = false;

                dataTable.AcceptChanges();
                //CalculatorPoint(empPointID);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        List<string> lstTeamTBP = new List<string>() { "TEAM01", "TEAM02", "TEAM03" }; //19/12/2024
        private void CalculatorPoint(int empPointID, DataTable dt)
        {
            //return;
            try
            {
                //================ update 12/12/2024 ====================================
                KPIEmployeePointModel empPointModel = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);
                KPIEvaluationRuleModel ruleModel = SQLHelper<KPIEvaluationRuleModel>.FindByID(TextUtils.ToInt(empPointModel.KPIEvaluationRuleID));
                bool isTBP = ruleModel.KPIPositionID == 5; // TBP
                //======================================================================
                Lib.LockEvents = true;
                CalculatorNoError(dt, TextUtils.ToInt(empPointModel.EmployeeID));
                //List<TreeListNode> lst = treeList3.GetNodeList();
                //for (int i = lst.Count - 1; i >= 0; i--)


                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    //if (i == 42)
                    //{
                    //    int a = 1;
                    //}
                    //TreeListNode row = lst[i];
                    DataRow row = dt.Rows[i];
                    if (row == null) continue;
                    string stt = TextUtils.ToString(row["STT"]);
                    string ruleCode = TextUtils.ToString(row["EvaluationCode"]).ToUpper();
                    bool isDiemThuong = ruleCode == "THUONG";

                    //if (!isDiemThuong) continue;

                    decimal maxPercentBonus = TextUtils.ToDecimal(row["MaxPercent"]);
                    decimal percentageAdjustment = TextUtils.ToDecimal(row["PercentageAdjustment"]);
                    decimal maxPercentageAdjustment = TextUtils.ToDecimal(row["MaxPercentageAdjustment"]);

                    int id = TextUtils.ToInt(row["ID"]);
                    //if (id == 354)
                    //{
                    //    int a = 0;
                    //}
                    DataRow[] rows = dt.Select($"ParentID = {id}");

                    //if (isDiemThuong)
                    //{
                    //    int b = 2;
                    //}
                    if (rows.Length > 0)
                    {
                        decimal totalPercentBonus = 0;
                        decimal totalPercentRemaining = 0;
                        bool isKPI = false;
                        foreach (DataRow childrenNode in rows)
                        {
                            string childRuleCode = TextUtils.ToString(childrenNode["EvaluationCode"]);
                            isKPI = childRuleCode.ToUpper().StartsWith("KPI");
                            totalPercentBonus += TextUtils.ToDecimal(childrenNode["PercentBonus"]);
                            totalPercentRemaining += TextUtils.ToDecimal(childrenNode["PercentRemaining"]);
                        }


                        if (lstTeamTBP.Contains(ruleCode) && isTBP) //Update 13/12/2024  Tính trực tiếp node cha bên PP
                        {
                            row["TotalError"] = TextUtils.ToDecimal(row["ThirdMonth"]);
                        }
                        else if (isKPI) // Tính tổng KPI lên node cha
                        {
                            row["PercentRemaining"] = totalPercentRemaining;
                        }
                        else if (isDiemThuong)
                        {
                            row["PercentRemaining"] = maxPercentBonus > totalPercentBonus ? totalPercentBonus : maxPercentBonus;
                        }
                        else if (maxPercentBonus > 0) row["PercentRemaining"] = maxPercentBonus > totalPercentBonus ? maxPercentBonus - totalPercentBonus : 0;
                        else
                        {
                            row["PercentBonus"] = totalPercentBonus;
                            row["PercentRemaining"] = totalPercentRemaining;
                        }

                        if (lstTeamTBP.Contains(ruleCode) && isTBP)//Update 13/12/2024 Tính % thưởng KPITeam PP
                        {
                            row["PercentBonus"] = TextUtils.ToDecimal(row["ThirdMonth"]) * percentageAdjustment > maxPercentageAdjustment ? maxPercentageAdjustment : TextUtils.ToDecimal(row["ThirdMonth"]) * percentageAdjustment;
                        }
                        else if (maxPercentageAdjustment > 0) row["PercentBonus"] = (maxPercentageAdjustment > totalPercentBonus ? totalPercentBonus : maxPercentageAdjustment);
                    }
                    else
                    {

                        decimal totalError = (TextUtils.ToDecimal(row["FirstMonth"]) + TextUtils.ToDecimal(row["SecondMonth"]) + TextUtils.ToDecimal(row["ThirdMonth"]));
                        row["TotalError"] = totalError;
                        if (ruleCode == "OT") row["TotalError"] = (totalError / 3) >= 20 ? 1 : 0;

                        decimal totalPercentDeduction = percentageAdjustment * TextUtils.ToDecimal(row["TotalError"]);
                        row["PercentBonus"] = maxPercentageAdjustment > 0 ? (totalPercentDeduction > maxPercentageAdjustment ? maxPercentageAdjustment : totalPercentDeduction) : totalPercentDeduction;


                        if ((ruleCode.StartsWith("KPI") && !(ruleCode == "KPINL" || ruleCode == "KPINQ")))
                        {
                            row["TotalError"] = TextUtils.ToDecimal(row["ThirdMonth"]);
                            row["PercentRemaining"] = TextUtils.ToDecimal(row["TotalError"]) * maxPercentBonus / 5;
                        }
                        else if (ruleCode.StartsWith("TEAMKPI"))
                        {
                            decimal test = TextUtils.ToDecimal(row["TotalError"]) * maxPercentageAdjustment / 5;
                            row["PercentBonus"] = TextUtils.ToDecimal(row["TotalError"]) * maxPercentageAdjustment / 5;
                        }
                        else if (ruleCode == "MA09")
                        {
                            //row["PercentBonus"] = totalPercentDeduction > maxPercentageAdjustment ? maxPercentageAdjustment : maxPercentageAdjustment - totalPercentDeduction;
                            row["PercentBonus"] = totalPercentDeduction > maxPercentageAdjustment ? 0 : maxPercentageAdjustment - totalPercentDeduction;
                        }
                        else
                        {
                            decimal test = TextUtils.ToDecimal(row["TotalError"]) * maxPercentBonus;
                            row["PercentRemaining"] = TextUtils.ToDecimal(row["TotalError"]) * maxPercentBonus;
                        }
                    }

                    dt.AcceptChanges();
                }

                //treeList3.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
            finally
            {
                Lib.LockEvents = false;
            }
        }
        string[] listCodes = new string[] { "MA01", "MA02", "MA03", "MA04", "MA05", "MA06", "MA07", "WorkLate", "NotWorking" };
        string[] listAdminCodes = new string[] { "AMA01", "AMA02", "AMA03", "AMA04", "AMA05", "AMA06", "AMA07", "AMA08", "AMA09", "AMA10", "AMA11", "AMA12", "AMA13", "AMA14", "AMA15", "AMA16", "AMA17", "AMA18", "AMA19", "WorkLate", "NotWorking" };
        private void CalculatorNoError(DataTable dt, int employeeID)
        {

            //DataTable dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeam","A", new string[] { "@KPIEmployeePointID" }, new object[] { kpiEmployeePointID });

            //string codeText = string.Join(",", listCodes);

            string codeText = "";
            foreach (string item in listCodes)
            {
                codeText += $"'{item}',";
            }
            codeText = codeText.Remove(codeText.Length - 1, 1);

            var list = dt.Select($"EvaluationCode IN ({codeText})");

            if (employeeID == 548) //Nếu là admin
            {
                foreach (string item in listAdminCodes)
                {
                    codeText += $"'{item}',";
                }
                codeText = codeText.Remove(codeText.Length - 1, 1);
                list = dt.Select($"EvaluationCode IN ({codeText})");
            }

            var listFirstMonth = list.AsEnumerable().Select(x => x.Field<decimal?>("FirstMonth")).ToList();
            var listSecondMonth = list.AsEnumerable().Select(x => x.Field<decimal?>("SecondMonth")).ToList();
            var listThirdMonth = list.AsEnumerable().Select(x => x.Field<decimal?>("ThirdMonth")).ToList();

            decimal firstMonth = TextUtils.ToDecimal(listFirstMonth.Sum());
            decimal secondMonth = TextUtils.ToDecimal(listSecondMonth.Sum());
            decimal thirdMonth = TextUtils.ToDecimal(listThirdMonth.Sum());

            var rows = dt.Select("EvaluationCode = 'MA09'");
            if (rows.Length <= 0) return;
            rows[0]["FirstMonth"] = firstMonth;
            rows[0]["SecondMonth"] = secondMonth;
            rows[0]["ThirdMonth"] = thirdMonth;


            //node.SetValue(colFirstMonth, firstMonth);
            //node.SetValue(colSecondMonth, secondMonth);
            //node.SetValue(colThirdMonth, thirdMonth);

            //var list = treeList3.GetNodeList().Where(x => listCodes.Contains(x.GetValue(colEvaluationCode)));

            //decimal firstMonth = list.Sum(x => TextUtils.ToDecimal(x.GetValue(colFirstMonth)));
            //decimal secondMonth = list.Sum(x => TextUtils.ToDecimal(x.GetValue(colSecondMonth)));
            //decimal thirdMonth = list.Sum(x => TextUtils.ToDecimal(x.GetValue(colThirdMonth)));

            //var node = treeList3.FindNodeByFieldValue(colEvaluationCode.FieldName, "MA09");
            //if (node == null) return;


            //node.SetValue(colFirstMonth, firstMonth);
            //node.SetValue(colSecondMonth, secondMonth);
            //node.SetValue(colThirdMonth, thirdMonth);

        }


        void LoadDetail(DataTable dt)
        {
            int year = TextUtils.ToInt(txtYear.Value);
            int quarter = TextUtils.ToInt(txtQuarter.Value);

            var exp1 = new Expression(KPISessionModel_Enum.YearEvaluation, year);
            var exp2 = new Expression(KPISessionModel_Enum.QuarterEvaluation, quarter);

            KPISessionModel session = SQLHelper<KPISessionModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault() ?? new KPISessionModel();

            foreach (DataRow row in dt.Rows)
            {
                int employeeID = TextUtils.ToInt(row[colID.FieldName]);
                if (employeeID != 282) continue;
                KPIPositionEmployeeModel position1 = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", employeeID).FirstOrDefault() ?? new KPIPositionEmployeeModel();
                int positionID = position1.KPIPosiotionID > 0 ? TextUtils.ToInt(position1.KPIPosiotionID) : 1; // 1: kỹ thuật;


                Expression exFindRule1 = new Expression(KPIEvaluationRuleModel_Enum.KPIPositionID.ToString(), positionID);
                Expression exFindRule2 = new Expression(KPIEvaluationRuleModel_Enum.KPISessionID.ToString(), session.ID);
                KPIEvaluationRuleModel ruleModel = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(exFindRule1.And(exFindRule2)).FirstOrDefault() ?? new KPIEvaluationRuleModel();
                if (ruleModel.ID <= 0) continue;

                Expression ex1 = new Expression("EmployeeID", employeeID);
                Expression ex2 = new Expression("KPIEvaluationRuleID", ruleModel.ID);
                Expression ex3 = new Expression("IsDelete", 0);
                KPIEmployeePointModel empPoint = SQLHelper<KPIEmployeePointModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPIEmployeePointModel();
                if (empPoint.ID <= 0) continue;

                //if (!frm.lstEmpChose.Contains(emp))
                //{
                //    empPoint.IsDelete = true;
                //    SQLHelper<KPIEmployeePointModel>.Update(empPoint);
                //    continue;
                //}

                empPoint.IsDelete = false;
                SQLHelper<KPIEmployeePointModel>.Update(empPoint);

                //DataTable dtKpiRule = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointID", "LMKTable",
                //                                                new string[] { "@KPIEmployeePointID" },
                //                                                new object[] { empPoint.ID });

                DataTable dtKpiRule = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointIDNew", "spGetEmployeeRulePointByKPIEmpPointIDNew",
                                    new string[] { "@KPIEmployeePointID" },
                                    new object[] { empPoint.ID });
                if (dtKpiRule.Rows.Count <= 0) continue;
                //SaveDataDetails(LoadDataView(empPoint.ID, dtKpiRule), empPoint.ID);
            }
        }

        private DataTable LoadDataView(int empPointID, DataTable dt)
        {
            try
            {
                //List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointID",
                //                                                       new string[] { "@KPIEmployeePointID" },
                //                                                       new object[] { empPointID });

                List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointIDNew",
                                                                       new string[] { "@KPIEmployeePointID" },
                                                                       new object[] { empPointID });

                //decimal timeWork = TextUtils.ToDecimal(grvTeam.Columns["TimeWork"].SummaryItem.SummaryValue);
                //decimal fiveS = TextUtils.ToDecimal(grvTeam.Columns["FiveS"].SummaryItem.SummaryValue);
                //decimal reportWork = TextUtils.ToDecimal(grvTeam.Columns["ReportWork"].SummaryItem.SummaryValue);
                //decimal customerComplaint = TextUtils.ToDecimal(grvTeam.Columns["CustomerComplaint"].SummaryItem.SummaryValue);
                //decimal deadlineDelay = TextUtils.ToDecimal(grvTeam.Columns["DeadlineDelay"].SummaryItem.SummaryValue);
                //decimal teamKPIKyNang = TextUtils.ToDecimal(grvTeam.Columns["KPIKyNang"].SummaryItem.SummaryValue);
                //decimal teanKPIChung = TextUtils.ToDecimal(grvTeam.Columns["KPIChung"].SummaryItem.SummaryValue);
                //decimal teamKPIPLC = TextUtils.ToDecimal(grvTeam.Columns["KPIPLC_Robot"].SummaryItem.SummaryValue);
                //decimal teamKPIVISION = TextUtils.ToDecimal(grvTeam.Columns["KPIVision"].SummaryItem.SummaryValue);
                //decimal teamKPISOFTWARE = TextUtils.ToDecimal(grvTeam.Columns["KPISoftware"].SummaryItem.SummaryValue);
                //decimal missingTool = TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);  //làm mất mát hỏng thiết bị 12/12/2024
                //================================== update 13/12/2024 ================================== 
                //List<string> lstCodeTBP = new List<string>() { "MA03", "MA04", "NotWorking", "WorkLate" }; // MA011 Tổng số liệu thời gian đi làm ko đúng giờ + đi làm ko đủ công + L4 + L5
                //var ltsMA11 = lstResult.Where(p => lstCodeTBP.Contains(p.EvaluationCode.Trim())).ToList();
                ////decimal totalErrorTBP = lstResult.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);
                //decimal totalErrorTBP = ltsMA11.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);
                //==========================================  END ==========================================
                //lstResult.AddRange(new List<KPISumarizeDTO>
                //{
                //    new KPISumarizeDTO(){ EvaluationCode = "TEAM01", ThirdMonth = timeWork},
                //    new KPISumarizeDTO(){ EvaluationCode = "TEAM02", ThirdMonth = fiveS},
                //    new KPISumarizeDTO(){ EvaluationCode = "TEAM03", ThirdMonth = reportWork},
                //   new KPISumarizeDTO(){ EvaluationCode = "TEAM04", ThirdMonth = customerComplaint + missingTool + deadlineDelay},//update  12/12/2024
                //    new KPISumarizeDTO(){ EvaluationCode = "TEAM05", ThirdMonth = customerComplaint}, //update  12/12/2024
                //    new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = missingTool},//update  12/12/2024
                //    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIKYNANG", ThirdMonth = teamKPIKyNang},
                //    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIChung", ThirdMonth = teanKPIChung},
                //    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIPLC", ThirdMonth = teamKPIPLC},
                //    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIVISION", ThirdMonth = teamKPIVISION},
                //    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPISOFTWARE", ThirdMonth = teamKPISOFTWARE},
                //    new KPISumarizeDTO(){ EvaluationCode = "MA11", ThirdMonth = totalErrorTBP}, // update 13/12/2024
                //});


                foreach (KPISumarizeDTO item in lstResult)
                {
                    DataRow[] rows = dt.Select($"EvaluationCode = '{item.EvaluationCode}'");
                    if (rows.Length <= 0) continue;
                    DataRow row = rows[0];
                    row["FirstMonth"] = item.FirstMonth;
                    row["SecondMonth"] = item.SecondMonth;
                    row["ThirdMonth"] = item.ThirdMonth;
                }
                dt.AcceptChanges();
                return dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return dt;
            }
        }

        private void SaveDataDetails(DataTable dt, int empPointID)
        {
            try
            {
                List<KPIEmployeePointDetailModel> lstDetails = SQLHelper<KPIEmployeePointDetailModel>.FindByAttribute("KPIEmployeePointID", empPointID);
                if (lstDetails.Count > 0) return;

                foreach (DataRow row in dt.Rows)
                {
                    KPIEmployeePointDetailModel detail = new KPIEmployeePointDetailModel();
                    detail.KPIEmployeePointID = empPointID;
                    detail.KPIEvaluationRuleDetailID = TextUtils.ToInt(row["ID"]);
                    detail.FirstMonth = TextUtils.ToDecimal(row["FirstMonth"]);
                    detail.SecondMonth = TextUtils.ToDecimal(row["SecondMonth"]);
                    detail.ThirdMonth = TextUtils.ToDecimal(row["ThirdMonth"]);
                    detail.PercentBonus = TextUtils.ToDecimal(row["PercentBonus"]);
                    detail.PercentRemaining = TextUtils.ToDecimal(row["PercentRemaining"]);
                    SQLHelper<KPIEmployeePointDetailModel>.Insert(detail);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");

            }
        }

        void LoadDepartment()
        {
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();

            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = list;

            cboDepartment.EditValue = departmentID;
        }

        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }


        void PublishEvaluate(bool isPublish)
        {
            string isPublishText = isPublish ? "duyệt đánh giá" : "hủy duyệt đánh giá";

            int[] selectedRows = grvData.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn nhân viên muốn {isPublishText}!", "Thông báo");
                return;
            }

            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn {isPublishText} danh sách đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                List<int> ids = new List<int>();

                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colKPIEmployeePointID));

                    if (id <= 0) continue;

                    ids.Add(id);
                }

                if (ids.Count <= 0) return;

                var myDict = new Dictionary<string, object>
                {
                    {KPIEmployeePointModel_Enum.IsPublish.ToString(),isPublish },
                    {KPIEmployeePointModel_Enum.UpdatedBy.ToString(),Global.AppCodeName },
                    {KPIEmployeePointModel_Enum.UpdatedDate.ToString(),DateTime.Now.ToString() },
                };

                var exp = new Expression(KPIEmployeePointModel_Enum.ID.ToString(), string.Join(",", ids), "IN");
                SQLHelper<KPIEmployeePointModel>.UpdateFields(myDict, exp);
                LoadData();
            }
        }


        private DataTable GetDataRanking()
        {
            DataTable dt = new DataTable();

            // Khởi tạo schema cột
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("TotalPercentActual", typeof(decimal));
            dt.Columns.Add("TotalPercentActualText", typeof(string));
            dt.Columns.Add("TotalPercent", typeof(decimal));
            dt.Columns.Add("TotalPercentText", typeof(string));
            dt.Columns.Add("DepartmentName", typeof(string));

            for (int i = 0; i < grvData.RowCount; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow["FullName"] = TextUtils.ToString(grvData.GetRowCellValue(i, colFullName));
                newRow["Code"] = TextUtils.ToString(grvData.GetRowCellValue(i, colCode));
                newRow["TotalPercentActual"] = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPercentActual));
                newRow["TotalPercentActualText"] = TextUtils.ToString(grvData.GetRowCellValue(i, colTotalPercentTextActual));
                newRow["TotalPercent"] = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPercent));
                newRow["TotalPercentText"] = TextUtils.ToString(grvData.GetRowCellValue(i, colTotalPercentText));
                newRow["DepartmentName"] = TextUtils.ToString(grvData.GetRowCellValue(i, colDepartmentName));
                dt.Rows.Add(newRow);
            }



            return dt;
        }

        private DataTable GetKpiLevelSummary(DataTable tempKPI)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("KPILevel", typeof(string));
            dtResult.Columns.Add("SortOrder", typeof(int));
            dtResult.Columns.Add("SoLuongExpected", typeof(int));
            dtResult.Columns.Add("SoLuongActual", typeof(int));

            Dictionary<string, int> dicLevel = new Dictionary<string, int>
               {
                   { "A+", 1 },
                   { "A", 2 },
                   { "A-", 3 },
                   { "B+", 4 },
                   { "B", 5 },
                   { "B-", 6 },
                   { "C+", 7 },
                   { "C", 8 },
                   { "C-", 9 },
                   { "D", 10 }
               };

            foreach (var level in dicLevel)
            {
                string kpiLevel = level.Key;
                int sortOrder = level.Value;

                int expectedCount = tempKPI.Select($"TotalPercentText = '{kpiLevel}'").Length;   // KPILevel_Expected
                int actualCount = tempKPI.Select($"TotalPercentActualText = '{kpiLevel}'").Length; // KPILevel_Actual

                DataRow row = dtResult.NewRow();
                row["KPILevel"] = kpiLevel;
                row["SortOrder"] = sortOrder;
                row["SoLuongExpected"] = expectedCount;
                row["SoLuongActual"] = actualCount;

                dtResult.Rows.Add(row);
            }

            return dtResult;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnPublish_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PublishEvaluate(true);
        }

        private void btnUnPublish_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PublishEvaluate(false);
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"TongHopDanhGiaKPI_{txtYear.Value}-Q{txtQuarter.Value}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = f.FileName;

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                //optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;

                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            //int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (empID <= 0) return;

            //frmKPIEvaluationEmployee frm = new frmKPIEvaluationEmployee();
            //frm.isTBPView = true;
            //frm.employeeID = empID;
            //frm.year = TextUtils.ToInt(txtYear.Value);
            //frm.quarter = TextUtils.ToInt(txtQuarter.Value);
            //frm.employeeName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            //frm.Show();



            //int rowhandle = grvData.FocusedRowHandle;
            int empId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (empId <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo");
                return;
            }
            int kpiExamID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colKPIExamID));
            frmKPIEvaluationFactorScoringDetails frm = new frmKPIEvaluationFactorScoringDetails();
            departmentID = 2;
            frm._departmentID = departmentID; //--160525-- lee min khooi-- update
            frm.employeeID = empId;
            frm.kpiExam = SQLHelper<KPIExamModel>.FindByID(kpiExamID);
            frm.isAdminConfirm = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsAdminConfirm));
            if (frm.kpiExam.ID <= 0)
            {
                MessageBox.Show("Bài đánh giá không hợp lệ! Hãy chọn lại bài đánh giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frm.typePoint = 3;
            foreach (ToolStripItem item in frm.mnuMenu.Items)
            {
                item.Enabled = (Global.IsAdmin && Global.EmployeeID <= 0);
            }
            frm.Show();

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    isUpdate = false;
            //    LoadEmployee();
            //    grvData.FocusedRowHandle = rowhandle;
            //}
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //var view = sender as GridView;
            //if (view.FocusedRowHandle == e.RowHandle)
            //{
            //    e.Appearance.BackColor = Color.LightYellow;
            //    //e.HighPriority = true;
            //}


            var view = sender as GridView;

            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow; //focus dòng 
                //e.HighPriority = true;
            }
            // TN.Binh update 04/10/25
            string totalPercent = view.GetRowCellDisplayText(e.RowHandle, colTotalPercentText.FieldName)?.Trim().ToUpper();
            string totalPercentActual = view.GetRowCellDisplayText(e.RowHandle, colTotalPercentTextActual.FieldName)?.Trim().ToUpper();

            if (!string.Equals(totalPercent, totalPercentActual))//so sánh điểm xếp loại nếu khác nhau
            {
                e.Appearance.BackColor = Color.LightBlue; 
                //e.HighPriority = true;
            }
            //endupdate
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void btnSaveData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvData.CloseEditor();
            grvData.FocusedRowHandle = -1;

            DataTable dtChange = dtSummary.GetChanges();
            if (dtChange == null) return;
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn lưu điểm các dòng đã sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                foreach (DataRow row in dtChange.Rows)
                {
                    int id = TextUtils.ToInt(row[colKPIEmployeePointID.FieldName]);
                    if (id <= 0) continue;
                    decimal totalPercent = TextUtils.ToDecimal(row[colTotalPercentActual.FieldName]);

                    var myDict = new Dictionary<string, object>()
                    {
                        {KPIEmployeePointModel_Enum.TotalPercentActual.ToString(),totalPercent },
                        {KPIEmployeePointModel_Enum.UpdatedBy.ToString(),Global.AppUserName },
                        {KPIEmployeePointModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                    };

                    SQLHelper<KPIEmployeePointModel>.UpdateFieldsByID(myDict, id);
                }

                LoadData();
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column != colTotalPercentActual) return;

            grvData.CloseEditor();
            grvData.FocusedRowHandle = -1;

            decimal totalPercent = TextUtils.ToDecimal(e.Value);
            string totalPercentText = "";

            if (totalPercent < 60) totalPercentText = "D";
            else if (totalPercent >= 60 && totalPercent < 65) totalPercentText = "C-";
            else if (totalPercent >= 65 && totalPercent < 70) totalPercentText = "C";
            else if (totalPercent >= 70 && totalPercent < 75) totalPercentText = "C+";
            else if (totalPercent >= 75 && totalPercent < 80) totalPercentText = "B-";
            else if (totalPercent >= 80 && totalPercent < 85) totalPercentText = "B";
            else if (totalPercent >= 85 && totalPercent < 90) totalPercentText = "B+";
            else if (totalPercent >= 90 && totalPercent < 95) totalPercentText = "A-";
            else if (totalPercent >= 95 && totalPercent < 100) totalPercentText = "A";
            else if (totalPercent >= 100) totalPercentText = "A+";

            grvData.SetRowCellValue(e.RowHandle, colTotalPercentTextActual, totalPercentText);
        }

        private void btnRanking_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DataTable tempKPI = getDataRanking(); // #TempKPI

            //frmKPIRanking frm = new frmKPIRanking();
            //frm.dtData = tempKPI;
            //frm.dtSummary = GetKpiLevelSummary(tempKPI); // KPILevel, Expected, Actual
            //frm.year = TextUtils.ToInt(txtYear.Value);
            //frm.quarter = TextUtils.ToInt(txtQuarter.Value);
            //frm.departmentID = TextUtils.ToInt(cboDepartment.EditValue);

            //frm.Show();

            DataTable tempKPI = GetDataRanking();
            DataTable summary = GetKpiLevelSummary(tempKPI);
            int year = TextUtils.ToInt(txtYear.Value);
            int quarter = TextUtils.ToInt(txtQuarter.Value);
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);

            // Kiểm tra nếu form đã được mở
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is frmKPIRanking existingForm)
                {
                    // Cập nhật dữ liệu vào form đang mở
                    existingForm.dtData = tempKPI;
                    existingForm.dtSummary = summary;
                    existingForm.year = year;
                    existingForm.quarter = quarter;
                    existingForm.departmentID = departmentID;


                    existingForm.loadData();
                    existingForm.BringToFront();
                    return;
                }
            }

            // Nếu chưa mở thì mở mới
            frmKPIRanking frm = new frmKPIRanking();
            frm.dtData = tempKPI;
            frm.dtSummary = summary;
            frm.year = year;
            frm.quarter = quarter;
            frm.departmentID = departmentID;
            frm.Show();
        }
    }



}
