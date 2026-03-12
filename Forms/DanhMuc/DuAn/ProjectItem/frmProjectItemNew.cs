using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BMS;
using BMS.Model;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Columns;
using BMS.Business;
using DevExpress.Data;
using DevExpress.XtraTreeList;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Frames;
using DevExpress.XtraGrid;
using System.IO;
using DocumentFormat.OpenXml.EMMA;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using Irony;
using DevExpress.XtraTreeList.StyleFormatConditions;
using DevExpress.CodeParser;
using System.Threading;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.Diagnostics;
using DevExpress.Utils;

namespace Forms.DanhMuc.DuAn.ProjectItem
{

    public partial class frmProjectItemNew : _Forms
    {
        public ProjectModel project = new ProjectModel();

        DataTable dtProjectItem = new DataTable();

        List<int> listID = new List<int>();

        DataTable dtProjectItemLog = new DataTable();
        int idAdd = 0;
        string projectEmployee = "";

        bool isSave = false;

        bool isAdding = false;

        public string projectItemCode = "";

        bool isDataChanged = false;

        public Action SaveEvent;

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
        public frmProjectItemNew()
        {
            InitializeComponent();
            tlCategory.ContextMenuStrip = contextMenuStrip2;
            dtProjectItemLog.Clear();
        }


        private void frmJobCategory_Load(object sender, EventArgs e)
        {


            try
            {
                this.Text += $" - {project.ProjectCode}";

                loadUser();
                loadStatus();
                loadTypeProjectItem();

                //loadPermission();

                loadStatusApproved();
                //updateItemLate();
                LoadEmployeeRequest();
                LoadEmployeeRequestName();
                loadProjectItem();
                SetupFormatRules();

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetupFormatRules()
        {
            // Clear existing rules
            tlCategory.FormatRules.Clear();

            // Parent nodes rule
            TreeListFormatRule parentRule = new TreeListFormatRule();
            FormatConditionRuleExpression parentCondition = new FormatConditionRuleExpression();
            parentCondition.Expression = "HasChildren = true";
            parentCondition.Appearance.BackColor = Color.LightGray;
            parentRule.Rule = parentCondition;
            parentRule.ApplyToRow = true;
            tlCategory.FormatRules.Add(parentRule);

            // Late items level 1
            TreeListFormatRule lateRule1 = new TreeListFormatRule();
            FormatConditionRuleExpression lateCondition1 = new FormatConditionRuleExpression();
            lateCondition1.Expression = "ItemLateActual = 1 OR ItemLate = 1";
            lateCondition1.Appearance.BackColor = Color.Orange;
            lateRule1.Rule = lateCondition1;
            lateRule1.ApplyToRow = true;
            tlCategory.FormatRules.Add(lateRule1);

            // Expiring soon

            TreeListFormatRule expiringRule = new TreeListFormatRule();
            FormatConditionRuleExpression expiringCondition = new FormatConditionRuleExpression();
            expiringCondition.Expression = "PlanEndDate!=null AND TotalDayExpridSoon <= 3 AND IsNull(ActualEndDate) OR ActualEndDate = ''";
            expiringCondition.Appearance.BackColor = Color.LightYellow;
            expiringRule.Rule = expiringCondition;
            expiringRule.ApplyToRow = true;
            tlCategory.FormatRules.Add(expiringRule);
            // Late items level 2
            TreeListFormatRule lateRule2 = new TreeListFormatRule();
            FormatConditionRuleExpression lateCondition2 = new FormatConditionRuleExpression();
            lateCondition2.Expression = "ItemLateActual = 2 OR ItemLate = 2";
            lateCondition2.Appearance.BackColor = Color.Red;
            lateCondition2.Appearance.ForeColor = Color.White;
            lateRule2.Rule = lateCondition2;
            lateRule2.ApplyToRow = true;
            tlCategory.FormatRules.Add(lateRule2);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                isSave = true;
                isAdding = true;
                if (saveData())
                {
                    loadProjectItem();
                    tlCategory.Columns["TypeProjectItem"].SortOrder = SortOrder.Ascending;

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

        private void btnCloseAndSave_Click(object sender, EventArgs e)
        {
            isSave = true;

            if (saveData())
            {
                dtProjectItem.AcceptChanges();
                //this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void btnDeleteProjectItem_Click(object sender, EventArgs e)
        {
            TreeListNode node = tlCategory.FocusedNode;
            if (node == null) return;

            int ID = TextUtils.ToInt(node.GetValue("ID"));
            int isApproved = TextUtils.ToInt(node.GetValue("IsApproved"));
            string isApprovedText = cboIsApproved.GetDisplayText(isApproved);
            int userID = TextUtils.ToInt(node.GetValue("UserID"));

            if (ID > 0)
            {
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
                }
            }

            if (MessageBox.Show(string.Format("Bạn có muốn xóa hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listID.Add(ID);
                tlCategory.DeleteNode(node);

                TreeListNode nextNode = GetNextNode(node);
                if (nextNode != null)
                {
                    tlCategory.FocusedNode = nextNode;
                }

                decimal total = 0;
                foreach (TreeListNode n in tlCategory.Nodes)
                {
                    total += TextUtils.ToDecimal(n.GetValue("TotalDayPlan"));
                }

                if (total > 0)
                {
                    foreach (TreeListNode n in tlCategory.Nodes)
                    {
                        decimal percent = (TextUtils.ToDecimal(n.GetValue("TotalDayPlan")) / total) * 100;
                        n.SetValue("PercentItem", percent);
                    }
                }
            }
        }
        private TreeListNode GetNextNode(TreeListNode currentNode)
        {
            int currentIndex = tlCategory.Nodes.IndexOf(currentNode);
            if (currentIndex >= 0 && currentIndex < tlCategory.Nodes.Count - 1)
            {
                return tlCategory.Nodes[currentIndex + 1];
            }
            return null;
        }

        private void btnAddProblem_Click(object sender, EventArgs e)
        {
            int projectItemID = TextUtils.ToInt(tlCategory.GetFocusedRowCellValue(colID));
            if (projectItemID <= 0) return;
            //string code = TextUtils.ToString(tlCategory.GetFocusedRowCellValue(colCode));
            DateTime? planEnd = TextUtils.ToDate4(tlCategory.GetFocusedRowCellValue(colPlanEndDate));
            DateTime dateNow = DateTime.Now;

            AddProblemItem(projectItemID);
        }

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



        void loadTypeProjectItem()
        {
            DataTable dt = TextUtils.GetTable("spGetProjectTypeChildren");


            cboTypeProjectItem.DisplayMember = "ProjectTypeName";
            cboTypeProjectItem.ValueMember = "ID";
            cboTypeProjectItem.DataSource = dt;

        }

        //Load hạng mục công việc
        void loadProjectItem()
        {
            dtProjectItem = TextUtils.LoadDataFromSP("spGetProjectItem", "A", new string[] { "@ProjectID" }, new object[] { project.ID });

            dtProjectItem.Columns.Add("ProjectEmployeeID");

            dtProjectItemLog = dtProjectItem.Copy();

            tlCategory.DataSource = dtProjectItem;

            //if (tlCategory.Nodes.Count >= 2)
            //    tlCategory.FocusedNode = tlCategory.Nodes[1];
            //tlCategory.FocusedNode = tlCategory.getno
            string filterString = $"([Status] In (0,1))";

            if (!string.IsNullOrWhiteSpace(projectItemCode))
            {
                filterString = $"([{colCodeItem.FieldName}] = '{projectItemCode}')";

                foreach (TreeListColumn column in tlCategory.Columns)
                {
                    //column.OptionsColumn.AllowEdit = false;
                    column.OptionsColumn.ReadOnly = true;
                }
            }

            tlCategory.ActiveFilterString = filterString;



            dtProjectItem.AcceptChanges();
        }

        //Load người phụ trách
        void loadUser()
        {
            DataTable dt = TextUtils.GetDataTableFromSP("spGetUserProjectItem", new string[] { "@ProjectID" }, new object[] { 0 });


            cboUserID.ValueMember = "UserID";
            cboUserID.DisplayMember = "FullName";
            cboUserID.DataSource = dt;
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
        private List<ToolStripMenuItem> itemFilters = new List<ToolStripMenuItem>();

        private void Item_Click(object sender, EventArgs e)
        {
            var focusedNode = tlCategory.FocusedNode;
            if (focusedNode == null) return;

            string value = TextUtils.ToString(focusedNode.GetValue(tlCategory.FocusedColumn));
            if (string.IsNullOrWhiteSpace(value)) return;

            string filterString = $"[{tlCategory.FocusedColumn.FieldName}] = '{value.Replace("'", "''")}'";

            tlCategory.ActiveFilterString = filterString;

            if (tlCategory.FocusedColumn == colUserID)
            {
            }
            else if (tlCategory.FocusedColumn == colEmployeeIDRequest)
            {
            }
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }
        string loadProjectItemCode(int projectId)
        {
            var listItem = SQLHelper<ProjectItemModel>.FindByAttribute("ProjectID", projectId);
            string code = $"{project.ProjectCode}_{listItem.Count() + 1}";
            return code;
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
        void updateItemLate()
        {
            // Lấy danh sách tất cả các node trong TreeList
            var nodes = tlCategory.GetNodeList();

            foreach (TreeListNode node in nodes)
            {
                int itemLate = 0;
                var stt = node.GetValue(colSTT);

                DateTime? planStart = TextUtils.ToDate4(node.GetValue(colPlanStartDate));
                DateTime? planEnd = TextUtils.ToDate4(node.GetValue(colPlanEndDate));
                DateTime? actualStart = TextUtils.ToDate4(node.GetValue(colActualStartDate));
                DateTime? actualEnd = TextUtils.ToDate4(node.GetValue(colActualEndDate));

                // Logic xử lý trễ
                if (actualStart.HasValue && !actualEnd.HasValue && planEnd.HasValue)
                {
                    if ((actualStart.Value.Date - planEnd.Value.Date).TotalDays > 0 ||
                        (DateTime.Now.Date - planEnd.Value.Date).TotalDays > 0)
                    {
                        itemLate = 2;
                    }
                }

                if (actualStart.HasValue && actualEnd.HasValue && planEnd.HasValue)
                {
                    if ((actualEnd.Value.Date - planEnd.Value.Date).TotalDays > 0)
                    {
                        itemLate = 1;
                    }
                }

                if (!actualStart.HasValue && !actualEnd.HasValue && planEnd.HasValue)
                {
                    if ((DateTime.Now.Date - planEnd.Value.Date).TotalDays > 0)
                    {
                        itemLate = 2;
                    }
                }

                if (planStart.HasValue && !planEnd.HasValue && !actualStart.HasValue && !actualEnd.HasValue)
                {
                    if ((DateTime.Now.Date - planStart.Value.Date).TotalDays > 0)
                    {
                        itemLate = 2;
                    }
                }

                // Cập nhật giá trị cho node
                node.SetValue(colItemLate, itemLate);
            }

            // Refresh TreeList nếu cần
            tlCategory.RefreshDataSource();
        }
        private void btnSave_Close_Click(object sender, EventArgs e)
        {
            this.btnCloseAndSave_Click(null, null);
        }

        private void btnClearFitller_Click(object sender, EventArgs e)
        {
            tlCategory.ClearColumnsFilter();
            updateItemLate();
        }
        //bool saveData()
        //{
        //    tlCategory.CloseEditor();
        //    tlCategory.PostEditor(); 

        //    if (!validate())
        //    {
        //        return false;
        //    }

        //    DataTable data = (DataTable)tlCategory.DataSource;
        //    DataTable dataChange = data?.GetChanges();

        //    if (dataChange != null || IsDragging)
        //    {
        //        var dataLog = dataChange.Select("StatusUpdate = 2");
        //        SaveLogItem(dataLog);

        //        dataChange.AcceptChanges();
        //        foreach (DataRow row in dataChange.Rows)
        //        {
        //            int id = TextUtils.ToInt(row["ID"]);
        //            ProjectItemModel projectItem = new ProjectItemModel();
        //            if (id > 0)
        //            {
        //                projectItem = (ProjectItemModel)ProjectItemBO.Instance.FindByPK(id);
        //            }
        //            projectItem.Status = TextUtils.ToInt(row["Status"]);
        //            projectItem.STT = TextUtils.ToString(row["STT"]);
        //            projectItem.UserID = TextUtils.ToInt(row["UserID"]);
        //            projectItem.ProjectID = project.ID;
        //            projectItem.Mission = TextUtils.ToString(row["Mission"]);
        //            projectItem.PlanStartDate = TextUtils.ToDate4(row["PlanStartDate"]);
        //            projectItem.PlanEndDate = TextUtils.ToDate4(row["PlanEndDate"]);
        //            projectItem.ActualStartDate = TextUtils.ToDate4(row["ActualStartDate"]);
        //            projectItem.ActualEndDate = TextUtils.ToDate4(row["ActualEndDate"]);
        //            projectItem.Note = TextUtils.ToString(row["Note"]);
        //            projectItem.TotalDayPlan = TextUtils.ToDecimal(row["TotalDayPlan"]);
        //            projectItem.TotalDayActual = 0;
        //            if (projectItem.ActualStartDate.HasValue && projectItem.ActualEndDate.HasValue)
        //            {
        //                projectItem.TotalDayActual = (decimal)(projectItem.ActualEndDate.Value - projectItem.ActualStartDate.Value).TotalDays;
        //            }
        //            projectItem.ItemLate = TextUtils.ToInt(row["ItemLate"]);
        //            projectItem.PercentItem = TextUtils.ToDecimal(row["PercentItem"]);
        //            projectItem.TypeProjectItem = TextUtils.ToInt(row["TypeProjectItem"]);
        //            projectItem.PercentageActual = TextUtils.ToInt(row["PercentageActual"]);
        //            projectItem.EmployeeIDRequest = TextUtils.ToInt(row["EmployeeIDRequest"]);
        //            projectItem.IsUpdateLate = TextUtils.ToBoolean(row["IsUpdateLate"]);
        //            projectItem.ReasonLate = TextUtils.ToString(row["ReasonLate"]);


        //            projectItem.ParentID = TextUtils.ToInt(row["ParentID"]);
        //            if (projectItem.ActualEndDate.HasValue)
        //            {
        //                projectItem.IsApproved = 2;
        //            }
        //            if (Global.IsLeader <= 0 && Global.EmployeeID != Global.HeadOfDepartment)
        //            {
        //                projectItem.UpdatedDateActual = TextUtils.ToDate4(row["UpdatedDateActual"]);
        //            }
        //            projectItem.EmployeeRequestID = TextUtils.ToInt(row["EmployeeRequestID"]);
        //            projectItem.EmployeeRequestName = TextUtils.ToString(row["EmployeeRequestName"]);

        //            if (projectItem.ID > 0)
        //            {
        //                ProjectItemBO.Instance.Update(projectItem);
        //            }
        //            else
        //            {
        //                projectItem.Code = loadProjectItemCode(project.ID);
        //                projectItem.CreatedBy = Global.LoginName;
        //                projectItem.CreatedDate = DateTime.Now;
        //                projectItem.ID = (int)ProjectItemBO.Instance.Insert(projectItem);
        //            }


        //            if (string.IsNullOrEmpty(projectItem.Mission))
        //            {
        //                // Optionally handle empty mission here if needed
        //            }
        //        }
        //    }
        //    IsDragging = false;
        //    if (listID.Count > 0)
        //    {
        //        string id = string.Join(",", listID);
        //        string sql = $"DELETE ProjectItem WHERE ID IN ({id})\n";
        //        TextUtils.ExcuteSQL(sql);
        //        listID.Clear();
        //    }

        //    dtProjectItem.AcceptChanges();
        //    UpdatePercentItem();
        //    return true;
        //}
        private DataTable GetCurrentDataFromTreeList()
        {
            tlCategory.CloseEditor();
            tlCategory.PostEditor();
            tlCategory.FocusedNode = null;
            return (DataTable)tlCategory.DataSource;
        }
        public void GetTableDiff(
                    DataTable dtOld,
                    DataTable dtNew,
                    ref DataTable dtSame,
                    ref DataTable dtDifferences,
                    ref DataTable dtAdded,
                    ref DataTable dtRemoved,
                    ref DataTable dtDraged)
        {
            try
            {
                // Reset các DataTable
                dtSame = dtOld.Clone();
                dtDifferences = dtOld.Clone();
                dtAdded = dtOld.Clone();
                dtRemoved = dtOld.Clone();
                dtDraged = dtOld.Clone();

                // Tìm các dòng bị xóa (có trong old nhưng không có trong new)
                var removedRows = dtOld.AsEnumerable()
                    .Where(oldRow => !dtNew.AsEnumerable()
                        .Any(newRow => newRow["ID"].Equals(oldRow["ID"])));
                foreach (var row in removedRows)
                {
                    dtRemoved.ImportRow(row);
                }

                // Tìm các dòng mới thêm (có trong new nhưng không có trong old)
                var addedRows = dtNew.AsEnumerable()
                    .Where(newRow => !dtOld.AsEnumerable()
                        .Any(oldRow => oldRow["ID"].Equals(newRow["ID"])));
                foreach (var row in addedRows)
                {
                    dtAdded.ImportRow(row);
                }

                // Tìm các dòng bị thay đổi (cùng ID nhưng khác nội dung)
                var modifiedRows = dtNew.AsEnumerable()
                    .Where(newRow => dtOld.AsEnumerable()
                        .Any(oldRow =>
                            oldRow["ID"].Equals(newRow["ID"]) &&
                            !DataRowComparer.Default.Equals(oldRow, newRow)));
                foreach (var row in modifiedRows)
                {
                    dtDraged.ImportRow(row);
                }

                // Gom tất cả differences
                dtDifferences.Merge(dtRemoved);
                dtDifferences.Merge(dtAdded);
                dtDifferences.Merge(dtDraged);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi so sánh dữ liệu: " + ex.Message);
            }
        }
        DataRow[] changedRows;
        bool saveData()
        {
            try
            {
                tlCategory.Refresh();
                tlCategory.CloseEditor();
                tlCategory.PostEditor();

                tlCategory.FocusedNode = null;
                if (!validate())
                {
                    return false;
                }
                //lưu log

                //foreach (var row in changedRows)
                //{
                //    dtProjectItemLog.ImportRow(row);
                //}

                // Nếu cần thì log luôn
                //SaveLogItem(changedRows);

                Dictionary<int, int> idMapping = new Dictionary<int, int>();
                foreach (TreeListNode node in tlCategory.GetNodeList())
                {
                    if (node == null) continue;
                    int id = TextUtils.ToInt(node.GetValue(colID));
                    ProjectItemModel projectItem = id > 0 ? SQLHelper<ProjectItemModel>.FindByID(id) : new ProjectItemModel();

                    projectItem.Status = TextUtils.ToInt(node["Status"]);
                    projectItem.STT = TextUtils.ToString(node["STT"]);
                    projectItem.UserID = TextUtils.ToInt(node["UserID"]);
                    projectItem.Code = TextUtils.ToString(node["Code"]);
                    projectItem.ProjectID = project.ID;
                    projectItem.Mission = TextUtils.ToString(node["Mission"]);
                    projectItem.PlanStartDate = TextUtils.ToDate4(node["PlanStartDate"]);
                    projectItem.PlanEndDate = TextUtils.ToDate4(node["PlanEndDate"]);
                    projectItem.ActualStartDate = TextUtils.ToDate4(node["ActualStartDate"]);
                    projectItem.ActualEndDate = TextUtils.ToDate4(node["ActualEndDate"]);
                    projectItem.Note = TextUtils.ToString(node["Note"]);
                    projectItem.TotalDayPlan = TextUtils.ToDecimal(node["TotalDayPlan"]);
                    projectItem.TotalDayActual = 0;

                    if (projectItem.ActualStartDate.HasValue && projectItem.ActualEndDate.HasValue)
                    {
                        projectItem.TotalDayActual = (decimal)(projectItem.ActualEndDate.Value - projectItem.ActualStartDate.Value).TotalDays;
                    }

                    projectItem.ItemLate = TextUtils.ToInt(node["ItemLate"]);
                    projectItem.PercentItem = TextUtils.ToDecimal(node["PercentItem"]);
                    projectItem.TypeProjectItem = TextUtils.ToInt(node["TypeProjectItem"]);
                    projectItem.PercentageActual = TextUtils.ToInt(node["PercentageActual"]);
                    projectItem.EmployeeIDRequest = TextUtils.ToInt(node["EmployeeIDRequest"]);
                    projectItem.IsUpdateLate = TextUtils.ToBoolean(node["IsUpdateLate"]);
                    projectItem.ReasonLate = TextUtils.ToString(node["ReasonLate"]);
                    projectItem.ParentID = TextUtils.ToInt(node["ParentID"]);

                    if (projectItem.ActualEndDate.HasValue)
                    {
                        projectItem.IsApproved = 2;
                    }

                    if (Global.IsLeader <= 0 && Global.EmployeeID != Global.HeadOfDepartment)
                    {
                        projectItem.UpdatedDateActual = TextUtils.ToDate4(node["UpdatedDateActual"]);
                    }

                    projectItem.EmployeeRequestID = TextUtils.ToInt(node["EmployeeRequestID"]);
                    projectItem.EmployeeRequestName = TextUtils.ToString(node["EmployeeRequestName"]);

                    if (projectItem.ID > 0)
                    {
                        SQLHelper<ProjectItemModel>.Update(projectItem);
                    }
                    else
                    {
                        projectItem.Code = loadProjectItemCode(project.ID);
                        projectItem.CreatedBy = Global.LoginName;
                        projectItem.CreatedDate = DateTime.Now;
                        projectItem.ID = SQLHelper<ProjectItemModel>.Insert(projectItem).ID;
                        idMapping.Add(id, projectItem.ID);
                    }
                }
                // Cập nhật ParentID cho các node con
                foreach (TreeListNode node in tlCategory.GetNodeList())
                {
                    int parentID = TextUtils.ToInt(node.GetValue("ParentID"));
                    if (parentID < 0 && idMapping.ContainsKey(parentID))
                    {
                        int newParentID = idMapping[parentID];
                        node.SetValue("ParentID", newParentID);
                        int id = idMapping[TextUtils.ToInt(node.GetValue(colID))];
                        Dictionary<string, object> dics = new Dictionary<string, object>()
                    {
                        { "ParentID",newParentID}
                    };
                        SQLHelper<ProjectItemModel>.UpdateFieldsByID(dics, id);
                    }
                }

                IsDragging = false;
                if (listID.Count > 0)
                {
                    string id = string.Join(",", listID);
                    string sql = $"DELETE ProjectItem WHERE ID IN ({id})\n";
                    TextUtils.ExcuteSQL(sql);
                    listID.Clear();
                }

                dtProjectItem.AcceptChanges();
                UpdatePercentItem();
                isDataChanged = false;
                SetupFormatRules();
                SaveEvent();//update
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
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

            tlCategory.CloseEditor();
            tlCategory.Refresh();
            tlCategory.PostEditor();

            //DataTable data = (DataTable)tlCategory.DataSource;
            DateTime dateFixed = new DateTime(2023, 12, 1, 0, 0, 0);

            foreach (TreeListNode row in tlCategory.GetNodeList())
            {
                int id = TextUtils.ToInt(row.GetValue("ID"));
                ProjectItemModel projectItem = SQLHelper<ProjectItemModel>.FindByID(id);

                string createdBy = TextUtils.ToString(row.GetValue("CreatedBy"));
                int userID = TextUtils.ToInt(row.GetValue("UserID"));
                int employeeRequest = TextUtils.ToInt(row.GetValue("EmployeeIDRequest"));
                int isApproved = TextUtils.ToInt(row.GetValue("IsApproved"));

                if (projectItem != null && projectItem.ID > 0)
                {
                    if (TextUtils.ToString(projectItem.CreatedBy).Trim().ToLower() != Global.LoginName.ToLower() && TextUtils.ToInt(projectItem.UserID) != Global.UserID) continue;
                }
                if (isApproved == 3) continue;

                int type = TextUtils.ToInt(row.GetValue("TypeProjectItem"));
                int status = TextUtils.ToInt(row.GetValue("Status"));
                string stt = TextUtils.ToString(row.GetValue("STT"));
                string mission = TextUtils.ToString(row.GetValue("Mission")).Trim();
                string employeeRequestName = TextUtils.ToString(row.GetValue("EmployeeRequestName")).Trim();

                DateTime? planDS = TextUtils.ToDate4(row.GetValue("PlanStartDate"));
                DateTime? planDE = TextUtils.ToDate4(row.GetValue("PlanEndDate"));
                DateTime? actualDS = TextUtils.ToDate4(row.GetValue("ActualStartDate"));
                DateTime? actualDE = TextUtils.ToDate4(row.GetValue("ActualEndDate"));
                string reasonLate = TextUtils.ToString(row.GetValue("ReasonLate"));
                int updateStatus = TextUtils.ToInt(row.GetValue("StatusUpdate"));
                DateTime? dateCreated = TextUtils.ToDate4(row.GetValue("CreatedDate"));

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
                    MessageBox.Show($"Vui lòng nhập Người phụ trách!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!planDS.HasValue)
                {
                    MessageBox.Show($"Vui lòng nhập Ngày bắt đầu (KẾ HOẠCH)!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!planDE.HasValue)
                {
                    MessageBox.Show($"Vui lòng nhập Ngày kết thúc (KẾ HOẠCH)!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (updateStatus == 2)
                {
                    if (TextUtils.ToString(projectItem?.ReasonLate).ToLower().Trim() == TextUtils.ToString(reasonLate).ToLower().Trim())
                    {
                        // In TreeList, we can't set FocusedColumn easily in this context; adjust if UI interaction is needed
                        DialogResult dialog = MessageBox.Show($"Bạn vừa thay đổi dữ liệu - Stt: {stt}.\nBạn có muốn thêm Lý do phát sinh không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            AddProblemItem(id);
                        }
                        else
                        {
                            //row.GetValue("Mission") = row["Mission", DataRowVersion.Original];
                            //row["PlanStartDate"] = row["PlanStartDate", DataRowVersion.Original];
                            //row["TotalDayPlan"] = row["TotalDayPlan", DataRowVersion.Original];
                            //row["PlanEndDate"] = row["PlanEndDate", DataRowVersion.Original];
                            //row["StatusUpdate"] = row["StatusUpdate", DataRowVersion.Original];
                            if (projectItem != null)
                            {
                                row.SetValue("Mission", projectItem.Mission);
                                row.SetValue("PlanStartDate", projectItem.PlanStartDate);
                                row.SetValue("TotalDayPlan", projectItem.TotalDayPlan);
                                row.SetValue("PlanEndDate", projectItem.PlanEndDate);
                                row.SetValue("StatusUpdate", projectItem.Status);
                            }
                            updateItemLate();
                        }
                        return false;
                    }
                }

                if (status == 2)
                {
                    if (!actualDS.HasValue)
                    {
                        MessageBox.Show($"Vui lòng nhập Ngày bắt đầu (THỰC TẾ)!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (!actualDE.HasValue)
                    {
                        MessageBox.Show($"Vui lòng nhập Ngày kết thúc (THỰC TẾ)!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (actualDS.HasValue && actualDE.HasValue)
                    {
                        double timeSpan = (actualDE.Value.Date - actualDS.Value.Date).TotalDays;
                        if (timeSpan < 0)
                        {
                            MessageBox.Show($"Ngày Kết thúc phải lớn hơn Ngày bắt đầu (THỰC TẾ)!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }

                DateTime dateUpdate = new DateTime(2025, 04, 12, 17, 30, 00);
                if (string.IsNullOrEmpty(employeeRequestName))
                {
                    if (projectItem?.CreatedDate.HasValue == true && projectItem.CreatedDate.Value >= dateUpdate)
                    {
                        MessageBox.Show($"Vui lòng nhập Tên người yêu cầu!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else if (!projectItem?.CreatedDate.HasValue == true)
                    {
                        MessageBox.Show($"Vui lòng nhập Tên người yêu cầu!\n(Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            return true;
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

        private void updatePercent()
        {
            // Tính toán phần trăm
            decimal totalDays = tlCategory.GetNodeList()
                .Cast<TreeListNode>()
                .Sum(n => TextUtils.ToDecimal(n.GetValue(colTotalDayPlan)));

            if (totalDays > 0)
            {
                foreach (TreeListNode n in tlCategory.GetNodeList())
                {
                    decimal percent = (TextUtils.ToDecimal(n.GetValue(colTotalDayPlan)) * 100) / totalDays;
                    n.SetValue(colPercentItem, percent);
                }
            }
        }
        private void tlCategory_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (Lib.LockEvents) return;
            if (IsDragging)
            {
                IsDragging = false;
                return;
            }
            try
            {
                while (Lib.LockEvents == true)
                {

                }
                Lib.LockEvents = true;
                tlCategory.CloseEditor();
                tlCategory.PostEditor();
                tlCategory.FocusedColumn = colID;

                dtProjectItem.AcceptChanges();

                TreeListNode node = e.Node;
                if (node == null) return;

                // Xử lý các cột ngày tháng
                DateTime? dateStart = TextUtils.ToDate4(node.GetValue(colPlanStartDate));
                DateTime? dateEnd = TextUtils.ToDate4(node.GetValue(colPlanEndDate));
                int totalDay = TextUtils.ToInt(node.GetValue(colTotalDayPlan));

                // Xử lý thay đổi ngày bắt đầu
                if (e.Column == colPlanStartDate)
                {
                    if (dateStart.HasValue)
                    {
                        if (totalDay > 0)
                        {
                            DateTime endDate = dateStart.Value.AddDays(totalDay - 1);
                            node.SetValue(colPlanEndDate, endDate);
                        }
                        else if (dateEnd.HasValue)
                        {
                            int days = (int)(dateEnd.Value - dateStart.Value).TotalDays + 1;
                            node.SetValue(colTotalDayPlan, days);
                        }
                    }

                    //updatePercent();//update 
                }
                // Xử lý thay đổi tổng số ngày
                else if (e.Column == colTotalDayPlan)
                {
                    if (totalDay > 0)
                    {
                        if (dateStart.HasValue)
                        {
                            DateTime endDate = dateStart.Value.AddDays(totalDay - 1);
                            node.SetValue(colPlanEndDate, endDate);
                        }
                        else if (dateEnd.HasValue)
                        {
                            DateTime startDate = dateEnd.Value.AddDays(-(totalDay - 1));
                            node.SetValue(colPlanStartDate, startDate);
                        }
                    }

                    //updatePercent();//update 
                }
                // Xử lý thay đổi ngày kết thúc
                else if (e.Column == colPlanEndDate)
                {
                    if (dateEnd.HasValue)
                    {
                        if (dateStart.HasValue)
                        {
                            int days = (int)(dateEnd.Value - dateStart.Value).TotalDays + 1;
                            node.SetValue(colTotalDayPlan, days);
                        }
                        else if (totalDay > 0)
                        {
                            DateTime startDate = dateEnd.Value.AddDays(-(totalDay - 1));
                            node.SetValue(colPlanStartDate, startDate);
                        }
                    }

                    //updatePercent();//update 
                }
                //if (e.Column == colTotalDayPlan)
                //{
                //    // Tính toán phần trăm
                //    decimal totalDays = tlCategory.Nodes
                //        .Cast<TreeListNode>()
                //        .Sum(n => TextUtils.ToDecimal(n.GetValue(colTotalDayPlan)));

                //    if (totalDays > 0)
                //    {
                //        foreach (TreeListNode n in tlCategory.Nodes)
                //        {
                //            decimal percent = TextUtils.ToDecimal(n.GetValue(colTotalDayPlan)) / (totalDays * 100);
                //            n.SetValue(colPercentItem, percent);
                //        }
                //    }
                //}
                updatePercent();
                DateTime? planStart = TextUtils.ToDate4(node.GetValue(colPlanStartDate));
                DateTime? planEnd = TextUtils.ToDate4(node.GetValue(colPlanEndDate));
                DateTime? actualStart = TextUtils.ToDate4(node.GetValue(colActualStartDate));
                DateTime? actualEnd = TextUtils.ToDate4(node.GetValue(colActualEndDate));

                int itemLate = 0;

                // Logic kiểm tra trễ
                if (actualStart.HasValue && !actualEnd.HasValue && planEnd.HasValue)
                {
                    if ((actualStart.Value.Date - planEnd.Value.Date).TotalDays > 0 ||
                        (DateTime.Now.Date - planEnd.Value.Date).TotalDays > 0)
                    {
                        itemLate = 2;
                    }
                }
                else if (actualStart.HasValue && actualEnd.HasValue && planEnd.HasValue)
                {
                    if ((actualEnd.Value.Date - planEnd.Value.Date).TotalDays > 0)
                    {
                        itemLate = 1;
                    }
                }
                else if (!actualStart.HasValue && !actualEnd.HasValue && planEnd.HasValue)
                {
                    if ((DateTime.Now.Date - planEnd.Value.Date).TotalDays > 0)
                    {
                        itemLate = 2;
                    }
                }
                else if (planStart.HasValue && !planEnd.HasValue &&
                         !actualStart.HasValue && !actualEnd.HasValue)
                {
                    if ((DateTime.Now.Date - planStart.Value.Date).TotalDays > 0)
                    {
                        itemLate = 2;
                    }
                }

                if (planEnd.HasValue)
                {
                    int daysUntilEnd = (planEnd.Value.Date - DateTime.Now.Date).Days;
                    node.SetValue(colTotalDayExpridSoon, daysUntilEnd);
                }
                else
                {
                    node.SetValue(colTotalDayExpridSoon, 0);
                }

                node.SetValue(colItemLate, itemLate);


                // Cập nhật trạng thái hoàn thành
                if (e.Column == colActualStartDate || e.Column == colActualEndDate)
                {
                    DateTime? actualEndDate = TextUtils.ToDate4(node.GetValue(colActualEndDate));

                    if (actualEndDate.HasValue)
                    {
                        node.SetValue(colUpdatedDateActual, DateTime.Now);
                        node.SetValue(colStatus, 2);

                        DateTime? planEndDate = TextUtils.ToDate4(node.GetValue(colPlanEndDate));
                        if (planEndDate.HasValue && (actualEndDate.Value - planEndDate.Value).TotalDays > 0)
                        {
                            node.SetValue(colIsUpdateLate, 1);
                        }
                    }
                    else
                    {
                        node.SetValue(colUpdatedDateActual, DBNull.Value);
                        node.SetValue(colStatus,
                            TextUtils.ToDate4(node.GetValue(colActualStartDate)).HasValue ? 1 : 0);
                    }
                }

                // Xử lý thay đổi trạng thái
                if (e.Column == colStatus)
                {
                    int status = TextUtils.ToInt(e.Value);
                    if (status == 1)
                    {
                        node.SetValue(colActualStartDate, DateTime.Now);
                    }
                    else if (status == 2)
                    {
                        node.SetValue(colActualEndDate, DateTime.Now);
                    }
                }
            }
            finally
            {
                SetupFormatRules();
                Lib.LockEvents = false;
                tlCategory.RefreshDataSource();
            }
        }

        private void tlCategory_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl.FocusedNode == null) return;

            int id = TextUtils.ToInt(tl.FocusedNode.GetValue("ID"));
            if (id > 0)
            {
                int isApproved = TextUtils.ToInt(tl.FocusedNode.GetValue("IsApproved"));
                string createdBy = TextUtils.ToString(tl.FocusedNode.GetValue("CreatedBy")).Trim();
                int userID = TextUtils.ToInt(tl.FocusedNode.GetValue("UserID"));
                int employeeRequest = TextUtils.ToInt(tl.FocusedNode.GetValue("EmployeeIDRequest"));

                // Kiểm tra cột Mission
                bool isMissionColumn = tl.FocusedColumn.FieldName == "Mission";
                // Kiểm tra các cột thuộc nhóm "Plan" (danh sách cần được xác định)
                bool isPlanColumn = new[] { "PlanStartDate", "PlanEndDate" /* thêm các cột khác nếu cần */ }.Contains(tl.FocusedColumn.FieldName);

                if (Global.IsAdmin) return;

                if (isApproved == 3)
                {
                    tl.BeginUpdate();
                    e.Valid = false;
                    e.ErrorText = "Đã duyệt thực tế.\nBạn không thể cập nhật!";
                    tl.EndUpdate();
                }
                else if (!checkIsPermission(createdBy, userID, employeeRequest))
                {
                    tl.BeginUpdate();
                    e.Valid = false;
                    e.ErrorText = "Bạn không thể cập nhật hạng mục của người khác!";
                    tl.EndUpdate();
                }
                else if (isMissionColumn || isPlanColumn)
                {
                    int statusUpdate = TextUtils.ToInt(tl.FocusedNode.GetValue("StatusUpdate"));
                    if (statusUpdate != 2 && statusUpdate != 1)
                    {
                        DataRow[] dataRows = dtProjectItem.Select($"ID = {id}");
                        if (dataRows.Length > 0)
                        {
                            int dtRowHandle = dtProjectItem.Rows.IndexOf(dataRows[0]);
                            dtProjectItem.Rows[dtRowHandle]["StatusUpdate"] = 2;
                        }
                    }
                }
            }
        }


        private void tlCategory_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    TreeListHitInfo info = tlCategory.CalcHitInfo(e.Location);

            //    if (info.HitInfoType == HitInfoType.Column && info.Column != null && info.Column == colSTT)
            //    {
            //        tlCategory.FocusedNode = null; 

            //        List<int> listSTT = new List<int>();
            //        DataTable dt = (DataTable)tlCategory.DataSource;
            //        dt.AcceptChanges();
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            string strSTT = TextUtils.ToString(dt.Rows[i]["STT"]);
            //            if (!strSTT.Contains("."))
            //            {
            //                int stt = TextUtils.ToInt(dt.Rows[i]["STT"]);
            //                listSTT.Add(stt);
            //            }
            //        }

            //        DataRow dtrow = dt.NewRow();
            //        // Thiết lập ParentID
            //        if (tlCategory.FocusedNode != null)
            //        {
            //            dtrow["ParentID"] = tlCategory.FocusedNode.GetValue("ID");
            //        }
            //        else
            //        {
            //            dtrow["ParentID"] = 0;
            //        }
            //        dtrow["STT"] = listSTT.Count > 0 ? (listSTT.Max() + 1).ToString() : "1";
            //        dtrow["Code"] = $"{project.ProjectCode}_{dt.Rows.Count + 1}";
            //        dt.Rows.Add(dtrow);

            //        tlCategory.DataSource = dt;
            //        tlCategory.RefreshDataSource();

            //        // Focus vào node mới thêm
            //        tlCategory.FocusedNode = tlCategory.FindNodeByKeyID(dtrow["ID"]);
            //        tlCategory.FocusedColumn = tlCategory.Columns["Mission"];
            //        // Nếu cần sort, thêm dòng sau:
            //        // tlCategory.Columns["TypeProjectItem"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            //    }
            //}
        }

        void AddNewNode(TreeListNode parentNode)
        {
            try
            {
                if (Lib.LockEvents) return;
                tlCategory.FocusedColumn = colID;
                if (tlCategory.DataSource is DataTable dt)
                {
                    DataRow newRow = dt.NewRow();

                    // Tạo ID mới
                    int tempID = -1;
                    if (dt.Rows.Count > 0)
                    {
                        var negativeIDs = dt.AsEnumerable()
                            .Where(r => r["ID"] != DBNull.Value && Convert.ToInt32(r["ID"]) < 0)
                            .Select(r => Convert.ToInt32(r["ID"]))
                            .ToList();

                        tempID = (negativeIDs.Count > 0 ? negativeIDs.Min() - 1 : -1);
                    }
                    // Get the parent information
                    int parentID = 0;
                    string parentSTT = "0";
                    string parentCode = project.ProjectCode;

                    if (parentNode != null)
                    {
                        parentID = TextUtils.ToInt(parentNode.GetValue("ID"));
                        parentSTT = TextUtils.ToString(parentNode.GetValue("STT"));
                    }

                    if (parentID == 0)
                    {
                        var maxSTTRow = dt.AsEnumerable()
                            .Where(r => TextUtils.ToInt(r["ParentID"]) == 0).FirstOrDefault();

                        if (maxSTTRow != null)
                        {
                            newRow["EmployeeIDRequest"] = TextUtils.ToInt(maxSTTRow["EmployeeIDRequest"]);
                            newRow["UserID"] = TextUtils.ToInt(maxSTTRow["UserID"]);
                            newRow["TypeProjectItem"] = TextUtils.ToInt(maxSTTRow["TypeProjectItem"]);
                            newRow["EmployeeRequestID"] = TextUtils.ToInt(maxSTTRow["EmployeeRequestID"]);
                            newRow["EmployeeRequestName"] = TextUtils.ToString(maxSTTRow["EmployeeRequestName"]);
                        }
                    }
                    else
                    {

                        var maxSTTChildRow = dt.AsEnumerable()
                            .Where(r => TextUtils.ToInt(r["ParentID"]) == parentID)
                            .FirstOrDefault();

                        if (maxSTTChildRow != null)
                        {
                            newRow["EmployeeIDRequest"] = TextUtils.ToInt(maxSTTChildRow["EmployeeIDRequest"]);
                            newRow["UserID"] = TextUtils.ToInt(maxSTTChildRow["UserID"]);
                            newRow["TypeProjectItem"] = TextUtils.ToInt(maxSTTChildRow["TypeProjectItem"]);
                            newRow["EmployeeRequestID"] = TextUtils.ToInt(maxSTTChildRow["EmployeeRequestID"]);
                            newRow["EmployeeRequestName"] = TextUtils.ToString(maxSTTChildRow["EmployeeRequestName"]);
                        }
                        else
                        {
                            var parent = tlCategory.Nodes.Where(r => TextUtils.ToInt(r["ID"]) == parentID).First();
                            newRow["EmployeeIDRequest"] = TextUtils.ToInt(parent.GetValue("EmployeeIDRequest"));
                            newRow["UserID"] = TextUtils.ToInt(parent.GetValue("UserID"));
                            newRow["TypeProjectItem"] = TextUtils.ToInt(parent.GetValue("TypeProjectItem"));
                            newRow["EmployeeRequestID"] = TextUtils.ToInt(parent.GetValue("EmployeeRequestID"));
                            newRow["EmployeeRequestName"] = TextUtils.ToString(parent.GetValue("EmployeeRequestName"));
                        }
                    }

                    List<int> STTs = dt.AsEnumerable()
                        .Where(s => TextUtils.ToInt(s["ParentID"]) == parentID)
                        .Select(r => TextUtils.ToInt(r["STT"]))
                        .ToList();

                    int nextIndex = 1;
                    if (STTs.Count > 0)
                    {
                        nextIndex = STTs.Max() + 1;
                    }
                    List<string> Codes = dt.AsEnumerable().Select(s => TextUtils.ToString(s["Code"])).ToList();
                    List<int> sttCode = new List<int>();
                    foreach (string s in Codes)
                    {
                        int codeIndex = TextUtils.ToInt(s.Split('_')[1]);
                        sttCode.Add(codeIndex);
                    }
                    int indexcode = 1;
                    if (sttCode.Count > 0)
                    {
                        indexcode = sttCode.Max() + 1;
                    }
                    //List<int> sttCodes = Codes.Select(a => TextUtils.ToInt(a.Split(new char[] { '_', StringSplitOptions.RemoveEmptyEntries })[1]));
                    string newSTT = nextIndex.ToString();
                    string newCode = $"{parentCode}_{indexcode}";

                    newRow["ID"] = tempID;
                    newRow["ParentID"] = parentID;
                    newRow["STT"] = newSTT;
                    newRow["Code"] = newCode;
                    newRow["Status"] = 0;
                    newRow["IsApproved"] = 0;

                    dt.Rows.Add(newRow);

                    TreeListNode newNode = tlCategory.FindNodeByKeyID(newRow["ID"]);
                    tlCategory.FocusedNode = newNode;
                    tlCategory.FocusedColumn = colMission;
                    tlCategory.RefreshDataSource();
                    dtProjectItem = (DataTable)tlCategory.DataSource;
                    dtProjectItem.AcceptChanges();
                    isDataChanged = true;

                    tlCategory.FocusedColumn = tlCategory.Columns["Mission"];
                    SetupFormatRules();
                    tlCategory.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }


        private void tlCategory_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            //loadDetail();
        }

        private void cboEmployeeRequestID_Click(object sender, EventArgs e)
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

            // Cập nhật giá trị cho node đang được chọn trong TreeList
            if (tlCategory.FocusedNode != null)
            {
                tlCategory.FocusedNode.SetValue("EmployeeRequestID", employeeID);
                tlCategory.FocusedNode.SetValue("EmployeeRequestName", fullName);
            }
        }

        private void tlCategory_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (tlCategory.FocusedColumn != colEmployeeRequestName) return;
            int employeeRequestID = TextUtils.ToInt(tlCategory.GetFocusedRowCellValue(colEmployeeRequestID));
            e.Cancel = !(employeeRequestID <= 0);
        }

        private void cboTypeProjectItem_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cboUserID_EditValueChanged(object sender, EventArgs e)
        {

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
            //tlCategory.FocusedNode.SetValue(colEmployeeIDRequest, employeeID);
            tlCategory.FocusedNode.SetValue(colEmployeeRequestName, fullName);
        }


        private void btnDeleteRepo_Click(object sender, EventArgs e)
        {
            TreeListNode focusedNode = tlCategory.FocusedNode;
            if (focusedNode == null) return;

            int ID = TextUtils.ToInt(focusedNode.GetValue("ID"));
            int isApproved = TextUtils.ToInt(focusedNode.GetValue("IsApproved"));
            string isApprovedText = cboIsApproved.GetDisplayText(isApproved);
            int userID = TextUtils.ToInt(focusedNode.GetValue("UserID"));
            int parentID = TextUtils.ToInt(focusedNode.GetValue("ParentID"));

            if (ID > 0)
            {
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
                }
            }

            if (MessageBox.Show("Bạn có muốn xóa hay không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                tlCategory.BeginUpdate();
                try
                {
                    listID.Add(ID);
                    int deletedSTT = TextUtils.ToInt(focusedNode.GetValue("STT"));
                    tlCategory.DeleteNode(focusedNode);
                    UpdateSTTForNodes(parentID, deletedSTT);

                    TreeListNode nextNode = GetNextNode(focusedNode);
                    if (nextNode != null)
                    {
                        tlCategory.FocusedNode = nextNode;
                    }
                }
                finally
                {
                    tlCategory.EndUpdate();
                    tlCategory.RefreshDataSource();
                    dtProjectItem = (DataTable)tlCategory.DataSource;
                    dtProjectItem.AcceptChanges();
                    isDataChanged = true;
                }
            }
        }
        //private void UpdateSTTForNodes(int parentId, int deletedSTT)
        //{
        //    // Tìm parent node dựa trên parentId
        //    TreeListNode parentNode = tlCategory.FindNodeByKeyID(parentId);
        //    if (parentNode == null)
        //    {
        //        // Trường hợp parentId = 0 (node gốc)
        //        if (parentId == 0)
        //        {
        //            var rootNodes = tlCategory.Nodes
        //                .Where(n => TextUtils.ToInt(n.GetValue("STT")) > deletedSTT)
        //                .OrderBy(n => TextUtils.ToInt(n.GetValue("STT")))
        //                .ToList();

        //            foreach (var node in rootNodes)
        //            {
        //                int currentSTT = TextUtils.ToInt(node.GetValue("STT"));
        //                node.SetValue("STT", currentSTT - 1);
        //            }
        //        }
        //        return;
        //    }

        //    // Lấy các node con của parentNode có STT > deletedSTT
        //    var siblingNodes = parentNode.Nodes
        //        .Where(n => TextUtils.ToInt(n.GetValue("STT")) > deletedSTT)
        //        .OrderBy(n => TextUtils.ToInt(n.GetValue("STT")))
        //        .ToList();

        //    // Dịch chuyển STT cho các node này
        //    foreach (var node in siblingNodes)
        //    {
        //        int currentSTT = TextUtils.ToInt(node.GetValue("STT"));
        //        node.SetValue("STT", currentSTT - 1);
        //    }
        //}

        private void UpdateSTTForNodes(int parentId, int deletedSTT)
        {
            TreeListNode parentNode = tlCategory.FindNodeByKeyID(parentId);
            var nodesToUpdate = (parentNode != null ? parentNode.Nodes : tlCategory.Nodes)
                .Cast<TreeListNode>()
                .Where(n => TextUtils.ToInt(n.GetValue("STT")) > deletedSTT)
                .ToList();

            foreach (var node in nodesToUpdate)
            {
                int currentSTT = TextUtils.ToInt(node.GetValue("STT"));
                node.SetValue("STT", currentSTT - 1);
            }
        }
        //private void frmProjectItemNew_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (isAdding)
        //    {
        //        MessageBox.Show("Đang lưu dữ liệu.\nVui lòng chờ giây lát...", "Thông báo");
        //        e.Cancel = true;
        //    }
        //    tlCategory.FocusedNode = null;

        //    if (changedRows != null && isSave==false)
        //    {
        //        DialogResult dialog = MessageBox.Show("Bạn có muốn lưu những thay đổi không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        //        if (dialog == DialogResult.Yes)
        //        {
        //            //btnCloseAndSave_Click(null, null);
        //            if (saveData())
        //            {

        //                e.Cancel = false;
        //                this.DialogResult = DialogResult.OK;
        //            }
        //            else
        //            {
        //                e.Cancel = true;
        //            }

        //        }
        //        else if (dialog == DialogResult.No)
        //        {
        //            e.Cancel = false;
        //        }
        //        else
        //        {
        //            e.Cancel = true;
        //        }
        //    }
        //}
        private void frmProjectItemNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isAdding)
            {
                MessageBox.Show("Đang lưu dữ liệu.\nVui lòng chờ giây lát...", "Thông báo");
                e.Cancel = true;
                return;
            }

            tlCategory.FocusedNode = null;

            // Kiểm tra nếu có thay đổi mà chưa lưu
            if (isDataChanged && isSave == false)
            {
                DialogResult dialog = MessageBox.Show("Bạn có muốn lưu những thay đổi không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    if (saveData()) // Gọi phương thức lưu dữ liệu
                    {
                        e.Cancel = false; // Cho phép đóng form
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        e.Cancel = true; // Ngừng đóng form nếu lưu thất bại
                    }
                }
                else if (dialog == DialogResult.No)
                {
                    e.Cancel = false; // Cho phép đóng form mà không lưu
                }
                else
                {
                    e.Cancel = true; // Hủy đóng form nếu chọn Cancel
                }
            }
            else
            {
                e.Cancel = false; // Nếu không có thay đổi, cho phép đóng form
            }
        }

        private bool IsChildOf(TreeListNode parent, TreeListNode child)
        {
            TreeListNode current = child;
            while (current != null)
            {
                if (current == parent) return true;
                current = current.ParentNode;
            }
            return false;
        }

        private void tlCategory_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeListHitInfo hitInfo = tlCategory.CalcHitInfo(e.Location);
                if (hitInfo.Node != null && hitInfo.Node.Selected)
                {
                    tlCategory.DoDragDrop(hitInfo.Node, DragDropEffects.Move);
                }
            }
        }
        bool IsDragging = false;

        private void tlCategory_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeListNode)))
            {
                IsDragging = true;
                TreeListNode draggedNode = (TreeListNode)e.Data.GetData(typeof(TreeListNode));

                Point clientPoint = tlCategory.PointToClient(new Point(e.X, e.Y));

                TreeListHitInfo hitInfo = tlCategory.CalcHitInfo(clientPoint);
                TreeListNode targetNode = hitInfo.Node;
                bool isValidTarget = true;

                if (targetNode != null)
                {
                    int targetParentID = TextUtils.ToInt(targetNode.GetValue("ParentID"));
                    if (targetParentID != 0)
                    {
                        isValidTarget = false;
                    }

                    //update
                    if (draggedNode.HasChildren)
                    {
                        isValidTarget = false;
                    }
                }

                // Kiểm tra node không được tự kéo vào chính nó hoặc con của nó
                if (draggedNode == targetNode || IsChildOf(draggedNode, targetNode))
                {
                    isValidTarget = false;
                }

                e.Effect = isValidTarget ? DragDropEffects.Move : DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void tlCategory_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeListNode)))
            {
                TreeListNode draggedNode = (TreeListNode)e.Data.GetData(typeof(TreeListNode));
                Point clientPoint = tlCategory.PointToClient(new Point(e.X, e.Y));
                TreeListHitInfo hitInfo = tlCategory.CalcHitInfo(clientPoint);
                TreeListNode targetNode = hitInfo.Node;

                if (draggedNode == targetNode || IsChildOf(draggedNode, targetNode))
                    return;

                DataTable dt = (DataTable)tlCategory.DataSource;
                tlCategory.BeginUpdate();
                try
                {
                    DataRow draggedRow = ((DataRowView)tlCategory.GetDataRecordByNode(draggedNode)).Row;
                    object oldParentID = draggedRow["ParentID"]; // Lưu ParentID cũ

                    if (targetNode != null)
                    {
                        object targetID = targetNode.GetValue("ID");
                        int targetParentID = TextUtils.ToInt(targetNode.GetValue("ParentID"));
                        if (targetParentID != 0)
                        {
                            return;
                        }

                        //update
                        if (draggedNode.HasChildren)
                        {
                            return;
                        }

                        // Lấy STT hiện tại của các con trong node mới
                        List<int> lsSTT = dt.AsEnumerable()
                            .Where(r => TextUtils.ToInt(r["ParentID"]) == TextUtils.ToInt(targetID))
                            .Select(r => TextUtils.ToInt(r["STT"]))
                            .ToList();

                        // Cập nhật ParentID và STT mới cho node được kéo
                        draggedRow["ParentID"] = targetID;
                        draggedRow["STT"] = lsSTT.Count > 0 ? (lsSTT.Max() + 1) : 1;
                    }
                    else
                    {
                        // Kéo ra ngoài -> ParentID = 0
                        draggedRow["ParentID"] = 0;

                        List<int> lsSTT = dt.AsEnumerable()
                            .Where(r => TextUtils.ToInt(r["ParentID"]) == 0)
                            .Select(r => TextUtils.ToInt(r["STT"]))
                            .ToList();

                        int nextIndex = lsSTT.Count > 0 ? lsSTT.Max() + 1 : 1;
                        draggedRow["STT"] = nextIndex;
                    }

                    // Cập nhật STT cho các node còn lại trong node cha cũ (nếu có)
                    if (oldParentID != null)
                    {
                        var siblings = dt.AsEnumerable()
                            .Where(r => TextUtils.ToInt(r["ParentID"]) == TextUtils.ToInt(oldParentID) && r != draggedRow)
                            .OrderBy(r => TextUtils.ToInt(r["STT"]))
                            .ToList();

                        int newSTT = 1;
                        foreach (var sibling in siblings)
                        {
                            sibling["STT"] = newSTT++;
                        }
                    }

                    // Cuối cùng: đánh lại STT cho các node có ParentID = 0 (các node ngoài)
                    var parentZeroRows = dt.AsEnumerable()
                        .Where(r => TextUtils.ToInt(r["ParentID"]) == 0)
                        .OrderBy(r => TextUtils.ToInt(r["STT"]))
                        .ToList();

                    int stt = 1;
                    foreach (var row in parentZeroRows)
                    {
                        row["STT"] = stt++;
                    }

                    // Update lại view node
                    draggedNode.SetValue("STT", draggedRow["STT"]);
                    draggedNode.SetValue("ParentID", draggedRow["ParentID"]);
                    //isDataChanged = true;
                }
                finally
                {
                    tlCategory.EndUpdate();
                    tlCategory.RefreshDataSource();
                    dtProjectItem = (DataTable)tlCategory.DataSource;
                    dtProjectItem.AcceptChanges();
                    isDataChanged = true;
                }
            }
        }

        private void tlCategory_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeListHitInfo hitInfo = tlCategory.CalcHitInfo(e.Location);
                if (hitInfo.Node != null)
                {
                    tlCategory.FocusedNode = hitInfo.Node;
                }

                contextMenuStrip2.Show(tlCategory, e.Location);
            }
        }

        private void contextMenuStrip2_Opening_1(object sender, CancelEventArgs e)
        {
            //// Chỉ xử lý cho các column cụ thể
            //if (tlCategory.FocusedColumn != colUserID &&
            //    tlCategory.FocusedColumn != colEmployeeIDRequest)
            //{
            //    e.Cancel = true;
            //    return;
            //}

            //var focusedNode = tlCategory.FocusedNode;
            //if (focusedNode == null) return;

            //string value = TextUtils.ToString(focusedNode.GetDisplayText(tlCategory.FocusedColumn));
            //if (string.IsNullOrWhiteSpace(value)) return;

            //// Xóa các filter cũ
            //foreach (var item in itemFilters.ToArray())
            //{
            //    contextMenuStrip2.Items.Remove(item);
            //    item.Dispose();
            //}
            //itemFilters.Clear();

            //// Tạo menu item mới
            //ToolStripMenuItem filterItem = new ToolStripMenuItem
            //{
            //    Text = $"Lọc {value}",
            //    Tag = focusedNode.GetValue(tlCategory.FocusedColumn)
            //};
            //filterItem.Click += Item_Click;

            //// Thêm vào menu
            //contextMenuStrip2.Items.Add(filterItem);
            //itemFilters.Add(filterItem);
        }

        private void btnAddChild_Click(object sender, EventArgs e)
        {
            try
            {
                var focusedNode = tlCategory.FocusedNode;
                if (focusedNode == null)
                {
                    return;
                }
                else
                {
                    int parentID = TextUtils.ToInt(focusedNode.GetValue("ParentID"));
                    if (parentID != 0)
                    {
                        TreeListNode node = focusedNode.ParentNode;
                        AddNewNode(node);
                    }
                    else
                    {
                        AddNewNode(focusedNode);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAddParent_Click(object sender, EventArgs e)
        {
            AddNewNode(null);
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            tlCategory.ClearColumnsFilter();
            updateItemLate();
        }

        private void tlCategory_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            if (e.Column?.FieldName == "STT")
            {
                object val1Obj = e.Node1?.GetValue(e.Column);
                object val2Obj = e.Node2?.GetValue(e.Column);

                int val1 = SafeParseInt(val1Obj);
                int val2 = SafeParseInt(val2Obj);

                e.Result = val1.CompareTo(val2);
            }
        }

        private int SafeParseInt(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0;
            return int.TryParse(value.ToString(), out int result) ? result : 0;
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(tlCategory.FocusedNode.GetValue("ID"));
            if (id <= 0)
            {
                MessageBox.Show("Vui lòng chọn một hạng mục để thêm file đính kèm.");
                return;
            }
            flyoutPanel1.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Manual;

            int x = (Screen.PrimaryScreen.Bounds.Width / 2) - (flyoutPanel1.Width / 2);
            int y = (Screen.PrimaryScreen.Bounds.Height / 2) - (flyoutPanel1.Height / 2) - 300;
            flyoutPanel1.Options.Location = new System.Drawing.Point(x, y);
            List<ProjectItemFilesModel> listFile = SQLHelper<ProjectItemFilesModel>.FindByAttribute("ProjectItemID", id);
            grdProjectItemFiles.DataSource = listFile;
            if (flyoutPanel1.IsPopupOpen)
            {
                flyoutPanel1.HidePopup();
            }
            else
            {
                flyoutPanel1.ShowPopup();
            }
            if (lstDelete.Count > 0)
            {
                foreach (var item in lstDelete)
                {
                    ProjectItemFilesModel model = SQLHelper<ProjectItemFilesModel>.FindByID(item);
                    if (model != null)
                    {
                        model.IsDeleted = true;
                        SQLHelper<ProjectItemFilesModel>.Update(model);
                    }
                }
                lstDelete.Clear();
            }
        }

        private void btnOpenDialog_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(filePath);
                int id = TextUtils.ToInt(tlCategory.FocusedNode.GetValue("ID"));
                if (id <= 0)
                {
                    MessageBox.Show("Vui lòng chọn một hạng mục để thêm file đính kèm.");
                    return;
                }
                grvProjectItemFiles.SetFocusedRowCellValue(colFileName, fileName);
                grvProjectItemFiles.SetFocusedRowCellValue(colOriginpath, filePath);
                grvProjectItemFiles.SetFocusedRowCellValue(colProjectItemID, id);
            }
        }

        private void flyoutPanel1_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            if (e.Button.Tag.ToString() == "OK")
            {
                // Loop through all rows in the grid
                grvProjectItemFiles.FocusedRowHandle = -1; // Reset focused row handle
                for (int rowHandle = 0; rowHandle < grvProjectItemFiles.RowCount; rowHandle++)
                {
                    // Ensure the row is valid (rowHandle >= 0)
                    if (rowHandle >= 0)
                    {
                        int id = TextUtils.ToInt(grvProjectItemFiles.GetRowCellValue(rowHandle, "ID"));
                        ProjectItemFilesModel model = id > 0 ? SQLHelper<ProjectItemFilesModel>.FindByID(id) : new ProjectItemFilesModel();

                        model.ProjectItemID = TextUtils.ToInt(grvProjectItemFiles.GetRowCellValue(rowHandle, colProjectItemID));
                        model.FileName = TextUtils.ToString(grvProjectItemFiles.GetRowCellValue(rowHandle, colFileName));
                        model.OriginPath = TextUtils.ToString(grvProjectItemFiles.GetRowCellValue(rowHandle, colOriginpath));
                        model.Note = TextUtils.ToString(grvProjectItemFiles.GetRowCellValue(rowHandle, "Note"));
                        model.IsDeleted = false;
                        model.STT = TextUtils.ToInt(grvProjectItemFiles.GetRowCellValue(rowHandle, colAdd));

                        // Insert or update the record
                        if (model.ID > 0)
                        {
                            SQLHelper<ProjectItemFilesModel>.Update(model);
                        }
                        else
                        {
                            model.ID = SQLHelper<ProjectItemFilesModel>.Insert(model).ID;
                        }
                        uploadFile(model.ID);
                    }
                }

                // Hide the flyout panel after saving
                flyoutPanel1.HidePopup();
            }
            else if (e.Button.Tag.ToString() == "Cancel")
            {
                flyoutPanel1.HidePopup();
            }
        }

        List<int> lstDelete = new List<int>();
        private void btnDeletefile_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvProjectItemFiles.GetFocusedRowCellValue("ID"));
            string name = TextUtils.ToString(grvProjectItemFiles.GetFocusedRowCellValue("FileName"));
            if (MessageBox.Show($"Bạn có muốn xóa {name} hay không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (ID > 0)
                {
                    lstDelete.Add(ID);
                }
                grvProjectItemFiles.DeleteSelectedRows();
            }
        }
        List<ProjectItemFilesModel> listProjectFiles = new List<ProjectItemFilesModel>();
        private void grdProjectItemFiles_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvProjectItemFiles.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colAdd && info.HitTest == GridHitTest.Column)
                {
                    grvProjectItemFiles.FocusedRowHandle = -1;


                    listProjectFiles = grvProjectItemFiles.DataSource as List<ProjectItemFilesModel> ?? new List<ProjectItemFilesModel>();

                    List<int> listSTT = new List<int>();

                    foreach (var row in listProjectFiles)
                    {
                        int stt = row.STT ?? 0;
                        if (stt > 0)
                        {
                            listSTT.Add(stt);
                        }
                    }

                    ProjectItemFilesModel newFile = new ProjectItemFilesModel();
                    newFile.STT = listSTT.Count > 0 ? (listSTT.Max() + 1) : 1;
                    listProjectFiles.Add(newFile);

                    grdProjectItemFiles.DataSource = listProjectFiles;
                    grvProjectItemFiles.RefreshData();
                }
            }
        }

        async void uploadFile(int id)
        {
            try
            {
                // Lấy đường dẫn server từ cấu hình
                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathProjectItem").FirstOrDefault();
                if (config == null || string.IsNullOrEmpty(config.KeyValue))
                {
                    MessageBox.Show("Không tìm thấy cấu hình server upload!", "Thông báo");
                    return;
                }

                // Lấy thông tin file từ DB
                ProjectItemFilesModel model = SQLHelper<ProjectItemFilesModel>.FindByID(id);
                if (model == null)
                {
                    return;
                }

                string fileName = model.FileName.Trim();
                string filePath = model.OriginPath;  // Đường dẫn gốc của file
                string serverPath = config.KeyValue.Trim();

                if (!System.IO.File.Exists(filePath))
                {
                    MessageBox.Show("File không tồn tại trên máy!", "Thông báo");
                    return;
                }

                // Đọc dữ liệu từ file
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Tạo nội dung HTTP để upload
                using (var content = new MultipartFormDataContent())
                {
                    var fileContent = new ByteArrayContent(fileBytes);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    content.Add(fileContent, "file", fileName);

                    string url = $"https://localhost:7037/api/Employee/upload?path={serverPath}";

                    using (var client = new HttpClient())
                    {
                        var response = await client.PostAsync(url, content);
                        string responseContent = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            //MessageBox.Show("Upload file thành công!", "Thông báo");
                            Console.WriteLine("Upload file thành công!");
                        }
                        else
                        {
                            Console.WriteLine($"Upload file thất bại! {responseContent}");
                            //MessageBox.Show($"Upload thất bại! {responseContent}", "Thông báo");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi");
            }
        }

        private void btnResetParent_Click(object sender, EventArgs e)
        {
            TreeListNode focusedNode = tlCategory.FocusedNode;
            if (focusedNode != null)
            {
                DataTable dt = (DataTable)tlCategory.DataSource;
                int id = TextUtils.ToInt(focusedNode.GetValue("ID"));
                int oldParentId = TextUtils.ToInt(focusedNode.GetValue("ParentID"));
                int stt = TextUtils.ToInt(focusedNode.GetValue("STT"));

                // Kiểm tra xem node có phải là node gốc không
                if (oldParentId != 0)
                {
                    // Cập nhật STT cho các node cùng parent cũ
                    UpdateSTTForOldSiblings(dt, oldParentId, stt);

                    // Set ParentID thành 0 (trở thành node gốc)
                    focusedNode.SetValue("ParentID", 0);

                    // Tính STT mới dựa trên các node gốc
                    int maxRootSTT = dt.AsEnumerable()
                        .Where(row => TextUtils.ToInt(row["ParentID"]) == 0)
                        .Select(row => TextUtils.ToInt(row["STT"]))
                        .DefaultIfEmpty(0)
                        .Max();
                    int newSTT = maxRootSTT + 1;
                    focusedNode.SetValue("STT", newSTT);

                    // Làm mới TreeList
                    tlCategory.RefreshDataSource();
                    dtProjectItem = (DataTable)tlCategory.DataSource;
                    dtProjectItem.AcceptChanges();
                    isDataChanged = true;
                }
                else
                {
                    MessageBox.Show("Node này đã là node gốc.", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một node để reset ParentID.", "Thông báo");
            }
        }

        private void UpdateSTTForOldSiblings(DataTable dt, int parentId, int deletedSTT)
        {
            // Lọc các node cùng parent và có STT lớn hơn deletedSTT
            var siblings = dt.AsEnumerable()
                .Where(row => TextUtils.ToInt(row["ParentID"]) == parentId && TextUtils.ToInt(row["STT"]) > deletedSTT)
                .OrderBy(row => TextUtils.ToInt(row["STT"]));

            // Giảm STT của các node này đi 1
            foreach (var row in siblings)
            {
                row["STT"] = TextUtils.ToInt(row["STT"]) - 1;
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            tlCategory.ExpandAll();
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"HangMucCongViec_{project.ProjectCode}.xls");

                // Set up export options for Excel
                XlsExportOptions optionsEx = new XlsExportOptions();
                optionsEx.ExportMode = XlsExportMode.SingleFilePageByPage;  // Export in single file page by page

                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = tlCategory;

                try
                {
                    for (int i = tlCategory.Columns.Count - 1; i >= 0; i--)
                    {
                        if (string.IsNullOrWhiteSpace(tlCategory.Columns[i].Caption))
                        {
                            tlCategory.Columns.RemoveAt(i);
                        }
                    }


                    foreach (TreeListNode node in tlCategory.Nodes)
                    {
                        if (node.HasChildren)
                        {
                            ExportChildNodes(node);
                        }
                    }

                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();
                    compositeLink.ExportToXls(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ExportChildNodes(TreeListNode parentNode)
        {
            foreach (TreeListNode childNode in parentNode.Nodes)
            {
                ExportChildNodes(childNode);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var node = tlCategory.FocusedNode;
            if (node == null) return;

            if (node.ParentNode != null) node = node.ParentNode;

            //TreeListNode newNode = new TreeListNode();
            var newNode = new object[] { 1, "12313" };

            tlCategory.AppendNode(newNode, node);
        }
    }
}