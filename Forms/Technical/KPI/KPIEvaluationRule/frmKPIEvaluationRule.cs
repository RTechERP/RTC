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
    public partial class frmKPIEvaluationRule : _Forms
    {
        public int departmentID = 0;
        public string deName;
        public frmKPIEvaluationRule()
        {
            InitializeComponent();
        }

        private void frmKPIEvaluationRule_Load(object sender, EventArgs e)
        {
            this.Text += " - " + deName;
            LoadDepartMent();
            LoadPosition();
            txtYear.Value = DateTime.Now.Year;
            LoadSession();
            LoadDataRule();
        }

        private void LoadDepartMent()
        {
            List<DepartmentModel> lst = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboDepartMent.Properties.DataSource = lst;
            cboDepartMent.Properties.ValueMember = "ID";
            cboDepartMent.Properties.DisplayMember = "Name";

            cboDepartMent.EditValue = departmentID;
        }

        private void LoadPosition()
        {
            List<KPIPositionModel> lst = SQLHelper<KPIPositionModel>.FindByAttribute(KPIPositionModel_Enum.IsDeleted.ToString(), 0);
            cboPosition.DataSource = lst;
            cboPosition.ValueMember = "ID";
            cboPosition.DisplayMember = "PositionCode";
        }
        private void LoadSession()
        {
            Expression ex1 = new Expression("YearEvaluation", TextUtils.ToInt(txtYear.Value));
            Expression ex2 = new Expression("IsDeleted", 0);
            Expression ex3 = new Expression("DepartmentID", TextUtils.ToInt(cboDepartMent.EditValue)); // update 5225
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByExpression(ex1.And(ex2).And(ex3)).OrderByDescending(p => p.ID).ToList();
            grdMaster.DataSource = lst;

            int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;
            KPISessionModel currentSession = lst.FirstOrDefault(p=> p.YearEvaluation == txtYear.Value && p.QuarterEvaluation == currentQuarter) ?? new KPISessionModel();
            grvMaster.FocusedRowHandle = LoadCurrentSession(currentSession);
            LoadDataDetailsNew();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSession();
        }
        private int LoadCurrentSession(KPISessionModel currentSession)
        {
            if (currentSession.ID <= 0) return -1;
            for (int i = 0; i < grvMaster.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvMaster.GetRowCellValue(i, "ID"));
                if (ID <= 0) continue;
                if (ID == currentSession.ID)
                {
                   return i;
                }
            }
            return -1;
        }
        private void btAddSession_Click(object sender, EventArgs e)
        {
            int rowHandle = grvMaster.FocusedRowHandle;
            frmKPISessionDetails frm = new frmKPISessionDetails();
            frm.departmentID = departmentID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadSession();
                grvMaster.FocusedRowHandle = rowHandle;
            }
        }

        private void btnUpdateSession_Click(object sender, EventArgs e)
        {
            int rowHandle = grvMaster.FocusedRowHandle;
            int kpiSessionId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSessionID));
            frmKPISessionDetails frm = new frmKPISessionDetails();
            frm.kpiSession = SQLHelper<KPISessionModel>.FindByID(kpiSessionId);
            if (frm.kpiSession.ID <= 0) return;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadSession();
                grvMaster.FocusedRowHandle = rowHandle;
            }
        }

        private void btnDeleteSession_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSessionID));
            if (ID <= 0) return;
            if (MessageBox.Show($"Bạn có muốn xóa Kỳ đánh giá [{TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colSessionCode))}] hay không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                KPISessionModel model = SQLHelper<KPISessionModel>.FindByID(ID);
                model.IsDeleted = true;
                SQLHelper<KPISessionModel>.Update(model);
                LoadSession();
            }
        }
        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            btnUpdateSession_Click(null,null);
        }
        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataDetailsNew();
        }
        //private void LoadDataDetails()
        //{
        //    int kpiSessionID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
        //    Expression ex1 = new Expression("KPISessionID", kpiSessionID);
        //    Expression ex2 = new Expression("IsDeleted", 0);
        //    List<KPIEvaluationRuleModel> lst = SQLHelper<KPIEvaluationRuleModel>.FindByExpression(ex1.And(ex2));
        //    grdDetails.DataSource = lst;
        //    LoadDataRule();
        //}


        private void btnAddExam_Click(object sender, EventArgs e)
        {
            int kpiSessionID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            int yearEvaluation = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSesionYear));
            int quarterEvaluation = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSesionQuarter));
            if (kpiSessionID <= 0)
            {
                MessageBox.Show("Vui lòng chọn Kỳ đánh giá trước khi thêm Rule đánh giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmKPIEvaluationRuleDetails frm = new frmKPIEvaluationRuleDetails();
            frm.kpiSessionID = kpiSessionID;
            frm.year = yearEvaluation;
            frm.quarter = quarterEvaluation;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                int rowHandle = grvDetails.FocusedRowHandle;
                LoadDataDetailsNew();
                grvDetails.FocusedRowHandle = rowHandle;
            }
        }

        private void btnUpdateExam_Click(object sender, EventArgs e)
        {
            int kpiSessionID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            int yearEvaluation = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSesionYear));
            int quarterEvaluation = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSesionQuarter));
            int ruleID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue("ID"));
            KPIEvaluationRuleModel model = SQLHelper<KPIEvaluationRuleModel>.FindByID(ruleID);
            if (kpiSessionID <= 0)
            {
                MessageBox.Show("Vui lòng chọn Kỳ đánh giá trước khi thêm Rule đánh giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (model.ID <= 0) return;
            frmKPIEvaluationRuleDetails frm = new frmKPIEvaluationRuleDetails();
            frm.kpiSessionID = kpiSessionID;
            frm.year = yearEvaluation;
            frm.quarter = quarterEvaluation;
            frm.ruleModel = model;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                int rowHandle = grvDetails.FocusedRowHandle;
                LoadDataDetailsNew();
                grvDetails.FocusedRowHandle = rowHandle;
            }
        }

        private void btnDeleteExam_Click(object sender, EventArgs e)
        {
            int ruleID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue("ID"));
            string ruleCode = TextUtils.ToString(grvDetails.GetFocusedRowCellValue("RuleCode"));
            if (ruleID <= 0) return;
            if(MessageBox.Show($"Bạn có chắc chắn xóa Rule đánh giá [{ruleCode}] không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Dictionary<string, object> newDict = new Dictionary<string, object>()
                {
                    {"IsDeleted", 1},
                    {"UpdatedBy", Global.AppFullName},
                    {"UpdatedDate", DateTime.Now}
                };
                SQLHelper<KPIEvaluationRuleModel>.UpdateFieldsByID(newDict, ruleID);
                LoadDataDetailsNew();
            }
        }
        private void btnCopyExam_Click(object sender, EventArgs e)
        {
            int kpiSessionID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            int yearEvaluation = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSesionYear));
            int quarterEvaluation = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSesionQuarter));
            if (kpiSessionID <= 0)
            {
                MessageBox.Show("Vui lòng chọn Kỳ đánh giá trước khi thêm Rule đánh giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmKPIEvaluationRuleDetails frm = new frmKPIEvaluationRuleDetails();
            frm.kpiSessionID = kpiSessionID;
            frm.year = yearEvaluation;
            frm.quarter = quarterEvaluation;
            frm.ckbCopy.Checked = true;
            frm.cboKPIRule.EditValue = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue("ID"));

            if (frm.ShowDialog() == DialogResult.OK)
            {
                int rowHandle = grvDetails.FocusedRowHandle;
                LoadDataDetailsNew();
                grvDetails.FocusedRowHandle = rowHandle;
            }
        }
        private void grvDetails_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataRule();
        }
        private void grvDetails_DoubleClick(object sender, EventArgs e)
        {
            btnUpdateExam_Click(null, null);
        }

        private void LoadDataRule()
        {
            int ruleID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue("ID"));
            //Expression ex1 = new Expression("KPIEvaluationRuleID", ruleID);
            //Expression ex2 = new Expression("IsDeleted", 0);
            //List<KPIEvaluationRuleDetailModel> lst = SQLHelper<KPIEvaluationRuleDetailModel>.FindByExpression(ex1.And(ex2)).OrderBy(p=> p.STT).ToList();
            //treeData.DataSource = lst;

            DataTable dt = SQLHelper<KPIEvaluationRuleDetailModel>.LoadDataFromSP("spGetKPIEvaluationRuleDetail", new string[] { "@KPIEvaluationRuleID" }, new object[] { ruleID });
            treeData.DataSource = dt;
            treeData.ExpandAll();
        }
        private void btnAddRule_Click(object sender, EventArgs e)
        {
            int ruleID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue("ID"));
            if (ruleID <= 0) return;
            int parentID = 0;
            TreeListNode nodeFocus = treeData.FocusedNode;
            if(nodeFocus != null)
            {
                if (nodeFocus.HasChildren) parentID = TextUtils.ToInt(nodeFocus.GetValue("ID"));
                else if (nodeFocus.ParentNode != null) parentID = TextUtils.ToInt(nodeFocus.ParentNode.GetValue("ID"));
                if (ruleID <= 0)
                {
                    MessageBox.Show("Vui lòng chọn Rule đánh giá trước khi thêm mới!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            } 
            frmKPIRuleDetails frm = new frmKPIRuleDetails();
            frm.ruleID = ruleID;
            frm.ruleDetail.ParentID = parentID;
            frm.departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataRule();
                treeData.SetFocusedNode(nodeFocus);
            }
        }

        private void btnEditRule_Click(object sender, EventArgs e)
        {
            int ruleID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue("ID"));
            TreeListNode nodeFocus = treeData.FocusedNode;
            if (nodeFocus == null) return;
            int ruleDetailsID = TextUtils.ToInt(nodeFocus.GetValue("ID"));
            if (ruleID <= 0 || ruleDetailsID <= 0) return;
            frmKPIRuleDetails frm = new frmKPIRuleDetails();
            frm.ruleID = ruleID;
            frm.ruleDetail = SQLHelper<KPIEvaluationRuleDetailModel>.FindByID(ruleDetailsID);
            frm.departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            if (frm.ruleDetail.ID <= 0) return;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataRule();
                treeData.SetFocusedNode(nodeFocus);
            }
        }

        private void treeData_DoubleClick(object sender, EventArgs e)
        {
            btnEditRule_Click(null,null);
        }

        private void treeData_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                return;
            }
        }

        private void btnDeleteRule_Click(object sender, EventArgs e)
        {
            TreeListNode nodeFocus = treeData.FocusedNode;
            if (nodeFocus == null) return;
            int ruleDetailsID = TextUtils.ToInt(nodeFocus.GetValue("ID"));
            if (ruleDetailsID <= 0) return;

            if(MessageBox.Show($"Bạn có chắc muốn xóa Nội dung đánh giá thứ [{nodeFocus.GetValue("STT").ToString()}]",TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                KPIEvaluationRuleDetailModel ruleDetails = SQLHelper<KPIEvaluationRuleDetailModel>.FindByID(ruleDetailsID);
                ruleDetails.IsDeleted = true;
                SQLHelper<KPIEvaluationRuleDetailModel>.Update(ruleDetails);
                LoadDataRule();
            }
        }

        private void treeData_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            bool isStyle = e.Column == colMaxPercent || e.Column == colPercentageAdjustment || e.Column == colMaxPercentageAdjustment;
            if (isStyle)
            {
                if (TextUtils.ToDecimal(e.Value) == 0)
                {
                    e.DisplayText = "";
                }
            }
        }

        #region TN.Binh 
        //TN.Binh update 09/09/25
        private void LoadDataDetailsNew()
        {
            int kpiSessionID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            DataTable lst = SQLHelper<KPIEvaluationRuleModel>.LoadDataFromSP("spGetKPIEvaluationRule", new string[] { "@KPISession" }, new object[] { kpiSessionID });
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
            grdDetails.DataSource = lst;
            LoadDataRule();
        }
        #endregion
    }
}
