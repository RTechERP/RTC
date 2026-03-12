using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BMS
{
    public partial class frmHangMucCongViec : _Forms
    {
        //public int projectID;
        public ProjectModel project = new ProjectModel();

        DataTable dtProjectItem = new DataTable();
        DataTable dtProjectItemDetail = new DataTable();
        List<int> listID = new List<int>();
        List<int> listIDDetail = new List<int>();
        List<ProjectItemDetailModel> listDetail = new List<ProjectItemDetailModel>();

        int idAdd = 0;
        string projectEmployee = "";

        bool isAdding = false;


        public string projectItemCode = "";

        private class Status
        {
            public int value { get; set; }
            public string status { get; set; }

            public Status()
            {

            }

            public Status(int value, string status)
            {
                this.value = value;
                this.status = status;
            }

        }

        public frmHangMucCongViec()
        {
            InitializeComponent();
        }


        private void frmHangMucCongViec_Load(object sender, EventArgs e)
        {
            this.Text += $" - {project.ProjectCode}";
            listDetail = SQLHelper<ProjectItemDetailModel>.ProcedureToList("spGetProjectItemDetail",
                                                                            new string[] { "@ProjectID" },
                                                                            new object[] { project.ID });

            loadUser();
            loadStatus();
            loadTypeProjectItem();
            loadDetail();
            //loadPermission();

            loadStatusApproved();
            //updateItemLate();
            LoadEmployeeRequest();
            LoadEmployeeRequestName();
            loadProjectItem();
        }

        public void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit editor = sender as ComboBoxEdit;
            grvData.SetRowCellValue(grvData.FocusedRowHandle, colStatus, editor.SelectedIndex + 1);
        }


        //Load phân quyền
        bool checkIsPermission(string createdby, int userID, int employeeRequest)
        {
            //List<ProjectEmployeeModel> list = SQLHelper<ProjectEmployeeModel>.ProcedureToList("spGetProjectEmployee",
            //                                    new string[] { "@ProjectID", "@IsDeleted" },
            //                                    new object[] { projectID, 0 });

            //var projectEmployee = list.Where(x => x.EmployeeID == Global.EmployeeID && x.IsLeader == true).ToList();

            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectEmployeePermisstion", "A",
                                                        new string[] { "@ProjectID", "@EmployeeID" },
                                                        new object[] { project.ID, Global.EmployeeID });

            int valueRow = TextUtils.ToInt(dt.Rows[0]["RowNumber"]);

            createdby = TextUtils.ToString(createdby);
            userID = TextUtils.ToInt(userID);
            employeeRequest = TextUtils.ToInt(employeeRequest);

            if (createdby == Global.LoginName.Trim() || userID == Global.UserID || employeeRequest == Global.EmployeeID) //Người tạo hạng mục hoặc người phụ trách hạng mục hoặc người giao việc
            {
                return true;
            }

            if (valueRow > 0 || Global.IsAdmin)
            {
                return true;
            }

            return false;
        }

        //Load chi tiết người tham gia
        void loadDetail()
        {
            int idMaster = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //dtProjectItemDetail = TextUtils.Select($"SELECT * FROM dbo.ProjectItemDetail WHERE ProjectItemID = {idMaster} ORDER BY STT");

            var listData = listDetail.Where(x => x.ID == idMaster).ToList();
            grdDetail.DataSource = listData;

            if (idMaster > 0)
            {
                string createdBy = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCreatedBy)).Trim();
                int userID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUserID));
                int employeeRequest = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeIDRequest));

                btnAddEmployee.Enabled = checkIsPermission(createdBy, userID, employeeRequest);
                btnDeleteEmployee.Enabled = checkIsPermission(createdBy, userID, employeeRequest);


                //if (createdBy != Global.LoginName.Trim())
                //{
                //    btnAddEmployee.Enabled = btnDeleteEmployee.Enabled = false;
                //}
                //else
                //{
                //    btnAddEmployee.Enabled = btnDeleteEmployee.Enabled = true;
                //}
            }
        }

        //Load kiểu của hạng mục công việc
        //void loadTypeProjectItem()
        //{
        //    DataTable dt = TextUtils.Select("select ID, ProjectTypeName from ProjectType");
        //    cboTypeProjetcItem.DisplayMember = "ProjectTypeName";
        //    cboTypeProjetcItem.ValueMember = "ID";
        //    cboTypeProjetcItem.DataSource = dt;
        //}

        void loadTypeProjectItem()
        {
            //   DataTable dt = TextUtils.Select("select ID, ProjectTypeName from ProjectType");
            DataTable dt = TextUtils.GetTable("spGetProjectTypeChildren");

            cboTypeProjetcItem.DisplayMember = "ProjectTypeName";
            cboTypeProjetcItem.ValueMember = "ID";
            cboTypeProjetcItem.DataSource = dt;
        }

        //Load hạng mục công việc
        void loadProjectItem()
        {
            //DataTable dt = TextUtils.Select($"SELECT * FROM ProjectItem WHERE ProjectID = {projectID} ORDER BY ID, TypeProjectItem");
            //dtProjectItem = TextUtils.Select($"SELECT * FROM ProjectItem WHERE ProjectID = {projectID} ORDER BY ID, TypeProjectItem");
            dtProjectItem = TextUtils.LoadDataFromSP("spGetProjectItem", "A", new string[] { "@ProjectID" }, new object[] { project.ID });

            //DataRow row = dtProjectItem.NewRow();
            //row["ProjectEmployeeID"] = 0;
            dtProjectItem.Columns.Add("ProjectEmployeeID");
            //dtProjectItem.Columns.Add("EmployeeRequestName");


            dtProjectItem.AcceptChanges();
            grdData.DataSource = dtProjectItem;

            //Mặc định lọc những hạng mục chưa làm hoặc đang làm
            string filterString = $"([Status] In (0,1))";

            if (!string.IsNullOrWhiteSpace(projectItemCode))
            {
                filterString = $"([{colCodeItem.FieldName}] = '{projectItemCode}')";

                foreach (BandedGridColumn column in bandCommon.Columns.ToList())
                {
                    column.OptionsColumn.AllowEdit = false;
                    column.OptionsColumn.ReadOnly = true;
                }
            }
            grvData.Columns["Status"].FilterInfo = new ColumnFilterInfo(filterString);
        }

        //Load người phụ trách
        void loadUser()
        {
            //DataTable dt = TextUtils.Select("select * from Users where DepartmentID = 2 AND Status <> 1");
            DataTable dt = TextUtils.GetDataTableFromSP("spGetUserProjectItem", new string[] { "@ProjectID" }, new object[] { 0 });

            cboUserID.ValueMember = "UserID";
            cboUserID.DisplayMember = "FullName";
            cboUserID.DataSource = dt;

            cboEmployee.ValueMember = "ID";
            cboEmployee.DisplayMember = "FullName";
            cboEmployee.DataSource = dt;

            //cboEmployeeRequest.ValueMember = "ID";
            //cboEmployeeRequest.DisplayMember = "FullName";
            //cboEmployeeRequest.DataSource = dt;
        }

        void LoadEmployeeRequest()
        {
            List<EmployeeModel> list = SQLHelper<EmployeeModel>.ProcedureToList("spGetEmployeeRequestProjectItem", new string[] { }, new object[] { });
            cboEmployeeRequest.ValueMember = "ID";
            cboEmployeeRequest.DisplayMember = "FullName";
            cboEmployeeRequest.DataSource = list;
        }

        //Load trạng thái
        void loadStatus()
        {
            List<Status> statuses = new List<Status>()
            {
                new Status(0,"Chưa làm"),
                new Status(1,"Đang làm"),
                new Status(2,"Hoàn thành"),
                new Status(3,"Pending")
            };

            cboStatus.ValueMember = "value";
            cboStatus.DisplayMember = "status";
            cboStatus.DataSource = statuses;

        }

        //Load trạng thái duyệt
        void loadStatusApproved()
        {
            List<Status> statuses = new List<Status>()
            {
                new Status(0,"Chờ duyệt kế hoạch"),
                new Status(1,"Duyệt kế hoạch"),
                new Status(2,"Chờ duyệt thực tế"),
                new Status(3,"Duyệt thực tế")
            };

            cboIsApproved.ValueMember = "value";
            cboIsApproved.DisplayMember = "status";
            cboIsApproved.DataSource = statuses;

        }


        void LoadEmployeeRequestName()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployeeRequestID.DataSource = dt;
            cboEmployeeRequestID.DisplayMember = "Code";
            cboEmployeeRequestID.ValueMember = "ID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<int> listSTT = new List<int>();
            for (int i = 0; i < grvData.RowCount; i++)
            {
                string strStt = TextUtils.ToString(grvData.GetRowCellValue(i, colSTT));
                if (!strStt.Equals("."))
                {
                    int stt = 0;
                    listSTT.Add(stt);
                }

            }
            int userID = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colUserID));
            MyLib.AddNewRow(grdData, grvData);

            grvData.SetFocusedRowCellValue(colSTT, listSTT.Max() + 1);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //var focusedRowHandle = grvData.FocusedRowHandle;
            //int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //string groupFile = TextUtils.ToString(grvData.GetFocusedRowCellValue(colGroupFileCode));
            //string projectCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectCode));
            //if (MessageBox.Show(string.Format("Bạn có muốn xóa hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    ProjectItemBO.Instance.Delete(ID);
            //    grvData.DeleteSelectedRows();

            //}

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //grvData.FocusedRowHandle = -1;
            //if (validate())
            //{
            //    if (saveData())
            //    {
            //        //dtProjectItem.AcceptChanges();
            //        //dtProjectItemDetail.AcceptChanges();
            //        loadProjectItem();
            //        loadDetail();
            //        grvData.Columns["TypeProjectItem"].SortOrder = ColumnSortOrder.Ascending;
            //    }
            //}

            try
            {
                btnSave.Enabled = false;
                isAdding = true;
                if (saveData())
                {
                    loadProjectItem();
                    loadDetail();
                    grvData.Columns["TypeProjectItem"].SortOrder = ColumnSortOrder.Ascending;

                    dtProjectItem.AcceptChanges();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
            finally
            {
                btnSave.Enabled = true;
                isAdding = false;
            }
        }


        //Lưu hạng mục công việc
        bool saveData()
        {
            grvData.CloseEditor();
            grvData.FocusedRowHandle = -1;
            if (!validate())
            {
                return false;
            }

            DataTable data = (DataTable)grdData.DataSource;
            DataTable dataChange = data?.GetChanges();
            //DataTable dataChange = dtProjectItem?.GetChanges();

            if (dataChange != null)
            {
                var dataLog = dataChange.Select("StatusUpdate = 2");
                SaveLogItem(dataLog);

                dataChange.AcceptChanges();

                //var totalDays = data.AsEnumerable().Sum(x => x.Field<decimal>("TotalDayPlan"));
                foreach (DataRow row in dataChange.Rows)
                {
                    int id = TextUtils.ToInt(row["ID"]);
                    ProjectItemModel projectItem = new ProjectItemModel();
                    if (id > 0)
                    {
                        projectItem = (ProjectItemModel)ProjectItemBO.Instance.FindByPK(id);
                    }
                    projectItem.Status = TextUtils.ToInt(row["Status"]);
                    projectItem.STT = TextUtils.ToString(row["STT"]);
                    projectItem.UserID = TextUtils.ToInt(row["UserID"]);
                    projectItem.ProjectID = project.ID;
                    projectItem.Mission = TextUtils.ToString(row["Mission"]);
                    projectItem.PlanStartDate = TextUtils.ToDate4(row["PlanStartDate"]);
                    projectItem.PlanEndDate = TextUtils.ToDate4(row["PlanEndDate"]);
                    projectItem.ActualStartDate = TextUtils.ToDate4(row["ActualStartDate"]);
                    projectItem.ActualEndDate = TextUtils.ToDate4(row["ActualEndDate"]);
                    projectItem.Note = TextUtils.ToString(row["Note"]);
                    projectItem.TotalDayPlan = TextUtils.ToDecimal(row["TotalDayPlan"]);
                    //projectItem.TotalDayActual = TextUtils.ToDecimal(row["TotalDayActual"]);
                    projectItem.TotalDayActual = 0;
                    if (projectItem.ActualStartDate.HasValue && projectItem.ActualEndDate.HasValue)
                    {
                        projectItem.TotalDayActual = (decimal)(projectItem.ActualEndDate.Value - projectItem.ActualStartDate.Value).TotalDays;
                    }
                    projectItem.ItemLate = TextUtils.ToInt(row["ItemLate"]);
                    projectItem.PercentItem = TextUtils.ToDecimal(row["PercentItem"]);
                    //projectItem.PercentItem = totalDays <= 0 ? 0 : (projectItem.TotalDayPlan / totalDays) * 100;
                    projectItem.TypeProjectItem = TextUtils.ToInt(row["TypeProjectItem"]);
                    projectItem.PercentageActual = TextUtils.ToInt(row["PercentageActual"]);


                    //projectItem.Code = $"{project.ProjectCode }-{projectItem.STT}";
                    projectItem.EmployeeIDRequest = TextUtils.ToInt(row["EmployeeIDRequest"]);

                    projectItem.IsUpdateLate = TextUtils.ToBoolean(row["IsUpdateLate"]);
                    projectItem.ReasonLate = TextUtils.ToString(row["ReasonLate"]);
                    if (projectItem.ActualEndDate.HasValue)
                    {
                        projectItem.IsApproved = 2;
                    }

                    if (Global.IsLeader <= 0 && Global.EmployeeID != Global.HeadOfDepartment)
                    {
                        projectItem.UpdatedDateActual = TextUtils.ToDate4(row["UpdatedDateActual"]);
                    }

                    projectItem.EmployeeRequestID = TextUtils.ToInt(row[colEmployeeRequestID.FieldName]);
                    projectItem.EmployeeRequestName = TextUtils.ToString(row[colEmployeeRequestName.FieldName]);

                    if (projectItem.ID > 0)
                    {

                        ProjectItemBO.Instance.Update(projectItem);
                    }
                    else
                    {
                        projectItem.Code = loadProjectItemCode(project.ID);
                        projectItem.CreatedBy = Global.LoginName;
                        projectItem.CreatedDate = DateTime.Now;
                        projectItem.ID = (int)ProjectItemBO.Instance.Insert(projectItem);
                    }

                    saveDetail(id, projectItem.ID);

                    if (string.IsNullOrEmpty(projectItem.Mission))
                    {

                        //if (projectItem.ID > 0)
                        //{
                        //    ProjectItemBO.Instance.Update(projectItem);
                        //}
                        //else
                        //{
                        //    projectItem.ID = (int)ProjectItemBO.Instance.Insert(projectItem);
                        //}

                        //saveDetail(id, projectItem.ID);
                        //return false;
                    }
                    //else
                    //{
                    //    return false;
                    //}
                }

                //return true;

            }

            //for (int i = 0; i < grvData.RowCount; i++)
            //{

            //    int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
            //    ProjectItemModel projectItem = new ProjectItemModel();
            //    if (id > 0)
            //    {
            //        projectItem = (ProjectItemModel)ProjectItemBO.Instance.FindByPK(id);
            //    }
            //    projectItem.Status = TextUtils.ToInt(grvData.GetRowCellValue(i, colStatus));
            //    projectItem.STT = TextUtils.ToString(grvData.GetRowCellValue(i, colSTT));
            //    projectItem.UserID = TextUtils.ToInt(grvData.GetRowCellValue(i, colUserID));
            //    projectItem.ProjectID = projectID;
            //    projectItem.Mission = TextUtils.ToString(grvData.GetRowCellValue(i, colMission));
            //    projectItem.PlanStartDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colPlanStartDate));
            //    projectItem.PlanEndDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colPlanEndDate));
            //    projectItem.ActualStartDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colActualStartDate));
            //    projectItem.ActualEndDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colActualEndDate));
            //    projectItem.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));

            //    projectItem.TotalDayPlan = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalDayPlan));
            //    projectItem.TotalDayActual = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalDayActual));
            //    projectItem.ItemLate = TextUtils.ToInt(grvData.GetRowCellValue(i, colItemLate));
            //    projectItem.PercentItem = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPercentItem));

            //    projectItem.TypeProjectItem = TextUtils.ToInt(grvData.GetRowCellValue(i, colTypeProjectItem));
            //    projectItem.PercentageActual = TextUtils.ToInt(grvData.GetRowCellValue(i, colPercentageActual));

            //    if (!string.IsNullOrEmpty(projectItem.Mission))
            //    {
            //        if (projectItem.ID > 0)
            //        {
            //            ProjectItemBO.Instance.Update(projectItem);
            //        }
            //        else
            //        {
            //            projectItem.ID = (int)ProjectItemBO.Instance.Insert(projectItem);
            //        }

            //        saveDetail(id, projectItem.ID);
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}

            if (listID.Count > 0)
            {
                string id = string.Join(",", listID);
                string sql = $"DELETE ProjectItem WHERE ID IN ({id})\n" +
                            $"DELETE dbo.ProjectItemDetail WHERE ProjectItemID IN ({id})";
                TextUtils.ExcuteSQL(sql);
                listID.Clear();
            }

            dtProjectItem.AcceptChanges();
            UpdatePercentItem();
            return true;
        }

        void saveDetail(int id, int idMaster)
        {
            var listData = listDetail.Where(x => x.ProjectItemID == id).ToList();
            foreach (var item in listData)
            {
                item.ProjectItemID = idMaster;
                if (item.ID > 0)
                {
                    SQLHelper<ProjectItemDetailModel>.Update(item);
                }
                else
                {
                    SQLHelper<ProjectItemDetailModel>.Insert(item);
                }
            }
            //for (int i = 0; i < grvDetail.RowCount; i++)
            //{
            //    if (idMaster <= 0)
            //    {
            //        continue;
            //    }

            //    int id = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colIDDetail));
            //    ProjectItemDetailModel model = new ProjectItemDetailModel();
            //    if (id > 0)
            //    {
            //        model = SQLHelper<ProjectItemDetailModel>.FindByID(id);
            //    }

            //    model.ProjectItemID = idMaster;
            //    model.STT = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colSTTDetail));
            //    model.EmployeeID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colEmployeeID));

            //    if (model.ID > 0)
            //    {
            //        SQLHelper<ProjectItemDetailModel>.Update(model);
            //    }
            //    else
            //    {
            //        SQLHelper<ProjectItemDetailModel>.Insert(model);
            //    }
            //}
        }


        void UpdatePercentItem()
        {
            try
            {
                decimal totalDays = TextUtils.ToDecimal(dtProjectItem.AsEnumerable().Sum(x => x.Field<decimal?>("TotalDayPlan")));
                foreach (DataRow row in dtProjectItem.Rows)
                {
                    int id = TextUtils.ToInt(row["ID"]);
                    ProjectItemModel item = SQLHelper<ProjectItemModel>.FindByID(id);
                    if (item != null)
                    {
                        item.PercentItem = totalDays > 0 ? (item.TotalDayPlan / totalDays) * 100 : 0;
                        SQLHelper<ProjectItemModel>.Update(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        bool validate()
        {
            grvData.CloseEditor();

            //return Global.IsAdmin;
            DateTime dateFixed = new DateTime(2023, 12, 1, 0, 0, 0);
            for (int i = 0; i < grvData.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                ProjectItemModel projectItem = SQLHelper<ProjectItemModel>.FindByID(id);

                string createdBy = TextUtils.ToString(grvData.GetRowCellValue(i, colCreatedBy));
                int userID = TextUtils.ToInt(grvData.GetRowCellValue(i, colUserID));
                int employeeRequest = TextUtils.ToInt(grvData.GetRowCellValue(i, colEmployeeIDRequest));

                int isApproved = TextUtils.ToInt(grvData.GetRowCellValue(i, colIsApproved));
                //if (!checkIsPermission(createdBy, userID, employeeRequest))
                //{
                //    continue;
                //}
                if (projectItem != null && projectItem.ID > 0)
                {
                    if (TextUtils.ToString(projectItem.CreatedBy).Trim().ToLower() != Global.LoginName.ToLower() && TextUtils.ToInt(projectItem.UserID) != Global.UserID) continue;
                }
                if (isApproved == 3) continue;

                int type = TextUtils.ToInt(grvData.GetRowCellValue(i, colTypeProjectItem));
                int status = TextUtils.ToInt(grvData.GetRowCellValue(i, colStatus));
                string stt = TextUtils.ToString(grvData.GetRowCellValue(i, colSTT));
                string mission = TextUtils.ToString(grvData.GetRowCellValue(i, colMission)).Trim();
                string employeeRequestName = TextUtils.ToString(grvData.GetRowCellValue(i, colEmployeeRequestName)).Trim();

                DateTime? planDS = TextUtils.ToDate4(grvData.GetRowCellValue(i, colPlanStartDate));
                DateTime? planlDE = TextUtils.ToDate4(grvData.GetRowCellValue(i, colPlanEndDate));

                DateTime? actualDS = TextUtils.ToDate4(grvData.GetRowCellValue(i, colActualStartDate));
                DateTime? actualDE = TextUtils.ToDate4(grvData.GetRowCellValue(i, colActualEndDate));


                string reasonLate = TextUtils.ToString(grvData.GetRowCellValue(i, colReasonLate));
                int updateStatus = TextUtils.ToInt(grvData.GetRowCellValue(i, colStatusUpdate));

                DateTime? dateCreated = TextUtils.ToDate4(grvData.GetRowCellValue(i, colCreatedDate));
                if (id <= 0 || (dateCreated.HasValue && dateCreated.Value.Date > dateFixed.Date))
                {
                    if (type <= 0)
                    {
                        MessageBox.Show($"Vui lòng nhập Kiểu!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (employeeRequest <= 0)
                    {
                        MessageBox.Show($"Vui lòng nhập Người giao việc!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }


                if (string.IsNullOrEmpty(mission))
                {

                    MessageBox.Show($"Vui lòng nhập Công việc!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (userID <= 0)
                {
                    //grvData.FocusedColumn = colMission;
                    //grvData.FocusedRowHandle = i;
                    MessageBox.Show($"Vui lòng nhập Người phụ trách!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }


                if (!planDS.HasValue)
                {
                    MessageBox.Show($"Vui lòng nhập Ngày bắt đầu (KẾ HOẠCH)!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!planlDE.HasValue)
                {
                    MessageBox.Show($"Vui lòng nhập Ngày kết thúc (KẾ HOẠCH)!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (updateStatus == 2)
                {
                    //ProjectItemModel projectItem = SQLHelper<ProjectItemModel>.FindByID(id);
                    if (TextUtils.ToString(projectItem.ReasonLate).ToLower().Trim() == TextUtils.ToString(reasonLate).ToLower().Trim())
                    {
                        BandedGridColumn column = (BandedGridColumn)grvData.FocusedColumn;
                        string bandCaption = !string.IsNullOrEmpty(column.OwnerBand.Caption) ? $" ({column.OwnerBand.Caption})" : "";
                        DialogResult dialog = MessageBox.Show($"Bạn vừa thay đổi [{column.Caption}]{bandCaption} - Stt: {stt}.\nBạn có muốn thêm Lý do phát sinh không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            AddProblemItem(id);
                        }
                        else
                        {
                            DataTable data = (DataTable)grdData.DataSource;
                            var arrRow = data.Select($"ID = {id}");
                            if (arrRow != null)
                            {
                                DataRow dataRow = arrRow[0];
                                dataRow["Mission"] = dataRow["Mission", DataRowVersion.Original];
                                dataRow["PlanStartDate"] = dataRow["PlanStartDate", DataRowVersion.Original];
                                dataRow["TotalDayPlan"] = dataRow["TotalDayPlan", DataRowVersion.Original];
                                dataRow["PlanEndDate"] = dataRow["PlanEndDate", DataRowVersion.Original];
                                dataRow["StatusUpdate"] = dataRow["StatusUpdate", DataRowVersion.Original];

                                updateItemLate();
                            }
                        }
                        return false;
                    }
                }

                if (status == 2)
                {
                    if (!actualDS.HasValue)
                    {
                        //grvData.FocusedColumn = colActualEndDate;
                        //grvData.FocusedRowHandle = i;
                        MessageBox.Show($"Vui lòng nhập Ngày bắt đầu (THỰC TẾ)!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (!actualDE.HasValue)
                    {
                        //grvData.FocusedColumn = colActualEndDate;
                        //grvData.FocusedRowHandle = i;
                        MessageBox.Show($"Vui lòng nhập Ngày kết thúc (THỰC TẾ)!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (actualDS.HasValue && actualDE.HasValue)
                    {
                        double timeSpan = (actualDE.Value.Date - actualDS.Value.Date).TotalDays;
                        if (timeSpan < 0)
                        {
                            //grvData.FocusedRowHandle = i;
                            MessageBox.Show($"Ngày Kết thúc phải lớn hơn Ngày bắt đầu (THỰC TẾ)!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }

                DateTime dateUpdate = new DateTime(2025, 04, 12, 17, 30, 00);
                if (string.IsNullOrEmpty(employeeRequestName))
                {

                    if (projectItem.CreatedDate.HasValue && projectItem.CreatedDate.Value >= dateUpdate)
                    {
                        MessageBox.Show($"Vui lòng nhập Tên người yêu cầu!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else if (!projectItem.CreatedDate.HasValue)
                    {
                        MessageBox.Show($"Vui lòng nhập Tên người yêu cầu!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                }
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Công việc chưa lưu sẽ bị mất. \nBạn có chắc muốn hủy không ?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                loadProjectItem();
            }
        }

        private void btnDeleteRepo_Click(object sender, EventArgs e)
        {
            int row = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int isApproved = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIsApproved));
            //string isApprovedText = TextUtils.ToString(grvData.GetFocusedRowCellValue(colIsApprovedText));
            string isApprovedText = cboIsApproved.GetDisplayText(isApproved);
            int userID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUserID));

            //MessageBox.Show($"Hạng mục này đang {isApprovedText}!", "Thông báo");
            if (ID > 0)
            {

                //string createdBy = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCreatedBy));

                //if (!checkIsPermission(createdBy))
                //{
                //    MessageBox.Show("Bạn không được xoá công việc này!", "Thông báo");
                //    return;
                //}


                if (!Global.IsAdmin)
                {
                    bool isTBP = (Global.EmployeeID == Global.HeadOfDepartment || Global.EmployeeID == 54);
                    bool isPBP = (Global.PositionCode == "CV57" || Global.PositionCode == "CV28");

                    if (isApproved > 0)
                    {
                        MessageBox.Show($"Hạng mục này đang {isApprovedText}!", "Thông báo");
                        return;
                    }
                    else if (!isTBP && !isPBP)
                    {
                        MessageBox.Show("Bạn không thể xoá.\nVui lòng liên hệ TBP", "Thông báo");
                        return;
                    }


                    //EmployeeModel employee = SQLHelper<EmployeeModel>.FindByAttribute("UserID", userID).FirstOrDefault();
                    //if (employee != null)
                    //{
                    //    if (employee.DepartmentID != Global.DepartmentID) //Check cùng phòng ban
                    //    {
                    //        if ((Global.PositionCode != "CV57" || Global.PositionCode != "CV28") && Global.EmployeeID != Global.HeadOfDepartment)
                    //        {
                    //            MessageBox.Show("Bạn không thể xoá.\nVui lòng liên hệ TBP", "Thông báo");
                    //            return;
                    //        }
                    //    }

                    //}

                }

            }

            if (MessageBox.Show(string.Format("Bạn có muốn xóa hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listID.Add(ID);
                grvData.DeleteSelectedRows();

                grvData.FocusedRowHandle = row + 1;
                decimal total = TextUtils.ToDecimal(grvData.Columns["TotalDayPlan"].SummaryItem.SummaryValue);
                if (total > 0)
                {
                    for (int i = 0; i < grvData.RowCount; i++)
                    {
                        decimal percent = (TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalDayPlan)) / total) * 100;
                        grvData.SetRowCellValue(i, colPercentItem, percent);
                    }
                }
            }

        }


        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colSTT && info.HitTest == GridHitTest.Column)
                {
                    grvData.FocusedRowHandle = -1;

                    List<int> listSTT = new List<int>();
                    DataTable dt = (DataTable)grdData.DataSource;
                    dt.AcceptChanges();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string strSTT = TextUtils.ToString(dt.Rows[i]["STT"]);
                        if (!strSTT.Contains("."))
                        {
                            int stt = TextUtils.ToInt(dt.Rows[i]["STT"]);
                            listSTT.Add(stt);
                        }
                    }

                    int userID = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colUserID));
                    int typeProjectItem = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colTypeProjectItem));
                    int employeeRequest = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colEmployeeIDRequest));


                    DataRow dtrow = dt.NewRow();
                    dtrow["STT"] = listSTT.Count > 0 ? (listSTT.Max() + 1).ToString() : "1";
                    dtrow["UserID"] = userID;
                    dtrow["Status"] = 0;
                    dtrow["TypeProjectItem"] = typeProjectItem;
                    dtrow["ID"] = idAdd--;
                    dtrow["StatusUpdate"] = 1;
                    dtrow["EmployeeIDRequest"] = employeeRequest;
                    dtrow["IsApproved"] = 0;
                    dtrow["Code"] = $"{project.ProjectCode}_{dt.Rows.Count + 1}";
                    dt.Rows.Add(dtrow);

                    grdData.DataSource = dt;
                    grvData.FocusedRowHandle = grvData.RowCount - 1;
                    grvData.FocusedColumn = colMission;
                    //grvData.Columns["TypeProjectItem"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                }
            }
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Black;

                decimal itemLate = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colItemLate));
                int itemLateActual = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, "ItemLateActual"));

                if (itemLate == 1 || itemLateActual == 1)
                {
                    e.Appearance.BackColor = Color.Orange;
                    e.HighPriority = true;
                }

                if (itemLate == 2 || itemLateActual == 2)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                    e.HighPriority = true;
                }
            }
        }


        private void frmHangMucCongViec_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (isAdding)
            {
                MessageBox.Show("Đang lưu dữ liệu.\nVui lòng chờ giây lát...", "Thông báo");
                e.Cancel = true;
            }
            grvData.FocusedRowHandle = -1;
            DataTable dataChange = dtProjectItem.GetChanges();

            if (dataChange != null)
            {
                DialogResult dialog = MessageBox.Show("Bạn có muốn lưu những thay đổi không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    //btnCloseAndSave_Click(null, null);
                    if (saveData())
                    {

                        e.Cancel = false;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        e.Cancel = true;
                    }

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


        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (Lib.LockEvents)
                return;
            try
            {
                Lib.LockEvents = true;
                grvData.CloseEditor();
                dtProjectItem.AcceptChanges();
                if (e.RowHandle >= 0)
                {
                    //if (e.Column == colTypeProjectItem)
                    //{
                    //    grvData.Columns["TypeProjectItem"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    //}
                    grvData.FocusedRowHandle = -1;
                    //decimal total = 0;

                    //Set ngày dự kiến và tổng số ngày
                    DateTime? dateStart = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colPlanStartDate));
                    DateTime? dateEnd = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colPlanEndDate));
                    int totalDay = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colTotalDayPlan));

                    if (e.Column == colPlanStartDate)
                    {
                        if (dateStart.HasValue)
                        {
                            if (totalDay > 0)
                            {
                                DateTime dateValue = dateStart.Value.AddDays(totalDay - 1);
                                grvData.SetRowCellValue(e.RowHandle, colPlanEndDate, dateValue);
                            }
                            else if (dateEnd.HasValue)
                            {
                                decimal timeSpan = (decimal)(dateEnd.Value - dateStart.Value).TotalDays + 1;
                                grvData.SetRowCellValue(e.RowHandle, colTotalDayPlan, (int)timeSpan);
                            }
                        }

                        //total = TextUtils.ToDecimal(grvData.Columns["TotalDayPlan"].SummaryItem.SummaryValue);
                    }
                    else if (e.Column == colTotalDayPlan)
                    {
                        if (totalDay > 0)
                        {
                            if (dateStart.HasValue)
                            {
                                DateTime dateValue = dateStart.Value.AddDays(totalDay - 1);
                                grvData.SetRowCellValue(e.RowHandle, colPlanEndDate, dateValue);
                            }
                            else if (dateEnd.HasValue)
                            {
                                DateTime dateValue = dateEnd.Value.AddDays(-(totalDay - 1));
                                grvData.SetRowCellValue(e.RowHandle, colPlanStartDate, dateValue);
                            }
                        }

                        //total = TextUtils.ToDecimal(grvData.Columns["TotalDayPlan"].SummaryItem.SummaryValue);
                    }
                    else if (e.Column == colPlanEndDate)
                    {
                        if (dateEnd.HasValue)
                        {
                            if (dateStart.HasValue)
                            {
                                decimal timeSpan = (decimal)(dateEnd.Value - dateStart.Value).TotalDays + 1;
                                grvData.SetRowCellValue(e.RowHandle, colTotalDayPlan, (int)timeSpan);

                            }
                            else if (totalDay > 0)
                            {
                                DateTime dateValue = dateEnd.Value.AddDays(-(totalDay - 1));
                                grvData.SetRowCellValue(e.RowHandle, colPlanStartDate, dateValue);
                            }
                        }

                        //total = TextUtils.ToDecimal(grvData.Columns["TotalDayPlan"].SummaryItem.SummaryValue);
                    }


                    //Tính % của hạng mục công việc
                    var a = dtProjectItem.Rows[e.RowHandle]["TotalDayPlan"];
                    decimal? totalDays = dtProjectItem.AsEnumerable().Sum(x => x.Field<decimal?>("TotalDayPlan"));

                    if (totalDays > 0)
                    {
                        for (int i = 0; i < grvData.RowCount; i++)
                        {
                            decimal? percent = (TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalDayPlan)) / totalDays) * 100;
                            grvData.SetRowCellValue(i, colPercentItem, percent);
                        }
                    }

                    //Check hạng mục công việc quá hạn
                    if (e.Column == colPlanStartDate || e.Column == colPlanEndDate || e.Column == colActualStartDate || e.Column == colActualEndDate || e.Column == colTotalDayPlan)
                    {
                        //string strPlanDS = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colPlanStartDate)).Trim();
                        //string strPlanDE = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colPlanEndDate)).Trim();
                        //string strActualDS = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colActualStartDate)).Trim();
                        //string strActualDE = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colActualEndDate)).Trim();

                        //if (!DateTime.TryParse(strPlanDS, out DateTime planDS))
                        //{
                        //    grvData.SetRowCellValue(e.RowHandle, colItemLate, 0);
                        //    grvData.SetRowCellValue(e.RowHandle, colTotalDayActual, 0);
                        //}

                        //if (!DateTime.TryParse(strPlanDE, out DateTime planDE))
                        //{
                        //    grvData.SetRowCellValue(e.RowHandle, colItemLate, 0);
                        //    grvData.SetRowCellValue(e.RowHandle, colTotalDayActual, 0);
                        //}

                        //if (!DateTime.TryParse(strActualDS, out DateTime actualDS))
                        //{
                        //    grvData.SetRowCellValue(e.RowHandle, colItemLate, 0);
                        //    grvData.SetRowCellValue(e.RowHandle, colTotalDayActual, 0);
                        //}

                        //if (!DateTime.TryParse(strActualDE, out DateTime actualDE))
                        //{
                        //    grvData.SetRowCellValue(e.RowHandle, colItemLate, 0);
                        //    grvData.SetRowCellValue(e.RowHandle, colTotalDayActual, 0);
                        //}


                        //decimal timeSpanPlan = TextUtils.ToDecimal((planDE - planDS).TotalDays + 1);

                        decimal timeSpanActual = 0;
                        int itemLate = 0;


                        DateTime? planStart = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colPlanStartDate));
                        DateTime? planEnd = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colPlanEndDate));
                        DateTime? actualStart = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colActualStartDate));
                        DateTime? actualEnd = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colActualEndDate));

                        //Nếu đã có ngày bắt đầu thực tế và chưa có ngày kết thúc thực tế
                        //Nếu ngày bắt đầu thực tế > ngày kết thúc dự kiến --> Failed
                        if (actualStart.HasValue && !actualEnd.HasValue && planEnd.HasValue)
                        {
                            if ((actualStart.Value.Date - planEnd.Value.Date).TotalDays > 0)
                            {
                                itemLate = 2;
                            }
                            else if ((DateTime.Now.Date - planEnd.Value.Date).TotalDays > 0)
                            {
                                itemLate = 2;
                            }

                        }

                        //Nếu đã có ngày bắt đầu thực tế và ngày kết thúc thực tế
                        //Nếu ngày kết thúc thực tế > ngày kết thúc dự kiến --> Chậm
                        if (actualStart.HasValue && actualEnd.HasValue && planEnd.HasValue)
                        {
                            if ((actualEnd.Value.Date - planEnd.Value.Date).TotalDays > 0)
                            {
                                itemLate = 1;
                            }

                            timeSpanActual = TextUtils.ToDecimal((actualStart.Value.Date - actualEnd.Value.Date).TotalDays + 1);
                        }

                        //Nếu chưa có ngày bắt đầu thực tế và ngày kết thúc thực tế
                        //Nếu ngày hiện tại > ngày kết thúc dự kiến --> Failed
                        if (!actualStart.HasValue && !actualEnd.HasValue && planEnd.HasValue)
                        {
                            if ((DateTime.Now.Date - planEnd.Value.Date).TotalDays > 0)
                            {
                                itemLate = 2;
                            }
                        }

                        //Nếu chỉ có ngày bắt đầu dự kiến
                        if (planStart.HasValue && !planEnd.HasValue && !actualStart.HasValue && !actualEnd.HasValue)
                        {
                            if ((DateTime.Now.Date - planStart.Value.Date).TotalDays > 0)
                            {
                                itemLate = 2;
                            }
                        }

                        grvData.SetRowCellValue(e.RowHandle, colItemLate, itemLate);
                        grvData.SetRowCellValue(e.RowHandle, colTotalDayActual, timeSpanActual);
                    }


                    //Check ngày cập nhật ngày kết thúc thực tế
                    if (e.Column == colActualStartDate || e.Column == colActualEndDate)
                    {
                        DateTime? Start = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colActualStartDate));
                        DateTime? End = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colActualEndDate));
                        if (End.HasValue)
                        {
                            grvData.SetRowCellValue(e.RowHandle, colUpdatedDateActual, DateTime.Now);
                            grvData.SetRowCellValue(e.RowHandle, colStatus, 2);

                            DateTime? planEnd = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colPlanEndDate));
                            if (planEnd.HasValue)
                            {
                                if ((End.Value - planEnd.Value).TotalDays > 0)
                                {
                                    grvData.SetRowCellValue(e.RowHandle, colIsUpdateLate, 1);
                                }
                            }

                        }
                        else
                        {
                            grvData.SetRowCellValue(e.RowHandle, colUpdatedDateActual, (DateTime?)null);

                            if (Start.HasValue)
                            {
                                grvData.SetRowCellValue(e.RowHandle, colStatus, 1);
                            }
                            else
                            {
                                grvData.SetRowCellValue(e.RowHandle, colStatus, 0);
                            }

                        }
                    }

                    //Update ngày khi chọn trạng thái
                    if (e.Column == colStatus)
                    {
                        int status = TextUtils.ToInt(e.Value);
                        if (status == 1)
                        {
                            grvData.SetRowCellValue(e.RowHandle, colActualStartDate, DateTime.Now);
                        }
                        else if (status == 2)
                        {
                            grvData.SetRowCellValue(e.RowHandle, colActualEndDate, DateTime.Now);
                        }
                    }

                    //Cập nhật lịch sử sửa đổi
                    //int statusUpdate = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatusUpdate));
                    //if (statusUpdate != 2 && statusUpdate != 1)
                    //{
                    //    grvData.SetRowCellValue(e.RowHandle,colStatusUpdate, 2);
                    //}
                }
            }
            finally
            {
                Lib.LockEvents = false;
            }

        }

        private void SaveLogItem(DataRow[] dataRows)
        {
            foreach (DataRow row in dataRows)
            {
                int id = TextUtils.ToInt(row["ID"]);
                ProjectItemModel projectItem = SQLHelper<ProjectItemModel>.FindByID(id);

                if (projectItem.ID <= 0)
                {
                    continue;
                }
                ProjectItemLogModel log = new ProjectItemLogModel();
                log.ProjectItemID = projectItem.ID;
                log.Status = projectItem.Status;
                log.ContentLog = projectItem.Mission;
                log.Note = projectItem.Note;
                log.DateStart = projectItem.PlanStartDate;
                log.DateEnd = projectItem.PlanEndDate;

                ProjectItemLogBO.Instance.Insert(log);

                projectItem.IsApproved = 0;
                ProjectItemBO.Instance.Update(projectItem);
            }
        }

        private void grdData_ProcessGridKey(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Tab)
            //{
            //    if (grvData.FocusedColumn == colMission)
            //    {
            //        grvData.FocusedColumn = colPlanStartDate;
            //    }
            //}
        }

        private void btnCloseAndSave_Click(object sender, EventArgs e)
        {
            //grvData.FocusedRowHandle = -1;
            //if (validate())
            //{
            //    if (saveData())
            //    {
            //        dtProjectItem.AcceptChanges();
            //        dtProjectItemDetail.AcceptChanges();
            //        this.DialogResult = DialogResult.OK;
            //    }
            //}

            if (saveData())
            {
                dtProjectItem.AcceptChanges();
                dtProjectItemDetail.AcceptChanges();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            btnCloseAndSave_Click(null, null);
        }

        private void dragDropEvents1_DragDrop(object sender, DevExpress.Utils.DragDrop.DragDropEventArgs e)
        {
            GridView targetGrid = e.Target as GridView;
            GridView sourceGrid = e.Source as GridView;
            if (e.Action == DragDropActions.None || targetGrid != sourceGrid)
                return;
            DataTable sourceTable = sourceGrid.GridControl.DataSource as DataTable;

            Point hitPoint = targetGrid.GridControl.PointToClient(Cursor.Position);
            GridHitInfo hitInfo = targetGrid.CalcHitInfo(hitPoint);

            int[] sourceHandles = e.GetData<int[]>();

            int targetRowHandle = hitInfo.RowHandle;
            int targetRowIndex = targetGrid.GetDataSourceRowIndex(targetRowHandle);

            List<DataRow> draggedRows = new List<DataRow>();
            foreach (int sourceHandle in sourceHandles)
            {
                int oldRowIndex = sourceGrid.GetDataSourceRowIndex(sourceHandle);
                DataRow oldRow = sourceTable.Rows[oldRowIndex];
                draggedRows.Add(oldRow);
            }

            int newRowIndex;

            switch (e.InsertType)
            {
                case InsertType.Before:
                    newRowIndex = targetRowIndex > sourceHandles[sourceHandles.Length - 1] ? targetRowIndex - 1 : targetRowIndex;
                    for (int i = draggedRows.Count - 1; i >= 0; i--)
                    {
                        DataRow oldRow = draggedRows[i];
                        DataRow newRow = sourceTable.NewRow();
                        newRow.ItemArray = oldRow.ItemArray;
                        sourceTable.Rows.Remove(oldRow);
                        sourceTable.Rows.InsertAt(newRow, newRowIndex);
                    }

                    ////Set lại stt
                    //for (int i = 0; i < grvData.RowCount; i++)
                    //{
                    //    grvData.SetRowCellValue(i, colSTT, (i + 1));
                    //}
                    break;
                case InsertType.After:
                    newRowIndex = targetRowIndex < sourceHandles[0] ? targetRowIndex + 1 : targetRowIndex;
                    for (int i = 0; i < draggedRows.Count; i++)
                    {
                        DataRow oldRow = draggedRows[i];
                        DataRow newRow = sourceTable.NewRow();
                        newRow.ItemArray = oldRow.ItemArray;
                        sourceTable.Rows.Remove(oldRow);
                        sourceTable.Rows.InsertAt(newRow, newRowIndex);
                    }

                    //Set lại stt
                    for (int i = 0; i < grvData.RowCount; i++)
                    {
                        grvData.SetRowCellValue(i, colSTT, (i + 1));
                    }
                    break;
                default:
                    newRowIndex = -1;
                    break;
            }
            int insertedIndex = targetGrid.GetRowHandle(newRowIndex);
            targetGrid.FocusedRowHandle = insertedIndex;
            targetGrid.SelectRow(targetGrid.FocusedRowHandle);
        }

        private void dragDropEvents1_DragOver(object sender, DevExpress.Utils.DragDrop.DragOverEventArgs e)
        {
            DragOverGridEventArgs args = DragOverGridEventArgs.GetDragOverGridEventArgs(e);
            e.InsertType = args.InsertType;
            e.InsertIndicatorLocation = args.InsertIndicatorLocation;
            e.Action = args.Action;
            Cursor.Current = args.Cursor;
            args.Handled = true;
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmInputExcelHMCV frm = new frmInputExcelHMCV();
            frm.ProjectID = project.ID;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectItem();
            }
        }

        private void grvDetail_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                GridHitInfo info = grvDetail.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colSTTDetail && info.HitTest == GridHitTest.Column)
                {
                    grvDetail.FocusedRowHandle = -1;

                    DataTable dt = (DataTable)grdDetail.DataSource;

                    DataRow dtrow = dt.NewRow();
                    dtrow["STT"] = grvDetail.RowCount + 1;
                    dt.Rows.Add(dtrow);

                    grdDetail.DataSource = dt;

                    grvDetail.FocusedRowHandle = grvDetail.RowCount - 1;
                    grvDetail.FocusedColumn = colEmployeeID;
                }
            }
        }

        private void btnDeleteDetail_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvDetail.GetFocusedRowCellValue(colID));
            string fullName = TextUtils.ToString(grvDetail.GetFocusedRowCellDisplayText(colEmployeeID));
            DialogResult dialog = MessageBox.Show(string.Format($"Bạn có muốn xóa nhân viên [{fullName}] không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                listIDDetail.Add(ID);
                grvDetail.DeleteSelectedRows();
                for (int i = 0; i < grvDetail.RowCount; i++)
                {
                    grvDetail.SetRowCellValue(i, colSTT, (i + 1));
                }
            }
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            //frmProjectEmployee frm = new frmProjectEmployee();
            //frm.isAddEmployee = true;
            //frm.projectID = projectID;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    loadDetail();
            //}

            int projectItemID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //DataTable dt = (DataTable)grdDetail.DataSource;
            List<ProjectItemDetailModel> list = (List<ProjectItemDetailModel>)grdDetail.DataSource;
            ProjectItemDetailModel model = new ProjectItemDetailModel()
            {
                ProjectItemID = projectItemID
            };
            list.Add(model);

            //DataRow dtrow = dt.NewRow();
            //dtrow["STT"] = grvDetail.RowCount + 1;
            //dtrow["ProjectItemID"] = projectItemID;
            //dt.Rows.Add(dtrow);

            grvDetail.RefreshData();
            grdDetail.DataSource = list;
            grvDetail.FocusedRowHandle = grvDetail.RowCount - 1;
            grvDetail.FocusedColumn = colEmployeeID;
        }



        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            grvDetail.CloseEditor();
            int[] rowSelected = grvDetail.GetSelectedRows();
            foreach (int row in rowSelected)
            {
                int id = TextUtils.ToInt(grvDetail.GetRowCellValue(row, colIDDetail));
                ProjectItemDetailBO.Instance.Delete(id);

            }
            grvDetail.DeleteSelectedRows();
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadDetail();
        }


        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            int row = grvData.FocusedRowHandle;
            grvDetail.CloseEditor();

            ProjectItemDetailModel model = new ProjectItemDetailModel();
            model.ID = TextUtils.ToInt(grvDetail.GetFocusedRowCellValue(colIDDetail));
            model.ProjectItemID = TextUtils.ToInt(grvDetail.GetFocusedRowCellValue(colProjectItemID));
            model.EmployeeID = TextUtils.ToInt(grvDetail.GetFocusedRowCellValue(colEmployeeID));

            projectEmployee += ";" + model.EmployeeID.ToString();

            var list = listDetail.Where(x => x.EmployeeID == model.EmployeeID && x.ProjectItemID == model.ProjectItemID).ToList();
            if (list.Count <= 0)
            {
                listDetail.Add(model);
            }

            grvData.SetFocusedRowCellValue(colProjectEmployee, projectEmployee);
            grvData.FocusedRowHandle = row;
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            BandedGridView view = sender as BandedGridView;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id > 0)
            {
                int isApproved = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIsApproved));
                string createdBy = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCreatedBy)).Trim();
                int userID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUserID));
                int employeeRequest = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeIDRequest));

                bool isPlan = bandPlan.Columns.Contains((BandedGridColumn)view.FocusedColumn);

                if (Global.IsAdmin) return;

                if (isApproved == 3)
                {
                    grvData.BeginUpdate();
                    e.Valid = false;
                    e.ErrorText = $"Đã duyệt thực tế.\nBạn không thể cập nhật!";
                    grvData.EndUpdate();
                }
                else if (!checkIsPermission(createdBy, userID, employeeRequest))
                {
                    grvData.BeginUpdate();
                    e.Valid = false;
                    e.ErrorText = $"Bạn không thể cập nhật hạng mục của người khác!";
                    grvData.EndUpdate();
                }
                else if (view.FocusedColumn == colMission || isPlan)
                {
                    //Lib.LockEvents = true;
                    int statusUpdate = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colStatusUpdate));
                    if (statusUpdate != 2 && statusUpdate != 1)
                    {
                        //grvData.SetRowCellValue(grvData.FocusedRowHandle, colStatusUpdate, 2);
                        DataRow[] dataRows = dtProjectItem.Select($"ID = {id}");
                        if (dataRows.Length > 0)
                        {
                            int dtRowHandle = dtProjectItem.Rows.IndexOf(dataRows[0]);
                            dtProjectItem.Rows[dtRowHandle][colStatusUpdate.FieldName] = 2;
                            //dtProjectItem.AcceptChanges();
                        }

                    }
                }
            }


        }

        private void dockPanel2_Expanding(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            dockPanel2.BringToFront();
            //stackPanel1.SendToBack();
        }
        private void dockPanel2_DockChanged(object sender, EventArgs e)
        {
            //if (dockPanel2.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible)
            //    stackPanel1.Visible = false;
            //else
            //    stackPanel1.Visible = true;
        }

        private void dockPanel2_Collapsed(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {
            //stackPanel1.Visible = true;
            //stackPanel1.BringToFront();
            //dockPanel2.SendToBack();
        }

        private void dockPanel2_ClosedPanel(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {
            //stackPanel1.Visible = true;
            //stackPanel1.BringToFront();
        }

        private void frmHangMucCongViec_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Control && e.Shift && e.KeyCode == Keys.A)
            //{
            //    dockPanel2.Show();
            //    if (dockPanel2.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible)
            //        stackPanel1.Visible = false;
            //    else
            //        stackPanel1.Visible = true;
            //}
        }

        private void repositoryItemCheckedComboBoxEdit2_EditValueChanged(object sender, EventArgs e)
        {
            var selectedValues = repositoryItemCheckedComboBoxEdit2.Items.GetCheckedValues();
            //grvData.SetFocusedRowCellValue(colProjectEmployee, string.Join(";", selectedValues));
            //MessageBox.Show(string.Join(";", selectedValues));
        }

        private void cboUserID_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = sender as SearchLookUpEdit;
            int userid = TextUtils.ToInt(lookUpEdit.EditValue);

        }


        void updateItemLate()
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                var stt = grvData.GetRowCellValue(i, colSTT);
                int itemLate = 0;
                DateTime? planStart = TextUtils.ToDate4(grvData.GetRowCellValue(i, colPlanStartDate));
                DateTime? planEnd = TextUtils.ToDate4(grvData.GetRowCellValue(i, colPlanEndDate));
                DateTime? actualStart = TextUtils.ToDate4(grvData.GetRowCellValue(i, colActualStartDate));
                DateTime? actualEnd = TextUtils.ToDate4(grvData.GetRowCellValue(i, colActualEndDate));

                //Nếu đã có ngày bắt đầu thực tế và chưa có ngày kết thúc thực tế
                //Nếu ngày bắt đầu thực tế > ngày kết thúc dự kiến --> Failed
                if (actualStart.HasValue && !actualEnd.HasValue && planEnd.HasValue)
                {
                    if ((actualStart.Value.Date - planEnd.Value.Date).TotalDays > 0)
                    {
                        itemLate = 2;
                    }
                    else if ((DateTime.Now.Date - planEnd.Value.Date).TotalDays > 0)
                    {
                        itemLate = 2;
                    }

                }

                //Nếu đã có ngày bắt đầu thực tế và ngày kết thúc thực tế
                //Nếu ngày kết thúc thực tế > ngày kết thúc dự kiến --> Chậm
                if (actualStart.HasValue && actualEnd.HasValue && planEnd.HasValue)
                {
                    if ((actualEnd.Value.Date - planEnd.Value.Date).TotalDays > 0)
                    {
                        itemLate = 1;
                    }
                }

                //Nếu chưa có ngày bắt đầu thực tế và ngày kết thúc thực tế
                //Nếu ngày hiện tại > ngày kết thúc dự kiến --> Failed
                if (!actualStart.HasValue && !actualEnd.HasValue && planEnd.HasValue)
                {
                    if ((DateTime.Now.Date - planEnd.Value.Date).TotalDays > 0)
                    {
                        itemLate = 2;
                    }
                }

                //Nếu chỉ có ngày bắt đầu dự kiến
                if (planStart.HasValue && !planEnd.HasValue && !actualStart.HasValue && !actualEnd.HasValue)
                {
                    if ((DateTime.Now.Date - planStart.Value.Date).TotalDays > 0)
                    {
                        itemLate = 2;
                    }
                }

                grvData.SetRowCellValue(i, colItemLate, itemLate);
            }
        }


        string loadProjectItemCode(int projectId)
        {
            var listItem = SQLHelper<ProjectItemModel>.FindByAttribute("ProjectID", projectId);
            string code = $"{project.ProjectCode}_{listItem.Count() + 1}";
            return code;
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            grvData.ClearColumnsFilter();
            updateItemLate();
        }

        private void btnAddProblem_Click(object sender, EventArgs e)
        {
            int projectItemID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (projectItemID <= 0) return;
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            DateTime? planEnd = TextUtils.ToDate4(grvData.GetFocusedRowCellValue(colPlanEndDate));
            DateTime dateNow = DateTime.Now;
            //var timeSpan = (dateNow.Date - planEnd.Value.Date).TotalDays;
            //if (timeSpan > 0)
            //{
            //    MessageBox.Show($"Bạn không thể cập nhật vấn đề phát sinh cho hạng mục [{code}] vì đã quá Ngày kết thúc dự kiến!", "Thông báo");
            //    return;
            //}


            AddProblemItem(projectItemID);
        }

        void AddProblemItem(int projectItemID)
        {
            ProjectItemModel model = SQLHelper<ProjectItemModel>.FindByID(projectItemID);

            frmProjectItemProblem frm = new frmProjectItemProblem();
            frm.projectItem = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //grvData.SetFocusedRowCellValue(colReasonLate, frm.SetContent());

                DataRow[] dataRows = dtProjectItem.Select($"ID = {projectItemID}");
                if (dataRows.Length > 0)
                {
                    int dtRowHandle = dtProjectItem.Rows.IndexOf(dataRows[0]);
                    dtProjectItem.Rows[dtRowHandle][colReasonLate.FieldName] = frm.SetContent();
                    //dtProjectItem.AcceptChanges();
                }
            }
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            int projectItemID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (projectItemID <= 0) return;
            AddProblemItem(projectItemID);
        }

        private void grvData_CellMerge(object sender, CellMergeEventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"HangMucCongViec_{project.ProjectCode}.xls");
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsExportOptions optionsEx = new XlsExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;
                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);


                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsExportMode.SingleFilePageByPage;
                    //optionsEx.AllowSortingAndFiltering = DefaultBoolean.True;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXls(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void hideContainerRight_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemSearchLookUpEdit1View_RowStyle(object sender, RowStyleEventArgs e)
        {
            //if (e.RowHandle < 0) return;
            //int status = TextUtils.ToInt(repositoryItemSearchLookUpEdit1View.GetRowCellValue(e.RowHandle, "Status"));
            //if (status == 1)
            //{
            //    e.Appearance.BackColor = Color.Red;
            //    e.Appearance.ForeColor = Color.White;
            //}
        }


        List<ToolStripMenuItem> itemFilters = new List<ToolStripMenuItem>();
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (grvData.FocusedColumn != colUserID && grvData.FocusedColumn != colEmployeeIDRequest) return;
            string value = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(grvData.FocusedColumn));
            if (string.IsNullOrWhiteSpace(value)) return;

            var listItems = contextMenuStrip1.Items.Cast<ToolStripMenuItem>().ToList();
            if (itemFilters.Count > 0)
            {
                foreach (ToolStripMenuItem item in listItems)
                {
                    if (!itemFilters.Contains(item)) continue;
                    contextMenuStrip1.Items.Remove(item);
                }
                itemFilters.Clear();

            }
            ToolStripMenuItem toolStripMenu = new ToolStripMenuItem();
            toolStripMenu.Text = $"Lọc {value}";
            toolStripMenu.Click += Item_Click;
            contextMenuStrip1.Items.Add(toolStripMenu);
            itemFilters.Add(toolStripMenu);
        }

        private void Item_Click(object sender, EventArgs e)
        {
            string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
            if (string.IsNullOrWhiteSpace(value)) return;
            string filterString = $"([{grvData.FocusedColumn.FieldName}] = {value})";
            grvData.Columns[$"{grvData.FocusedColumn.FieldName}"].FilterInfo = new ColumnFilterInfo(filterString);

            if (grvData.FocusedColumn == colUserID) //Lọc theo người phụ trách
            {

            }

            if (grvData.FocusedColumn == colEmployeeIDRequest) //Lọc theo người giao việc
            {

            }
        }

        private void cboEmployeeRequestID_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;

            DataRowView employee = (DataRowView)lookUpEdit.GetSelectedDataRow();

            int employeeID = 0;
            string fullName = "";
            if (employee != null)
            {
                employeeID = TextUtils.ToInt(employee["ID"]);
                fullName = TextUtils.ToString(employee["FullName"]);
            }
            grvData.SetRowCellValue(grvData.FocusedRowHandle, colEmployeeRequestID, employeeID);
            grvData.SetRowCellValue(grvData.FocusedRowHandle, colEmployeeRequestName, fullName);
        }

        private void grvData_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (grvData.FocusedColumn != colEmployeeRequestName) return;
            int employeeRequestID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeRequestID));
            e.Cancel = !(employeeRequestID <= 0);
        }
    }
}
