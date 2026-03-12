using BaseBusiness.DTO;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
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
    public partial class frmKPISumarizeRuleDetails : _Forms
    {
        public KPIEmployeePointModel kpiEmpPoint = new KPIEmployeePointModel();
        public int kpiSessionID = 0;
        public int kpiRuleID = 0;
        public int empID = 0;
        public frmKPISumarizeRuleDetails()
        {
            InitializeComponent();
        }

        private void frmKPISumarizeRuleDetails_Load(object sender, EventArgs e)
        {
            KPIEvaluationRuleModel modelCheckRule = SQLHelper<KPIEvaluationRuleModel>.FindByID(kpiRuleID);
            bool isActiveTeam = modelCheckRule.KPIPositionID == 5 || modelCheckRule.KPIPositionID == 4; // 1: KT, 2:Admin, 3:Pro; 4:Sen; 5:PP
            xtraTabPage2.PageVisible = isActiveTeam;

            LoadKPISession();
            CalculatorTotalPoint();

            treeData.ContextMenuStrip = contextMenuStrip1; //TN.Binh update 
        }

        private void LoadKPISession()
        {
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(p => p.ID).ToList();

            cboKPISession.Properties.DataSource = lst;
            cboKPISession.Properties.DisplayMember = "Code";
            cboKPISession.Properties.ValueMember = "ID";
            cboKPISession.EditValue = kpiSessionID;
            LoadKpiRule();
        }
        private void cboKPISession_EditValueChanged(object sender, EventArgs e)
        {
            kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue);
        }
        private void LoadKpiRule()
        {
            int kpiSessionId = TextUtils.ToInt(cboKPISession.EditValue);
            Expression ex1 = new Expression("KPISessionID", kpiSessionId);
            Expression ex2 = new Expression("IsDeleted", 0);
            List<KPIEvaluationRuleModel> lst = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2));
            cboKPIRule.Properties.DataSource = lst;
            cboKPIRule.Properties.DisplayMember = "RuleCode";
            cboKPIRule.Properties.ValueMember = "ID";
            cboKPIRule.EditValue = kpiRuleID;
            //LoadEmployee();

        }
        private void cboKPIRule_EditValueChanged(object sender, EventArgs e)
        {
            kpiRuleID = TextUtils.ToInt(cboKPIRule.EditValue);
            LoadEmployee();
        }

        private void LoadEvaluationRuleDetails(int empPointID)
        {
            //DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointID", "LMKTable", new string[] { "@KPIEmployeePointID" }, 
            //    new object[] { empPointID });

            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeRulePointByKPIEmpPointIDNew", "spGetEmployeeRulePointByKPIEmpPointIDNew",
                                                new string[] { "@KPIEmployeePointID" },
                                                new object[] { empPointID });
            treeData.DataSource = dt;
            treeData.ExpandAll();
            CalculatorTotalPoint();

            //SaveData();
        }

        private void LoadDataEmployeeTeam()
        {
            int empPointID = GetKPIEmployeePointID();
            //DataTable dt = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeam", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
            DataTable dt = TextUtils.LoadDataFromSP("spGetKpiRuleSumarizeTeamNew", "LMKTable", new string[] { "@KPIEmployeePointID" }, new object[] { empPointID });
            grdTeam.DataSource = dt;
            LoadEvaluationRuleDetails(empPointID);
            List<KPIEmployeePointDetailModel> lst = SQLHelper<KPIEmployeePointDetailModel>.FindByAttribute("KPIEmployeePointID", empPointID);
            if (lst.Count <= 0)
            {
                btnLoadData_Click(null, null);
            }
            CalculatorPoint();

            SaveData();
        }

        private void LoadEmployee()
        {
            int kpiRuleID = TextUtils.ToInt(cboKPIRule.EditValue);
            DataTable list = TextUtils.LoadDataFromSP("spGetKPIEmployeePoint", "A", new string[] { "@KPIRuleID" }, new object[] { kpiRuleID });
            cboEmployee.Properties.DataSource = list;
            cboEmployee.Properties.ValueMember = "EmployeeID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.EditValue = empID;
            //LoadDataEmployeeTeam();
        }
        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataEmployeeTeam();
        }
        private int GetKPIEmployeePointID()
        {
            int empsID = TextUtils.ToInt(cboEmployee.EditValue);
            string empName = TextUtils.ToString(cboEmployee.Text);
            int ruleID = TextUtils.ToInt(cboKPIRule.EditValue);
            if (empsID <= 0)
            {
                MessageBox.Show($"Không tìm thấy ID của nhân viên [{empName}]", "Thông báo");
                return -1;
            }
            if (ruleID <= 0)
            {
                MessageBox.Show($"Không tìm thấy ID của rule đánh giá! Vui lòng kiểm tra lại", "Thông báo");
                return -1;
            }
            Expression ex1 = new Expression("EmployeeID", empsID);
            Expression ex2 = new Expression("KPIEvaluationRuleID", ruleID);
            Expression ex3 = new Expression("IsDelete", 0);
            KPIEmployeePointModel model = SQLHelper<KPIEmployeePointModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPIEmployeePointModel();
            model.EmployeeID = empsID;
            model.KPIEvaluationRuleID = ruleID;
            model.Status = 1;
            return model.ID > 0 ? model.ID : SQLHelper<KPIEmployeePointModel>.Insert(model).ID;
        }

        private void treeData_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
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
        List<string> lstCodeDisplay = new List<string>() { "KPINQ", "KPINL" };
        private void treeData_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            //bool isStyle = e.Column == colMaxPercent || e.Column == colPercentageAdjustment || e.Column == colMaxPercentageAdjustment || e.Column == colFirstMonth || e.Column == colSecondMonth ||
            //               e.Column == colThirdMonth || e.Column == colTotalError || e.Column == colPercentBonus || e.Column == colPercentRemaining;
            //if (isStyle)
            //{
            //    if (TextUtils.ToDecimal(e.Value) == 0)
            //    {
            //        e.DisplayText = "";
            //    }
            //}
            //List<TreeListBand> listBands = new List<TreeListBand>() {/* treeListBand2,*/ treeListBand3 };
            //if (!listBands.Contains(e.Column.ParentBand)) return;
            //decimal value = TextUtils.ToDecimal(e.Value);
            //if (value == 0) e.DisplayText = "";


            List<TreeListBand> listBands = new List<TreeListBand>() { /*treeListBand2,*/ treeListBand3 };
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
                if ((treeListBand2 == e.Column.ParentBand || e.Column == colPercentBonus) && childs.Count > 0 && maxPercent <= 0)
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

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
            {
                int empPointID = GetKPIEmployeePointID();
                //List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointID",
                //                                                           new string[] { "@KPIEmployeePointID" },
                //                                                           new object[] { empPointID });

                List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetSumarizebyKPIEmpPointIDNew",
                                                                       new string[] { "@KPIEmployeePointID" },
                                                                       new object[] { empPointID });

                decimal timeWork = TextUtils.ToDecimal(grvTeam.Columns["TimeWork"].SummaryItem.SummaryValue);
                decimal fiveS = TextUtils.ToDecimal(grvTeam.Columns["FiveS"].SummaryItem.SummaryValue);
                decimal reportWork = TextUtils.ToDecimal(grvTeam.Columns["ReportWork"].SummaryItem.SummaryValue);
                decimal customerComplaint = TextUtils.ToDecimal(grvTeam.Columns["CustomerComplaint"].SummaryItem.SummaryValue);
                decimal deadlineDelay = TextUtils.ToDecimal(grvTeam.Columns["DeadlineDelay"].SummaryItem.SummaryValue);
                decimal teamKPIKyNang = TextUtils.ToDecimal(grvTeam.Columns["KPIKyNang"].SummaryItem.SummaryValue);
                decimal teanKPIChung = TextUtils.ToDecimal(grvTeam.Columns["KPIChung"].SummaryItem.SummaryValue);
                decimal teamKPIPLC = TextUtils.ToDecimal(grvTeam.Columns["KPIPLC"].SummaryItem.SummaryValue);
                decimal teamKPIVISION = TextUtils.ToDecimal(grvTeam.Columns["KPIVision"].SummaryItem.SummaryValue);
                decimal teamKPISOFTWARE = TextUtils.ToDecimal(grvTeam.Columns["KPISoftware"].SummaryItem.SummaryValue);
                decimal missingTool = TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);  //làm mất mát hỏng thiết bị 12/12/2024
                //================================== update 13/12/2024 ================================== 
                List<string> lstCodeTBP = new List<string>() { "MA03", "MA04", "NotWorking", "WorkLate" }; // MA011 Tổng số liệu thời gian đi làm ko đúng giờ + đi làm ko đủ công + L4 + L5
                var ltsMA11 = lstResult.Where(p => lstCodeTBP.Contains(p.EvaluationCode.Trim())).ToList();
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
                    TreeListNode node = treeData.GetNodeList().FirstOrDefault(x => item.EvaluationCode == TextUtils.ToString(x.GetValue(colEvaluationCode)));
                    if (node == null) continue;
                    node.SetValue(colFirstMonth, item.FirstMonth);
                    node.SetValue(colSecondMonth, item.SecondMonth);
                    node.SetValue(colThirdMonth, item.ThirdMonth);
                }

                Lib.LockEvents = false;

                CalculatorPoint();
                CalculatorTotalPoint();
            }
            //SaveData();
        }

        List<string> lstTeamTBP = new List<string>() { "TEAM01", "TEAM02", "TEAM03" }; //19/12/2024
        private void CalculatorPoint()
        {

            try
            {
                //================ update 12/12/2024 ====================================
                int empPointID = GetKPIEmployeePointID();
                KPIEmployeePointModel empPointModel = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);
                KPIEvaluationRuleModel ruleModel = SQLHelper<KPIEvaluationRuleModel>.FindByID(TextUtils.ToInt(empPointModel.KPIEvaluationRuleID));
                bool isTBP = ruleModel.KPIPositionID == 5; // TBP
                //======================================================================
                Lib.LockEvents = true;
                CalculatorNoError();
                List<TreeListNode> lst = treeData.GetNodeList();
                for (int i = lst.Count - 1; i >= 0; i--)
                {
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
                            totalPercentBonus += TextUtils.ToDecimal(childrenNode["PercentBonus"]);
                            totalPercentRemaining += TextUtils.ToDecimal(childrenNode["PercentRemaining"]);
                            total += TextUtils.ToDecimal(childrenNode[colTotalError.FieldName]);
                        }

                        row["PercentBonus"] = totalPercentBonus;
                        row["TotalError"] = total;

                        if (lstTeamTBP.Contains(ruleCode) && isTBP) //Update 13/12/2024  Tính trực tiếp node cha bên PP
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
                }

                treeData.RefreshDataSource();
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


        private void treeData_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {

            if (Lib.LockEvents) return;
            else
            {
                try
                {
                    Lib.LockEvents = true;

                    CalculatorPoint();
                    CalculatorTotalPoint();
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

        private void treeData_ShowingEditor(object sender, CancelEventArgs e)
        {
            //TreeListColumn focusedColumn = treeData.FocusedColumn;


            //string ruleCode = TextUtils.ToString(treeData.GetFocusedRowCellValue(colEvaluationCode)).ToUpper();
            //bool isColumn = focusedColumn == colFirstMonth || focusedColumn == colSecondMonth || focusedColumn == colThirdMonth;
            //bool isKPI = ruleCode.StartsWith("KPI");
            //bool isNQNL = ruleCode == "KPINL" || ruleCode == "KPINQ" || ruleCode.StartsWith("TEAM"); ;

            //if ((isColumn && isNQNL && focusedColumn != colThirdMonth) || (!isNQNL && focusedColumn == colTotalError) || (isKPI && focusedColumn != colThirdMonth) || treeData.FocusedNode.HasChildren)
            //{
            //    e.Cancel = true;
            //}
            TreeListColumn focusedColumn = treeData.FocusedColumn;
            string ruleCode = TextUtils.ToString(treeData.GetFocusedRowCellValue(colEvaluationCode)).ToUpper();
            bool isColumn = focusedColumn == colFirstMonth || focusedColumn == colSecondMonth || focusedColumn == colThirdMonth;
            bool isKPI = ruleCode.StartsWith("KPI") || ruleCode == "KPINL" || ruleCode == "KPINQ" || ruleCode.StartsWith("TEAM");

            if (!isColumn || treeData.FocusedNode.HasChildren || isKPI)
            {
                e.Cancel = true;
            }
        }
        private bool SaveData()
        {
            treeData.CloseEditor();
            treeData.FocusedColumn = colEvaluationCode;
            int empPointID = GetKPIEmployeePointID();
            KPIEmployeePointModel master = SQLHelper<KPIEmployeePointModel>.FindByID(empPointID);
            SQLHelper<KPIEmployeePointDetailModel>.DeleteByAttribute("KPIEmployeePointID", empPointID);

            //master.Status = 1;
            decimal totalPercent = TextUtils.ToDecimal(treeData.GetSummaryValue(colPercentRemaining));
            //master.TotalPercent = TextUtils.ToDecimal(txtTotalPercent.Text);
            master.TotalPercent = totalPercent;
            master.Status = 2;
            SQLHelper<KPIEmployeePointModel>.Update(master);
            foreach (TreeListNode node in treeData.GetNodeList())
            {
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                LoadDataEmployeeTeam();
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }


        private void CalculatorTotalPoint()
        {
            decimal totalPercent = TextUtils.ToDecimal(treeData.GetSummaryValue(colPercentRemaining));
            txtTotalPercent.Text = totalPercent.ToString("n1");

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
        private class KPIType
        {
            public string TypeName { get; set; }
            public decimal TotalPercent { get; set; }
        }

        //=============== ltanh 16/11/2024 ======================

        string[] listCodes = new string[] { "MA01", "MA02", "MA03", "MA04", "MA05", "MA06", "MA07", "WorkLate", "NotWorking" };
        private void CalculatorNoError()
        {
            var list = treeData.GetNodeList().Where(x => listCodes.Contains(x.GetValue(colEvaluationCode)));

            decimal firstMonth = list.Sum(x => TextUtils.ToDecimal(x.GetValue(colFirstMonth)));
            decimal secondMonth = list.Sum(x => TextUtils.ToDecimal(x.GetValue(colSecondMonth)));
            decimal thirdMonth = list.Sum(x => TextUtils.ToDecimal(x.GetValue(colThirdMonth)));

            var node = treeData.FindNodeByFieldValue(colEvaluationCode.FieldName, "MA09");
            if (node == null) return;


            node.SetValue(colFirstMonth, firstMonth);
            node.SetValue(colSecondMonth, secondMonth);
            node.SetValue(colThirdMonth, thirdMonth);

        }

        private void btnRefreshData_Click(object sender, EventArgs e)
        {
            LoadDataEmployeeTeam();
        }

        private void grvTeam_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            List<GridBand> listBands = new List<GridBand>() { gridBand4, gridBand5, gridBand6, gridBand7 };
            BandedGridColumn col = (BandedGridColumn)e.Column;

            if (!listBands.Contains(col.OwnerBand)) return;
            decimal value = TextUtils.ToDecimal(e.Value);
            if (value == 0) e.DisplayText = "";
        }


        //===================== Update 28/12/2024 =====================
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
                        //if (emp.ID != 282) continue;
                        KPIPositionEmployeeModel position1 = SQLHelper<KPIPositionEmployeeModel>.FindByAttribute("EmployeeID", emp.ID).FirstOrDefault() ?? new KPIPositionEmployeeModel();
                        int positionID = position1.KPIPosiotionID > 0 ? TextUtils.ToInt(position1.KPIPosiotionID) : 1; // 1: kỹ thuật;

                        Expression exFindRule1 = new Expression(KPIEvaluationRuleModel_Enum.KPIPositionID.ToString(), positionID);
                        Expression exFindRule2 = new Expression(KPIEvaluationRuleModel_Enum.KPISessionID.ToString(), kpiSessionID);
                        KPIEvaluationRuleModel ruleModel = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(exFindRule1.And(exFindRule2)).FirstOrDefault() ?? new KPIEvaluationRuleModel();
                        if (ruleModel.ID <= 0) continue;

                        Expression ex1 = new Expression("EmployeeID", emp.ID);
                        Expression ex2 = new Expression("KPIEvaluationRuleID", ruleModel.ID);
                        Expression ex3 = new Expression("IsDelete", 0);
                        KPIEmployeePointModel empPoint = SQLHelper<KPIEmployeePointModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPIEmployeePointModel();
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
                    LoadKpiRule();
                    SaveData();
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
                dt.AcceptChanges();
                return dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return dt;
            }
        }

        private void treeData_GetCustomSummaryValue(object sender, DevExpress.XtraTreeList.GetCustomSummaryValueEventArgs e)
        {
            if (e.IsSummaryFooter && e.Column == colPercentBonus)
            {
                decimal totalPercent = TextUtils.ToDecimal(treeData.GetSummaryValue(colPercentRemaining));
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
        }

        private void btnUpdateDataRow_Click(object sender, EventArgs e)
        {
            if (treeData.FocusedNode == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TreeListNode selectedNode = treeData.FocusedNode;

            string code = TextUtils.ToString(selectedNode.GetValue(colEvaluationCode));

            int empPointID = GetKPIEmployeePointID();
            List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList(
                "spGetSumarizebyKPIEmpPointIDNew",
                new string[] { "@KPIEmployeePointID" },
                new object[] { empPointID });

            // Tính toán lại các tổng 
            decimal timeWork = TextUtils.ToDecimal(grvTeam.Columns["TimeWork"].SummaryItem.SummaryValue);
            decimal fiveS = TextUtils.ToDecimal(grvTeam.Columns["FiveS"].SummaryItem.SummaryValue);
            decimal reportWork = TextUtils.ToDecimal(grvTeam.Columns["ReportWork"].SummaryItem.SummaryValue);
            decimal customerComplaint = TextUtils.ToDecimal(grvTeam.Columns["CustomerComplaint"].SummaryItem.SummaryValue);
            decimal deadlineDelay = TextUtils.ToDecimal(grvTeam.Columns["DeadlineDelay"].SummaryItem.SummaryValue);
            decimal teamKPIKyNang = TextUtils.ToDecimal(grvTeam.Columns["KPIKyNang"].SummaryItem.SummaryValue);
            decimal teanKPIChung = TextUtils.ToDecimal(grvTeam.Columns["KPIChung"].SummaryItem.SummaryValue);
            decimal teamKPIPLC = TextUtils.ToDecimal(grvTeam.Columns["KPIPLC"].SummaryItem.SummaryValue);
            decimal teamKPIVISION = TextUtils.ToDecimal(grvTeam.Columns["KPIVision"].SummaryItem.SummaryValue);
            decimal teamKPISOFTWARE = TextUtils.ToDecimal(grvTeam.Columns["KPISoftware"].SummaryItem.SummaryValue);
            decimal missingTool = TextUtils.ToDecimal(grvTeam.Columns["MissingTool"].SummaryItem.SummaryValue);

            List<string> lstCodeTBP = new List<string>() { "MA03", "MA04", "NotWorking", "WorkLate" };
            var ltsMA11 = lstResult.Where(p => lstCodeTBP.Contains(p.EvaluationCode.Trim())).ToList();
            decimal totalErrorTBP = ltsMA11.Sum(p => p.FirstMonth + p.SecondMonth + p.ThirdMonth);

            //logic giống hàm loa tổng
            lstResult.AddRange(new List<KPISumarizeDTO>
            {
                new KPISumarizeDTO(){ EvaluationCode = "TEAM01", ThirdMonth = timeWork},
                new KPISumarizeDTO(){ EvaluationCode = "TEAM02", ThirdMonth = fiveS},
                new KPISumarizeDTO(){ EvaluationCode = "TEAM03", ThirdMonth = reportWork},
                new KPISumarizeDTO(){ EvaluationCode = "TEAM04", ThirdMonth = customerComplaint + missingTool + deadlineDelay},
                new KPISumarizeDTO(){ EvaluationCode = "TEAM05", ThirdMonth = customerComplaint},
                new KPISumarizeDTO(){ EvaluationCode = "TEAM06", ThirdMonth = missingTool},
                new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIKYNANG", ThirdMonth = teamKPIKyNang},
                new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIChung", ThirdMonth = teanKPIChung},
                new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIPLC", ThirdMonth = teamKPIPLC},
                new KPISumarizeDTO(){ EvaluationCode = "TEAMKPIVISION", ThirdMonth = teamKPIVISION},
                new KPISumarizeDTO(){ EvaluationCode = "TEAMKPISOFTWARE", ThirdMonth = teamKPISOFTWARE},
                new KPISumarizeDTO(){ EvaluationCode = "MA11", ThirdMonth = totalErrorTBP},
            });


            KPISumarizeDTO updateItem = lstResult
                .FirstOrDefault(x => x.EvaluationCode.Trim() == code.Trim());

            if (updateItem != null)
            {
                Lib.LockEvents = true;
                selectedNode.SetValue(colFirstMonth, updateItem.FirstMonth);
                selectedNode.SetValue(colSecondMonth, updateItem.SecondMonth);
                selectedNode.SetValue(colThirdMonth, updateItem.ThirdMonth);
                Lib.LockEvents = false;

                CalculatorPoint();
                CalculatorTotalPoint();

                //MessageBox.Show($"Đã cập nhật mã lỗi '{code}' thành công!",
                //                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Không tìm thấy dữ liệu cho mã: {code}",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

}
