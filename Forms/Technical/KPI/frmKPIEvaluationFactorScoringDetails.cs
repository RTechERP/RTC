using DevExpress.Utils;
using BaseBusiness.DTO;
using BMS.Model;
using BMS.Utils;
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
using DevExpress.Charts.Model;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.Utils.Drawing;

namespace BMS
{
    public partial class frmKPIEvaluationFactorScoringDetails : _Forms
    {

        /// <summary>
        ///  1: Nhân viên; 2: TBP; 3: Giám đốc; 4: Admin
        /// </summary>
        public int typePoint;
        public int employeeID = 0;
        public KPIExamModel kpiExam = new KPIExamModel();
        UserTeamModel team = SQLHelper<UserTeamModel>.FindByAttribute("LeaderID", Global.EmployeeID).FirstOrDefault();
        public int status = 0;

        //============= update 02/01/2025 =============
        private bool isTBP = false;
        private int empPointID = 0;
        private KPIPositionEmployeeModel positionEmp = new KPIPositionEmployeeModel();


        public bool isAdminConfirm = false;

        //==== -- 160525 -- lee min khooi -- update
        public int _departmentID = 0;


        int departmentCoKhi = 10;

        public frmKPIEvaluationFactorScoringDetails()
        {
            InitializeComponent();
        }

        private void frmKPIEvaluationFactorScoringDetails_Load(object sender, EventArgs e)
        {

            //DataTable list2 = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
            //                                            new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
            //                                            new object[] { 133, 2, 58, 1 });
            ////treeData2.DataSource = CalculatorAvgPoint(list2);
            //treeData2.DataSource = list2;

            if (team != null && typePoint == 2)
            {
                List<UserTeamLinkModel> lstTeam = SQLHelper<UserTeamLinkModel>.FindByAttribute("UserID", team.ID);
                bool isAccept = Global.IsAdmin || lstTeam.Any(p => p.UserID == employeeID);
                btnSave.Enabled = btnSaveAndClose.Enabled = !isAccept;
            }

            try
            {
                //--160525-- lee min khooi-- update
                if (_departmentID == departmentCoKhi) LoadEventForCKTK();



                LoadPositionType();
                LoadEmployee();
                LoadKPISession();
                LoadKPIExam();
                LoadEmpPointID(); // update 02/01/2025


                LoadDetails();
                //LoadKPIRule();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            if (!(Global.IsAdmin && Global.EmployeeID <= 0))
            {
                if (typePoint == 2)
                {
                    btnSave.Enabled = btnSaveAndClose.Enabled = (status != 3 && status != 3);
                }
                else if (typePoint == 1)
                {
                    btnSave.Enabled = btnSaveAndClose.Enabled = !(status > 2);
                    xtraTabPage4.PageVisible = xtraTabPage5.PageVisible = false;
                }
                else if (typePoint == 4)
                {
                    xtraTabPage4.PageVisible = xtraTabPage5.PageVisible = true;
                    xtraTabPage1.PageVisible = xtraTabPage2.PageVisible = xtraTabPage3.PageVisible = xtraTabPage6.PageVisible = false;
                }


                btnLoadData.Enabled = !(typePoint == 3);
            }
            treeDataRule.ContextMenuStrip = contextMenuStrip1; //TN.Binh update 
        }
        private void LoadEmpPointID()
        {
            int kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue);
            int empID = TextUtils.ToInt(cboEmployee.EditValue);

            //positionEmp = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", empID).FirstOrDefault() ?? new KPIPositionEmployeeModel();
            //Get possition của nhân viên
            var expPoint1 = new Expression(KPIPositionEmployeeModel_Enum.EmployeeID, empID);
            var expPoint2 = new Expression(KPIPositionModel_Enum.KPISessionID, kpiSessionID);
            var expPoint3 = new Expression(KPIPositionEmployeeModel_Enum.IsDeleted, 0);

            var kpiPositions = SQLHelper<KPIPositionModel>.FindByExpression(expPoint2.And(expPoint3));
            var kpiPositionEmployees = SQLHelper<KPIPositionEmployeeModel>.FindByExpression(expPoint1.And(expPoint3));

            positionEmp = (from p in kpiPositions
                           join pe in kpiPositionEmployees on p.ID equals pe.KPIPosiotionID
                           select pe)
                     .FirstOrDefault() ?? new KPIPositionEmployeeModel();
            //


            Expression ex1 = new Expression("KPISessionID", kpiSessionID);
            //Expression ex2 = new Expression("KPIPositionID", TextUtils.ToInt(positionEmp.KPIPosiotionID) > 0 ? TextUtils.ToInt(positionEmp.KPIPosiotionID) : 1);
            Expression ex2 = new Expression("KPIPositionID", TextUtils.ToInt(positionEmp.KPIPosiotionID));
            Expression ex3 = new Expression("IsDeleted", 0);
            KPIEvaluationRuleModel kpiRule = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPIEvaluationRuleModel();

            empPointID = GetKPIEmployeePointID(kpiRule.ID);
            //KPIEmployeePointModel empPointModel = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);
            //KPIEvaluationRuleModel ruleModel = SQLHelper<KPIEvaluationRuleModel>.FindByID(TextUtils.ToInt(empPointModel.KPIEvaluationRuleID));
        }
        private void LoadPositionType()
        {
            List<KPIPositionModel> lst = SQLHelper<KPIPositionModel>.FindByAttribute("IsDeleted", 0);
            //cboPosition.DataSource = lst;
            //cboPosition.ValueMember = "ID";
            //cboPosition.DisplayMember = "PositionName";
        }
        private void LoadKPISession()
        {
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(p => p.ID).ToList();
            cboKPISession.Properties.DataSource = lst;
            cboKPISession.Properties.DisplayMember = "Code";
            cboKPISession.Properties.ValueMember = "ID";
            cboKPISession.EditValue = kpiExam.KPISessionID;
        }
        private void cboKPISession_EditValueChanged(object sender, EventArgs e)
        {
            LoadKPIExam();
        }
        private void LoadKPIExam()
        {
            int kpiSessionId = TextUtils.ToInt(cboKPISession.EditValue);
            Expression ex1 = new Expression("KPISessionID", kpiSessionId);
            Expression ex2 = new Expression("IsDeleted", 0);
            List<KPIExamModel> lst = SQLHelper<KPIExamModel>.FindByExpression(ex1.And(ex2));

            //DataTable dt = SQLHelper<KPIExamModel>.LoadDataFromSP("spGetKPIExam", new string[] { "@EmployeeID", "@KPISessionID" },
            //                                                                        new object[] { Global.EmployeeID, kpiSessionId });

            cboKpiExam.Properties.DataSource = lst;
            cboKpiExam.Properties.ValueMember = "ID";
            cboKpiExam.Properties.DisplayMember = "ExamName";
            cboKpiExam.EditValue = kpiExam.ID;


        }
        private void LoadDetails()
        {
            cboEmployee.EditValue = employeeID;
        }
        private void LoadData()
        {

            if (Lib.LockEvents) return;
            try
            {
                Lib.LockEvents = true;
                //// ==================================== 09/12/2024 =================================================
                //int kpiSessionId = TextUtils.ToInt(cboKPISession.EditValue);
                //KPIPositionEmployeeModel empPosition = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", employeeID).FirstOrDefault() ?? new KPIPositionEmployeeModel();
                //Expression ex1 = new Expression("KPISessionID", kpiSessionId);
                //Expression ex2 = new Expression("KPIPositionID", empPosition.KPIPosiotionID > 0 ? empPosition.KPIPosiotionID : 1); // 1 là kỹ thuật
                //Expression ex3 = new Expression("IsDeleted", 0);
                //KPIEvaluationRuleModel rule = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPIEvaluationRuleModel();

                KPIEmployeePointModel empPoint = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);
                bool isPublic = typePoint == 2 || typePoint == 3 || empPoint.IsPublish == true;
                employeeID = TextUtils.ToInt(cboEmployee.EditValue);
                int empId = employeeID;
                int kpiExamID = TextUtils.ToInt(cboKpiExam.EditValue);


                //DataTable list = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
                //                                            new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                //                                            new object[] { empId, 1, kpiExam, isPublic });
                //DataRow parentRow = list.NewRow();
                //parentRow["ID"] = -1;
                //parentRow["ParentID"] = 0;
                //parentRow["EvaluationContent"] = "TỔNG HỆ SỐ";
                //parentRow["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
                //list.AcceptChanges();
                //list.Rows.Add(parentRow);

                //treeData.DataSource = CalculatorAvgPoint(list);
                //treeData.ExpandAll();


                //DataTable list2 = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
                //                                            new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                //                                            new object[] { empId, 2, kpiExam, isPublic });
                //treeData2.DataSource = CalculatorAvgPoint(list2);
                ////treeData2.DataSource = list2;
                //treeData2.ExpandAll();


                //DataTable list3 = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
                //                                            new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                //                                            new object[] { empId, 3, kpiExam, isPublic });
                ////DataRow parentRow3 = list3.NewRow();
                ////parentRow3["ID"] = -1;
                ////parentRow3["ParentID"] = 0;
                ////parentRow3["EvaluationContent"] = "TỔNG HỆ SỐ";
                ////parentRow3["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
                ////list3.AcceptChanges();
                ////list3.Rows.Add(parentRow3);
                //treeData3.DataSource = CalculatorAvgPoint(list3);
                ////treeData3.DataSource = list3;
                //treeData3.ExpandAll();

                //======== update 02/01/2024  ============
                LoadKPIKyNang(empId, kpiExamID, isPublic);
                LoadKPIChung(empId, kpiExamID, isPublic);
                LoadKPIChuyenMon(empId, kpiExamID, isPublic);
                //LoadTotalAVGNew();
                //LoadKPIRule();

                //TN.Binh update 02/10/25
                if (_departmentID == departmentCoKhi) LoadSumaryRank_CKTK();
                else
                {
                    LoadTotalAVGNew();
                    LoadKPIRule();
                }
                //end

                if (typePoint == 1)//cá nhân
                {
                    colGeneralEmployeePoint.OptionsColumn.AllowEdit = true;
                    colGeneralEmployeePoint.OptionsColumn.ReadOnly = false;

                    colTree2EmployeePoint.OptionsColumn.AllowEdit = true;
                    colTree2EmployeePoint.OptionsColumn.ReadOnly = false;

                    colEmployeePoint.OptionsColumn.AllowEdit = true;
                    colEmployeePoint.OptionsColumn.ReadOnly = false;

                }
                else if (typePoint == 2) //TBP
                {
                    colTree2TBPPointInput.OptionsColumn.AllowEdit = true;
                    colTree2TBPPointInput.OptionsColumn.ReadOnly = false;

                    colGeneralTBPPointInput.OptionsColumn.AllowEdit = true;
                    colGeneralTBPPointInput.OptionsColumn.ReadOnly = false;

                    //colTBPPoint.OptionsColumn.AllowEdit = true;
                    //colTBPPoint.OptionsColumn.ReadOnly = false;

                    colTBPPointInput.OptionsColumn.AllowEdit = true;
                    colTBPPointInput.OptionsColumn.ReadOnly = false;


                }
                else if (typePoint == 3) //bgd
                {
                    colTree2BGDPointInput.OptionsColumn.AllowEdit = true;
                    colTree2BGDPointInput.OptionsColumn.ReadOnly = false;

                    colGeneralBGDPointInput.OptionsColumn.AllowEdit = true;
                    colGeneralBGDPointInput.OptionsColumn.ReadOnly = false;

                    //colBGDPoint.OptionsColumn.AllowEdit = true;
                    //colBGDPoint.OptionsColumn.ReadOnly = false;

                    colBGDPointInput.OptionsColumn.AllowEdit = true;
                    colBGDPointInput.OptionsColumn.ReadOnly = false;
                }
            }
            finally
            {
                Lib.LockEvents = false;
            }

        }
        private void LoadEmployee()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.DataSource = list;
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
        }


        //private void ChangeValueNameCode()
        //{
        //    txtCode.Text = $"KPI_{txtYear.Value}_Q{txtQuarter.Value}";
        //    txtName.Text = $"Kỳ đánh giá quý {txtQuarter.Value}-{txtYear.Value}";
        //}

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void treeData_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                if (_departmentID == departmentCoKhi) return;
            }

            bool isStyle = (e.Column.FieldName == colTBPPointInput.FieldName || e.Column.FieldName == colEmployeePoint.FieldName || e.Column.FieldName == colBGDPointInput.FieldName);
            if (isStyle)
            {

                if (e.Column.OptionsColumn.AllowEdit)
                {
                    e.Appearance.BackColor = Color.LightYellow;
                }
            }
        }

        private void treeData_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //TreeList treeList = (TreeList)sender;
            //if (treeList == null) return;


            //int employeePoint = TextUtils.ToInt(treeList.FocusedNode.GetValue("EmployeePoint"));
            //if (e.Column.FieldName == "TBPPointInput")
            //{
            //    decimal tbpPointInput = TextUtils.ToDecimal(e.Value);

            //    decimal diff = Math.Abs(tbpPointInput - employeePoint);
            //    decimal tbpPoint = tbpPointInput;
            //    if (diff >= 2 && _departmentID != departmentCoKhi) tbpPoint = tbpPointInput / 2;
            //    treeList.FocusedNode.SetValue(colTBPPoint, tbpPoint);

            //    if (_departmentID == departmentCoKhi) treeList.FocusedNode.SetValue(colBGDPoint, tbpPoint);

            //}

            //if (e.Column.FieldName == "BGDPointInput")
            //{
            //    decimal bgdPointInput = TextUtils.ToDecimal(e.Value);

            //    decimal diff = Math.Abs(bgdPointInput - employeePoint);
            //    decimal bgdPoint = bgdPointInput;
            //    if (diff >= 2 && _departmentID != departmentCoKhi) bgdPoint = bgdPointInput / 2;
            //    treeList.FocusedNode.SetValue(colBGDPoint, bgdPoint);
            //}



            //DataTable dt = (DataTable)treeList.DataSource;
            //treeList.DataSource = CalculatorAvgPoint(dt);
            //treeList.ExpandAll();
            TreeList treeList = (TreeList)sender;
            if (treeList == null) return;

            //TN.Binh update 08/09/25 -- fill dữ liệu trực tiếp lên cột điểm NV đánh giá 
            TreeListNode currentNode = e.Node;
            if (currentNode == null) return;

            decimal employeePoint = TextUtils.ToDecimal(currentNode.GetValue("EmployeePoint"));
            decimal coefficient = TextUtils.ToDecimal(currentNode.GetValue("Coefficient"));

            // Gán lại giá trị colEmployeeCoefficient = colEmployeePoint * colCoefficient
            currentNode.SetValue(colEmployeeCoefficient, employeePoint * coefficient);
            currentNode.SetValue(colEmployeeEvaluation, employeePoint);
            //end

            if (e.Column.FieldName == "TBPPointInput")
            {

                currentNode.SetValue(colEmployeeCoefficient, employeePoint * coefficient);

                decimal tbpPointInput = TextUtils.ToDecimal(e.Value);

                decimal diff = Math.Abs(tbpPointInput - employeePoint);
                decimal tbpPoint = tbpPointInput;
                if (diff >= 2) tbpPoint = tbpPointInput / 2;
                treeList.FocusedNode.SetValue(colTBPPoint, tbpPoint);
                currentNode.SetValue(colTBPEvaluation, tbpPoint); //TN.Binh update 09/09/25
                currentNode.SetValue(colTBPCoefficient, tbpPoint * coefficient); //TN.Binh update 09/09/25
            }

            if (e.Column.FieldName == "BGDPointInput")
            {
                decimal bgdPointInput = TextUtils.ToDecimal(e.Value);

                decimal diff = Math.Abs(bgdPointInput - employeePoint);
                decimal bgdPoint = bgdPointInput;
                if (diff >= 2) bgdPoint = bgdPointInput / 2;
                treeList.FocusedNode.SetValue(colBGDPoint, bgdPoint);
                currentNode.SetValue(colBGDEvaluation, bgdPoint); //TN.Binh update 09/09/25
                currentNode.SetValue(colBGDCoefficient, bgdPoint * coefficient); //TN.Binh update 09/09/25
            }
            //TN.Binh update 08/09/25 -- fill dữ liệu trực tiếp lên cột điểm NV đánh giá 
            // Đồng bộ giá trị với DataRow để đảm bảo DataSource được cập nhật
            DataRow row = treeList.GetDataRecordByNode(currentNode) as DataRow;
            if (row != null)
            {
                row["EmployeeCoefficient"] = employeePoint * coefficient;
                row["EmployeeEvaluation"] = employeePoint;
                if (e.Column.FieldName == "TBPPointInput")
                {
                    row["TBPPoint"] = currentNode.GetValue(colTBPPoint);
                }
                else if (e.Column.FieldName == "BGDPointInput")
                {
                    row["BGDPoint"] = currentNode.GetValue(colBGDPoint);
                }
            }
            //end

            // Chỉ làm mới node hiện tại thay vì toàn bộ TreeList
            treeList.RefreshNode(currentNode);

            DataTable dt = (DataTable)treeList.DataSource;
            treeList.DataSource = CalculatorAvgPointNew(dt);
            treeList.ExpandAll();
        }
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

        private bool SaveData()
        {

            treeData.CloseEditor();
            treeData2.CloseEditor();
            treeData3.CloseEditor();

            if (TextUtils.ToInt(cboKPISession.EditValue) <= 0)
            {
                MessageBox.Show("Hãy chọn Kỳ đánh giá KPI", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (TextUtils.ToInt(cboKpiExam.EditValue) <= 0)
            {
                MessageBox.Show("Hãy chọn Bài đánh giá KPI", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (TextUtils.ToInt(cboEmployee.EditValue) <= 0)
            {
                MessageBox.Show("Hãy chọn Nhân viên", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {


                    foreach (TreeListNode item in treeData.GetNodeList())
                    {
                        int Id = TextUtils.ToInt(treeData.GetRowCellValue(item, colKPIEvaluationPointID));
                        if (Id < 0) continue;
                        KPIEvaluationPointModel model = SQLHelper<KPIEvaluationPointModel>.FindByID(Id);

                        model.EmployeeID = employeeID;
                        if (typePoint == 1)
                        {
                            if (!Global.IsAdmin && employeeID != 0 && employeeID != Global.EmployeeID)
                            {
                                MessageBox.Show($"Bạn không thể đánh giá KPI của người khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            model.EmployeePoint = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colEmployeePoint));
                            model.EmployeeEvaluation = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colEmployeeEvaluation));
                            model.Status = 0;
                        }
                        else if (typePoint == 2)
                        {
                            model.TBPPoint = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colTBPPoint));
                            model.TBPID = Global.EmployeeID;
                            model.TBPEvaluation = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colTBPEvaluation));
                            if (_departmentID == departmentCoKhi) model.BGDEvaluation = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colTBPEvaluation));

                        }
                        else if (typePoint == 3)
                        {
                            model.BGDPoint = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colBGDPoint));
                            model.BGDID = Global.EmployeeID;
                            model.BGDEvaluation = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colBGDEvaluation));
                        }

                        model.EmployeeCoefficient = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colEmployeeCoefficient));
                        model.TBPCoefficient = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colTBPCoefficient));
                        model.BGDCoefficient = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colBGDCoefficient));


                        model.KPIEvaluationFactorsID = TextUtils.ToInt(treeData.GetRowCellValue(item, colID));

                        model.TBPPointInput = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colTBPPointInput));
                        model.BGDPointInput = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colBGDPointInput));


                        if (model.ID > 0) SQLHelper<KPIEvaluationPointModel>.Update(model);
                        else model.ID = SQLHelper<KPIEvaluationPointModel>.Insert(model).ID;
                    }

                    foreach (TreeListNode item in treeData2.GetNodeList())
                    {
                        int Id = TextUtils.ToInt(treeData2.GetRowCellValue(item, colTree2KPIEvaluationPointID));
                        if (Id < 0) continue;
                        KPIEvaluationPointModel model = SQLHelper<KPIEvaluationPointModel>.FindByID(Id);
                        string stt = TextUtils.ToString(treeData2.GetRowCellValue(item, colTree2STT));

                        model.EmployeeID = employeeID;
                        if (typePoint == 1)
                        {
                            model.EmployeeEvaluation = TextUtils.ToDecimal(treeData2.GetRowCellValue(item, colTree2EmployeeEvaluation));
                            model.EmployeePoint = TextUtils.ToDecimal(treeData2.GetRowCellValue(item, colTree2EmployeePoint));
                            model.Status = 0;
                        }
                        else if (typePoint == 2)
                        {
                            model.TBPPoint = TextUtils.ToDecimal(treeData2.GetRowCellValue(item, colTree2TBPPoint));
                            model.TBPID = Global.EmployeeID;
                            model.TBPEvaluation = TextUtils.ToDecimal(treeData2.GetRowCellValue(item, colTree2TBPEvaluation));
                            if (_departmentID == departmentCoKhi) model.BGDEvaluation = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colTBPEvaluation));
                        }
                        else if (typePoint == 3)
                        {
                            model.BGDPoint = TextUtils.ToDecimal(treeData2.GetRowCellValue(item, colTree2BGDPoint));
                            model.BGDID = Global.EmployeeID;
                            model.BGDEvaluation = TextUtils.ToDecimal(treeData2.GetRowCellValue(item, colTree2BGDEvaluation));
                        }
                        model.EmployeeCoefficient = TextUtils.ToDecimal(treeData2.GetRowCellValue(item, colTree2EmployeeCoefficient));
                        model.TBPCoefficient = TextUtils.ToDecimal(treeData2.GetRowCellValue(item, colTree2TBPCoefficient));
                        model.BGDCoefficient = TextUtils.ToDecimal(treeData2.GetRowCellValue(item, colTree2BGDCoefficient));

                        model.KPIEvaluationFactorsID = TextUtils.ToInt(treeData2.GetRowCellValue(item, colTree2ID));

                        model.TBPPointInput = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colTBPPointInput));
                        model.BGDPointInput = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colBGDPointInput));


                        if (model.ID > 0) SQLHelper<KPIEvaluationPointModel>.Update(model);
                        else model.ID = SQLHelper<KPIEvaluationPointModel>.Insert(model).ID;
                    }

                    foreach (TreeListNode item in treeData3.GetNodeList())
                    {
                        int Id = TextUtils.ToInt(treeData3.GetRowCellValue(item, colGeneralKPIEvaluationPointID));
                        if (Id < 0) continue;
                        KPIEvaluationPointModel model = SQLHelper<KPIEvaluationPointModel>.FindByID(Id);
                        string stt = TextUtils.ToString(treeData3.GetRowCellValue(item, colGeneralSTT));

                        model.EmployeeID = employeeID;
                        if (typePoint == 1)
                        {
                            model.EmployeeEvaluation = TextUtils.ToDecimal(treeData3.GetRowCellValue(item, colGeneralEmployeeEvaluation));
                            model.EmployeePoint = TextUtils.ToDecimal(treeData3.GetRowCellValue(item, colGeneralEmployeePoint));
                            model.Status = 0;
                        }
                        else if (typePoint == 2)
                        {
                            model.TBPPoint = TextUtils.ToDecimal(treeData3.GetRowCellValue(item, colGeneralTBPPoint));
                            model.TBPID = Global.EmployeeID;
                            model.TBPEvaluation = TextUtils.ToDecimal(treeData3.GetRowCellValue(item, colGeneralTBPEvaluation));
                        }
                        else if (typePoint == 3)
                        {
                            model.BGDPoint = TextUtils.ToDecimal(treeData3.GetRowCellValue(item, colGeneralBGDPoint));
                            model.BGDID = Global.EmployeeID;
                            model.BGDEvaluation = TextUtils.ToDecimal(treeData3.GetRowCellValue(item, colGeneralBGDEvaluation));
                        }
                        model.EmployeeCoefficient = TextUtils.ToDecimal(treeData3.GetRowCellValue(item, colGeneralEmployeeCoefficient));
                        model.TBPCoefficient = TextUtils.ToDecimal(treeData3.GetRowCellValue(item, colGeneralTBPCoefficient));
                        model.BGDCoefficient = TextUtils.ToDecimal(treeData3.GetRowCellValue(item, colGeneralBGDCoefficient));

                        model.KPIEvaluationFactorsID = TextUtils.ToInt(treeData3.GetRowCellValue(item, colGeneralID));

                        model.TBPPointInput = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colTBPPointInput));
                        model.BGDPointInput = TextUtils.ToDecimal(treeData.GetRowCellValue(item, colBGDPointInput));


                        if (model.ID > 0) SQLHelper<KPIEvaluationPointModel>.Update(model);
                        else model.ID = SQLHelper<KPIEvaluationPointModel>.Insert(model).ID;
                    }






                    // ============  -- 200525 -- lee min khooi -- update
                    // Lưu thông tin tổng hợp đánh giá
                    //LoadTotalAVGNew();
                    //TN.Binh update 01/10/25
                    if (_departmentID == departmentCoKhi) LoadSumaryRank_CKTK();
                    else LoadTotalAVGNew();
                    //end

                    if (grvMaster.RowCount <= 0) return true;
                    try
                    {
                        int empId = employeeID;
                        int kpiExamID = TextUtils.ToInt(cboKpiExam.EditValue);
                        Expression ex1 = new Expression("EmployeeID", empId);
                        Expression ex2 = new Expression("KPIExamID", kpiExamID);

                        foreach (BandedGridColumn col in grvMaster.Columns)
                        {
                            if (col.Name.StartsWith("colPoint"))
                            {
                                string subStr = col.Name.Substring(8, 1);
                                //int specializationTypeID = TextUtils.ToInt(subStr);
                                int specializationTypeID = TextUtils.ToInt(col.Tag);
                                Expression ex3 = new Expression("SpecializationType", specializationTypeID);
                                KPISumaryEvaluationModel sumaryModel = SQLHelper<KPISumaryEvaluationModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault()
                                                                ?? new KPISumaryEvaluationModel();
                                sumaryModel.SpecializationType = specializationTypeID;
                                sumaryModel.EmployeeID = empId;
                                sumaryModel.KPIExamID = kpiExamID;
                                sumaryModel.EmployeePoint = TextUtils.ToDecimal(grvMaster.GetRowCellValue(0, col.FieldName));
                                sumaryModel.TBPPoint = TextUtils.ToDecimal(grvMaster.GetRowCellValue(1, col.FieldName));
                                sumaryModel.BGDPoint = TextUtils.ToDecimal(grvMaster.GetRowCellValue(2, col.FieldName));

                                if (sumaryModel.ID > 0) SQLHelper<KPISumaryEvaluationModel>.Update(sumaryModel);
                                else SQLHelper<KPISumaryEvaluationModel>.Insert(sumaryModel);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        //MessageBox.Show(ex.ToString());
                        return false;
                    }
                    //================== END Update --200525-- lee min khooi-- 
                    return true;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                return false;
            }


        }


        bool SaveDataKPI()
        {
            if (typePoint == 4)
            {
                return SaveDataRule();
            }
            else
            {
                bool isSaveRule = true;
                if (_departmentID != departmentCoKhi) isSaveRule = SaveDataRule(); //--190525-- lee min khooi-- update
                bool isSaveKPI = SaveData();

                //if (!isSaveRule || !isSaveKPI) return false;
                return (isSaveRule && isSaveKPI);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //============== update 02/01/2024 ==============
            if (SaveDataKPI())
            {
                LoadData();
            }

        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            //============== update 02/01/2024 ==============
            //bool isSaveRule = SaveDataRule();
            //bool isSaveKPI = SaveData();
            //if (!isSaveRule || !isSaveKPI) return;

            if (SaveDataKPI())
            {

                this.DialogResult = DialogResult.OK;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmKPICriteriaView frm = new frmKPICriteriaView();
            KPISessionModel kPISession = SQLHelper<KPISessionModel>.FindByID(TextUtils.ToInt(cboKPISession.EditValue));
            frm.criteriaYear = TextUtils.ToInt(kPISession.YearEvaluation);
            frm.criteriaQuarter = TextUtils.ToInt(kPISession.QuarterEvaluation);
            frm.Show();
        }

        private void treeData_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            //bool isStyle = e.Column == colCoefficient || e.Column == colStandardPoint || e.Column == colEmployeeEvaluation || e.Column == colBGDEvaluation || e.Column == colTBPEvaluation ||
            //               e.Column == colEmployeeCoefficient || e.Column == colTBPCoefficient || e.Column == colBGDCoefficient;
            //if (isStyle)
            //{
            //    if (TextUtils.ToInt(e.Value) == 0)
            //    {
            //        e.DisplayText = "";
            //    }
            //}
        }

        private void treeData_ShowingEditor(object sender, CancelEventArgs e)
        {
            //if (treeData.FocusedNode.HasChildren)
            //{
            //    e.Cancel = true;
            //}

            if ((typePoint == 2 || typePoint == 3) && _departmentID == departmentCoKhi)
            {
                e.Cancel = !treeData.FocusedNode.HasChildren;
                return;
            }

            e.Cancel = treeData.FocusedNode.HasChildren;


        }

        private void treeData2_ShowingEditor(object sender, CancelEventArgs e)
        {
            //if (treeData2.FocusedNode.HasChildren)
            //{
            //    e.Cancel = true;
            //}

            e.Cancel = treeData2.FocusedNode.HasChildren;
        }

        private void cboKpiExam_EditValueChanged(object sender, EventArgs e)
        {
            LoadDetails();
        }

        private void treeData2_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column == colTree2Coefficient || e.Column == colTree2StandardPoint || e.Column == colTree2EmployeeEvaluation || e.Column == colTree2BGDEvaluation || e.Column == colTree2TBPEvaluation ||
            //               e.Column == colTree2EmployeeCoefficient || e.Column == colTree2TBPCoefficient || e.Column == colTree2BGDCoefficient)
            //{
            //    if (TextUtils.ToInt(e.Value) == 0)
            //    {
            //        e.DisplayText = "";
            //    }
            //}
        }

        private void treeData3_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column == colGeneralConficient || e.Column == colGeneralStandardPoint || e.Column == colGeneralEmployeeEvaluation || e.Column == colGeneralBGDEvaluation || e.Column == colGeneralTBPEvaluation ||
            //               e.Column == colGeneralEmployeeCoefficient || e.Column == colGeneralTBPCoefficient || e.Column == colGeneralBGDCoefficient)
            //{
            //    if (TextUtils.ToInt(e.Value) == 0)
            //    {
            //        e.DisplayText = "";
            //    }
            //}
        }

        private void treeData3_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeData3.FocusedNode.HasChildren)
            {
                e.Cancel = true;
            }

            e.Cancel = treeData3.FocusedNode.HasChildren;
        }

        private void treeData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TreeListNode nodeFocus1 = treeData.FocusedNode;
                TreeListNode nodeFocus2 = treeData2.FocusedNode;
                TreeListNode nodeFocus3 = treeData3.FocusedNode;
                //bool isTreeData1 = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? true : false;
                bool isTabOne = xtraTabControl1.SelectedTabPage == xtraTabPage1;
                bool isTabTwo = xtraTabControl1.SelectedTabPage == xtraTabPage2;
                DevExpress.XtraTreeList.TreeList data = isTabOne ? treeData : (isTabTwo ? treeData2 : treeData3);

                TreeListNode currentNode = isTabOne ? nodeFocus1 : (isTabTwo ? nodeFocus2 : nodeFocus3);

                List<TreeListNode> lst = data.GetNodeList();
                int currentIndex = lst.FindIndex(p => p == currentNode);
                if (currentIndex + 1 < lst.Count) data.SetFocusedNode(lst[currentIndex + 1]);
            }
        }

        private void treeData3_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            //TreeListNode currentNode = e.Node;
            //int standardPoint = TextUtils.ToInt(treeData3.GetRowCellValue(currentNode, colGeneralStandardPoint));
            //int EPoint = TextUtils.ToInt(treeData3.GetRowCellValue(currentNode, colGeneralEmployeePoint));
            //int TPoint = TextUtils.ToInt(treeData3.GetRowCellValue(currentNode, colGeneralTBPPoint));
            //int GPoint = TextUtils.ToInt(treeData3.GetRowCellValue(currentNode, colGeneralBGDPoint));
            //if (EPoint > standardPoint)
            //{
            //    MessageBox.Show("Mức tự đánh giá phải <= Điểm chuẩn!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    treeData3.SetRowCellValue(currentNode, colGeneralEmployeePoint, 0);
            //    return;
            //}
            //if (TPoint > standardPoint)
            //{
            //    MessageBox.Show("TBP/PGĐ đánh giá phải <= Điểm chuẩn!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    treeData3.SetRowCellValue(currentNode, colGeneralTBPPoint, 0);
            //    return;
            //}
            //if (GPoint > standardPoint)
            //{
            //    MessageBox.Show("Đánh giá của BGĐ phải <= Điểm chuẩn!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    treeData3.SetRowCellValue(currentNode, colGeneralBGDPoint, 0);
            //    return;
            //}
            //DataTable dt = (DataTable)treeData3.DataSource;
            //treeData3.DataSource = CalculatorAvgPoint(dt);
            //treeData3.ExpandAll();
        }


        private int GetKPIEmployeePointID(int ruleID)
        {
            int empID = employeeID;
            string empName = Global.AppFullName;
            if (empID <= 0)
            {
                MessageBox.Show($"Không tìm thấy ID của nhân viên [{empName}]", "Thông báo");
                return -1;
            }
            if (ruleID <= 0 && _departmentID != departmentCoKhi)
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

        private void treeData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            TreeList treeList = (TreeList)sender;
            if (treeList == null) return;

            treeList.FocusedColumn = colID;
            treeList.CloseEditor();
            treeList.Update();

            if (treeList.FocusedColumn.FieldName != colEmployeePoint.FieldName
                && treeList.FocusedColumn.FieldName != colTBPPointInput.FieldName
                && treeList.FocusedColumn.FieldName != colBGDPointInput.FieldName) return;

            TreeListNode parentNode = treeList.FocusedNode.ParentNode;
            if (parentNode == null) return;
            int standardPoint = TextUtils.ToInt(treeList.FocusedNode.GetValue(colStandardPoint.FieldName)); //-- 160525 -- lee min khooi -- update

            //int ePoint = TextUtils.ToInt(treeList.FocusedNode.GetValue(colEmployeePoint.FieldName));
            //int tbpPoint = TextUtils.ToInt(treeList.FocusedNode.GetValue(colTBPPointInput.FieldName));
            //int bgdPoint = TextUtils.ToInt(treeList.FocusedNode.GetValue(colBGDPointInput.FieldName));

            int value = TextUtils.ToInt(e.Value);
            if (value > standardPoint && _departmentID != departmentCoKhi)
            {
                e.Value = standardPoint;
            }

        }

        // =========================================  Update 02/01/2025 ==========================================================
        private void LoadKPIKyNang(int empId, int kpiExamID, bool isPublic)
        {
            treeData.ClearNodes();
            treeData.DataSource = null;

            DataTable list = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
                                                            new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                                                            new object[] { empId, 1, kpiExamID, isPublic });

            DataRow parentRow = list.NewRow();
            parentRow["ID"] = -1;
            parentRow["ParentID"] = 0;
            parentRow["EvaluationContent"] = "TỔNG HỆ SỐ";
            parentRow["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
            list.AcceptChanges();
            //if (_departmentID != departmentCoKhi)
            list.Rows.Add(parentRow);

            treeData.DataSource = _departmentID == departmentCoKhi ? CalculatorAvgPoint_CKTK(list) : CalculatorAvgPointNew(list);
            treeData.ExpandAll();
        }
        private void LoadKPIChung(int empId, int kpiExamID, bool isPublic)
        {
            if (_departmentID == departmentCoKhi) return;
            treeData3.ClearNodes();
            treeData3.DataSource = null;

            DataTable list3 = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "lmkTable",
                                                           new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                                                           new object[] { empId, 3, kpiExamID, isPublic });
            DataRow parentRow3 = list3.NewRow();
            parentRow3["ID"] = -1;
            parentRow3["ParentID"] = 0;
            parentRow3["EvaluationContent"] = "TỔNG HỆ SỐ";
            parentRow3["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
            list3.Rows.Add(parentRow3);
            list3.AcceptChanges();

            treeData3.DataSource = CalculatorAvgPointNew(list3);
            //treeData3.DataSource = list3;
            treeData3.ExpandAll();
        }
        private void LoadKPIChuyenMon(int empId, int kpiExamID, bool isPublic)
        {
            try
            {
                //if (_departmentID == departmentCoKhi) return;
                treeData2.ClearNodes();
                treeData2.DataSource = null;


                treeData2.ClearNodes();
                treeData2.DataSource = null;
                DataTable list2 = TextUtils.LoadDataFromSP("spGetAllKPIEvaluationPoint", "spGetAllKPIEvaluationPoint",
                                                               new string[] { "@EmployeeID", "@EvaluationType", "@KPIExamID", "@IsPulbic" },
                                                               new object[] { empId, 2, kpiExamID, isPublic });

                DataRow parentRow2 = list2.NewRow();
                parentRow2["ID"] = -1;
                parentRow2["ParentID"] = 0;
                parentRow2["EvaluationContent"] = "TỔNG HỆ SỐ";
                parentRow2["VerificationToolsContent"] = "TỔNG ĐIỂM TRUNG BÌNH";
                list2.Rows.Add(parentRow2);
                list2.AcceptChanges();

                //treeData2.DataSource = CalculatorAvgPointNew(list2, true);
                treeData2.DataSource = _departmentID == departmentCoKhi ? CalculatorAvgPoint_CKTK(list2) : CalculatorAvgPointNew(list2, true);
                treeData2.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"LoadKPIChuyenMon\r\n{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        //private void LoadTotalAVG()
        //{
        //    try
        //    {
        //        grdMaster.DataSource = null;


        //        //-- 160525 -- lee min khooi -- update
        //        if (_departmentID == departmentCoKhi)
        //        {
        //            LoadSumaryRank_CKTK();
        //            return;
        //        }
        //        //=======END//=======

        //        DataTable dtSkill = (DataTable)treeData.DataSource;
        //        decimal totalEmpSkillPoint = 0;
        //        decimal totalTBPSkillPoint = 0;
        //        decimal totalBGDSkillPoint = 0;

        //        int countSkillPoint = 0;
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

        //        DataTable dt = (DataTable)treeData2.DataSource;

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
        //            countPLC = countPLC > 0 ? countPLC : 1;
        //            countVision = countVision > 0 ? countVision : 1;
        //            countSoft = countSoft > 0 ? countSoft : 1;
        //            countViRobot = countViRobot > 0 ? countViRobot : 1;
        //        }

        //        DataTable dtGeneral = (DataTable)treeData3.DataSource;
        //        decimal totalEmpGeneralPoint = 0;
        //        decimal totalTBPGeneralPoint = 0;
        //        decimal totalBGDGeneralPoint = 0;


        //        decimal plcEmpPoint = 0;
        //        decimal visionEmpPoint = 0;
        //        decimal plcTBPPoint = 0;
        //        decimal visionTBPPoint = 0;
        //        decimal plcBGDPoint = 0;
        //        decimal visionBGDPoint = 0;

        //        int countGeneralPoint = 0;
        //        if (dtGeneral != null)
        //        {
        //            List<DataRow> lstGeneralPoint = dtGeneral.Select("ID = -1").ToList();

        //            countGeneralPoint = lstGeneralPoint.Count > 0 ? lstGeneralPoint.Count : 1;


        //            foreach (DataRow item in lstGeneralPoint)
        //            {
        //                totalEmpGeneralPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
        //                totalTBPGeneralPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
        //                totalBGDGeneralPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
        //            }


        //            plcEmpPoint = (2 * totalEmpPLCPoint / countPLC + totalEmpViRobotPoint / countViRobot) / 3;
        //            visionEmpPoint = (2 * totalEmpPVisionPoint / countVision + totalEmpViRobotPoint / countViRobot) / 3;

        //            plcTBPPoint = (2 * totalTBPPLCPoint / countPLC + totalTBPViRobotPoint / countViRobot) / 3;
        //            visionTBPPoint = (2 * totalTBPVisionPoint / countVision + totalTBPViRobotPoint / countViRobot) / 3;


        //            plcBGDPoint = (2 * totalBGDPLCPoint / countPLC + totalBGDViRobotPoint / countViRobot) / 3;
        //            visionBGDPoint = (2 * totalBGDPVisionPoint / countVision + totalBGDViRobotPoint / countViRobot) / 3;

        //        }
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
        //        //MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
        //    }
        //}

        private void LoadKPIRule()
        {
            try
            {
                if (_departmentID == departmentCoKhi) return; // -- 190525 -- lee min khooi -- update
                treeDataRule.BeginUpdate();

                treeDataRule.ClearNodes();
                treeDataRule.DataSource = null;
                grdTeam.DataSource = null;


                //KPIPositionModel position = kpiPositions.FirstOrDefault(x => x.ID == positionEmp.KPIPosiotionID);

                int kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue);
                KPIPositionModel position = SQLHelper<KPIPositionModel>.FindAll().FirstOrDefault(x => x.ID == positionEmp.KPIPosiotionID &&
                                                                                                        x.KPISessionID == kpiSessionID &&
                                                                                                        x.IsDeleted == false) ?? new KPIPositionModel();
                //if (positionEmp.KPIPosiotionID >= 4)
                //if (position.TypePosition >= 3)
                {
                    //DataTable dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeam", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
                    DataTable dtTeam = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeamNew", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
                    grdTeam.DataSource = dtTeam;
                }


                //DataTable dtKpiRule = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointID", "LMKTable",
                //                                                new string[] { "@KPIEmployeePointID" },
                //                                                new object[] { empPointID });

                //return;
                DataTable dtKpiRule = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointIDNew", "spGetEmployeeRulePointByKPIEmpPointIDNew",
                                                                        new string[] { "@KPIEmployeePointID", "@IsPublic" },
                                                                        new object[] { empPointID, 1 });

                //DataTable dtKpiRule = TextUtils.ExecuteProcedureToDataTable("spGetEmployeeRulePointByKPIEmpPointIDNew", new { KPIEmployeePointID = empPointID, IsPublic = 1 });
                treeDataRule.DataSource = dtKpiRule;
                treeDataRule.ExpandAll();
                treeDataRule.EndUpdate();

                List<KPIEmployeePointDetailModel> lst = SQLHelper<KPIEmployeePointDetailModel>.FindByAttribute("KPIEmployeePointID", empPointID);

                var point = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);
                if (lst.Count <= 0)
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
                    {
                        LoadPointRuleNew(empPointID);
                    }
                }
                else if (!isAdminConfirm && point.IsPublish == false)
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
                    {
                        LoadPointRuleLastMonth(empPointID);
                    }
                
                }

                CalculatorPoint();

                DataRow row = dtKpiRule.NewRow();
                row["STT"] = "";
                row["EvaluationCode"] = "NewLine";
                row["FirstMonth"] = 0;
                row["SecondMonth"] = 0;
                row["ThirdMonth"] = 0;
                row["ParentID"] = 0;

                TreeListNode node = treeDataRule.FindNodeByFieldValue("EvaluationCode", "KPIKyNang");
                if (node != null)
                {
                    TreeListNode parentNode = node.ParentNode;
                    string parentCode = "";

                    if (parentNode == null) parentCode = "KPIKyNang";
                    else parentCode = parentNode.GetValue("STT").ToString();

                    int insertIndex = -1;
                    for (int i = 0; i < dtKpiRule.Rows.Count; i++)
                    {
                        if (dtKpiRule.Rows[i]["EvaluationCode"].ToString() == parentCode || dtKpiRule.Rows[i]["STT"].ToString() == parentCode)
                        {
                            insertIndex = i;
                            break;
                        }
                    }
                    if (insertIndex >= 0) dtKpiRule.Rows.InsertAt(row, insertIndex);
                }

                treeDataRule.RefreshDataSource();
                //SaveDataRule();
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }

        }

        private void LoadPointRuleLastMonth(int empPointID)
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
                decimal missingTool = TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);  //làm mất mát hỏng thiết bị 12/12/2024
                decimal teamKPICHUYENMON = TextUtils.ToDecimal(grvTeam.Columns["KPIChuyenMon"].SummaryItem.SummaryValue);
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
                    TreeListNode node = treeDataRule.GetNodeList().FirstOrDefault(x => item.EvaluationCode == TextUtils.ToString(x.GetValue(colEvaluationCode)));
                    if (node == null) continue;
                    //node.SetValue(colFirstMonth, item.FirstMonth);
                    //node.SetValue(colSecondMonth, item.SecondMonth);
                    node.SetValue(colThirdMonth, item.ThirdMonth);
                }

                var kpiSession = (KPISessionModel)cboKPISession.GetSelectedDataRow() ?? new KPISessionModel();
                int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
                var dtTraining = TextUtils.LoadDataSetFromSP("spGetCourseTraining"
                                                            , new string[] { "@Year", "@Quarter", "@EmployeeID" }
                                                            , new object[] { kpiSession.YearEvaluation, kpiSession.QuarterEvaluation, employeeID });

                //Tính tích cực tham gia training
                var nodeThuong2 = treeDataRule.FindNodeByFieldValue(colEvaluationCode.FieldName, "THUONG02");
                if (nodeThuong2 != null)
                {
                    var dtTHUONG02 = dtTraining.Tables[1];
                    nodeThuong2.SetValue(colFirstMonth, dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["FirstMonth"]) : 0);
                    nodeThuong2.SetValue(colSecondMonth, dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["SecondMonth"]) : 0);
                    nodeThuong2.SetValue(colThirdMonth, dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["ThirdMonth"]) : 0);
                }

                //Tính tổ chức training
                var nodeThuong3 = treeDataRule.FindNodeByFieldValue(colEvaluationCode.FieldName, "THUONG03");
                if (nodeThuong3 != null)
                {
                    var dtTHUONG03 = dtTraining.Tables[0];
                    nodeThuong3.SetValue(colFirstMonth, dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["FirstMonth"]) : 0);
                    nodeThuong3.SetValue(colSecondMonth, dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["SecondMonth"]) : 0);
                    nodeThuong3.SetValue(colThirdMonth, dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["ThirdMonth"]) : 0);
                }

                Lib.LockEvents = false;

                //CalculatorPoint(empPointID);

            }
            catch (Exception ex)
            {
                //MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
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
        //        decimal missingTool = TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);
        //        List<string> lstCodeTBP = new List<string>() { "MA03", "MA04", "NotWorking", "WorkLate" };
        //        var ltsMA11 = lstResult.Where(p => lstCodeTBP.Contains(p.EvaluationCode.Trim())).ToList();
        //        decimal totalErrorTBP = ltsMA11.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);

        //        lstResult.AddRange(new List<KPISumarizeDTO>
        //        {
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM01", ThirdMonth = timeWork},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM02", ThirdMonth = fiveS},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM03", ThirdMonth = reportWork},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM04", ThirdMonth = customerComplaint + missingTool + deadlineDelay},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM05", ThirdMonth = customerComplaint},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = missingTool},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIKYNANG", ThirdMonth = teamKPIKyNang},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIChung", ThirdMonth = teanKPIChung},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIPLC", ThirdMonth = teamKPIPLC},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIVISION", ThirdMonth = teamKPIVISION},
        //            new KPISumarizeDTO(){ EvaluationCode = "TEAMKPISOFTWARE", ThirdMonth = teamKPISOFTWARE},
        //            new KPISumarizeDTO(){ EvaluationCode = "MA11", ThirdMonth = totalErrorTBP},
        //        });


        //        Lib.LockEvents = true;
        //        foreach (KPISumarizeDTO item in lstResult)
        //        {
        //            TreeListNode node = treeDataRule.GetNodeList().FirstOrDefault(x => item.EvaluationCode == TextUtils.ToString(x.GetValue(colEvaluationCode)));
        //            if (node == null) continue;
        //            node.SetValue(colFirstMonth, item.FirstMonth);
        //            node.SetValue(colSecondMonth, item.SecondMonth);
        //            node.SetValue(colThirdMonth, item.ThirdMonth);
        //        }


        //        var kpiSession = (KPISessionModel)cboKPISession.GetSelectedDataRow() ?? new KPISessionModel();
        //        int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
        //        var dtTraining = TextUtils.LoadDataSetFromSP("spGetCourseTraining"
        //                                                    , new string[] { "@Year", "@Quarter", "@EmployeeID" }
        //                                                    , new object[] { kpiSession.YearEvaluation, kpiSession.QuarterEvaluation, employeeID });

        //        //Tính tích cực tham gia training
        //        var nodeThuong2 = treeDataRule.FindNodeByFieldValue(colEvaluationCode.FieldName, "THUONG02");
        //        if (nodeThuong2 != null)
        //        {
        //            var dtTHUONG02 = dtTraining.Tables[1];
        //            nodeThuong2.SetValue(colFirstMonth, dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["FirstMonth"]) : 0);
        //            nodeThuong2.SetValue(colSecondMonth, dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["SecondMonth"]) : 0);
        //            nodeThuong2.SetValue(colThirdMonth, dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["ThirdMonth"]) : 0);
        //        }

        //        //Tính tổ chức training
        //        var nodeThuong3 = treeDataRule.FindNodeByFieldValue(colEvaluationCode.FieldName, "THUONG03");
        //        if (nodeThuong3 != null)
        //        {
        //            var dtTHUONG03 = dtTraining.Tables[0];
        //            nodeThuong3.SetValue(colFirstMonth, dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["FirstMonth"]) : 0);
        //            nodeThuong3.SetValue(colSecondMonth, dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["SecondMonth"]) : 0);
        //            nodeThuong3.SetValue(colThirdMonth, dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["ThirdMonth"]) : 0);
        //        }

        //        //treeDataRule.Refresh();
        //        Lib.LockEvents = false;

        //        CalculatorPoint();
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
        //    }
        //}

        List<string> lstTeamTBP = new List<string>() { "TEAM01", "TEAM02", "TEAM03" };
        private void CalculatorPoint()
        {
            try
            {
                Lib.LockEvents = true;
                CalculatorNoError();
                List<TreeListNode> lst = treeDataRule.GetNodeList();
                for (int i = lst.Count - 1; i >= 0; i--)
                {
                    TreeListNode row = lst[i];
                    if (row == null) continue;
                    string stt = TextUtils.ToString(row["STT"]);
                    //if (stt != "1.3.2") continue;
                    string ruleCode = TextUtils.ToString(row["EvaluationCode"]).ToUpper();
                    bool isDiemThuong = ruleCode == "THUONG";
                    //if (ruleCode != "TEAMKPICHUYENMON") continue;
                    decimal maxPercentBonus = TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["MaxPercent"]), 1);
                    decimal percentageAdjustment = TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["PercentageAdjustment"]), 1);
                    decimal maxPercentageAdjustment = TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["MaxPercentageAdjustment"]), 1);

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
                        else
                        {
                            row["PercentBonus"] = totalPercentBonus;
                            row["PercentRemaining"] = totalPercentRemaining;
                        }

                        if (lstTeamTBP.Contains(ruleCode) /*&& isTBP*/)// Tính % thưởng KPITeam PP
                        {
                            row["PercentBonus"] = TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["ThirdMonth"]), 1) * percentageAdjustment > maxPercentageAdjustment ? maxPercentageAdjustment : TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["ThirdMonth"]), 1) * percentageAdjustment;
                        }
                        else if (maxPercentageAdjustment > 0) row["PercentBonus"] = (maxPercentageAdjustment > totalPercentBonus ? totalPercentBonus : maxPercentageAdjustment);
                        if (percentageAdjustment > 0)
                        {
                            decimal totalPercentDeduction = percentageAdjustment * TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["TotalError"]), 1);
                            row["PercentBonus"] = maxPercentageAdjustment > 0 ? (totalPercentDeduction > maxPercentageAdjustment ? maxPercentageAdjustment : totalPercentDeduction) : totalPercentDeduction;
                        }
                    }
                    else
                    {

                        decimal totalError = (TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["FirstMonth"]), 1) + TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["SecondMonth"]), 1) + TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["ThirdMonth"]), 1));
                        row["TotalError"] = totalError;
                        if (ruleCode == "OT") row["TotalError"] = (totalError / 3) >= 20 ? 1 : 0;

                        decimal totalPercentDeduction = percentageAdjustment * TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["TotalError"]), 1);
                        row["PercentBonus"] = maxPercentageAdjustment > 0 ? (totalPercentDeduction > maxPercentageAdjustment ? maxPercentageAdjustment : totalPercentDeduction) : totalPercentDeduction;


                        if ((ruleCode.StartsWith("KPI") && !(ruleCode == "KPINL" || ruleCode == "KPINQ")))
                        {
                            row["TotalError"] = TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["ThirdMonth"]), 1);
                            row["PercentRemaining"] = TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["TotalError"]), 1) * maxPercentBonus / 5;
                        }
                        else if (ruleCode.StartsWith("TEAMKPI"))
                        {
                            //decimal test = TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["TotalError"]),1) * maxPercentageAdjustment / 5;
                            row["PercentBonus"] = TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["TotalError"]), 1) * maxPercentageAdjustment / 5;
                        }
                        else if (ruleCode == "MA09")
                        {
                            //row["PercentBonus"] = totalPercentDeduction > maxPercentageAdjustment ? maxPercentageAdjustment : maxPercentageAdjustment - totalPercentDeduction;
                            row["PercentBonus"] = totalPercentDeduction > maxPercentageAdjustment ? 0 : maxPercentageAdjustment - totalPercentDeduction;
                        }
                        else
                        {
                            //decimal test = TextUtils.ToDecimal(row["TotalError"]) * maxPercentBonus;
                            row["PercentRemaining"] = TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(row["TotalError"]), 1) * maxPercentBonus;
                        }
                    }
                }

                treeDataRule.RefreshDataSource();
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
        string[] listAdminCodes = new string[] { "AMA01", "AMA02", "AMA03", "AMA04", "AMA05", "AMA06", "AMA07", "AMA08", "AMA09", "AMA10", "AMA11", "AMA12", "AMA13", "AMA14", "AMA15", "AMA16", "AMA17", "AMA18", "AMA19", "WorkLate", "NotWorking" };
        private void CalculatorNoError()
        {
            var list = treeDataRule.GetNodeList().Where(x => listCodes.Contains(x.GetValue(colEvaluationCode)));

            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            if (employeeID == 548) //Nếu là admin
            {
                list = treeDataRule.GetNodeList().Where(x => listAdminCodes.Contains(x.GetValue(colEvaluationCode)));
            }

            decimal firstMonth = list.Sum(x => TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(x.GetValue(colFirstMonth)), 1));
            decimal secondMonth = list.Sum(x => TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(x.GetValue(colSecondMonth)), 1));
            decimal thirdMonth = list.Sum(x => TextUtils.FormatDecimalNumber(TextUtils.ToDecimal(x.GetValue(colThirdMonth)), 1));

            var node = treeDataRule.FindNodeByFieldValue(colEvaluationCode.FieldName, "MA09");
            if (node == null) return;

            node.SetValue(colFirstMonth, firstMonth);
            node.SetValue(colSecondMonth, secondMonth);
            node.SetValue(colThirdMonth, thirdMonth);
        }

        private void treeDataRule_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (Lib.LockEvents) return;
            else
            {
                try
                {
                    Lib.LockEvents = true;

                    CalculatorPoint();
                    //CalculatorTotalPoint();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Thông báo");
                }
                finally
                {
                    Lib.LockEvents = false;
                }
            }
        }
        List<string> lstCodeDisplay = new List<string>() { "KPINQ", "KPINL" };
        private void treeDataRule_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            List<TreeListBand> listBands = new List<TreeListBand>() { /*treeListBand18,*/ treeListBand24 };
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

            if (ruleCode == "NewLine".ToUpper() && e.Column.FieldName != "STT" && !isColumn)
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

            if (isColumn && isTeam)
            {
                e.DisplayText = "";
            }

        }

        private void treeDataRule_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            //return;
            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                return;
            }
            else
            {
                string ruleCode = TextUtils.ToString(e.Node["EvaluationCode"]).ToUpper();
                bool isColumn = e.Column == colFirstMonth || e.Column == colSecondMonth || e.Column == colThirdMonth;

                if (ruleCode == "NewLine".ToUpper() && isColumn) // VTN update 
                {
                    e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }

                if (e.Node.HasChildren || ruleCode == "NewLine".ToUpper()) // VTN update
                {
                    e.Appearance.BackColor = Color.LightGray;
                    return;
                }
                else
                {
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
                        e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#d1e7dd");
                    }
                }


            }
        }


        bool isAdmin = (Global.IsAdmin && Global.EmployeeID <= 0);

        private void treeDataRule_ShowingEditor(object sender, CancelEventArgs e)
        {
            //TreeListColumn focusedColumn = treeDataRule.FocusedColumn;

            //string ruleCode = TextUtils.ToString(treeDataRule.GetFocusedRowCellValue(colEvaluationCode)).ToUpper();
            //bool isColumn = focusedColumn == colFirstMonth || focusedColumn == colSecondMonth || focusedColumn == colThirdMonth;
            //bool isKPI = ruleCode.StartsWith("KPI");
            //bool isNQNL = ruleCode == "KPINL" || ruleCode == "KPINQ" || ruleCode.StartsWith("TEAM"); ;

            //if ((isColumn && isNQNL && focusedColumn != colThirdMonth) || (!isNQNL && focusedColumn == colTotalError) /*|| (isKPI && focusedColumn != colThirdMonth )*/ || treeDataRule.FocusedNode.HasChildren || (isNQNL || isKPI))
            //{
            //    e.Cancel = true;
            //}
            TreeListColumn focusedColumn = treeDataRule.FocusedColumn;
            string ruleCode = TextUtils.ToString(treeDataRule.GetFocusedRowCellValue(colEvaluationCode)).ToUpper();
            bool isColumn = focusedColumn == colFirstMonth || focusedColumn == colSecondMonth || focusedColumn == colThirdMonth;
            bool isKPI = ruleCode.StartsWith("KPI") || ruleCode == "KPINL" || ruleCode == "KPINQ" || ruleCode.StartsWith("TEAM");

            if (!isAdmin && (!isColumn || treeDataRule.FocusedNode.HasChildren || isKPI))
            {
                e.Cancel = true;
            }

            if (ruleCode == "NewLine".ToUpper() && isColumn) e.Cancel = true;
        }
        private void btnLoadDataTeam_Click(object sender, EventArgs e)
        {
            int empID = TextUtils.ToInt(cboEmployee.EditValue);
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
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
                {
                    foreach (EmployeeModel emp in lstTeam)
                    {
                        KPIPositionEmployeeModel position1 = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", emp.ID).FirstOrDefault() ?? new KPIPositionEmployeeModel();
                        int positionID = position1.ID > 0 ? TextUtils.ToInt(position1.KPIPosiotionID) : 1; // 1: kỹ thuật;

                        Expression exFindRule1 = new Expression(KPIEvaluationRuleModel_Enum.KPIPositionID.ToString(), positionID);
                        Expression exFindRule2 = new Expression(KPIEvaluationRuleModel_Enum.KPISessionID.ToString(), kpiSessionID);
                        Expression exFindRule3 = new Expression(KPIEvaluationRuleModel_Enum.IsDeleted.ToString(), 0);
                        KPIEvaluationRuleModel ruleModel = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(exFindRule1.And(exFindRule2).And(exFindRule3)).FirstOrDefault() ?? new KPIEvaluationRuleModel();
                        if (ruleModel.ID <= 0) continue;

                        Expression ex1 = new Expression("EmployeeID", emp.ID);
                        Expression ex2 = new Expression("KPIEvaluationRuleID", ruleModel.ID);
                        Expression ex3 = new Expression("IsDelete", !frm.lstEmpChose.Contains(emp) ? 0 : 1);
                        KPIEmployeePointModel empPoint = SQLHelper<KPIEmployeePointModel>.FindByExpression(ex1.And(ex2).And(ex3)).OrderByDescending(x => x.ID).FirstOrDefault() ?? new KPIEmployeePointModel();
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
                    LoadKPIRule();


                    KPIPositionEmployeeModel position = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", empID).FirstOrDefault() ?? new KPIPositionEmployeeModel();
                    int positionMasterID = position.KPIPosiotionID > 0 ? TextUtils.ToInt(position.KPIPosiotionID) : 1; // 1: kỹ thuật;
                    Expression exFindRuleM1 = new Expression(KPIEvaluationRuleModel_Enum.KPIPositionID.ToString(), positionMasterID);
                    Expression exFindRuleM2 = new Expression(KPIEvaluationRuleModel_Enum.KPISessionID.ToString(), kpiSessionID);
                    Expression exFindRuleM3 = new Expression(KPIEvaluationRuleModel_Enum.IsDeleted.ToString(), 0);
                    KPIEvaluationRuleModel rule = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(exFindRuleM1.And(exFindRuleM2).And(exFindRuleM3)).FirstOrDefault() ?? new KPIEvaluationRuleModel();
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

                foreach (KPISumarizeDTO item in lstResult)
                {
                    DataRow[] rows = dt.Select($"EvaluationCode = '{item.EvaluationCode}'");
                    if (rows.Length <= 0) continue;
                    DataRow row = rows[0];
                    row["FirstMonth"] = item.FirstMonth;
                    row["SecondMonth"] = item.SecondMonth;
                    row["ThirdMonth"] = item.ThirdMonth;
                }

                var kpiSession = (KPISessionModel)cboKPISession.GetSelectedDataRow() ?? new KPISessionModel();
                int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
                var dtTraining = TextUtils.LoadDataSetFromSP("spGetCourseTraining"
                                                            , new string[] { "@Year", "@Quarter", "@EmployeeID" }
                                                            , new object[] { kpiSession.YearEvaluation, kpiSession.QuarterEvaluation, employeeID });

                //Tính tích cực tham gia training
                var nodeThuong2 = treeDataRule.FindNodeByFieldValue(colEvaluationCode.FieldName, "THUONG02");
                if (nodeThuong2 != null)
                {
                    var dtTHUONG02 = dtTraining.Tables[1];
                    nodeThuong2.SetValue(colFirstMonth, dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["FirstMonth"]) : 0);
                    nodeThuong2.SetValue(colSecondMonth, dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["SecondMonth"]) : 0);
                    nodeThuong2.SetValue(colThirdMonth, dtTHUONG02.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG02.Rows[0]["ThirdMonth"]) : 0);
                }

                //Tính tổ chức training
                var nodeThuong3 = treeDataRule.FindNodeByFieldValue(colEvaluationCode.FieldName, "THUONG03");
                if (nodeThuong3 != null)
                {
                    var dtTHUONG03 = dtTraining.Tables[0];
                    nodeThuong3.SetValue(colFirstMonth, dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["FirstMonth"]) : 0);
                    nodeThuong3.SetValue(colSecondMonth, dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["SecondMonth"]) : 0);
                    nodeThuong3.SetValue(colThirdMonth, dtTHUONG03.Rows.Count > 0 ? TextUtils.ToInt(dtTHUONG03.Rows[0]["ThirdMonth"]) : 0);
                }

                dt.AcceptChanges();
                return dt;

            }
            catch (Exception ex)
            {
                //MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return dt;
            }
        }
        //================= 
        private bool SaveDataRule()
        {
            try
            {
                treeDataRule.CloseEditor();
                treeDataRule.FocusedColumn = colEvaluationCode;

                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {

                    KPIEmployeePointModel master = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);
                    SQLHelper<KPIEmployeePointDetailModel>.DeleteByAttribute("KPIEmployeePointID", empPointID);

                    //master.TotalPercent = TextUtils.ToDecimal(treeDataRule.GetSummaryValue(colMaxPercent)); //Khôi gà
                    master.TotalPercent = TextUtils.ToDecimal(treeDataRule.GetSummaryValue(colPercentRemaining));
                    master.Status = 2;
                    SQLHelper<KPIEmployeePointModel>.Update(master);
                    foreach (TreeListNode node in treeDataRule.GetNodeList())
                    {
                        var code = node.GetValue(colEvaluationCode.FieldName);
                        KPIEmployeePointDetailModel detail = new KPIEmployeePointDetailModel();
                        detail.KPIEmployeePointID = empPointID;
                        detail.KPIEvaluationRuleDetailID = TextUtils.ToInt(node["ID"]);
                        detail.FirstMonth = TextUtils.ToDecimal(node["FirstMonth"]);
                        detail.SecondMonth = TextUtils.ToDecimal(node["SecondMonth"]);
                        detail.ThirdMonth = TextUtils.ToDecimal(node["ThirdMonth"]);
                        detail.PercentBonus = TextUtils.ToDecimal(node["PercentBonus"]);
                        detail.PercentRemaining = TextUtils.ToDecimal(node["PercentRemaining"]);
                        SQLHelper<KPIEmployeePointDetailModel>.Insert(detail);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            int selectedTab = xtraTabControl1.SelectedTabPageIndex;
            if (selectedTab < 0) return;

            KPIEmployeePointModel empPoint = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);
            bool isPublic = typePoint == 2 || typePoint == 3 || empPoint.IsPublish == true;
            employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            int empId = employeeID;
            int kpiExamID = TextUtils.ToInt(cboKpiExam.EditValue);

            switch (selectedTab)
            {
                //case 0:
                //    LoadKPIKyNang(empId, kpiExamID, isPublic);
                //    break;

                //case 1:
                //    LoadKPIChung(empId, kpiExamID, isPublic);
                //    break;

                //case 2:
                //    LoadKPIChuyenMon(empId, kpiExamID, isPublic);
                //    break;

                case 3:
                    //LoadTotalAVGNew();
                    //TN.Binh update 01/10/25
                    if (_departmentID == departmentCoKhi) LoadSumaryRank_CKTK();
                    else LoadTotalAVGNew();

                    //end
                    break;

                case 4:
                    LoadKPIRule();
                    break;

                case 5:
                    LoadKPIRule();
                    break;
                default:
                    break;
            }
        }

        private void treeData3_FocusedNodeChanged_1(object sender, FocusedNodeChangedEventArgs e)
        {

        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tải..."))
            {
                LoadPointRuleNew(empPointID);
            }
        }


        #region Công thức tính CƠ KHÍ - THIẾT KẾ
        //=======================================  ====================================================== -- 160525 -- lee min khooi -- update
        private void LoadEventForCKTK()
        {

            xtraTabPage4.PageVisible = xtraTabPage5.PageVisible /*= xtraTabPage2.PageVisible*/ = xtraTabPage3.PageVisible = false;
            treeData.OptionsView.AutoWidth = treeData2.OptionsView.AutoWidth /*= treeData3.OptionsView.AutoWidth*/ = true;

            //xtraTabPage4.PageVisible = xtraTabPage5.PageVisible = xtraTabPage6.PageVisible = false; //LĐ.Dat update 2/10/25
            //treeData.OptionsView.AutoWidth = true;

            treeData.CellValueChanged -= treeData_CellValueChanged;
            treeData.CellValueChanged += treeData_CellValueChanged_CKTK;
            colCoefficient.Visible = colEmployeeCoefficient.Visible = colTBPCoefficient.Visible = colPoint6General.Visible = colBGDCoefficient.Visible = colTBPPoint.Visible = colBGDPoint.Visible = false;
            colStandartPoint.Visible = true;
            colTree2Coefficient.Visible = false;
            //treeData3.CellValueChanged -= treeData_CellValueChanged;
            //treeData3.CellValueChanged += treeData_CellValueChanged_AGV;
            //colGeneralCoefficient.Visible = colGeneralEmployeeCoefficient.Visible = colGeneralTBPCoefficient.Visible = colGeneralBGDCoefficient.Visible = false;


            treeData2.CellValueChanged -= treeData_CellValueChanged;
            treeData2.CellValueChanged += treeData_CellValueChanged_CKTK;
            colTree2Coefficient.Visible = colTree2EmployeeCoefficient.Visible = colTree2TBPCoefficient.Visible = colTree2BGDCoefficient.Visible = false;


            // ============= Khởi tạo lại gridcontrol
            gridBand2.Visible = false;
            gridBand8.Visible = true;
        }


        private void treeData_CellValueChanged_CKTK(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            TreeList treeList = (TreeList)sender;
            if (treeList == null) return;

            DataTable dt = (DataTable)treeList.DataSource;
            treeList.DataSource = CalculatorAvgPoint_CKTK(dt);
            treeList.ExpandAll();
        }

        private DataTable CalculatorAvgPoint_CKTK(DataTable dataTable)
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

                        totaltbpPoint = TextUtils.ToDecimal(row["TBPPointInput"]);
                        //totalbgdPointt = TextUtils.ToDecimal(row["BGDPointInput"]);
                        totalbgdPointt = TextUtils.ToDecimal(row["TBPPointInput"]);
                    }
                    else if (Stt.StartsWith(startStt))
                    {
                        if (isCheck) continue;
                        totalempPoint += TextUtils.ToDecimal(row["EmployeePoint"]);

                        totaltbpPoint += TextUtils.ToDecimal(row["TBPPointInput"]);
                        //totalbgdPointt += TextUtils.ToDecimal(row["BGDPointInput"]);
                        totalbgdPointt += TextUtils.ToDecimal(row["TBPPointInput"]);
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

                //dataTable.Rows[fatherRowIndex]["EmployeeCoefficient"] = (decimal)totalempPoint;
                //dataTable.Rows[fatherRowIndex]["TBPCoefficient"] = (decimal)totaltbpPoint;
                //dataTable.Rows[fatherRowIndex]["BGDCoefficient"] = (decimal)totalbgdPointt;
            }
            dataTable = CalculatorTotalPoint_CKTK(dataTable);
            return dataTable;
        }

        private DataTable CalculatorTotalPoint_CKTK(DataTable dataTable)
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

        private void LoadSumaryRank_CKTK()
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
                DataTable dtChuyenMon = (DataTable)treeData2.DataSource;

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
                List<object> data = new List<object>()
                {
                    new
                    {
                        EvaluatedType = "Tự đánh giá",
                        SkillPoint = totalEmpSkillPoint,
                        //PercentageAchieved = Math.Round((totalEmpSkillPoint / totalSkillPoint) * 100, 2),
                        PercentageAchieved = Math.Round(((totalEmpSkillPoint + totalEmpCMPoint) / (totalSkillPoint + totalCMPoint)) * 100, 2),
                        //EvaluationRank = GetEvaluationRank_CKTK(Math.Round((totalEmpSkillPoint / totalSkillPoint) * 100, 2)),
                         EvaluationRank = GetEvaluationRank_CKTK(Math.Round((totalEmpSkillPoint + totalEmpCMPoint) / (totalSkillPoint + totalCMPoint) * 100, 2)), 
                        //StandartPoint = totalSkillPoint,
                        SpecializationPoint = totalEmpCMPoint,
                        StandartPoint = totalSkillPoint + totalCMPoint, // TN.Binh update 02/10/25
                    },
                    new
                    {
                        EvaluatedType = "Đánh giá của Trưởng/Phó BP",
                        SkillPoint = totalTBPSkillPoint ,
                        //PercentageAchieved = Math.Round((totalTBPSkillPoint / totalSkillPoint) * 100, 2),
                        PercentageAchieved = Math.Round(( (totalTBPSkillPoint+totalTBPCMPoint) / (totalSkillPoint+totalCMPoint)) * 100, 2),
                        //EvaluationRank = GetEvaluationRank_CKTK(Math.Round((totalTBPSkillPoint / totalSkillPoint) * 100, 2)),
                        EvaluationRank = GetEvaluationRank_CKTK(Math.Round((totalTBPSkillPoint + totalTBPCMPoint) / (totalSkillPoint + totalCMPoint) * 100, 2)), // TN.Binh update
                        //StandartPoint = totalSkillPoint,
                        SpecializationPoint = totalTBPCMPoint,
                        StandartPoint = totalSkillPoint + totalCMPoint, // TN.Binh update 02/10/25
                    },
                     new
                    {
                        EvaluatedType = "Đánh giá của GĐ",
                        SkillPoint = totalBGDSkillPoint ,
                        //PercentageAchieved = Math.Round((totalBGDSkillPoint / totalSkillPoint) * 100, 2),
                        PercentageAchieved = Math.Round(((totalBGDSkillPoint+totalBGDCMPoint) / (totalSkillPoint+totalCMPoint)) * 100, 2),
                        //EvaluationRank = GetEvaluationRank_CKTK(Math.Round((totalBGDSkillPoint / totalSkillPoint) * 100, 2)),
                        EvaluationRank = GetEvaluationRank_CKTK(Math.Round((totalBGDSkillPoint + totalBGDCMPoint) / (totalSkillPoint + totalCMPoint) * 100, 2)), // TN.Binh update
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


        private string GetEvaluationRank_CKTK(decimal totalPercent)
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



        #region
        //TN.Binh update 08/09/25
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

        //TN.Binh update 08/09/25
        private DataTable CalculatorTotalPointNew(DataTable dataTable)
        {
            try
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
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion
        #region
        private void LoadTotalAVGNew()
        {
            try
            {
                grdMaster.DataSource = null;

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

                #region tính điểm chuyên môn cũ
                DataTable dt = (DataTable)treeData2.DataSource;
                List<DataRow> lstPoint = dt.Select("ParentID = 0").ToList();

                decimal totalEmpPLCPoint = 0;
                decimal totalTBPPLCPoint = 0;
                decimal totalBGDPLCPoint = 0;
                int countPLC = 0;

                decimal totalEmpPVisionPoint = 0;
                decimal totalTBPVisionPoint = 0;
                decimal totalBGDPVisionPoint = 0;
                int countVision = 0;

                decimal totalEmpSoftPoint = 0;
                decimal totalTBPSoftPoint = 0;
                decimal totalBGDSoftPoint = 0;
                int countSoft = 0;


                decimal totalEmpViRobotPoint = 0;
                decimal totalTBPViRobotPoint = 0;
                decimal totalBGDViRobotPoint = 0;
                int countViRobot = 0;
                foreach (DataRow item in lstPoint)
                {
                    if (TextUtils.ToInt(item["SpecializationType"]) == 2)
                    {
                        //totalEmpPLCPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
                        //totalTBPPLCPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
                        //totalBGDPLCPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);

                        totalEmpPLCPoint += TextUtils.ToDecimal(item["EmployeeEvaluation"]);
                        totalTBPPLCPoint += TextUtils.ToDecimal(item["TBPEvaluation"]);
                        totalBGDPLCPoint += TextUtils.ToDecimal(item["BGDEvaluation"]);
                        countPLC++;
                    }
                    else if (TextUtils.ToInt(item["SpecializationType"]) == 3)
                    {
                        totalEmpPVisionPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
                        totalTBPVisionPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
                        totalBGDPVisionPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
                        countVision++;
                    }
                    else if (TextUtils.ToInt(item["SpecializationType"]) == 4)
                    {
                        totalEmpSoftPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
                        totalTBPSoftPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
                        totalBGDSoftPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
                        countSoft++;
                    }
                    else if (TextUtils.ToInt(item["SpecializationType"]) == 5)
                    {
                        totalEmpViRobotPoint += TextUtils.ToDecimal(item["EmployeeCoefficient"]);
                        totalTBPViRobotPoint += TextUtils.ToDecimal(item["TBPCoefficient"]);
                        totalBGDViRobotPoint += TextUtils.ToDecimal(item["BGDCoefficient"]);
                        countViRobot++;
                    }
                    else continue;
                }
                countPLC = countPLC > 0 ? countPLC : 1;
                countVision = countVision > 0 ? countVision : 1;
                countSoft = countSoft > 0 ? countSoft : 1;
                countViRobot = countViRobot > 0 ? countViRobot : 1;
                #endregion cũ

                #region tính điểm chuyên môn mới
                // Phần chuyên môn (thay đổi: lấy parent row ID = -1 giống kỹ năng, bỏ phân loại SpecializationType)
                DataTable dtSpecialization = (DataTable)treeData2.DataSource;
                List<DataRow> lstSpecializationPoint = dtSpecialization.Select("ID = -1").ToList();
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

                DataTable dtGeneral = (DataTable)treeData3.DataSource;
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


                decimal plcEmpPoint = (2 * totalEmpPLCPoint / countPLC + totalEmpViRobotPoint / countViRobot) / 3;
                decimal visionEmpPoint = (2 * totalEmpPVisionPoint / countVision + totalEmpViRobotPoint / countViRobot) / 3;

                decimal plcTBPPoint = (2 * totalTBPPLCPoint / countPLC + totalTBPViRobotPoint / countViRobot) / 3;
                decimal visionTBPPoint = (2 * totalTBPVisionPoint / countVision + totalTBPViRobotPoint / countViRobot) / 3;


                decimal plcBGDPoint = (2 * totalBGDPLCPoint / countPLC + totalBGDViRobotPoint / countViRobot) / 3;
                decimal visionBGDPoint = (2 * totalBGDPVisionPoint / countVision + totalBGDViRobotPoint / countViRobot) / 3;
                List<object> data = new List<object>()
                {
                    new
                    {
                        EvaluatedType = "Tự đánh giá",
                        SkillPoint = totalEmpSkillPoint / countSkillPoint,
                         //TN.Binh update 10/09/25
                        SpecializationPoint = totalEmpSpecializationPoint / countSpecializationPoint, // Thay mới
                        PLCPoint = plcEmpPoint,
                        VisionPoint = visionEmpPoint,
                        SoftWarePoint = totalEmpSoftPoint / countSoft,
                        AVGPoint = ( plcEmpPoint + visionEmpPoint + (totalEmpSoftPoint / countSoft)) /3,
                        GeneralPoint = totalEmpGeneralPoint / countGeneralPoint
                    },
                    new
                    {
                        EvaluatedType = "Đánh giá của Trưởng/Phó BP",
                        SkillPoint = totalTBPSkillPoint / countSkillPoint,
                        //TN.Binh update 10/09/25
                        SpecializationPoint = totalTBPSpecializationPoint / countSpecializationPoint, // Thay mới
                        PLCPoint = plcTBPPoint,
                        VisionPoint = visionTBPPoint,
                        SoftWarePoint = totalTBPSoftPoint / countSoft,
                        AVGPoint = ( plcTBPPoint + visionTBPPoint + (totalTBPSoftPoint / countSoft)) / 3,
                        GeneralPoint = totalTBPGeneralPoint / countGeneralPoint
                    },
                     new
                    {
                        EvaluatedType = "Đánh giá của GĐ",
                        SkillPoint = totalBGDSkillPoint / countSkillPoint,
                         //TN.Binh update 10/09/25
                        SpecializationPoint = totalBGDSpecializationPoint / countSpecializationPoint, // Thay mới
                        PLCPoint = plcBGDPoint,
                        VisionPoint = visionBGDPoint,
                        SoftWarePoint = totalBGDSoftPoint / countSoft,
                        AVGPoint = (plcBGDPoint + visionBGDPoint + (totalBGDSoftPoint / countSoft)) / 3,
                        GeneralPoint = totalBGDGeneralPoint / countGeneralPoint
                    },

                };
                grdMaster.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }


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
                decimal customerComplaint = TextUtils.ToDecimal(grvTeam.Columns[colComplaneAndMissing.FieldName].SummaryItem.SummaryValue);
                decimal deadlineDelay = TextUtils.ToDecimal(grvTeam.Columns["DeadlineDelay"].SummaryItem.SummaryValue);
                decimal teamKPIKyNang = TextUtils.ToDecimal(grvTeam.Columns["KPIKyNang"].SummaryItem.SummaryValue);
                decimal teanKPIChung = TextUtils.ToDecimal(grvTeam.Columns["KPIChung"].SummaryItem.SummaryValue);
                decimal teamKPIPLC = TextUtils.ToDecimal(grvTeam.Columns["KPIPLC"].SummaryItem.SummaryValue);
                decimal teamKPIVISION = TextUtils.ToDecimal(grvTeam.Columns["KPIVision"].SummaryItem.SummaryValue);
                decimal teamKPISOFTWARE = TextUtils.ToDecimal(grvTeam.Columns["KPISoftware"].SummaryItem.SummaryValue);
                decimal missingTool = TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);
                decimal teamKPIChuyenMon = TextUtils.ToDecimal(grvTeam.Columns[colKPIChuyenMon.FieldName].SummaryItem.SummaryValue);


                List<string> lstCodeTBP = new List<string>() { "MA03", "MA04", "NotWorking", "WorkLate" };
                var ltsMA11 = lstResult.Where(p => lstCodeTBP.Contains(p.EvaluationCode.Trim())).ToList();
                decimal totalErrorTBP = ltsMA11.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);

                lstResult.AddRange(new List<KPISumarizeDTO>
                {
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM01", ThirdMonth = timeWork},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM02", ThirdMonth = fiveS},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM03", ThirdMonth = reportWork},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM04", ThirdMonth = customerComplaint + missingTool + deadlineDelay},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM05", ThirdMonth = customerComplaint},
                    //new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = missingTool},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = deadlineDelay},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIKYNANG", ThirdMonth = teamKPIKyNang},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIChung", ThirdMonth = teanKPIChung},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIPLC", ThirdMonth = teamKPIPLC},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIVISION", ThirdMonth = teamKPIVISION},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPISOFTWARE", ThirdMonth = teamKPISOFTWARE},
                    new KPISumarizeDTO(){ EvaluationCode = "MA11", ThirdMonth = totalErrorTBP},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPICHUYENMON", ThirdMonth = teamKPIChuyenMon},
                });


                Lib.LockEvents = true;
                foreach (KPISumarizeDTO item in lstResult)
                {
                    TreeListNode node = treeDataRule.GetNodeList().FirstOrDefault(x => item.EvaluationCode == TextUtils.ToString(x.GetValue(colEvaluationCode)));
                    if (node == null) continue;
                    node.SetValue(colFirstMonth, item.FirstMonth);
                    node.SetValue(colSecondMonth, item.SecondMonth);
                    node.SetValue(colThirdMonth, item.ThirdMonth);
                }

                CalculatorPoint();

                Lib.LockEvents = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        #endregion

        private void btnUpdateDataRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeDataRule.FocusedNode == null)
                {
                    MessageBox.Show("Vui lòng chọn một dòng để cập nhật!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang cập nhật dữ liệu..."))
                {


                    TreeListNode selectedNode = treeDataRule.FocusedNode;

                    string code = TextUtils.ToString(selectedNode.GetValue(colEvaluationCode));

                    int kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue);
                    int empID = TextUtils.ToInt(cboEmployee.EditValue);

                    //positionEmp = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", empID).FirstOrDefault() ?? new KPIPositionEmployeeModel();

                    var expPoint1 = new Expression(KPIPositionEmployeeModel_Enum.EmployeeID, empID);
                    var expPoint2 = new Expression(KPIPositionModel_Enum.KPISessionID, kpiSessionID);
                    var expPoint3 = new Expression(KPIPositionEmployeeModel_Enum.IsDeleted, 0);

                    var kpiPositions = SQLHelper<KPIPositionModel>.FindByExpression(expPoint2.And(expPoint3));
                    var kpiPositionEmployees = SQLHelper<KPIPositionEmployeeModel>.FindByExpression(expPoint1.And(expPoint3));

                    positionEmp = (from p in kpiPositions
                                   join pe in kpiPositionEmployees on p.ID equals pe.KPIPosiotionID
                                   select pe)
                            .FirstOrDefault() ?? new KPIPositionEmployeeModel();

                    Expression ex1 = new Expression("KPISessionID", kpiSessionID);
                    //Expression ex2 = new Expression("KPIPositionID", TextUtils.ToInt(positionEmp.KPIPosiotionID) > 0 ? TextUtils.ToInt(positionEmp.KPIPosiotionID) : 1);
                    Expression ex2 = new Expression("KPIPositionID", TextUtils.ToInt(positionEmp.KPIPosiotionID));
                    Expression ex3 = new Expression("IsDeleted", 0);
                    KPIEvaluationRuleModel kpiRule = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPIEvaluationRuleModel();

                    int empPointID = GetKPIEmployeePointID(kpiRule.ID);
                    List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList(
                                                                            "spGetSumarizebyKPIEmpPointIDNew",
                                                                            new string[] { "@KPIEmployeePointID" },
                                                                            new object[] { empPointID });

                    // Tính toán lại các tổng 
                    decimal timeWork = TextUtils.ToDecimal(grvTeam.Columns["TimeWork"].SummaryItem.SummaryValue);
                    decimal fiveS = TextUtils.ToDecimal(grvTeam.Columns["FiveS"].SummaryItem.SummaryValue);
                    decimal reportWork = TextUtils.ToDecimal(grvTeam.Columns["ReportWork"].SummaryItem.SummaryValue);
                    decimal customerComplaint = TextUtils.ToDecimal(grvTeam.Columns["ComplaneAndMissing"].SummaryItem.SummaryValue);
                    decimal deadlineDelay = TextUtils.ToDecimal(grvTeam.Columns["DeadlineDelay"].SummaryItem.SummaryValue);
                    decimal teamKPIKyNang = TextUtils.ToDecimal(grvTeam.Columns["KPIKyNang"].SummaryItem.SummaryValue);
                    decimal teanKPIChung = TextUtils.ToDecimal(grvTeam.Columns["KPIChung"].SummaryItem.SummaryValue);
                    decimal teamKPIPLC = TextUtils.ToDecimal(grvTeam.Columns["KPIPLC"].SummaryItem.SummaryValue);
                    decimal teamKPIVISION = TextUtils.ToDecimal(grvTeam.Columns["KPIVision"].SummaryItem.SummaryValue);
                    decimal teamKPISOFTWARE = TextUtils.ToDecimal(grvTeam.Columns["KPISoftware"].SummaryItem.SummaryValue);
                    decimal missingTool = TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);
                    decimal teamKPIChuyenMon = TextUtils.ToDecimal(grvTeam.Columns[colKPIChuyenMon.FieldName].SummaryItem.SummaryValue);

                    List<string> lstCodeTBP = new List<string>() { "MA03", "MA04", "NotWorking", "WorkLate" };
                    var ltsMA11 = lstResult.Where(p => lstCodeTBP.Contains(p.EvaluationCode.Trim())).ToList();
                    decimal totalErrorTBP = ltsMA11.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);

                    //logic giống hàm loa tổng
                    lstResult.AddRange(new List<KPISumarizeDTO>
                {
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM01", ThirdMonth = timeWork},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM02", ThirdMonth = fiveS},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM03", ThirdMonth = reportWork},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM04", ThirdMonth = customerComplaint + deadlineDelay},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM05", ThirdMonth = customerComplaint},
                    //new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = missingTool},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = deadlineDelay},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIKYNANG", ThirdMonth = teamKPIKyNang},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIChung", ThirdMonth = teanKPIChung},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIPLC", ThirdMonth = teamKPIPLC},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIVISION", ThirdMonth = teamKPIVISION},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPISOFTWARE", ThirdMonth = teamKPISOFTWARE},
                    new KPISumarizeDTO(){ EvaluationCode = "MA11", ThirdMonth = totalErrorTBP},
                    new KPISumarizeDTO(){ EvaluationCode = "TEAMKPICHUYENMON", ThirdMonth = teamKPIChuyenMon},
                });


                    var updateItems = lstResult.Where(x => x.EvaluationCode.Trim() == code.Trim()).ToList();

                    foreach (var item in updateItems)
                    {
                        //Lib.LockEvents = true;
                        selectedNode.SetValue(colFirstMonth, item.FirstMonth);
                        selectedNode.SetValue(colSecondMonth, item.SecondMonth);
                        selectedNode.SetValue(colThirdMonth, item.ThirdMonth);
                        //Lib.LockEvents = false;
                    }

                    CalculatorPoint();

                    KPIEmployeePointModel empPoint = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);
                    bool isPublic = typePoint == 2 || typePoint == 3 || empPoint.IsPublish == true;
                    int empId = TextUtils.ToInt(cboEmployee.EditValue);
                    int kpiExamID = TextUtils.ToInt(cboKpiExam.EditValue);

                    LoadKPIChuyenMon(empId, kpiExamID, isPublic);

                    //if (updateItem != null)
                    //{
                    //    Lib.LockEvents = true;
                    //    selectedNode.SetValue(colFirstMonth, updateItem.FirstMonth);
                    //    selectedNode.SetValue(colSecondMonth, updateItem.SecondMonth);
                    //    selectedNode.SetValue(colThirdMonth, updateItem.ThirdMonth);
                    //    Lib.LockEvents = false;

                    //    CalculatorPoint();

                    //    KPIEmployeePointModel empPoint = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);
                    //    bool isPublic = typePoint == 2 || typePoint == 3 || empPoint.IsPublish == true;
                    //    int empId = TextUtils.ToInt(cboEmployee.EditValue);
                    //    int kpiExamID = TextUtils.ToInt(cboKpiExam.EditValue);

                    //    LoadKPIChuyenMon(empId, kpiExamID, isPublic);

                    //    //MessageBox.Show($"Đã cập nhật mã lỗi '{code}' thành công!",
                    //    //                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //{
                    //    MessageBox.Show($"Không tìm thấy dữ liệu cho mã: {code}",
                    //                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"btnUpdateDataRow_Click\r\n{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void treeDataRule_GetCustomSummaryValue(object sender, GetCustomSummaryValueEventArgs e)
        {
            // === PHẦN MỚI: Footer text "Tổng điểm " cho cột colRuleContent ===
            if (e.IsSummaryFooter && e.Column == colRuleContent)
            {
                e.CustomValue = "Tổng điểm ";
                return; // Không cần xử lý thêm cho cột này
            }
            if (e.IsSummaryFooter && e.Column == colPercentBonus)
            {
                decimal totalPercent = TextUtils.ToDecimal(treeDataRule.GetSummaryValue(colPercentRemaining));
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
            }            //TN.Binh update 13/10/2025

            // Lấy toàn bộ node trong TreeList
            List<TreeListNode> lst = treeDataRule.GetNodeList();
            if (lst == null || lst.Count == 0) return;

            bool IsKPI(TreeListNode r)
            {
                string ruleCode = TextUtils.ToString(r["EvaluationCode"]).ToUpper();
                return ruleCode.StartsWith("KPI") && ruleCode != "KPINL" && ruleCode != "KPINQ";
            }
            decimal percentRemaining = Math.Round(TextUtils.ToDecimal(treeDataRule.GetSummaryValue(colPercentRemaining)), 2);

            decimal emp = 0;
            decimal tbp = 0;
            decimal bgd = 0;

            foreach (var r in lst.Where(r => r.Nodes.Count == 0 && IsKPI(r)))
            {
                decimal maxPercent = TextUtils.ToDecimal(r["MaxPercent"]);
                decimal firstMonth = Math.Round(TextUtils.ToDecimal(r["FirstMonth"]), 2);
                decimal secondMonth = Math.Round(TextUtils.ToDecimal(r["SecondMonth"]), 2);
                //decimal thirdMonth = Math.Round(TextUtils.ToDecimal(r["ThirdMonth"]), 1);
                decimal thirdMonth = Math.Round(TextUtils.ToDecimal(r["ThirdMonth"]), 2);
                emp += Math.Round((firstMonth * maxPercent / 5), 2);
                tbp += Math.Round((secondMonth * maxPercent / 5), 2);
                bgd += Math.Round((thirdMonth * maxPercent / 5), 2);

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
        private void treeDataRule_CustomDrawFooterCell(object sender, CustomDrawFooterCellEventArgs e)
        {
            // Chỉ áp dụng cho footer của cột colRuleContent
            if (e.Column == colRuleContent)
            {
                // Tô đậm chữ "Tổng điểm "
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
            int kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue);
            int empID = TextUtils.ToInt(cboEmployee.EditValue);

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
    }
}
