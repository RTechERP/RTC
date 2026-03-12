using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
    public partial class frmKPIExam : _Forms
    {
        List<int> positionIDs = new List<int>();
        int _kpiSessionId = 0;
        public KPIExamModel kpiExam = new KPIExamModel();
        public frmKPIExam(int kpiSessionId)
        {
            InitializeComponent();
            _kpiSessionId = kpiSessionId;
        }

        private void frmKPIExam_Load(object sender, EventArgs e)
        {
            LoadDetails();
            LoadSession();
            LoadDataPosition();
        }
        private void LoadDataPosition()
        {
            DataTable dt = SQLHelper<KPIPositionModel>.LoadDataFromSP("spGetKPIPositionByExamID",
                new string[] { "@KPIExamID", "@KPISessionID" },
                new object[] { kpiExam.ID, _kpiSessionId });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int poisitionID = TextUtils.ToInt(dt.Rows[i]["ID"]);
                bool isCheck = TextUtils.ToBoolean(dt.Rows[i]["IsCheck"]);
                if (isCheck) positionIDs.Add(poisitionID);
            }
            grdData.DataSource = dt;
        }
        private void LoadDetails()
        {
            cboSession.EditValue = _kpiSessionId;
            if (kpiExam.ID > 0)
            {
                ckbIsActive.Checked = TextUtils.ToBoolean(kpiExam.IsActive);
                dtpDateDeadline.Value = TextUtils.ToDate5(kpiExam.Deadline);
                txtExamCode.Text = kpiExam.ExamCode;
                txtExamName.Text = kpiExam.ExamName;
                cboSession.EditValue = kpiExam.KPISessionID;
            }
        }
        private void LoadSession()
        {
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByAttribute("IsDeleted", 0);
            cboSession.Properties.DataSource = lst;
            cboSession.Properties.DisplayMember = "Name";
            cboSession.Properties.ValueMember = "ID";
        }

        private bool CheckValidate()
        {
            if (string.IsNullOrWhiteSpace(txtExamCode.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã bài đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtExamName.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên bài đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (TextUtils.ToInt(cboSession.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Kỳ đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //if (dtpDateDeadline.Value <= DateTime.Now)
            //{
            //    MessageBox.Show("Vui lòng nhập Hạn làm bài lớn hơn ngày hiện tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            grvData.FocusedRowHandle = -1;
            int count = 0;
            for (int i = 0; i < grvData.RowCount; i++)
            {
                bool isCheck = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsCheck));
                if (isCheck) count++;
            }
            if (count <= 0)
            {
                MessageBox.Show("Vui lòng Chọn ít nhất một Vị trí!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            Expression ex1 = new Expression("IsDeleted", 0);
            Expression ex2 = new Expression("ExamCode", txtExamCode.Text.Trim());
            Expression ex3 = new Expression("ID", kpiExam.ID, "<>");
            Expression ex4 = new Expression("KPISessionID", TextUtils.ToInt(cboSession.EditValue));


            List<KPIExamModel> lst = SQLHelper<KPIExamModel>.FindByExpression(ex1.And(ex2).And(ex3).And(ex4));
            if (lst.Count > 0)
            {
                MessageBox.Show($"Mã bài đánh giá [{txtExamCode.Text.Trim()}] đã được sử dụng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private bool SaveData()
        {
            if (!CheckValidate()) return false;
            DateTime date = dtpDateDeadline.Value;
            KPIExamModel model = SQLHelper<KPIExamModel>.FindByID(kpiExam.ID);
            model.KPISessionID = TextUtils.ToInt(cboSession.EditValue);
            model.ExamCode = txtExamCode.Text.Trim();
            model.ExamName = txtExamName.Text.Trim();
            model.IsDeleted = false;
            model.IsActive = ckbIsActive.Checked;
            model.Deadline = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            if (model.ID > 0) SQLHelper<KPIExamModel>.Update(model);
            else model.ID = SQLHelper<KPIExamModel>.Insert(model).ID;


            SQLHelper<KPIExamPositionModel>.DeleteByAttribute("KPIExamID", model.ID);
            for (int i = 0; i < grvData.RowCount; i++)
            {
                bool isCheck = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsCheck));
                int positionId = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                if (isCheck)
                {
                    KPIExamPositionModel newdetail = new KPIExamPositionModel()
                    {
                        KPIExamID = model.ID,
                        KPIPositionID = positionId
                    };
                    SQLHelper<KPIExamPositionModel>.Insert(newdetail);
                }
            }
            return true;
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (SaveData()) this.DialogResult = DialogResult.OK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                kpiExam = new KPIExamModel();
                txtExamCode.Clear();
                txtExamName.Clear();
            }
        }

        private void frmKPIExam_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(e.Location);
                if (info.Column != null && info.Column == colIsCheck && info.HitTest == GridHitTest.Column)
                {
                    int rowHandle = grvData.FocusedRowHandle;
                    frmKPIPositionDetails frm = new frmKPIPositionDetails(2);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        DataTable dt = SQLHelper<KPIPositionModel>.LoadDataFromSP("spGetKPIPositionByExamID", new string[] { "@KPIExamID" }, new object[] { kpiExam.ID });
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int poisitionID = TextUtils.ToInt(dt.Rows[i]["ID"]);
                            bool isCheck = TextUtils.ToBoolean(dt.Rows[i]["IsCheck"]);
                            bool isMatch = positionIDs.Any(p => p == poisitionID);
                            if (isMatch) dt.Rows[i]["IsCheck"] = true;
                            else if (isCheck) dt.Rows[i]["IsCheck"] = false;
                        }
                        grdData.DataSource = dt;
                        grvData.FocusedRowHandle = rowHandle;
                    }
                }
            }
        }


        private void repositoryItemCheckEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (TextUtils.ToBoolean(e.OldValue) == true)
            {
                bool isDelete = MessageBox.Show($"Bạn có muốn xóa Vị trí [{TextUtils.ToString(grvData.GetFocusedRowCellValue(colPositionName))}] hay không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                if (isDelete)
                {
                    positionIDs.Remove(ID);
                }
                else e.Cancel = true;
            }
            else
            {
                bool isDuplicate = positionIDs.Any(p => p == ID);
                if (!isDuplicate) positionIDs.Add(ID);
            }
        }
    }
}
