using BMS;
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
    public partial class frmPracticeDetails : _Forms
    {
        private int rowHandleCourseResult;
        private DataTable dtResult = new DataTable();
        public CourseExamResultModel examResult;
        public frmPracticeDetails()
        {
            InitializeComponent();

        }
        private void frmPracticeDetails_Load(object sender, EventArgs e)
        {
            toolStripButton1.Enabled = btnSaveAndClose.Enabled = ValidateUser();
            LoadData();
        }
        private void LoadData()
        {
            LoadDataCourseExamResult();
            LoadDataExamResultDetails();
        }
        private void LoadDataCourseExamResult()
        {
            DataTable dtResultHistory = TextUtils.LoadDataFromSP("spGetResultHistoryPractice", "A",
                                                   new string[] { "@EmployeeId", "@CourseExamId" },
                                                   new object[] { examResult.EmployeeId, examResult.CourseExamId });
            grdCourseResult.DataSource = dtResultHistory;
        }
        private void LoadDataExamResultDetails()
        {
            dtResult = TextUtils.LoadDataFromSP("spGetResultHistoryByPractice", "A",
                                                 new string[] { "@CourseExamId", "@EmployeeId", "@CourseResultId" },
                                                 new object[] { examResult.CourseExamId, examResult.EmployeeId, examResult.ID });

            grdExamResult.DataSource = dtResult;
        }
        private bool Save()
        {
            grvExamResult.FocusedRowHandle = -1;
            decimal point = 0;
            decimal totalPoint = 0;

            //int maxPoint = TextUtils.ToInt(repositoryItemSpinEdit1.MaxValue) == 0 ? 10 : TextUtils.ToInt(repositoryItemSpinEdit1.MaxValue);
            //int maxPoint = 10;
            for (int i = 0; i < grvExamResult.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvExamResult.GetRowCellValue(i, colExamResultId));
                CourseExamEvaluateModel detail = SQLHelper<CourseExamEvaluateModel>.FindByID(ID);
                //if (detail.ID <= 0) return false;
                if (detail.ID <= 0) continue;
                detail.Point = TextUtils.ToDecimal(grvExamResult.GetRowCellValue(i, colExamResultPoint));
                detail.Note = TextUtils.ToString(grvExamResult.GetRowCellValue(i, colExamResultNote));
                SQLHelper<CourseExamEvaluateModel>.Update(detail);
                point += detail.Point;
                totalPoint += 10;
            }

            int rowHandle = grvCourseResult.FocusedRowHandle;
            rowHandleCourseResult = grvCourseResult.FocusedRowHandle;
            int courseExamId = TextUtils.ToInt(grvCourseResult.GetRowCellValue(rowHandle, colIDExamResult));
            decimal goalPoint = TextUtils.ToDecimal(grvCourseResult.GetRowCellValue(rowHandle, colGoal));
            CourseExamResultModel result = SQLHelper<CourseExamResultModel>.FindByID(courseExamId) ?? new CourseExamResultModel();
            if (result == null) return false;
            result.Status = 3;
            result.PracticePoints = point;

            if (totalPoint == 0) result.Evaluate = false;
            else result.Evaluate = ((point / totalPoint) * 100) >= goalPoint ? true : false;
            SQLHelper<CourseExamResultModel>.Update(result);
            examResult = result;
            return true;
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (!Save())
            {
                MessageBox.Show("Đã xảy ra lỗi! Vui lòng thử lại", "Thông báo");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                dtResult.AcceptChanges();
                LoadData();
                grvCourseResult.FocusedRowHandle = rowHandleCourseResult;
                MessageBox.Show("Lưu kết quả thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi! Vui lòng thử lại", "Thông báo");
            }
            ;
        }

        private void grvCourseResult_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataTable dataChange = dtResult.GetChanges();
            if (dataChange != null)
            {
                DialogResult dialog = MessageBox.Show("Bạn có muốn lưu những thay đổi không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    if (!Save()) MessageBox.Show("Đã xảy ra lỗi! Vui lòng thử lại", "Thông báo");
                }
            }

            int rowHandle = grvCourseResult.FocusedRowHandle;
            int courseExamId = TextUtils.ToInt(grvCourseResult.GetRowCellValue(rowHandle, colIDExamResult));
            CourseExamResultModel result = SQLHelper<CourseExamResultModel>.FindByID(courseExamId) ?? new CourseExamResultModel();
            if (result != null)
            {
                examResult = result;
                LoadDataExamResultDetails();
            }
            else
            {
                MessageBox.Show("Không thể tìm thấy bài thi!", "Thông báo");
            }
        }



        private void grvCourseResult_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == 0)
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
        }

        private void frmPracticeDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataTable dataChange = dtResult.GetChanges();
            if (dataChange != null)
            {
                DialogResult dialog = MessageBox.Show("Bạn có muốn lưu những thay đổi không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    if (!Save()) MessageBox.Show("Đã xảy ra lỗi! Vui lòng thử lại", "Thông báo");
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private bool ValidateUser()
        {
            CourseExamModel model = SQLHelper<CourseExamModel>.FindByID(examResult.CourseExamId);
            var ex1 = new Expression("PositionCode", "TBP/PP", "<>");
            List<KPIPositionModel> listPositions = SQLHelper<KPIPositionModel>.FindByExpression(ex1);
            string lstCode = string.Join(",", listPositions.Select(x => x.ID.ToString()));

            List<EmployeeModel> lstPro = SQLHelper<EmployeeModel>.ProcedureToList("spGetAllEmployeePositionID", new string[] { "@KPIPostionID" },
                                                                                      new object[] { lstCode });
            bool isProSen = lstPro.Any(p => p.ID == Global.EmployeeID);
            bool isCreated = TextUtils.ToString(Global.AppUserName) == model.CreatedBy && model.ID > 0;
            if (isProSen || isCreated || Global.IsAdmin) return true;
            return false;
        }
    }
}
