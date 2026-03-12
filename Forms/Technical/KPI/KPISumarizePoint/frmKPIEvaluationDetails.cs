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
    public partial class frmKPIEvaluationDetails : _Forms
    {
        public KPIEvaluationModel detail = new KPIEvaluationModel();
        List<int> errorIDs = new List<int>();

        public int deparmentID = 0;
        public frmKPIEvaluationDetails()
        {
            InitializeComponent();
        }

        private void frmKPIEvaluationRuleDetails_Load(object sender, EventArgs e)
        {
            LoadDetails();
            LoadError();
        }
        private void LoadError()
        {
            DataTable dt = SQLHelper<KPIErrorModel>.LoadDataFromSP("spGetErrorByEvaluation",
                                                                    new string[] { "@EvaluationID" },
                                                                    new object[] { detail.ID });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int poisitionID = TextUtils.ToInt(dt.Rows[i]["ID"]);
                bool isCheck = TextUtils.ToBoolean(dt.Rows[i]["IsCheck"]);
                if (isCheck) errorIDs.Add(poisitionID);
            }
            grdData.DataSource = dt;
        }
        private void LoadDetails()
        {
            txtEvaluationCode.Text = detail.EvaluationCode;
            txtNote.Text = detail.Note;
        }


        private bool CheckValidate()
        {

            if (string.IsNullOrWhiteSpace(txtEvaluationCode.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã nội dung", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            Expression ex1 = new Expression("EvaluationCode", txtEvaluationCode.Text.Trim());
            Expression ex2 = new Expression("ID", detail.ID, "<>");
            Expression ex3 = new Expression(KPIEvaluationModel_Enum.DepartmentID, deparmentID);
            List<KPIEvaluationModel> lst = SQLHelper<KPIEvaluationModel>.FindByExpression(ex1.And(ex2).And(ex3));
            if (lst.Count > 0)
            {
                MessageBox.Show($"Mã nội dung [{txtEvaluationCode.Text.Trim()}] đã được sử dụng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private bool SaveData()
        {
            if (!CheckValidate()) return false;
            KPIEvaluationModel model = SQLHelper<KPIEvaluationModel>.FindByID(detail.ID);
            model.EvaluationCode = txtEvaluationCode.Text.Trim();
            model.Note = txtNote.Text.Trim();
            model.IsDeleted = false;
            model.DepartmentID = deparmentID;
            if (model.ID > 0) SQLHelper<KPIEvaluationModel>.Update(model);
            else model.ID = SQLHelper<KPIEvaluationModel>.Insert(model).ID;

            SQLHelper<KPIEvaluationErrorModel>.DeleteByAttribute("KPIEvaluationID", model.ID);
            foreach (int errorId in errorIDs)
            {
                KPIEvaluationErrorModel newChild = new KPIEvaluationErrorModel()
                {
                    KPIErrorID = errorId,
                    KPIEvaluationID = model.ID
                };
                SQLHelper<KPIEvaluationErrorModel>.Insert(newChild);
            }
            return true;
        }

        private void Reset()
        {
            detail = new KPIEvaluationModel();
            LoadDetails();
            errorIDs = new List<int>();
            LoadError();
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


        private void repositoryItemCheckEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (TextUtils.ToBoolean(e.OldValue) == true)
            {
                bool isDelete = MessageBox.Show($"Bạn có muốn xóa Mã lỗi [{TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode))}] hay không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                if (isDelete)
                {
                    errorIDs.Remove(ID);
                }
                else e.Cancel = true;
            }
            else
            {
                bool isDuplicate = errorIDs.Any(p => p == ID);
                if (!isDuplicate) errorIDs.Add(ID);
            }
        }

        private void frmKPIEvaluationRuleDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
