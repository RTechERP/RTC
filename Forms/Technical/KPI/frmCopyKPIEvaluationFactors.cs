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
    public partial class frmCopyKPIEvaluationFactors : _Forms
    {
        public KPISessionModel kpiSession = new KPISessionModel();
        public frmCopyKPIEvaluationFactors()
        {
            InitializeComponent();
        }

        private void frmCopyKPIEvaluationFactors_Load(object sender, EventArgs e)
        {
            LoadKPISession();
            txtYear.Value = DateTime.Now.Year;
            txtQuarter.Value = (int)((DateTime.Now.Month + 2) / 3);
        }
        private void LoadKPISession()
        {
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(p => p.ID).ToList();
            cboKPISession.Properties.DataSource = lst;
            cboKPISession.Properties.DisplayMember = "Code";
            cboKPISession.Properties.ValueMember = "ID";
            cboKPISession.EditValue = kpiSession.ID;
        }
        private bool CheckValidate()
        {
            if (TextUtils.ToInt(cboKPISession.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn dữ liệu Kỳ đánh giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã Kỳ đánh giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập mã Tên đánh giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void ChangeValueNameCode()
        {
            txtCode.Text = $"KPI_{txtYear.Value}_Q{txtQuarter.Value}";
            txtName.Text = $"Kỳ đánh giá quý {txtQuarter.Value}-{txtYear.Value}";
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {


            if (!CheckValidate()) return;

            string code = txtCode.Text.Trim();
            string name = txtName.Text.Trim();
            int quarter = TextUtils.ToInt(txtQuarter.Value);
            int year = TextUtils.ToInt(txtYear.Value);

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang sao chép dữ liệu..."))
            {

                KPISessionModel kpiDataCopy = SQLHelper<KPISessionModel>.FindByID(TextUtils.ToInt(cboKPISession.EditValue));
                Expression ex1 = new Expression("YearEvaluation", year);
                Expression ex2 = new Expression("QuarterEvaluation", quarter);
                Expression ex3 = new Expression("IsDeleted", 0);
                KPISessionModel session = SQLHelper<KPISessionModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPISessionModel();
                if (session.ID > 0)
                    if (MessageBox.Show($"Yếu tố đánh giá tại quý {quarter} năm {year} đã tồn tại! Bạn có muốn ghi đè dữ liệu không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;


                session.Code = code;
                session.Name = name;
                session.QuarterEvaluation = quarter;
                session.YearEvaluation = year;
                session.IsDeleted = false;
                session.DepartmentID = kpiDataCopy.DepartmentID;
                if (session.ID > 0) SQLHelper<KPISessionModel>.Update(session);
                else session.ID = SQLHelper<KPISessionModel>.Insert(session).ID;

                CopyData(session);
                CopyCriteria();
                this.DialogResult = DialogResult.OK;
            }
        }
        private void CopyData(KPISessionModel session)
        {


            //Get các phần tử cũ!
            Expression ex3 = new Expression("KPISessionID", session.ID);
            Expression ex4 = new Expression("IsDeleted", 0);
            List<KPIExamModel> lstExamOld = SQLHelper<KPIExamModel>.FindByExpression(ex3.And(ex4));
            foreach (KPIExamModel item in lstExamOld)
            {
                item.IsDeleted = true;
                SQLHelper<KPIExamModel>.Update(item);
            }

            //Get các phần tử sao chép
            Expression ex1 = new Expression("KPISessionID", TextUtils.ToInt(cboKPISession.EditValue));
            Expression ex2 = new Expression("IsDeleted", 0);
            List<KPIExamModel> lstExam = SQLHelper<KPIExamModel>.FindByExpression(ex1.And(ex2));
            foreach (KPIExamModel item in lstExam)
            {
                KPIExamModel newKPIExam = new KPIExamModel();
                newKPIExam.KPISessionID = session.ID;
                newKPIExam.IsDeleted = false;
                newKPIExam.ExamCode = item.ExamCode;
                newKPIExam.ExamName = item.ExamName;

                newKPIExam.ID = SQLHelper<KPIExamModel>.Insert(newKPIExam).ID;

                List<KPIExamPositionModel> listPosition = SQLHelper<KPIExamPositionModel>.FindByAttribute("KPIExamID", item.ID);
                foreach (KPIExamPositionModel position in listPosition)
                {
                    KPIExamPositionModel newPosition = new KPIExamPositionModel();
                    newPosition.KPIExamID = newKPIExam.ID;
                    newPosition.KPIPositionID = position.KPIPositionID;

                    SQLHelper<KPIExamPositionModel>.Insert(newPosition);
                }


                //Get các phần tử sao chép!
                List<KPIEvaluationFactorsModel> lstDetails = SQLHelper<KPIEvaluationFactorsModel>.ProcedureToList("spGetAllKPIEvaluationByYearAndQuarter",
                                                                                new string[] { "@KPIExamID", "@EvaluationType" },
                                                                                new object[] { item.ID, 0 });

                foreach (KPIEvaluationFactorsModel detail in lstDetails)
                {
                    KPIEvaluationFactorsModel model = new KPIEvaluationFactorsModel();
                    model.KPIExamID = newKPIExam.ID;
                    model.STT = detail.STT;
                    model.EvaluationContent = detail.EvaluationContent;
                    model.VerificationToolsContent = detail.VerificationToolsContent;
                    model.StandardPoint = detail.StandardPoint;
                    model.Coefficient = detail.Coefficient;
                    model.Unit = detail.Unit;
                    model.IsDeleted = false;
                    model.EvaluationType = detail.EvaluationType;
                    model.SpecializationType = detail.SpecializationType;
                    model.ParentID = GetParentId(model.STT, newKPIExam.ID, TextUtils.ToInt(detail.EvaluationType));

                    model.ID = SQLHelper<KPIEvaluationFactorsModel>.Insert(model).ID;
                }
            };
        }

        int GetParentId(string stt, int examID, int evaluationType)
        {
            if (!stt.Contains(".")) return 0;

            string parentTt = stt.Substring(0, stt.LastIndexOf(".")).Trim();

            Expression ex1 = new Expression("STT", parentTt);
            Expression ex2 = new Expression("KPIExamID", examID);
            Expression ex3 = new Expression("EvaluationType", evaluationType);
            Expression ex4 = new Expression("IsDeleted", 0);

            KPIEvaluationFactorsModel parent = SQLHelper<KPIEvaluationFactorsModel>.FindByExpression(ex1.And(ex2.And(ex3.And(ex4)))).FirstOrDefault() ?? new KPIEvaluationFactorsModel();
            return parent.ID;
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            ChangeValueNameCode();
        }

        private void txtQuarter_ValueChanged(object sender, EventArgs e)
        {
            ChangeValueNameCode();
        }
        //============================================== lee min khooi update 28/09/2024 ==============================================
        private void CopyCriteria()
        {
            KPISessionModel session = SQLHelper<KPISessionModel>.FindByID(TextUtils.ToInt(cboKPISession.EditValue));
            if (session.ID <= 0) return;


            // Xoa du lieu cu
            Expression ex4 = new Expression(KPICriteriaModel_Enum.KPICriteriaQuater, txtQuarter.Value);
            Expression ex5 = new Expression(KPICriteriaModel_Enum.KPICriteriaYear, txtYear.Value);
            var mydict = new Dictionary<string, object>() {
                    { KPICriteriaModel_Enum.IsDeleted.ToString(), true },
                    {  KPICriteriaModel_Enum.UpdatedDate.ToString(), DateTime.Now },
                    {  KPICriteriaModel_Enum.UpdatedBy.ToString(), Global.LoginName }
                };
            SQLHelper<KPICriteriaModel>.UpdateFields(mydict, ex4.And(ex5));

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
    }
}
