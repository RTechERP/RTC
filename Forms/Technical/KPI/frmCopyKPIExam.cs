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
    public partial class frmCopyKPIExam : _Forms
    {
        public int kpiSessionID, kpiSessionDetailId;
        public string nameExam;

        public int departmentID = 0;
        public frmCopyKPIExam()
        {
            InitializeComponent();
        }
        private void frmCopyKPIExam_Load(object sender, EventArgs e)
        {
            LoadKPISession();
            
        }
        private void LoadKPISession()
        {
            var exp1 = new Expression(KPISessionModel_Enum.IsDeleted, 0);
            var exp2 = new Expression(KPISessionModel_Enum.DepartmentID, departmentID);
            //List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(p => p.ID).ToList();
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByExpression(exp1.And(exp2)).OrderByDescending(p => p.ID).ToList();
            cboKPISession.Properties.DataSource = lst;
            cboKPISession.Properties.DisplayMember = "Code";
            cboKPISession.Properties.ValueMember = "ID";
            cboKPISession.EditValue = kpiSessionID;



            cboKPISessionCopy.Properties.DataSource = lst;
            cboKPISessionCopy.Properties.DisplayMember = "Code";
            cboKPISessionCopy.Properties.ValueMember = "ID";
            cboKPISessionCopy.EditValue = kpiSessionID;


            LoadKPIExam();
            LoadKPIExamCopy();
        }
        private void LoadKPIExam()
        {
            KPISessionModel kpiSession = (KPISessionModel)cboKPISession.GetSelectedDataRow() ?? new KPISessionModel();

            DataTable lst = SQLHelper<KPIExamModel>.LoadDataFromSP("spGetKPIExamByKPISessionID", new string[] { "@KPISessionID", "@DepartmentID" }, 
                new object[] { TextUtils.ToInt(cboKPISession.EditValue), kpiSession.DepartmentID });
            cboExam.Properties.DisplayMember = "ExamName";
            cboExam.Properties.ValueMember = "ID";
            cboExam.Properties.DataSource = lst;
            cboExam.EditValue = -1;
        }
        private void LoadKPIExamCopy()
        {
            KPISessionModel kpiSession = (KPISessionModel)cboKPISession.GetSelectedDataRow() ?? new KPISessionModel();

            DataTable lst = SQLHelper<KPIExamModel>.LoadDataFromSP("spGetKPIExamByKPISessionID", new string[] { "@KPISessionID", "@DepartmentID" }, 
                new object[] { TextUtils.ToInt(cboKPISessionCopy.EditValue) , kpiSession.DepartmentID });
            cboExamCopy.Properties.DisplayMember = "ExamName";
            cboExamCopy.Properties.ValueMember = "ID";
            cboExamCopy.Properties.DataSource = lst;
            cboExamCopy.EditValue = kpiSessionDetailId;
        }
        private void cboKPISession_EditValueChanged(object sender, EventArgs e)
        {
            LoadKPIExam();
        }

        private void cboKPISessionCopy_EditValueChanged(object sender, EventArgs e)
        {
            LoadKPIExamCopy();
        }
        private void btnCopy_Click(object sender, EventArgs e)
        {
            int cboExamValue = TextUtils.ToInt(cboExam.EditValue);
            
            int cboExamCopyValue = TextUtils.ToInt(cboExamCopy.EditValue);

            if (cboExamValue <= 0)
            {
                MessageBox.Show("Vui lòng chọn bài thi muốn sao chép tới!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (cboExamCopyValue <= 0)
            {
                MessageBox.Show("Vui lòng chọn bài thi muốn sao chép!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (cboExamValue == cboExamCopyValue)
            {
                MessageBox.Show("Không thể copy trùng bài thi!", "Thông báo", MessageBoxButtons.OK);
                return;
            }


            bool isCopy = false;
            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                List<KPIEvaluationFactorsModel> lstDetailsOld = SQLHelper<KPIEvaluationFactorsModel>.ProcedureToList("spGetAllKPIEvaluationByYearAndQuarter",
                                                                                new string[] { "@KPIExamID", "@EvaluationType" },
                                                                                new object[] { cboExamCopyValue, 0 }).OrderBy(p=> p.STT).ToList();

                List<KPIEvaluationFactorsModel> lstDetailsNew = SQLHelper<KPIEvaluationFactorsModel>.ProcedureToList("spGetAllKPIEvaluationByYearAndQuarter",
                                                                                new string[] { "@KPIExamID", "@EvaluationType" },
                                                                                new object[] { cboExamValue, 0 }).OrderBy(p => p.STT).ToList();

                if(lstDetailsNew.Count > 0)
                {
                    if(MessageBox.Show($"Tiêu chí đánh giá bài {cboExam.Text} đã tồn tại! Bạn có muốn ghi đè dữ liệu không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    foreach(KPIEvaluationFactorsModel item in lstDetailsNew)
                    {
                        item.IsDeleted = true;
                        SQLHelper<KPIEvaluationFactorsModel>.Update(item);
                    }
                    
                }
                else 
                {
                    if (MessageBox.Show($"Bạn có chắc chắn muốn sao chép dữ liệu bài {nameExam} sang {cboExam.Text} không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    isCopy = true;
                }

                foreach (KPIEvaluationFactorsModel detail in lstDetailsOld)
                {
                    KPIEvaluationFactorsModel model = new KPIEvaluationFactorsModel();
                    model.KPIExamID = cboExamValue;
                    model.STT = detail.STT;
                    model.EvaluationContent = detail.EvaluationContent;
                    model.VerificationToolsContent = detail.VerificationToolsContent;
                    model.StandardPoint = detail.StandardPoint;
                    model.Coefficient = detail.Coefficient;
                    model.Unit = detail.Unit;
                    model.IsDeleted = false;
                    model.EvaluationType = detail.EvaluationType;
                    model.SpecializationType = detail.SpecializationType;
                    model.ParentID = GetParentId(model.STT, cboExamValue, TextUtils.ToInt(detail.EvaluationType));

                    model.ID = SQLHelper<KPIEvaluationFactorsModel>.Insert(model).ID;
                }
            }
            if (isCopy == true) MessageBox.Show("Dữ liệu đã được sao chép", "Thông báo", MessageBoxButtons.OK);
            else MessageBox.Show("Dữ liệu đã được ghi đè", "Thông báo", MessageBoxButtons.OK);
            this.DialogResult = DialogResult.OK;
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

    }
}
