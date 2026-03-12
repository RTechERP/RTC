using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
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

namespace Forms.DanhMuc.DuAn
{
    public partial class frmTreeProjectItem : _Forms
    {
        public int projectID;
        DataTable dtItem = new DataTable();

        DataSet dataSet = new DataSet();

        List<TreeListColumn> _lst = new List<TreeListColumn>();

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
        public frmTreeProjectItem()
        {
            InitializeComponent();
        }

        private void frmTreeProjectItem_Load(object sender, EventArgs e)
        {
            
            loadProjectItem();
            loadUser();
            loadStatus();
            //addColumnTimeline();
        }
        
        /// <summary>
        /// Load Hạng mục công việc
        /// </summary>
        void loadProjectItem()
        {
            dtItem = TextUtils.Select($"SELECT * FROM ProjectItem WHERE ProjectID = {projectID}");
            tlProjectItem.DataSource = dtItem;
            tlProjectItem.ExpandAll();

        }

        /// <summary>
        /// Load người phụ trách
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("select ID, Code, FullName, LoginName from Users where DepartmentID = 2 AND Status <> 1");

            cboUserID.ValueMember = "ID";
            cboUserID.DisplayMember = "FullName";
            cboUserID.DataSource = dt;
        }


        /// <summary>
        /// Load trạng thái
        /// </summary>
        void loadStatus()
        {
            List<Status> statuses = new List<Status>()
            {
                new Status(0,"Chưa làm"),
                new Status(1,"Đang làm"),
                new Status(2,"Đã hoàn thành"),
                new Status(3,"Pending")
            };

            cboStatus.ValueMember = "value";
            cboStatus.DisplayMember = "status";
            cboStatus.DataSource = statuses;

        }

        void addColumnTimeline()
        {
            
            //colTimeline.Visible = false;
            loadProjectItem();
            DataTable dt = dataSet.Tables[1];

            DateTime dateMin = TextUtils.ToDate1(TextUtils.ToString(dt.Rows[0]["AllDates"]));
            dateMin = dateMin.AddDays(-1);

            DateTime dateMax = TextUtils.ToDate1(TextUtils.ToString(dt.Rows[dt.Rows.Count - 1]["AllDates"]));
            dateMax = dateMax.AddDays(+1);

            DataRow dataRow1 = dt.NewRow();
            DataRow dataRow2 = dt.NewRow();

            dataRow1["AllDates"] = dateMin;
            dt.Rows.InsertAt(dataRow1, 0);

            dataRow2["AllDates"] = dateMax;
            dt.Rows.InsertAt(dataRow2, dt.Rows.Count + 1);

            if (_lst.Count > 0)
            {
                for (int i = 0; i < _lst.Count; i++)
                {
                    tlProjectItem.Columns.Remove(_lst[i]);
                }

                _lst.Clear();
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string capt = TextUtils.ToString(dt.Rows[i]["AllDates"]);
                    if (!string.IsNullOrEmpty(capt))
                    {
                        TreeListColumn col = tlProjectItem.Columns.Add();
                        col.Caption = capt.Substring(0, 5).Replace("/"," ");
                        col.FieldName = capt.Substring(0, 10);
                        col.Name = "colDate" + i;
                        col.Visible = true;
                        col.Width = 30;
                        col.MinWidth = 30;
                        col.MaxWidth = 30;
                        col.OptionsColumn.AllowEdit = false;
                        col.OptionsColumn.AllowSort = false;
                        col.OptionsColumn.AllowMove = false;
                        col.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        col.AppearanceHeader.Options.UseFont = true;
                        col.AppearanceHeader.Options.UseForeColor = true;
                        col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                        _lst.Add(col);
                    }

                }
            }
        }

        private void tlProjectItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeListHitInfo info = tlProjectItem.CalcHitInfo(e.Location);
                if (info.Column != null && info.Column == colSTT && info.HitInfoType == HitInfoType.Column)
                {
                    if (dtItem.Rows.Count <= 0)
                    {
                        btnAddParent_Click(null, null);
                    }
                    else
                    {
                        int currentID = TextUtils.ToInt(tlProjectItem.GetFocusedRowCellValue(colID));
                        int userID = TextUtils.ToInt(tlProjectItem.GetFocusedRowCellValue(colUserID));

                        DataRow dtrow = dtItem.NewRow();
                        if (currentID <= 0)
                        {
                            currentID = saveParent();
                            tlProjectItem.SetFocusedRowCellValue(colID, currentID);

                            DataTable dt = TextUtils.Select("select top 1 id, userid from ProjectItem order by ID desc");

                            currentID = TextUtils.ToInt(dt.Rows[0]["id"]);
                            userID = TextUtils.ToInt(dt.Rows[0]["userid"]);
                        }

                        dtrow["ParentID"] = currentID;
                        dtrow["UserID"] = userID;
                        dtrow["Status"] = 0;

                        dtItem.Rows.Add(dtrow);
                        tlProjectItem.ExpandAll();

                        TreeListNode node = tlProjectItem.FindNodeByFieldValue("ID", currentID);
                        tlProjectItem.SetFocusedNode(node);
                    }
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(tlProjectItem.GetFocusedRowCellValue(colID));

            if (MessageBox.Show(string.Format("Bạn có muốn xóa hạng mục công việc này không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ProjectItemBO.Instance.Delete(ID);

                DataTable dt = TextUtils.Select($"select ID from ProjectItemDetail where ProjectItemID = {ID}");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProjectItemDetailBO.Instance.Delete(TextUtils.ToInt(dt.Rows[i]["ID"]));
                }
                loadProjectItem();
               
            }
        }

        private void frmTreeProjectItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("Công việc chưa lưu sẽ bị mất. \nBạn có chắc muốn thoát không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //{
            //    this.DialogResult = DialogResult.OK;
            //}
        }

        private void tlProjectItem_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (tlProjectItem.GetNodeList().Count > 0)
            {
                foreach (TreeListNode node in tlProjectItem.GetNodeList())
                {
                    if (e.Column == colTotalDay)
                    {
                        decimal total = TextUtils.ToDecimal(tlProjectItem.GetSummaryValue(colTotalDay));

                        //set % khi nhập Tổng số ngày
                        if (total > 0)
                        {
                            decimal percent = (TextUtils.ToDecimal(tlProjectItem.GetRowCellValue(node, colTotalDay)) / total) * 100;
                            tlProjectItem.SetRowCellValue(node, colPercent, percent.ToString("0.#"));

                        }

                        //Set Ngày kết thúc dự kiến khi nhập Tổng số ngày
                        if (TextUtils.ToDate4(tlProjectItem.GetRowCellValue(node, colPlanStartDate)).HasValue)
                        {
                            DateTime planDS = TextUtils.ToDate1(TextUtils.ToString(tlProjectItem.GetRowCellValue(node, colPlanStartDate)));
                            DateTime planDE = planDS.AddDays(TextUtils.ToDouble(tlProjectItem.GetRowCellValue(node, colTotalDay)) - 1);

                            tlProjectItem.SetRowCellValue(node, colPlanEndDate, planDE);
                        }
                    }

                    //Set Ngày kết thúc dự kiến khi nhập Ngày bắt đầu dự kiến
                    if (e.Column == colPlanStartDate)
                    {
                        double totalDay = TextUtils.ToDouble(tlProjectItem.GetRowCellValue(node, colTotalDay));
                        if (totalDay > 0)
                        {
                            DateTime planDS = TextUtils.ToDate1(TextUtils.ToString(tlProjectItem.GetRowCellValue(node, colPlanStartDate)));
                            DateTime planDE = planDS.AddDays(TextUtils.ToDouble(tlProjectItem.GetRowCellValue(node, colTotalDay)) - 1);

                            tlProjectItem.SetRowCellValue(node, colPlanEndDate, planDE);
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TreeListNode node = tlProjectItem.FindNodeByKeyID(colParentID);
            tlProjectItem.SetFocusedNode(node);
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }


        int parentID = 0;
        /// <summary>
        /// Lưu hạng mục công việc
        /// </summary>
        bool saveData()
        {
            foreach (TreeListNode node in tlProjectItem.GetNodeList())
            {
                long id = TextUtils.ToInt64(tlProjectItem.GetRowCellValue(node, colID));

                bool hasChild = false;

                ProjectItemModel projectItem = new ProjectItemModel();
                ProjectItemDetailModel detailParent = new ProjectItemDetailModel();
                ProjectItemDetailModel detailPlan = new ProjectItemDetailModel();
                ProjectItemDetailModel detailActual = new ProjectItemDetailModel();
                if (id > 0)
                {
                    projectItem = (ProjectItemModel)ProjectItemBO.Instance.FindByPK(id);
                    //detailParent = (ProjectItemDetailModel)ProjectItemDetailBO.Instance.FindByCode("ProjectItemID", "Type", id.ToString(), "0");
                    detailParent = (ProjectItemDetailModel)ProjectItemDetailBO.Instance.FindByCode("ProjectItemID", id.ToString());
                }
                projectItem.Status = TextUtils.ToInt(tlProjectItem.GetRowCellValue(node, colStatus));
                projectItem.STT = TextUtils.ToString(tlProjectItem.GetRowCellValue(node, colSTT));
                projectItem.UserID = TextUtils.ToInt(tlProjectItem.GetRowCellValue(node, colUserID));
                projectItem.ProjectID = projectID;
                projectItem.Mission = TextUtils.ToString(tlProjectItem.GetRowCellValue(node, colMission));

                projectItem.PlanStartDate = TextUtils.ToDate4(tlProjectItem.GetRowCellValue(node, colPlanStartDate));
                projectItem.PlanEndDate = TextUtils.ToDate4(tlProjectItem.GetRowCellValue(node, colPlanEndDate));
                projectItem.ActualStartDate = TextUtils.ToDate4(tlProjectItem.GetRowCellValue(node, colActualStartDate));
                projectItem.ActualEndDate = TextUtils.ToDate4(tlProjectItem.GetRowCellValue(node, colActualEndDate));
                projectItem.TotalDayPlan = TextUtils.ToDecimal(tlProjectItem.GetRowCellValue(node, colTotalDay));
                projectItem.Note = TextUtils.ToString(tlProjectItem.GetRowCellValue(node, colNote));
                projectItem.PercentItem = TextUtils.ToDecimal(tlProjectItem.GetRowCellValue(node, colPercent));
                projectItem.ParentID = TextUtils.ToInt(tlProjectItem.GetRowCellValue(node, colParentID));

                if (projectItem.ID > 0)
                {
                    ProjectItemBO.Instance.Update(projectItem);
                }
                else
                {                    
                  projectItem.ID = (int)ProjectItemBO.Instance.Insert(projectItem);
                }

                //List<DateTime?> lisDate = new List<DateTime?>();
                //lisDate.Add(projectItem.PlanStartDate);
                //lisDate.Add(projectItem.PlanEndDate);
                //lisDate.Add(projectItem.ActualStartDate);
                //lisDate.Add(projectItem.ActualEndDate);

                if (detailParent == null)
                {
                    detailParent = new ProjectItemDetailModel();
                }
                else
                {
                    hasChild = detailParent.HasChild;
                }

                detailParent.ProjectItemID = projectItem.ID;
                detailParent.StartDate = projectItem.PlanStartDate;
                detailParent.EndDate = projectItem.PlanEndDate;
                detailParent.Type = 0;
                detailParent.ParentID = 0;
                detailParent.HasChild = node.HasChildren;

                if (hasChild != detailParent.HasChild)
                {
                    DataTable dt = TextUtils.Select($"select * from ProjectItemDetail where ProjectItemID = {id}");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProjectItemDetailBO.Instance.Delete(TextUtils.ToInt(dt.Rows[i]["ID"]));
                    }
                }

                if (!node.HasChildren && node.ParentNode == null)
                {
                    // MessageBox.Show("KHông cha k con",node.GetValue(colID).ToString());

                    detailParent.StartDate = null;
                    detailParent.EndDate = null;

                    detailPlan = (ProjectItemDetailModel)ProjectItemDetailBO.Instance.FindByCode("ProjectItemID","Type", id.ToString(), "1");
                    detailActual = (ProjectItemDetailModel)ProjectItemDetailBO.Instance.FindByCode("ProjectItemID", "Type", id.ToString(), "2");

                    if (detailPlan == null)
                    {
                        detailPlan = new ProjectItemDetailModel();
                    }

                    if (detailActual == null)
                    {
                        detailActual = new ProjectItemDetailModel();
                    }

                    detailPlan.ProjectItemID = projectItem.ID;
                    detailPlan.StartDate = projectItem.PlanStartDate;
                    detailPlan.EndDate = projectItem.PlanEndDate;
                    detailPlan.Type = 1;
                    detailPlan.ParentID = detailParent.ID;
                    detailPlan.HasChild = node.HasChildren;

                    detailActual.ProjectItemID = projectItem.ID;
                    detailActual.StartDate = projectItem.ActualStartDate;
                    detailActual.EndDate = projectItem.ActualEndDate;
                    detailActual.Type = 2;
                    detailActual.ParentID = detailParent.ID;
                    detailActual.HasChild = node.HasChildren;

                    if (detailParent.ID > 0)
                    {
                        //ProjectItemDetailBO.Instance.Update(detailParent);
                        ProjectItemDetailBO.Instance.Update(detailPlan);
                        ProjectItemDetailBO.Instance.Update(detailActual);
                    }
                    else
                    {
                        //detailParent.ID = (int)ProjectItemDetailBO.Instance.Insert(detailParent);

                      
                        detailPlan.ParentID = detailParent.ID;
                        detailActual.ParentID = detailParent.ID;

                        ProjectItemDetailBO.Instance.Insert(detailPlan);
                        ProjectItemDetailBO.Instance.Insert(detailActual);
                    }
                }
                else if (!node.HasChildren && !node.HasAsParent(node))
                {
                    //MessageBox.Show("Là con và có cha",node.GetValue(colID).ToString());

                    //ProjectItemDetailModel detailPlan = new ProjectItemDetailModel();
                    //ProjectItemDetailModel detailActual = new ProjectItemDetailModel();

                    DataTable dt = TextUtils.Select($"select id from ProjectItemDetail where ProjectItemID = {node.ParentNode.GetValue(colID)}");
                    int parent = TextUtils.ToInt(dt.Rows[0]["id"]);

                    if (id > 0)
                    {
                        detailPlan = (ProjectItemDetailModel)ProjectItemDetailBO.Instance.FindByCode("ProjectItemID", "Type", id.ToString(), "1");
                        detailActual = (ProjectItemDetailModel)ProjectItemDetailBO.Instance.FindByCode("ProjectItemID", "Type", id.ToString(), "2");
                    }

                    if (detailPlan == null)
                    {
                        detailPlan = new ProjectItemDetailModel();
                    }

                    if (detailActual == null)
                    {
                        detailActual = new ProjectItemDetailModel();
                    }

                    detailPlan.ProjectItemID = projectItem.ID;
                    detailPlan.StartDate = projectItem.PlanStartDate;
                    detailPlan.EndDate = projectItem.PlanEndDate;
                    detailPlan.Type = 1;
                    detailPlan.ParentID = parent;
                    detailPlan.HasChild = node.HasChildren;

                    detailActual.ProjectItemID = projectItem.ID;
                    detailActual.StartDate = projectItem.ActualStartDate;
                    detailActual.EndDate = projectItem.ActualEndDate;
                    detailActual.Type = 2;
                    detailActual.ParentID = parent;
                    detailActual.HasChild = node.HasChildren;

                    if (detailPlan.ID > 0)
                    {
                        ProjectItemDetailBO.Instance.Update(detailPlan);
                    }
                    else
                    {
                        ProjectItemDetailBO.Instance.Insert(detailPlan);
                    }

                    if (detailActual.ID > 0)
                    {
                        ProjectItemDetailBO.Instance.Update(detailActual);
                    }
                    else
                    {
                        ProjectItemDetailBO.Instance.Insert(detailActual);
                    }
                }
                else if (node.HasChildren && node.ParentNode != null)
                {
                    //MessageBox.Show("Là cha và là con", node.GetValue(colID).ToString());

                    detailParent.ProjectItemID = projectItem.ID;
                    detailParent.StartDate = null;
                    detailParent.EndDate = null;
                    detailParent.Type = 0;
                    detailParent.ParentID = parentID;
                    detailParent.HasChild = node.HasChildren;

                    if (hasChild != detailParent.HasChild)
                    {
                        detailParent.ID = (int)ProjectItemDetailBO.Instance.Insert(detailParent);
                        parentID = detailParent.ID;
                    }
                    else
                    {
                        if (detailParent.ID > 0)
                        {
                            ProjectItemDetailBO.Instance.Update(detailParent);
                            parentID = detailParent.ID;
                        }
                        else
                        {
                            detailParent.ID = (int)ProjectItemDetailBO.Instance.Insert(detailParent);
                            parentID = detailParent.ID;
                        }
                    }
                    
                }
                else
                {
                    //MessageBox.Show("Là cha và có con",node.GetValue(colID).ToString());
                    //if (detailParent.HasChild)
                    //{
                    //    ProjectItemDetailBO.Instance.Delete(detailParent.ID);
                    //}

                    detailParent.StartDate = null;
                    detailParent.EndDate = null;

                    if (detailParent.ID > 0)
                    {
                        ProjectItemDetailBO.Instance.Update(detailParent);
                        parentID = detailParent.ID;
                    }
                    else
                    {
                        detailParent.ID = (int)ProjectItemDetailBO.Instance.Insert(detailParent);
                        parentID = detailParent.ID;
                    }
                }
                //lisDate.Clear();
            }

            return true;
        }

        /// <summary>
        /// Lưu hạng mục công việc cha
        /// </summary>
        int saveParent()
        {
            ProjectItemModel projectItem = new ProjectItemModel();
            long id = TextUtils.ToInt64(tlProjectItem.GetFocusedRowCellValue(colID));

            if (id > 0)
            {
                projectItem = (ProjectItemModel)ProjectItemBO.Instance.FindByPK(id);
            }

            projectItem.Status = TextUtils.ToInt(tlProjectItem.GetFocusedRowCellValue(colStatus));
            projectItem.STT = TextUtils.ToString(tlProjectItem.GetFocusedRowCellValue(colSTT));
            projectItem.UserID = TextUtils.ToInt(tlProjectItem.GetFocusedRowCellValue(colUserID));
            projectItem.ProjectID = projectID;
            projectItem.Mission = TextUtils.ToString(tlProjectItem.GetFocusedRowCellValue(colMission));
            //projectItem.PlanStartDate = null;
            //projectItem.PlanEndDate = null;
            //projectItem.ActualStartDate = null;
            //projectItem.ActualEndDate = null;

            projectItem.PlanStartDate = TextUtils.ToDate4(tlProjectItem.GetFocusedRowCellValue(colPlanStartDate));
            projectItem.PlanEndDate = TextUtils.ToDate4(tlProjectItem.GetFocusedRowCellValue(colPlanEndDate));
            projectItem.ActualStartDate = TextUtils.ToDate4(tlProjectItem.GetFocusedRowCellValue(colActualStartDate));
            projectItem.ActualEndDate = TextUtils.ToDate4(tlProjectItem.GetFocusedRowCellValue(colActualEndDate));
            projectItem.TotalDayPlan = TextUtils.ToDecimal(tlProjectItem.GetFocusedRowCellValue(colTotalDay));
            projectItem.Note = TextUtils.ToString(tlProjectItem.GetFocusedRowCellValue(colNote));

            projectItem.TotalDayPlan = TextUtils.ToDecimal(tlProjectItem.GetFocusedRowCellValue(colTotalDay));
            //projectItem.TotalDay = 0;
            projectItem.PercentItem = TextUtils.ToDecimal(tlProjectItem.GetFocusedRowCellValue(colPercent));
            projectItem.ParentID = TextUtils.ToInt(tlProjectItem.GetFocusedRowCellValue(colParentID));

            if (projectItem.ID > 0)
            {
                ProjectItemBO.Instance.Insert(projectItem);
            }

           return (int)ProjectItemBO.Instance.Insert(projectItem);
        }

        private void btnAddParent_Click(object sender, EventArgs e)
        {
            DataRow dtrow = dtItem.NewRow();

            dtrow["ParentID"] = 0;
            dtrow["Status"] = 0;
            //dtrow["ID"] = 
            dtItem.Rows.Add(dtrow);

        }

        private void tlProjectItem_FontChanged(object sender, EventArgs e)
        {
        }

        private void tlProjectItem_DataSourceChanged(object sender, EventArgs e)
        {
            //tlProjectItem.setf
        }

        private void tlProjectItem_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }
            int userID = TextUtils.ToInt(tlProjectItem.GetRowCellValue(e.Node, colUserID));
            string dateEnd = TextUtils.ToString(tlProjectItem.GetRowCellValue(e.Node, colActualEndDate)).Trim();
            string mission = TextUtils.ToString(tlProjectItem.GetRowCellValue(e.Node, colMission)).Trim();
            string planDateStart = TextUtils.ToString(tlProjectItem.GetRowCellValue(e.Node, colPlanStartDate)).Trim();
            string planDateEnd = TextUtils.ToString(tlProjectItem.GetRowCellValue(e.Node, colPlanEndDate)).Trim();


            string actualEndDate = TextUtils.ToString(tlProjectItem.GetRowCellValue(e.Node, colActualEndDate));
            string planEndDate = TextUtils.ToString(tlProjectItem.GetRowCellValue(e.Node, colPlanEndDate));
            if (!DateTime.TryParse(actualEndDate, out DateTime actualDE) || !DateTime.TryParse(planEndDate, out DateTime planDE))
            {
                return;
            }

            var date = actualDE - planDE;
            int day = date.Days;

            if (!string.IsNullOrEmpty(planDateStart) && !string.IsNullOrEmpty(planDateEnd) && userID > 0 && !string.IsNullOrEmpty(mission))
            {
                if (string.IsNullOrEmpty(dateEnd) || day >= 1)
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                }
            }
        }
    }
}
