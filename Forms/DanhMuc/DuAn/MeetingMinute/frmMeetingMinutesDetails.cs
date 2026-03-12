//using BaseBusiness.Model;
using BMS.Model;
using BMS.UP;
using BMS.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;

namespace BMS
{
    public partial class frmMeetingMinutesDetails : _Forms
    {
        public MeetingMinutesModel meetingMinutes = new MeetingMinutesModel();
        List<int> lstDeletedDetails = new List<int>();
        List<int> lstDeletedAttendance = new List<int>();
        DataTable dtContent = new DataTable();
        DataTable dtAtandance = new DataTable();

        //PQ.Chien - Update - 14/07/2025
        List<MeetingMinutesFileModel> listFiles = new List<MeetingMinutesFileModel>();
        List<MeetingMinutesFileModel> listFileDelete = new List<MeetingMinutesFileModel>();
        List<FileInfo> listFileUpload = new List<FileInfo>();
        string[] suggestions = { "Phòng họp 1 (Hồ Tây)", "Phòng họp 2 (Hồ Gươm)", "Phòng họp 3 (Hồ Trúc Bạch)" };
        //END UPDATE
        public Action ReloadMeetingType;//ndnhat update 09/08/2025
        public frmMeetingMinutesDetails()
        {
            InitializeComponent();
        }

        private void frmMeetingMinutesDetails_Load(object sender, EventArgs e)
        {
            //PQ.Chiến - UPDATE - 15/07/2025
            txtPlace.Items.AddRange(suggestions);
            txtPlace.DropDownStyle = ComboBoxStyle.DropDown; // Cho phép nhập tự do
            txtPlace.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Gợi ý và tự hoàn thành
            txtPlace.AutoCompleteSource = AutoCompleteSource.ListItems;
            //END
            LoadGroupMeetingType();
            LoadMeetingType();
            LoadEmployee();
            LoadProject();
            LoadDataDetails();


            LoadUserTeam(); //vtn Update
            grdMeetingMinutesFile.ContextMenuStrip = contextMenuStrip1;

            
        }
        private void LoadGroupMeetingType()
        {
            List<object> lst = new List<object>()
            {
                new {ID = 1, GroupName = "Nội bộ"},
                new {ID = 2, GroupName = "Khách hàng"},
            };
            cboGroupMeetingType.DataSource = lst;
            cboGroupMeetingType.ValueMember = "ID";
            cboGroupMeetingType.DisplayMember = "GroupName";
        }
        private void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.DataSource = dt;
            cboEmployee.ValueMember = "ID";
            cboEmployee.DisplayMember = "Code";

            cboEmployeeAttendance.DataSource = dt;
            cboEmployeeAttendance.ValueMember = "ID";
            cboEmployeeAttendance.DisplayMember = "Code";

            //PQ.Chien - UPDATE - 15/07/2025
            cboCreator.Properties.DataSource = dt;
            cboCreator.Properties.ValueMember = "ID";
            cboCreator.Properties.DisplayMember = "FullName";
            cboCreator.EditValue = Global.EmployeeID;
            //END
            cboCreator.ReadOnly = !(Global.IsAdmin && Global.EmployeeID <= 0);
        }
        private void LoadProject()
        {
            List<ProjectModel> lst = SQLHelper<ProjectModel>.FindAll();
            cboProject.Properties.DataSource = lst;
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.ValueMember = "ID";

            //PQ.Chiến - UPDATE - 15/07/2025
            cboProject.EditValue = meetingMinutes.ProjectID;
            //END
        }
        private void LoadMeetingType()
        {
            List<MeetingTypeModel> lst = SQLHelper<MeetingTypeModel>.FindByAttribute("IsDelete", 0);

            cboMeetingType.Properties.DataSource = lst;
            cboMeetingType.Properties.DisplayMember = "TypeName";
            cboMeetingType.Properties.ValueMember = "ID";



        }

        private void LoadDataDetails() //vtn Update
        {
            //PQ.Chien - Update - 14/07/2025
            listFileUpload = new List<FileInfo>();
            listFileDelete = new List<MeetingMinutesFileModel>();
            //listFiles = SQLHelper<MeetingMinutesFileModel>.FindByAttribute("MeetingMinutesID", meetingMinutes.ID);
            var exp1 = new Expression("MeetingMinutesID", meetingMinutes.ID);
            var exp2 = new Expression("IsDeleted", 0);
            listFiles = SQLHelper<MeetingMinutesFileModel>.FindByExpression(exp1.And(exp2));
            grdMeetingMinutesFile.DataSource = listFiles;
            //END-UPDATE

            dtpDateStart.EditValue = meetingMinutes.DateStart.HasValue ? meetingMinutes.DateStart.Value : DateTime.Now;
            dtpDateEnd.EditValue = meetingMinutes.DateEnd.HasValue ? meetingMinutes.DateEnd.Value : TextUtils.ToDate5(dtpDateStart.EditValue).AddHours(+1);
            cboMeetingType.EditValue = meetingMinutes.MeetingTypeID;
            txtTitle.Text = meetingMinutes.Title;
            txtPlace.Text = meetingMinutes.Place;
            cboProject.EditValue = meetingMinutes.ProjectID;

            //PQ.Chien - UPDATE - 15/07/2025
            if (meetingMinutes.ID > 0)
            {
                cboCreator.EditValue = meetingMinutes.CreatorID;
            }
            //END

            DataSet ds = TextUtils.LoadDataSetFromSP("spGetMeetingMinutesDetailsByID", new string[] { "@MeetingMinutesID" }, new object[] { meetingMinutes.ID });

            dtDetailCustomer = ds.Tables[0];
            dtContent = ds.Tables[1];
            dtCustomer = ds.Tables[3];
            dtAtandance = ds.Tables[2];

            grdDetails.DataSource = dtContent;
            grdDetailCustomer.DataSource = dtDetailCustomer;
            grdAttendance.DataSource = dtAtandance;
            grdCustomer.DataSource = dtCustomer;

        }
        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadProjectProblem();
        }
        private void LoadProjectProblem()
        {
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            List<ProjectHistoryProblemModel> lst = SQLHelper<ProjectHistoryProblemModel>.FindByAttribute("ProjectID", projectID);
            lst.Insert(0, new ProjectHistoryProblemModel()
            {
                ID = -1,
                ContentError = "-- Phát sinh mới --"
            });
            cboProjectProblem.DataSource = lst;
            cboProjectProblem.ValueMember = "ID";
            cboProjectProblem.DisplayMember = "ContentError";

            cboProblemCus.DataSource = lst;
            cboProblemCus.ValueMember = "ID";
            cboProblemCus.DisplayMember = "ContentError";
        }
        private void grvContent_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void grvAttendance_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvAttendance.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colAttendanceSTT && info.HitTest == GridHitTest.Column)
                {
                    grvAttendance.FocusedRowHandle = -1;

                    DataRow dtrow = dtAtandance.NewRow();
                    dtrow["STT"] = grvAttendance.RowCount + 1;
                    dtAtandance.Rows.Add(dtrow);

                    grdAttendance.DataSource = dtAtandance;
                    dtAtandance.AcceptChanges();
                }
            }
        }

        private void btnContentDelete_Click(object sender, EventArgs e)
        {
            int stt = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colDetailSTT));
            int idDetail = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colDetailID));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc chắn muốn xóa người phụ trách thứ [{stt}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (!lstDeletedDetails.Contains(idDetail) && idDetail > 0) lstDeletedDetails.Add(idDetail);
                grvDetails.DeleteSelectedRows();
                for (int i = 0; i < grvDetails.RowCount; i++)
                {
                    grvDetails.SetRowCellValue(i, colDetailSTT, i + 1);
                }
            }
        }

        private void btnAttendanceDelete_Click(object sender, EventArgs e)
        {
            int stt = TextUtils.ToInt(grvAttendance.GetFocusedRowCellValue(colAttendanceSTT));
            int idAttendance = TextUtils.ToInt(grvAttendance.GetFocusedRowCellValue(colAttendanceID));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc chắn muốn xóa người phụ trách thứ [{stt}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (!lstDeletedAttendance.Contains(idAttendance) && idAttendance > 0) lstDeletedAttendance.Add(idAttendance);
                grvAttendance.DeleteSelectedRows();
                for (int i = 0; i < grvAttendance.RowCount; i++)
                {
                    grvAttendance.SetRowCellValue(i, colAttendanceSTT, i + 1);
                }
            }
        }

        private void cboProjectProblem_EditValueChanged(object sender, EventArgs e)
        {
            grvDetails.FocusedColumn = colDetailID;
            int projectProblemID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colDetailProjectHistoryProblemID));
            string content = TextUtils.ToString(grvDetails.GetFocusedRowCellValue(colDetailDetailContent));
            string reason = TextUtils.ToString(grvDetails.GetFocusedRowCellValue(colDetailResult));
            string plan = TextUtils.ToString(grvDetails.GetFocusedRowCellValue(colDetailPlan));

            bool isEmpty = string.IsNullOrWhiteSpace(content) && string.IsNullOrWhiteSpace(reason) && string.IsNullOrWhiteSpace(plan);
            if (projectProblemID != -1 && isEmpty)
            {
                ProjectHistoryProblemModel model = (ProjectHistoryProblemModel)cboProjectProblem.GetRowByKeyValue(projectProblemID);
                grvDetails.SetFocusedRowCellValue(colDetailDetailContent, model.ContentError);
                grvDetails.SetFocusedRowCellValue(colDetailResult, model.Reason);
                grvDetails.SetFocusedRowCellValue(colDetailPlan, model.Remedies);
            }
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            grvDetails.FocusedColumn = colDetailID;
            int empID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colDetailEmployeeID));
            if (empID <= 0) return;

            DataTable dt = (DataTable)cboEmployee.DataSource;
            DataRow[] dtr = dt.Select($"ID='{empID}'");
            if (dtr.Length == 0) return;

            grvDetails.SetFocusedRowCellValue(colDetailCustomerName, dtr[0]["FullName"]);
            grvDetails.SetFocusedRowCellValue(colDetailPhoneNumber, dtr[0]["SDTCaNhan"]);
        }

        private void grvDetails_ShowingEditor(object sender, CancelEventArgs e)
        {
            //var focusColunm = grvDetails.FocusedColumn;
            //int empID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colDetailEmployeeID));
            //int projectProblemID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colDetailProjectHistoryProblemID));
            //bool isProblem = projectProblemID > 0 && (focusColunm == colDetailDetailContent || focusColunm == colDetailReason || focusColunm == colDetailPlan);
            //bool isCustomer = empID > 0 && (focusColunm == colDetailCustomerName || focusColunm == colDetailPhoneNumber);
            //if (isProblem || isCustomer)
            //{
            //    e.Cancel = true;
            //}
        }



        private async void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (await SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private async void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (await SaveData())
            {
                meetingMinutes = new MeetingMinutesModel();
                lstDeletedDetails = new List<int>();
                lstDeletedAttendance = new List<int>();
                LoadDataDetails();
            }
        }

        private void frmMeetingMinutesDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmMeetingTypeDetails frm = new frmMeetingTypeDetails();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                int oldType = TextUtils.ToInt(cboMeetingType.EditValue);
                LoadMeetingType();
                cboMeetingType.EditValue = oldType;
                ReloadMeetingType?.Invoke();//ndnhat update 09/08/2025
            }
        }

        #region vtn Update
        DataTable dtCustomer = new DataTable();
        DataTable dtDetailCustomer = new DataTable();
        private void LoadUserTeam()
        {
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetUserTeam", new string[] { "@DepartmentID" }, new object[] { 0 });

            if (ds.Tables[0].Columns.Contains("Name"))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    row["Name"] = row["Name"].ToString().ToUpper();
                }
            }
            cboTeam.DisplayMember = "Name";
            cboTeam.ValueMember = "ID";
            cboTeam.DataSource = ds.Tables[0];

        }

        private void cboMeetingType_EditValueChanged(object sender, EventArgs e)
        {
            int mtId = TextUtils.ToInt(cboMeetingType.EditValue);
            var checkGroupId = SQLHelper<MeetingTypeModel>.FindByID(mtId);
            if (checkGroupId.GroupID == 1)
            {
                splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                splitContainerControl1.IsSplitterFixed = true;
                splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
                splitContainerControl2.IsSplitterFixed = true;
            }
            else
            {
                splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                splitContainerControl1.IsSplitterFixed = false;
                splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                splitContainerControl2.IsSplitterFixed = false;
            }
        }

        private void grvCustomer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvCustomer.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colCusStt && info.HitTest == GridHitTest.Column)
                {
                    grvCustomer.FocusedRowHandle = -1;

                    DataRow dtrow = dtCustomer.NewRow();
                    dtrow["STT"] = grvCustomer.RowCount + 1;
                    dtCustomer.Rows.Add(dtrow);

                    grdCustomer.DataSource = dtCustomer;
                    dtCustomer.AcceptChanges();
                }
            }
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            int stt = TextUtils.ToInt(grvCustomer.GetFocusedRowCellValue(colCusStt));
            int idCustomer = TextUtils.ToInt(grvCustomer.GetFocusedRowCellValue(colCustomerID));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng thứ [{stt}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (!lstDeletedAttendance.Contains(idCustomer) && idCustomer > 0) lstDeletedAttendance.Add(idCustomer);
                grvCustomer.DeleteSelectedRows();
                for (int i = 0; i < grvCustomer.RowCount; i++)
                {
                    grvCustomer.SetRowCellValue(i, colCusStt, i + 1);
                }
            }
        }

        private void grdDetails_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvDetails.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colDetailSTT && info.HitTest == GridHitTest.Column)
                {
                    grvDetails.FocusedRowHandle = -1;

                    DataRow dtrow = dtContent.NewRow();
                    dtrow["STT"] = grvDetails.RowCount + 1;
                    dtContent.Rows.Add(dtrow);

                    grdDetails.DataSource = dtContent;
                    dtContent.AcceptChanges();

                }
            }
        }

        private void grvDetailCustomer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvDetailCustomer.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colDetailSttCus && info.HitTest == GridHitTest.Column)
                {
                    grvDetailCustomer.FocusedRowHandle = -1;

                    DataRow dtrow = dtDetailCustomer.NewRow();
                    dtrow["STT"] = grvDetailCustomer.RowCount + 1;
                    dtDetailCustomer.Rows.Add(dtrow);

                    grdDetailCustomer.DataSource = dtDetailCustomer;
                    dtDetailCustomer.AcceptChanges();

                }
            }
        }

        private void btnDeleteDetailCus_Click(object sender, EventArgs e)
        {
            int stt = TextUtils.ToInt(grvDetailCustomer.GetFocusedRowCellValue(colDetailSttCus));
            int idCustomer = TextUtils.ToInt(grvDetailCustomer.GetFocusedRowCellValue(colDetailCusID));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc chắn muốn xóa chi tiết khách hàng thứ [{stt}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (!lstDeletedDetails.Contains(idCustomer) && idCustomer > 0) lstDeletedDetails.Add(idCustomer);
                grvDetailCustomer.DeleteSelectedRows();
                for (int i = 0; i < grvDetailCustomer.RowCount; i++)
                {
                    grvDetailCustomer.SetRowCellValue(i, colDetailSttCus, i + 1);
                }
            }
        }

        private void cboProblemCus_EditValueChanged(object sender, EventArgs e)
        {
            grvDetailCustomer.FocusedColumn = colDetailCusID;
            int projectProblemID = TextUtils.ToInt(grvDetailCustomer.GetFocusedRowCellValue(colProblemCusDe));
            string content = TextUtils.ToString(grvDetailCustomer.GetFocusedRowCellValue(colContentCusDe));
            string reason = TextUtils.ToString(grvDetailCustomer.GetFocusedRowCellValue(colResultCusDe));

            bool isEmpty = string.IsNullOrWhiteSpace(content) && string.IsNullOrWhiteSpace(reason);
            if (projectProblemID != -1 && isEmpty)
            {
                ProjectHistoryProblemModel model = (ProjectHistoryProblemModel)cboProblemCus.GetRowByKeyValue(projectProblemID);
                grvDetailCustomer.SetFocusedRowCellValue(colDetailDetailContent, model.ContentError);
                grvDetailCustomer.SetFocusedRowCellValue(colDetailResult, model.Reason);
            }
        }

        private void cboEmployeeAttendance_EditValueChanged(object sender, EventArgs e)
        {
            grvAttendance.FocusedColumn = colAttendanceID;
            int empID = TextUtils.ToInt(grvAttendance.GetFocusedRowCellValue(colAttendanceEmployeeID));
            if (empID <= 0) return;

            DataTable dt = (DataTable)cboEmployeeAttendance.DataSource;
            DataRow[] dtr = dt.Select($"ID='{empID}'");
            if (dtr.Length == 0) return;
            string cv = TextUtils.ToString(dtr[0]["ChucVu"]);
            grvAttendance.SetFocusedRowCellValue(colAttendanceCustomerName, dtr[0]["FullName"]);
            grvAttendance.SetFocusedRowCellValue(colAttendanceChucvu, dtr[0]["ChucVu"]);
            grvAttendance.SetFocusedRowCellValue(colAttendacePhoneNum, dtr[0]["SDTCaNhan"]);

            int deID = TextUtils.ToInt(dtr[0]["DepartmentID"]);
            DataTable dtCheck = TextUtils.LoadDataFromSP("spGetUserTeamLink_New", "A",
                   new string[] { "@UserTeamID", "@DepartmentID" },
                   new object[] { 0, deID });
            if (dtCheck.Rows.Count > 0)
            {
                DataRow[] row = dtCheck.Select($"EmployeeID='{empID}'");
                if (row != null)
                    try
                    {
                        grvAttendance.SetFocusedRowCellValue(colAttendanceTeam, row[0]["UserTeamID"]);
                    }
                    catch { }
            }

        }
        private void grvAttendance_ShowingEditor(object sender, CancelEventArgs e)
        {
            var focusColunm = grvAttendance.FocusedColumn;
            int empID = TextUtils.ToInt(grvAttendance.GetFocusedRowCellValue(colAttendanceEmployeeID));
            bool isCustomer = empID > 0 && (focusColunm == colAttendanceCustomerName || focusColunm == colAttendanceTeam);

            //if (isCustomer)
            //{
            //    e.Cancel = true;
            //}
        }

        private async Task<bool> SaveData()
        {
            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                if (!CheckValidate()) return false;

                int meetingType = TextUtils.ToInt(cboMeetingType.EditValue);
                int mtId = TextUtils.ToInt(cboMeetingType.EditValue);
                var checkGroupId = SQLHelper<MeetingTypeModel>.FindByID(mtId);

                MeetingMinutesModel model = SQLHelper<MeetingMinutesModel>.FindByID(meetingMinutes.ID);
                model.ProjectID = TextUtils.ToInt(cboProject.EditValue);
                model.MeetingTypeID = TextUtils.ToInt(cboMeetingType.EditValue);
                model.DateStart = (DateTime)dtpDateStart.EditValue;
                model.DateEnd = (DateTime)dtpDateEnd.EditValue;
                model.Title = txtTitle.Text.Trim();
                model.Place = txtPlace.Text.Trim();
                model.IsDeleted = false;


                //PQ.CHIEN - UPDATE - 15/07/2025
                model.CreatorID = TextUtils.ToInt(cboCreator.EditValue);
                //END


                if (model.ID > 0) SQLHelper<MeetingMinutesModel>.Update(model);
                else model.ID = SQLHelper<MeetingMinutesModel>.Insert(model).ID;

                //Xóa các detail
                Dictionary<string, object> newDict = new Dictionary<string, object>()
                {
                    {"IsDeleted", 1 },
                    {"UpdatedBy", Global.AppCodeName},
                    {"UpdatedDate", DateTime.Now }
                };
                if (lstDeletedDetails.Count > 0)
                {
                    string strIDs = string.Join(",", lstDeletedDetails);
                    Expression ex1 = new Expression("ID", strIDs, "IN");
                    SQLHelper<MeetingMinutesDetailsModel>.UpdateFields(newDict, ex1);
                }
                if (lstDeletedAttendance.Count > 0)
                {
                    string strIDs = string.Join(",", lstDeletedAttendance);
                    Expression ex1 = new Expression("ID", strIDs, "IN");
                    SQLHelper<MeetingMinutesAttendanceModel>.UpdateFields(newDict, ex1);
                }

                //============================= Lưu details nhân viên=============================
                for (int i = 0; i < grvDetails.RowCount; i++)
                {
                    int detailID = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colDetailID));
                    int problemID = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colDetailProjectHistoryProblemID));
                    int empID = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colDetailEmployeeID));
                    if (problemID == -1)
                    {
                        List<ProjectHistoryProblemModel> lst = SQLHelper<ProjectHistoryProblemModel>.FindByAttribute("ProjectID", model.ProjectID);
                        ProjectHistoryProblemModel newProblem = new ProjectHistoryProblemModel()
                        {
                            ProjectID = TextUtils.ToInt(model.ProjectID),
                            STT = lst.Count + 1,
                            ContentError = TextUtils.ToString(grvDetails.GetRowCellValue(i, colDetailDetailContent)),
                            Reason = TextUtils.ToString(grvDetails.GetRowCellValue(i, colDetailResult)),
                            Remedies = TextUtils.ToString(grvDetails.GetRowCellValue(i, colDetailPlan)),
                            DateProblem = DateTime.Now
                        };
                        problemID = SQLHelper<ProjectHistoryProblemModel>.Insert(newProblem).ID;
                    }

                    MeetingMinutesDetailsModel modelDetail = SQLHelper<MeetingMinutesDetailsModel>.FindByID(detailID);
                    modelDetail.MeetingMinutesID = model.ID;

                    modelDetail.PlanDate = TextUtils.ToDate4(grvDetails.GetRowCellValue(i, colDetailPlan));
                    modelDetail.DetailContent = TextUtils.ToString(grvDetails.GetRowCellValue(i, colDetailDetailContent));
                    modelDetail.DetailResult = TextUtils.ToString(grvDetails.GetRowCellValue(i, colDetailResult));
                    modelDetail.ProjectHistoryProblemID = problemID;
                    modelDetail.EmployeeID = empID;
                    modelDetail.IsEmployee = true;
                    modelDetail.CustomerName = TextUtils.ToString(grvDetails.GetRowCellValue(i, colDetailCustomerName));
                    modelDetail.PhoneNumber = TextUtils.ToString(grvDetails.GetRowCellValue(i, colDetailPhoneNumber));
                    modelDetail.Note = TextUtils.ToString(grvDetails.GetRowCellValue(i, colDetailNote));
                    modelDetail.IsDeleted = false;

                    if (modelDetail.ID > 0) SQLHelper<MeetingMinutesDetailsModel>.Update(modelDetail);
                    else modelDetail.ID = SQLHelper<MeetingMinutesDetailsModel>.Insert(modelDetail).ID;
                }

                //============================= Lưu attendance nhân viên =============================
                for (int i = 0; i < grvAttendance.RowCount; i++)
                {
                    int attendanceID = TextUtils.ToInt(grvAttendance.GetRowCellValue(i, colAttendanceID));

                    MeetingMinutesAttendanceModel modelAttendance = SQLHelper<MeetingMinutesAttendanceModel>.FindByID(attendanceID);
                    modelAttendance.MeetingMinutesID = model.ID;
                    modelAttendance.EmployeeID = TextUtils.ToInt(grvAttendance.GetRowCellValue(i, colAttendanceEmployeeID));
                    modelAttendance.Section = TextUtils.ToString(grvAttendance.GetRowCellValue(i, colAttendanceChucvu));
                    modelAttendance.FullName = TextUtils.ToString(grvAttendance.GetRowCellValue(i, colAttendanceCustomerName));
                    modelAttendance.PhoneNumber = TextUtils.ToString(grvAttendance.GetRowCellValue(i, colAttendacePhoneNum));
                    modelAttendance.UserTeamID = TextUtils.ToInt(grvAttendance.GetRowCellValue(i, colAttendanceTeam));
                    modelAttendance.IsDeleted = false;
                    modelAttendance.IsEmployee = false;

                    if (modelAttendance.ID > 0) SQLHelper<MeetingMinutesAttendanceModel>.Update(modelAttendance);
                    else modelAttendance.ID = SQLHelper<MeetingMinutesAttendanceModel>.Insert(modelAttendance).ID;
                }

                if (checkGroupId.GroupID == 2)
                {
                    //============================= Lưu attendance khách hàng =============================
                    for (int i = 0; i < grvCustomer.RowCount; i++)
                    {
                        int attendanceID = TextUtils.ToInt(grvCustomer.GetRowCellValue(i, colAttendanceID));

                        MeetingMinutesAttendanceModel modelAttendance = SQLHelper<MeetingMinutesAttendanceModel>.FindByID(attendanceID);
                        modelAttendance.MeetingMinutesID = model.ID;
                        modelAttendance.FullName = TextUtils.ToString(grvCustomer.GetRowCellValue(i, colNameCus));
                        modelAttendance.EmployeeID = -1;
                        modelAttendance.EmailCustomer = TextUtils.ToString(grvCustomer.GetRowCellValue(i, colEmailCus));
                        modelAttendance.AddressCustomer = TextUtils.ToString(grvCustomer.GetRowCellValue(i, colAddressCus));
                        modelAttendance.PhoneNumber = TextUtils.ToString(grvCustomer.GetRowCellValue(i, colSdtCus));
                        modelAttendance.IsDeleted = false;
                        modelAttendance.IsEmployee = true;

                        if (modelAttendance.ID > 0) SQLHelper<MeetingMinutesAttendanceModel>.Update(modelAttendance);
                        else modelAttendance.ID = SQLHelper<MeetingMinutesAttendanceModel>.Insert(modelAttendance).ID;
                    }

                    //============================= Lưu details khách hàng =============================
                    for (int i = 0; i < grvDetailCustomer.RowCount; i++)
                    {
                        int detailID = TextUtils.ToInt(grvDetailCustomer.GetRowCellValue(i, colDetailCusID));
                        int problemID = TextUtils.ToInt(grvDetailCustomer.GetRowCellValue(i, colProblemCusDe));
                        if (problemID == -1)
                        {
                            List<ProjectHistoryProblemModel> lst = SQLHelper<ProjectHistoryProblemModel>.FindByAttribute("ProjectID", model.ProjectID);
                            ProjectHistoryProblemModel newProblem = new ProjectHistoryProblemModel()
                            {
                                ProjectID = TextUtils.ToInt(model.ProjectID),
                                STT = lst.Count + 1,
                                ContentError = TextUtils.ToString(grvDetailCustomer.GetRowCellValue(i, colContentCusDe)),
                                Reason = TextUtils.ToString(grvDetailCustomer.GetRowCellValue(i, colResultCusDe)),
                                Remedies = "",
                                DateProblem = DateTime.Now
                            };
                            problemID = SQLHelper<ProjectHistoryProblemModel>.Insert(newProblem).ID;
                        }

                        MeetingMinutesDetailsModel modelDetail = SQLHelper<MeetingMinutesDetailsModel>.FindByID(detailID);
                        modelDetail.MeetingMinutesID = model.ID;

                        modelDetail.PlanDate = TextUtils.ToDate2(grvDetailCustomer.GetRowCellValue(i, colPlanCusDe));
                        modelDetail.DetailContent = TextUtils.ToString(grvDetailCustomer.GetRowCellValue(i, colContentCusDe));
                        modelDetail.DetailResult = TextUtils.ToString(grvDetailCustomer.GetRowCellValue(i, colResultCusDe));
                        modelDetail.ProjectHistoryProblemID = problemID;
                        modelDetail.EmployeeID = -1;
                        modelDetail.IsEmployee = false;
                        modelDetail.CustomerName = TextUtils.ToString(grvDetailCustomer.GetRowCellValue(i, colNameCusDe));
                        modelDetail.PhoneNumber = TextUtils.ToString(grvDetailCustomer.GetRowCellValue(i, colSdtCusDe));
                        modelDetail.Note = TextUtils.ToString(grvDetailCustomer.GetRowCellValue(i, colNoteCusDe));
                        modelDetail.IsDeleted = false;

                        if (modelDetail.ID > 0) SQLHelper<MeetingMinutesDetailsModel>.Update(modelDetail);
                        else modelDetail.ID = SQLHelper<MeetingMinutesDetailsModel>.Insert(modelDetail).ID;
                    }
                }


                //PQ.Chien - UPDATE - 14/07/2025
                await UploadFile(meetingMinutes.ID);
                RemoveFile();


                return true;
            }
        }

        private bool CheckValidate()
        {

            List<int> emIDs = new List<int>();
            DateTime? dateStartValue = TextUtils.ToDate4(dtpDateStart.EditValue);
            DateTime? dateEndValue = TextUtils.ToDate4(dtpDateEnd.EditValue);
            grvAttendance.FocusedRowHandle = -1;
            grvDetails.FocusedRowHandle = -1;

            grvCustomer.FocusedRowHandle = -1;
            grvDetailCustomer.FocusedRowHandle = -1;

            int meetingTypeID = TextUtils.ToInt(cboMeetingType.EditValue);
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            string meetingTitle = txtTitle.Text;


            if (meetingTypeID <= 0)
            {
                MessageBox.Show("Vui lòng nhập Loại cuộc họp!", "Thông báo!");
                return false;
            }

            if (projectID <= 0)
            {
                MessageBox.Show("Vui lòng nhập Dự án!", "Thông báo!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(meetingTitle))
            {
                MessageBox.Show("Vui lòng nhập Tiêu đề biên bản cuộc họp!", "Thông báo!");
                return false;
            }

            if (!dateStartValue.HasValue)
            {
                MessageBox.Show("Vui lòng nhập Thời gian bắt đầu!", "Thông báo!");
                return false;
            }

            if (!dateEndValue.HasValue)
            {
                MessageBox.Show("Vui lòng nhập Thời gian kết thúc!", "Thông báo!");
                return false;
            }

            if (dateStartValue.Value >= dateEndValue.Value)
            {
                MessageBox.Show("Thời gian kết thúc phải lớn hơn thời gian bắt đầu!", "Thông báo!");
                return false;
            }


            for (int i = 0; i < grvAttendance.RowCount; i++)
            {
                int emID = TextUtils.ToInt(grvAttendance.GetRowCellValue(i, colAttendanceEmployeeID));
                string fullName = TextUtils.ToString(grvAttendance.GetRowCellValue(i, colAttendanceCustomerName));
                if (string.IsNullOrWhiteSpace(fullName))
                {
                    MessageBox.Show($"Vui lòng nhập Họ tên tại dòng [{i + 1}] Nhân viên tham gia!", "Thông báo!");
                    return false;
                }
                if (!emIDs.Contains(emID)) emIDs.Add(emID);
                else
                {
                    MessageBox.Show($"Nhân viên số {i + 1} đã tồn tại vui lòng kiểm tra lại!", "Thông báo ");
                    return false;
                }
            }

            if (meetingTypeID == 2)
            {
                for (int i = 0; i < grvCustomer.RowCount; i++)
                {

                    string fullName = TextUtils.ToString(grvCustomer.GetRowCellValue(i, colNameCus));
                    if (string.IsNullOrWhiteSpace(fullName))
                    {
                        MessageBox.Show($"Vui lòng nhập Họ tên tại dòng [{i + 1}] Khách hàng tham gia!", "Thông báo!");
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        //PQ.Chiến - UPDATE - 15/07/2025
        private void grvMeetingMinutesFile_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvMeetingMinutesFile.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colSTT && info.HitTest == GridHitTest.Column)
                {
                    OpenFileDialog o = new OpenFileDialog();
                    o.Multiselect = true;
                    if (o.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string file in o.FileNames)
                        {
                            FileInfo fileInfo = new FileInfo(file);
                            MeetingMinutesFileModel fileRequest = new MeetingMinutesFileModel()
                            {
                                FileName = fileInfo.Name,
                                OriginPath = fileInfo.DirectoryName
                            };

                            listFiles.Insert(0, fileRequest);
                            listFileUpload.Add(fileInfo);
                        }
                        LoadFile(listFiles);
                    }


                }
            }
        }

        void LoadFile(List<MeetingMinutesFileModel> listFiles)
        {
            grdMeetingMinutesFile.DataSource = listFiles;
            grvMeetingMinutesFile.RefreshData();
        }

        //private void btnDeleteFile_Click(object sender, EventArgs e)
        //{
        //	int id = TextUtils.ToInt(grvMeetingMinutesFile.GetFocusedRowCellValue("ID"));
        //	string fileName = TextUtils.ToString(grvMeetingMinutesFile.GetFocusedRowCellValue("FileName"));

        //	DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file đính kèm [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //	if (dialog == DialogResult.Yes)
        //	{
        //		grvMeetingMinutesFile.DeleteSelectedRows();
        //		if (id <= 0) return;
        //		MeetingMinutesFileModel file = SQLHelper<MeetingMinutesFileModel>.FindByID(id);
        //		listFileDelete.Add(file);
        //	}
        //}

        public void RemoveFile()
        {
            if (listFileDelete.Count <= 0) return;
            var url = $"http://113.190.234.64:8083/api/Home/removefile?path=";
            //var url = $"http://localhost:8390/api/Home/removefile?path=";

            var client = new HttpClient();
            foreach (var item in listFileDelete)
            {
                item.IsDeleted = true;
                SQLHelper<MeetingMinutesFileModel>.Update(item);
            }
            listFileDelete.Clear();
        }


        public async Task<bool> UploadFile(int modelID)
        {
            try
            {
                if (listFileUpload.Count <= 0) return false;

                //ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathPaymentOrder").FirstOrDefault();
                //if (config == null || string.IsNullOrEmpty(config.KeyValue))
                //{
                //    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                //    return;
                //}

                MeetingMinutesModel model = SQLHelper<MeetingMinutesModel>.FindByID(modelID);
                if (model.ID <= 0) return false;

                ProjectModel prj = (ProjectModel)cboProject.GetSelectedDataRow();
                if (prj == null || !prj.CreatedDate.HasValue) return false;

                //string pathServer = @"\\192.168.1.190\duan\Projects\";
                string pathServer = @"\\192.168.1.190\Software\Test\duan\Projects\";


                //string pathPattern = $@"duan\Projects\{prj.CreatedDate.Value.Year}\{prj.ProjectCode}\TaiLieuChung\ThongTinKhaoSat";
                string pathPattern = $@"{prj.CreatedDate.Value.Year}\{prj.ProjectCode}\TaiLieuChung\BienBanCuocHop";
                string pathUpload = Path.Combine(pathServer, pathPattern);
                //if (!Directory.Exists(pathUpload))
                //{
                //    Directory.CreateDirectory(pathUpload);
                //}
                var client = new HttpClient();

                List<MeetingMinutesFileModel> listFiles = new List<MeetingMinutesFileModel>();
                foreach (var file in listFileUpload.ToList())
                {
                    MeetingMinutesFileModel fileOrder = new MeetingMinutesFileModel();
                    fileOrder.MeetingMinutesID = model.ID;
                    fileOrder.FileName = file.Name;
                    fileOrder.OriginPath = file.DirectoryName;
                    fileOrder.ServerPath = pathUpload;

                    if (file.Length < 0) continue;

                    var fileStream = new FileStream(file.FullName, FileMode.Open);
                    byte[] bytes = new byte[file.Length];
                    fileStream.Read(bytes, 0, (int)file.Length);
                    var byteArrayContent = new ByteArrayContent(bytes);

                    MultipartFormDataContent content = new MultipartFormDataContent();
                    content.Add(byteArrayContent, "file", file.Name);

                    var url = $"http://113.190.234.64:8083/api/Home/uploadfile?path={pathUpload}";
                    var result = await client.PostAsync(url, content);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SQLHelper<MeetingMinutesFileModel>.Insert(fileOrder);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo");
                return false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMeetingMinutesFile.GetFocusedRowCellValue("ID"));
            string fileName = TextUtils.ToString(grvMeetingMinutesFile.GetFocusedRowCellValue("FileName"));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file đính kèm [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvMeetingMinutesFile.DeleteSelectedRows();
                if (id <= 0) return;
                MeetingMinutesFileModel file = SQLHelper<MeetingMinutesFileModel>.FindByID(id);
                listFileDelete.Add(file);
            }
        }


        private void downloadFile()
        {

            try
            {
                ProjectModel prj = (ProjectModel)cboProject.GetSelectedDataRow();
                if (prj == null || !prj.CreatedDate.HasValue) return;

                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", "BienBanCuocHop");

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }

                string pathPattern = $@"{prj.CreatedDate.Value.Year}/{prj.ProjectCode}/TaiLieuChung/BienBanCuocHop";

                string fileName = TextUtils.ToString(grvMeetingMinutesFile.GetFocusedRowCellValue(colFileName));
                string folderDownload = Path.Combine(pathDownload, fileName);
                string url = $"http://113.190.234.64:8083/api/bienbancuochop/{pathPattern}/{fileName}";

                if (File.Exists(folderDownload))
                {
                    folderDownload = Path.Combine(pathDownload, $"{Path.GetFileNameWithoutExtension(fileName)}_{DateTime.Now.ToString("HHmmss")}.{Path.GetExtension(fileName)}");
                }


                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, folderDownload);
                Process.Start(folderDownload);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void tảiXuốngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            downloadFile();
        }

        private void xemFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = TextUtils.ToString(grvMeetingMinutesFile.GetFocusedRowCellValue(colServerPath));
                string fileName = TextUtils.ToString(grvMeetingMinutesFile.GetFocusedRowCellValue(colFileName));

                Process.Start(Path.Combine(path, fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdCustomer_Click(object sender, EventArgs e)
        {

        }
        //END
    }
}
