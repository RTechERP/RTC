using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
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
    public partial class frmKPISessionDetails : _Forms
    {
        public int departmentID = 0;
        public KPISessionModel kpiSession = new KPISessionModel();
        public frmKPISessionDetails()
        {
            InitializeComponent();
        }
        private void KPISessionDetails_Load(object sender, EventArgs e)
        {
            LoadDepartment();
            ChangeValueNameCode();
            LoadKPISession();
            LoadData();
        }
        private void LoadKPISession()
        {
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(p => p.ID).ToList();
            cboKPISession.Properties.DataSource = lst;
            cboKPISession.Properties.DisplayMember = "Code";
            cboKPISession.Properties.ValueMember = "ID";
        }
        private void LoadDepartment()
        {
            List<DepartmentModel> ls = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DataSource = ls;
            cboDepartment.EditValue = 2;
        }

        private void LoadData()
        {
            txtYear.Value = DateTime.Now.Year;
            txtQuarter.Value = (int)((DateTime.Now.Month + 2) / 3);
            cboDepartment.EditValue = departmentID;
            if (kpiSession.ID > 0)
            {
                txtYear.Value = TextUtils.ToInt(kpiSession.YearEvaluation);
                txtQuarter.Value = TextUtils.ToInt(kpiSession.QuarterEvaluation);
                txtCode.Text = kpiSession.Code;
                txtName.Text = kpiSession.Name;
                cboDepartment.EditValue = kpiSession.DepartmentID;
            }
        }
        private bool CheckValidate()
        {
            int year = TextUtils.ToInt(txtYear.Value);
            int quarter = TextUtils.ToInt(txtQuarter.Value);
            string code = txtCode.Text.Trim();
            string name = txtName.Text.Trim();
            if (ckbCopy.Checked)
            {
                if (TextUtils.ToInt(cboKPISession.EditValue) <= 0)
                {
                    MessageBox.Show("Vui lòng chọn dữ liệu Kỳ đánh giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (year <= 0)
            {
                MessageBox.Show($"Vui lòng nhập Năm!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (quarter <= 0)
            {
                MessageBox.Show($"Vui lòng nhập Quý!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show($"Vui lòng nhập Mã kỳ thi!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show($"Vui lòng nhập Tên kỳ thi!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            Expression ex4 = new Expression("YearEvaluation", year);
            Expression ex2 = new Expression("ID", kpiSession.ID, "<>");
            Expression e1 = new Expression("DepartmentID", TextUtils.ToInt(cboDepartment.EditValue));
            Expression ex3 = new Expression("IsDeleted", 0);
            List<KPISessionModel> duplicate = SQLHelper<KPISessionModel>.FindByExpression(ex4.And(ex3.And(ex2).And(e1)));

            if (ckbCopy.Checked)
            {
                if (duplicate.Any(p => p.QuarterEvaluation == quarter || p.Code == code))
                {
                    if (MessageBox.Show($"Yếu tố đánh giá tại quý {quarter} năm {year} đã tồn tại! Bạn có muốn ghi đè dữ liệu không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return false;
                }
            }
            else
            {
                if (duplicate.Any(p => p.QuarterEvaluation == quarter))
                {
                    MessageBox.Show($"Quý [{quarter}] trong năm [{year}] đã có kỳ thi!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (duplicate.Any(p => p.Code == code))
                {
                    MessageBox.Show($"Mã kỳ thi đã được sửa dụng trong năm [{year}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
        private bool SaveData()
        {
            if (!CheckValidate()) return false;

            KPISessionModel model = SQLHelper<KPISessionModel>.FindByID(kpiSession.ID);
            model.YearEvaluation = TextUtils.ToInt(txtYear.Value);
            model.QuarterEvaluation = TextUtils.ToInt(txtQuarter.Value);
            model.DepartmentID = TextUtils.ToInt(cboDepartment.EditValue);
            model.Code = txtCode.Text.Trim();
            model.Name = txtName.Text.Trim();
            if (model.ID > 0)
            {
                SQLHelper<KPISessionModel>.Update(model);
            }
            else
            {
                model.ID = SQLHelper<KPISessionModel>.Insert(model).ID;
                CreateAutoKPIExam(model);
                CreateAutoKPIRule(model);
            }

            return true;
        }
        private void Reset()
        {
            txtCode.Clear();
            txtName.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isSuccess = ckbCopy.Checked ? CopyData() : SaveData();
            if (isSuccess)
            {
                kpiSession = new KPISessionModel();
                Reset();
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            bool isSuccess = ckbCopy.Checked ? CopyData() : SaveData();
            if (isSuccess)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void KPISessionDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void ChangeValueNameCode()
        {
            DepartmentModel department = (DepartmentModel)cboDepartment.GetSelectedDataRow() ?? new DepartmentModel();
            txtCode.Text = $"KPI_{TextUtils.ToString(department.Code).Trim()}_{txtYear.Value}_Q{txtQuarter.Value}";
            txtName.Text = $"Kỳ đánh giá KPI {TextUtils.ToString(department.Name).Trim()} quý {txtQuarter.Value}-{txtYear.Value}";
        }
        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            ChangeValueNameCode();
        }
        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            ChangeValueNameCode();
        }

        private void txtQuarter_ValueChanged(object sender, EventArgs e)
        {
            ChangeValueNameCode();
        }
        private bool CopyData()
        {
            if (!CheckValidate()) return false;
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang sao chép dữ liệu..."))
            {
                Expression e1 = new Expression("DepartmentID", TextUtils.ToInt(cboDepartment.EditValue));
                Expression e2 = new Expression("QuarterEvaluation", TextUtils.ToInt(txtQuarter.Value));
                Expression e3 = new Expression("YearEvaluation", TextUtils.ToInt(txtYear.Value));
                Expression e4 = new Expression("IsDeleted", 0);

                //Xóa session cũ
                List<KPISessionModel> lstOldSession = SQLHelper<KPISessionModel>.FindByExpression(e1.And(e2).And(e3).And(e4));
                if (lstOldSession.Count > 0)
                {
                    string lstOldSessionIDs = string.Join(",", lstOldSession.Select(p => p.ID));
                    Dictionary<string, object> newDict = new Dictionary<string, object>()
                    {
                        {KPISessionModel_Enum.IsDeleted.ToString(), 1},
                        {KPISessionModel_Enum.UpdatedBy.ToString(), Global.AppCodeName},
                        {KPISessionModel_Enum.UpdatedDate.ToString(), DateTime.Now}
                    };
                    SQLHelper<KPISessionModel>.UpdateFields(newDict, new Expression("ID", lstOldSessionIDs, "IN"));
                }

                // Insert phần tử mới
                KPISessionModel model = new KPISessionModel();
                model.YearEvaluation = TextUtils.ToInt(txtYear.Value);
                model.QuarterEvaluation = TextUtils.ToInt(txtQuarter.Value);
                model.DepartmentID = TextUtils.ToInt(cboDepartment.EditValue);
                model.Code = txtCode.Text.Trim();
                model.Name = txtName.Text.Trim();
                model.ID = SQLHelper<KPISessionModel>.Insert(model).ID;


                //Get các phần tử sao chép
                Expression ex1 = new Expression("KPISessionID", TextUtils.ToInt(cboKPISession.EditValue));
                Expression ex2 = new Expression("IsDeleted", 0);
                List<KPIExamModel> lstExam = SQLHelper<KPIExamModel>.FindByExpression(ex1.And(ex2));

                KPISessionModel sessionOld = (KPISessionModel)cboKPISession.GetSelectedDataRow() ?? new KPISessionModel();
                string oldCode = $"{sessionOld.YearEvaluation}_Q{sessionOld.QuarterEvaluation}";
                string oldName = $"Q{sessionOld.QuarterEvaluation}-{sessionOld.YearEvaluation}";

                string newCode = $"{txtYear.Value}_Q{txtQuarter.Value}";
                string newName = $"Q{txtQuarter.Value}-{txtYear.Value}";


                string stt = "1.1".Remove("1.1".LastIndexOf("."));
                foreach (KPIExamModel item in lstExam)
                {
                    int oldId = item.ID;
                    item.KPISessionID = model.ID;
                    item.ID = 0;

                    item.ExamCode = item.ExamCode.Replace(oldCode, newCode);
                    item.ExamName = item.ExamName.Replace(oldName, newName);
                    item.ID = SQLHelper<KPIExamModel>.Insert(item).ID;

                    List<KPIExamPositionModel> listPosition = SQLHelper<KPIExamPositionModel>.FindByAttribute("KPIExamID", oldId);
                    foreach (KPIExamPositionModel position in listPosition)
                    {
                        position.KPIExamID = item.ID;
                        position.ID = 0;

                        SQLHelper<KPIExamPositionModel>.Insert(position);
                    }

                    //Get các phần tử sao chép!
                    List<KPIEvaluationFactorsModel> lstDetails = SQLHelper<KPIEvaluationFactorsModel>.ProcedureToList("spGetAllKPIEvaluationByYearAndQuarter",
                                                                                    new string[] { "@KPIExamID", "@EvaluationType" },
                                                                                    new object[] { oldId, 0 }).OrderBy(p => p.ParentID).ToList();
                    foreach (KPIEvaluationFactorsModel detail in lstDetails)
                    {
                        //if (detail.STT == "1.1.1")
                        //{

                        //    int a = 1;
                        //}
                        detail.KPIExamID = item.ID;
                        detail.ID = 0;
                        if (detail.ParentID > 0)
                        {
                            //int indexST = detail.STT.LastIndexOf(".");
                            //string parentStt = detail.STT.Substring(0, indexST);
                            //KPIEvaluationFactorsModel parentModel = lstDetails.FirstOrDefault(p => p.STT == parentStt) ?? new KPIEvaluationFactorsModel();
                            //detail.ParentID = parentModel.ID;

                            var exp1 = new Expression(KPIEvaluationFactorsModel_Enum.STT.ToString(), detail.STT.Remove(detail.STT.LastIndexOf(".")));
                            var exp2 = new Expression(KPIEvaluationFactorsModel_Enum.KPIExamID.ToString(), item.ID);
                            var exp3 = new Expression(KPIEvaluationFactorsModel_Enum.EvaluationType.ToString(), detail.EvaluationType);
                            var exp4 = new Expression(KPIEvaluationFactorsModel_Enum.IsDeleted.ToString(), 0);

                            KPIEvaluationFactorsModel parent = SQLHelper<KPIEvaluationFactorsModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4)).FirstOrDefault() ?? new KPIEvaluationFactorsModel();
                            detail.ParentID = parent.ID;
                        }
                        detail.ID = SQLHelper<KPIEvaluationFactorsModel>.Insert(detail).ID;
                    }
                };
                CopyCriteria();
            }
            return true;
        }
        private void ckbCopy_CheckedChanged(object sender, EventArgs e)
        {
            groupControl2.Enabled = ckbCopy.Checked;
        }
        private void CopyCriteria()
        {
            KPISessionModel session = SQLHelper<KPISessionModel>.FindByID(TextUtils.ToInt(cboKPISession.EditValue));
            if (session.ID <= 0) return;


            // Xoa du lieu cu
            Expression ex4 = new Expression(KPICriteriaModel_Enum.KPICriteriaQuater, txtQuarter.Value);
            Expression ex5 = new Expression(KPICriteriaModel_Enum.KPICriteriaYear, txtYear.Value);
            List<KPICriteriaModel> lstCriteriaOld = SQLHelper<KPICriteriaModel>.FindByExpression(ex4.And(ex5));

            foreach (KPICriteriaModel item in lstCriteriaOld)
            {
                SQLHelper<KPICriteriaDetailModel>.DeleteByAttribute("KPICriteriaID", item.ID);
                var mydict = new Dictionary<string, object>() {
                    { KPICriteriaModel_Enum.IsDeleted.ToString(), true },
                    {  KPICriteriaModel_Enum.UpdatedDate.ToString(), DateTime.Now },
                    {  KPICriteriaModel_Enum.UpdatedBy.ToString(), Global.LoginName }
                };
                SQLHelper<KPICriteriaModel>.UpdateFieldsByID(mydict, item.ID);
            }


            // copy dữ liệu từ năm chọn sang quý và năm mới
            Expression ex1 = new Expression(KPICriteriaModel_Enum.IsDeleted, 0);
            Expression ex2 = new Expression(KPICriteriaModel_Enum.KPICriteriaQuater, session.QuarterEvaluation);
            Expression ex3 = new Expression(KPICriteriaModel_Enum.KPICriteriaYear, session.YearEvaluation);
            List<KPICriteriaModel> lstData = SQLHelper<KPICriteriaModel>.FindByExpression(ex1.And(ex2).And(ex3));
            foreach (KPICriteriaModel item in lstData)
            {
                List<KPICriteriaDetailModel> lstDetails = SQLHelper<KPICriteriaDetailModel>.FindByAttribute("KPICriteriaID", item.ID);
                item.ID = 0;
                item.KPICriteriaQuater = TextUtils.ToInt(txtQuarter.Value);
                item.KPICriteriaYear = TextUtils.ToInt(txtYear.Value);
                item.ID = SQLHelper<KPICriteriaModel>.Insert(item).ID;
                foreach (KPICriteriaDetailModel detail in lstDetails)
                {
                    detail.ID = 0;
                    detail.KPICriteriaID = item.ID;
                    SQLHelper<KPICriteriaDetailModel>.Insert(detail);
                }

            }
        }

        private void CreateAutoKPIExam(KPISessionModel sessionKPI)
        {
            DataTable dt = SQLHelper<KPIPositionModel>.LoadDataFromSP("spGetKPIPositionByExamID", new string[] { "@KPIExamID" }, new object[] { 0 });
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

        private void CreateAutoKPIRule(KPISessionModel sessionKPI)
        {
            DataTable dt = SQLHelper<KPIPositionModel>.LoadDataFromSP("spGetKPIPositionByExamID", new string[] { "@KPIExamID" }, new object[] { 0 });
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


    }
}
