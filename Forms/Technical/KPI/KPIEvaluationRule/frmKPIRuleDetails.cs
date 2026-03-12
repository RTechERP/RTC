using BMS.Model;
using BMS.Utils;
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
    public partial class frmKPIRuleDetails : _Forms
    {
        public int ruleID = 0;
        public KPIEvaluationRuleDetailModel ruleDetail = new KPIEvaluationRuleDetailModel();

        public int departmentID = 0;
        public frmKPIRuleDetails()
        {
            InitializeComponent();
        }

        private void frmKPIRuleDetails_Load(object sender, EventArgs e)
        {
            LoadKPIRuleDetails();
            LoadRuleCode();
            LoadData();
        }
        private void LoadData()
        {
            if (ruleDetail.ID > 0)
            {
                txtMaxPercent.EditValue = ruleDetail.MaxPercent;
                txtMaxPercentageAdjustment.EditValue = ruleDetail.MaxPercentageAdjustment;
                txtSTT.Text = ruleDetail.STT;
                cboRuleCode.EditValue = ruleDetail.KPIEvaluationID;
                txtFormulaCode.Text = ruleDetail.FormulaCode;
                txtPercentageAdjustment.EditValue = ruleDetail.PercentageAdjustment;
                cboParentGroup.EditValue = ruleDetail.ParentID;
                txtNote.Text = ruleDetail.Note;
                txtRuleContent.Text = ruleDetail.RuleContent;
                txtRule.Text = ruleDetail.RuleNote;
            }
            else
            {

                if (ruleDetail.ParentID <= 0)
                {
                    Expression ex1 = new Expression("KPIEvaluationRuleID", ruleID);
                    Expression ex2 = new Expression("ParentID", 0);
                    Expression ex3 = new Expression("IsDeleted", 0);
                    List<KPIEvaluationRuleDetailModel> lst = SQLHelper<KPIEvaluationRuleDetailModel>.FindByExpression(ex1.And(ex2).And(ex3));
                    int maxCount = lst.Count + 1;
                    txtSTT.Text = maxCount.ToString();
                }
                else cboParentGroup.EditValue = ruleDetail.ParentID;
            }
        }
        private void LoadKPIRuleDetails()
        {
            Expression ex1 = new Expression("ID", ruleDetail.ID, "<>");
            Expression ex2 = new Expression("KPIEvaluationRuleID", ruleID);
            Expression ex3 = new Expression("IsDeleted", 0);
            List<KPIEvaluationRuleDetailModel> lst = SQLHelper<KPIEvaluationRuleDetailModel>.FindByExpression(ex1.And(ex2).And(ex3));
            lst.Add(new KPIEvaluationRuleDetailModel() { ID = 0, STT = "Không có nhóm cha", RuleContent = "Không có nhóm cha", ParentID = -1 });
            cboParentGroup.Properties.DataSource = lst;
            cboParentGroup.Properties.ValueMember = "ID";
            cboParentGroup.Properties.DisplayMember = "STT";
        }
        private void LoadRuleCode()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetKPIEvaluation", "LMKTable", new string[] { "@DepartmentID" }, new object[] { departmentID });
            cboRuleCode.Properties.DataSource = dt;
            cboRuleCode.Properties.ValueMember = "ID";
            cboRuleCode.Properties.DisplayMember = "EvaluationCode";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadKPIRuleDetails();
        }

        private bool CheckValidate()
        {
            if (string.IsNullOrWhiteSpace(txtSTT.Text))
            {
                MessageBox.Show("Vui lòng nhập [STT]", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string stt = txtSTT.Text.Trim();
            int parentId = TextUtils.ToInt(cboParentGroup.EditValue);
            if (parentId > 0)
            {
                KPIEvaluationRuleDetailModel parentKPI = SQLHelper<KPIEvaluationRuleDetailModel>.FindByID(parentId);
                List<KPIEvaluationRuleDetailModel> lst = SQLHelper<KPIEvaluationRuleDetailModel>.FindByAttribute("ParentID", parentId);
                bool isDuplicateSTT = lst.Any(p => p.IsDeleted == false
                                                && p.ID != ruleDetail.ID
                                                && p.STT == stt);
                if (isDuplicateSTT)
                {
                    MessageBox.Show($"[STT] trong Nhóm cha [{parentKPI.STT}] đã tồn tại! Vui lòng nhập lại [STT] khác.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtRuleContent.Text))
            {
                MessageBox.Show("Vui lòng nhập [Nội dung đánh giá]", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void cboParentGroup_EditValueChanged(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(cboParentGroup.EditValue);
            if(ruleDetail.ID > 0)
            {

            }
            else if (ID > 0)
            {
                KPIEvaluationRuleDetailModel model = SQLHelper<KPIEvaluationRuleDetailModel>.FindByID(ID);
                Expression ex1 = new Expression("ParentID", ID);
                Expression ex2 = new Expression("IsDeleted", 0);
                List<KPIEvaluationRuleDetailModel> lst = SQLHelper<KPIEvaluationRuleDetailModel>.FindByExpression(ex1.And(ex2));
                string _str = model.STT + "." + TextUtils.ToString(lst.Count + 1);
                txtSTT.Text = _str;
            }
            else
            {
                Expression ex2 = new Expression("KPIEvaluationRuleID", ruleID);
                Expression ex3 = new Expression("IsDeleted", 0);
                Expression ex4 = new Expression("ParentID", 0);
                List<KPIEvaluationRuleDetailModel> lst = SQLHelper<KPIEvaluationRuleDetailModel>.FindByExpression(ex2.And(ex3).And(ex4));
                string _str = TextUtils.ToString(lst.Count + 1);
                txtSTT.Text = _str;
            }
        }
        private void Reset()
        {
            int parentID = TextUtils.ToInt(cboParentGroup.EditValue);
            LoadKPIRuleDetails();
            cboParentGroup_EditValueChanged(null,null);
            cboRuleCode.Reset();
            txtFormulaCode.Reset();
            txtMaxPercent.Reset();
            txtPercentageAdjustment.Reset();
            txtMaxPercentageAdjustment.Reset();
            txtRule.Clear();
            txtNote.Clear();
            txtRuleContent.Clear();
        }

        private bool SaveData()
        {
            if (!CheckValidate()) return false;

            KPIEvaluationRuleDetailModel model = SQLHelper<KPIEvaluationRuleDetailModel>.FindByID(ruleDetail.ID);
            model.STT = txtSTT.Text.Trim();
            model.KPIEvaluationID = TextUtils.ToInt(cboRuleCode.EditValue);
            model.KPIEvaluationRuleID = ruleID;
            model.ParentID = TextUtils.ToInt(cboParentGroup.EditValue);
            model.RuleContent = txtRuleContent.Text.Trim();
            model.FormulaCode = txtFormulaCode.Text.Trim();
            model.MaxPercent = TextUtils.ToDecimal(txtMaxPercent.EditValue);
            model.PercentageAdjustment = TextUtils.ToDecimal(txtPercentageAdjustment.EditValue);
            model.MaxPercentageAdjustment = TextUtils.ToDecimal(txtMaxPercentageAdjustment.EditValue);
            model.RuleNote = txtRule.Text.Trim();
            model.Note = txtNote.Text.Trim();
            model.IsDeleted = false;
            if (model.ID > 0) SQLHelper<KPIEvaluationRuleDetailModel>.Update(model);
            else SQLHelper<KPIEvaluationRuleDetailModel>.Insert(model);

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                Reset();
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmKPIRuleDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void treeListLookUpEdit1TreeList_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                return;
            }
        }

        private void frmKPIRuleDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
        }
    }
}
