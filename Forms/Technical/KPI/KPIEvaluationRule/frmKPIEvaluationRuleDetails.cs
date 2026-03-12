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
    public partial class frmKPIEvaluationRuleDetails : _Forms
    {
        public int kpiSessionID = 0;
        public int year = 0;
        public int quarter = 0;
        public KPIEvaluationRuleModel ruleModel = new KPIEvaluationRuleModel();
        public frmKPIEvaluationRuleDetails()
        {
            InitializeComponent();
        }

        private void frmKPIEvaluationRuleDetails_Load(object sender, EventArgs e)
        {
            LoadKPISession();
            LoadKPIRule();
            LoadPosition();
            LoadDataDetails();
        }
        private void LoadDataDetails()
        {
            txtCode.Text = ruleModel.RuleCode;
            txtName.Text = ruleModel.RuleName;
            cboPosition.EditValue = ruleModel.KPIPositionID;
        }
        private void LoadPosition()
        {
            //List<KPIPositionModel> lst = SQLHelper<KPIPositionModel>.FindByAttribute(KPIPositionModel_Enum.IsDeleted.ToString(), 0);
            //cboPosition.Properties.DataSource = lst;
            //cboPosition.Properties.ValueMember = "ID";
            //cboPosition.Properties.DisplayMember = "PositionCode";

            if (TextUtils.ToInt(cboSession.EditValue)  > 0)
            {
                Expression ex1 = new Expression("KPISessionID", TextUtils.ToInt(cboSession.EditValue));
                Expression ex2 = new Expression("IsDeleted", 0);
                List<KPIPositionModel> lst = SQLHelper<KPIPositionModel>.FindByExpression(ex1.And(ex2));
                cboPosition.Properties.DataSource = lst;
                cboPosition.Properties.ValueMember = "ID";
                cboPosition.Properties.DisplayMember = "PositionCode";
            }
        }
        private void LoadKPISession()
        {
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByAttribute("IsDeleted", 0);
            cboSession.Properties.DataSource = lst;
            cboSession.Properties.ValueMember = "ID";
            cboSession.Properties.DisplayMember = "Code";
            cboSession.EditValue = kpiSessionID;


            cboSessionCopy.Properties.DataSource = lst;
            cboSessionCopy.Properties.ValueMember = "ID";
            cboSessionCopy.Properties.DisplayMember = "Code";
            cboSessionCopy.EditValue = kpiSessionID;
        }
        private void cboSessionCopy_EditValueChanged(object sender, EventArgs e)
        {
            LoadKPIRule();
        }
        private void LoadKPIRule()
        {
            Expression ex1 = new Expression("KPISessionID", TextUtils.ToInt(cboSessionCopy.EditValue));
            Expression ex2 = new Expression("IsDeleted", 0);
            List<KPIEvaluationRuleModel> lst = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2));
            cboKPIRule.Properties.DataSource = lst;
            cboKPIRule.Properties.ValueMember = "ID";
            cboKPIRule.Properties.DisplayMember = "RuleCode";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            grCopy.Enabled = ckbCopy.Checked;
            cboSessionCopy.Enabled = ckbCopy.Checked;
        }
        private bool CheckValidate()
        {
            int ruleCopyID = TextUtils.ToInt(cboKPIRule.EditValue);
            if (ckbCopy.Checked)
            {
                if (ruleCopyID <= 0)
                {
                    MessageBox.Show("Vui lòng chọn Rule đánh giá để copy!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (TextUtils.ToInt(cboSession.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Kỳ đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Rule đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Rule đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            if (TextUtils.ToInt(cboPosition.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Vị trí!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            Expression ex1 = new Expression("KPISessionID", TextUtils.ToInt(cboSession.EditValue));
            Expression ex2 = new Expression("IsDeleted", 0);
            Expression ex3 = new Expression("KPIPositionID", TextUtils.ToInt(cboPosition.EditValue));
            Expression ex4 = new Expression("ID", ruleModel.ID, "<>");

            List<KPIEvaluationRuleModel> lst = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2).And(ex3).And(ex4));

            if (lst.Count > 0)
            {
                if (ckbCopy.Checked)
                {
                    if (MessageBox.Show($"Trong kỳ [{cboSession.Text}] đã có Rule đánh giá với vị trí [{cboPosition.Text}]! \n Bạn có muốn ghi đè dữ liệu không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return false;
                }
                else
                {
                    MessageBox.Show($"Trong kỳ [{cboSession.Text}] đã có Rule đánh giá với vị trí [{cboPosition.Text}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
        private bool SaveData()
        {
            if (!CheckValidate()) return false;
            KPIEvaluationRuleModel model = SQLHelper<KPIEvaluationRuleModel>.FindByID(ruleModel.ID);
            model.KPISessionID = TextUtils.ToInt(cboSession.EditValue);
            model.KPIPositionID = TextUtils.ToInt(cboPosition.EditValue);
            model.RuleCode = txtCode.Text.Trim();
            model.RuleName = txtName.Text.Trim();

            if (model.ID > 0) SQLHelper<KPIEvaluationRuleModel>.Update(model);
            else model.ID = SQLHelper<KPIEvaluationRuleModel>.Insert(model).ID;
            return true;
        }
        private bool CopyData()
        {
            if (!Validate()) return false;

            Expression ex3 = new Expression("KPIEvaluationRuleID", TextUtils.ToInt(cboKPIRule.EditValue));
            Expression ex4 = new Expression("IsDeleted", 0);
            List<KPIEvaluationRuleDetailModel> lstDetails = SQLHelper<KPIEvaluationRuleDetailModel>.FindByExpression(ex3.And(ex4)).OrderBy(p => p.STT).ToList();

            Expression ex1 = new Expression("KPISessionID", kpiSessionID);
            Expression ex2 = new Expression("KPIPositionID", TextUtils.ToInt(cboPosition.EditValue));
            Dictionary<string, object> newDict = new Dictionary<string, object>()
            {
                {"IsDeleted", 1 },
                {"UpdatedBy", Global.AppFullName },
                {"UpdatedDate", DateTime.Now }
            };
            SQLHelper<KPIEvaluationRuleModel>.UpdateFields(newDict, ex1.And(ex2));

            KPIEvaluationRuleModel model = new KPIEvaluationRuleModel();
            model.KPISessionID = TextUtils.ToInt(cboSession.EditValue);
            model.KPIPositionID = TextUtils.ToInt(cboPosition.EditValue);
            model.RuleCode = txtCode.Text.Trim();
            model.RuleName = txtName.Text.Trim();
            model.ID = SQLHelper<KPIEvaluationRuleModel>.Insert(model).ID;

           
            foreach (KPIEvaluationRuleDetailModel item in lstDetails)
            {
                item.ID = 0;
                item.KPIEvaluationRuleID = model.ID;
                if(item.ParentID != 0)
                {
                    int indexST = item.STT.LastIndexOf(".");
                    string parentStt = item.STT.Substring(0, indexST);
                    KPIEvaluationRuleDetailModel parentModel = lstDetails.FirstOrDefault(p=> p.STT == parentStt) ?? new KPIEvaluationRuleDetailModel();
                    item.ParentID = parentModel.ID;
                }
                item.ID = SQLHelper<KPIEvaluationRuleDetailModel>.Insert(item).ID;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isSuccess = ckbCopy.Checked ? CopyData() : SaveData();
            if (isSuccess)
            {
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

        private void Reset()
        {
            ruleModel = new KPIEvaluationRuleModel();
            LoadDataDetails();


        }

        private void frmKPIEvaluationRuleDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }


        private void ChangeRuleCode()
        {
            KPISessionModel sessionKPI = (KPISessionModel)cboSession.GetSelectedDataRow();
            KPIPositionModel kpiPosition = (KPIPositionModel)cboPosition.GetSelectedDataRow();
            if (sessionKPI == null || kpiPosition == null) return;

            txtCode.Text = $"KPIRule_{kpiPosition.PositionCode}_{sessionKPI.YearEvaluation}_Q{sessionKPI.QuarterEvaluation}";
            txtName.Text = $"Đánh giá KPI Rule {kpiPosition.PositionName} Q{sessionKPI.QuarterEvaluation}-{sessionKPI.YearEvaluation}";
        }
        private void cboSession_EditValueChanged(object sender, EventArgs e)
        {
            LoadPosition(); //TN.Binh update 08/09/25
            ChangeRuleCode();
        }

        private void cboPosition_EditValueChanged(object sender, EventArgs e)
        {
            ChangeRuleCode();
        }

        
    }
}
