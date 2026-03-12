using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList.Nodes;
using Forms.Classes;
using Forms.DanhMuc.DuAn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace BMS
{
    public partial class frmProjectNew : _Forms
    {
        DataSet oDataSet;
        DataTable dtProjectTypeTreeFolder;
        public frmProjectNew()
        {
            InitializeComponent();
        }

        private void frmProject_Load(object sender, EventArgs e)
        {
            //oDataSet = TextUtils.LoadDataSetFromSP("spGetEmployeeForProject", new string[] { }, new object[] { });

            // ngày bắt đầu khi load form bằng ngày hiện tại trừ đi 1 tháng
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = new DateTime(dtpFromDate.Value.Year, 1, 1, 0, 0, 0).AddYears(-2);
            txtPageNumber.Text = "1";

            //loadFolder();
            //loadPM();
            //loadCustomer();
            //loadUserSale();
            //loadUserTech();
            //loadUserLeader();
            //loadProjectType();
            //loadProjectTypeLink();
            //loadProjectItem();
            //loadStatus();
            //loadBusinessField();
            //loadProject();
            //loadPermission();
            //int count = grvMaster.RowCount;
            //txtShowCount.Text = $"Show: {count}";

            //LinhTN update 18/06/2024
            LoadDepartment();
            LoadTeam();
            LoadUser();

            loadProject();
            //
        }

        private void loadBusinessField()
        {
            List<BusinessFieldModel> list = SQLHelper<BusinessFieldModel>.FindAll().OrderByDescending(x => x.STT).ToList();

            cboBusinessField.Properties.ValueMember = "ID";
            cboBusinessField.Properties.DisplayMember = "Name";
            cboBusinessField.Properties.DataSource = list;
        }

        #region Methods


        //Set permission thêm người tham gia
        bool CheckPermission()
        {
            int projectID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            int projectManager = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colPMID));
            int sale = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colUserIDSale));
            int technical = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colUserID));

            var exp1 = new Utils.Expression("ProjectID", projectID);
            var exp2 = new Utils.Expression("EmployeeID", Global.EmployeeID);
            var exp3 = new Utils.Expression("IsDeleted", 1, "<>");
            ProjectEmployeeModel projectEmployee = SQLHelper<ProjectEmployeeModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault();


            if (Global.EmployeeID == projectManager || Global.UserID == sale || Global.UserID == technical || Global.IsAdmin || (projectEmployee != null && projectEmployee.IsLeader))
            {
                //btnProjectEmployee.Enabled = btnProjectEmployees.Enabled = true;
                return true;
            }
            else
            {
                //btnProjectEmployee.Enabled = btnProjectEmployees.Enabled = false;
                return false;
            }
        }

        /// <summary>
        /// Load danh sách PM
        /// </summary>
        void loadPM()
        {
            DataTable dtPM = TextUtils.GetTable("spGetEmployeeForProject");
            cboPM.Properties.DataSource = dtPM;
            cboPM.Properties.DisplayMember = "FullName";
            cboPM.Properties.ValueMember = "EmployeeID";
        }

        /// <summary>
        /// load khách hàng
        /// </summary>
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT ID,CustomerCode, CustomerName FROM dbo.Customer where IsDeleted <> 1 Order By CreatedDate DESC");
            cbCustomer.Properties.DisplayMember = "CustomerCode";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }


        /// <summary>
        /// load ng phụ trách Sale lên cbUser
        /// </summary>
        void loadUserSale()
        {
            //DataTable dt = TextUtils.GetTable("spGetEmployeeForProject");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = oDataSet.Tables[1];
        }

        /// <summary>
        /// Load danh sách người phụ trách kỹ thuật lên cboUserTech
        /// </summary>
        void loadUserTech()
        {
            //DataTable dtUser = TextUtils.GetTable("spGetEmployeeForProject");
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = oDataSet.Tables[1];
        }

        /// <summary>
        /// Load danh sách leader lên cboLeader
        /// </summary>
        void loadUserLeader()
        {
            //DataTable dtUser = TextUtils.GetTable("spGetEmployeeForProject");
            cboLeader.Properties.DisplayMember = "FullName";
            cboLeader.Properties.ValueMember = "ID";
            cboLeader.Properties.DataSource = oDataSet.Tables[1];
        }


        /// <summary>
        /// load projecttype
        /// </summary>
        void loadProjectType()
        {
            DataTable dt = TextUtils.Select("SELECT ID,ProjectTypeName  FROM dbo.ProjectType WHERE ID <> 4");
            //  DataTable dt = TextUtils.Select("SELECT * FROM ProjectType p WHERE not (p.parentID = 0 AND p.id IN(SELECT ParentID FROM ProjectType) or p.id = 4)");

            //DataRow dtRow = dt.NewRow();
            //dtRow["ID"] = 0;
            //dtRow["ProjectTypeName"] = "Tất cả";  
            //dt.Rows.Add(dtRow);
            cboProjectType.Properties.DisplayMember = "ProjectTypeName";
            cboProjectType.Properties.ValueMember = "ID";
            cboProjectType.Properties.DataSource = dt;

            //cboProjectType.Properties.Items.Add(0);
        }


        /// <summary>
        /// Load kiểu dự án ở treelist
        /// </summary>
        void loadProjectTypeLink()
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectTypeLink", "A", new string[] { "@ProjectID" }, new object[] { id });
            tlProjectTypeMaster.DataSource = dt;
            tlProjectTypeMaster.ExpandAll();

        }

        /// <summary>
        /// Load Hạng mục công việc
        /// </summary>
        void loadProjectItem()
        {
            //  DataTable dt = TextUtils.Select("SELECT * FROM ProjectType p WHERE not (p.parentID = 0 AND p.id IN(SELECT ParentID FROM ProjectType) or p.id = 4)");
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (id <= 0) id--;
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectItem", "A", new string[] { "@ProjectID" }, new object[] { id });
            grdProjectItem.DataSource = dt;
            //tlProjectItem.DataSource = dt;
            //tlProjectItem.ExpandAll();
        }

        /// <summary>
        /// load project
        /// </summary>
        //void loadProject()
        //{
        //    DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
        //    DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

        //    string projectType = "";
        //    DataTable dt = (DataTable)cboProjectType.Properties.DataSource;

        //    List<int> listID = new List<int>();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        int id = TextUtils.ToInt(dt.Rows[i]["ID"]);
        //        listID.Add(id);
        //    }

        //    if (TextUtils.ToInt(cboProjectType.EditValue) == 0)
        //    {
        //        projectType = string.Join(",", listID);
        //    }
        //    else
        //    {
        //        projectType = TextUtils.ToString(cboProjectType.EditValue);
        //    }

        //    var employeePm = TextUtils.ToInt(cboPM.EditValue);

        //    DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetProject"
        //        , new string[] { "@PageSize", "@PageNumber", "@DateStart", "@DateEnd", "@FilterText", "@CustomerID", "@UserID", "@ListProjectType", "@LeaderID", "@UserIDTech", "@EmployeeIDPM" }
        //        , new object[] { TextUtils.ToInt(txtPageSize.Text), TextUtils.ToInt(txtPageNumber.Text), dateTimeS, dateTimeE, txtFilterText.Text.Trim()
        //                        ,TextUtils.ToInt(cbCustomer.EditValue),TextUtils.ToInt(cbUser.EditValue), projectType, TextUtils.ToInt(cboLeader.EditValue), TextUtils.ToInt(cboUserTech.EditValue),employeePm });
        //    grdMaster.DataSource = oDataSet.Tables[0];

        //    if (oDataSet.Tables.Count == 0) return;
        //    txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
        //    txtShowCount.Text = TextUtils.ToString(oDataSet.Tables[2].Rows[0]["TotalEntries"]) + " Entries";
        //}

        void loadProject()
        {
            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                DateTime dateStart = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
                DateTime dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

                //string projectType = "";
                //DataTable dt = TextUtils.Select("SELECT ID FROM dbo.ProjectType");
                //int[] typeCheck = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                //List<int> listID = new List<int>();

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    int id = TextUtils.ToInt(dt.Rows[i]["ID"]);
                //    listID.Add(id);
                //}
                //if (TextUtils.ToString(cboProjectType.EditValue) == "")
                //{
                //    projectType = string.Join(",", listID);
                //}
                //else
                //{
                //    projectType = TextUtils.ToString(cboProjectType.EditValue);
                //}
                //if (!string.IsNullOrEmpty(TextUtils.ToString(cboProjectType.Properties.GetCheckedItems())))
                //{
                //    string stringType = cboProjectType.Properties.GetCheckedItems().ToString();

                //    string[] checkedItems = stringType.Split(',');

                //    foreach (var item in checkedItems)
                //    {
                //        int index = listID.IndexOf(TextUtils.ToInt(item));
                //        if (index >= 0 && index < typeCheck.Length)
                //        {
                //            typeCheck[index] = 1;
                //        }
                //    }
                //}

                //var employeePm = TextUtils.ToInt(cboPM.EditValue);
                //var x = TextUtils.ToInt(cboLeader.EditValue);
                //int _bussinessFieldID = TextUtils.ToInt(cboBusinessField.EditValue);

                //DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetProjectNew"
                //    , new string[] { "@PageSize", "@PageNumber", "@DateStart", "@DateEnd", "@FilterText", "@CustomerID", "@UserID", "@ListProjectType", "@LeaderID", "@UserIDTech", "@EmployeeIDPM", "@1", "@2", "@3", "@4", "@5", "@6", "@7", "@8", "@9", "@UserIDPriotity", "@BusinessFieldID" }
                //    , new object[] { TextUtils.ToInt(txtPageSize.Text), TextUtils.ToInt(txtPageNumber.Text), dateTimeS, dateTimeE, txtFilterText.Text.Trim()
                //                ,TextUtils.ToInt(cbCustomer.EditValue),TextUtils.ToInt(cbUser.EditValue), projectType, TextUtils.ToInt(cboLeader.EditValue), TextUtils.ToInt(cboUserTech.EditValue),employeePm ,typeCheck[0] ,typeCheck[1] ,typeCheck[2] ,typeCheck[3] ,typeCheck[4] ,typeCheck[5] ,typeCheck[6] ,typeCheck[7] ,typeCheck[8],Global.UserID, _bussinessFieldID});
                //grdMaster.DataSource = oDataSet.Tables[0];

                //if (oDataSet.Tables.Count == 0) return;
                //txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
                //txtShowCount.Text = TextUtils.ToString(oDataSet.Tables[2].Rows[0]["TotalEntries"]) + " Entries";

                int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
                int userTeamID = TextUtils.ToInt(cboUserTeam.EditValue);
                int userID = TextUtils.ToInt(cboUser.EditValue);
                string projectTypeID = TextUtils.ToString(cboProjectType.EditValue);
                DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetProjectNew",
                                    new string[] { "@DateStart", "@DateEnd", "@DepartmentID", "@UserTeamID", "@UserID", "@ProjectTypeID", "@Keyword" },
                                    new object[] { dateStart, dateEnd, departmentID, userTeamID, userID, projectTypeID, txtFilterText.Text.Trim() });
                grdMaster.DataSource = dataSet.Tables[0];

                List<string> listStatus = new List<string>()
                {
                    "'Bàn giao/Nghiệm thu'"
                    ,"'Bảo trì'"
                    ,"'Hỗ trợ'"
                    ,"'Triển khai(đã PO)'"
                };

                //listStatus.Select(p => p.StatusName);

                string statusFilter = string.Join(",", listStatus);

                string filterString = $"([ProjectStatusName] In ({statusFilter}))";
                //string filterString = $"([ProjectStatus] In ('4'))";
                grvMaster.Columns["ProjectStatusName"].FilterInfo = new ColumnFilterInfo(filterString);

                loadProjectTypeLink();
                loadProjectItem();
            }
        }

        void loadProjectDetail()
        {
            if (grvMaster.RowCount <= 0) return;
            int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectDetail", "A", new string[] { "@ID" }, new object[] { IDMaster });
            grdData.DataSource = dt;
        }
        #endregion
        void loadFolder()
        {
            DataTable dt = TextUtils.Select($"Select * from ProjectTreeFolder");
            treeData.DataSource = dt;
            treeData.ExpandAll();
        }
        void loadStatus()
        {
            DataTable dt = TextUtils.Select("Select ID as ProjectStatusID, StatusName from ProjectStatus order by ID");
            cboProjectStatus.DataSource = dt;
            cboProjectStatus.DisplayMember = "StatusName";
            cboProjectStatus.ValueMember = "ProjectStatusID";
        }

        //LinhTN update 18/06/2024 start
        void LoadDepartment()
        {
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.SqlToList("SELECT * FROM Department");

            cboDepartment.Properties.DataSource = list;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";

            cboDepartment.EditValue = Global.DepartmentID;
        }
        void LoadTeam()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            List<UserTeamModel> list = SQLHelper<UserTeamModel>.FindByAttribute($"DepartmentID ", departmentID);

            cboUserTeam.Properties.DataSource = list;
            cboUserTeam.Properties.DisplayMember = "Name";
            cboUserTeam.Properties.ValueMember = "ID";

            UserTeamModel team = SQLHelper<UserTeamModel>.FindByAttribute("LeaderID", Global.EmployeeID).FirstOrDefault();
            if (team == null) return;
            cboUserTeam.EditValue = team.ID;
        }
        void LoadUser()
        {
            DataSet dtUser = TextUtils.LoadDataSetFromSP("spGetEmployeeForProject", new string[] { }, new object[] { }); ;

            cboUser.Properties.DataSource = dtUser.Tables[0];
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
        }
        //LinhTN update 18/06/2024 end

        #region Button Events
        /// <summary>
        /// click button thêm mới 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnNew_Click(object sender, EventArgs e)
        //{
        //    frmProjectDetail frm = new frmProjectDetail();
        //    if (frm.ShowDialog() == DialogResult.OK)
        //    {
        //        loadProject();
        //        grvMaster_FocusedRowChanged(null, null);
        //    }
        //}

        ///// <summary>
        ///// click button sửa
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnEdit_Click(object sender, EventArgs e)
        //{
        //    var focusedRowHandle = grvMaster.FocusedRowHandle;
        //    int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
        //    ProjectModel model = (ProjectModel)ProjectBO.Instance.FindByPK(ID);
        //    frmProjectDetail frm = new frmProjectDetail();
        //    frm.project = model;
        //    if (frm.ShowDialog() == DialogResult.OK)
        //    {
        //        loadProject();
        //        grvMaster.FocusedRowHandle = focusedRowHandle;
        //        grvMaster_FocusedRowChanged(null, null);
        //    }
        //}

        ///// <summary>
        ///// click button xóa
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    var focusedRowHandle = grvData.FocusedRowHandle;
        //    int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
        //    string groupFile = TextUtils.ToString(grvData.GetFocusedRowCellValue(colGroupFileCode));
        //    string projectCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectCode));
        //    if (MessageBox.Show(string.Format("Bạn có muốn xóa dự án [{0}] hay không ?", projectCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //    {
        //        ProjectBO.Instance.Delete(ID);
        //        grvMaster.DeleteSelectedRows();
        //        // xóa file trong ftp
        //        //for (int i = 0; i < grvData.RowCount; i++)
        //        //{
        //        //    string fileName = TextUtils.ToString(grvData.GetRowCellValue(i, colFileName));
        //        //    DocUtils.DeleteFile($"DuAn/{projectCode}/{groupFile}/{fileName}");
        //        //}

        //        grvData.FocusedRowHandle = focusedRowHandle;
        //        grvMaster_FocusedRowChanged(null, null);
        //    }
        //}

        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == true)
            {
                string projectCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectCode));
                MessageBox.Show(String.Format("Dự án có mã [{0}] đã được duyệt.", projectCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            approved(true);
        }

        private void btnCancelApprove_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == false)
            {
                string projectCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectCode));
                MessageBox.Show(String.Format("Dự án có mã [{0}] chưa được duyệt.", projectCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            approved(false);
        }
        //LinhTN update 18/06/2024 start
        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            LoadTeam();
        }
        private void cbTeam_EditValueChanged(object sender, EventArgs e)
        {

            int teamId = TextUtils.ToInt(cboUserTeam.EditValue);
            if (teamId == 0)
            {
                LoadUser();
            }
            else
            {
                List<EmployeeModel> list = SQLHelper<EmployeeModel>.ProcedureToList("spGetEmployeeByTeamID", new string[] { "@TeamID" }, new object[] { teamId });
                cboUser.Properties.DataSource = list;
                cboUser.Properties.DisplayMember = "FullName";
                cboUser.Properties.ValueMember = "UserID";
            }

            btnFind_Click(null, null);
        }
        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }
        //LinhTN update 18/06/2024 end
        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadProject();
            loadProjectItem();
            loadProjectTypeLink();
        }

        /// <summary>
        /// click button nhóm file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroupFile_Click(object sender, EventArgs e)
        {
            frmGroupFile frm = new frmGroupFile();
            frm.ShowDialog();
        }

        /// <summary>
        /// click button download file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownloadFile_Click(object sender, EventArgs e)
        {
            string fileName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFileName));
            if (MessageBox.Show(string.Format("Bạn có chắc muốn downnload file [{0}] này không?", fileName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string path = TextUtils.ToString(grvData.GetFocusedRowCellValue(colPathFull));
                string[] folders = path.Split('\\');
                string newPath = folders[folders.Length - 4] + "/" + folders[folders.Length - 3] + "/" + folders[folders.Length - 2] + "/" + folders[folders.Length - 1];
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                string PartLocal = "";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    PartLocal = fbd.SelectedPath;
                }
                else
                {
                    return;
                }

                // download file từ ftp -> máy
                DocUtils.DownloadFile(PartLocal, folders[folders.Length - 1], newPath);

                MessageBox.Show(string.Format("Bạn đã downnload file [{0}] thành công!", fileName));

                // hiển thị thư mục download
                Process.Start(PartLocal);

            }
        }

        /// <summary>
        /// click button download theo nhóm file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownloadGroupFile_Click(object sender, EventArgs e)
        {
            string groupFileCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colGroupFileCode));
            string groupFileName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colGroupFileName));
            if (MessageBox.Show(string.Format("Bạn có chắc muốn downnload nhóm file [{0}] : [{1}] này không?", groupFileCode, groupFileName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string path = TextUtils.ToString(grvData.GetFocusedRowCellValue(colPathFull));
                string[] folders = path.Split('\\');
                string newPath = folders[folders.Length - 4] + "/" + folders[folders.Length - 3] + "/" + folders[folders.Length - 2];

                // chọn thư mục cần tải về
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                string PartLocal = "";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    PartLocal = fbd.SelectedPath;
                }
                else
                {
                    return;
                }

                // download thư mục từ ftp về máy
                DocUtils.DownloadFile(newPath, PartLocal);

                MessageBox.Show(string.Format("Bạn đã downnload nhóm file [{0}] : [{1}] thành không!", groupFileCode, groupFileName));

                // hiển thị thư mục download
                Process.Start(PartLocal);
            }
        }

        /// <summary>
        /// Click button download theo dự án
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownloadProject_Click(object sender, EventArgs e)
        {
            string PartLocal = "";
            string projectCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectCode));
            if (MessageBox.Show(string.Format("Bạn có chắc muốn downnload thư mục [{0}] này không?", projectCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // chọn thư mục cần tải về
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    PartLocal = fbd.SelectedPath;
                }
                else
                {
                    return;
                }
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    //string[] pathShort1 = TextUtils.ToString(grvData.GetRowCellValue(i, colPathShort)).Split('\\');
                    //string[] pathShort2 = TextUtils.ToString(grvData.GetRowCellValue(i + 1, colPathShort)).Split('\\');
                    //if (pathShort1[0] != pathShort2[0])
                    //{
                    string path = TextUtils.ToString(grvData.GetRowCellValue(i, colPathFull));
                    string[] folders = path.Split('\\');
                    string newPath = folders[folders.Length - 4] + "/" + folders[folders.Length - 3] + "/" + folders[folders.Length - 2];

                    // tạo thư mục PartLocal: thư mục lựa chọn 
                    if (!Directory.Exists(PartLocal + Path.DirectorySeparatorChar + folders[folders.Length - 3]))
                    {
                        Directory.CreateDirectory(PartLocal + Path.DirectorySeparatorChar + folders[folders.Length - 3]);
                    }
                    // download thư mục từ ftp về máy
                    DocUtils.DownloadFile(newPath, PartLocal + Path.DirectorySeparatorChar + folders[folders.Length - 3]);
                    //}
                }

                MessageBox.Show(string.Format("Bạn đã downnload thư mục [{0}] thành không!", projectCode));

                // hiển thị thư mục được download
                Process.Start(PartLocal);
            }
        }

        /// <summary>
        /// click button xuất excel  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvMaster.OptionsPrint.AutoWidth = false;
                grvMaster.OptionsPrint.ExpandAllDetails = false;
                grvMaster.OptionsPrint.PrintDetails = true;
                grvMaster.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvMaster.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// click button xuất excel theo từng dự án
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportExcelProject_Click(object sender, EventArgs e)
        {
            int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (IDMaster == 0) return;

            string path = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            else
            {
                return;
            }
            string fileSourceName = "ProjectExcel.xlsx";

            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string projectCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectCode));
            string currentPath = path + "\\" + projectCode + ".xlsx";
            try
            {
                File.Copy(sourcePath, currentPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tạo phiếu!" + Environment.NewLine + ex.Message,
                    TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DataTable dtDetail = TextUtils.LoadDataFromSP("spGetProjectDetail", "A", new string[] { "@ID" }, new object[] { IDMaster });

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Excel.Application app = default(Excel.Application);
                Excel.Workbook workBoook = default(Excel.Workbook);
                Excel.Worksheet workSheet = default(Excel.Worksheet);
                try
                {
                    app = new Excel.Application();
                    app.Workbooks.Open(currentPath);
                    workBoook = app.Workbooks[1];
                    workSheet = (Excel.Worksheet)workBoook.Worksheets[1];

                    DateTime dtime = TextUtils.ToDate3(dtDetail.Rows[0]["CreatedDate"]);
                    string date = $"{dtime.Day}/{dtime.Month}/{dtime.Year}";
                    workSheet.Cells[2, 6] = projectCode;
                    workSheet.Cells[5, 1] = TextUtils.ToString(dtDetail.Rows[0]["ProjectStatusText"]);
                    workSheet.Cells[5, 2] = TextUtils.ToString(dtDetail.Rows[0]["ProjectCode"]);
                    workSheet.Cells[5, 3] = TextUtils.ToString(dtDetail.Rows[0]["ProjectName"]);
                    workSheet.Cells[5, 4] = TextUtils.ToString(dtDetail.Rows[0]["PO"]);
                    workSheet.Cells[5, 5] = TextUtils.ToString(dtDetail.Rows[0]["FullName"]);
                    workSheet.Cells[5, 6] = TextUtils.ToString(dtDetail.Rows[0]["CustomerName"]);
                    workSheet.Cells[5, 7] = TextUtils.ToString(dtDetail.Rows[0]["ContactName"]);
                    workSheet.Cells[5, 8] = TextUtils.ToString(dtDetail.Rows[0]["ContactPhone"]);
                    workSheet.Cells[5, 9] = TextUtils.ToString(dtDetail.Rows[0]["ContactEmail"]);
                    workSheet.Cells[5, 10] = TextUtils.ToString(dtDetail.Rows[0]["Note"]);
                    workSheet.Cells[5, 11] = date;

                    for (int i = dtDetail.Rows.Count - 1; i >= 0; i--)
                    {
                        workSheet.Cells[9, 1] = i + 1;
                        workSheet.Cells[9, 2] = TextUtils.ToString(dtDetail.Rows[i]["GroupFileName"]);
                        workSheet.Cells[9, 3] = TextUtils.ToString(dtDetail.Rows[i]["FileName"]);
                        workSheet.Cells[9, 4] = TextUtils.ToString(dtDetail.Rows[i]["PathShort"]);
                        ((Excel.Range)workSheet.Rows[9]).Insert();
                    }
                    ((Excel.Range)workSheet.Rows[8]).Delete();
                    ((Excel.Range)workSheet.Rows[8]).Delete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (app != null)
                    {
                        app.ActiveWorkbook.Save();
                        app.Workbooks.Close();
                        app.Quit();
                    }
                }
                Process.Start(currentPath);
            }
        }
        #endregion

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadProject();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadProject();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadProject();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadProject();
        }

        private void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "")
                return;
            else
            {
                txtPageNumber.Text = "1";
                loadProject();
            }
        }

        private void txtPageSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //private void grdMaster_DoubleClick(object sender, EventArgs e)
        //{
        //    btnEdit_Click(null, null);
        //}

        /// <summary>
        /// hàm duyệt
        /// </summary>
        /// <param name="isApproved"></param>
        void approved(bool isApproved)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt phiếu này?", isApproved ? "" : "bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int iD = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                string sql = string.Format(@"UPDATE dbo.Project SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, iD);
                TextUtils.ExcuteSQL(sql);
                if (isApproved == true)
                    grvMaster.SetFocusedRowCellValue(colIsApproved, 1);
                else
                    grvMaster.SetFocusedRowCellValue(colIsApproved, 0);
            }
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //loadProjectDetail();
            loadProjectTypeLink();
            loadProjectItem();
            //loadPermission();

            //cGlobVar.ProjectID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            //frmGanttChartProjectItemGrid frm = new frmGanttChartProjectItemGrid();
            //frm.projectID = projectId;

            //LinhTN update 01/08/2024
            loadProjectCurrentSituation();
        }

        private void btnProjectCost_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            ProjectModel model = (ProjectModel)ProjectBO.Instance.FindByPK(ID);
            frmProjectCost frm = new frmProjectCost();
            frm.project = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    btnFind_Click(null, null);
            //}
        }

        private void btnSettingTree_Click(object sender, EventArgs e)
        {
            oPENFILELOCATIONToolStripMenuItem_Click(null, null);
            return;
            frmSettingFolder frm = new frmSettingFolder();
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void treeData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string[] arrSv = File.ReadAllLines(Path.Combine(Application.StartupPath, "ftpServerMain.txt"));
                if (arrSv.Length < 3) MessageBox.Show("Lỗi file ftpServerMain.txt. Hãy kiểm tra lại! ");
                string ftpServerPath = arrSv[0];
                DataTable dt = (DataTable)treeData.DataSource;
                string path;
                string customer = TextUtils.ToString(TextUtils.ExcuteScalar($"Select CustomerShortName from Customer where CustomerName =N'{TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCustomerID))}'"));
                string projectcode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectCode));
                string projectname = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectName));
                string fodel = TextUtils.ToString(treeData.GetFocusedRowCellValue(colFolderName));
                int parentID = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colParentID));
                if (parentID > 0)
                {
                    DataRow[] dtr = dt.Select($"ID = {parentID}");
                    path = string.Format($@"{ftpServerPath}:/Projects/{ DateTime.Now.Year}/{ customer}/{ projectcode}-{ projectname}/{dtr[0]["FolderName"]}/{fodel}");
                }
                else
                    path = string.Format($@"{ftpServerPath}:/Projects/{ DateTime.Now.Year}/{ customer}/{ projectcode}-{ projectname}/{fodel}");

                Process.Start(path);
            }
            catch
            {
                MessageBox.Show("Không tìm thấy thư mục", "Thông báo");
            }
        }


        //string pathLocation = @"\\192.168.1.190\duan\Projects\"; //Thư mục trên server

        ////string pathLocation = @"D:\LeTheAnh\RTC\Project\"; //Thư mục test local

        private void CreateDirectoryWithDatatable(DataRow row, string path)
        {
            //Trước tiên kiểm tra nó có phải cấp đầu tiên hay ko
            int id = TextUtils.ToInt(row["ID"]);
            int parentId = TextUtils.ToInt(row["ParentID"]);
            string folderName = TextUtils.ToString(row["FolderName"]);
            if (parentId <= 0)
            {
                // Nó là 1 cha
                string newPath = Path.Combine(path, folderName);
                if (!Directory.Exists(newPath))
                    Directory.CreateDirectory(newPath);

                row["Path"] = newPath;
                // Lấy toàn bộ các con của nó
                DataRow[] childFolder = row.Table.Select($"ParentID={id}");
                foreach (DataRow childRow in childFolder)
                    CreateDirectoryWithDatatable(childRow, path);
            }
            else
            {
                // Nếu nó là 1 con
                //DataRow[] parentFolder = row.Table.Select($"ID={parentId}");
                //if (parentFolder.Length > 0 && string.IsNullOrEmpty(TextUtils.ToString(parentFolder[0]["Path"])))
                //    CreateDirectoryWithDatatable(parentFolder[0], path);
                //else
                //{
                //    string newPath = Path.Combine(TextUtils.ToString(parentFolder[0]["Path"]), folderName);
                //    if (!Directory.Exists(newPath))
                //        Directory.CreateDirectory(newPath);
                //    row["Path"] = newPath;
                //    // Lấy toàn bộ các con của nó
                //    DataRow[] childFolder = row.Table.Select($"ParentID={id}");
                //    foreach (DataRow childRow in childFolder)
                //        CreateDirectoryWithDatatable(childRow, path);
                //}

                ///-------------------------------------------- phuc add new
                int gp1ID = TextUtils.ToInt(row["GP1ID"]); // lấy ID để get dữ liệu của các GP 
                DataRow[] parentFolder;
                // nếu nó là các SP thì > 0
                if (gp1ID > 0)
                {
                    parentFolder = dtProjectTypeTreeFolder.Select($"ID={parentId}");
                }
                else
                {
                    parentFolder = row.Table.Select($"ID={parentId}");
                }

                if (parentFolder.Length > 0 && string.IsNullOrEmpty(TextUtils.ToString(parentFolder[0]["Path"])))
                {
                    CreateDirectoryWithDatatable(parentFolder[0], path);
                }
                else
                {
                    string newPath = Path.Combine(TextUtils.ToString(parentFolder[0]["Path"]), folderName);
                    if (!Directory.Exists(newPath))
                        Directory.CreateDirectory(newPath);
                    row["Path"] = newPath;
                    // nếu nó là SP động
                    if (gp1ID > 0)
                    {
                        // Lấy toàn bộ các con của nó
                        DataRow[] childFolder = dtProjectTypeTreeFolder.Select($"ParentID={gp1ID}");
                        foreach (DataRow childRow in childFolder)
                        {
                            int GP1IDnew = TextUtils.ToInt(childRow["ID"]);

                            DataRow dtNewRow = dtProjectTypeTreeFolder.NewRow();
                            dtNewRow["ID"] = TextUtils.ToInt(idMaxData());
                            dtNewRow["ParentID"] = id;
                            dtNewRow["FolderName"] = TextUtils.ToString(childRow["FolderName"]);
                            dtNewRow["Path"] = TextUtils.ToString(childRow["Path"]);
                            dtNewRow["GP1ID"] = GP1IDnew;
                            dtProjectTypeTreeFolder.Rows.Add(dtNewRow);

                            CreateDirectoryWithDatatable(dtNewRow, path);
                        }
                    }
                    else
                    {
                        // Lấy toàn bộ các con của nó
                        DataRow[] childFolder = row.Table.Select($"ParentID={id}");
                        foreach (DataRow childRow in childFolder)
                            CreateDirectoryWithDatatable(childRow, path);
                    }
                }
                //-----------------------------------end
            }
        }
        private void CreateDirectoryWithDatatable(DataTable dataTable, string path)
        {
            foreach (DataRow row in dataTable.Rows)
                CreateDirectoryWithDatatable(row, path);
        }

        #region PhucLH update 28/05/2024
        private void addGPProjectTypeTreeFolder(DataTable dt)
        {
            DataTable dtNew = dt.Clone();
            // kiểm tra nếu có các SP1.. là con của folder THIETKE.Co
            bool parentIdExists = dt.AsEnumerable().Any(row => TextUtils.ToInt(row["ParentID"]) == 3);
            if (parentIdExists)
            {
                // lấy danh sách yêu cầu giải pháp GP...
                List<string> lsSp = new List<string>();
                int idMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                DataTable dtProjectRequest = TextUtils.LoadDataFromSP("spGetProjectRequest", "A", new string[] { "@ProjectID" }, new object[] { idMaster });
                if (dtProjectRequest.Rows.Count > 0)
                {
                    foreach (DataRow rowProjectRequest in dtProjectRequest.Rows)
                    {
                        int idProjectRequest = TextUtils.ToInt(rowProjectRequest["ID"]);
                        DataTable dtProjectSolution = TextUtils.LoadDataFromSP("spGetProjectSolution", "B", new string[] { "@ProjectRequestID" }, new object[] { idProjectRequest });
                        if (dtProjectSolution.Rows.Count > 0)
                        {
                            foreach (DataRow rowProjectSolution in dtProjectSolution.Rows)
                            {
                                string codeSolution = TextUtils.ToString(rowProjectSolution["CodeSolution"]).Trim();
                                if (!string.IsNullOrEmpty(codeSolution) && !lsSp.Contains(codeSolution))
                                {
                                    // Thêm mã giải pháp vào danh sách nếu chưa tồn tại
                                    lsSp.Add(codeSolution);

                                    DataRow newRow1 = dtNew.NewRow();
                                    newRow1["ID"] = TextUtils.ToInt(idMaxData());
                                    newRow1["ParentID"] = 3; // ParentID của GP1 trong mẫu
                                    newRow1["FolderName"] = codeSolution;
                                    newRow1["GP1ID"] = 40; // id của GP1 trong mẫu
                                    dtNew.Rows.Add(newRow1);

                                    DataRow newRow2 = dtProjectTypeTreeFolder.NewRow();
                                    newRow2["ID"] = newRow1["ID"];
                                    newRow2["ParentID"] = newRow1["ParentID"];
                                    newRow2["FolderName"] = newRow1["FolderName"];
                                    newRow2["GP1ID"] = newRow1["GP1ID"];
                                    dtProjectTypeTreeFolder.Rows.Add(newRow2);
                                }
                                //
                            }
                        }
                    }
                }

                //Check xem nếu data mới có GP1, GP2, GP3 hay không nếu không thì đổi tên
                //List<string> folderNamesToCheck = new List<string> { "GP1", "GP2", "GP3" };
                //foreach (string folderName in folderNamesToCheck)
                //{
                //    DataRow spRow = dt.AsEnumerable().FirstOrDefault(row => row.Field<string>("FolderName") == folderName);
                //    if (spRow != null)
                //    {
                //        if (!dtNew.AsEnumerable().Any(x => x.Field<string>("FolderName") == folderName))
                //        {
                //            if (dtNew.Rows.Count > 0)
                //            {
                //                for (int j = 0; j < dtNew.Rows.Count; j++)
                //                {
                //                    string dtNewFolderName = TextUtils.ToString(dtNew.Rows[j]["FolderName"]);
                //                    if (!dt.AsEnumerable().Any(x => x.Field<string>("FolderName") == dtNewFolderName))
                //                    {
                //                        spRow["FolderName"] = dtNewFolderName;
                //                        break;
                //                    }
                //                }
                //                if (dt.AsEnumerable().Any(x => x.Field<string>("FolderName") == folderName))
                //                {

                //                    spRow["GP1ID"] = 5;
                //                }
                //            }
                //            else
                //            {
                //                spRow["GP1ID"] = 5;
                //            }
                //        }
                //    }

                //}

                // add dữ liệu vào dt và check không được trùng
                foreach (DataRow itemRow in dtNew.Rows)
                {
                    string folderName = TextUtils.ToString(itemRow["FolderName"]);
                    if (!dt.AsEnumerable().Any(x => x.Field<string>("FolderName") == folderName))
                    {
                        DataRow rowNew = dt.NewRow();
                        rowNew["ID"] = itemRow["ID"];
                        rowNew["ParentID"] = itemRow["ParentID"];
                        rowNew["FolderName"] = folderName;
                        rowNew["GP1ID"] = itemRow["GP1ID"];
                        dt.Rows.Add(rowNew);
                    }
                }
                dtProjectTypeTreeFolder.Clear();
            }
            dtProjectTypeTreeFolder = dt.Copy();
        }

        private int idMaxData()
        {
            if (dtProjectTypeTreeFolder.Rows.Count == 0)
                return 1;

            int maxId = dtProjectTypeTreeFolder.AsEnumerable().Max(row => TextUtils.ToInt(row["ID"]));
            return maxId + 1;
        }
        #endregion

        private void oPENFILELOCATIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var dateCreated = grvMaster.GetFocusedRowCellValue(colCreatedDate);
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectCode));
                int year = TextUtils.ToDate5(grvMaster.GetFocusedRowCellValue(colCreatedDate)).Year;
                //int year = TextUtils.ToDate3(grvMaster.GetFocusedRowCellValue(colCreatedDate)).Year;
                //string path = $@"{pathLocation}\{DateTime.Now.Year}\Projects\{code}";
                //Ping ping = new Ping();
                //PingReply reply = ping.Send("192.168.1.190");

                string pathLocation = Global.PathLocationProject;
                if (Global.IsOnline)
                {
                    //pathLocation = @"\\rtctechnologydata.ddns.net\DUAN\Projects\";
                    pathLocation = @"\\113.190.234.64\DUAN\Projects\";
                }
                //pathLocation = @"\\rtctechnologydata.ddns.net\DUAN\Projects\";

                string path = $@"{pathLocation}\{year}\{code}";


                List<int> listProjectTypeID = new List<int>();
                List<string> listProjectTypeName = new List<string>();

                var nodeSelected = tlProjectTypeMaster.GetNodeList().Where(x => TextUtils.ToBoolean(x.GetValue(colSeleted)) == true);

                foreach (TreeListNode node in nodeSelected)
                {
                    int projectTypeID;
                    string projectTypeName = "";
                    if (TextUtils.ToBoolean(node.GetValue(colSeleted)))
                    {
                        projectTypeID = TextUtils.ToInt(node.GetValue(colProjectTypeID));
                        projectTypeName = TextUtils.ToString(node.GetValue(colProjectTypeMaster));

                        listProjectTypeID.Add(projectTypeID);
                        listProjectTypeName.Add(projectTypeName);
                    }

                }

                if (listProjectTypeID.Count > 0)
                {
                    for (int i = 0; i < listProjectTypeID.Count; i++)
                    {

                        DataTable dt = TextUtils.LoadDataFromSP("sp_GetProjectTypeTreeFolder", "A", new string[] { "@ProjectTypeID" }, new object[] { listProjectTypeID[i] });
                        if (dt.Rows.Count <= 0)
                            continue;
                        dt.Columns.Add("Path", typeof(string));

                        //---------------------------------------------------------------- phuc add new
                        dt.Columns.Add("GP1ID", typeof(int));
                        dtProjectTypeTreeFolder = dt.Copy();
                        addGPProjectTypeTreeFolder(dt);
                        //------------------------------------------------------------------------end

                        CreateDirectoryWithDatatable(dt, path);

                        ////string parentfolder = TextUtils.ToString(dt.Rows[0]["FolderName"]);


                        //string subPath = "";
                        //for (int j = 0; j < dt.Rows.Count; j++)
                        //{
                        //    var dataRow1 = dt.Select("ParentID = 0");
                        //    var dataRow2 = dt.Select("ParentID <> 0");
                        //    string parentfolder = dataRow1[0]["FolderName"].ToString();
                        //    //string subFolder = dt.Rows[i]["FolderName"].ToString();

                        //    //subPath += parentfolder + "\\" + subFolder;

                        //    //string pathCreate = Path.Combine(path, parentfolder, subFolder);

                        //    Directory.CreateDirectory($@"{path}\{parentfolder}");
                        //    for (int k = 0; k < dataRow2.Length; k++)
                        //    {
                        //        string childfolder = dataRow2[k]["FolderName"].ToString();
                        //        Directory.CreateDirectory($@"{path}\{parentfolder}\{childfolder}");
                        //    }
                        //}
                    }
                }

                try
                {
                    //Danh sách thư mục con của TaiLieuChung
                    List<ProjectTreeFolderModel> list = SQLHelper<ProjectTreeFolderModel>.FindByAttribute("ParentID", 20);
                    Directory.CreateDirectory($@"{path}\DanhMucVatTu");
                    //Directory.CreateDirectory($@"{path}\TaiLieuChung");

                    foreach (var item in list)
                    {
                        Directory.CreateDirectory($@"{path}\TaiLieuChung\{item.FolderName}");
                    }

                    //Directory.CreateDirectory($@"{path}\TaiLieuChung\HuongDanVanHanh");
                    //Directory.CreateDirectory($@"{path}\TaiLieuChung\GiaiPhap");
                    //Directory.CreateDirectory($@"{path}\TaiLieuChung\YeuCauDuAn(REV02)");
                    //Directory.CreateDirectory($@"{path}\TaiLieuChung\CatalogManual");

                    //if (!Directory.Exists($@"{path}\01. Report"))
                    //    Directory.CreateDirectory($@"{path}\01. Report");
                    //if (!Directory.Exists($@"{path}\02. Image"))
                    //    Directory.CreateDirectory($@"{path}\02. Image");
                    //if (!Directory.Exists($@"{path}\03. Program"))
                    //    Directory.CreateDirectory($@"{path}\03. Program");
                    //if (!Directory.Exists($@"{path}\04. Partlist"))
                    //    Directory.CreateDirectory($@"{path}\04. Partlist");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Process.Start(path);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Chưa đăng nhập tài khoản server");
                MessageBox.Show(ex.Message);
            }
        }

        private void btnProjectType_Click(object sender, EventArgs e)
        {
            frmProjectTypeDetail frm = new frmProjectTypeDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectType();
            }
        }

        private void frmProject_SizeChanged(object sender, EventArgs e)
        {
            //int nodeSize = tlProjectTypeMaster.AllNodesCount * 20 + 50;
            //if (nodeSize>= splitContainer1.Height)
            //{
            //    return;
            //}
            //splitContainer1.SplitterDistance = splitContainer1.Height - nodeSize;

            //splitContainer1.Panel2.Height = nodeSize + 30;
            //splitContainer1.SplitterDistance = splitContainer1.Height - nodeSize - 30;

            int width = splitContainerControl1.Width;
            splitContainerControl1.Panel1.MinSize = 350;
            splitContainerControl1.Panel2.MinSize = width - 300;
        }

        private void bntProjectItem_Click(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)tlProjectTypeMaster.DataSource;

            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (ID <= 0)
            {
                return;
            }
            ProjectModel model = (ProjectModel)ProjectBO.Instance.FindByPK(ID);
            frmHangMucCongViec frm = new frmHangMucCongViec();

            //frm.projectID = ID;
            frm.project = model;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
                loadProjectItem();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void grvProjectItem_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colStatus)
            {
                if ((int)e.Value == 0)
                {
                    e.DisplayText = "Chưa làm";
                }
                else if ((int)e.Value == 1)
                {
                    e.DisplayText = "Đang làm";
                }
                else if ((int)e.Value == 2)
                {
                    e.DisplayText = "Đã hoàn thành";
                }
                else
                {
                    e.DisplayText = "Pending";
                }
            }
        }

        private void tlProjectTypeMaster_CustomDrawRow(object sender, DevExpress.XtraTreeList.CustomDrawRowEventArgs e)
        {
            //foreach (TreeListNode node in tlProjectTypeMaster.GetNodeList())
            //{
            //    bool select = TextUtils.ToBoolean(tlProjectTypeMaster.GetRowCellValue(node, colSeleted));
            //    if (TextUtils.ToBoolean(tlProjectTypeMaster.GetRowCellValue(node,colSeleted)) == true)
            //    {
            //        e.Appearance.BackColor = Color.Yellow;
            //    }
            //}
        }

        private void grvProjectItem_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                //string dateEnd = TextUtils.ToString(grvProjectItem.GetRowCellValue(e.RowHandle, colActualEndDate)).Trim();

                //DateTime actualDE = TextUtils.ToDate3(grvProjectItem.GetRowCellValue(e.RowHandle, colActualEndDate));
                //DateTime planDE = TextUtils.ToDate3(grvProjectItem.GetRowCellValue(e.RowHandle, colPlanEndDate));

                //var date = actualDE - planDE;
                //int day = date.Days;

                //if (string.IsNullOrEmpty(dateEnd) || day >= 1)
                //{
                //    e.Appearance.BackColor = Color.LightSalmon;
                //    //e.Appearance.BackColor2 = Color.Red;

                //    //e.Appearance.ForeColor = Color.White;
                //    e.HighPriority = true;
                //}

                int itemLate = TextUtils.ToInt(grvProjectItem.GetRowCellValue(e.RowHandle, "ItemLateActual"));
                if (itemLate == 1)
                {
                    e.Appearance.BackColor = Color.Orange;
                    e.HighPriority = true;
                }

                if (itemLate == 2)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                    e.HighPriority = true;
                }
            }
        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnChangeProjectReport_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            int rowHandle = grvMaster.FocusedRowHandle;
            frmChangeProjectReport frm = new frmChangeProjectReport();
            frm.projectIdOld = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvData.FocusedRowHandle = rowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }
        private void grvMaster_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    loadProjectItem();
            //    loadProjectTypeLink();
            //}

        }

        private void btnProjectAll_Click(object sender, EventArgs e)
        {
            int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (IDMaster <= 0) return;
            frmProjectAll frm = new frmProjectAll();
            frm.ID = IDMaster;
            frm.ShowDialog();
        }

        private void grvMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvMaster.GetFocusedRowCellValue(grvMaster.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            //Clipboard.SetText(TextUtils.ToString(grvMaster.GetFocusedRowCellValue(grvMaster.FocusedColumn)));
            btnPersonalPriotity_Click(null, null);
        }

        private void cboPM_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnProjectPartList_Click(object sender, EventArgs e)
        {
            //string projectCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectCode));
            //string projectName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectName));
            int projectID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));

            setJoinerIDs(projectID);

            if (projectID <= 0)
            {
                MessageBox.Show("Dự án chưa được chọn.\nVui lòng chọn dự án trước!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            //frmProjectPartList frm = new frmProjectPartList();
            ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectID);
            frmProjectPartList_New frm = new frmProjectPartList_New(false);
            //frm.ProjectID = projectID;
            //frm.projectCode = projectCode;
            //frm.projectName = projectName;
            frm.project = project;

            //frm.Text = "TIẾN ĐỘ VẬT TƯ DỰ ÁN: "+ projectCode + " - " + projectName;
            //frm.Text = $"TIẾN ĐỘ VẬT TƯ DỰ ÁN: {projectCode} - {projectName}";
            frm.ShowDialog();

        }

        private void setJoinerIDs(int projectID)
        {
            clearJoinerIDs();

            ProjectModel project = (ProjectModel)ProjectBO.Instance.FindByPK(projectID);
            if (Global.IsAdmin)
                ProjectPartListJoiner.UpdaterID = Global.UserID;
            else
                ProjectPartListJoiner.UpdaterID = Global.EmployeeID;

            ProjectPartListJoiner.SaleID = project.UserID;
            ProjectPartListJoiner.UserTechnicalID = project.UserTechnicalID;

            int pmID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colPMID));
            if (pmID > 0)
            {
                ProjectPartListJoiner.ProjectManagerID = pmID;
            }

            foreach (TreeListNode item in tlProjectTypeMaster.NodesIterator.All)
            {
                int v = TextUtils.ToInt(item[colLeaderID]);
                if (v > 0)
                {
                    ProjectPartListJoiner.LeaderIDs.Add(v);
                }
            }

            for (int i = 0; i < grvProjectItem.DataRowCount; i++)
            {
                int v = TextUtils.ToInt(grvProjectItem.GetRowCellValue(i, colChargerID));
                if (v > 0)
                    ProjectPartListJoiner.ChargerIDs.Add(TextUtils.ToInt(v));
            }
        }

        private void clearJoinerIDs()
        {
            ProjectPartListJoiner.UpdaterID = 0;
            ProjectPartListJoiner.SaleID = 0;
            ProjectPartListJoiner.UserTechnicalID = 0;
            ProjectPartListJoiner.LeaderIDs = new List<int>();
            ProjectPartListJoiner.ChargerIDs = new List<int>();
        }

        private void btnGranttChartProject_Click(object sender, EventArgs e)
        {
            frmGanttChartProjectItemGrid frm = new frmGanttChartProjectItemGrid();
            frm.projectID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            frm.createdDate = TextUtils.ToDate5(grvMaster.GetFocusedRowCellValue(colCreatedDate));

            frm.Show();

        }

        private void btnPersonalPriotity_Click(object sender, EventArgs e)
        {
            frmProjectPersonalPriotity frm = new frmProjectPersonalPriotity();
            //int[] rowIndex = grvMaster.GetSelectedRows();
            //for (int i = 0; i < rowIndex.Length; i++)
            //{
            //    listProjectID.Add(TextUtils.ToInt(grvMaster.GetRowCellValue(rowIndex[i], colIDMaster)));
            //}

            frm.ProjectID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
            }
        }

        private void grvMaster_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.HighPriority = true;
            }
            //if (e.RowHandle >= 0)
            //{
            //    e.Appearance.BackColor = Color.White;
            //    e.Appearance.ForeColor = Color.Black;

            //    int itemLate = 0;

            //    DateTime actualDE = TextUtils.ToDate5(grvData.GetRowCellValue(e.RowHandle, colActualDE));
            //    DateTime actualDS = TextUtils.ToDate5(grvData.GetRowCellValue(e.RowHandle, colActualDS));
            //    DateTime planDE = TextUtils.ToDate5(grvData.GetRowCellValue(e.RowHandle, colPlanDE));

            //    string strActualDS = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colActualDS)).Trim();
            //    string strActualDE = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colActualDE)).Trim();
            //    string strPlanDE = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colPlanDE)).Trim();

            //    //Nếu đã có ngày bắt đầu thực tế và chưa có ngày kết thúc thực tế
            //    //Nếu ngày bắt đầu thực tế > ngày kết thúc dự kiến --> Failed

            //    if (!string.IsNullOrEmpty(strActualDS) && string.IsNullOrEmpty(strActualDE))
            //    {
            //        if ((actualDS - planDE).TotalDays > 0)
            //        {
            //            itemLate = 2; //Failed (Màu đỏ)
            //        }
            //    }

            //    //Nếu đã có ngày bắt đầu thực tế và ngày kết thúc thực tế
            //    //Nếu ngày kết thúc thực tế > ngày kết thúc dự kiến --> Chậm
            //    if (!string.IsNullOrEmpty(strActualDS) && !string.IsNullOrEmpty(strActualDE))
            //    {
            //        if ((actualDE - planDE).TotalDays > 0)
            //        {
            //            itemLate = 1; //Chậm (Màu cam)
            //        }
            //    }

            //    //Nếu chưa có ngày bắt đầu thực tế và ngày kết thúc thực tế
            //    //Nếu ngày hiện tại > ngày kết thúc dự kiến --> Failed

            //    if (string.IsNullOrEmpty(strActualDS) && string.IsNullOrEmpty(strActualDE))
            //    {
            //        if (!string.IsNullOrEmpty(strPlanDE))
            //        {
            //            if ((DateTime.Now - planDE).TotalDays > 0)
            //            {
            //                itemLate = 2; //Failed (Màu đỏ)
            //            }
            //        }

            //    }

            //    if (itemLate == 1)
            //    {
            //        //e.Appearance.BackColor = Color.FromArgb(255, 205, 210);
            //        e.Appearance.BackColor = Color.Orange;
            //        //e.Appearance.ForeColor = Color.White;
            //        e.HighPriority = true;
            //    }

            //    if (itemLate == 2)
            //    {
            //        //e.Appearance.BackColor = Color.FromArgb(255, 205, 210);
            //        e.Appearance.BackColor = Color.Red;
            //        e.Appearance.ForeColor = Color.White;
            //        e.HighPriority = true;
            //    }

            //}
        }

        private void btnProjectStatus_Click(object sender, EventArgs e)
        {
            int rowHandle = grvMaster.FocusedRowHandle;

            frmProjectStatus frm = new frmProjectStatus();
            frm.projectID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
                grvMaster.FocusedRowHandle = rowHandle;

            }
        }

        private void cboStatusID_EditValueChanged(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thay đổi trạng thái dự án không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SearchLookUpEdit edit = sender as SearchLookUpEdit;
                int rowHandle = edit.Properties.GetIndexByKeyValue(edit.EditValue);
                object row = edit.Properties.View.GetRow(rowHandle);

                int projectID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                int projectStatusID = TextUtils.ToInt((row as DataRowView).Row["ProjectStatusID"]);
                TextUtils.ExcuteProcedure("spUpdateProjectStatus", new string[] { "@ProjectID", "@ProjectStatusID" }, new object[] { projectID, projectStatusID });
            }
            loadProject();
        }
        List<int> listProjectID = new List<int>();
        private void savePriority(int priority)
        {
            int[] rowIndex = grvMaster.GetSelectedRows();
            for (int i = 0; i < rowIndex.Length; i++)
            {
                listProjectID.Add(TextUtils.ToInt(grvMaster.GetRowCellValue(rowIndex[i], colIDMaster)));
            }
            foreach (var projectID in listProjectID)
            {
                ProjectPersonalPriotityModel model = new ProjectPersonalPriotityModel();
                int id = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 ID FROM dbo.ProjectPersonalPriotity WHERE UserID = {Global.UserID} AND ProjectID = {projectID}"));

                model.ProjectID = projectID;
                model.UserID = Global.UserID;
                model.Priotity = priority;
                if (id > 0)
                {
                    model.ID = id;
                    ProjectPersonalPriotityBO.Instance.Update(model);
                }
                else
                {
                    ProjectPersonalPriotityBO.Instance.Insert(model);
                }
            }
            listProjectID.Clear();
            loadProject();
        }

        private void btnPersonalPriority1_Click(object sender, EventArgs e)
        {
            savePriority(1);
        }

        private void btnPersonalPriority2_Click(object sender, EventArgs e)
        {
            savePriority(2);
        }

        private void btnPersonalPriority3_Click(object sender, EventArgs e)
        {
            savePriority(3);
        }

        private void btnPersonalPriority4_Click(object sender, EventArgs e)
        {
            savePriority(4);
        }

        private void btnPersonalPriority5_Click(object sender, EventArgs e)
        {
            savePriority(5);
        }

        private void toolStripProjectPartList_Click(object sender, EventArgs e)
        {
            btnProjectPartList_Click(null, e);
        }

        private void toolStripProjectReport_Click(object sender, EventArgs e)
        {
            btnProjectAll_Click(null, e);
        }

        private void toolStripProjectStatus_Click(object sender, EventArgs e)
        {
            btnProjectStatus_Click(null, e);
        }

        private void toolStripProjectItem_Click(object sender, EventArgs e)
        {
            bntProjectItem_Click(null, e);
        }

        private void btnProjectEmployee_Click(object sender, EventArgs e)
        {
            btnProjectEmployees_Click(null, null);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            //loadPermission();
        }

        private void btnProjectEmployees_Click(object sender, EventArgs e)
        {
            int projectID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (projectID <= 0)
            {
                return;
            }
            frmProjectEmployee frm = new frmProjectEmployee();
            ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectID);
            frm.project = project;


            if (CheckPermission())
            {
                TextUtils.ExcuteProcedure("spGetProjectParticipant", new string[] { "@ProjectID", "@LoginName" }, new object[] { projectID, Global.LoginName });

            }
            frm.btnSave.Enabled = frm.btnSaveAndClose.Enabled = frm.colDelete.OptionsColumn.AllowEdit = CheckPermission();
            frm.ShowDialog();
        }

        private void btnLeaderProject_Click(object sender, EventArgs e)
        {
            frmLeaderProject frm = new frmLeaderProject();
            frm.ShowDialog();
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            int rowHandle = grvMaster.FocusedRowHandle;
            int projectID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (projectID <= 0)
            {
                return;
            }

            ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectID);
            frmUploadFile frm = new frmUploadFile();
            frm.project = project;

            frm.Show();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    btnFind_Click(null, null);
            //    grvMaster.FocusedRowHandle = rowHandle;
            //}
        }

        private void btnProjectWorkerContext_Click(object sender, EventArgs e)
        {
            btnProjectWorker_Click(null, null);
        }

        private void btnSummaryWorkerContext_Click(object sender, EventArgs e)
        {
            btnSummaryWorker_Click(null, null);
        }

        private void btnSummaryWorker_Click(object sender, EventArgs e)
        {
            int projectID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (projectID <= 0)
            {
                return;
            }
            frmProjectWorkerSynthetic frm = new frmProjectWorkerSynthetic();
            frm.projectID = projectID;
            frm.ShowDialog();
        }

        private void btnProjectWorker_Click(object sender, EventArgs e)
        {
            string projectCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectCode));
            string projectName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectName));
            int projectID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (projectID <= 0) return;

            ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectID);
            //frmProjectWorker frm = new frmProjectWorker();
            frmProjectWorker_New frm = new frmProjectWorker_New();
            frm.project = project;
            //frm.projectID = projectID;
            //frm.projectCode = projectCode;
            //frm.projectName = projectName;
            frm.ShowDialog();
        }

        private void btnGranttChartProjectContext_Click(object sender, EventArgs e)
        {
            btnGranttChartProject_Click(null, null);
        }

        private void cboBusinessField_EditValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        void AddProjectEmployee()
        {
            List<ProjectEmployeeModel> list = new List<ProjectEmployeeModel>();
            int userTech = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUserID));

        }

        private void btnProjectRequestAndSolution_Click(object sender, EventArgs e)
        {
            int projectId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (projectId <= 0)
            {
                return;
            }
            frmProjectRequest frm = new frmProjectRequest();
            frm.cboProject.EditValue = projectId;
            frm.Show();
        }

        private void btnAddProjectItem_Click(object sender, EventArgs e)
        {

        }

        private void btnAddProjectItem_Click_1(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;

            frmProjectItemDetail frm = new frmProjectItemDetail();
            frm.cboProject.EditValue = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectItem();
            }
        }

        private void btnEditProjectItem_Click(object sender, EventArgs e)
        {
            int projectId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            int id = TextUtils.ToInt(grvProjectItem.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            ProjectItemModel projectItem = SQLHelper<ProjectItemModel>.FindByID(id);
            frmProjectItemDetail frm = new frmProjectItemDetail();
            frm.cboProject.EditValue = projectId;
            frm.projectItem = projectItem;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectItem();
            }
        }

        private void grvProjectItem_DoubleClick(object sender, EventArgs e)
        {
            btnEditProjectItem_Click(null, null);
        }

        private void btnProjectHistoryProblem_Click(object sender, EventArgs e)
        {
            //frmHistoryProjectProblem frm = new frmHistoryProjectProblem();
            //frm.ShowDialog();

            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (ID <= 0) return;
            ProjectModel model = (ProjectModel)ProjectBO.Instance.FindByPK(ID);
            frmHistoryProjectProblem frm = new frmHistoryProjectProblem();

            frm.project = model;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnProjectPartlistProblem_Click(object sender, EventArgs e)
        {
            int projectID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            frmPartlistProblem frm = new frmPartlistProblem();
            frm.projectID = projectID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectItem();
            }
        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            ProjectModel model = (ProjectModel)ProjectBO.Instance.FindByPK(ID);
            frmProjectDetail frm = new frmProjectDetail();
            frm.project = model;
            frm.txtPrio.Text = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPriotityText));

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                //grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnProjectTypeLink_Click(object sender, EventArgs e)
        {
            int rowHandle = grvMaster.FocusedRowHandle;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            frmProjectTypeLink frm = new frmProjectTypeLink();
            frm.cboProject.EditValue = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
                grvMaster.FocusedRowHandle = rowHandle;
                loadProjectTypeLink();
            }
        }

        private void cboProjectType_EditValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void Item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            //MessageBox.Show(item.Text, item.Name);

            var rowHandle = grvMaster.FocusedRowHandle;
            int[] selectedRows = grvMaster.GetSelectedRows();
            foreach (int row in selectedRows)
            {
                int projectID = TextUtils.ToInt(grvMaster.GetRowCellValue(row, "ID"));
                if (projectID <= 0) continue;
                ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectID);
                project.ProjectStatus = TextUtils.ToInt(item.Tag);

                SQLHelper<ProjectModel>.Update(project);

                TextUtils.ExcuteProcedure("spUpdateProjectStatus",
                                            new string[] { "@ProjectID", "@ProjectStatusID" },
                                            new object[] { project.ID, TextUtils.ToInt(item.Tag) });

            }

            loadProject();
            grvMaster.FocusedRowHandle = rowHandle;
        }

        private void btnUpdateProjectStatus_DropDownOpening(object sender, EventArgs e)
        {
            if (btnUpdateProjectStatus.DropDownItems.Count > 0)
            {
                btnUpdateProjectStatus.DropDownItems.Clear();
            }
            List<ProjectStatusModel> listStatus = SQLHelper<ProjectStatusModel>.FindAll().OrderBy(x => x.STT).ToList();
            foreach (ProjectStatusModel status in listStatus)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = $"btnProjectStatus{status.ID}";
                item.Text = $"{status.StatusName}";
                item.Tag = $"{status.ID}";
                item.Click += Item_Click;
                btnUpdateProjectStatus.DropDownItems.Add(item);
            }
        }

        //LinhTN update 01/08/2024
        private void loadProjectCurrentSituation()
        {
            int projectID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            DataTable dt = TextUtils.GetDataTableFromSP("spGetProjectCurrentSituation", new string[] { "@ProjectID" }, new object[] { projectID });
            grdProjectCurrentSituation.DataSource = dt;
        }
        //LinhTN update 01/08/2024
        private void btnProjectCurrentSituation_Click(object sender, EventArgs e)
        {
            int rowHandle = grvMaster.FocusedRowHandle;
            int projectID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (projectID <= 0) return;
            frmProjectCurrentSituation frm = new frmProjectCurrentSituation();
            frm.projectID = projectID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
                grvMaster.FocusedRowHandle = rowHandle;
                loadProjectCurrentSituation();
            }
        }
    }
}
