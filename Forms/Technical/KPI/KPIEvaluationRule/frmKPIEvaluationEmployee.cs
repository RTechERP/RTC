using BaseBusiness.DTO;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
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
    public partial class frmKPIEvaluationEmployee : _Forms
    {
        public string deName;
        public bool isTBPView = false;
        public int employeeID = 0;

        public int year = 0;
        public int quarter = 0;

        public string employeeName = "";

        public int departmentID = 0; // vtn update 07/02/2025

        int departmentCK = 10;
        public frmKPIEvaluationEmployee()
        {
            InitializeComponent();
        }

        private void frmKPIEvaluationEmployee_Load(object sender, EventArgs e)
        {
            //departmentID = Global.DepartmentID == 9 ? 9 : 2;
            departmentID = Global.DepartmentID;
            txtYear.Value = DateTime.Now.Year;
            //this.Text += " - " + deName;
            employeeID = isTBPView ? employeeID : Global.EmployeeID;

            if (departmentID == departmentCK) LoadEventForTKCK();

            LoadKPISession();
            if (!isTBPView) btnEmployeeApproved_Click(null, null);
            else dockPanel3.Hide();
            btnEmployeeApproved.Enabled = btnSuccessKPI.Enabled = !isTBPView;
        }

        private void LoadKPISession()
        {
            try
            {
                year = TextUtils.ToInt(txtYear.Value);
                quarter = quarter == 0 ? ((DateTime.Now.Month - 1) / 3 + 1) : quarter;

                string keyWords = txtKeywords.Text.Trim();
                //DataTable dt = SQLHelper<KPISessionModel>.LoadDataFromSP("spGetKPISession", new string[] { "@Year", "@Keywords" },
                //                                                                            new object[] { year, keyWords });

                DataTable dt = SQLHelper<KPISessionModel>.LoadDataFromSP("spGetKPISession",
                                                        new string[] { "@Year", "@Keywords", "@DepartmentID" },
                                                        new object[] { year, keyWords, departmentID });
                grdSession.DataSource = dt;



                //var dataRow = dt.Select($"YearEvaluation = {year} AND QuarterEvaluation = {quarter}");
                for (int i = 0; i < grvSession.RowCount; i++)
                {
                    int q = TextUtils.ToInt(grvSession.GetRowCellValue(i, colQuarterEvaluation));
                    int y = TextUtils.ToInt(grvSession.GetRowCellValue(i, colSessionYear));
                    if (q == quarter && y == year)
                    {
                        grvSession.FocusedRowHandle = i;
                    }
                }


                LoadKpiExam();
                //LoadDataDetails();
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}");
            }
        }
        private void LoadKpiExam()
        {
            int kpiSessionId = TextUtils.ToInt(grvSession.GetFocusedRowCellValue(colSessionID));



            DataTable dt = SQLHelper<KPIExamModel>.LoadDataFromSP("spGetKPIExam", new string[] { "@EmployeeID", "@KPISessionID" },
                                                                                    new object[] { employeeID, kpiSessionId });
            grdExam.DataSource = dt;
            grdExam.RefreshDataSource();


            string sessionName = TextUtils.ToString(grvSession.GetRowCellValue(grvSession.FocusedRowHandle, colName));
            employeeName = string.IsNullOrWhiteSpace(employeeName) ? "" : $" - {employeeName.ToUpper()}";

            lblSession.Text = sessionName.ToUpper() + $"{employeeName}";
            //LoadDataDetails();
        }
        private void LoadDataDetails()
        {
            try
            {
                // ==================================== 09/12/2024 =================================================
                int kpiSessionId = TextUtils.ToInt(grvSession.GetFocusedRowCellValue(colSessionID));
                var expPoint1 = new Expression(KPIPositionEmployeeModel_Enum.EmployeeID, employeeID);
                var expPoint2 = new Expression(KPIPositionModel_Enum.KPISessionID, kpiSessionId);
                var expPoint3 = new Expression(KPIPositionEmployeeModel_Enum.IsDeleted, 0);

                var kpiPositions = SQLHelper<KPIPositionModel>.FindByExpression(expPoint2.And(expPoint3));
                var kpiPositionEmployees = SQLHelper<KPIPositionEmployeeModel>.FindByExpression(expPoint1.And(expPoint3));

                var empPosition = (from p in kpiPositions
                                   join pe in kpiPositionEmployees on p.ID equals pe.KPIPosiotionID
                                   select pe)
                         .FirstOrDefault() ?? new KPIPositionEmployeeModel();



                //KPIPositionEmployeeModel empPosition = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", employeeID).FirstOrDefault() ?? new KPIPositionEmployeeModel();
                //KPIPositionEmployeeModel empPosition = SQLHelper<KPIPositionEmployeeModel>.FindByExpression(expPoint1.And(expPoint2).And(expPoint3)).FirstOrDefault() ?? new KPIPositionEmployeeModel();
                Expression ex1 = new Expression("KPISessionID", kpiSessionId);
                Expression ex2 = new Expression("KPIPositionID", empPosition.KPIPosiotionID > 0 ? empPosition.KPIPosiotionID : 1); // 1 là kỹ thuật
                Expression ex3 = new Expression("IsDeleted", 0);
                KPIEvaluationRuleModel rule = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPIEvaluationRuleModel();

                KPIEmployeePointModel empPoint = SQLHelper<KPIEmployeePointModel>.FindByID(GetKPIEmployeePointID(rule.ID));
                bool isPublic = isTBPView || empPoint.IsPublish == true;
                if (rule.ID <= 0)
                {
                    isPublic = false;
                }
                // ==================================== END =================================================



                int kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));
                //DataTable list = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
                //                                            new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                //                                            new object[] { employeeID, 1, kpiExamID, isPublic });

                //DataRow parentRow = list.NewRow();
                //parentRow["ID"] = -1;
                //parentRow["ParentID"] = 0;
                //parentRow["EvaluationContent"] = "TỔNG HỆ SỐ";
                //parentRow["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
                //list.AcceptChanges();
                //list.Rows.Add(parentRow);

                //treeData.DataSource = CalculatorAvgPoint(list);
                //treeData.ExpandAll();
                ////treeData.BestFitColumns();



                //DataTable list2 = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
                //                                            new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                //                                            new object[] { employeeID, 2, kpiExamID, isPublic });

                //treeList1.DataSource = CalculatorAvgPoint(list2);
                //treeList1.ExpandAll();



                //DataTable list3 = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
                //                                            new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                //                                            new object[] { employeeID, 3, kpiExamID, isPublic });

                //DataRow parentRow3 = list3.NewRow();
                //parentRow3["ID"] = -1;
                //parentRow3["ParentID"] = 0;
                //parentRow3["EvaluationContent"] = "TỔNG HỆ SỐ";
                //parentRow3["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
                //list3.AcceptChanges();
                //list3.Rows.Add(parentRow3);

                //treeList2.DataSource = CalculatorAvgPoint(list3);
                //treeList2.ExpandAll();

                //LoadTotalAVG();


                ////if (!isPublic) return;
                //LoadKPIRule(empPoint.ID);
                //// ========================= 09/12/2024 =======================================================
                //List<KPIEmployeePointDetailModel> lst = SQLHelper<KPIEmployeePointDetailModel>.FindByAttribute("KPIEmployeePointID", empPoint.ID);
                //if (lst.Count <= 0)
                //{
                //    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
                //    {
                //        LoadPointRule(empPoint.ID);
                //    }
                //}
                //CalculatorPoint(empPoint.ID);

                LoadKPIKyNangNew(kpiExamID, isPublic);
                LoadKPIChungNew(kpiExamID, isPublic);
                LoadKPIChuyenMonNew(kpiExamID, isPublic);
                //LoadTotalAVGNew();
                //LoadSummaryRuleNew(empPoint.ID, isPublic);

                // TN.Binh update 02/10/25
                if (departmentID == departmentCK)
                {
                    LoadSumaryRank_TKCK();
                }
                else
                {
                    LoadTotalAVGNew();
                    LoadSummaryRuleNew(empPoint.ID, isPublic);
                }
                //endupdate
            }
            catch (Exception ex)
            {
                MessageBox.Show($"LoadDataDetails\r\n{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        //private void LoadTotalAVG()
        //{
        //    if(departmentID == departmentCK)
        //    {
        //        LoadSumaryRank_TKCK();
        //        return;
        //    }

        //    DataTable dtSkill = (DataTable)treeData.DataSource;
        //    List<DataRow> lstSkillPoint = new List<DataRow>();
        //    if (dtSkill != null) lstSkillPoint = dtSkill.Select("ID = -1").ToList();

        //    decimal totalEmpSkillPoint = 0;
        //    decimal totalTBPSkillPoint = 0;
        //    decimal totalBGDSkillPoint = 0;
        //    int countSkillPoint = lstSkillPoint.Count > 0 ? lstSkillPoint.Count : 1;
        //    foreach (DataRow item in lstSkillPoint)
        //    {
        //        totalEmpSkillPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
        //        totalTBPSkillPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
        //        totalBGDSkillPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
        //    }


        //    DataTable dt = (DataTable)treeList1.DataSource;
        //    List<DataRow> lstPoint = new List<DataRow>();
        //    if (dt != null) lstPoint = dt.Select("ParentID = 0").ToList();

        //    decimal totalEmpPLCPoint = 0;
        //    decimal totalTBPPLCPoint = 0;
        //    decimal totalBGDPLCPoint = 0;
        //    int countPLC = 0;

        //    decimal totalEmpPVisionPoint = 0;
        //    decimal totalTBPVisionPoint = 0;
        //    decimal totalBGDPVisionPoint = 0;
        //    int countVision = 0;

        //    decimal totalEmpSoftPoint = 0;
        //    decimal totalTBPSoftPoint = 0;
        //    decimal totalBGDSoftPoint = 0;
        //    int countSoft = 0;


        //    decimal totalEmpViRobotPoint = 0;
        //    decimal totalTBPViRobotPoint = 0;
        //    decimal totalBGDViRobotPoint = 0;
        //    int countViRobot = 0;
        //    foreach (DataRow item in lstPoint)
        //    {
        //        if (TextUtils.ToInt(item["SpecializationType"]) == 2)
        //        {
        //            totalEmpPLCPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
        //            totalTBPPLCPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
        //            totalBGDPLCPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
        //            countPLC++;
        //        }
        //        else if (TextUtils.ToInt(item["SpecializationType"]) == 3)
        //        {
        //            totalEmpPVisionPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
        //            totalTBPVisionPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
        //            totalBGDPVisionPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
        //            countVision++;
        //        }
        //        else if (TextUtils.ToInt(item["SpecializationType"]) == 4)
        //        {
        //            totalEmpSoftPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
        //            totalTBPSoftPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
        //            totalBGDSoftPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
        //            countSoft++;
        //        }
        //        else if (TextUtils.ToInt(item["SpecializationType"]) == 5)
        //        {
        //            totalEmpViRobotPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
        //            totalTBPViRobotPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
        //            totalBGDViRobotPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
        //            countViRobot++;
        //        }
        //        else continue;
        //    }
        //    countPLC = countPLC > 0 ? countPLC : 1;
        //    countVision = countVision > 0 ? countVision : 1;
        //    countSoft = countSoft > 0 ? countSoft : 1;
        //    countViRobot = countViRobot > 0 ? countViRobot : 1;


        //    DataTable dtGeneral = (DataTable)treeList2.DataSource;
        //    List<DataRow> lstGeneralPoint = new List<DataRow>();
        //    if (dtGeneral != null) lstGeneralPoint = dtGeneral.Select("ID = -1").ToList();
        //    decimal totalEmpGeneralPoint = 0;
        //    decimal totalTBPGeneralPoint = 0;
        //    decimal totalBGDGeneralPoint = 0;
        //    int countGeneralPoint = lstGeneralPoint.Count > 0 ? lstGeneralPoint.Count : 1;
        //    foreach (DataRow item in lstGeneralPoint)
        //    {
        //        totalEmpGeneralPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
        //        totalTBPGeneralPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
        //        totalBGDGeneralPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
        //    }


        //    decimal plcEmpPoint = (2 * totalEmpPLCPoint / countPLC + totalEmpViRobotPoint / countViRobot) / 3;
        //    decimal visionEmpPoint = (2 * totalEmpPVisionPoint / countVision + totalEmpViRobotPoint / countViRobot) / 3;

        //    decimal plcTBPPoint = (2 * totalTBPPLCPoint / countPLC + totalTBPViRobotPoint / countViRobot) / 3;
        //    decimal visionTBPPoint = (2 * totalTBPVisionPoint / countVision + totalTBPViRobotPoint / countViRobot) / 3;


        //    decimal plcBGDPoint = (2 * totalBGDPLCPoint / countPLC + totalBGDViRobotPoint / countViRobot) / 3;
        //    decimal visionBGDPoint = (2 * totalBGDPVisionPoint / countVision + totalBGDViRobotPoint / countViRobot) / 3;
        //    List<object> data = new List<object>()
        //    {
        //        new
        //        {
        //            EvaluatedType = "Tự đánh giá",
        //            SkillPoint = totalEmpSkillPoint / countSkillPoint,
        //            PLCPoint = plcEmpPoint,
        //            VisionPoint = visionEmpPoint,
        //            SoftWarePoint = totalEmpSoftPoint / countSoft,
        //            AVGPoint = ( plcEmpPoint + visionEmpPoint + (totalEmpSoftPoint / countSoft)) /3,
        //            GeneralPoint = totalEmpGeneralPoint / countGeneralPoint
        //        },
        //        new
        //        {
        //            EvaluatedType = "Đánh giá của Trưởng/Phó BP",
        //            SkillPoint = totalTBPSkillPoint / countSkillPoint,
        //            PLCPoint = plcTBPPoint,
        //            VisionPoint = visionTBPPoint,
        //            SoftWarePoint = totalTBPSoftPoint / countSoft,
        //            AVGPoint = ( plcTBPPoint + visionTBPPoint + (totalTBPSoftPoint / countSoft)) / 3,
        //            GeneralPoint = totalTBPGeneralPoint / countGeneralPoint
        //        },
        //         new
        //        {
        //            EvaluatedType = "Đánh giá của GĐ",
        //            SkillPoint = totalBGDSkillPoint / countSkillPoint,
        //            PLCPoint = plcBGDPoint,
        //            VisionPoint = visionBGDPoint,
        //            SoftWarePoint = totalBGDSoftPoint / countSoft,
        //            AVGPoint = (plcBGDPoint + visionBGDPoint + (totalBGDSoftPoint / countSoft)) / 3,
        //            GeneralPoint = totalBGDGeneralPoint / countGeneralPoint
        //        },

        //    };
        //    grdMaster.DataSource = data;
        //}
        //private DataTable CalculatorAvgPoint(DataTable dataTable)
        //{
        //    if (dataTable.Rows == null) return dataTable;
        //    List<string> listFatherID = new List<string>();
        //    foreach (DataRow row in dataTable.Rows)
        //    {
        //        string Stt = TextUtils.ToString(row["STT"]);
        //        if (string.IsNullOrWhiteSpace(Stt)) continue;
        //        string fatherID = Stt.Substring(0, Stt.LastIndexOf('.') > 0 ? Stt.LastIndexOf('.') : 1);
        //        bool isDuplicate = listFatherID.Any(p => p == fatherID);
        //        if (!isDuplicate) listFatherID.Add(fatherID);
        //    }

        //    for (int i = (listFatherID.Count - 1); i >= 0; i--)
        //    {
        //        string fatherId = listFatherID[i];
        //        int fatherRowIndex = -1;
        //        decimal coefficient = 0;

        //        int count = 0;
        //        decimal totalempPoint = 0;
        //        decimal totaltbpPoint = 0;
        //        decimal totalbgdPointt = 0;
        //        string startStt = fatherId + ".";
        //        foreach (DataRow row in dataTable.Rows)
        //        {
        //            string Stt = TextUtils.ToString(row["STT"]);
        //            bool isCheck = listFatherID.Any(p => p == Stt);

        //            if (string.IsNullOrWhiteSpace(Stt)) continue;
        //            if (Stt == fatherId)
        //            {
        //                fatherRowIndex = dataTable.Rows.IndexOf(row);
        //                coefficient = TextUtils.ToDecimal(row["Coefficient"]);
        //            }
        //            else if (Stt.StartsWith(startStt))
        //            {
        //                if (isCheck) continue;
        //                count++;
        //                totalempPoint += TextUtils.ToDecimal(row["EmployeePoint"]);
        //                totaltbpPoint += TextUtils.ToDecimal(row["TBPPoint"]);
        //                totalbgdPointt += TextUtils.ToDecimal(row["BGDPoint"]);
        //            }
        //        }
        //        if (fatherRowIndex == -1 || count == 0) continue;
        //        // if (TextUtils.ToInt(cboPositionType.EditValue) == 2) count = 1;

        //        dataTable.Rows[fatherRowIndex]["EmployeeEvaluation"] = (decimal)(totalempPoint / count);
        //        dataTable.Rows[fatherRowIndex]["BGDEvaluation"] = (decimal)(totalbgdPointt / count);
        //        dataTable.Rows[fatherRowIndex]["TBPEvaluation"] = (decimal)(totaltbpPoint / count);

        //        dataTable.Rows[fatherRowIndex]["EmployeeCoefficient"] = (decimal)((totalempPoint / count) * coefficient);
        //        dataTable.Rows[fatherRowIndex]["TBPCoefficient"] = (decimal)((totaltbpPoint / count) * coefficient);
        //        dataTable.Rows[fatherRowIndex]["BGDCoefficient"] = (decimal)((totalbgdPointt / count) * coefficient);
        //    }
        //    dataTable = CalculatorTotalPoint(dataTable);
        //    return dataTable;
        //}
        //private DataTable CalculatorTotalPoint(DataTable dataTable)
        //{
        //    List<DataRow> parentRow = dataTable.Select("ParentID = 0").ToList();
        //    foreach (DataRow row in parentRow)
        //    {
        //        int rowIndex = dataTable.Rows.IndexOf(row);
        //        List<DataRow> childrenRow = dataTable.Select($"ParentID = {row["ID"]}").ToList();
        //        decimal totalCoefficient = 0;
        //        decimal totalEmpAVGPoint = 0;
        //        decimal totalTBPAVGPoint = 0;
        //        decimal totalBGDAVGPoint = 0;
        //        foreach (DataRow item in childrenRow)
        //        {
        //            totalCoefficient += TextUtils.ToDecimal(item["Coefficient"]);
        //            totalEmpAVGPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
        //            totalTBPAVGPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
        //            totalBGDAVGPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
        //        }

        //        dataTable.Rows[rowIndex]["Coefficient"] = totalCoefficient;
        //        dataTable.Rows[rowIndex]["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
        //        totalCoefficient = totalCoefficient > 0 ? totalCoefficient : 1;
        //        dataTable.Rows[rowIndex]["EmployeeCoefficient"] = (decimal)(totalEmpAVGPoint / totalCoefficient);
        //        dataTable.Rows[rowIndex]["TBPCoefficient"] = (decimal)(totalTBPAVGPoint / totalCoefficient);
        //        dataTable.Rows[rowIndex]["BGDCoefficient"] = (decimal)(totalBGDAVGPoint / totalCoefficient);

        //        dataTable.Rows[rowIndex]["EmployeeEvaluation"] = 0;
        //        dataTable.Rows[rowIndex]["BGDEvaluation"] = 0;
        //        dataTable.Rows[rowIndex]["TBPEvaluation"] = 0;
        //    }

        //    return dataTable;
        //}
        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            LoadKPISession();
        }

        private void grvSession_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            //LoadKpiExam();

            int kpiSessionID = TextUtils.ToInt(grvSession.GetRowCellValue(e.FocusedRowHandle, colSessionID));

            LoadKPIPosition(kpiSessionID);  //TN.Binh update 23/09/25

            //List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(p => p.ID).ToList();

            //TN.Binh update 23/09/25
            List<KPIPositionEmployeeModel> employee = SQLHelper<KPIPositionEmployeeModel>.ProcedureToList("spGetEmployeeInKPISession", new string[] { "@KPISessionID", "@EmployeeID" },
                                                                                                   new object[] { kpiSessionID, Global.EmployeeID });
            if (employee.Count > 0)
            {
                cboChoicePosition.EditValue = employee[0].KPIPosiotionID;
                cboChoicePosition.Properties.ReadOnly = true;
                btnChoicePosition.Enabled = false;
                string sessionName = TextUtils.ToString(grvSession.GetRowCellValue(e.FocusedRowHandle, colName));
                employeeName = string.IsNullOrWhiteSpace(employeeName) ? "" : $" - {employeeName.ToUpper()}";
                LoadKpiExam();
                lblSession.Text = sessionName.ToUpper() + $"{employeeName}";
            }
            else
            {
                cboChoicePosition.Properties.ReadOnly = false;
                cboChoicePosition.EditValue = null;
                btnChoicePosition.Enabled = true;
            }
            //endupdate
            //LoadDataDetails();


            //LoadDataDetails();

        }

        private void grvExam_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataDetails();
        }

        private void btnEmployeeApproved_Click(object sender, EventArgs e)
        {
            int quarter = (DateTime.Now.Month - 1) / 3 + 1;
            int empId = isTBPView ? employeeID : Global.EmployeeID;
            int kpiExamID = 0;
            int status = 0;

            status = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamStatus));
            kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));
            frmKPIEvaluationFactorScoringDetails frm = new frmKPIEvaluationFactorScoringDetails();
            frm.employeeID = employeeID;
            if (Global.IsAdmin) frm.employeeID = empId;
            frm.kpiExam = SQLHelper<KPIExamModel>.FindByID(kpiExamID);
            frm.typePoint = 1;
            frm.cboEmployee.Enabled = false;
            frm.cboKpiExam.Enabled = false;
            frm.status = status;
            frm._departmentID = departmentID;

            if (frm.kpiExam.ID <= 0)
            {
                if (sender == null) return;
                MessageBox.Show("Bài đánh giá không hợp lệ! Hãy chọn lại bài đánh giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (status > 0)
            //{
            //    MessageBox.Show("Bài đánh giá đã xác nhận hoàn thành, không thể đánh giá lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            if (frm.kpiExam.Deadline.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Bài đánh giá đã hết hạn làm bài!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataDetails();
            }
        }

        private void treeData_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                return;
            }
            else
            {

                if (e.Column == colTBPPoint || e.Column == colEmployeePoint || e.Column == colBGDPoint)
                {
                    e.Appearance.BackColor = Color.LightYellow;
                }
            }
        }

        private void treeData_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            //bool isStyle = e.Column == colCoefficient || e.Column == colStandardPoint ||
            //                e.Column == colEmployeeEvaluation || e.Column == colBGDEvaluation || e.Column == colTBPEvaluation ||
            //               e.Column == colEmployeeCoefficient || e.Column == colTBPCoefficient || e.Column == colBGDCoefficient;
            //if (isStyle)
            //{
            //    if (TextUtils.ToInt(e.Value) == 0)
            //    {
            //        e.DisplayText = "";
            //    }
            //}

            List<string> listFieldNames = treeListBand2.Columns.Select(x => x.FieldName).ToList();
            listFieldNames.AddRange(treeListBand3.Columns.Select(x => x.FieldName));
            listFieldNames.AddRange(treeListBand4.Columns.Select(x => x.FieldName));
            listFieldNames.AddRange(new List<string>() { colStandardPoint.FieldName, colCoefficient.FieldName, colEmployeePoint.FieldName, colTBPPoint.FieldName, colBGDPoint.FieldName });


            if (!listFieldNames.Contains(e.Column.FieldName)) return;
            decimal value = TextUtils.ToDecimal(e.Value);
            if (value == 0) e.DisplayText = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //LoadKPISession();

            LoadDataDetails();

        }

        private void btnSuccessKPI_Click(object sender, EventArgs e)
        {
            int kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));
            if (kpiExamID <= 0)
            {
                MessageBox.Show("Vui lòng chọn bài đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<KPIEvaluationPointModel> lst = SQLHelper<KPIEvaluationPointModel>.ProcedureToList("spGetKPIEvaluationPoint", new string[] { "@KPIExamID", "@EmployeeID" },
                                                                                                    new object[] { kpiExamID, employeeID });
            if (lst.Count <= 0)
            {
                MessageBox.Show("Vui lòng Đánh giá KPI trước khi hoàn thành!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                bool isSuccess = lst.Any(p => p.Status >= 1);
                if (isSuccess) return;
                bool isDelete = MessageBox.Show($"Bạn có muốn xác nhận hoàn thành Bài đánh giá [{TextUtils.ToString(grvExam.GetFocusedRowCellValue(colExamName))}] hay không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                if (isDelete)
                {
                    foreach (KPIEvaluationPointModel item in lst)
                    {
                        item.Status = 1;
                        item.DateEmployeeConfirm = DateTime.Now; //TN.Binh update 26/09/2025
                        SQLHelper<KPIEvaluationPointModel>.Update(item);
                    }
                }
            }
            LoadKpiExam();
        }

        private void treeList1_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colCoefficient1 || e.Column == colStandardPoint1 || e.Column == colEmployeeEvaluation1 || e.Column == colBGDEvaluation1 || e.Column == colTBPEvaluation1 ||
                           e.Column == colEmployeeCoefficient1 || e.Column == colTBPCoefficient1 || e.Column == colBGDCoefficient1)
            {
                if (TextUtils.ToInt(e.Value) == 0)
                {
                    e.DisplayText = "";
                }
            }
        }

        private void treeList1_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                return;
            }
            else
            {

                if (e.Column == colTBPPoint1 || e.Column == colEmployeePoint1 || e.Column == colBGDPoint1)
                {
                    e.Appearance.BackColor = Color.LightYellow;
                }
            }
        }

        private void grvExam_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int status = TextUtils.ToInt(grvExam.GetRowCellValue(e.RowHandle, colExamStatus));
            DateTime deadline = TextUtils.ToDate5(grvExam.GetRowCellValue(e.RowHandle, colExamDeadline));
            if (status == 0 && deadline < DateTime.Now)
            {
                e.Appearance.BackColor = Color.OrangeRed;
                e.Appearance.ForeColor = Color.WhiteSmoke;

            }
            else if (status > 0)
            {
                e.Appearance.BackColor = Color.LightGreen;
            }
            e.HighPriority = true;
        }

        private void treeList2_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                return;
            }
            else
            {

                if (e.Column == colTBPPoint2 || e.Column == colEmployeePoint2 || e.Column == colBGDPoint2)
                {
                    e.Appearance.BackColor = Color.LightYellow;
                }
            }
        }

        private void treeList2_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colStandardPoint2 || e.Column == colCoefficient2)
            {
                if (TextUtils.ToInt(e.Value) == 0)
                {
                    e.DisplayText = "";
                }
            }
        }

        private void treeData_CustomDrawNodeCell_1(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                return;
            }
            else
            {

                if (e.Column == colTBPPoint || e.Column == colEmployeePoint || e.Column == colBGDPoint)
                {
                    e.Appearance.BackColor = Color.LightYellow;
                }
            }
        }

        private void treeData_CustomColumnDisplayText_1(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colStandardPoint || e.Column == colCoefficient)
            {
                if (TextUtils.ToInt(e.Value) == 0)
                {
                    e.DisplayText = "";
                }
            }
        }

        private void treeList1_CustomDrawNodeCell_1(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                return;
            }
            else
            {

                if (e.Column == colTBPPoint1 || e.Column == colEmployeePoint1 || e.Column == colBGDPoint1)
                {
                    e.Appearance.BackColor = Color.LightYellow;
                }
            }
        }

        private void treeList1_CustomColumnDisplayText_1(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colStandardPoint1 || e.Column == colCoefficient1)
            {
                if (TextUtils.ToInt(e.Value) == 0)
                {
                    e.DisplayText = "";
                }
            }
        }


        private void LoadKPIRule(int empointID, bool isPublic)
        {
            tlKPIRule.DataSource = null;
            grdTeam.DataSource = null;
            KPIEmployeePointModel kpiEmpPoint = SQLHelper<KPIEmployeePointModel>.FindByID(empointID);
            //if (!kpiEmpPoint.IsPublish && !isTBPView)
            //{
            //    DataTable dt = new DataTable();
            //    grdTeam.DataSource = dt;
            //    tlKPIRule.DataSource = dt;
            //    return;
            //}//======================  09/2/2024  ======================
            //DataTable dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeam", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { kpiEmpPoint.ID });
            DataTable dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeamNew", "spGetKpiRuleSumarizeTeamNew",
                                                        new string[] { "@KPIEmployeePointID" }, new object[] { kpiEmpPoint.ID });
            grdTeam.DataSource = dtTeam;


            //DataTable dtKpiRule = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointID", "LMKTable",
            //                                                new string[] { "@KPIEmployeePointID", "@IsPublic" },
            //                                                new object[] { kpiEmpPoint.ID, isPublic });

            DataTable dtKpiRule = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointIDNew", "spGetEmployeeRulePointByKPIEmpPointIDNew",
                                                                        new string[] { "@KPIEmployeePointID", "@IsPublic" },
                                                                        new object[] { kpiEmpPoint.ID, isPublic });
            if (!TextUtils.ToBoolean(kpiEmpPoint.IsPublish) && !isTBPView)
            {
                dtKpiRule.AsEnumerable().ToList().ForEach(row =>
                {
                    row[colPercentRemaining.FieldName] = 0;
                });
            }
            tlKPIRule.DataSource = dtKpiRule;
            tlKPIRule.ExpandAll();
        }

        private int GetKPIEmployeePointID(int ruleID)
        {
            int empID = employeeID;
            string empName = Global.AppFullName;
            if (empID <= 0)
            {
                //MessageBox.Show($"Không tìm thấy ID của nhân viên [{empName}]", "Thông báo");
                return -1;
            }
            if (ruleID <= 0)
            {
                //MessageBox.Show($"Không tìm thấy ID của rule đánh giá! Vui lòng kiểm tra lại", "Thông báo");
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


        private void treeList3_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            //if (e.Node.HasChildren)
            //{
            //    e.Appearance.BackColor = Color.LightGray;
            //    return;
            //}
            //else
            //{
            //    string ruleCode = TextUtils.ToString(e.Node["EvaluationCode"]).ToUpper();
            //    bool isColumn = e.Column == colFirstMonth || e.Column == colSecondMonth || e.Column == colThirdMonth;
            //    bool isKPI = ruleCode.StartsWith("KPI");
            //    bool isNQNL = ruleCode == "KPINL" || ruleCode == "KPINQ" || ruleCode.StartsWith("TEAM");


            //    if ((isColumn && !isKPI && !isNQNL) || (isNQNL && e.Column == colTotalError))
            //    {
            //        e.Appearance.BackColor = Color.LightYellow;
            //    }
            //}

            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                return;
            }
            else
            {
                string ruleCode = TextUtils.ToString(e.Node["EvaluationCode"]).ToUpper();
                bool isColumn = e.Column == colFirstMonth || e.Column == colSecondMonth || e.Column == colThirdMonth;
                bool isKPI = ruleCode.StartsWith("KPI");
                bool isNQNL = ruleCode == "KPINL" || ruleCode == "KPINQ"; //|| ruleCode.StartsWith("TEAM");
                bool isTeam = ruleCode.StartsWith("TEAM");
                TreeListNode parentNode = e.Node.ParentNode;
                if (parentNode != null)
                {
                    string parentNodeCode = TextUtils.ToString(parentNode["EvaluationCode"]).ToUpper();
                    isTeam = isTeam || parentNodeCode.StartsWith("TEAM");
                }
                if ((isColumn && !isKPI && !isNQNL) || (isNQNL && e.Column == colTotalError))
                {
                    e.Appearance.BackColor = Color.LightYellow;
                }


                if (isColumn && isTeam)
                {
                    //e.Appearance.BackColor = Color.BlueViolet;
                    e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#d1e7dd");
                }
            }
        }

        private void treeList3_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            //bool isStyle = e.Column == colMaxPercent || e.Column == colPercentageAdjustment || e.Column == colMaxPercentageAdjustment || e.Column == colFirstMonth || e.Column == colSecondMonth ||
            //              e.Column == colThirdMonth || e.Column == colTotalError || e.Column == colPercentBonus || e.Column == colPercentRemaining;
            //if (isStyle)
            //{
            //    if (TextUtils.ToDecimal(e.Value) == 0)
            //    {
            //        e.DisplayText = "";
            //    }
            //}

            List<TreeListBand> listBands = new List<TreeListBand>() { /*treeListBand18,*/ treeListBand19 };
            if (!listBands.Contains(e.Column.ParentBand)) return;
            decimal value = TextUtils.ToDecimal(e.Value);
            if (value == 0) e.DisplayText = "";
        }

        //================================ 09/12/2024 =================================
        //private void LoadPointRule(int empPointID)
        //{


        //    List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointID",
        //                                                               new string[] { "@KPIEmployeePointID" },
        //                                                               new object[] { empPointID });

        //    decimal timeWork = TextUtils.ToDecimal(grvTeam.Columns["TimeWork"].SummaryItem.SummaryValue);
        //    decimal fiveS = TextUtils.ToDecimal(grvTeam.Columns["FiveS"].SummaryItem.SummaryValue);
        //    decimal reportWork = TextUtils.ToDecimal(grvTeam.Columns["ReportWork"].SummaryItem.SummaryValue);
        //    decimal customerComplaint = TextUtils.ToDecimal(grvTeam.Columns["CustomerComplaint"].SummaryItem.SummaryValue);
        //    decimal deadlineDelay = TextUtils.ToDecimal(grvTeam.Columns["DeadlineDelay"].SummaryItem.SummaryValue);
        //    decimal teamKPIKyNang = TextUtils.ToDecimal(grvTeam.Columns["KPIKyNang"].SummaryItem.SummaryValue);
        //    decimal teanKPIChung = TextUtils.ToDecimal(grvTeam.Columns["KPIChung"].SummaryItem.SummaryValue);
        //    decimal teamKPIPLC = TextUtils.ToDecimal(grvTeam.Columns["KPIPLC"].SummaryItem.SummaryValue);
        //    decimal teamKPIVISION = TextUtils.ToDecimal(grvTeam.Columns["KPIVision"].SummaryItem.SummaryValue);
        //    decimal teamKPISOFTWARE = TextUtils.ToDecimal(grvTeam.Columns["KPISoftware"].SummaryItem.SummaryValue);
        //    decimal missingTool = TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);  //làm mất mát hỏng thiết bị 12/12/2024
        //                                                                                                         //================================== update 13/12/2024 ================================== 
        //    List<string> lstCodeTBP = new List<string>() { "MA03", "MA04", "NotWorking", "WorkLate" }; // MA011 Tổng số liệu thời gian đi làm ko đúng giờ + đi làm ko đủ công + L4 + L5
        //    var ltsMA11 = lstResult.Where(p => lstCodeTBP.Contains(p.EvaluationCode.Trim())).ToList();
        //    //decimal totalErrorTBP = lstResult.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);
        //    decimal totalErrorTBP = ltsMA11.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);
        //    //==========================================  END ==========================================
        //    lstResult.AddRange(new List<KPISumarizeDTO>
        //        {
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM01", ThirdMonth = timeWork},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM02", ThirdMonth = fiveS},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM03", ThirdMonth = reportWork},
        //             new KPISumarizeDTO(){ EvaluationCode = "TEAM04", ThirdMonth = customerComplaint + missingTool + deadlineDelay},//update  12/12/2024
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM05", ThirdMonth = customerComplaint}, //update  12/12/2024
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = missingTool},//update  12/12/2024
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIKYNANG", ThirdMonth = teamKPIKyNang},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIChung", ThirdMonth = teanKPIChung},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIPLC", ThirdMonth = teamKPIPLC},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIVISION", ThirdMonth = teamKPIVISION},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPISOFTWARE", ThirdMonth = teamKPISOFTWARE},
        //            new KPISumarizeDTO(){ EvaluationCode = "MA11", ThirdMonth = totalErrorTBP}, // update 13/12/2024
        //        });


        //    Lib.LockEvents = true;
        //    foreach (KPISumarizeDTO item in lstResult)
        //    {
        //        TreeListNode node = tlKPIRule.GetNodeList().FirstOrDefault(x => item.EvaluationCode == TextUtils.ToString(x.GetValue(colEvaluationCode)));
        //        if (node == null) continue;
        //        node.SetValue(colFirstMonth, item.FirstMonth);
        //        node.SetValue(colSecondMonth, item.SecondMonth);
        //        node.SetValue(colThirdMonth, item.ThirdMonth);
        //    }




        //    Lib.LockEvents = false;

        //    //CalculatorPoint(empPointID);
        //}

        List<string> lstTeamTBP = new List<string>() { "TEAM01", "TEAM02", "TEAM03" }; //19/12/2024
        private void CalculatorPoint(int empPointID)
        {

            try
            {
                KPIEmployeePointModel kpiEmpPoint = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);
                //if (!kpiEmpPoint.IsPublish && !isTBPView) return;

                //================ update 12/12/2024 ====================================

                KPIEmployeePointModel empPointModel = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);
                KPIEvaluationRuleModel ruleModel = SQLHelper<KPIEvaluationRuleModel>.FindByID(TextUtils.ToInt(empPointModel.KPIEvaluationRuleID));
                bool isTBP = ruleModel.KPIPositionID == 5; // TBP
                //======================================================================
                Lib.LockEvents = true;
                CalculatorNoError();
                List<TreeListNode> lst = tlKPIRule.GetNodeList();
                for (int i = lst.Count - 1; i >= 0; i--)
                {
                    if (!tlKPIRule.Visible) continue;
                    TreeListNode row = lst[i];
                    if (row == null) continue;
                    string stt = TextUtils.ToString(row["STT"]);
                    string ruleCode = TextUtils.ToString(row["EvaluationCode"]).ToUpper();
                    bool isDiemThuong = ruleCode == "THUONG";

                    decimal maxPercentBonus = TextUtils.ToDecimal(row["MaxPercent"]);
                    decimal percentageAdjustment = TextUtils.ToDecimal(row["PercentageAdjustment"]);
                    decimal maxPercentageAdjustment = TextUtils.ToDecimal(row["MaxPercentageAdjustment"]);

                    if (row.Nodes.Count > 0)
                    {
                        decimal totalPercentBonus = 0;
                        decimal totalPercentRemaining = 0;
                        bool isKPI = false;
                        decimal total = 0;
                        foreach (TreeListNode childrenNode in row.Nodes)
                        {
                            string childRuleCode = TextUtils.ToString(childrenNode["EvaluationCode"]);
                            isKPI = childRuleCode.ToUpper().StartsWith("KPI");
                            totalPercentBonus += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(childrenNode["PercentBonus"]), 1);
                            totalPercentRemaining += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(childrenNode["PercentRemaining"]), 1);
                            total += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(childrenNode[colTotalError.FieldName]), 1);
                        }

                        row["PercentBonus"] = totalPercentBonus;
                        row["TotalError"] = total;

                        if (lstTeamTBP.Contains(ruleCode) /*&& isTBP*/) //Update 13/12/2024  Tính trực tiếp node cha bên PP
                        {
                            row["TotalError"] = TextUtils.ToDecimal(row["ThirdMonth"]);
                        }
                        else if (isKPI) // Tính tổng KPI lên node cha
                        {
                            row["PercentRemaining"] = totalPercentRemaining;
                        }
                        else if (isDiemThuong) row["PercentRemaining"] = maxPercentBonus > totalPercentBonus ? totalPercentBonus : maxPercentBonus;
                        else if (maxPercentBonus > 0) row["PercentRemaining"] = maxPercentBonus > totalPercentBonus ? maxPercentBonus - totalPercentBonus : 0;
                        //else row["PercentBonus"] = totalPercentBonus;
                        else
                        {
                            row["PercentBonus"] = totalPercentBonus;
                            row["PercentRemaining"] = totalPercentRemaining;
                        }

                        if (lstTeamTBP.Contains(ruleCode) /*&& isTBP*/)//Update 13/12/2024 Tính % thưởng KPITeam PP
                        {
                            row["PercentBonus"] = TextUtils.ToDecimal(row["ThirdMonth"]) * percentageAdjustment > maxPercentageAdjustment ? maxPercentageAdjustment : TextUtils.ToDecimal(row["ThirdMonth"]) * percentageAdjustment;
                        }
                        else if (maxPercentageAdjustment > 0) row["PercentBonus"] = (maxPercentageAdjustment > totalPercentBonus ? totalPercentBonus : maxPercentageAdjustment);

                        if (percentageAdjustment > 0)
                        {
                            decimal totalPercentDeduction = percentageAdjustment * TextUtils.ToDecimal(row["TotalError"]);
                            row["PercentBonus"] = maxPercentageAdjustment > 0 ? (totalPercentDeduction > maxPercentageAdjustment ? maxPercentageAdjustment : totalPercentDeduction) : totalPercentDeduction;
                        }
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
                    if (!TextUtils.ToBoolean(kpiEmpPoint.IsPublish) && !isTBPView) row["PercentRemaining"] = 0;
                }

                tlKPIRule.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CalculatorPoint\r\n{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
            finally
            {
                Lib.LockEvents = false;
            }
        }
        string[] listCodes = new string[] { "MA01", "MA02", "MA03", "MA04", "MA05", "MA06", "MA07", "WorkLate", "NotWorking" };
        private void CalculatorNoError()
        {
            var list = tlKPIRule.GetNodeList().Where(x => listCodes.Contains(x.GetValue(colEvaluationCode)));

            decimal firstMonth = list.Sum(x => TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(x.GetValue(colFirstMonth)), 1));
            decimal secondMonth = list.Sum(x => TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(x.GetValue(colSecondMonth)), 1));
            decimal thirdMonth = list.Sum(x => TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(x.GetValue(colThirdMonth)), 1));

            var node = tlKPIRule.FindNodeByFieldValue(colEvaluationCode.FieldName, "MA09");
            if (node == null) return;


            node.SetValue(colFirstMonth, firstMonth);
            node.SetValue(colSecondMonth, secondMonth);
            node.SetValue(colThirdMonth, thirdMonth);

        }

        private void grvTeam_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            List<GridBand> listBands = new List<GridBand>() { gridBand4, gridBand5, gridBand6, gridBand7 };
            BandedGridColumn col = (BandedGridColumn)e.Column;

            if (!listBands.Contains(col.OwnerBand)) return;
            decimal value = TextUtils.ToDecimal(e.Value);
            if (value == 0) e.DisplayText = "";
        }

        // ============================= 28/12/2024 ============================
        //private void LoadKPIKyNang(int kpiExamID, bool isPublic)
        //{
        //    try
        //    {
        //        DataTable list = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
        //                                                new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
        //                                                new object[] { employeeID, 1, kpiExamID, isPublic });

        //        DataRow parentRow = list.NewRow();
        //        parentRow["ID"] = -1;
        //        parentRow["ParentID"] = 0;
        //        parentRow["EvaluationContent"] = "TỔNG HỆ SỐ";
        //        parentRow["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
        //        list.AcceptChanges();
        //        list.Rows.Add(parentRow);

        //        treeData.DataSource = departmentID == departmentCK ? CalculatorAvgPoint_TKCK(list) : CalculatorAvgPoint(list);
        //        treeData.ExpandAll();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
        //private void LoadKPIChung(int kpiExamID, bool isPublic)
        //{
        //    DataTable list2 = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
        //                                               new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
        //                                               new object[] { employeeID, 2, kpiExamID, isPublic });

        //    treeList1.DataSource = CalculatorAvgPoint(list2);
        //    treeList1.ExpandAll();
        //}
        //private void LoadKPIChuyenMon(int kpiExamID, bool isPublic)
        //{
        //    DataTable list3 = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
        //                                                new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
        //                                                new object[] { employeeID, 3, kpiExamID, isPublic });

        //    DataRow parentRow3 = list3.NewRow();
        //    parentRow3["ID"] = -1;
        //    parentRow3["ParentID"] = 0;
        //    parentRow3["EvaluationContent"] = "TỔNG HỆ SỐ";
        //    parentRow3["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
        //    list3.AcceptChanges();
        //    list3.Rows.Add(parentRow3);

        //    treeList2.DataSource = CalculatorAvgPoint(list3);
        //    treeList2.ExpandAll();
        //}
        //private void LoadSummaryRule(int empPointID, bool isPublic)
        //{
        //    LoadKPIRule(empPointID, isPublic);
        //    // ========================= 09/12/2024 =======================================================
        //    List<KPIEmployeePointDetailModel> lst = SQLHelper<KPIEmployeePointDetailModel>.FindByAttribute("KPIEmployeePointID", empPointID);
        //    if (lst.Count <= 0)
        //    {
        //        using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
        //        {
        //            LoadPointRuleNew(empPointID);
        //        }
        //    }
        //    CalculatorPoint(empPointID);


        //    //decimal totalPercent = TextUtils.ToDecimal(tlKPIRule.GetSummaryValue(colPercentRemaining));
        //    //string totalPercentText = "";
        //    //if (totalPercent < 60) totalPercentText = "Xếp loại: D";
        //    //else if (totalPercent >= 60 && totalPercent < 65) totalPercentText = "Xếp loại: C-";
        //    //else if (totalPercent >= 65 && totalPercent < 70) totalPercentText = "Xếp loại: C";
        //    //else if (totalPercent >= 70 && totalPercent < 75) totalPercentText = "Xếp loại: C+";
        //    //else if (totalPercent >= 75 && totalPercent < 80) totalPercentText = "Xếp loại: B-";
        //    //else if (totalPercent >= 80 && totalPercent < 85) totalPercentText = "Xếp loại: B";
        //    //else if (totalPercent >= 85 && totalPercent < 90) totalPercentText = "Xếp loại: B+";
        //    //else if (totalPercent >= 90 && totalPercent < 95) totalPercentText = "Xếp loại: A-";
        //    //else if (totalPercent >= 95 && totalPercent < 100) totalPercentText = "Xếp loại: A";
        //    //else if (totalPercent >= 100) totalPercentText = "Xếp loại: A+";

        //    //string sessionName = TextUtils.ToString(grvSession.GetRowCellValue(grvSession.FocusedRowHandle, colName));
        //    //employeeName = string.IsNullOrWhiteSpace(employeeName) ? "" : $" - { employeeName.ToUpper()}";

        //    //lblSession.Text = sessionName.ToUpper() + $"{employeeName} - {totalPercentText}";

        //}
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            int selectedTab = xtraTabControl1.SelectedTabPageIndex;
            if (selectedTab < 0) return;

            int kpiSessionId = TextUtils.ToInt(grvSession.GetFocusedRowCellValue(colSessionID));
            //KPIPositionEmployeeModel empPosition = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", employeeID).FirstOrDefault() ?? new KPIPositionEmployeeModel();

            //Get possition của nhân viên
            var expPoint1 = new Expression(KPIPositionEmployeeModel_Enum.EmployeeID, employeeID);
            var expPoint2 = new Expression(KPIPositionModel_Enum.KPISessionID, kpiSessionId);
            var expPoint3 = new Expression(KPIPositionEmployeeModel_Enum.IsDeleted, 0);

            var kpiPositions = SQLHelper<KPIPositionModel>.FindByExpression(expPoint2.And(expPoint3));
            var kpiPositionEmployees = SQLHelper<KPIPositionEmployeeModel>.FindByExpression(expPoint1.And(expPoint3));

            var empPosition = (from p in kpiPositions
                               join pe in kpiPositionEmployees on p.ID equals pe.KPIPosiotionID
                               select pe)
                     .FirstOrDefault() ?? new KPIPositionEmployeeModel();
            //

            Expression ex1 = new Expression("KPISessionID", kpiSessionId);
            Expression ex2 = new Expression("KPIPositionID", empPosition.KPIPosiotionID > 0 ? empPosition.KPIPosiotionID : 1); // 1 là kỹ thuật
            Expression ex3 = new Expression("IsDeleted", 0);
            KPIEvaluationRuleModel rule = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPIEvaluationRuleModel();
            int kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));

            KPIEmployeePointModel empPoint = SQLHelper<KPIEmployeePointModel>.FindByID(GetKPIEmployeePointID(rule.ID));
            bool isPublic = isTBPView || empPoint.IsPublish == true;
            if (rule.ID <= 0)
            {
                isPublic = false;
            }


            switch (selectedTab)
            {
                case 0:
                    LoadKPIKyNangNew(kpiExamID, isPublic);
                    break;

                case 1:
                    LoadKPIChungNew(kpiExamID, isPublic);
                    break;

                case 2:
                    LoadKPIChuyenMonNew(kpiExamID, isPublic);
                    break;

                case 3:
                    //LoadTotalAVGNew();
                    // TN.Binh update 02/10/25
                    if (departmentID == departmentCK) LoadSumaryRank_TKCK();

                    else LoadTotalAVGNew();

                    //endupdate
                    break;

                case 4:
                    LoadSummaryRuleNew(empPoint.ID, isPublic);
                    break;
                case 5:
                    LoadSummaryRuleNew(empPoint.ID, isPublic);
                    break;
                default:
                    break;
            }
        }

        List<string> lstCodeDisplay = new List<string>() { "KPINQ", "KPINL" };
        private void tlKPIRule_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            List<TreeListBand> listBands = new List<TreeListBand>() { /*treeListBand18,*/ treeListBand19 };
            bool isColumn = e.Column == colFirstMonth || e.Column == colSecondMonth || e.Column == colThirdMonth;
            string ruleCode = TextUtils.ToString(e.Node["EvaluationCode"]).ToUpper();
            decimal maxPercent = TextUtils.ToDecimal(e.Node[colMaxPercent.FieldName]);
            bool isTeam = ruleCode.StartsWith("TEAM");
            //if (treeListBand18.Columns.Contains(e.Column) && !lstCodeDisplay.Contains(ruleCode))
            //{
            //    e.DisplayText = TextUtils.ToDecimal(e.Value).ToString("N1");
            //}

            if (lstCodeDisplay.Contains(ruleCode) && isColumn)
            {
                e.DisplayText = "";
            }
            TreeListNode parentNode = e.Node.ParentNode;
            List<TreeListNode> childs = e.Node.Nodes.ToList();
            if (childs.Count > 0)
            {
                isTeam = isTeam || childs.Any(p => TextUtils.ToString(p["EvaluationCode"]).ToUpper().StartsWith("TEAM"));
            }

            if (parentNode != null)
            {
                string parentNodeCode = TextUtils.ToString(parentNode["EvaluationCode"]).ToUpper();
                isTeam = isTeam || parentNodeCode.StartsWith("TEAM");
            }
            else
            {
                if ((treeListBand18 == e.Column.ParentBand || e.Column == colPercentBonus) && childs.Count > 0 && maxPercent <= 0)
                {
                    e.DisplayText = "";
                }

            }

            if (isColumn && isTeam)
            {
                e.DisplayText = "";
            }

            if (listBands.Contains(e.Column.ParentBand))
            {
                decimal value = TextUtils.ToDecimal(e.Value);
                if (value == 0) e.DisplayText = "";
            }
        }

        private void tlKPIRule_GetCustomSummaryValue(object sender, DevExpress.XtraTreeList.GetCustomSummaryValueEventArgs e)
        {
            if (e.IsSummaryFooter && e.Column == colPercentBonus)
            {
                decimal totalPercent = TextUtils.ToDecimal(tlKPIRule.GetSummaryValue(colPercentRemaining));
                string totalPercentText = "";
                if (totalPercent < 60) totalPercentText = "Xếp loại: D";
                else if (totalPercent >= 60 && totalPercent < 65) totalPercentText = "Xếp loại: C-";
                else if (totalPercent >= 65 && totalPercent < 70) totalPercentText = "Xếp loại: C";
                else if (totalPercent >= 70 && totalPercent < 75) totalPercentText = "Xếp loại: C+";
                else if (totalPercent >= 75 && totalPercent < 80) totalPercentText = "Xếp loại: B-";
                else if (totalPercent >= 80 && totalPercent < 85) totalPercentText = "Xếp loại: B";
                else if (totalPercent >= 85 && totalPercent < 90) totalPercentText = "Xếp loại: B+";
                else if (totalPercent >= 90 && totalPercent < 95) totalPercentText = "Xếp loại: A-";
                else if (totalPercent >= 95 && totalPercent < 100) totalPercentText = "Xếp loại: A";
                else if (totalPercent >= 100) totalPercentText = "Xếp loại: A+";

                e.CustomValue = totalPercentText;
            }

            //TN.Binh update 13/10/2025
            // Lấy toàn bộ node trong TreeList
            List<TreeListNode> lst = tlKPIRule.GetNodeList();
            if (lst == null || lst.Count == 0) return;

            bool IsKPI(TreeListNode r)
            {
                string ruleCode = TextUtils.ToString(r["EvaluationCode"]).ToUpper();
                return ruleCode.StartsWith("KPI") && ruleCode != "KPINL" && ruleCode != "KPINQ";
            }
            decimal percentRemaining = Math.Round(TextUtils.ToDecimal(tlKPIRule.GetSummaryValue(colPercentRemaining)), 2);

            decimal emp = 0;
            decimal tbp = 0;
            decimal bgd = 0;

            foreach (var r in lst.Where(r => r.Nodes.Count == 0 && IsKPI(r)))
            {
                decimal maxPercent = TextUtils.ToDecimal(r["MaxPercent"]);
                decimal firstMonth = Math.Round(TextUtils.ToDecimal(r["FirstMonth"]), 1);
                decimal secondMonth = Math.Round(TextUtils.ToDecimal(r["SecondMonth"]), 1);
                //decimal thirdMonth = Math.Round(TextUtils.ToDecimal(r["ThirdMonth"]), 1);
                decimal thirdMonth = Math.Round(TextUtils.ToDecimal(r["ThirdMonth"]), 2);
                emp += Math.Round((firstMonth * maxPercent / 5), 1);
                tbp += Math.Round((secondMonth * maxPercent / 5), 1);
                bgd += Math.Round((thirdMonth * maxPercent / 5), 1);

            }
            if (e.IsSummaryFooter && e.Column == colFirstMonth)
            {
                e.CustomValue = percentRemaining - bgd + emp;
            }
            else if (e.IsSummaryFooter && e.Column == colSecondMonth)
            {
                e.CustomValue = percentRemaining - bgd + tbp;

            }
            else if (e.IsSummaryFooter && e.Column == colThirdMonth)
            {
                e.CustomValue = percentRemaining;
            }
            //end TN.Binh update 13/10/2025
        }



        #region Công thức tính AGV
        //=======================================  ====================================================== -- 270525 -- lee min khooi -- update
        private void LoadEventForTKCK()
        {
            //xtraTabPage4.PageVisible = xtraTabPage5.PageVisible = xtraTabPage2.PageVisible = xtraTabPage6.PageVisible = false;
            //treeData.OptionsView.AutoWidth = true;

            xtraTabPage4.PageVisible = xtraTabPage5.PageVisible = xtraTabPage6.PageVisible = false; //LĐ.Dat update 2/10/25
            treeData.OptionsView.AutoWidth = true;



            colCoefficient.Visible = colEmployeeCoefficient.Visible = colTBPCoefficient.Visible = colPoint6General.Visible = colBGDCoefficient.Visible = colTBPPoint.Visible = colBGDPoint.Visible = false;
            colStandartPoint.Visible = true;
            colCoefficient1.Visible = false;

            // ============= Khởi tạo lại gridcontrol
            gridBand2.Visible = false;
            gridBand8.Visible = true;
        }

        private DataTable CalculatorAvgPoint_TKCK(DataTable dataTable)
        {
            if (dataTable.Rows == null) return dataTable;
            List<string> listFatherID = new List<string>();

            //TÌm danh sách sách các node cha
            foreach (DataRow row in dataTable.Rows)
            {
                string Stt = TextUtils.ToString(row["STT"]);
                if (string.IsNullOrWhiteSpace(Stt)) continue;
                string fatherID = Stt.Substring(0, Stt.LastIndexOf('.') > 0 ? Stt.LastIndexOf('.') : 1);
                bool isDuplicate = listFatherID.Any(p => p == fatherID);
                if (!isDuplicate) listFatherID.Add(fatherID);
            }

            for (int i = (listFatherID.Count - 1); i >= 0; i--)
            {
                string fatherId = listFatherID[i];
                int fatherRowIndex = -1;

                int count = 0;
                decimal totalempPoint = 0;
                decimal totaltbpPoint = 0;
                decimal totalbgdPointt = 0;
                decimal totalStandardPoint = 0; //LĐ.Dat update 2/10/25
                string startStt = fatherId + "."; //Các node con
                foreach (DataRow row in dataTable.Rows)
                {
                    string Stt = TextUtils.ToString(row["STT"]);
                    bool isCheck = listFatherID.Any(p => p == Stt);

                    if (string.IsNullOrWhiteSpace(Stt)) continue;
                    if (Stt == fatherId)
                    {
                        fatherRowIndex = dataTable.Rows.IndexOf(row); // Vị trí của node cha hiện tại

                        totaltbpPoint = TextUtils.ToDecimal(row["TBPEvaluation"]);
                        totalbgdPointt = TextUtils.ToDecimal(row["BGDEvaluation"]);
                    }
                    else if (Stt.StartsWith(startStt))
                    {
                        if (isCheck) continue;
                        totalempPoint += TextUtils.ToDecimal(row["EmployeePoint"]);
                        totaltbpPoint += TextUtils.ToDecimal(row["TBPPoint"]);
                        totalbgdPointt += TextUtils.ToDecimal(row["TBPPoint"]);
                        totalStandardPoint += TextUtils.ToDecimal(row["StandardPoint"]); //LĐ.Dat update 2/10/25
                        count++;
                    }
                }
                if (fatherRowIndex == -1 || count == 0) continue;
                // if (TextUtils.ToInt(cboPositionType.EditValue) == 2) count = 1;

                dataTable.Rows[fatherRowIndex]["EmployeeEvaluation"] = (decimal)(totalempPoint);
                dataTable.Rows[fatherRowIndex]["TBPEvaluation"] = (decimal)(totaltbpPoint);
                dataTable.Rows[fatherRowIndex]["BGDEvaluation"] = (decimal)(totalbgdPointt);
                dataTable.Rows[fatherRowIndex]["StandardPoint"] = (decimal)(totalStandardPoint); //LĐ.Dat update 2/10/25
            }
            dataTable = CalculatorTotalPoint_TKCK(dataTable);
            return dataTable;
        }
        private DataTable CalculatorTotalPoint_TKCK(DataTable dataTable)
        {
            List<DataRow> parentRow = dataTable.Select("ParentID = 0").ToList();
            foreach (DataRow row in parentRow)
            {
                int rowIndex = dataTable.Rows.IndexOf(row);
                List<DataRow> childrenRow = dataTable.Select($"ParentID = {row["ID"]}").ToList();
                decimal totalPoint = 0;
                decimal totalEmpPoint = 0;
                decimal totalTBPPoint = 0;
                decimal totalBGDPoint = 0;
                foreach (DataRow item in childrenRow)
                {
                    totalPoint += TextUtils.ToDecimal(item["StandardPoint"]);

                    totalEmpPoint += TextUtils.ToDecimal(item["EmployeeEvaluation"]);
                    totalTBPPoint += TextUtils.ToDecimal(item["TBPEvaluation"]);
                    totalBGDPoint += TextUtils.ToDecimal(item["BGDEvaluation"]);
                }

                dataTable.Rows[rowIndex]["StandardPoint"] = totalPoint;
                dataTable.Rows[rowIndex]["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";


                dataTable.Rows[rowIndex]["EmployeeEvaluation"] = totalEmpPoint;
                dataTable.Rows[rowIndex]["TBPEvaluation"] = totalTBPPoint;
                dataTable.Rows[rowIndex]["BGDEvaluation"] = totalBGDPoint;
            }

            return dataTable;
        }

        private void LoadSumaryRank_TKCK()
        {
            try
            {
                grdMaster.DataSource = null;

                decimal totalEmpSkillPoint = 0;
                decimal totalTBPSkillPoint = 0;
                decimal totalBGDSkillPoint = 0;
                decimal totalSkillPoint = 0;
                DataTable dtSkill = (DataTable)treeData.DataSource;


                //chuyen mon
                decimal totalEmpCMPoint = 0;
                decimal totalTBPCMPoint = 0;
                decimal totalBGDCMPoint = 0;
                decimal totalCMPoint = 0;

                DataTable dtChuyenMon = (DataTable)treeList1.DataSource;
                //endupdate

                if (dtSkill != null)
                {
                    List<DataRow> lstSkillPoint = dtSkill.Select("ID = -1").ToList();
                    foreach (DataRow item in lstSkillPoint)
                    {
                        totalSkillPoint += TextUtils.ToDecimal(item["StandardPoint"]);
                        totalEmpSkillPoint += TextUtils.ToDecimal(item["EmployeeEvaluation"]);
                        totalTBPSkillPoint += TextUtils.ToDecimal(item["TBPEvaluation"]);
                        totalBGDSkillPoint += TextUtils.ToDecimal(item["BGDEvaluation"]);
                    }
                }

                //TN.Binh update
                if (dtChuyenMon != null)
                {
                    List<DataRow> lstChuyenMonPoint = dtChuyenMon.Select("ID = -1").ToList();
                    foreach (DataRow item in lstChuyenMonPoint)
                    {
                        totalCMPoint += TextUtils.ToDecimal(item["StandardPoint"]);
                        totalEmpCMPoint += TextUtils.ToDecimal(item["EmployeeEvaluation"]);
                        totalTBPCMPoint += TextUtils.ToDecimal(item["TBPEvaluation"]);
                        totalBGDCMPoint += TextUtils.ToDecimal(item["BGDEvaluation"]);
                    }
                }

                totalSkillPoint = totalSkillPoint > 0 ? totalSkillPoint : 1;
                List<EvaluationSummary> data = new List<EvaluationSummary>()
                {
                    new EvaluationSummary
                    {
                        EvaluatedType = "Tự đánh giá",
                        SkillPoint = totalEmpSkillPoint,
                        //PercentageAchieved = Math.Round((totalEmpSkillPoint / totalSkillPoint) * 100, 2),
                        PercentageAchieved = Math.Round(((totalEmpSkillPoint + totalEmpCMPoint) / (totalSkillPoint + totalCMPoint)) * 100, 2),
                        EvaluationRank = GetEvaluationRank_TKCK(Math.Round((totalEmpSkillPoint / totalSkillPoint) * 100, 2)),
                        //StandartPoint = totalSkillPoint,
                        SpecializationPoint = totalEmpCMPoint,
                        StandartPoint = totalSkillPoint + totalCMPoint, // TN.Binh update 02/10/25
                    },
                    new EvaluationSummary
                    {
                        EvaluatedType = "Đánh giá của Trưởng/Phó BP",
                        SkillPoint = totalTBPSkillPoint ,
                        //PercentageAchieved = Math.Round((totalTBPSkillPoint / totalSkillPoint) * 100, 2),
                        PercentageAchieved = Math.Round(( (totalTBPSkillPoint+totalTBPCMPoint) / (totalSkillPoint+totalCMPoint)) * 100, 2),
                        EvaluationRank = GetEvaluationRank_TKCK(Math.Round((totalTBPSkillPoint / totalSkillPoint) * 100, 2)),
                        //StandartPoint = totalSkillPoint,
                        SpecializationPoint = totalTBPCMPoint,
                        StandartPoint = totalSkillPoint + totalCMPoint, // TN.Binh update 02/10/25
                    },
                     new EvaluationSummary
                    {
                        EvaluatedType = "Đánh giá của GĐ",
                        SkillPoint = totalBGDSkillPoint ,
                        //PercentageAchieved = Math.Round((totalBGDSkillPoint / totalSkillPoint) * 100, 2),
                        PercentageAchieved = Math.Round(((totalBGDSkillPoint+totalBGDCMPoint) / (totalSkillPoint+totalCMPoint)) * 100, 2),
                        EvaluationRank = GetEvaluationRank_TKCK(Math.Round((totalBGDSkillPoint / totalSkillPoint) * 100, 2)),
                        //StandartPoint = totalSkillPoint,
                        SpecializationPoint = totalBGDCMPoint,
                        StandartPoint = totalSkillPoint + totalCMPoint, // TN.Binh update 02/10/25

                    },

                };
                grdMaster.DataSource = data;
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private string GetEvaluationRank_TKCK(decimal totalPercent)
        {
            try
            {
                if (totalPercent < 60) return "D";
                if (totalPercent < 65) return "C-";
                if (totalPercent < 70) return "C";
                if (totalPercent < 75) return "C+";
                if (totalPercent < 80) return "B-";
                if (totalPercent < 85) return "B";
                if (totalPercent < 90) return "B+";
                if (totalPercent < 95) return "A-";
                if (totalPercent < 100) return "A";
                return "A+";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        #endregion


        #region tính tổng điểm mới TNB update 11/09/25
        private void LoadTotalAVGNew()
        {
            DataTable dtSkill = (DataTable)treeData.DataSource;
            List<DataRow> lstSkillPoint = dtSkill.Select("ID = -1").ToList();
            decimal totalEmpSkillPoint = 0;
            decimal totalTBPSkillPoint = 0;
            decimal totalBGDSkillPoint = 0;
            int countSkillPoint = lstSkillPoint.Count > 0 ? lstSkillPoint.Count : 1;
            foreach (DataRow item in lstSkillPoint)
            {
                //totalEmpSkillPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
                //totalTBPSkillPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
                //totalBGDSkillPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);

                totalEmpSkillPoint += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(item["EmployeeEvaluation"]), 1);
                totalTBPSkillPoint += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(item["TBPEvaluation"]), 1);
                totalBGDSkillPoint += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(item["BGDEvaluation"]), 1);
            }


            #region tính điểm chuyên môn mới
            // Phần chuyên môn (thay đổi: lấy parent row ID = -1 giống kỹ năng, bỏ phân loại SpecializationType)
            DataTable dtSpecialization = (DataTable)treeList1.DataSource;
            List<DataRow> lstSpecializationPoint = dtSpecialization.Select("ID = -1").ToList();
            //List<DataRow> lstSpecializationPoint = dtSpecialization.Select("ID = -1").ToList();
            decimal totalEmpSpecializationPoint = 0;
            decimal totalTBPSpecializationPoint = 0;
            decimal totalBGDSpecializationPoint = 0;
            int countSpecializationPoint = lstSpecializationPoint.Count > 0 ? lstSpecializationPoint.Count : 1;
            foreach (DataRow item in lstSpecializationPoint)
            {
                //totalEmpSpecializationPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
                //totalTBPSpecializationPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
                //totalBGDSpecializationPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);

                totalEmpSpecializationPoint += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(item["EmployeeEvaluation"]), 1);
                totalTBPSpecializationPoint += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(item["TBPEvaluation"]), 1);
                totalBGDSpecializationPoint += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(item["BGDEvaluation"]), 1);
            }
            #endregion


            DataTable dtGeneral = (DataTable)treeList2.DataSource;
            List<DataRow> lstGeneralPoint = dtGeneral.Select("ID = -1").ToList();
            decimal totalEmpGeneralPoint = 0;
            decimal totalTBPGeneralPoint = 0;
            decimal totalBGDGeneralPoint = 0;
            int countGeneralPoint = lstGeneralPoint.Count > 0 ? lstGeneralPoint.Count : 1;
            foreach (DataRow item in lstGeneralPoint)
            {
                //totalEmpGeneralPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
                //totalTBPGeneralPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
                //totalBGDGeneralPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);

                totalEmpGeneralPoint += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(item["EmployeeEvaluation"]), 1);
                totalTBPGeneralPoint += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(item["TBPEvaluation"]), 1);
                totalBGDGeneralPoint += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(item["BGDEvaluation"]), 1);
            }

            List<object> data = new List<object>()
             {
                 new
                 {
                     EvaluatedType = "Tự đánh giá",
                     SkillPoint = totalEmpSkillPoint / countSkillPoint,
                     SpecializationPoint = totalEmpSpecializationPoint / countSpecializationPoint, // Thay mới
                     GeneralPoint = totalEmpGeneralPoint / countGeneralPoint
                 },
                 new
                 {
                     EvaluatedType = "Đánh giá của Trưởng/Phó BP",
                     SkillPoint = totalTBPSkillPoint / countSkillPoint,
                     SpecializationPoint = totalTBPSpecializationPoint / countSpecializationPoint, // Thay mới
                     GeneralPoint = totalTBPGeneralPoint / countGeneralPoint
                 },
                  new
                 {
                     EvaluatedType = "Đánh giá của GĐ",
                     SkillPoint = totalBGDSkillPoint / countSkillPoint,
                     SpecializationPoint = totalBGDSpecializationPoint / countSpecializationPoint, // Thay mới
                     GeneralPoint = totalBGDGeneralPoint / countGeneralPoint
                 },

             };
            grdMaster.DataSource = data;
        }
        #endregion
        #region hàm tính điểm đánh giá, điểm hệ số mới TNB update 11/09/25
        private DataTable CalculatorAvgPointNew(DataTable dataTable, bool isSpecial = false)
        {
            if (dataTable.Rows == null) return dataTable;
            List<string> listFatherID = new List<string>();
            foreach (DataRow row in dataTable.Rows)
            {
                string Stt = TextUtils.ToString(row["STT"]);
                if (string.IsNullOrWhiteSpace(Stt)) continue;
                string fatherID = Stt.Substring(0, Stt.LastIndexOf('.') > 0 ? Stt.LastIndexOf('.') : 1);
                bool isDuplicate = listFatherID.Any(p => p == fatherID);
                if (!isDuplicate) listFatherID.Add(fatherID);
            }

            for (int i = (listFatherID.Count - 1); i >= 0; i--)
            {
                string fatherId = listFatherID[i];
                int fatherRowIndex = -1;
                decimal coefficient = 0;

                decimal totalcoefficientNew = 0; //TN.Binh update

                int count = 0;
                decimal totalempPoint = 0;
                decimal totalcoefficient = 0;
                decimal totaltbpPoint = 0;
                decimal totalbgdPointt = 0;
                string startStt = fatherId + ".";
                foreach (DataRow row in dataTable.Rows)
                {
                    string Stt = TextUtils.ToString(row["STT"]);
                    bool isCheck = listFatherID.Any(p => p == Stt);

                    if (string.IsNullOrWhiteSpace(Stt)) continue;
                    if (Stt == fatherId)
                    {
                        fatherRowIndex = dataTable.Rows.IndexOf(row);
                        coefficient = TextUtils.ToDecimal(row["Coefficient"]);
                    }
                    else if (Stt.StartsWith(startStt))
                    {
                        if (Stt.LastIndexOf('.') == 1)  //TN.Binh update
                        {
                            totalcoefficientNew += TextUtils.ToDecimal(row["Coefficient"]);
                        }
                        if (isCheck) continue;
                        count++;
                        totalempPoint += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["EmployeeCoefficient"]), 1);
                        totaltbpPoint += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["TBPCoefficient"]), 1);
                        totalbgdPointt += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["BGDCoefficient"]), 1);
                        totalcoefficient += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["Coefficient"]), 1);
                    }
                }
                if (fatherRowIndex == -1 || count == 0) continue;
                // if (TextUtils.ToInt(cboPositionType.EditValue) == 2) count = 1;

                //cập nhập điểm đánh giá
                if (totalcoefficient == 0)
                {
                    dataTable.Rows[fatherRowIndex]["EmployeeEvaluation"] = (decimal)(totalempPoint / count);
                    dataTable.Rows[fatherRowIndex]["BGDEvaluation"] = (decimal)(totalbgdPointt / count);
                    dataTable.Rows[fatherRowIndex]["TBPEvaluation"] = (decimal)(totaltbpPoint / count);
                }
                else
                {
                    dataTable.Rows[fatherRowIndex]["EmployeeEvaluation"] = (decimal)(totalempPoint / totalcoefficient);
                    dataTable.Rows[fatherRowIndex]["BGDEvaluation"] = (decimal)(totalbgdPointt / totalcoefficient);
                    dataTable.Rows[fatherRowIndex]["TBPEvaluation"] = (decimal)(totaltbpPoint / totalcoefficient);
                    if (startStt.Replace(".", "").Length == 1 && isSpecial) dataTable.Rows[fatherRowIndex]["Coefficient"] = totalcoefficientNew;  //TN.Binh update
                }

                // cap nhat lại điểm theo hệ số
                if (dataTable.Rows[fatherRowIndex]["TBPEvaluation"] != DBNull.Value && dataTable.Rows[fatherRowIndex]["Coefficient"] != DBNull.Value)
                {
                    dataTable.Rows[fatherRowIndex]["TBPCoefficient"] = TextUtils.ToDecimal(dataTable.Rows[fatherRowIndex]["TBPEvaluation"]) * TextUtils.ToDecimal(dataTable.Rows[fatherRowIndex]["Coefficient"]);
                }
                if (dataTable.Rows[fatherRowIndex]["EmployeeEvaluation"] != DBNull.Value && dataTable.Rows[fatherRowIndex]["Coefficient"] != DBNull.Value)
                {
                    dataTable.Rows[fatherRowIndex]["EmployeeCoefficient"] = TextUtils.ToDecimal(dataTable.Rows[fatherRowIndex]["EmployeeEvaluation"]) * TextUtils.ToDecimal(dataTable.Rows[fatherRowIndex]["Coefficient"]);
                }
                if (dataTable.Rows[fatherRowIndex]["BGDEvaluation"] != DBNull.Value && dataTable.Rows[fatherRowIndex]["Coefficient"] != DBNull.Value)
                {
                    dataTable.Rows[fatherRowIndex]["BGDCoefficient"] = TextUtils.ToDecimal(dataTable.Rows[fatherRowIndex]["BGDEvaluation"]) * TextUtils.ToDecimal(dataTable.Rows[fatherRowIndex]["Coefficient"]);
                }
            }
            dataTable = CalculatorTotalPointNew(dataTable);
            return dataTable;
        }
        #endregion
        #region hàm tính tồng điểm hệ số (dòng đặc biệt)  update 11/09/25
        private DataTable CalculatorTotalPointNew(DataTable dataTable)
        {
            List<DataRow> parentRow = dataTable.Select("ParentID = 0").ToList();
            foreach (DataRow row in parentRow)
            {
                int rowIndex = dataTable.Rows.IndexOf(row);
                List<DataRow> childrenRow = dataTable.Select($"ParentID = {row["ID"]}").ToList();
                decimal totalCoefficient = 0;
                decimal totalEmpAVGPoint = 0;
                decimal totalTBPAVGPoint = 0;
                decimal totalBGDAVGPoint = 0;

                foreach (DataRow item in childrenRow)
                {
                    totalCoefficient += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(item["Coefficient"]), 1);
                    totalEmpAVGPoint += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(item["EmployeeCoefficient"]), 1);
                    totalTBPAVGPoint += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(item["TBPCoefficient"]), 1);
                    totalBGDAVGPoint += TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(item["BGDCoefficient"]), 1);

                    //var nodeChild = dataTable.Select($"ParentID = {item["ID"]}");
                    //if (totalCoefficient == 0) totalCoefficient = TextUtils.ToDecimal(nodeChild.AsEnumerable().Sum(x => x.Field<int>("Coefficient")));
                    //if (totalEmpAVGPoint == 0) totalEmpAVGPoint = TextUtils.ToDecimal(nodeChild.AsEnumerable().Sum(x => x.Field<decimal>("EmployeeCoefficient")));
                    //if (totalTBPAVGPoint == 0) totalTBPAVGPoint = TextUtils.ToDecimal(nodeChild.AsEnumerable().Sum(x => x.Field<decimal>("TBPCoefficient")));
                    //if (totalBGDAVGPoint == 0) totalBGDAVGPoint = TextUtils.ToDecimal(nodeChild.AsEnumerable().Sum(x => x.Field<decimal>("BGDCoefficient")));
                }

                dataTable.Rows[rowIndex]["Coefficient"] = totalCoefficient;
                dataTable.Rows[rowIndex]["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
                totalCoefficient = totalCoefficient > 0 ? totalCoefficient : 1;

                //TN.Binh update tính lại tổng hệ số dòng đặc biệt 08/09/25
                dataTable.Rows[rowIndex]["EmployeeCoefficient"] = (decimal)(totalEmpAVGPoint);
                dataTable.Rows[rowIndex]["TBPCoefficient"] = (decimal)(totalTBPAVGPoint);
                dataTable.Rows[rowIndex]["BGDCoefficient"] = (decimal)(totalBGDAVGPoint);

                //TN.Binh update tính lại tổng điểm đánh trung bình dòng đặc biệt 08/09/25
                dataTable.Rows[rowIndex]["EmployeeEvaluation"] = (decimal)(totalEmpAVGPoint / totalCoefficient);
                dataTable.Rows[rowIndex]["BGDEvaluation"] = (decimal)(totalBGDAVGPoint / totalCoefficient);
                dataTable.Rows[rowIndex]["TBPEvaluation"] = (decimal)(totalTBPAVGPoint / totalCoefficient);
            }

            return dataTable;
        }
        #endregion

        private void LoadPointRuleNew(int empPointID)
        {


            //List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointID",
            //                                                   new string[] { "@KPIEmployeePointID" },
            //                                                   new object[] { empPointID });

            List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointIDNew",
                                                                       new string[] { "@KPIEmployeePointID" },
                                                                       new object[] { empPointID });

            decimal timeWork = TextUtils.ToDecimal(grvTeam.Columns["TimeWork"].SummaryItem.SummaryValue);
            decimal fiveS = TextUtils.ToDecimal(grvTeam.Columns["FiveS"].SummaryItem.SummaryValue);
            decimal reportWork = TextUtils.ToDecimal(grvTeam.Columns["ReportWork"].SummaryItem.SummaryValue);
            //decimal customerComplaint = TextUtils.ToDecimal(grvTeam.Columns["CustomerComplaint"].SummaryItem.SummaryValue);
            decimal customerComplaint = TextUtils.ToDecimal(grvTeam.Columns["ComplaneAndMissing"].SummaryItem.SummaryValue);
            decimal deadlineDelay = TextUtils.ToDecimal(grvTeam.Columns["DeadlineDelay"].SummaryItem.SummaryValue);
            decimal teamKPIKyNang = TextUtils.ToDecimal(grvTeam.Columns["KPIKyNang"].SummaryItem.SummaryValue);
            decimal teanKPIChung = TextUtils.ToDecimal(grvTeam.Columns["KPIChung"].SummaryItem.SummaryValue);
            decimal teamKPIPLC = TextUtils.ToDecimal(grvTeam.Columns["KPIPLC"].SummaryItem.SummaryValue);
            decimal teamKPIVISION = TextUtils.ToDecimal(grvTeam.Columns["KPIVision"].SummaryItem.SummaryValue);
            decimal teamKPISOFTWARE = TextUtils.ToDecimal(grvTeam.Columns["KPISoftware"].SummaryItem.SummaryValue);
            decimal missingTool = TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);  //làm mất mát hỏng thiết bị 12/12/2024
                                                                                                                 //TN.Binh update 
            decimal teamKPIChuyenMon = TextUtils.ToDecimal(grvTeam.Columns[colKPIChuyenMon.FieldName].SummaryItem.SummaryValue);
            //decimal TEAM21 = TextUtils.ToDecimal(grvTeam.Columns["ComplaneAndMissing"].SummaryItem.SummaryValue);
            //decimal TEAM214 = TextUtils.ToDecimal(grvTeam.Columns["DeadlineDelay"].SummaryItem.SummaryValue);
            //end
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
                     new KPISumarizeDTO(){ EvaluationCode = "TEAM04", ThirdMonth = customerComplaint  + deadlineDelay},//update  12/12/2024
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM05", ThirdMonth = customerComplaint}, //update  12/12/2024
                    //new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = missingTool},//update  12/12/2024
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = deadlineDelay},//update  12/12/2024
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIKYNANG", ThirdMonth = teamKPIKyNang},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIChung", ThirdMonth = teanKPIChung},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIPLC", ThirdMonth = teamKPIPLC},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIVISION", ThirdMonth = teamKPIVISION},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPISOFTWARE", ThirdMonth = teamKPISOFTWARE},
                    //TN.Binh update
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPICHUYENMON", ThirdMonth = teamKPIChuyenMon},
                    //new KPISumarizeDTO(){ EvaluationCode = "TEAM214", ThirdMonth = TEAM214},
                    //new KPISumarizeDTO(){ EvaluationCode = "TEAM21", ThirdMonth = TEAM21},
                    //end
                    new KPISumarizeDTO(){ EvaluationCode = "MA11", ThirdMonth = totalErrorTBP}, // update 13/12/2024
                });


            Lib.LockEvents = true;
            foreach (KPISumarizeDTO item in lstResult)
            {
                TreeListNode node = tlKPIRule.GetNodeList().FirstOrDefault(x => item.EvaluationCode == TextUtils.ToString(x.GetValue(colEvaluationCode)));
                if (node == null) continue;
                node.SetValue(colFirstMonth, item.FirstMonth);
                node.SetValue(colSecondMonth, item.SecondMonth);
                node.SetValue(colThirdMonth, item.ThirdMonth);
            }

            Lib.LockEvents = false;

            //CalculatorPoint(empPointID);
        }


        private void LoadKPIKyNangNew(int kpiExamID, bool isPublic)
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
                                                        new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                                                        new object[] { employeeID, 1, kpiExamID, isPublic });

            DataRow parentRow = list.NewRow();
            parentRow["ID"] = -1;
            parentRow["ParentID"] = 0;
            parentRow["EvaluationContent"] = "TỔNG HỆ SỐ";
            parentRow["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH ";
            list.AcceptChanges();
            list.Rows.Add(parentRow);

            treeData.DataSource = departmentID != departmentCK ? CalculatorAvgPointNew(list) : CalculatorAvgPoint_TKCK(list);
            treeData.ExpandAll();
        }
        private void LoadKPIChungNew(int kpiExamID, bool isPublic)
        {
            DataTable list2 = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
                                                       new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                                                       new object[] { employeeID, 3, kpiExamID, isPublic });
            DataRow parentRow2 = list2.NewRow();
            parentRow2["ID"] = -1;
            parentRow2["ParentID"] = 0;
            parentRow2["EvaluationContent"] = "TỔNG HỆ SỐ";
            parentRow2["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
            list2.AcceptChanges();
            list2.Rows.Add(parentRow2);
            treeList2.DataSource = CalculatorAvgPointNew(list2);
            treeList2.ExpandAll();
        }
        private void LoadKPIChuyenMonNew(int kpiExamID, bool isPublic)
        {
            DataTable list3 = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
                                                        new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                                                        new object[] { employeeID, 2, kpiExamID, isPublic });

            DataRow parentRow3 = list3.NewRow();
            parentRow3["ID"] = -1;
            parentRow3["ParentID"] = 0;
            parentRow3["EvaluationContent"] = "TỔNG HỆ SỐ";
            parentRow3["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
            list3.AcceptChanges();
            list3.Rows.Add(parentRow3);

            //treeList1.DataSource = CalculatorAvgPointNew(list3, true);
            treeList1.DataSource = departmentID != departmentCK ? CalculatorAvgPointNew(list3, true) : CalculatorAvgPoint_TKCK(list3);

            treeList1.ExpandAll();
        }

        private void LoadSummaryRuleNew(int empPointID, bool isPublic)
        {
            bool isAdminConfirm = TextUtils.ToBoolean(grvExam.GetFocusedRowCellValue(colIsAdminConfirm));
            //bool isAdminConfirm = false;
            LoadKPIRule(empPointID, isPublic);
            // ========================= 09/12/2024 =======================================================
            List<KPIEmployeePointDetailModel> lst = SQLHelper<KPIEmployeePointDetailModel>.FindByAttribute("KPIEmployeePointID", empPointID);
            if (lst.Count <= 0)
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
                {
                    LoadPointRuleNew(empPointID);
                }
            }
            else if (!isAdminConfirm)
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
                {
                    LoadPointRuleLastMonthNew(empPointID, isPublic);
                }
            }
            //LoadPointRuleNew(empPointID); //TN.Binh update 
            CalculatorPoint(empPointID);
        }


        #region
        //TN.Binh update 24/09/2025
        private void LoadKPIPosition(int kpiSession)
        {

            List<KPIPositionModel> lst = SQLHelper<KPIPositionModel>.ProcedureToList("spGetPoistionByEmployeeID", new string[] { "@KPISessionID", "@EmployeeID" },
                                                                                                  new object[] { kpiSession, Global.EmployeeID });
            cboChoicePosition.Properties.DataSource = lst;
            cboChoicePosition.Properties.DisplayMember = "PositionName";
            cboChoicePosition.Properties.ValueMember = "ID";
        }

        //private void grvSession_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        //    int kpiSessionID = TextUtils.ToInt(
        //   grvSession.GetRowCellValue(e.FocusedRowHandle, colSessionID));

        //    LoadKPIPosition(kpiSessionID);  //TN.Binh update 23/09/25

        //    //List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(p => p.ID).ToList();

        //    //TN.Binh update 23/09/25
        //    List<KPIPositionEmployeeModel> employee = SQLHelper<KPIPositionEmployeeModel>.ProcedureToList("spGetEmployeeInKPISession", new string[] { "@KPISessionID", "@EmployeeID" },
        //                                                                                           new object[] { kpiSessionID, Global.EmployeeID });
        //    if (employee.Count > 0)
        //    {
        //        cboChoicePosition.EditValue = employee[0].KPIPosiotionID;
        //        cboChoicePosition.Properties.ReadOnly = true;
        //        btnChoicePosition.Enabled = false;
        //        string sessionName = TextUtils.ToString(grvSession.GetRowCellValue(e.FocusedRowHandle, colName));
        //        employeeName = string.IsNullOrWhiteSpace(employeeName) ? "" : $" - {employeeName.ToUpper()}";
        //        LoadKpiExam();
        //        lblSession.Text = sessionName.ToUpper() + $"{employeeName}";
        //    }
        //    else
        //    {
        //        cboChoicePosition.Properties.ReadOnly = false;
        //        cboChoicePosition.EditValue = null;
        //        btnChoicePosition.Enabled = true;
        //    }
        //    //endupdate
        //    //LoadDataDetails();

        //}
        private void btnChoicePosition_Click(object sender, EventArgs e)
        {
            if (cboChoicePosition.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn vị trí của bạn!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboChoicePosition.Focus();
                return;
            }

            int positionID = Convert.ToInt32(cboChoicePosition.EditValue); // ID
            string positionName = cboChoicePosition.Text;
            bool isChoice = MessageBox.Show($"Bạn có muốn xác nhận vị trí [{positionName}] cho kỳ đánh giá này không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            if (isChoice)
            {
                KPIPositionEmployeeModel newModel = new KPIPositionEmployeeModel()
                {
                    KPIPosiotionID = positionID,
                    EmployeeID = Global.EmployeeID,
                };
                SQLHelper<KPIPositionEmployeeModel>.Insert(newModel);
                cboChoicePosition.Properties.ReadOnly = true;
                btnChoicePosition.Enabled = false;
                LoadKpiExam();

            }
        }
        #endregion

        #region LoadPointRuleLastMonthNew
        private void LoadPointRuleLastMonthNew(int empPointID, bool isPublic)
        {
            try
            {
                //List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointID",
                //                                                       new string[] { "@KPIEmployeePointID" },
                //                                                       new object[] { empPointID });

                List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointIDNew",
                                                                       new string[] { "@KPIEmployeePointID", "@IsPublic" },
                                                                       new object[] { empPointID, isPublic });

                decimal timeWork = TextUtils.ToDecimal(grvTeam.Columns["TimeWork"].SummaryItem.SummaryValue);
                decimal fiveS = TextUtils.ToDecimal(grvTeam.Columns["FiveS"].SummaryItem.SummaryValue);
                decimal reportWork = TextUtils.ToDecimal(grvTeam.Columns["ReportWork"].SummaryItem.SummaryValue);
                //decimal customerComplaint = TextUtils.ToDecimal(grvTeam.Columns["CustomerComplaint"].SummaryItem.SummaryValue);
                decimal customerComplaint = TextUtils.ToDecimal(grvTeam.Columns["ComplaneAndMissing"].SummaryItem.SummaryValue);
                decimal deadlineDelay = TextUtils.ToDecimal(grvTeam.Columns["DeadlineDelay"].SummaryItem.SummaryValue);
                decimal teamKPIKyNang = TextUtils.ToDecimal(grvTeam.Columns["KPIKyNang"].SummaryItem.SummaryValue);
                decimal teanKPIChung = TextUtils.ToDecimal(grvTeam.Columns["KPIChung"].SummaryItem.SummaryValue);
                decimal teamKPIPLC = TextUtils.ToDecimal(grvTeam.Columns["KPIPLC"].SummaryItem.SummaryValue);
                decimal teamKPIVISION = TextUtils.ToDecimal(grvTeam.Columns["KPIVision"].SummaryItem.SummaryValue);
                decimal teamKPISOFTWARE = TextUtils.ToDecimal(grvTeam.Columns["KPISoftware"].SummaryItem.SummaryValue);
                decimal teamKPICHUYENMON = TextUtils.ToDecimal(grvTeam.Columns["KPIChuyenMon"].SummaryItem.SummaryValue);
                decimal missingTool = TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);  //làm mất mát hỏng thiết bị 12/12/2024
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
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPICHUYENMON", ThirdMonth = teamKPICHUYENMON},
                });


                Lib.LockEvents = true;
                foreach (KPISumarizeDTO item in lstResult)
                {
                    TreeListNode node = tlKPIRule.GetNodeList().FirstOrDefault(x => item.EvaluationCode == TextUtils.ToString(x.GetValue(colEvaluationCode)));
                    if (node == null) continue;
                    //node.SetValue(colFirstMonth, item.FirstMonth);
                    //node.SetValue(colSecondMonth, item.SecondMonth);
                    node.SetValue(colThirdMonth, item.ThirdMonth);
                }

                Lib.LockEvents = false;

                //CalculatorPoint(empPointID);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        #endregion

        public class EvaluationSummary
        {
            public string EvaluatedType { get; set; } = string.Empty;
            public decimal SkillPoint { get; set; }
            public decimal SpecializationPoint { get; set; }
            public string EvaluationRank { get; set; } = string.Empty;
            public decimal StandartPoint { get; set; }
            public decimal PercentageAchieved { get; set; }
        }

        //TN.Binh update 14/10/25
        private string GetDisplayText(decimal percent)
        {
            string totalPercentText = "";
            if (percent < 60) totalPercentText = " D";
            else if (percent >= 60 && percent < 65) totalPercentText = " C-";
            else if (percent >= 65 && percent < 70) totalPercentText = " C";
            else if (percent >= 70 && percent < 75) totalPercentText = " C+";
            else if (percent >= 75 && percent < 80) totalPercentText = " B-";
            else if (percent >= 80 && percent < 85) totalPercentText = " B";
            else if (percent >= 85 && percent < 90) totalPercentText = " B+";
            else if (percent >= 90 && percent < 95) totalPercentText = " A-";
            else if (percent >= 95 && percent < 100) totalPercentText = " A";
            else if (percent >= 100) totalPercentText = " A+";
            return totalPercentText;
        }
        private void tlKPIRule_CustomDrawFooterCell(object sender, CustomDrawFooterCellEventArgs e)
        {
            int kpiSessionID = TextUtils.ToInt(grvSession.GetFocusedRowCellValue(colSessionID));
            //int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            int empID = Global.EmployeeID;

            //Get possition của nhân viên
            var expPoint1 = new Expression(KPIPositionEmployeeModel_Enum.EmployeeID, empID);
            var expPoint2 = new Expression(KPIPositionModel_Enum.KPISessionID, kpiSessionID);
            var expPoint3 = new Expression(KPIPositionEmployeeModel_Enum.IsDeleted, 0);

            var kpiPositions = SQLHelper<KPIPositionModel>.FindByExpression(expPoint2.And(expPoint3));
            var kpiPositionEmployees = SQLHelper<KPIPositionEmployeeModel>.FindByExpression(expPoint1.And(expPoint3));

            var positionEmp = (from p in kpiPositions
                               join pe in kpiPositionEmployees on p.ID equals pe.KPIPosiotionID
                               select pe)
                     .FirstOrDefault() ?? new KPIPositionEmployeeModel();


            KPIPositionModel position = kpiPositions.FirstOrDefault(x => x.ID == positionEmp.KPIPosiotionID) ?? new KPIPositionModel();
            Expression ex1 = new Expression("KPISessionID", kpiSessionID);
            Expression ex2 = new Expression("KPIPositionID", TextUtils.ToInt(positionEmp.KPIPosiotionID) > 0 ? TextUtils.ToInt(positionEmp.KPIPosiotionID) : 1);
            Expression ex3 = new Expression("IsDeleted", 0);
            KPIEvaluationRuleModel kpiRule = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPIEvaluationRuleModel();

            int empPointID = GetKPIEmployeePointID(kpiRule.ID);
            KPIEmployeePointModel kPIEmployeePoint = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);

            if (e.Column != colPercentRemaining)
                return;

            string sumText = $"Tổng điểm: {e.Info.DisplayText} ({GetDisplayText(Convert.ToDecimal(e.Info.DisplayText))})" ;
            string customText = $"Điểm cuối cùng: {kPIEmployeePoint.TotalPercentActual} ({GetDisplayText(TextUtils.ToDecimal(kPIEmployeePoint.TotalPercentActual))})" ;

            GraphicsCache cache = e.Cache;
            Rectangle bounds = e.Bounds;
            bounds.Height += 10;
            using (SolidBrush backgroundBrush = new SolidBrush(Color.White))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            /* //  Viền đen (4 cạnh)
             using (Pen borderPen = new Pen(Color.Gray, 1))
             {
                 e.Graphics.DrawRectangle(borderPen, e.Bounds);
             }*/

            // Vẽ 2 dòng 
            using (Brush textBrush = new SolidBrush(e.Appearance.ForeColor))
            {
                float lineHeight = e.Appearance.Font.GetHeight(e.Graphics);
                float totalHeight = lineHeight * 2;
                float startY = e.Bounds.Top + (e.Bounds.Height - totalHeight) / 2;

                e.Graphics.DrawString(sumText, e.Appearance.Font, textBrush, e.Bounds.Left + 3, startY + 2);
                e.Graphics.DrawString(customText, e.Appearance.Font, Brushes.Red, e.Bounds.Left + 3, startY + lineHeight);
            }


            e.Handled = true;

        }
    }
}