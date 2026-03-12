using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors.Repository;
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
    public partial class frmKPIEvaluationFactors : _Forms
    {
        public string deName;
        public int departmentID = 0;
        private int rowHandleMaster = 0;
        private int rowHandleDetails = 0;
        public frmKPIEvaluationFactors()
        {
            InitializeComponent();
        }
        private void frmKPIEvaluationFactors_Load(object sender, EventArgs e)
        {
            this.Text += " - " + deName;
            txtYear.Value = DateTime.Now.Year;
            LoadDepartMent();
            LoadData();
        }

        private void LoadDepartMent()
        {
            List<DepartmentModel> lst = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboDepartMent.Properties.DataSource = lst;
            cboDepartMent.Properties.ValueMember = "ID";
            cboDepartMent.Properties.DisplayMember = "Name";

            cboDepartMent.EditValue = departmentID;
        }

        private void LoadData()
        {
            Expression ex1 = new Expression("YearEvaluation", TextUtils.ToInt(txtYear.Value));
            Expression ex2 = new Expression("IsDeleted", 0);
            Expression ex3 = new Expression("DepartmentID", TextUtils.ToInt(cboDepartMent.EditValue)); // update 5225
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByExpression(ex1.And(ex2).And(ex3)).OrderByDescending(p => p.ID).ToList();
            grdMaster.DataSource = lst;
            grvMaster.FocusedRowHandle = rowHandleMaster;
            LoadDetailNew();
        }
        //private void LoadDetail()
        //{
        //    int kpiSessionId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSessionID));
        //    departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
        //    DataTable lst = SQLHelper<KPIExamModel>.LoadDataFromSP("spGetKPIExamByKPISessionID",
        //                                new string[] { "@KPISessionID", "@DepartmentID" },
        //                                new object[] { kpiSessionId, departmentID });
        //    grdDetails.DataSource = lst;
        //    grvDetails.FocusedRowHandle = rowHandleDetails;
        //    LoadKPIEvaluation();
        //}
        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetailNew();
        }
        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            btnUpdateSession_Click(null, null);
        }
        private void grvDetails_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadKPIEvaluation();
        }

        private void LoadKPIEvaluation()
        {
            int kpiExamID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colExamID));
            List<KPIEvaluationFactorsModel> lst = SQLHelper<KPIEvaluationFactorsModel>.ProcedureToList("spGetAllKPIEvaluationByYearAndQuarter",
                                                                                new string[] { "@KPIExamID", "@EvaluationType" },
                                                                                new object[] { kpiExamID, 1 });
            treeData.DataSource = lst;
            treeData.ExpandAll();


            List<KPIEvaluationFactorsModel> lst2 = SQLHelper<KPIEvaluationFactorsModel>.ProcedureToList("spGetAllKPIEvaluationByYearAndQuarter",
                                                                                new string[] { "@KPIExamID", "@EvaluationType" },
                                                                                new object[] { kpiExamID, 2 });
            treeData2.DataSource = lst2;
            treeData2.ExpandAll();



            List<KPIEvaluationFactorsModel> lst3 = SQLHelper<KPIEvaluationFactorsModel>.ProcedureToList("spGetAllKPIEvaluationByYearAndQuarter",
                                                                                new string[] { "@KPIExamID", "@EvaluationType" },
                                                                                new object[] { kpiExamID, 3 });
            treeData3.DataSource = lst3;
            treeData3.ExpandAll();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            TreeListNode nodeFocus1 = treeData.FocusedNode;
            TreeListNode nodeFocus2 = treeData2.FocusedNode;
            TreeListNode nodeFocus3 = treeData3.FocusedNode;
            //bool isTreeData1 = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? true : false;
            bool isTabOne = xtraTabControl1.SelectedTabPage == xtraTabPage1;
            bool isTabTwo = xtraTabControl1.SelectedTabPage == xtraTabPage2;
            DevExpress.XtraTreeList.TreeList data = isTabOne ? treeData : (isTabTwo ? treeData2 : treeData3);
            int ExamID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colExamID));
            if (ExamID <= 0)
            {
                MessageBox.Show("Hãy chọn Bài đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmKPIEvaluationFactorsDetails frm = new frmKPIEvaluationFactorsDetails();

            TreeListNode focusRowHandle = isTabOne ? nodeFocus1 : (isTabTwo ? nodeFocus2 : nodeFocus3);

            int evalationFactorID = TextUtils.ToInt(data.GetFocusedRowCellValue(isTabOne ? colID : (isTabTwo ? colSpecialtyID : colGeneralID)));
            KPIEvaluationFactorsModel kpiEvalFactor = SQLHelper<KPIEvaluationFactorsModel>.FindByID(evalationFactorID);
            if (kpiEvalFactor.ParentID > 0)
            {
                KPIEvaluationFactorsModel model = SQLHelper<KPIEvaluationFactorsModel>.FindByID(TextUtils.ToInt(kpiEvalFactor.ParentID));
                Expression ex1 = new Expression(KPIEvaluationFactorsModel_Enum.ParentID.ToString(), kpiEvalFactor.ParentID);
                Expression ex2 = new Expression(KPIEvaluationFactorsModel_Enum.IsDeleted.ToString(), 0);
                List<KPIEvaluationFactorsModel> lst = SQLHelper<KPIEvaluationFactorsModel>.FindByExpression(ex1.And(ex2));
                string _str = model.STT + "." + TextUtils.ToString(lst.Count + 1);


                frm.kpiEvaluationFactors.StandardPoint = kpiEvalFactor.StandardPoint;
                frm.kpiEvaluationFactors.STT = _str;
                frm.kpiEvaluationFactors.ParentID = kpiEvalFactor.ParentID;
                frm.kpiEvaluationFactors.SpecializationType = kpiEvalFactor.SpecializationType;
            }

            if (focusRowHandle != null)
            {
                if (focusRowHandle.HasChildren)
                {
                    KPIEvaluationFactorsModel model = SQLHelper<KPIEvaluationFactorsModel>.FindByID(evalationFactorID);
                    Expression ex1 = new Expression(KPIEvaluationFactorsModel_Enum.ParentID.ToString(), model.ID);
                    Expression ex2 = new Expression(KPIEvaluationFactorsModel_Enum.IsDeleted.ToString(), 0);
                    List<KPIEvaluationFactorsModel> lst = SQLHelper<KPIEvaluationFactorsModel>.FindByExpression(ex1.And(ex2));
                    string _str = model.STT + "." + TextUtils.ToString(lst.Count + 1);

                    frm.kpiEvaluationFactors.StandardPoint = model.StandardPoint;
                    frm.kpiEvaluationFactors.STT = _str;
                    frm.kpiEvaluationFactors.ParentID = model.ID;
                    frm.kpiEvaluationFactors.SpecializationType = model.SpecializationType;
                }

            }


            frm.kpiExam = SQLHelper<KPIExamModel>.FindByID(ExamID);
            frm.YearEvaluation = TextUtils.ToInt(txtYear.Value);
            frm.QuaterEvaluation = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSesionQuarter));
            frm.EValuationType = isTabOne ? 1 : (isTabTwo ? 2 : 3);
            frm.departmentID = TextUtils.ToInt(cboDepartMent.EditValue);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadKPIEvaluation();
                treeData.FocusedNode = nodeFocus1;
                treeData2.FocusedNode = nodeFocus2;
                treeData3.FocusedNode = nodeFocus3;
            }
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtQuarter_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboPositionType_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            TreeListNode nodeFocus1 = treeData.FocusedNode;
            TreeListNode nodeFocus2 = treeData2.FocusedNode;
            TreeListNode nodeFocus3 = treeData3.FocusedNode;


            //bool isTreeData1 = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? true : false;
            bool isTabOne = xtraTabControl1.SelectedTabPage == xtraTabPage1;
            bool isTabTwo = xtraTabControl1.SelectedTabPage == xtraTabPage2;

            DevExpress.XtraTreeList.TreeList data = isTabOne ? treeData : (isTabTwo ? treeData2 : treeData3);
            int evalationFactorID = TextUtils.ToInt(data.GetFocusedRowCellValue(isTabOne ? colID : (isTabTwo ? colSpecialtyID : colGeneralID)));
            if (evalationFactorID <= 0)
            {
                MessageBox.Show("Hãy chọn Tiêu chí đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int ExamID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colExamID));
            if (ExamID <= 0)
            {
                MessageBox.Show("Hãy chọn Bài đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmKPIEvaluationFactorsDetails frm = new frmKPIEvaluationFactorsDetails();

            frm.kpiEvaluationFactors = SQLHelper<KPIEvaluationFactorsModel>.FindByID(evalationFactorID);
            frm.kpiExam = SQLHelper<KPIExamModel>.FindByID(ExamID);
            frm.YearEvaluation = TextUtils.ToInt(txtYear.Value);
            frm.QuaterEvaluation = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSesionQuarter));
            frm.EValuationType = isTabOne ? 1 : (isTabTwo ? 2 : 3);
            frm.departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadKPIEvaluation();
                treeData.FocusedNode = nodeFocus1;
                treeData2.FocusedNode = nodeFocus2;
                treeData3.FocusedNode = nodeFocus3;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //bool isTreeData1 = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? true : false;
            bool isTabOne = xtraTabControl1.SelectedTabPage == xtraTabPage1;
            bool isTabTwo = xtraTabControl1.SelectedTabPage == xtraTabPage2;


            DevExpress.XtraTreeList.TreeList data = isTabOne ? treeData : (isTabTwo ? treeData2 : treeData3);
            string stt = TextUtils.ToString(data.GetFocusedRowCellValue(isTabOne ? colSTT : (isTabTwo ? colSpecialtySTT : colGeneralSTT)));
            int ID = TextUtils.ToInt(data.GetFocusedRowCellValue(isTabOne ? colID : (isTabTwo ? colSpecialtyID : colGeneralID)));
            if (MessageBox.Show(string.Format("Bạn có muốn xóa Tiêu đánh giá [{0}] hay không ?", stt), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SQLHelper<KPIEvaluationFactorsModel>.DeleteModelByID(ID);
                LoadKPIEvaluation();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            //========================= lee min khooi update 08/10/2024 =====================
            rowHandleMaster = grvMaster.FocusedRowHandle;
            int kpiSessionId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSessionID));

            frmKPISessionDetails frm = new frmKPISessionDetails();
            frm.cboKPISession.EditValue = kpiSessionId;
            frm.ckbCopy.Checked = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void treeData_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                return;
            }
        }

        private void btAddSession_Click(object sender, EventArgs e)
        {
            rowHandleMaster = grvMaster.FocusedRowHandle;
            frmKPISessionDetails frm = new frmKPISessionDetails();
            departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            frm.departmentID = departmentID;
            if (frm.ShowDialog() == DialogResult.OK)
            {

                //// VTNam update
                //int id = frm.id;
                //int year = frm.year;
                //int quarter = frm.quarter;
                //string department = frm.department;
                //if (year > 0 || quarter > 0)
                //{
                //    DataTable dt = SQLHelper<KPIPositionModel>.LoadDataFromSP("spGetKPIPositionByExamID", new string[] { "@KPIExamID" }, new object[] { 0 });
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        string positionCode = TextUtils.ToString(row["PositionCode"]).ToUpper();
                //        string kpi = "KPI";
                //        string q = $"Q{quarter}-{year}";

                //        switch (positionCode)
                //        {
                //            case "KT":
                //                AddSessionDetail(id, $"{kpi}KYTHUAT", $"{kpi} {department} {q}", 1);
                //                break;
                //            case "ADMIN":
                //                AddSessionDetail(id, $"{kpi}{positionCode}", $"{kpi} {positionCode} {department} {q}", 2);
                //                break;
                //            case "PRO":
                //                AddSessionDetail(id, $"{kpi}{positionCode}", $"{kpi} {positionCode} {department} {q}", 3);
                //                break;
                //            case "SENIOR":
                //                AddSessionDetail(id, $"{kpi}{positionCode}", $"{kpi} {positionCode} {department} {q}", 4);
                //                break;
                //            case "TBP/PP":
                //                AddSessionDetail(id, $"{kpi}PHOPHONG", $"{kpi} PHOPHONG {department} {q}", 5);
                //                break;
                //            default:
                //                break;

                //        }

                //    }
                //}

                LoadData();
            }
        }

        //// VTNam update
        //private void AddSessionDetail(int id, string ec, string en, int idPosition)
        //{
        //    KPIExamModel model = new KPIExamModel();
        //    model.KPISessionID = id;
        //    model.ExamCode = ec;
        //    model.ExamName = en;
        //    model.IsDeleted = false;
        //    model.ID = SQLHelper<KPIExamModel>.Insert(model).ID;

        //    KPIExamPositionModel newdetail = new KPIExamPositionModel()
        //    {
        //        KPIExamID = model.ID,
        //        KPIPositionID = idPosition
        //    };
        //    SQLHelper<KPIExamPositionModel>.Insert(newdetail);
        //}

        private void btnUpdateSession_Click(object sender, EventArgs e)
        {
            rowHandleMaster = grvMaster.FocusedRowHandle;
            int kpiSessionId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSessionID));
            frmKPISessionDetails frm = new frmKPISessionDetails();
            frm.kpiSession = SQLHelper<KPISessionModel>.FindByID(kpiSessionId);
            if (frm.kpiSession.ID <= 0) return;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void btnDeleteSession_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSessionID));
            if (ID <= 0) return;
            if (MessageBox.Show($"Bạn có muốn xóa Kỳ đánh giá [{TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colSessionCode))}] hay không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                rowHandleMaster = 0;
                KPISessionModel model = SQLHelper<KPISessionModel>.FindByID(ID);
                model.IsDeleted = true;
                SQLHelper<KPISessionModel>.Update(model);
                LoadData();
            }
        }

        private void btnAddExam_Click(object sender, EventArgs e)
        {
            rowHandleDetails = grvDetails.FocusedRowHandle;
            int sessionId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSessionID));
            if (sessionId <= 0)
            {
                MessageBox.Show("Vui lòng chọn Kỳ thi!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmKPIExam frm = new frmKPIExam(sessionId);
            //frm.kpiSessionId = sessionId;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDetailNew();
            }
        }
        private void btnUpdateExam_Click(object sender, EventArgs e)
        {
            rowHandleDetails = grvDetails.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colExamID));
            if (ID <= 0) return;

            int sessionId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSessionID));
            if (sessionId <= 0)
            {
                MessageBox.Show("Vui lòng chọn Kỳ thi!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmKPIExam frm = new frmKPIExam(sessionId);
            frm.kpiExam = SQLHelper<KPIExamModel>.FindByID(ID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDetailNew();
            }
        }

        private void btnDeleteExam_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colExamID));
            if (ID <= 0) return;
            if (MessageBox.Show($"Bạn có muốn xóa Bài đánh giá [{TextUtils.ToString(grvDetails.GetFocusedRowCellValue(colExamName))}] hay không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                KPIExamModel model = SQLHelper<KPIExamModel>.FindByID(ID);
                model.IsDeleted = true;
                SQLHelper<KPIExamModel>.Update(model);
                LoadDetailNew();
            }
        }

        private void grvDetails_DoubleClick(object sender, EventArgs e)
        {
            btnUpdateExam_Click(null, null);
        }

        private void treeData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnCopyExam_Click(object sender, EventArgs e)
        {
            rowHandleMaster = grvMaster.FocusedRowHandle;
            int kpiSessionId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSessionID));
            int kpiSessionDetailId = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colExamID));
            string nameExam = TextUtils.ToString(grvDetails.GetFocusedRowCellValue(colExamName));
            if (kpiSessionId <= 0)
            {
                MessageBox.Show("Vui lòng chọn kỳ có bài đánh giá bạn muốn sao chép!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmCopyKPIExam frm = new frmCopyKPIExam();
            frm.kpiSessionID = kpiSessionId;
            frm.kpiSessionDetailId = kpiSessionDetailId;
            frm.nameExam = nameExam;
            frm.departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cboDepartMent_EditValueChanged(object sender, EventArgs e)
        {

        }


        #region TN.Bình update
        private void LoadDetailNew()
        {
            int kpiSessionId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSessionID));
            DataTable lst = SQLHelper<KPIExamModel>.LoadDataFromSP("spGetKPIExamByKPISessionID", new string[] { "@KPISessionID", "@DepartmentID" }, new object[] { kpiSessionId, departmentID });
            //TN.Binh update 10/09/25
            //RepositoryItemLookUpEdit repo = new RepositoryItemLookUpEdit();
            //repo.DataSource = new[]
            //{
            //    new { ID = 1, Name = "Kỹ thuật, Pro" },
            //    new { ID = 3, Name = "Senior" },
            //    new { ID = 4, Name = "Phó phòng" },
            //    new { ID = 2, Name = "Admin" },
            //};
            //repo.DisplayMember = "Name";   // Cột hiển thị
            //repo.ValueMember = "ID";       // Cột dữ liệu thực tế (TypePosition)

            //// Gán cho cột Loại vị trí trong grid
            //grvDetails.Columns["TypePosition"].ColumnEdit = repo;
            ////end update
            grdDetails.DataSource = lst;
            grvDetails.FocusedRowHandle = rowHandleDetails;
            LoadKPIEvaluation();
        }

        private void CreateAutoKPIExamNew(KPISessionModel sessionKPI)
        {
            //TN.Binh update 10/09/25
            DataTable dt = SQLHelper<KPIPositionModel>.LoadDataFromSP("spGetKPIPositionByExamID",
                new string[] { "@KPIExamID", "@KPISessionID" },
                new object[] { 0, sessionKPI.ID }); //TN.Binh update 05/09/25
            List<KPIExamModel> lstExamOld = SQLHelper<KPIExamModel>.FindAll().Where(x => x.KPISessionID == sessionKPI.ID).ToList();
            if (lstExamOld != null && lstExamOld.Count > 0)
            {
                foreach (var item in lstExamOld)
                {
                    item.IsDeleted = true;
                    item.UpdatedBy = Global.AppCodeName;
                    item.UpdatedDate = DateTime.Now;
                    SQLHelper<KPIExamModel>.Update(item);
                    List<KPIExamPositionModel> lstExamPositionOld = SQLHelper<KPIExamPositionModel>.FindAll().Where(x => x.KPIExamID == item.ID).ToList();
                    if (lstExamPositionOld != null && lstExamPositionOld.Count > 0)
                    {
                        foreach (var itemDetail in lstExamPositionOld)
                        {
                            itemDetail.IsDeleted = true;
                            itemDetail.UpdatedBy = Global.AppCodeName;
                            itemDetail.UpdatedDate = DateTime.Now;
                            SQLHelper<KPIExamPositionModel>.Update(itemDetail);
                        }
                    }

                }
            }

            foreach (DataRow row in dt.Rows)
            {
                string positionCode = TextUtils.ToString(row["PositionCode"]).ToUpper();
                int positionID = TextUtils.ToInt(row["ID"]);
                string positionName = TextUtils.ToString(row["PositionName"]).ToUpper();
                if (positionID <= 0) continue;

                KPIExamModel model = new KPIExamModel();
                model.KPISessionID = sessionKPI.ID;
                model.ExamCode = $"KPI_{positionCode.Trim()}_{sessionKPI.YearEvaluation}_Q{sessionKPI.QuarterEvaluation}";
                model.ExamName = $"KPI {positionName.Trim()} Q{sessionKPI.QuarterEvaluation}-{sessionKPI.YearEvaluation}";
                model.IsDeleted = false;
                model.IsActive = true;
                model.Deadline = DateTime.Now.AddMonths(1);
                model.ID = SQLHelper<KPIExamModel>.Insert(model).ID;

                KPIExamPositionModel newdetail = new KPIExamPositionModel()
                {
                    KPIExamID = model.ID,
                    KPIPositionID = positionID
                };
                SQLHelper<KPIExamPositionModel>.Insert(newdetail);
            }
        }
        private void CreateAutoKPIRuleNew(KPISessionModel sessionKPI)
        {
            List<KPIEvaluationRuleModel> lstRuleOld = SQLHelper<KPIEvaluationRuleModel>.FindAll().Where(x => x.KPISessionID == sessionKPI.ID).ToList();
            if (lstRuleOld != null && lstRuleOld.Count > 0)
            {
                foreach (var item in lstRuleOld)
                {
                    item.IsDeleted = true;
                    item.UpdatedBy = Global.AppCodeName;
                    item.UpdatedDate = DateTime.Now;
                    SQLHelper<KPIEvaluationRuleModel>.Update(item);
                }
            }
            DataTable dt = SQLHelper<KPIPositionModel>.LoadDataFromSP("spGetKPIPositionByExamID",
                                                                    new string[] { "@KPIExamID", "@KPISessionID" },
                                                                    new object[] { 0, sessionKPI.ID }); //TN.Binh update 05/09/25
            foreach (DataRow row in dt.Rows)
            {
                string positionCode = TextUtils.ToString(row["PositionCode"]).ToUpper();
                int positionID = TextUtils.ToInt(row["ID"]);
                string positionName = TextUtils.ToString(row["PositionName"]).ToUpper();
                if (positionID <= 0) continue;

                KPIEvaluationRuleModel model = new KPIEvaluationRuleModel();
                model.KPISessionID = sessionKPI.ID;
                model.RuleCode = $"KPIRule_{positionCode.Trim()}_{sessionKPI.YearEvaluation}_Q{sessionKPI.QuarterEvaluation}";
                model.RuleName = $"Đánh giá KPI Rule {positionName.Trim()} Q{sessionKPI.QuarterEvaluation}-{sessionKPI.YearEvaluation}";
                model.IsDeleted = false;
                model.KPIPositionID = positionID;
                model.ID = SQLHelper<KPIEvaluationRuleModel>.Insert(model).ID;
            }
        }
        #endregion

        private void btnCreateAutoKPIExam_Click(object sender, EventArgs e)
        {
            int kpiSessionId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSessionID));
            KPISessionModel model = SQLHelper<KPISessionModel>.FindByID(kpiSessionId);
            Expression ex1 = new Expression("KPISessionID", kpiSessionId);
            Expression ex2 = new Expression("IsDeleted", 0);
            List<KPIPositionModel> lstData = SQLHelper<KPIPositionModel>.FindByExpression(ex1.And(ex2));
            if (lstData.Count <= 0)
            {
                MessageBox.Show("Kỳ đánh giá chưa được thêm vị trí.\n Vui lòng thêm vị trí!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                int departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
                frmKPIPositionEmployee frm = new frmKPIPositionEmployee(departmentID);
                frm.kpiSessionID = kpiSessionId;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            CreateAutoKPIExamNew(model);
            CreateAutoKPIRuleNew(model);
            LoadData();
        }
    }
}
