using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Forms.Classes;
using System;
using System.Collections;
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
    public partial class frmPlanWeekDetail : _Forms
    {
        List<int> listID = new List<int>();
        public DateTime dateStart = DateTime.Now;
        public int userID = 0;
        DataTable dtWeekplan = new DataTable();

        public frmPlanWeekDetail()
        {
            InitializeComponent();
        }

        private void frmWeekReport_Load(object sender, EventArgs e)
        {
            //dtpDateStart.Value = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            dtpDateStart.Value = dateStart;
            dtpDateEnd.Value = dtpDateStart.Value.AddDays(6);

            loadUser();
            loadPlanWeekDetail();

            btnSave.Enabled = checkPermission();
        }

        bool checkPermission()
        {
            bool result = false;
            if (Global.IsAdmin)
            {
                result = true;
            }
            else if (Global.UserID == userID)
            {
                result = true;
            }
            return result;
        }

        void loadUser()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboUser.Properties.ValueMember = "UserID";
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.DataSource = dt;
            cboUser.EditValue = userID;
        }

        void loadPlanWeekDetail()
        {
            //if (dtr != null && dtr.Length > 0)
            //{
            //    cbUser.EditValue = UserID;
            //    grdData.DataSource = dtr.CopyToDataTable();
            //}
            //else
            //{
            //    DataTable dt = TextUtils.Select($"Select * From WeekPlan where ID=0");
            //    if (dt.Rows.Count == 0)
            //        for (int i = 0; i < 6; i++)
            //        {
            //            dt.Rows.Add(0, DateS);
            //            DateS = DateS.AddDays(1);
            //        }
            //    grdData.DataSource = dt;
            //}

            dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            userID = TextUtils.ToInt(cboUser.EditValue);
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetPlanWeek",
                                       new string[] { "@DateStart", "@DateEnd", "@Department", "@UserID" },
                                       new object[] { dateStart, dateEnd, 0, userID });
            dtWeekplan = dataSet.Tables[1];
            grdData.DataSource = dtWeekplan;
        }

        bool SaveData()
        {
            grvData.CloseEditor();
            for (int i = 0; i < grvData.RowCount; i++)
            {
                WeekPlanModel weekPlan = new WeekPlanModel();
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                if (id > 0)
                {
                    weekPlan = SQLHelper<WeekPlanModel>.FindByID(id);
                }

                weekPlan.DatePlan = TextUtils.ToDate5(grvData.GetRowCellValue(i, colDatePlan));
                weekPlan.UserID = TextUtils.ToInt(cboUser.EditValue);
                weekPlan.ContentPlan = TextUtils.ToString(grvData.GetRowCellValue(i, colContentPlan));
                weekPlan.Result = TextUtils.ToString(grvData.GetRowCellValue(i, colResult));

                if (weekPlan.ID > 0)
                {
                    if (string.IsNullOrEmpty(weekPlan.ContentPlan))
                    {
                        WeekPlanBO.Instance.Delete(weekPlan.ID);
                    }
                    else
                    {
                        WeekPlanBO.Instance.Update(weekPlan);
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(weekPlan.ContentPlan))
                    {
                        continue;
                    }
                    WeekPlanBO.Instance.Insert(weekPlan);
                }
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                dtWeekplan.AcceptChanges();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!checkPermission())
            {
                MessageBox.Show($"Bạn không có quyền xoá kế hoạch của nhân viên [{cboUser.Text}]!", "Thông báo");
                return;
            }
            string datePlan = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colDatePlan));
            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa kế hoạch ngày [{datePlan}] không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.SetFocusedRowCellValue(colContentPlan, "");
                grvData.SetFocusedRowCellValue(colResult, "");
            }
        }



        private void btnNext_Click(object sender, EventArgs e)
        {
            dtpDateStart.Value = dtpDateStart.Value.AddDays(+7);
            dtpDateEnd.Value = dtpDateEnd.Value.AddDays(+7);
            loadPlanWeekDetail();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            dtpDateStart.Value = dtpDateStart.Value.AddDays(-7);
            dtpDateEnd.Value = dtpDateEnd.Value.AddDays(-7);
            loadPlanWeekDetail();
        }

        private void cboUser_EditValueChanged(object sender, EventArgs e)
        {
            loadPlanWeekDetail();
        }

        private void frmWeekReportDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            if (!checkPermission())
            {
                this.DialogResult = DialogResult.OK;
                return;
            }

            //DataTable dtEdit = (DataTable)grdData.DataSource;
            DataTable dataChange = dtWeekplan.GetChanges();
            if (dataChange != null)
            {
                DialogResult dialog = MessageBox.Show("Những thay đổi chưa được lưu.\nBạn có muốn lưu lại không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    btnSave_Click(null, null);
                }
                else if (dialog == DialogResult.No)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void frmPlanWeekDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                frmWeekReportDetail_FormClosing(null, null);
            }
        }
    }
}
