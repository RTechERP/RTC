using DevExpress.Utils;
using BaseBusiness.DTO;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
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
using BMS.Business;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraTab;
using DevExpress.XtraSpreadsheet.Commands;
using DocumentFormat.OpenXml.Bibliography;
using System.IO;
using DevExpress.Utils.Drawing;

namespace BMS
{
    public partial class frmKPIEvaluationFactorScoring : _Forms
    {
        public int departmentID = 0;
        bool isUpdate = false;

        /// <summary>
        /// 2: Trưởng / Phó bộ phận
        /// 3: Ban giám đốc
        /// 4: Admin Kỹ thuật
        /// </summary>
        public int typeID = 0;

        int[] _departmentCoKhiLRs = new int[] { 10, 23 };
        public frmKPIEvaluationFactorScoring()
        {
            InitializeComponent();
        }

        private void frmKPIEvaluationFactorScoring_Load(object sender, EventArgs e)
        {
            //if (departmentID == departmentCoKhi) LoadEventForTKCK();


            btnAdminApprove.Visible = (Global.IsAdmin && Global.EmployeeID <= 0);
            btnSuccessKPI.Visible = (Global.IsAdmin && Global.EmployeeID <= 0);

            LoadDepartMent();

            if (_departmentCoKhiLRs.Contains(departmentID)) LoadEventForTKCK();
            LoadPositionType();
            LoadKPISession();
            LoadEValuationType();
            LoadStatus();
            LoadUserTeam();
            //LoadEmployee();
            //LoadKpiExam();

            if (typeID == 2)
            {
                colTBPPoint1.OptionsColumn.AllowEdit = true;
                colTBPPoint1.OptionsColumn.ReadOnly = false;

                colTBPPoint2.OptionsColumn.AllowEdit = true;
                colTBPPoint2.OptionsColumn.ReadOnly = false;

                colTBPPoint.OptionsColumn.AllowEdit = true;
                colTBPPoint.OptionsColumn.ReadOnly = false;

            }
            else if (typeID == 3)
            {
                colBGDPoint1.OptionsColumn.AllowEdit = true;
                colBGDPoint1.OptionsColumn.ReadOnly = false;

                colBGDPoint2.OptionsColumn.AllowEdit = true;
                colBGDPoint2.OptionsColumn.ReadOnly = false;

                colBGDPoint.OptionsColumn.AllowEdit = true;
                colBGDPoint.OptionsColumn.ReadOnly = false;
            }


            //Check nếu là admin
            foreach (ToolStripItem item in mnuMenu.Items)
            {
                if (item == btnEvaluatedRule || item == btnLoadDataTeam || item == btnAdminConfirm) continue;
                item.Visible = !(typeID == 4);
            }

            treeData.ContextMenuStrip = contextMenuStrip1; //TN.Binh update 

            //btnAdminConfirm.Visible = false;
        }
        private void LoadPositionType()
        {
            List<KPIPositionModel> lst = SQLHelper<KPIPositionModel>.FindByAttribute("IsDeleted", 0);
            cboPosition.DataSource = lst;
            cboPosition.ValueMember = "ID";
            cboPosition.DisplayMember = "PositionName";
        }
        private void LoadKpiExam()
        {
            try
            {
                departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
                int kpiSessionId = TextUtils.ToInt(cboKPISession.EditValue);
                //departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
                DataTable lst = SQLHelper<KPIExamModel>.LoadDataFromSP("spGetKPIExamByKPISessionID",
                    new string[] { "@KPISessionID", "@DepartmentID" },
                    new object[] { kpiSessionId, departmentID });
                grdExam.DataSource = lst;
                LoadEmployee();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"LoadKpiExam\r\n{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        private void LoadKPISession()
        {
            int year = DateTime.Now.Year;
            int quarter = (int)((DateTime.Now.Month + 2) / 3);
            departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            var exp1 = new Expression(KPISessionModel_Enum.DepartmentID, departmentID);
            var exp2 = new Expression(KPISessionModel_Enum.IsDeleted, 0);
            //List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(p => p.ID).ToList();
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByExpression(exp1.And(exp2)).OrderByDescending(p => p.QuarterEvaluation).ToList();
            cboKPISession.Properties.DataSource = lst;
            cboKPISession.Properties.DisplayMember = "Code";
            cboKPISession.Properties.ValueMember = "ID";
            KPISessionModel currentSession = lst.FirstOrDefault(p => p.YearEvaluation == year && p.QuarterEvaluation == quarter) ?? new KPISessionModel();
            cboKPISession.EditValue = currentSession.ID;
            LoadKpiExam();
        }

        private void LoadEmployee()
        {
            int kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));
            int evaluationType = TextUtils.ToInt(cboEvaluationType.EditValue);
            int status = TextUtils.ToInt(cboStatus.EditValue);
            int departMent = TextUtils.ToInt(cboDepartMent.EditValue);
            int userTeam = TextUtils.ToInt(cboUserTeam.EditValue);
            string keyWords = txtKeywords.Text.Trim();

            DataTable dt = SQLHelper<object>.LoadDataFromSP("spGetAllEmployeeKPIEvaluated",
                                                            new string[] { "@EvaluationType", "@DepartmentID", "@Keywords", "@Status", "@UserTeamID", "@KPIExamID" },
                                                            new object[] { evaluationType, departMent, keyWords, status, userTeam, kpiExamID });
            grdData.DataSource = dt;
            //LoadDataDetails();
            //grvData.FocusedRowHandle = 0;
        }

        private void LoadDataDetails()
        {
            //return;
            try
            {
                int focusRow = grvData.FocusedRowHandle;
                if (focusRow < 0) return;

                int empId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
                int kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));

                LoadKPIKyNang(empId, kpiExamID);
                LoadKPIChung(empId, kpiExamID);
                LoadKPIChuyenMon(empId, kpiExamID);
                //LoadTotalAVGNew();
                departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
                ////if (departmentID == departmentCoKhi) return;//-- 160525 -- lee min khooi -- update
                //if (_departmentCoKhiLRs.Contains(departmentID)) return;//-- 160525 -- lee min khooi -- update
                //LoadKPIRuleNew();


                //TN.Binh update
                if (!_departmentCoKhiLRs.Contains(departmentID))
                {
                    LoadTotalAVGNew();
                    LoadKPIRuleNew();
                }
                else
                {
                    LoadSumaryRank_TKCK();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        private void LoadUserTeam()
        {
            try
            {
                int departMent = TextUtils.ToInt(cboDepartMent.EditValue);
                //List<KPIEmployeeTeamModel> lst = SQLHelper<KPIEmployeeTeamModel>.FindByAttribute("DepartmentID", departMent).Where(p => typeID == 3 || Global.IsAdmin || p.LeaderID == Global.EmployeeID).ToList();

                //lst = lst.Select(c => { c.Name = c.Name.ToUpper(); return c; }).ToList();

                //cboUserTeam.Properties.DataSource = lst;
                //cboUserTeam.Properties.ValueMember = "ID";
                //cboUserTeam.Properties.DisplayMember = "Name";
                //cboUserTeam.EditValue = Global.UserTeamID;

                //int departMent = TextUtils.ToInt(cboDepartMent.EditValue);

                int kpiSessionId = TextUtils.ToInt(cboKPISession.EditValue);
                KPISessionModel kpiSession = SQLHelper<KPISessionModel>.FindByID(kpiSessionId);

                DataTable dt = TextUtils.LoadDataFromSP("spGetALLKPIEmployeeTeam", "A",
                                                            new string[] { "@YearValue", "@QuarterValue", "@DepartmentID" },
                                                            new object[] { kpiSession.YearEvaluation, kpiSession.QuarterEvaluation, departMent });


                var dataCombo = dt;
                //var datafiltered = dt.AsEnumerable().Where(r => typeID == 3 || Global.IsAdmin || TextUtils.ToInt(r["LeaderID"]) == Global.EmployeeID);
                var datafiltered = dt.AsEnumerable().Where(r => typeID == 3 || Global.IsAdmin);
                if (datafiltered.Any()) dataCombo = datafiltered.CopyToDataTable();

                //var filteredRows = dt.AsEnumerable().Where(r => typeID == 3 || Global.IsAdmin || TextUtils.ToInt(r["LeaderID"]) == Global.EmployeeID).CopyToDataTable();
                DataRow all = dataCombo.NewRow();
                all["ID"] = 0;
                all["Name"] = "--Tất cả các Team--";
                dataCombo.Rows.InsertAt(all, 0);
                cboUserTeam.Properties.DataSource = dataCombo;
                cboUserTeam.Properties.ValueMember = "ID";
                cboUserTeam.Properties.DisplayMember = "Name";
                //cboUserTeam.EditValue = Global.UserTeamID;
                cboUserTeam.EditValue = 0; //TN.Binh update 14/20/25
            }
            catch (Exception ex)
            {
                MessageBox.Show($"LoadUserTeam\r\n{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        //private void LoadTotalAVG()
        //{
        //    departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
        //    //if (departmentID == departmentCoKhi) //-- 160525 -- lee min khooi -- update
        //    if (_departmentCoKhiLRs.Contains(departmentID))
        //    {
        //        LoadSumaryRank_TKCK();
        //        return;
        //    }
        //    try
        //    {
        //        grdMaster.DataSource = null;

        //        decimal totalEmpSkillPoint = 0;
        //        decimal totalTBPSkillPoint = 0;
        //        decimal totalBGDSkillPoint = 0;
        //        int countSkillPoint = 1;
        //        DataTable dtSkill = (DataTable)treeData.DataSource;

        //        if (dtSkill != null)
        //        {


        //            List<DataRow> lstSkillPoint = dtSkill.Select("ID = -1").ToList();

        //            countSkillPoint = lstSkillPoint.Count > 0 ? lstSkillPoint.Count : 1;
        //            foreach (DataRow item in lstSkillPoint)
        //            {
        //                totalEmpSkillPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
        //                totalTBPSkillPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
        //                totalBGDSkillPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
        //            }
        //        }

        //        decimal totalEmpPLCPoint = 0;
        //        decimal totalTBPPLCPoint = 0;
        //        decimal totalBGDPLCPoint = 0;
        //        int countPLC = 0;

        //        decimal totalEmpPVisionPoint = 0;
        //        decimal totalTBPVisionPoint = 0;
        //        decimal totalBGDPVisionPoint = 0;
        //        int countVision = 0;

        //        decimal totalEmpSoftPoint = 0;
        //        decimal totalTBPSoftPoint = 0;
        //        decimal totalBGDSoftPoint = 0;
        //        int countSoft = 0;


        //        decimal totalEmpViRobotPoint = 0;
        //        decimal totalTBPViRobotPoint = 0;
        //        decimal totalBGDViRobotPoint = 0;
        //        int countViRobot = 0;
        //        DataTable dt = (DataTable)treeList1.DataSource;
        //        if (dt != null)
        //        {
        //            List<DataRow> lstPoint = dt.Select("ParentID = 0").ToList();


        //            foreach (DataRow item in lstPoint)
        //            {
        //                if (TextUtils.ToInt(item["SpecializationType"]) == 2)
        //                {
        //                    totalEmpPLCPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
        //                    totalTBPPLCPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
        //                    totalBGDPLCPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
        //                    countPLC++;
        //                }
        //                else if (TextUtils.ToInt(item["SpecializationType"]) == 3)
        //                {
        //                    totalEmpPVisionPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
        //                    totalTBPVisionPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
        //                    totalBGDPVisionPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
        //                    countVision++;
        //                }
        //                else if (TextUtils.ToInt(item["SpecializationType"]) == 4)
        //                {
        //                    totalEmpSoftPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
        //                    totalTBPSoftPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
        //                    totalBGDSoftPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
        //                    countSoft++;
        //                }
        //                else if (TextUtils.ToInt(item["SpecializationType"]) == 5)
        //                {
        //                    totalEmpViRobotPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
        //                    totalTBPViRobotPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
        //                    totalBGDViRobotPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
        //                    countViRobot++;
        //                }
        //                else continue;
        //            }

        //        }
        //        countPLC = countPLC > 0 ? countPLC : 1;
        //        countVision = countVision > 0 ? countVision : 1;
        //        countSoft = countSoft > 0 ? countSoft : 1;
        //        countViRobot = countViRobot > 0 ? countViRobot : 1;

        //        decimal totalEmpGeneralPoint = 0;
        //        decimal totalTBPGeneralPoint = 0;
        //        decimal totalBGDGeneralPoint = 0;
        //        int countGeneralPoint = 1;
        //        DataTable dtGeneral = (DataTable)treeList2.DataSource;
        //        if (dtGeneral != null)
        //        {


        //            List<DataRow> lstGeneralPoint = dtGeneral.Select("ID = -1").ToList();
        //            //decimal totalEmpGeneralPoint = 0;
        //            //decimal totalTBPGeneralPoint = 0;
        //            //decimal totalBGDGeneralPoint = 0;
        //            countGeneralPoint = lstGeneralPoint.Count > 0 ? lstGeneralPoint.Count : 1;


        //            foreach (DataRow item in lstGeneralPoint)
        //            {
        //                totalEmpGeneralPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
        //                totalTBPGeneralPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
        //                totalBGDGeneralPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
        //            }
        //        }

        //        decimal plcEmpPoint = (2 * totalEmpPLCPoint / countPLC + totalEmpViRobotPoint / countViRobot) / 3;
        //        decimal visionEmpPoint = (2 * totalEmpPVisionPoint / countVision + totalEmpViRobotPoint / countViRobot) / 3;

        //        decimal plcTBPPoint = (2 * totalTBPPLCPoint / countPLC + totalTBPViRobotPoint / countViRobot) / 3;
        //        decimal visionTBPPoint = (2 * totalTBPVisionPoint / countVision + totalTBPViRobotPoint / countViRobot) / 3;


        //        decimal plcBGDPoint = (2 * totalBGDPLCPoint / countPLC + totalBGDViRobotPoint / countViRobot) / 3;
        //        decimal visionBGDPoint = (2 * totalBGDPVisionPoint / countVision + totalBGDViRobotPoint / countViRobot) / 3;
        //        List<object> data = new List<object>()
        //        {
        //            new
        //            {
        //                EvaluatedType = "Tự đánh giá",
        //                SkillPoint = totalEmpSkillPoint / countSkillPoint,
        //                PLCPoint = plcEmpPoint,
        //                VisionPoint = visionEmpPoint,
        //                SoftWarePoint = totalEmpSoftPoint / countSoft,
        //                AVGPoint = ( plcEmpPoint + visionEmpPoint + (totalEmpSoftPoint / countSoft)) /3,
        //                GeneralPoint = totalEmpGeneralPoint / countGeneralPoint
        //            },
        //            new
        //            {
        //                EvaluatedType = "Đánh giá của Trưởng/Phó BP",
        //                SkillPoint = totalTBPSkillPoint / countSkillPoint,
        //                PLCPoint = plcTBPPoint,
        //                VisionPoint = visionTBPPoint,
        //                SoftWarePoint = totalTBPSoftPoint / countSoft,
        //                AVGPoint = ( plcTBPPoint + visionTBPPoint + (totalTBPSoftPoint / countSoft)) / 3,
        //                GeneralPoint = totalTBPGeneralPoint / countGeneralPoint
        //            },
        //             new
        //            {
        //                EvaluatedType = "Đánh giá của GĐ",
        //                SkillPoint = totalBGDSkillPoint / countSkillPoint,
        //                PLCPoint = plcBGDPoint,
        //                VisionPoint = visionBGDPoint,
        //                SoftWarePoint = totalBGDSoftPoint / countSoft,
        //                AVGPoint = (plcBGDPoint + visionBGDPoint + (totalBGDSoftPoint / countSoft)) / 3,
        //                GeneralPoint = totalBGDGeneralPoint / countGeneralPoint
        //            },

        //        };
        //        grdMaster.DataSource = data;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
        //    }
        //}

        //private DataTable CalculatorAvgPoint(DataTable dataTable)
        //{
        //    departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
        //    //if (departmentID == departmentCoKhi) return new DataTable();//-- 160525 -- lee min khooi -- update
        //    if (_departmentCoKhiLRs.Contains(departmentID)) return new DataTable();//-- 160525 -- lee min khooi -- update

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
        //    dataTable = CalculatorTotalPointNew(dataTable);
        //    dataTable.AcceptChanges();
        //    return dataTable;
        //}
        //private DataTable CalculatorTotalPoint(DataTable dataTable)
        //{
        //    departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
        //    //if (departmentID == departmentCoKhi) return new DataTable();//-- 160525 -- lee min khooi -- update
        //    if (_departmentCoKhiLRs.Contains(departmentID)) return new DataTable();//-- 160525 -- lee min khooi -- update

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
        //    dataTable.AcceptChanges();
        //    return dataTable;
        //}

        private void LoadEValuationType()
        {
            List<object> lst = new List<object>()
            {
                new {ID = 1, EValuationType = "Đánh giá kỹ năng"},
                new {ID = 2, EValuationType = "Chuyên môn"}
            };
            cboEvaluationType.Properties.DataSource = lst;
            cboEvaluationType.Properties.ValueMember = "ID";
            cboEvaluationType.Properties.DisplayMember = "EValuationType";
            cboEvaluationType.EditValue = 1;
        }

        private void LoadStatus()
        {
            List<object> lst = new List<object>()
            {
                new {ID = -1, Status = "--Tất cả--"},
                new {ID = 0, Status = "Chưa chấm điểm"},
                new {ID = 1, Status = "Đã chấm điểm"}
            };
            cboStatus.Properties.DataSource = lst;
            cboStatus.Properties.ValueMember = "ID";
            cboStatus.Properties.DisplayMember = "Status";
            cboStatus.EditValue = -1;
        }
        private void LoadDepartMent()
        {
            List<DepartmentModel> lst = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            //lst.Insert(0, new DepartmentModel()
            //{
            //    ID = 0,
            //    Name = "--Tất cả--"
            //});
            cboDepartMent.Properties.DataSource = lst;
            cboDepartMent.Properties.ValueMember = "ID";
            cboDepartMent.Properties.DisplayMember = "Name";

            //cboDepartMent.EditValue = Global.DepartmentID;
            cboDepartMent.EditValue = departmentID;
        }

        private void btnEmployeeApproved_Click(object sender, EventArgs e)
        {
            int empId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            int kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));
            frmKPIEvaluationFactorScoringDetails frm = new frmKPIEvaluationFactorScoringDetails();
            frm.employeeID = Global.EmployeeID;
            frm._departmentID = departmentID; //--160525-- lee min khooi-- update
            if (Global.IsAdmin) frm.employeeID = empId;
            frm.kpiExam = SQLHelper<KPIExamModel>.FindByID(kpiExamID);
            frm.typePoint = 1;
            if (frm.kpiExam.ID <= 0)
            {
                MessageBox.Show("Bài đánh giá không hợp lệ! Hãy chọn lại bài đánh giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (frm.ShowDialog() == DialogResult.OK)
            {
                isUpdate = false;
                LoadEmployee();
            }

        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //========================== lee min khooi update 08/10/2024 ========================================
            //string employeeName = TextUtils.ToString(grvData.GetRowCellValue(e.PrevFocusedRowHandle, colEmployeeName));
            //if (isUpdate)
            //{
            //    //var answerQues = MessageBox.Show($"Bạn đang sửa điểm đánh giá KPI của [{employeeName}]!\n Bạn có muốn lưu lại dữ liệu không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    //if (answerQues == DialogResult.Yes)
            //    //{
            //    //========= save data ======================
            //    SaveAllData(e.PrevFocusedRowHandle);
            //    //}
            //    isUpdate = false;
            //}
            LoadDataDetails();
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colIsEvaluated));
            //if (status == 1)
            //{
            //    e.Appearance.BackColor = Color.LightYellow;
            //    e.HighPriority = true;
            //}
        }

        private void btnTBPApproved_Click(object sender, EventArgs e)
        {

            int rowhandle = grvData.FocusedRowHandle;
            int empId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            if (empId <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo");
                return;
            }
            int kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));
            frmKPIEvaluationFactorScoringDetails frm = new frmKPIEvaluationFactorScoringDetails();
            departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            frm._departmentID = departmentID; //--160525-- lee min khooi-- update

            frm.employeeID = empId;
            frm.kpiExam = SQLHelper<KPIExamModel>.FindByID(kpiExamID);
            frm.status = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colExamStatus));
            if (frm.kpiExam.ID <= 0)
            {
                MessageBox.Show("Bài đánh giá không hợp lệ! Hãy chọn lại bài đánh giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frm.typePoint = 2;
            frm.isAdminConfirm = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsAdminConfirm));
            //frm.Show();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                isUpdate = false;
                LoadEmployee();
                grvData.FocusedRowHandle = rowhandle;
            }
        }

        private void btnBGDApproved_Click(object sender, EventArgs e)
        {
            int rowhandle = grvData.FocusedRowHandle;
            int empId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            if (empId <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo");
                return;
            }
            int kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));
            frmKPIEvaluationFactorScoringDetails frm = new frmKPIEvaluationFactorScoringDetails();
            departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            frm._departmentID = departmentID; //--160525-- lee min khooi-- update
            frm.employeeID = empId;
            frm.kpiExam = SQLHelper<KPIExamModel>.FindByID(kpiExamID);
            if (frm.kpiExam.ID <= 0)
            {
                MessageBox.Show("Bài đánh giá không hợp lệ! Hãy chọn lại bài đánh giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frm.typePoint = 3;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                isUpdate = false;
                LoadEmployee();
                grvData.FocusedRowHandle = rowhandle;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        private void LoadData()
        {
            int currentRow = grvData.FocusedRowHandle;
            LoadEmployee();
            grvData.FocusedRowHandle = currentRow;
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

        private void cboStatus_EditValueChanged(object sender, EventArgs e)
        {
            departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            if (_departmentCoKhiLRs.Contains(departmentID)) LoadEventForTKCK();
            LoadKPISession();
            LoadUserTeam();
            btnSearch_Click(null, null);
        }

        private void txtQuarter_ValueChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
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


            //List<GridBand> listBands = new List<GridBand>() { gridBand4, gridBand5, gridBand6, gridBand7 };
            //BandedGridColumn col = (BandedGridColumn)e.Column;


            List<string> listFieldNames = treeListBand2.Columns.Select(x => x.FieldName).ToList();
            listFieldNames.AddRange(treeListBand3.Columns.Select(x => x.FieldName));
            listFieldNames.AddRange(treeListBand4.Columns.Select(x => x.FieldName));
            listFieldNames.AddRange(new List<string>() { colStandardPoint.FieldName, colCoefficient.FieldName, colEmployeePoint.FieldName, colTBPPoint.FieldName, colBGDPoint.FieldName });


            if (!listFieldNames.Contains(e.Column.FieldName)) return;
            decimal value = TextUtils.ToDecimal(e.Value);
            if (value == 0) e.DisplayText = "";
        }

        private void treeList1_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colStandardPoint1 || e.Column == colCoefficient1)
            {
                if (TextUtils.ToInt(e.Value) == 0)
                {
                    e.DisplayText = "";
                }
            }
        }

        private void cboUserTeam_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboKPISession_EditValueChanged(object sender, EventArgs e)
        {
            LoadKpiExam();
            LoadUserTeam();
        }

        private void grvExam_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ////========================== lee min khooi update 08/10/2024 ========================================
            //if (isUpdate)
            //{
            //    //========= save data ======================
            //    SaveAllData(grvData.FocusedRowHandle);
            //    isUpdate = false;
            //}
            LoadData();
        }

        private void treeList1_CustomColumnDisplayText_1(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
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
        private void UpdateStatusKPI(int status)
        {
            int rowhandle = grvData.FocusedRowHandle;
            int kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));
            int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            if (kpiExamID <= 0)
            {
                MessageBox.Show("Vui lòng chọn bài đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<KPIEvaluationPointModel> lst = SQLHelper<KPIEvaluationPointModel>.ProcedureToList("spGetKPIEvaluationPoint", new string[] { "@KPIExamID", "@EmployeeID" },
                                                                                                    new object[] { kpiExamID, empID });
            if (lst.Count <= 0)
            {
                MessageBox.Show("Vui lòng Đánh giá KPI trước khi hoàn thành!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int[] statusCancel = new int[] { 0, 4, 5 };
                string statusText = statusCancel.Contains(status) ? "Hủy" : "Hoàn thành";
                bool isDelete = MessageBox.Show($"Bạn có muốn xác nhận {statusText} Bài đánh giá [{TextUtils.ToString(grvExam.GetFocusedRowCellValue(colExamName))}] của nhân viên [{TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName))}] hay không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                if (isDelete)
                {
                    foreach (KPIEvaluationPointModel item in lst)
                    {
                        item.Status = status;
                        if (status == 2)
                        {
                            item.DateTBPConfirm = DateTime.Now;
                        }
                        else
                        {
                            item.DateBGDConfirm = DateTime.Now;
                        }
                        SQLHelper<KPIEvaluationPointModel>.Update(item);
                    }
                }
            }
            LoadEmployee();
            grvData.FocusedRowHandle = rowhandle;
        }
        private void btnEmployeeCancel_Click(object sender, EventArgs e)
        {
            UpdateStatusKPI(0);
        }

        private void btnTBPAccess_Click(object sender, EventArgs e)
        {
            UpdateStatusKPI(2);
        }

        private void btnBGDAccess_Click(object sender, EventArgs e)
        {
            UpdateStatusKPI(3);
        }

        private void btnTBPCancleAccess_Click(object sender, EventArgs e)
        {
            UpdateStatusKPI(5);
        }

        private void btnBGDCancleAccess_Click(object sender, EventArgs e)
        {
            UpdateStatusKPI(4);
        }


        //================================================= lee min khooi update 08/10/2024 =================================================
        private void treeData_KeyDown(object sender, KeyEventArgs e)
        {
            //int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            //if (e.Control && e.KeyCode == Keys.S && isUpdate)
            //{
            //    SaveAllData(empID);
            //}
            //else if (e.KeyCode == Keys.Enter)
            //{
            //    bool isColumn = (treeData.FocusedColumn == colTBPPoint && typeID == 2) || (treeData.FocusedColumn == colBGDPoint && typeID == 3);
            //    if (isColumn)
            //    {
            //        TreeListNode currentNode = treeData.FocusedNode;
            //        if (UpdateNodeChange(currentNode, 1, empID)) isUpdate = false;

            //        List<TreeListNode> lst = treeData.GetNodeList();
            //        int currentIndex = lst.FindIndex(p => p == currentNode);
            //        if (currentIndex + 1 < lst.Count) treeData.SetFocusedNode(lst[currentIndex + 1]);
            //    }

            //}

            TreeList treeList = (TreeList)sender;
            if (treeList == null) return;

            //treeList.CloseEditor();
            //treeList.FocusedColumn = colID;

            if (e.KeyCode == Keys.Enter)//lưu dữ liệu tại dòng đang focus
            {
                //Stopwatch stopwatch = new Stopwatch();
                //stopwatch.Start();
                //foreach (TreeListNode node in treeList.GetNodeList())
                //{
                //    int id = TextUtils.ToInt(node.GetValue(colKPIEvaluationPointID.FieldName));

                //    KPIEvaluationPointModel point = SQLHelper<KPIEvaluationPointModel>.FindByID(id);
                //    point.KPIEvaluationFactorsID = TextUtils.ToInt(node.GetValue(colID.FieldName));
                //    point.EmployeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));

                //    point.EmployeePoint = TextUtils.ToInt(node.GetValue(colEmployeePoint.FieldName));
                //    point.TBPPoint = TextUtils.ToInt(node.GetValue(colTBPPoint.FieldName));
                //    point.BGDPoint = TextUtils.ToInt(node.GetValue(colBGDPoint.FieldName));

                //    if (typeID == 3) point.BGDID = Global.EmployeeID;
                //    point.EmployeeEvaluation = TextUtils.ToDecimal(node.GetValue(colEmployeeEvaluation.FieldName));
                //    point.EmployeeCoefficient = TextUtils.ToDecimal(node.GetValue(colEmployeeCoefficient.FieldName));

                //    point.TBPEvaluation = TextUtils.ToDecimal(node.GetValue(colTBPEvaluation.FieldName));
                //    point.TBPCoefficient = TextUtils.ToDecimal(node.GetValue(colTBPCoefficient.FieldName));

                //    point.BGDEvaluation = TextUtils.ToDecimal(node.GetValue(colBGDEvaluation.FieldName));
                //    point.BGDCoefficient = TextUtils.ToDecimal(node.GetValue(colBGDCoefficient.FieldName));
                //    if (point.ID <= 0)
                //    {
                //        point.ID = SQLHelper<KPIEvaluationPointModel>.Insert(point).ID;
                //    }
                //    else
                //    {
                //        SQLHelper<KPIEvaluationPointModel>.Update(point);
                //    }

                //    node.SetValue(colKPIEvaluationPointID.FieldName, point.ID);
                //}

                //stopwatch.Stop();
                //MessageBox.Show(stopwatch.ElapsedMilliseconds.ToString());
                //treeList.RefreshDataSource();

            }
            else if (e.Control && e.KeyCode == Keys.C) //Copy
            {
                string value = TextUtils.ToString(treeList.GetFocusedRowCellValue(treeList.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void treeData_ShowingEditor(object sender, CancelEventArgs e)
        {
            //if (treeData.FocusedNode.HasChildren)
            //{
            //    e.Cancel = true;
            //}

            e.Cancel = treeData.FocusedNode.HasChildren;
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList1.FocusedNode.HasChildren)
            {
                e.Cancel = true;
            }
        }

        private void treeData_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //DataTable dt = (DataTable)treeData.DataSource;
            //treeData.DataSource = CalculatorAvgPoint(dt);
            //LoadTotalAVG();
            //treeData.ExpandAll();
            //isUpdate = true;
        }

        private bool UpdateNodeChange(TreeListNode focusNode, int evaluationType, int empID)
        {
            try
            {

                int Id = TextUtils.ToInt(focusNode.GetValue("KPIEvaluationPointID"));
                int kpiFactorID = TextUtils.ToInt(focusNode.GetValue("ID"));
                if (Id < 0 || empID <= 0 || kpiFactorID < 0) return true;

                KPIEvaluationPointModel model = SQLHelper<KPIEvaluationPointModel>.FindByID(Id);
                model.EmployeeID = empID;

                model.EmployeeCoefficient = TextUtils.ToDecimal(focusNode.GetValue("EmployeeCoefficient")); ;
                model.TBPCoefficient = TextUtils.ToDecimal(focusNode.GetValue("TBPCoefficient"));
                model.BGDCoefficient = TextUtils.ToDecimal(focusNode.GetValue("BGDCoefficient"));
                model.KPIEvaluationFactorsID = kpiFactorID;

                if (typeID == 2)
                {
                    model.TBPPoint = TextUtils.ToInt(focusNode.GetValue("TBPPoint"));
                    model.TBPID = Global.EmployeeID;
                    model.TBPEvaluation = TextUtils.ToDecimal(focusNode.GetValue("TBPEvaluation"));
                }
                else if (typeID == 3)
                {
                    model.BGDPoint = TextUtils.ToInt(focusNode.GetValue("BGDPoint"));
                    model.BGDID = Global.EmployeeID;
                    model.BGDEvaluation = TextUtils.ToDecimal(focusNode.GetValue("BGDEvaluation"));
                }

                if (model.ID > 0) SQLHelper<KPIEvaluationPointModel>.Update(model);
                else model.ID = SQLHelper<KPIEvaluationPointModel>.Insert(model).ID;

                TreeList tree = evaluationType == 1 ? treeData : treeList1;
                tree.SetRowCellValue(focusNode, "KPIEvaluationPointID", model.ID);

                return true;
            }
            catch (Exception err)
            {
                MessageBox.Show($"Xảy ra lỗi: {err.Message}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }


        private bool SaveAllData(int employeeFocus)
        {

            try
            {
                int employeeID = TextUtils.ToInt(grvData.GetRowCellValue(employeeFocus, colEmployeeID));
                foreach (TreeListNode item in treeData.GetNodeList())
                {
                    UpdateNodeChange(item, 1, employeeID);
                }

                foreach (TreeListNode item in treeList1.GetNodeList())
                {
                    UpdateNodeChange(item, 2, employeeID);
                }


                foreach (TreeListNode item in treeList2.GetNodeList())
                {
                    UpdateNodeChange(item, 2, employeeID);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private void treeList1_KeyDown(object sender, KeyEventArgs e)
        {
            //TreeList treeList = (TreeList)sender;

            int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            if (e.Control && e.KeyCode == Keys.S && isUpdate)
            {
                SaveAllData(empID);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                bool isColumn = (treeList1.FocusedColumn == colTBPPoint1 && typeID == 2) || (treeList1.FocusedColumn == colBGDPoint1 && typeID == 3);
                if (isColumn)
                {
                    TreeListNode currentNode = treeList1.FocusedNode;
                    if (UpdateNodeChange(currentNode, 2, empID)) isUpdate = false;

                    List<TreeListNode> lst = treeList1.GetNodeList();
                    int currentIndex = lst.FindIndex(p => p == currentNode);
                    if (currentIndex + 1 < lst.Count) treeList1.SetFocusedNode(lst[currentIndex + 1]);
                }
            }
        }

        private void treeList2_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
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

        private void treeList2_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colStandardPoint2 || e.Column == colCoefficient2)
            {
                if (TextUtils.ToInt(e.Value) == 0)
                {
                    e.DisplayText = "";
                }
            }
        }

        private void treeList2_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList2.FocusedNode.HasChildren)
            {
                e.Cancel = true;
            }
        }

        private void treeList2_KeyDown(object sender, KeyEventArgs e)
        {
            int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            if (e.Control && e.KeyCode == Keys.S && isUpdate)
            {
                SaveAllData(empID);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                bool isColumn = (treeList2.FocusedColumn == colTBPPoint2 && typeID == 2) || (treeList2.FocusedColumn == colBGDPoint2 && typeID == 3);
                if (isColumn)
                {
                    TreeListNode currentNode = treeList2.FocusedNode;
                    if (UpdateNodeChange(currentNode, 2, empID)) isUpdate = false;

                    List<TreeListNode> lst = treeList2.GetNodeList();
                    int currentIndex = lst.FindIndex(p => p == currentNode);
                    if (currentIndex + 1 < lst.Count) treeList1.SetFocusedNode(lst[currentIndex + 1]);
                }
            }
        }

        private void treeList2_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            /*DataTable dt = (DataTable)treeList2.DataSource;
            treeList2.DataSource = CalculatorAvgPoint(dt);
            LoadTotalAVG();
            treeList2.ExpandAll();
            isUpdate = true;*/
        }

        private void treeList1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //DataTable dt = (DataTable)treeList1.DataSource;
            //treeList1.DataSource = CalculatorAvgPoint(dt);
            //LoadTotalAVG();
            //treeList1.ExpandAll();
            //isUpdate = true;
        }

        //private void LoadKPIRule()
        //{
        //    try
        //    {
        //        departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
        //        //if (departmentID == departmentCoKhi) return;//-- 160525 -- lee min khooi -- update
        //        if (_departmentCoKhiLRs.Contains(departmentID)) return;//-- 160525 -- lee min khooi -- update

        //        treeList3.ClearNodes();
        //        treeList3.DataSource = null;

        //        grdTeam.DataSource = null;
        //        int kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue);
        //        int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
        //        //empID = 55;
        //        KPIPositionEmployeeModel positionEmp = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", empID).FirstOrDefault() ?? new KPIPositionEmployeeModel();
        //        //if (positionEmp == null)
        //        //{
        //        //    DataTable dt = new DataTable();
        //        //    grdTeam.DataSource = dt;
        //        //    treeList3.DataSource = dt;
        //        //    return;
        //        //}


        //        Expression ex1 = new Expression("KPISessionID", kpiSessionID);
        //        Expression ex2 = new Expression("KPIPositionID", TextUtils.ToInt(positionEmp.KPIPosiotionID) > 0 ? TextUtils.ToInt(positionEmp.KPIPosiotionID) : 1);
        //        Expression ex3 = new Expression("IsDeleted", 0);
        //        KPIEvaluationRuleModel kpiRule = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPIEvaluationRuleModel();
        //        //if (kpiRule == null)
        //        //{
        //        //    DataTable dt = new DataTable();
        //        //    grdTeam.DataSource = dt;
        //        //    treeList3.DataSource = dt;
        //        //    return;
        //        //}

        //        int empPointID = GetKPIEmployeePointID(kpiRule.ID);
        //        if (positionEmp.KPIPosiotionID >= 4)
        //        {
        //            DataTable dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeam", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
        //            //DataTable dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeam_test", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
        //            grdTeam.DataSource = dtTeam;
        //        }

        //        DataTable dtKpiRule = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointID", "LMKTable",
        //                                                        new string[] { "@KPIEmployeePointID" },
        //                                                        new object[] { empPointID });


        //        //var data = SQLHelper<object>.ProcedureToList("spGetEmployeeRulePointByKPIEmpPointID",
        //        //                                                new string[] { "@KPIEmployeePointID" },
        //        //                                                new object[] { empPointID });
        //        treeList3.DataSource = dtKpiRule;
        //        treeList3.ExpandAll();
        //        // ========================= 09/12/2024 =======================================================
        //        List<KPIEmployeePointDetailModel> lst = SQLHelper<KPIEmployeePointDetailModel>.FindByAttribute("KPIEmployeePointID", empPointID);
        //        bool isAdminConfirm = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsAdminConfirm));
        //        if (lst.Count <= 0)
        //        {
        //            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
        //            {
        //                LoadPointRule(empPointID);
        //            }
        //        }
        //        else if (!isAdminConfirm)
        //        {
        //            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
        //            {
        //                LoadPointRuleLastMonth(empPointID);
        //            }
        //        }
        //        CalculatorPoint(empPointID, TextUtils.ToInt(positionEmp.KPIPosiotionID));

        //        DataRow row = dtKpiRule.NewRow();
        //        row["STT"] = "";
        //        row["EvaluationCode"] = "NewLine";
        //        row["FirstMonth"] = 0;
        //        row["SecondMonth"] = 0;
        //        row["ThirdMonth"] = 0;
        //        row["ParentID"] = 0;

        //        TreeListNode node = treeList3.FindNodeByFieldValue("EvaluationCode", "KPIKyNang");
        //        if (node != null)
        //        {
        //            TreeListNode parentNode = node.ParentNode;
        //            string parentCode = "";

        //            if (parentNode == null) parentCode = "KPIKyNang";
        //            else parentCode = parentNode.GetValue("STT").ToString();

        //            int insertIndex = -1;
        //            for (int i = 0; i < dtKpiRule.Rows.Count; i++)
        //            {
        //                if (dtKpiRule.Rows[i]["EvaluationCode"].ToString() == parentCode || dtKpiRule.Rows[i]["STT"].ToString() == parentCode)
        //                {
        //                    insertIndex = i;
        //                    break;
        //                }
        //            }
        //            if (insertIndex >= 0) dtKpiRule.Rows.InsertAt(row, insertIndex);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
        //    }

        //}

        private int GetKPIEmployeePointID(int ruleID)
        {
            departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            //if (departmentID == departmentCoKhi) return 0;//-- 160525 -- lee min khooi -- update
            if (_departmentCoKhiLRs.Contains(departmentID)) return 0;//-- 160525 -- lee min khooi -- update

            int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            string empName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));
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

        private void treeList3_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
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

            string code = TextUtils.ToString(e.Node.GetValue(colEvaluationCode.FieldName));

            if (e.Node.HasChildren || code == "NewLine")
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

        List<string> lstCodeDisplay = new List<string>() { "KPINQ", "KPINL" };
        private void treeList3_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            List<TreeListBand> listBands = new List<TreeListBand>() { /*treeListBand18,*/ treeListBand19 };
            bool isColumn = e.Column == colFirstMonth || e.Column == colSecondMonth || e.Column == colThirdMonth;
            string ruleCode = TextUtils.ToString(e.Node["EvaluationCode"]).ToUpper();
            decimal maxPercent = TextUtils.ToDecimal(e.Node[colMaxPercent.FieldName]);
            bool isTeam = ruleCode.StartsWith("TEAM");


            if (ruleCode == "NewLine".ToUpper() && isColumn)
            {
                if (e.Column == colFirstMonth) e.DisplayText = "Tự đáng giá";
                else if (e.Column == colSecondMonth) e.DisplayText = "Trưởng/Phó BP";
                else if (e.Column == colThirdMonth) e.DisplayText = "Ban giám đốc";
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


            if (isColumn && isTeam)
            {
                e.DisplayText = "";
            }

        }


        //================================ 09/12/2024 =================================
        private void btnEvaluatedRule_Click(object sender, EventArgs e)
        {

            int rowhandle = grvData.FocusedRowHandle;
            int empId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            int kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));
            frmKPIEvaluationFactorScoringDetails frm = new frmKPIEvaluationFactorScoringDetails();
            departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            frm._departmentID = departmentID; //--160525-- lee min khooi-- update
            frm.employeeID = empId;
            frm.kpiExam = SQLHelper<KPIExamModel>.FindByID(kpiExamID);
            frm.status = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colExamStatus));
            if (frm.kpiExam.ID <= 0)
            {
                MessageBox.Show("Bài đánh giá không hợp lệ! Hãy chọn lại bài đánh giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frm.typePoint = typeID;
            frm.isAdminConfirm = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsAdminConfirm));
            //frm.Show();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                isUpdate = false;
                LoadEmployee();
                grvData.FocusedRowHandle = rowhandle;
            }

            //int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            //int kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue);
            //KPIPositionEmployeeModel positionEmp = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", empID).FirstOrDefault() ?? new KPIPositionEmployeeModel();
            //Expression ex1 = new Expression("KPISessionID", kpiSessionID);
            //Expression ex2 = new Expression("KPIPositionID", positionEmp.KPIPosiotionID > 0 ? positionEmp.KPIPosiotionID : 1);
            //Expression ex3 = new Expression("IsDeleted", 0);
            //KPIEvaluationRuleModel kpiRule = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault();
            //if (kpiRule == null) return;


            //int empPoint = GetKPIEmployeePointID(kpiRule.ID);
            //if (empPoint <= 0) return;

            //int sessionID = TextUtils.ToInt(cboKPISession.EditValue);
            //int kpiRuleID = kpiRule.ID;
            //string empName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));

            //KPIEmployeePointModel model = SQLHelper<KPIEmployeePointModel>.FindByID(empPoint);
            //if (model.ID <= 0)
            //{
            //    MessageBox.Show($"Không tìm thấy Tổng hợp đánh giá của nhân viên [{empName}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //frmKPISumarizeRuleDetails frm = new frmKPISumarizeRuleDetails();
            //frm.kpiEmpPoint = model;
            //frm.kpiSessionID = sessionID;
            //frm.kpiRuleID = kpiRuleID;
            //frm.empID = empID;
            //frm.Show();
            ////if (frm.ShowDialog() == DialogResult.OK)
            ////{
            ////    LoadKPIRule();
            ////}
        }

        //private void LoadPointRule(int empPointID)
        //{
        //    try
        //    {
        //        List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointID",
        //                                                               new string[] { "@KPIEmployeePointID" },
        //                                                               new object[] { empPointID });

        //        decimal timeWork = TextUtils.ToDecimal(grvTeam.Columns["TimeWork"].SummaryItem.SummaryValue);
        //        decimal fiveS = TextUtils.ToDecimal(grvTeam.Columns["FiveS"].SummaryItem.SummaryValue);
        //        decimal reportWork = TextUtils.ToDecimal(grvTeam.Columns["ReportWork"].SummaryItem.SummaryValue);
        //        decimal customerComplaint = TextUtils.ToDecimal(grvTeam.Columns["CustomerComplaint"].SummaryItem.SummaryValue);
        //        decimal deadlineDelay = TextUtils.ToDecimal(grvTeam.Columns["DeadlineDelay"].SummaryItem.SummaryValue);
        //        decimal teamKPIKyNang = TextUtils.ToDecimal(grvTeam.Columns["KPIKyNang"].SummaryItem.SummaryValue);
        //        decimal teanKPIChung = TextUtils.ToDecimal(grvTeam.Columns["KPIChung"].SummaryItem.SummaryValue);
        //        decimal teamKPIPLC = TextUtils.ToDecimal(grvTeam.Columns["KPIPLC"].SummaryItem.SummaryValue);
        //        decimal teamKPIVISION = TextUtils.ToDecimal(grvTeam.Columns["KPIVision"].SummaryItem.SummaryValue);
        //        decimal teamKPISOFTWARE = TextUtils.ToDecimal(grvTeam.Columns["KPISoftware"].SummaryItem.SummaryValue);
        //        decimal missingTool = TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);  //làm mất mát hỏng thiết bị 12/12/2024
        //                                                                                                             //================================== update 13/12/2024 ================================== 
        //        List<string> lstCodeTBP = new List<string>() { "MA03", "MA04", "NotWorking", "WorkLate" }; // MA011 Tổng số liệu thời gian đi làm ko đúng giờ + đi làm ko đủ công + L4 + L5
        //        var ltsMA11 = lstResult.Where(p => lstCodeTBP.Contains(p.EvaluationCode.Trim())).ToList();
        //        //decimal totalErrorTBP = lstResult.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);
        //        decimal totalErrorTBP = ltsMA11.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);
        //        //==========================================  END ==========================================
        //        lstResult.AddRange(new List<KPISumarizeDTO>
        //        {
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM01", ThirdMonth = timeWork},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM02", ThirdMonth = fiveS},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM03", ThirdMonth = reportWork},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM04", ThirdMonth = customerComplaint + missingTool + deadlineDelay},//update  12/12/2024
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM05", ThirdMonth = customerComplaint}, //update  12/12/2024
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = missingTool},//update  12/12/2024
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIKYNANG", ThirdMonth = teamKPIKyNang},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIChung", ThirdMonth = teanKPIChung},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIPLC", ThirdMonth = teamKPIPLC},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIVISION", ThirdMonth = teamKPIVISION},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPISOFTWARE", ThirdMonth = teamKPISOFTWARE},
        //            new KPISumarizeDTO(){ EvaluationCode = "MA11", ThirdMonth = totalErrorTBP}, // update 13/12/2024
        //        });


        //        Lib.LockEvents = true;
        //        foreach (KPISumarizeDTO item in lstResult)
        //        {
        //            TreeListNode node = treeList3.GetNodeList().FirstOrDefault(x => item.EvaluationCode == TextUtils.ToString(x.GetValue(colEvaluationCode)));
        //            if (node == null) continue;
        //            node.SetValue(colFirstMonth, item.FirstMonth);
        //            node.SetValue(colSecondMonth, item.SecondMonth);
        //            node.SetValue(colThirdMonth, item.ThirdMonth);
        //        }


        //        var kpiSession = (KPISessionModel)cboKPISession.GetSelectedDataRow() ?? new KPISessionModel();
        //        int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
        //        var dtTraining = TextUtils.LoadDataSetFromSP("spGetCourseTraining"
        //                                                    , new string[] { "@Year", "@Quarter", "@EmployeeID" }
        //                                                    , new object[] { kpiSession.YearEvaluation, kpiSession.QuarterEvaluation, employeeID });

        //        //Tính tích cực tham gia training
        //        var nodeThuong2 = treeList3.FindNodeByFieldValue(colEvaluationCode.FieldName, "THUONG02");
        //        if (nodeThuong2 != null)
        //        {
        //            var dtTHUONG02 = dtTraining.Tables[1];
        //            nodeThuong2.SetValue(colFirstMonth, dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["FirstMonth"]) : 0);
        //            nodeThuong2.SetValue(colSecondMonth, dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["SecondMonth"]) : 0);
        //            nodeThuong2.SetValue(colThirdMonth, dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["ThirdMonth"]) : 0);
        //        }

        //        //Tính tổ chức training
        //        var nodeThuong3 = treeList3.FindNodeByFieldValue(colEvaluationCode.FieldName, "THUONG03");
        //        if (nodeThuong3 != null)
        //        {
        //            var dtTHUONG03 = dtTraining.Tables[0];
        //            nodeThuong3.SetValue(colFirstMonth, dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["FirstMonth"]) : 0);
        //            nodeThuong3.SetValue(colSecondMonth, dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["SecondMonth"]) : 0);
        //            nodeThuong3.SetValue(colThirdMonth, dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["ThirdMonth"]) : 0);
        //        }

        //        Lib.LockEvents = false;

        //        //CalculatorPoint(empPointID);

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
        //    }
        //}

        List<string> lstTeamTBP = new List<string>() { "TEAM01", "TEAM02", "TEAM03" }; //19/12/2024

        private void CalculatorPoint(int empPointID, int position)
        {
            //return;
            try
            {
                //================ update 12/12/2024 ====================================
                KPIEmployeePointModel empPointModel = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);
                KPIEvaluationRuleModel ruleModel = SQLHelper<KPIEvaluationRuleModel>.FindByID(TextUtils.ToInt(empPointModel.KPIEvaluationRuleID));
                //bool isTBP = ruleModel.KPIPositionID == 5; // TBP
                //======================================================================
                Lib.LockEvents = true;
                CalculatorNoError();
                List<TreeListNode> lst = tlDataKPIRule.GetNodeList();
                for (int i = lst.Count - 1; i >= 0; i--)
                {
                    TreeListNode row = lst[i];
                    if (row == null) continue;
                    string stt = TextUtils.ToString(row["STT"]);
                    string ruleCode = TextUtils.ToString(row["EvaluationCode"]).ToUpper();
                    bool isDiemThuong = ruleCode == "THUONG";

                    decimal maxPercentBonus = TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["MaxPercent"]), 1);
                    decimal percentageAdjustment = TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["PercentageAdjustment"]), 1);
                    decimal maxPercentageAdjustment = TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["MaxPercentageAdjustment"]), 1);

                    if (row.Nodes.Count > 0)
                    {
                        decimal totalPercentBonus = 0;
                        decimal totalPercentRemaining = 0;
                        decimal total = 0;


                        bool isKPI = false;
                        foreach (TreeListNode childrenNode in row.Nodes)
                        {
                            string childRuleCode = TextUtils.ToString(childrenNode["EvaluationCode"]);
                            //if (childRuleCode == "THUONG") continue;

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
                }

                tlDataKPIRule.RefreshDataSource();
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

        private void CalculatorNoError()
        {
            var list = tlDataKPIRule.GetNodeList().Where(x => listCodes.Contains(x.GetValue(colEvaluationCode)));

            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (employeeID == 548) //Nếu là admin
            {
                list = tlDataKPIRule.GetNodeList().Where(x => listAdminCodes.Contains(x.GetValue(colEvaluationCode)));
            }


            decimal firstMonth = list.Sum(x => TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(x.GetValue(colFirstMonth)), 1));
            decimal secondMonth = list.Sum(x => TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(x.GetValue(colSecondMonth)), 1));
            decimal thirdMonth = list.Sum(x => TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(x.GetValue(colThirdMonth)), 1));

            var node = tlDataKPIRule.FindNodeByFieldValue(colEvaluationCode.FieldName, "MA09");
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

        private void treeData_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            //var view = sender as TreeList;
            //if (view.FocusedNode == e.Node)e.Appearance.BackColor = SystemColors.GradientActiveCaption;
        }

        private void repositoryItemSpinEdit2_KeyDown(object sender, KeyEventArgs e)
        {
            //SpinEdit spinEdit = (SpinEdit)sender;
            //TreeList treeList = (TreeList)spinEdit.Parent;
            //if (treeList == null) return;

            //if (e.KeyCode == Keys.Enter)//lưu dữ liệu tại dòng đang focus
            //{
            //    foreach (TreeListNode node in treeList.GetNodeList())
            //    {
            //        int id = TextUtils.ToInt(node.GetValue(colKPIEvaluationPointID.FieldName));

            //        KPIEvaluationPointModel point = SQLHelper<KPIEvaluationPointModel>.FindByID(id);
            //        point.KPIEvaluationFactorsID = TextUtils.ToInt(node.GetValue(colID.FieldName));
            //        point.EmployeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));

            //        point.EmployeePoint = TextUtils.ToInt(node.GetValue(colEmployeePoint.FieldName));
            //        point.TBPPoint = TextUtils.ToInt(node.GetValue(colTBPPoint.FieldName));
            //        point.BGDPoint = TextUtils.ToInt(node.GetValue(colBGDPoint.FieldName));

            //        if (typeID == 3) point.BGDID = Global.EmployeeID;
            //        point.EmployeeEvaluation = TextUtils.ToDecimal(node.GetValue(colEmployeeEvaluation.FieldName));
            //        point.EmployeeCoefficient = TextUtils.ToDecimal(node.GetValue(colEmployeeCoefficient.FieldName));

            //        point.TBPEvaluation = TextUtils.ToDecimal(node.GetValue(colTBPEvaluation.FieldName));
            //        point.TBPCoefficient = TextUtils.ToDecimal(node.GetValue(colTBPCoefficient.FieldName));

            //        point.BGDEvaluation = TextUtils.ToDecimal(node.GetValue(colBGDEvaluation.FieldName));
            //        point.BGDCoefficient = TextUtils.ToDecimal(node.GetValue(colBGDCoefficient.FieldName));
            //        if (point.ID <= 0)
            //        {
            //            point.ID = SQLHelper<KPIEvaluationPointModel>.Insert(point).ID;
            //        }
            //        else
            //        {
            //            SQLHelper<KPIEvaluationPointModel>.Update(point);
            //        }

            //        node.SetValue(colKPIEvaluationPointID.FieldName, point.ID);
            //    }
            //    treeList.RefreshDataSource();

            //}
        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column != colExamStatusText) return;
            int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatusKPIExam));
            if (status == 1)
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
            else if (status == 2)
            {
                e.Appearance.BackColor = Color.DeepPink;
                e.Appearance.ForeColor = Color.White;
            }
            else if (status == 3)
            {
                e.Appearance.BackColor = Color.Green;
            }
        }

        // ================================ Update 27/12/2024 =======================================
        private void btnLoadDataTeam_Click(object sender, EventArgs e)
        {
            int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            int kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue);
            if (kpiSessionID <= 0)
            {
                MessageBox.Show("Vui lòng chọn Kỳ đánh giá!", "Thông báo");
                return;
            }
            List<EmployeeModel> lstTeam = SQLHelper<EmployeeModel>.ProcedureToList("spGetAllTeamByEmployeeID", new string[] { "@EmployeeID" }, new object[] { empID });
            if (lstTeam.Count <= 0) return;

            frmKpiRuleSumarizeTeamChooseEmployee frm = new frmKpiRuleSumarizeTeamChooseEmployee();
            frm.lstEmp = lstTeam;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var expPoint1 = new Expression(KPIPositionEmployeeModel_Enum.EmployeeID, empID);
                var expPoint2 = new Expression(KPIPositionModel_Enum.KPISessionID, kpiSessionID);
                var expPoint3 = new Expression(KPIPositionEmployeeModel_Enum.IsDeleted, 0);
                var kpiPositions = SQLHelper<KPIPositionModel>.FindByExpression(expPoint2.And(expPoint3));
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
                {
                    foreach (EmployeeModel emp in lstTeam)
                    {
                        //if (emp.ID != 282) continue;
                        //KPIPositionEmployeeModel position1 = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", emp.ID).FirstOrDefault() ?? new KPIPositionEmployeeModel();

                        var kpiPositionEmployees1 = SQLHelper<KPIPositionEmployeeModel>.FindByExpression(new Expression(KPIPositionEmployeeModel_Enum.EmployeeID, emp.ID).And(expPoint3));

                        var position1 = (from p in kpiPositions
                                         join pe in kpiPositionEmployees1 on p.ID equals pe.KPIPosiotionID
                                         select pe)
                                 .FirstOrDefault() ?? new KPIPositionEmployeeModel();

                        int positionID = position1.KPIPosiotionID > 0 ? TextUtils.ToInt(position1.KPIPosiotionID) : 1; // 1: kỹ thuật;

                        Expression exFindRule1 = new Expression(KPIEvaluationRuleModel_Enum.KPIPositionID.ToString(), positionID);
                        Expression exFindRule2 = new Expression(KPIEvaluationRuleModel_Enum.KPISessionID.ToString(), kpiSessionID);
                        Expression exFindRule3 = new Expression(KPIEvaluationRuleModel_Enum.IsDeleted.ToString(), 0);
                        KPIEvaluationRuleModel ruleModel = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(exFindRule1.And(exFindRule2).And(exFindRule3)).FirstOrDefault() ?? new KPIEvaluationRuleModel();
                        if (ruleModel.ID <= 0) continue;

                        Expression ex1 = new Expression("EmployeeID", emp.ID);
                        Expression ex2 = new Expression("KPIEvaluationRuleID", ruleModel.ID);
                        Expression ex3 = new Expression("IsDelete", !frm.lstEmpChose.Contains(emp) ? 0 : 1);
                        KPIEmployeePointModel empPoint = SQLHelper<KPIEmployeePointModel>.FindByExpression(ex1.And(ex2).And(ex3)).OrderByDescending(x => x.ID).FirstOrDefault() ?? new KPIEmployeePointModel();
                        //KPIEmployeePointModel empPoint = SQLHelper<KPIEmployeePointModel>.FindByExpression(ex1.And(ex2)).FirstOrDefault() ?? new KPIEmployeePointModel();
                        if (empPoint.ID <= 0) continue;

                        if (!frm.lstEmpChose.Contains(emp))
                        {
                            empPoint.IsDelete = true;
                            SQLHelper<KPIEmployeePointModel>.Update(empPoint);
                            continue;
                        }

                        empPoint.IsDelete = false;
                        SQLHelper<KPIEmployeePointModel>.Update(empPoint);

                        //DataTable dtKpiRule = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointID", "LMKTable",
                        //                                                new string[] { "@KPIEmployeePointID" },
                        //                                                new object[] { empPoint.ID });

                        DataTable dtKpiRule = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointIDNew", "spGetEmployeeRulePointByKPIEmpPointIDNew",
                                                                        new string[] { "@KPIEmployeePointID" },
                                                                        new object[] { empPoint.ID });
                        if (dtKpiRule.Rows.Count <= 0) continue;
                        DataTable dt = LoadDataView(empPoint.ID, dtKpiRule);
                        SaveDataDetails(dt, empPoint.ID);
                    }
                    LoadKPIRuleNew();


                    //KPIPositionEmployeeModel position = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", empID).FirstOrDefault() ?? new KPIPositionEmployeeModel();
                    //int positionMasterID = position.KPIPosiotionID > 0 ? TextUtils.ToInt(position.KPIPosiotionID) : 1; // 1: kỹ thuật;

                    //Get possition của nhân viên
                    var kpiPositionEmployees = SQLHelper<KPIPositionEmployeeModel>.FindByExpression(expPoint1.And(expPoint3));

                    var position = (from p in kpiPositions
                                    join pe in kpiPositionEmployees on p.ID equals pe.KPIPosiotionID
                                    select pe)
                             .FirstOrDefault() ?? new KPIPositionEmployeeModel();
                    //

                    int positionMasterID = TextUtils.ToInt(position.KPIPosiotionID);

                    Expression exFindRuleM1 = new Expression(KPIEvaluationRuleModel_Enum.KPIPositionID.ToString(), positionMasterID);
                    Expression exFindRuleM2 = new Expression(KPIEvaluationRuleModel_Enum.KPISessionID.ToString(), kpiSessionID);
                    KPIEvaluationRuleModel rule = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(exFindRuleM1.And(exFindRuleM2)).FirstOrDefault() ?? new KPIEvaluationRuleModel();
                    int empPointMaster = GetKPIEmployeePointID(rule.ID);
                    LoadPointRuleNew(empPointMaster);
                }

            }


        }
        private void SaveDataDetails(DataTable dt, int empPointID)
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

                //var kpiSession = (KPISessionModel)cboKPISession.GetSelectedDataRow() ?? new KPISessionModel();
                //int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
                //var dtTraining = TextUtils.LoadDataSetFromSP("spGetCourseTraining"
                //                                            , new string[] { "@Year", "@Quarter", "@EmployeeID" }
                //                                            , new object[] { kpiSession.YearEvaluation, kpiSession.QuarterEvaluation, employeeID });

                ////Tính tích cực tham gia training
                //DataRow[] rowTHUONG02s = dt.Select($"EvaluationCode = 'THUONG02'");
                //if (rowTHUONG02s.Length > 0)
                //{
                //    var dtTHUONG02 = dtTraining.Tables[1];
                //    DataRow row = rowTHUONG02s[0];
                //    row["FirstMonth"] = dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["FirstMonth"]) : 0;
                //    row["SecondMonth"] = dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["SecondMonth"]) : 0;
                //    row["ThirdMonth"] = dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["ThirdMonth"]) : 0;
                //}

                ////Tính tổ chức training
                //DataRow[] rowTHUONG03s = dt.Select($"EvaluationCode = 'THUONG03'");
                //if (rowTHUONG03s.Length > 0)
                //{
                //    var dtTHUONG03 = dtTraining.Tables[0];
                //    DataRow row = rowTHUONG03s[0];
                //    row["FirstMonth"] = dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["FirstMonth"]) : 0;
                //    row["SecondMonth"] = dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["FirstMonth"]) : 0;
                //    row["ThirdMonth"] = dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["FirstMonth"]) : 0;
                //}

                dt.AcceptChanges();
                return dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return dt;
            }
        }
        // =============================== 28/12/2024 ============================
        private void LoadKPIKyNang(int empId, int kpiExamID)
        {
            treeData.ClearNodes();
            treeData.DataSource = null;

            DataTable list = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
                                                        new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                                                        new object[] { empId, 1, kpiExamID, true });

            DataRow parentRow = list.NewRow();
            parentRow["ID"] = -1;
            parentRow["ParentID"] = 0;
            parentRow["EvaluationContent"] = "TỔNG HỆ SỐ";
            parentRow["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
            list.AcceptChanges();
            //if (departmentID != departmentCoKhi) 
            list.Rows.Add(parentRow);

            departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            //treeData.DataSource = departmentID == departmentCoKhi ? CalculatorAvgPoint_TKCK(list) : CalculatorAvgPoint(list); //-- 160525 -- lee min khooi -- update
            treeData.DataSource = _departmentCoKhiLRs.Contains(departmentID) ? CalculatorAvgPoint_TKCK(list) : CalculatorAvgPointNew(list); //-- 160525 -- lee min khooi -- update
            treeData.ExpandAll();
        }
        private void LoadKPIChung(int empId, int kpiExamID)
        {
            treeList2.ClearNodes();
            treeList2.DataSource = null;
            DataTable list2 = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
                                                             new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                                                             new object[] { empId, 3, kpiExamID, true });



            DataRow parentRow3 = list2.NewRow();
            parentRow3["ID"] = -1;
            parentRow3["ParentID"] = 0;
            parentRow3["EvaluationContent"] = "TỔNG HỆ SỐ";
            parentRow3["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
            list2.Rows.Add(parentRow3);
            list2.AcceptChanges();

            departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            //treeList2.DataSource = departmentID == departmentCoKhi ? CalculatorAvgPoint_TKCK(list2) : CalculatorAvgPoint(list2); //-- 160525 -- lee min khooi -- update
            treeList2.DataSource = _departmentCoKhiLRs.Contains(departmentID) ? CalculatorAvgPoint_TKCK(list2) : CalculatorAvgPointNew(list2); //-- 160525 -- lee min khooi -- update
            //treeList2.DataSource = CalculatorAvgPoint(list3);
            treeList2.ExpandAll();
        }
        private void LoadKPIChuyenMon(int empId, int kpiExamID)
        {
            treeList1.ClearNodes();
            treeList1.DataSource = null;
            DataTable list3 = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
                                                           new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                                                           new object[] { empId, 2, kpiExamID, true });

            DataRow parentRow3 = list3.NewRow();
            parentRow3["ID"] = -1;
            parentRow3["ParentID"] = 0;
            parentRow3["EvaluationContent"] = "TỔNG HỆ SỐ";
            parentRow3["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
            list3.Rows.Add(parentRow3);
            list3.AcceptChanges();

            departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            //treeList1.DataSource = departmentID == departmentCoKhi ? CalculatorAvgPoint_TKCK(list3) : CalculatorAvgPoint(list3); //-- 160525 -- lee min khooi -- update
            treeList1.DataSource = _departmentCoKhiLRs.Contains(departmentID) ? CalculatorAvgPoint_TKCK(list3) : CalculatorAvgPointNew(list3, true); //-- 160525 -- lee min khooi -- update
            //treeList1.DataSource = CalculatorAvgPoint(list2);
            treeList1.ExpandAll();
        }
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                int selectedTab = xtraTabControl1.SelectedTabPageIndex;
                if (selectedTab < 0) return;

                int empId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
                int kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));
                switch (selectedTab)
                {
                    case 0:
                        LoadKPIKyNang(empId, kpiExamID);
                        break;

                    case 1:
                        LoadKPIChung(empId, kpiExamID);
                        break;

                    case 2:
                        LoadKPIChuyenMon(empId, kpiExamID);
                        break;

                    case 3:
                        //LoadTotalAVGNew();
                        if (!_departmentCoKhiLRs.Contains(departmentID)) LoadTotalAVGNew();
                        else LoadSumaryRank_TKCK();

                        break;

                    case 4:
                        LoadKPIRuleNew();
                        break;

                    case 5:
                        LoadKPIRuleNew();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"xtraTabControl1_SelectedPageChanged\r\n{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        //private void LoadPointRuleLastMonth(int empPointID)
        //{
        //    try
        //    {
        //        List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointID",
        //                                                               new string[] { "@KPIEmployeePointID" },
        //                                                               new object[] { empPointID });

        //        decimal timeWork = TextUtils.ToDecimal(grvTeam.Columns["TimeWork"].SummaryItem.SummaryValue);
        //        decimal fiveS = TextUtils.ToDecimal(grvTeam.Columns["FiveS"].SummaryItem.SummaryValue);
        //        decimal reportWork = TextUtils.ToDecimal(grvTeam.Columns["ReportWork"].SummaryItem.SummaryValue);
        //        decimal customerComplaint = TextUtils.ToDecimal(grvTeam.Columns["CustomerComplaint"].SummaryItem.SummaryValue);
        //        decimal deadlineDelay = TextUtils.ToDecimal(grvTeam.Columns["DeadlineDelay"].SummaryItem.SummaryValue);
        //        decimal teamKPIKyNang = TextUtils.ToDecimal(grvTeam.Columns["KPIKyNang"].SummaryItem.SummaryValue);
        //        decimal teanKPIChung = TextUtils.ToDecimal(grvTeam.Columns["KPIChung"].SummaryItem.SummaryValue);
        //        decimal teamKPIPLC = TextUtils.ToDecimal(grvTeam.Columns["KPIPLC"].SummaryItem.SummaryValue);
        //        decimal teamKPIVISION = TextUtils.ToDecimal(grvTeam.Columns["KPIVision"].SummaryItem.SummaryValue);
        //        decimal teamKPISOFTWARE = TextUtils.ToDecimal(grvTeam.Columns["KPISoftware"].SummaryItem.SummaryValue);
        //        decimal missingTool = TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);  //làm mất mát hỏng thiết bị 12/12/2024
        //                                                                                                             //================================== update 13/12/2024 ================================== 
        //        List<string> lstCodeTBP = new List<string>() { "MA03", "MA04", "NotWorking", "WorkLate" }; // MA011 Tổng số liệu thời gian đi làm ko đúng giờ + đi làm ko đủ công + L4 + L5
        //        var ltsMA11 = lstResult.Where(p => lstCodeTBP.Contains(p.EvaluationCode.Trim())).ToList();
        //        //decimal totalErrorTBP = lstResult.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);
        //        decimal totalErrorTBP = ltsMA11.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);
        //        //==========================================  END ==========================================
        //        lstResult.AddRange(new List<KPISumarizeDTO>
        //        {
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM01", ThirdMonth = timeWork},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM02", ThirdMonth = fiveS},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM03", ThirdMonth = reportWork},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM04", ThirdMonth = customerComplaint + missingTool + deadlineDelay},//update  12/12/2024
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM05", ThirdMonth = customerComplaint}, //update  12/12/2024
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = missingTool},//update  12/12/2024
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIKYNANG", ThirdMonth = teamKPIKyNang},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIChung", ThirdMonth = teanKPIChung},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIPLC", ThirdMonth = teamKPIPLC},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIVISION", ThirdMonth = teamKPIVISION},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPISOFTWARE", ThirdMonth = teamKPISOFTWARE},
        //            new KPISumarizeDTO(){ EvaluationCode = "MA11", ThirdMonth = totalErrorTBP}, // update 13/12/2024
        //        });


        //        Lib.LockEvents = true;
        //        foreach (KPISumarizeDTO item in lstResult)
        //        {
        //            TreeListNode node = treeList3.GetNodeList().FirstOrDefault(x => item.EvaluationCode == TextUtils.ToString(x.GetValue(colEvaluationCode)));
        //            if (node == null) continue;
        //            //node.SetValue(colFirstMonth, item.FirstMonth);
        //            //node.SetValue(colSecondMonth, item.SecondMonth);
        //            node.SetValue(colThirdMonth, item.ThirdMonth);
        //        }

        //        Lib.LockEvents = false;

        //        //CalculatorPoint(empPointID);

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
        //    }
        //}

        private void btnAdminConfirm_Click(object sender, EventArgs e)
        {
            int rowhandle = grvData.FocusedRowHandle;
            int kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));
            int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            if (kpiExamID <= 0)
            {
                MessageBox.Show("Vui lòng chọn bài đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<KPIEvaluationPointModel> lst = SQLHelper<KPIEvaluationPointModel>.ProcedureToList("spGetKPIEvaluationPoint", new string[] { "@KPIExamID", "@EmployeeID" },
                                                                                                    new object[] { kpiExamID, empID });
            if (lst.Count <= 0)
            {
                MessageBox.Show("Vui lòng Đánh giá KPI trước khi hoàn thành!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                bool isDelete = MessageBox.Show($"Bạn có muốn xác nhận Bài đánh giá [{TextUtils.ToString(grvExam.GetFocusedRowCellValue(colExamName))}] của nhân viên [{TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName))}] hay không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                if (isDelete)
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
                    {
                        foreach (KPIEvaluationPointModel item in lst)
                        {
                            item.IsAdminConfirm = true;
                            SQLHelper<KPIEvaluationPointModel>.Update(item);
                        }
                        grvData.SetFocusedRowCellValue(colIsAdminConfirm, true);
                        SaveDataRule();
                        LoadKPIRuleNew();
                    }
                }
            }
        }


        private bool SaveDataRule()
        {
            try
            {
                tlDataKPIRule.CloseEditor();
                tlDataKPIRule.FocusedColumn = colEvaluationCode;
                int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
                //KPIPositionEmployeeModel positionEmp = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", empID).FirstOrDefault() ?? new KPIPositionEmployeeModel();

                int kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue);
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
                //

                Expression ex1 = new Expression("KPISessionID", kpiSessionID);
                Expression ex2 = new Expression("KPIPositionID", TextUtils.ToInt(positionEmp.KPIPosiotionID) > 0 ? TextUtils.ToInt(positionEmp.KPIPosiotionID) : 1);
                Expression ex3 = new Expression("IsDeleted", 0);
                KPIEvaluationRuleModel kpiRule = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPIEvaluationRuleModel();
                int empPointID = GetKPIEmployeePointID(kpiRule.ID);

                KPIEmployeePointModel master = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);

                master.TotalPercent = TextUtils.ToDecimal(tlDataKPIRule.GetSummaryValue(colPercentRemaining));
                master.Status = 2;
                SQLHelper<KPIEmployeePointModel>.Update(master);
                foreach (TreeListNode node in tlDataKPIRule.GetNodeList())
                {
                    int detailID = TextUtils.ToInt(node["EmpPointDetailID"]);
                    KPIEmployeePointDetailModel detail = SQLHelper<KPIEmployeePointDetailModel>.FindByID(detailID);
                    detail.KPIEmployeePointID = empPointID;
                    detail.KPIEvaluationRuleDetailID = TextUtils.ToInt(node["ID"]);
                    detail.FirstMonth = TextUtils.ToDecimal(node["FirstMonth"]);
                    detail.SecondMonth = TextUtils.ToDecimal(node["SecondMonth"]);
                    detail.ThirdMonth = TextUtils.ToDecimal(node["ThirdMonth"]);
                    detail.PercentBonus = TextUtils.ToDecimal(node["PercentBonus"]);
                    detail.PercentRemaining = TextUtils.ToDecimal(node["PercentRemaining"]);


                    if (detail.ID > 0) SQLHelper<KPIEmployeePointDetailModel>.Update(detail);
                    else SQLHelper<KPIEmployeePointDetailModel>.Insert(detail);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
        }


        private void btnAdminApprove_Click(object sender, EventArgs e)
        {
            AdminApproved(true);
        }

        void AdminApproved(bool isAdminConfirm)
        {

            int kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));
            int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            if (kpiExamID <= 0)
            {
                MessageBox.Show("Vui lòng chọn bài đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<KPIEvaluationPointModel> lst = SQLHelper<KPIEvaluationPointModel>.ProcedureToList("spGetKPIEvaluationPoint", new string[] { "@KPIExamID", "@EmployeeID" },
                                                                                                    new object[] { kpiExamID, empID });
            //if (lst.Count <= 0)
            //{
            //    MessageBox.Show("Vui lòng Đánh giá KPI trước khi hoàn thành!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            {
                bool isDelete = MessageBox.Show($"Bạn có muốn xác nhận Bài đánh giá [{TextUtils.ToString(grvExam.GetFocusedRowCellValue(colExamName))}] của nhân viên [{TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName))}] hay không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                if (isDelete)
                {
                    foreach (KPIEvaluationPointModel item in lst)
                    {
                        item.IsAdminConfirm = isAdminConfirm;
                        SQLHelper<KPIEvaluationPointModel>.Update(item);
                    }
                    grvData.SetFocusedRowCellValue(colIsAdminConfirm, isAdminConfirm);
                }
            }
        }

        private void treeList3_GetCustomSummaryValue(object sender, GetCustomSummaryValueEventArgs e)
        {
            if (e.IsSummaryFooter && e.Column == colPercentBonus)
            {
                decimal totalPercent = TextUtils.ToDecimal(tlDataKPIRule.GetSummaryValue(colPercentRemaining));
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
            List<TreeListNode> lst = tlDataKPIRule.GetNodeList();
            if (lst == null || lst.Count == 0) return;

            bool IsKPI(TreeListNode r)
            {
                string ruleCode = TextUtils.ToString(r["EvaluationCode"]).ToUpper();
                return ruleCode.StartsWith("KPI") && ruleCode != "KPINL" && ruleCode != "KPINQ";
            }
            decimal percentRemaining = Math.Round(TextUtils.ToDecimal(tlDataKPIRule.GetSummaryValue(colPercentRemaining)), 2);

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

        private void btnSuccessKPI_Click(object sender, EventArgs e)
        {
            int kpiExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamID));
            if (kpiExamID <= 0)
            {
                MessageBox.Show("Vui lòng chọn bài đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
            {

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
                            SQLHelper<KPIEvaluationPointModel>.Update(item);
                        }
                    }
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";


            string exam = TextUtils.ToString(grvExam.GetFocusedRowCellValue(colExamCode));
            string employeeName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));
            f.FileName = $"DanhGiaKPI_{exam}_{employeeName}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = f.FileName;

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);

                        foreach (XtraTabPage item in xtraTabControl1.TabPages)
                        {

                            if (item.Controls.Count <= 0) continue;
                            PrintableComponentLink printableComponentLink = new PrintableComponentLink(printingSystem);
                            try
                            {
                                printableComponentLink.Component = (GridControl)item.Controls[0];
                            }
                            catch
                            {
                                printableComponentLink.Component = (TreeList)item.Controls[0];

                            }
                            compositeLink.Links.Add(printableComponentLink);
                        }

                        compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;
                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;
                        //optionsEx.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);

                        var excelApp = new Microsoft.Office.Interop.Excel.Application();
                        var workbook = excelApp.Workbooks.Open(filepath);

                        foreach (Microsoft.Office.Interop.Excel.Worksheet sheet in workbook.Sheets)
                        {
                            sheet.Columns.AutoFit();
                            sheet.Rows.AutoFit();
                        }

                        workbook.Save();
                        workbook.Close();
                        excelApp.Quit();

                        Process.Start(filepath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void PrintingSystem_XlSheetCreated(object sender, XlSheetCreatedEventArgs e)
        {
            try
            {
                e.SheetName = xtraTabControl1.TabPages[e.Index].Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo");
            }
        }



        #region Công thức tính AGV


        //=======================================  ====================================================== -- 160525 -- lee min khooi -- update
        private void LoadEventForTKCK()
        {
            //xtraTabPage4.PageVisible = xtraTabPage5.PageVisible = xtraTabPage2.PageVisible = xtraTabPage6.PageVisible = false;
            //treeData.OptionsView.AutoWidth = true;

            xtraTabPage4.PageVisible = xtraTabPage5.PageVisible = xtraTabPage6.PageVisible = false; //LD.Dat update 02/10/2025
            treeData.OptionsView.AutoWidth = true;


            treeData.CellValueChanged -= treeData_CellValueChanged;
            treeData.CellValueChanged += treeData_CellValueChanged_TKCK;
            colCoefficient.Visible = colEmployeeCoefficient.Visible = colTBPCoefficient.Visible = colPoint6General.Visible = colBGDCoefficient.Visible = colTBPPoint.Visible = colBGDPoint.Visible = false;
            colCoefficient1.Visible = false;
            colStandartPoint.Visible = true;

            // ============= Khởi tạo lại gridcontrol
            gridBand2.Visible = false;
            gridBand8.Visible = true;
        }


        private void treeData_CellValueChanged_TKCK(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            TreeList treeList = (TreeList)sender;
            if (treeList == null) return;

            DataTable dt = (DataTable)treeList.DataSource;
            treeList.DataSource = CalculatorAvgPoint_TKCK(dt);
            treeList.ExpandAll();
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
                decimal totalStandardPoint = 0; //TN.Binh update 2/10/25

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
                        totalStandardPoint += TextUtils.ToDecimal(row["StandardPoint"]); //TN.Binh update 2/10/25
                        count++;
                    }
                }
                if (fatherRowIndex == -1 || count == 0) continue;
                // if (TextUtils.ToInt(cboPositionType.EditValue) == 2) count = 1;

                dataTable.Rows[fatherRowIndex]["EmployeeEvaluation"] = (decimal)(totalempPoint);
                dataTable.Rows[fatherRowIndex]["TBPEvaluation"] = (decimal)(totaltbpPoint);
                dataTable.Rows[fatherRowIndex]["BGDEvaluation"] = (decimal)(totalbgdPointt);
                dataTable.Rows[fatherRowIndex]["StandardPoint"] = (decimal)(totalStandardPoint); //TN.Binh update 2/10/25

                //dataTable.Rows[fatherRowIndex]["EmployeeCoefficient"] = (decimal)totalempPoint;
                //dataTable.Rows[fatherRowIndex]["TBPCoefficient"] = (decimal)totaltbpPoint;
                //dataTable.Rows[fatherRowIndex]["BGDCoefficient"] = (decimal)totalbgdPointt;
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
                //chuyen mon
                decimal totalEmpCMPoint = 0;
                decimal totalTBPCMPoint = 0;
                decimal totalBGDCMPoint = 0;
                decimal totalCMPoint = 0;

                DataTable dtChuyenMon = (DataTable)treeList1.DataSource;
                //endupdate

                DataTable dtSkill = (DataTable)treeData.DataSource;

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
                        SkillPoint = totalEmpSkillPoint,
                        //PercentageAchieved = Math.Round((totalEmpSkillPoint / totalSkillPoint) * 100, 2),
                        //EvaluationRank = GetEvaluationRank_TKCK(Math.Round((totalEmpSkillPoint / totalSkillPoint) * 100, 2)),
                        EvaluationRank = GetEvaluationRank_TKCK(Math.Round((totalEmpSkillPoint + totalEmpCMPoint) / (totalSkillPoint + totalCMPoint) * 100, 2)), // TN.Binh update
                        //StandartPoint = totalSkillPoint,
                        SpecializationPoint = totalEmpCMPoint,
                        StandartPoint = totalSkillPoint + totalCMPoint, // TN.Binh update 02/10/25
                        PercentageAchieved = Math.Round(((totalEmpSkillPoint + totalEmpCMPoint) / (totalSkillPoint + totalCMPoint)) * 100, 2),
                    },
                    new EvaluationSummary
                    {
                        EvaluatedType = "Đánh giá của Trưởng/Phó BP",
                        SkillPoint = totalTBPSkillPoint ,
                        //PercentageAchieved = Math.Round((totalTBPSkillPoint / totalSkillPoint) * 100, 2),
                        PercentageAchieved = Math.Round(( (totalTBPSkillPoint+totalTBPCMPoint) / (totalSkillPoint+totalCMPoint)) * 100, 2),
                        //EvaluationRank = GetEvaluationRank_TKCK(Math.Round((totalTBPSkillPoint / totalSkillPoint) * 100, 2)),
                        EvaluationRank = GetEvaluationRank_TKCK(Math.Round((totalTBPSkillPoint + totalTBPCMPoint) / (totalSkillPoint + totalCMPoint) * 100, 2)), // TN.Binh update
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
                        //EvaluationRank = GetEvaluationRank_TKCK(Math.Round((totalBGDSkillPoint / totalSkillPoint) * 100, 2)),
                        EvaluationRank = GetEvaluationRank_TKCK(Math.Round((totalBGDSkillPoint + totalBGDCMPoint) / (totalSkillPoint + totalCMPoint) * 100, 2)), // TN.Binh update
                        //StandartPoint = totalSkillPoint,
                        SpecializationPoint = totalBGDCMPoint,
                        StandartPoint = totalSkillPoint + totalCMPoint, // TN.Binh update 02/10/25

                    },

                };

                if (grdMaster.InvokeRequired)
                {
                    grdMaster.Invoke(new Action(() => { grdMaster.DataSource = data; }));

                }
                else grdMaster.DataSource = data;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
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

        private void btnAdminUnApproved_Click(object sender, EventArgs e)
        {
            AdminApproved(false);
        }

        private void btnExportExcelByTeam_Click(object sender, EventArgs e)
        {
            try
            {
                // Chọn thư mục gốc và nhập tên thư mục chính
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                if (folderDialog.ShowDialog() != DialogResult.OK)
                    return;
                string baseDirectory = folderDialog.SelectedPath;

                //string mainFolderName = Microsoft.VisualBasic.Interaction.InputBox("Nhập tên thư mục chính:", "Tên thư mục chính", "KPI_Evaluation");
                //if (string.IsNullOrWhiteSpace(mainFolderName))
                //    return;

                //string mainFolderPath = Path.Combine(baseDirectory, SanitizeFolderName(mainFolderName));
                //Directory.CreateDirectory(mainFolderPath);

                // Lấy kỳ đánh giá đang chọn từ cboKPISession
                KPISessionModel selectedSession = cboKPISession.GetSelectedDataRow() as KPISessionModel;

                if (selectedSession == null)
                {
                    MessageBox.Show("Vui lòng chọn một kỳ đánh giá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang xuất dữ liệu..."))
                {
                    string year = selectedSession.YearEvaluation.ToString();
                    string yearFolderPath = Path.Combine(baseDirectory, SanitizeFolderName(year));
                    Directory.CreateDirectory(yearFolderPath);

                    string quarter = "Q" + selectedSession.QuarterEvaluation.ToString();
                    string sessionFolderPath = Path.Combine(yearFolderPath, SanitizeFolderName(quarter));
                    Directory.CreateDirectory(sessionFolderPath);

                    // Lấy tất cả bài đánh giá cho kỳ đánh giá đang chọn
                    DataTable exams = SQLHelper<KPIExamModel>.LoadDataFromSP("spGetKPIExamByKPISessionID",
                        new string[] { "@KPISessionID", "@DepartmentID" },
                        new object[] { selectedSession.ID, departmentID });

                    foreach (DataRow examRow in exams.Rows)
                    {
                        int kpiExamID = TextUtils.ToInt(examRow["ID"]);
                        string examCode = TextUtils.ToString(examRow["ExamCode"]);
                        string examName = TextUtils.ToString(examRow["ExamName"]);

                        // Lấy dữ liệu nhân viên cho bài đánh giá
                        DataTable employees = SQLHelper<object>.LoadDataFromSP("spGetAllEmployeeKPIEvaluated",
                            new string[] { "@EvaluationType", "@DepartmentID", "@Keywords", "@Status", "@UserTeamID", "@KPIExamID" },
                            new object[] { 1, departmentID, "", -1, 0, kpiExamID });

                        if (employees == null || employees.Rows.Count == 0) continue;

                        var projectTypeGroups = employees.AsEnumerable().GroupBy(row => row["ProjectTypeName"].ToString());

                        foreach (var group in projectTypeGroups)
                        {
                            string projectTypeName = group.Key;
                            string safeProjectTypeName = SanitizeFolderName(projectTypeName);
                            string projectTypeFolderPath = Path.Combine(sessionFolderPath, safeProjectTypeName);
                            Directory.CreateDirectory(projectTypeFolderPath);

                            foreach (DataRow row in group)
                            {
                                int employeeID = Convert.ToInt32(row["ID"]);
                                string employeeName = row["FullName"].ToString();
                                string safeEmployeeName = SanitizeFolderName(employeeName);

                                string fileName = $"DanhGiaKPI_{examCode}_{safeEmployeeName}.xlsx";
                                string filePath = Path.Combine(projectTypeFolderPath, fileName);

                                LoadDataDetails(employeeID, kpiExamID);
                                ExportToExcel(filePath);
                            }
                        }
                    }
                }

                MessageBox.Show("Xuất dữ liệu hoàn tất.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(baseDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }


        private void LoadDataDetails(int empId, int kpiExamID)
        {
            LoadKPIKyNang(empId, kpiExamID);
            LoadKPIChung(empId, kpiExamID);
            LoadKPIChuyenMon(empId, kpiExamID);
            //LoadTotalAVGNew();
            //if (!_departmentCoKhiLRs.Contains(departmentID))
            //{
            //    LoadKPIRuleNew();
            //}

            //TN.Binh update
            if (!_departmentCoKhiLRs.Contains(departmentID))
            {
                LoadTotalAVGNew();
                LoadKPIRuleNew();
            }
            else
            {
                LoadSumaryRank_TKCK();
            }
            //end
        }

        private string SanitizeFolderName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }
            return name;
        }

        private void ExportToExcel(string filePath)
        {
            XlsxExportOptions optionsEx = new XlsxExportOptions();
            PrintingSystem printingSystem = new PrintingSystem();

            try
            {
                CompositeLink compositeLink = new CompositeLink(printingSystem);

                foreach (XtraTabPage item in xtraTabControl1.TabPages)
                {
                    if (item.Controls.Count <= 0) continue;
                    PrintableComponentLink printableComponentLink = new PrintableComponentLink(printingSystem);
                    try
                    {
                        printableComponentLink.Component = (GridControl)item.Controls[0];
                    }
                    catch
                    {
                        printableComponentLink.Component = (TreeList)item.Controls[0];
                    }
                    compositeLink.Links.Add(printableComponentLink);
                }

                compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;
                compositeLink.CreatePageForEachLink();
                optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;
                compositeLink.PrintingSystem.SaveDocument(filePath);
                compositeLink.ExportToXlsx(filePath, optionsEx);

                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workbook = excelApp.Workbooks.Open(filePath);
                foreach (Microsoft.Office.Interop.Excel.Worksheet sheet in workbook.Sheets)
                {
                    sheet.Columns.AutoFit();
                    sheet.Rows.AutoFit();
                }
                workbook.Save();
                workbook.Close();
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất file {filePath}: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region tính tổng điểm mới TNB update 11/09/25
        private void LoadTotalAVGNew()
        {
            try
            {
                decimal totalEmpSkillPoint = 0;
                decimal totalTBPSkillPoint = 0;
                decimal totalBGDSkillPoint = 0;
                int countSkillPoint = 1;
                DataTable dtSkill = (DataTable)treeData.DataSource;
                if (dtSkill != null)
                {
                    List<DataRow> lstSkillPoint = dtSkill.Select("ID = -1").ToList();
                    DataTable da = lstSkillPoint.CopyToDataTable();
                    countSkillPoint = lstSkillPoint.Count > 0 ? lstSkillPoint.Count : 1;
                    foreach (DataRow item in lstSkillPoint)
                    {

                        //totalEmpSkillPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
                        //totalTBPSkillPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
                        //totalBGDSkillPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);

                        totalEmpSkillPoint += TextUtils.ToDecimal(item["EmployeeEvaluation"]);
                        totalTBPSkillPoint += TextUtils.ToDecimal(item["TBPEvaluation"]);
                        totalBGDSkillPoint += TextUtils.ToDecimal(item["BGDEvaluation"]);
                    }
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

                    totalEmpSpecializationPoint += TextUtils.ToDecimal(item["EmployeeEvaluation"]);
                    totalTBPSpecializationPoint += TextUtils.ToDecimal(item["TBPEvaluation"]);
                    totalBGDSpecializationPoint += TextUtils.ToDecimal(item["BGDEvaluation"]);
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

                    totalEmpGeneralPoint += TextUtils.ToDecimal(item["EmployeeEvaluation"]);
                    totalTBPGeneralPoint += TextUtils.ToDecimal(item["TBPEvaluation"]);
                    totalBGDGeneralPoint += TextUtils.ToDecimal(item["BGDEvaluation"]);
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
            catch (Exception ex)
            {
                MessageBox.Show($"LoadTotalAVGNew\r\n{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        #endregion

        #region hàm tính điểm đánh giá, điểm hệ số mới TNB update 11/09/25
        private DataTable CalculatorAvgPointNew(DataTable dataTable, bool isSpecial = false)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"LoadTotalAVGNew\r\n{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return dataTable;
            }
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

        private void LoadKPIRuleNew()
        {
            try
            {
                tlDataKPIRule.ClearNodes();
                tlDataKPIRule.DataSource = null;
                int kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue);
                int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
                //empID = 55;
                //KPIPositionEmployeeModel positionEmp = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", empID).FirstOrDefault() ?? new KPIPositionEmployeeModel();
                //if (positionEmp == null)
                //{
                //    DataTable dt = new DataTable();
                //    grdTeam.DataSource = dt;
                //    treeList3.DataSource = dt;
                //    return;
                //}

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
                //


                Expression ex1 = new Expression("KPISessionID", kpiSessionID);
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

                int empPointID = GetKPIEmployeePointID(kpiRule.ID);
                //if (positionEmp.KPIPosiotionID >= 4)
                //if (position.TypePosition >= 3)
                {
                    //DataTable dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeam", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
                    DataTable dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeamNew", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
                    //DataTable dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeam_test", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
                    grdTeam.DataSource = dtTeam;
                }

                //DataTable dtKpiRule = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointID", "LMKTable1",
                //                                                new string[] { "@KPIEmployeePointID" },
                //                                                new object[] { empPointID });

                //return;
                DataTable dtKpiRule = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointIDNew", "spGetEmployeeRulePointByKPIEmpPointIDNew",
                                                                        new string[] { "@KPIEmployeePointID", "@IsPublic" },
                                                                        new object[] { empPointID, 1 });
                tlDataKPIRule.DataSource = dtKpiRule;
                tlDataKPIRule.ExpandAll();
                // ========================= 09/12/2024 =======================================================
                List<KPIEmployeePointDetailModel> lst = SQLHelper<KPIEmployeePointDetailModel>.FindByAttribute("KPIEmployeePointID", empPointID);
                bool isAdminConfirm = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsAdminConfirm));
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
                        LoadPointRuleLastMonthNew(empPointID);
                    }
                }
                CalculatorPoint(empPointID, TextUtils.ToInt(positionEmp.KPIPosiotionID));


                DataRow row = dtKpiRule.NewRow();
                row["STT"] = "3.9";
                row["EvaluationCode"] = "NewLine";
                row["FirstMonth"] = -1;
                row["SecondMonth"] = -2;
                row["ThirdMonth"] = -3;


                tlDataKPIRule.AppendNode(row, null);
                tlDataKPIRule.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }

        }
        #region LoadPointRuleNew
        private void LoadPointRuleNew(int empPointID)
        {
            try
            {
                //List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointID",
                //                                                       new string[] { "@KPIEmployeePointID" },
                //                                                       new object[] { empPointID });

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
                //decimal teamKPIChuyenMon = TextUtils.ToDecimal(grvTeam.Columns["KPIChuyenMon"].SummaryItem.SummaryValue);
                decimal teamKPIChuyenMon = TextUtils.ToDecimal(grvTeam.Columns[colKPIChuyenMon.FieldName].SummaryItem.SummaryValue);
                decimal teamKPIPLC = TextUtils.ToDecimal(grvTeam.Columns["KPIPLC"].SummaryItem.SummaryValue);
                decimal teamKPIVISION = TextUtils.ToDecimal(grvTeam.Columns["KPIVision"].SummaryItem.SummaryValue);
                decimal teamKPISOFTWARE = TextUtils.ToDecimal(grvTeam.Columns["KPISoftware"].SummaryItem.SummaryValue);
                decimal missingTool = TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);  //làm mất mát hỏng thiết bị 12/12/2024
                //decimal TEAM21 = TextUtils.ToDecimal(grvTeam.Columns["ComplaneAndMissing"].SummaryItem.SummaryValue);
                //decimal TEAM214 = TextUtils.ToDecimal(grvTeam.Columns["DeadlineDelay"].SummaryItem.SummaryValue);
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
                     new KPISumarizeDTO(){ EvaluationCode = "TEAMKPICHUYENMON", ThirdMonth = teamKPIChuyenMon},
                     //new KPISumarizeDTO(){ EvaluationCode = "TEAM214", ThirdMonth = TEAM214},
                     //new KPISumarizeDTO(){ EvaluationCode = "TEAM21", ThirdMonth = TEAM21},
                     new KPISumarizeDTO(){ EvaluationCode = "MA11", ThirdMonth = totalErrorTBP}, // update 13/12/2024
                 });


                Lib.LockEvents = true;
                foreach (KPISumarizeDTO item in lstResult)
                {
                    TreeListNode node = tlDataKPIRule.GetNodeList().FirstOrDefault(x => item.EvaluationCode == TextUtils.ToString(x.GetValue(colEvaluationCode)));
                    if (node == null) continue;
                    node.SetValue(colFirstMonth, item.FirstMonth);
                    node.SetValue(colSecondMonth, item.SecondMonth);
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
        #region LoadPointRuleLastMonthNew
        private void LoadPointRuleLastMonthNew(int empPointID)
        {
            try
            {
                //List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointID",
                //                                                       new string[] { "@KPIEmployeePointID" },
                //                                                       new object[] { empPointID });

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
                   new KPISumarizeDTO(){ EvaluationCode = "TEAM04", ThirdMonth = customerComplaint + deadlineDelay},//update  12/12/2024
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
                    TreeListNode node = tlDataKPIRule.GetNodeList().FirstOrDefault(x => item.EvaluationCode == TextUtils.ToString(x.GetValue(colEvaluationCode)));
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

        private void tlDataKPIRule_CustomDrawFooterCell(object sender, CustomDrawFooterCellEventArgs e)
        {
            int kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue);
            int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));

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

            string sumText = $"Điểm xếp loại: {e.Info.DisplayText} ({GetDisplayText(Convert.ToDecimal(e.Info.DisplayText))})";
            string customText = $"Điểm cuối cùng: {kPIEmployeePoint.TotalPercentActual} ({GetDisplayText(TextUtils.ToDecimal(kPIEmployeePoint.TotalPercentActual))})";

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
    }
}

