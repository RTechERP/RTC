using BMS.Model;
using BMS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmKPIEvaluationFactorsDetails : _Forms
    {
        public KPIEvaluationFactorsModel kpiEvaluationFactors = new KPIEvaluationFactorsModel();
        public KPIExamModel kpiExam = new KPIExamModel();
        public int YearEvaluation = 0;
        public int QuaterEvaluation = 0;
        public int EValuationType = 1;
        public decimal parentStandartPoint = 0;

        public int departmentID = 0;

        public frmKPIEvaluationFactorsDetails()
        {
            InitializeComponent();
        }
        private void frmKPIEvaluationFactorsDetails_Load(object sender, EventArgs e)
        {
            LoadSpecializationType();
            LoadParentGroup();
            LoadEValuationType();
            LoadData();
        }
        private void LoadData()
        {
            if (kpiEvaluationFactors.ID > 0)
            {
                txtCoefficient.Value = TextUtils.ToDecimal(kpiEvaluationFactors.Coefficient);
                txtEvaluationContent.Text = kpiEvaluationFactors.EvaluationContent;
                txtVerificationToolsContent.Text = kpiEvaluationFactors.VerificationToolsContent;
                txtStandardPoint.Value = TextUtils.ToDecimal(kpiEvaluationFactors.StandardPoint);
                txtUnit.Text = kpiEvaluationFactors.Unit;
                cboParentGroup.EditValue = kpiEvaluationFactors.ParentID;
                txtSTT.Text = kpiEvaluationFactors.STT;
                cboEValuationType.EditValue = kpiEvaluationFactors.EvaluationType;
                cboSpecializationType.EditValue = kpiEvaluationFactors.SpecializationType;

            }
            else
            {
                txtSTT.Text = kpiEvaluationFactors.STT;
                if (kpiEvaluationFactors.ParentID <= 0)
                {
                    Expression ex2 = new Expression("KPIExamID", kpiExam.ID);
                    Expression ex3 = new Expression("IsDeleted", 0);
                    Expression ex4 = new Expression("ParentID", 0);
                    Expression ex5 = new Expression("EvaluationType", EValuationType);

                    List<KPIEvaluationFactorsModel> lst = SQLHelper<KPIEvaluationFactorsModel>.FindByExpression(ex2.And(ex3.And(ex4.And(ex5))));
                    string _str = TextUtils.ToString(lst.Count + 1);
                    txtSTT.Text = _str;
                    cboParentGroup.EditValue = 0;
                    cboSpecializationType.EditValue = 0;

                }
                else
                {
                    if (departmentID != 9) txtStandardPoint.Value = TextUtils.ToDecimal(kpiEvaluationFactors.StandardPoint);
                    cboParentGroup.EditValue = kpiEvaluationFactors.ParentID;
                    cboSpecializationType.EditValue = kpiEvaluationFactors.SpecializationType;
                }
                cboEValuationType.EditValue = EValuationType;
            }
        }
        private void LoadSpecializationType()
        {
            //List<object> lst = new List<object>()
            //{
            //    //new {ID = 0, SpecializationType = "--Chọn loại chuyên môn--"},
            //    new {ID = 1, SpecializationType = "Kỹ năng"},
            //    //new {ID = 0, SpecializationType = "Chuyên môn"},
            //    new { ID = 2, SpecializationType = "PLC, ROBOT" },
            //    new { ID = 3, SpecializationType = "VISION" },
            //    new { ID = 4, SpecializationType = "SOFTWARE" },
            //    new { ID = 5, SpecializationType = "VISION-ROBOT" },
            //    new {ID = 6, SpecializationType = "Đánh giá chung"},

            //};

            var exp1 = new Expression(KPISpecializationTypeModel_Enum.DepartmentID, departmentID);
            var exp2 = new Expression(KPISpecializationTypeModel_Enum.IsDeleted, 0);
            List<KPISpecializationTypeModel> lst = SQLHelper<KPISpecializationTypeModel>.FindByExpression(exp1.And(exp2)).OrderBy(x => x.STT).ToList();

            cboSpecializationType.Properties.DataSource = lst;
            cboSpecializationType.Properties.ValueMember = "ID";
            cboSpecializationType.Properties.DisplayMember = "Name";
        }
        private void LoadEValuationType()
        {
            List<object> lst = new List<object>()
            {
                new {ID = 0, EValuationType = "---Chọn yếu tố ---"},
                new {ID = 1, EValuationType = "Đánh giá kỹ năng"},
                new {ID = 2, EValuationType = "Chuyên môn"},
                new {ID = 3, EValuationType = "Đánh giá chung"}
            };
            cboEValuationType.Properties.DataSource = lst;
            cboEValuationType.Properties.ValueMember = "ID";
            cboEValuationType.Properties.DisplayMember = "EValuationType";
        }
        private void LoadParentGroup()
        {
            int EvaluationType = TextUtils.ToInt(cboEValuationType.EditValue);
            DataTable lst = SQLHelper<KPIEvaluationFactorsModel>.LoadDataFromSP("spGetAllKPIEvaluationByYearAndQuarter",
                                                                                new string[] { "@KPIExamID", "@EvaluationType", "@ID" },
                                                                                new object[] { kpiExam.ID, EvaluationType, kpiEvaluationFactors.ID });
            DataRow newRow = lst.NewRow();
            newRow["STT"] = "Không có nhóm cha";
            newRow["ID"] = 0;
            newRow["EvaluationContent"] = "";
            newRow["EvaluationDetails"] = "Không có nhóm cha";
            lst.Rows.InsertAt(newRow, 0);

            cboParentGroup.Properties.DataSource = lst;
            cboParentGroup.Properties.ValueMember = "ID";
            cboParentGroup.Properties.DisplayMember = "EvaluationDetails";
        }


        private void cboParentGroup_EditValueChanged(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(cboParentGroup.EditValue);
            if (ID > 0)
            {
                label6.Visible = false;
                KPIEvaluationFactorsModel model = SQLHelper<KPIEvaluationFactorsModel>.FindByID(ID);
                cboSpecializationType.EditValue = model.SpecializationType;
                if (departmentID == 2) txtStandardPoint.Value = TextUtils.ToDecimal(model.StandardPoint);
                parentStandartPoint = TextUtils.ToDecimal(model.StandardPoint);
                Expression ex1 = new Expression("ParentID", ID);
                Expression ex2 = new Expression("IsDeleted", 0);
                List<KPIEvaluationFactorsModel> lst = SQLHelper<KPIEvaluationFactorsModel>.FindByExpression(ex1.And(ex2));
                string _str = model.STT + "." + TextUtils.ToString(lst.Count + 1);
                txtSTT.Text = _str;
            }
            else
            {
                label6.Visible = true;
                Expression ex2 = new Expression("KPIExamID", kpiExam.ID);
                Expression ex3 = new Expression("IsDeleted", 0);
                Expression ex4 = new Expression("ParentID", 0);
                Expression ex5 = new Expression("EvaluationType", EValuationType);
                List<KPIEvaluationFactorsModel> lst = SQLHelper<KPIEvaluationFactorsModel>.FindByExpression(ex2.And(ex3.And(ex4.And(ex5))));
                string _str = TextUtils.ToString(lst.Count + 1);
                txtSTT.Text = _str;
            }
        }
        private bool SaveData()
        {
            if (!CheckValidate()) return false;

            KPIEvaluationFactorsModel newModel = SQLHelper<KPIEvaluationFactorsModel>.FindByID(kpiEvaluationFactors.ID);
            newModel.STT = txtSTT.Text.Trim();
            newModel.KPIExamID = kpiExam.ID;
            newModel.EvaluationContent = txtEvaluationContent.Text.Trim();
            newModel.VerificationToolsContent = txtVerificationToolsContent.Text.Trim();
            newModel.StandardPoint = TextUtils.ToInt(txtStandardPoint.Value);
            newModel.Coefficient = TextUtils.ToInt(txtCoefficient.Value);
            newModel.ParentID = TextUtils.ToInt(cboParentGroup.EditValue);
            newModel.Unit = txtUnit.Text.Trim();
            newModel.EvaluationType = TextUtils.ToInt(cboEValuationType.EditValue);
            newModel.SpecializationType = TextUtils.ToInt(cboSpecializationType.EditValue);
            newModel.IsDeleted = false;

            if (newModel.ID > 0)
            {
                SQLHelper<KPIEvaluationFactorsModel>.Update(newModel);
            }
            else
            {
                newModel.ID = SQLHelper<KPIEvaluationFactorsModel>.Insert(newModel).ID;
            }
            return true;
        }
        private bool CheckValidate()
        {
            if (string.IsNullOrWhiteSpace(txtSTT.Text))
            {
                MessageBox.Show("Vui lòng nhập [STT]", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                Regex regex = new Regex(@"^\d+(\.\d+)*$");

                if (!regex.IsMatch(txtSTT.Text.Trim()))
                {
                    MessageBox.Show("STT chỉ được nhập số và dấu chấm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                    //txtCodeDocument.Text = string.Empty;  // hoặc cắt ký tự sai: input.Remove(input.Length - 1)
                }
            }

            string stt = txtSTT.Text.Trim();
            int parentId = TextUtils.ToInt(cboParentGroup.EditValue);
            if (parentId > 0)
            {
                KPIEvaluationFactorsModel parentKPI = SQLHelper<KPIEvaluationFactorsModel>.FindByID(parentId);
                List<KPIEvaluationFactorsModel> lst = SQLHelper<KPIEvaluationFactorsModel>.FindByAttribute("ParentID", parentId);
                bool isDuplicateSTT = lst.Any(p => p.IsDeleted == false
                                                && p.ID != kpiEvaluationFactors.ID
                                                && p.STT == stt);
                //&& p.EvaluationType == TextUtils.ToInt(cboEValuationType.EditValue)
                if (isDuplicateSTT)
                {
                    MessageBox.Show($"[STT] trong Nhóm cha [{parentKPI.STT}] đã tồn tại! Vui lòng nhập lại [STT] khác.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (parentKPI.ID > 0 && departmentID != 9)
                {
                    if (parentKPI.StandardPoint < TextUtils.ToInt(txtStandardPoint.Value))
                    {
                        MessageBox.Show($"Điểm chuẩn phải <= điểm chuẩn của nhóm Cha [{parentKPI.STT}]! Vui lòng nhập lại Điểm chuẩn khác.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            else
            {
                if (TextUtils.ToInt(txtStandardPoint.Value) <= 0)
                {
                    MessageBox.Show("Vui lòng nhập [Điểm chuẩn]", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (TextUtils.ToInt(cboEValuationType.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn [Loại yếu tố]", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (TextUtils.ToInt(cboSpecializationType.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn [Loại chuyên môn]", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEvaluationContent.Text))
            {
                MessageBox.Show("Vui lòng nhập [Yếu tố đánh giá]", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //if (string.IsNullOrWhiteSpace(txtVerificationToolsContent.Text))
            //{
            //    MessageBox.Show("Vui lòng nhập [Phương tiện xác minh tiêu chí]", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            return true;
        }
        private void Reset()
        {
            kpiEvaluationFactors.ID = 0;
            cboParentGroup_EditValueChanged(null, null);
            txtEvaluationContent.Text = "";
            txtVerificationToolsContent.Text = "";
            LoadParentGroup();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                Reset();
            }
        }

        private void cboEValuationType_EditValueChanged(object sender, EventArgs e)
        {
            LoadParentGroup();
        }

        private void cboPositionType_EditValueChanged(object sender, EventArgs e)
        {
            LoadParentGroup();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadParentGroup();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmKPICriteriaView frm = new frmKPICriteriaView();
            frm.criteriaYear = TextUtils.ToInt(YearEvaluation);
            frm.criteriaQuarter = TextUtils.ToInt(QuaterEvaluation);
            frm.Show();
        }

        private void frmKPIEvaluationFactorsDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void frmKPIEvaluationFactorsDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSaveNew_Click(null, null);
            }
        }
    }
}
