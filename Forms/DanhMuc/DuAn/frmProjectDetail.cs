using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList.Nodes;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace BMS
{
    public partial class frmProjectDetail : _Forms
    {
        int warehouseID = 1;
        public ProjectModel project = new ProjectModel();
        DataTable dtCustomer = new DataTable();
        DataTable dtcontact = new DataTable();
        DataTable dtGroupFile = new DataTable();
        List<int> lstIDDelete = new List<int>();


        DataTable dtStatus = new DataTable();

        DataSet dsEmployee;

        int selectedProjectType = 0;
        List<ProjectPriorityLinkModel> listPriorities = new List<ProjectPriorityLinkModel>();

        int projectStatusOld = 0;
        DateTime? dateStatusLog = null;

        public frmProjectDetail()
        {
            InitializeComponent();
        }

        private void frmProjectDetail_Load(object sender, EventArgs e)
        {
            //cboTypeProject.SelectedIndex = 1;
            btnAddStatus.Visible = Global.IsAdmin;

            dsEmployee = TextUtils.LoadDataSetFromSP("spGetEmployeeForProject", new string[] { }, new object[] { });

            loadLeader();
            loadFolder();
            loadUser();
            LoadPM();
            loadCustomer();
            loadGroupFile();
            loadProjectTypeCbo();
            loadProjectTypeTree();
            LoadProjectStatus();

            //cboStatus.EditValue = 1;

            LoadFirmBase();
            LoadProjectTypeBase();


            loadProjectUser();

            loadBusinessField(); // TODO 061023: QUANG BỔ SUNG LOAD DỮ LIỆU LĨNH VỰC DỰ ÁN

            btnEdit.Visible = false;
            //this.cbGroupFile.EditValueChanged += new EventHandler(cbGroupFile_EditValueChanged);

            loadProjectDetail();
            //txtProjectCode.Enabled = true;
            txtProjectCode.ReadOnly = !(Global.IsAdmin && Global.EmployeeID <= 0);
        }
        #region Methods
        /// <summary>
        /// lấy ra danh sách khách hàng
        /// </summary>
        void loadCustomer()
        {
            dtCustomer = TextUtils.Select("SELECT * FROM dbo.Customer where IsDeleted <> 1");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dtCustomer;

            cboEndUser.Properties.DisplayMember = "CustomerName";
            cboEndUser.Properties.ValueMember = "ID";
            cboEndUser.Properties.DataSource = dtCustomer;
        }

        /// <summary>
        /// lấy ra danh sách bảng nhóm file
        /// </summary>
        void loadGroupFile()
        {
            dtGroupFile = TextUtils.Select($"SELECT * FROM dbo.GroupFile");
            //cbGroupFile.DisplayMember = "GroupFileCode";
            //cbGroupFile.ValueMember = "ID";
            //cbGroupFile.DataSource = dtGroupFile;
            //colGroupFileID.ColumnEdit = cbGroupFile;
        }

        /// <summary>
        /// lấy ra danh sách kiểu dự án
        /// </summary>
        void loadProjectTypeTree()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectTypeLink", "A", new string[] { "@ProjectID" }, new object[] { project.ID, });
            tlProjectType.DataSource = dt;
            tlProjectType.ExpandAll();
            //cbProjectType.Properties.DisplayMember = "ProjectTypeName";
            //cbProjectType.Properties.ValueMember = "ID";
            //cbProjectType.Properties.DataSource = dt;
        }
        DataTable dtProjectType = null;
        void ResetdtProjectType()
        {
            if (dtProjectType == null)
                return;
            foreach (DataRow row in dtProjectType.Rows)
                row["Selected"] = false;
        }
        void loadProjectTypeCbo()
        {
            //dtProjectType = TextUtils.Select("SELECT * FROM ProjectType");
            ////DataTable dt = TextUtils.LoadDataFromSP("spGetProjectTypeLink", "A", new string[] { "@ProjectID" }, new object[] { project.ID, });
            ////tlProjectType.DataSource = dt;
            ////tlProjectType.ExpandAll();
            //dtProjectType.Columns.Add("Selected", typeof(bool));
            //ResetdtProjectType();
            //treeList2.DataSource = dtProjectType;

            //cboProjectTypeTreeList.Properties.DisplayMember = "ProjectTypeName";
            //cboProjectTypeTreeList.Properties.ValueMember = "ID";
            //cboProjectTypeTreeList.Properties.DataSource = dtProjectType;
        }

        /// <summary>
        /// Load Leader cho dự án
        /// </summary>
        void loadLeader()
        {
            //List<EmployeeApproveModel> list = SQLHelper<EmployeeApproveModel>.FindByAttribute("Type", 2);

            //DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeApprove", "A", new string[] { "@Type", "@ProjectID" }, new object[] { 2, project.ID });

            //DateTime dateCreated = project.ID <= 0 ? txtCreateDate.Value : project.CreatedDate.Value;

            //DataTable dt = TextUtils.LoadDataFromSP("spGetLeaderProject", "A", new string[] { "@DateCreated" }, new object[] { dateCreated });

            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetUserTeam", new string[] { "@DepartmentID" }, new object[] { 0 });
            cboLeaderID.DisplayMember = "FullName";
            //cboLeaderID.ValueMember = "LeaderID";
            cboLeaderID.ValueMember = "EmployeeID";
            cboLeaderID.DataSource = dataSet.Tables[1];
            //cboLeaderID.DataSource = dt;
        }

        /// <summary>
        /// lấy ra danh sách bảng contact theo từng khách hàng
        /// </summary>
        void loadContact()
        {
            dtcontact = TextUtils.Select($"SELECT ContactPhone,ContactEmail,ContactName,ID FROM dbo.CustomerContact where CustomerID={cbCustomer.EditValue}");
            cbContact.Properties.DisplayMember = "ContactName";
            cbContact.Properties.ValueMember = "ID";
            cbContact.Properties.DataSource = dtcontact;
        }

        /// <summary>
        /// lấy ra danh sách người phụ trách
        /// </summary>
        void loadUser()
        {
            cbUserSale.Properties.DataSource = cbUserTechnical.Properties.DataSource = cbUserProject.DataSource = dsEmployee.Tables[1];
            cbUserSale.Properties.DisplayMember = cbUserTechnical.Properties.DisplayMember = cbUserProject.DisplayMember = "FullName";
            cbUserSale.Properties.ValueMember = cbUserTechnical.Properties.ValueMember = cbUserProject.ValueMember = "ID";
        }
        /// <summary>
        /// Lấy ra danh sách PM
        /// </summary>
        void LoadPM()
        {
            cbPM.Properties.DataSource = TextUtils.GetTable("spGetEmployeeForProject");
            cbPM.Properties.DisplayMember = "FullName";
            cbPM.Properties.ValueMember = "EmployeeID";
        }
        /// <summary>
        /// load trạng thái dự án
        /// </summary>
        void LoadProjectStatus()
        {
            List<ProjectStatusModel> list = SQLHelper<ProjectStatusModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboStatus.Properties.DataSource = list;// TextUtils.Select("Select * from ProjectStatus");
            cboStatus.Properties.DisplayMember = "StatusName";
            cboStatus.Properties.ValueMember = "ID";
        }
        /// <summary>
        /// load ra project detail
        /// </summary>
        void loadProjectDetail()
        {
            if (project.ID > 0)
            {
                txtProjectCode.Text = TextUtils.ToString(project.ProjectCode);
                txtProjectName.Text = TextUtils.ToString(project.ProjectName);
                //txtShortName.Text = TextUtils.ToString(project.ProjectShortName);
                txtNote.Text = TextUtils.ToString(project.Note);
                txtPO.Text = TextUtils.ToString(project.PO);
                cbCustomer.EditValue = project.CustomerID;
                cbUserSale.EditValue = project.UserID;
                cbUserTechnical.EditValue = project.UserTechnicalID;
                txtCreateDate.Value = (DateTime)project.CreatedDate;
                //cboStatus.EditValue = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT ProjectStatusID FROM dbo.ProjectStatusDetail WHERE ProjectID = {project.ID} AND Selected = 1"));

                cboStatus.EditValue = project.ProjectStatus;
                cbContact.EditValue = project.ContactID;
                cbPM.EditValue = project.ProjectManager;
                //cbProjectType.EditValue = project.ProjectType;
                txtEU.Text = project.EU;
                txtCurrentState.Text = project.CurrentState;

                //dtpPlanDS.EditValue = project.PlanDateStart.HasValue == true ? project.PlanDateStart.Value : (DateTime?)dtpPlanDS.EditValue;
                //dtpPlanDE.EditValue = project.PlanDateEnd.HasValue == true ? project.PlanDateEnd.Value : (DateTime?)dtpPlanDE.EditValue;
                //dtpActualDS.EditValue = project.ActualDateStart.HasValue == true ? project.ActualDateStart.Value : (DateTime?)dtpActualDS.EditValue;
                //dtpActualDE.EditValue = project.ActualDateEnd.HasValue == true ? project.ActualDateEnd.Value : (DateTime?)dtpActualDE.EditValue;

                //if (project.Priotity == 5) cbPriotity.SelectedIndex = 4;
                //else if (project.Priotity == 2) cbPriotity.SelectedIndex = 1;
                //else if (project.Priotity == 3) cbPriotity.SelectedIndex = 2;
                //else if (project.Priotity == 4) cbPriotity.SelectedIndex = 3;
                //else cbPriotity.SelectedIndex = 0;

                //txtPrio.Text = project.Priotity.ToString();

                cboEndUser.EditValue = project.EndUser;
                //dtpPODate.EditValue = project.PODate;


                projectStatusOld = project.ProjectStatus;


            }

            cboTypeProject.SelectedIndex = project.TypeProject <= 0 ? 1 : project.TypeProject;
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectDetail", "A", new string[] { "@ID" }, new object[] { project.ID });
            //grdData.DataSource = dt;

            loadFollowProjectBase();
        }

        private void loadFollowProjectBase()
        {
            if (project.ID <= 0) return;
            FollowProjectBaseModel followProject = SQLHelper<FollowProjectBaseModel>.FindByAttribute("ProjectID", project.ID).OrderByDescending(x => x.ExpectedPlanDate).FirstOrDefault();
            if (followProject != null)
            {
                //Thực tế
                //followProject.ExpectedPlanDate = followProject.ExpectedPlanDate;
                //project.PlanDateEnd = followProject.ExpectedProjectEndDate;
                //project.PODate = followProject.ExpectedPODate;
                //dtpExpectedQuotationDate.EditValue = followProject.ExpectedQuotationDate;

                dtpRealityPlanDate.EditValue = followProject.RealityPlanDate;
                dtpRealityQuotationDate.EditValue = followProject.RealityQuotationDate;
                dtpRealityPODate.EditValue = followProject.RealityPODate;
                dtpRealityProjectEndDate.EditValue = followProject.RealityProjectEndDate;

                dtpRealityProjectEndDate.Enabled = !followProject.RealityProjectEndDate.HasValue;

                // thực tế
                //project.ActualDateStart = followProject.RealityPlanDate;
                //project.ActualDateEnd = followProject.RealityProjectEndDate;
                //dtpRealityPODate.EditValue = followProject.RealityPODate;
                //dtpRealityQuotationDate.EditValue = followProject.RealityProjectEndDate;

                dtpExpectedPlanDate.EditValue = followProject.ExpectedPlanDate;
                dtpExpectedQuotationDate.EditValue = followProject.ExpectedQuotationDate;
                dtpExpectedPODate.EditValue = followProject.ExpectedPODate;
                dtpExpectedProjectEndDate.EditValue = followProject.ExpectedProjectEndDate;

                cboFirmBase.EditValue = followProject.FirmBaseID;
                cboProjectTypeBase.EditValue = followProject.ProjectTypeBaseID;
                txtProjectContactName.Text = followProject.ProjectContactName;
            }

            //if (project.ID > 0)
            //{
            //    var ex1 = new Expression("ProjectID", project.ID);

            //    List<FollowProjectBaseModel> followProjectBase = SQLHelper<FollowProjectBaseModel>.FindByExpression(ex1);
            //    if (followProjectBase.Count > 0)
            //    {
            //        followProjectBase = followProjectBase.OrderByDescending(f => f.UpdatedDate).ToList();

            //        if (string.IsNullOrEmpty(TextUtils.ToString(dtpPODate.EditValue)))
            //        {
            //            dtpPODate.EditValue = followProjectBase[0].ExpectedPODate;
            //        }
            //        dtpExpectedQuotationDate.EditValue = followProjectBase[0].ExpectedProjectEndDate;
            //        dtpRealityPODate.EditValue = followProjectBase[0].RealityPODate;
            //        dtpRealityQuotationDate.EditValue = followProjectBase[0].RealityQuotationDate;
            //    }
            //}
        }

        void loadFolder()
        {
            DataTable dt = TextUtils.Select($"Select * from ProjectTreeFolder");
            //treeList1.DataSource = dt;
        }

        private void LoadFirmBase()
        {
            //DataTable dt = TextUtils.Select("SELECT * FROM FirmBase");
            List<FirmBaseModel> listFirmBase = SQLHelper<FirmBaseModel>.FindAll();
            cboFirmBase.Properties.DisplayMember = "FirmName";
            cboFirmBase.Properties.ValueMember = "ID";
            cboFirmBase.Properties.DataSource = listFirmBase;
        }
        private void LoadProjectTypeBase()
        {
            //DataTable dt = TextUtils.Select("SELECT * FROM ProjectTypeBase");
            List<ProjectTypeBaseModel> listType = SQLHelper<ProjectTypeBaseModel>.FindAll();
            cboProjectTypeBase.Properties.DisplayMember = "ProjectTypeName";
            cboProjectTypeBase.Properties.ValueMember = "ID";
            cboProjectTypeBase.Properties.DataSource = listType;
        }
        #endregion

        #region Buttons Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            //string selected = txtSeleted.Text;
            string path = "ftp://192.168.1.2/DuAn";

            if (saveData())
            {

                //// thư mục
                //try
                //{
                //    for (int i = 0; i < grvData.RowCount; i++)
                //    {
                //        string groupFile = TextUtils.ToString(grvData.GetRowCellValue(i, colGroupFileCode));
                //        string fileName = TextUtils.ToString(grvData.GetRowCellValue(i, colFileName));
                //        string projectCode = txtProjectCode.Text.Trim();
                //        string projectName = txtProjectName.Text.Trim();
                //        //tạo thư mục theo tên dự án
                //        if (!DocUtils.CheckExits($"DuAn/{projectCode}/{groupFile}"))
                //        {
                //            DocUtils.CreateDirectory($"DuAn/{projectCode}/{groupFile}");
                //        }
                //        if (DocUtils.CheckExits($"DuAn/{projectCode}/{groupFile}/{fileName}"))
                //        {
                //            DocUtils.DeleteFile($"DuAn/{projectCode}/{groupFile}/{fileName}");
                //        }

                //        // triển khai xuất excel
                //        string fileProject = fileName;// + DateTime.Now.ToString("_yyyy_MM_dd") + ".xlsx";
                //        string fileSourceName = "ProjectExcel.xlsx";
                //        string sourcePath = Application.StartupPath + "\\" + fileSourceName;
                //        string currentPath = Application.StartupPath + "\\" + fileProject;
                //        try
                //        {
                //            File.Copy(sourcePath, currentPath, true);
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show("Có lỗi khi tạo excel!" + Environment.NewLine + ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //            return;
                //        }
                //        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                //        Excel.Application app = default(Excel.Application);
                //        Excel.Workbook workBoook = default(Excel.Workbook);
                //        Excel.Worksheet workSheet = default(Excel.Worksheet);
                //        try
                //        {
                //            app = new Excel.Application();
                //            app.Workbooks.Open(currentPath);
                //            workBoook = app.Workbooks[1];
                //            workSheet = (Excel.Worksheet)workBoook.Worksheets[1];
                //            workSheet.Cells[2, 6] = txtProjectName.Text.Trim();
                //            workSheet.Cells[5, 1] = cbStatus.Text.Trim();
                //            workSheet.Cells[5, 2] = txtProjectCode.Text.Trim();
                //            workSheet.Cells[5, 3] = txtProjectName.Text.Trim();
                //            workSheet.Cells[5, 4] = txtPO.Text.Trim();
                //            workSheet.Cells[5, 5] = cbUserSale.Text.Trim();
                //            workSheet.Cells[5, 6] = cbCustomer.Text.Trim();
                //            workSheet.Cells[5, 7] = cbContact.Text.Trim();
                //            workSheet.Cells[5, 8] = txtPhone.Text.Trim();
                //            workSheet.Cells[5, 9] = txtEmail.Text.Trim();
                //            workSheet.Cells[5, 10] = txtNote.Text.Trim();
                //            workSheet.Cells[5, 11] = txtCreateDate.Text.Trim();
                //            workSheet.Cells[8, 1] = i + 1;
                //            workSheet.Cells[8, 2] = TextUtils.ToString(grvData.GetRowCellValue(i, colGroupFileCode));
                //            workSheet.Cells[8, 3] = TextUtils.ToString(grvData.GetRowCellValue(i, colFileName));
                //            workSheet.Cells[8, 4] = TextUtils.ToString(grvData.GetRowCellValue(i, colPathFull));
                //        }

                //        catch (Exception ex)
                //        {
                //            MessageBox.Show(ex.Message);
                //        }
                //        finally
                //        {
                //            if (app != null)
                //            {
                //                app.ActiveWorkbook.Save();
                //                app.Workbooks.Close();
                //                app.Quit();
                //            }
                //        }
                //        //Process.Start(currentPath);

                //        // load lại đg dẫy ftp tạm
                //        DocUtils.InitFTPQLSX();

                //        // đẩy file lên sever
                //        DocUtils.UploadFile(currentPath, $@"{path}/{projectCode}/{groupFile}");

                //        // xóa excel trong debug 
                //        File.Delete(Path.Combine(currentPath));
                //    }
                //}
                //catch (Exception ex)
                //{
                //    //MessageBox.Show(ex.Message);
                //    MessageBox.Show(string.Format("Thư mục của dự án {0} chưa tồn tại trong {1}", txtProjectCode.Text.Trim(), path));
                //}
                CreateFodelProject();
                this.DialogResult = DialogResult.OK;
            }

        }
        void CreateFodelProject()
        {
            //DocUtils.InitFTPTK();
            //if (!DocUtils.CheckExits($@"Projects/{DateTime.Now.Year}/{customer}/{txtProjectCode}-{txtProjectName}"))
            //{
            //    DataSet ds = TextUtils.LoadDataSetFromSP("spGetFolderName", new string[] { }, new object[] { });
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        DataRow[] dtr = ds.Tables[1].Select($" ParentID = {ds.Tables[0].Rows[i]["ID"]}");
            //        if (dtr.Length == 0)
            //            DocUtils.CreateDirectory($@"Projects/{DateTime.Now.Year}/{customer}/{txtProjectCode.Text}-{txtProjectName.Text}/{ds.Tables[0].Rows[i]["FolderName"]}");
            //        for (int j = 0; j < dtr.Length; j++)
            //        {
            //            DocUtils.CreateDirectory($@"Projects/{DateTime.Now.Year}/{customer}/{txtProjectCode.Text}-{txtProjectName.Text}/{ds.Tables[0].Rows[i]["FolderName"]}/{dtr[j]["FolderName"]}");
            //        }
            //    }

            //}

        }
        private void btnNewGroupFile_Click(object sender, EventArgs e)
        {
            frmGroupFileDetail frm = new frmGroupFileDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGroupFile();
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn thêm dự án mới hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                saveData();
                cbCustomer.Text = "";
                txtProjectCode.Clear();
                cboStatus.EditValue = -1;
                txtProjectName.Clear();
                cbUserSale.Text = "";
                cbContact.Text = "";
                txtPhone.Clear();
                txtEmail.Clear();
                txtPO.Clear();
                txtNote.Clear();
                //for (int i = grvData.RowCount - 1; i >= 0; i--)
                //{
                //    grvData.DeleteRow(i);
                //}
                project = new ProjectModel();
            }
        }

        /// <summary>
        /// click button sửa khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(cbCustomer.EditValue);
            if (ID == 0) return;
            CustomerModel model = (CustomerModel)CustomerBO.Instance.FindByPK(ID);
            //frmCustomerDetail frm = new frmCustomerDetail();
            frmCustomerDetailNew frm = new frmCustomerDetailNew(warehouseID);


            frm.customer = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCustomer();
                cbCustomer_EditValueChanged(null, null);
            }
        }

        /// <summary>
        /// click chọn nhiều file đồng thời thêm dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //int STT;
            //OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Multiselect = true; // lựa chọn nhiều file
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    foreach (string file in dialog.FileNames)
            //    {
            //        string[] folders = file.Split('\\'); // tách khi chọn đường dẫn
            //        string[] fileName = folders[folders.Length - 1].Split('.'); // tách file bất kì để lấy tên 

            //        DataTable dt = (DataTable)grdData.DataSource;
            //        DataRow dtrow = dt.NewRow();
            //        if (dt.Rows.Count == 0) STT = 1;
            //        else STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;
            //        dtrow["STT"] = STT;
            //        dtrow["FileName"] = fileName[0] + ".xlsx";
            //        dt.Rows.Add(dtrow);
            //        grdData.DataSource = dt;
            //    }
            //}
        }

        /// <summary>
        /// click button xóa dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //if (grdData.DataSource == null) return;
            //int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            //string fileName = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colFileName));

            //if (MessageBox.Show(String.Format($"Bạn có chắc chắn muốn xóa file '{fileName}' không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    grvData.DeleteSelectedRows();
            //    lstIDDelete.Add(strID);
            //}
        }

        /// <summary>
        /// click chọn file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFile_Click(object sender, EventArgs e)
        {
            //OpenFileDialog dialog = new OpenFileDialog();
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    string[] folders = dialog.FileName.Split('\\'); // tách khi chọn đường dẫn
            //    string[] fileName = folders[folders.Length - 1].Split('.'); // tách file bất kì để lấy tên 
            //    grvData.SetFocusedRowCellValue(colFileName, fileName[0] + ".xlsx");
            //    cbGroupFile_EditValueChanged(null, null);
            //}
        }

        /// <summary>
        /// click button thêm khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewCustomer_Click(object sender, EventArgs e)
        {
            //frmCustomerDetail frm = new frmCustomerDetail();
            frmCustomerDetailNew frm = new frmCustomerDetailNew(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCustomer();
            }
        }

        /// <summary>
        /// click button reset mã dự án
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetProjectCode_Click(object sender, EventArgs e)
        {
            if (project.ID < 0) return;
            DataRow[] rows = dtCustomer.Select($"ID ={cbCustomer.EditValue}");
            if (TextUtils.ToString(rows[0]["CustomerShortName"]) == "")
            {
                MessageBox.Show("Khách hàng đang không có tên kí hiệu. Xin vui lòng thêm thông tin tên kí hiệu!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            int _number = 0, number = 0;
            txtProjectCode.Enabled = true;
            string year = TextUtils.ToString(DateTime.Now.Year).Substring(2);
            string _projectCode = txtProjectCode.Text.Trim();
            if (_projectCode != "") _number = TextUtils.ToInt(_projectCode.Substring(_projectCode.Length - 2));
            string projectCode = TextUtils.ToString(TextUtils.ExcuteScalar("SELECT top 1 ProjectCode FROM Project ORDER BY CreatedDate"));
            if (projectCode != "" && projectCode.Contains(".")) number = TextUtils.ToInt(projectCode.Substring(projectCode.Length - 2));
            if (_number <= number)
            {
                string count = TextUtils.ToString(number + 1);
                for (int j = 0; count.Length < 2; j++)
                {
                    count = ".0" + count;
                }
                txtProjectCode.Text = TextUtils.ToString(rows[0]["CustomerShortName"]) + year + TextUtils.ToString(count);
            }
        }

        /// <summary>
        /// click button insert dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsert_Click(object sender, EventArgs e)
        {
            //int STT;
            //DataTable dt = (DataTable)grdData.DataSource;
            //if (dt.Rows.Count == 0) STT = 1;
            //else STT = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, "STT")) + 1;
            //DataRow dtrow = dt.NewRow();
            //dtrow["STT"] = STT;
            //dt.Rows.InsertAt(dtrow, STT - 1);
            //grdData.DataSource = dt;
        }
        #endregion

        /// <summary>
        /// chọn nhóm file -> 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbGroupFile_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    string projectCode = txtProjectCode.Text.Trim();
            //    grvData.Focus();
            //    txtProjectCode.Focus();
            //    int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colGroupFileID));
            //    string fileName = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colFileName));
            //    DataRow[] dtrows = dtGroupFile.Select("ID = " + ID);
            //    if (dtrows.Length > 0)
            //    {
            //        string groupFileCode = TextUtils.ToString(dtrows[0]["GroupFileCode"]);
            //        grvData.SetFocusedRowCellValue(colGroupFileCode, groupFileCode);
            //        string newPath = $"\\" + $"\\192.168.1.171\\ftp\\DuAn\\" + projectCode + "\\" + groupFileCode + "\\" + fileName;
            //        grvData.SetFocusedRowCellValue(colPathShort, projectCode + "\\" + groupFileCode + "\\" + fileName);
            //        grvData.SetFocusedRowCellValue(colPathFull, newPath);
            //    }
            //}
            //catch (Exception ex)
            //{ }
        }

        bool saveData()
        {
            projectStatusOld = project.ProjectStatus;
            //grvData.Focus();
            try
            {
                txtProjectCode.Focus();

                if (!ValidateForm()) return false;
                project.ProjectCode = TextUtils.ToString(txtProjectCode.Text);
                project.ProjectName = TextUtils.ToString(txtProjectName.Text);
                //project.ProjectShortName = TextUtils.ToString(txtShortName.Text);
                project.ProjectStatus = TextUtils.ToInt(cboStatus.EditValue);
                project.Note = TextUtils.ToString(txtNote.Text);
                project.CustomerID = TextUtils.ToInt(cbCustomer.EditValue);
                project.ContactID = TextUtils.ToInt(cbContact.EditValue);
                project.UserID = TextUtils.ToInt(cbUserSale.EditValue);
                project.UserTechnicalID = TextUtils.ToInt(cbUserTechnical.EditValue);
                project.PO = TextUtils.ToString(txtPO.Text);
                project.CreatedDate = txtCreateDate.Value;
                project.ProjectManager = TextUtils.ToInt(cbPM.EditValue);
                project.CurrentState = txtCurrentState.Text;
                //project.ProjectType = TextUtils.ToInt(cbProjectType.EditValue);

                //project.PlanDateStart = (DateTime?)dtpPlanDS.EditValue;
                //project.PlanDateEnd = (DateTime?)dtpPlanDE.EditValue;
                //project.ActualDateStart = (DateTime?)dtpActualDS.EditValue;
                //project.ActualDateEnd = (DateTime?)dtpActualDE.EditValue;
                project.EU = txtEU.Text.Trim();
                if (cbPriotity.SelectedIndex == 0) project.Priotity = 1;
                else if (cbPriotity.SelectedIndex == 1) project.Priotity = 2;
                else if (cbPriotity.SelectedIndex == 2) project.Priotity = 3;
                else if (cbPriotity.SelectedIndex == 3) project.Priotity = 4;
                else project.Priotity = 5;
                //project.PODate = TextUtils.ToDate4(dtpPODate.EditValue);
                project.EndUser = TextUtils.ToInt(cboEndUser.EditValue);

                project.Priotity = TextUtils.ToDecimal(txtPrio.Text);

                project.TypeProject = cboTypeProject.SelectedIndex;
                project.ActualDateEnd = TextUtils.ToDate4(dtpRealityProjectEndDate.EditValue);
                //TODO: 061023
                BusinessFieldModel _bussinessField = (BusinessFieldModel)(cboBusinessField.GetSelectedDataRow());
                project.BusinessFieldID = _bussinessField == null ? 0 : _bussinessField.ID;

                //Update lịch sử trạng thái
                if (projectStatusOld != project.ProjectStatus && project.ID > 0 && dateStatusLog.HasValue)
                {

                    ProjectStatusLogModel statusLog = new ProjectStatusLogModel()
                    {
                        ProjectID = project.ID,
                        ProjectStatusID = project.ProjectStatus,
                        EmployeeID = Global.EmployeeID,
                        DateLog = dateStatusLog.Value
                    };

                    SQLHelper<ProjectStatusLogModel>.Insert(statusLog);
                }

                if (project.ID > 0)
                {
                    ProjectBO.Instance.Update(project);
                }
                else
                {
                    project.ID = (int)ProjectBO.Instance.Insert(project);
                }
                TextUtils.ExcuteProcedure("spUpdateProjectStatus", new string[] { "@ProjectID", "@ProjectStatusID" }, new object[] { project.ID, TextUtils.ToInt(cboStatus.EditValue) });

                //Thêm dữ liệu vào bảng người tham gia
                foreach (TreeListNode node in tlProjectType.GetNodeList())
                {
                    bool isSelected = TextUtils.ToBoolean(tlProjectType.GetRowCellValue(node, colTLSelected));
                    int userID = TextUtils.ToInt(tlProjectType.GetRowCellValue(node, colTlLeaderID));
                    var rowData = (DataRowView)cboLeaderID.GetRowByKeyValue(userID);
                    if (!isSelected || userID <= 0 || rowData == null)
                    {
                        continue;
                    }

                    ProjectEmployeeModel model = new ProjectEmployeeModel();


                    int projectTypeID = TextUtils.ToInt(tlProjectType.GetRowCellValue(node, colProjectTypeID));
                    int employeeID = TextUtils.ToInt(rowData["EmployeeID"]);

                    var exp1 = new Expression("ProjectID", project.ID);
                    var exp2 = new Expression("EmployeeID", employeeID);
                    var exp3 = new Expression("ProjectTypeID", projectTypeID);
                    var exp4 = new Expression("IsDeleted", 1, "<>");
                    var projectEmployee = SQLHelper<ProjectEmployeeModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4)).ToList();
                    if (projectEmployee.Count > 0)
                    {
                        model = projectEmployee.FirstOrDefault();
                    }

                    model.ProjectID = project.ID;
                    model.EmployeeID = employeeID;
                    model.ProjectTypeID = projectTypeID;
                    //model.ProjectTypeID = projectTypeID;
                    model.IsLeader = true;

                    if (model.ID > 0)
                    {
                        SQLHelper<ProjectEmployeeModel>.Update(model);
                    }
                    else
                    {
                        var list = SQLHelper<ProjectEmployeeModel>.FindByExpression(exp1.And(exp4)).ToList();
                        model.STT = list.Count + 1;
                        SQLHelper<ProjectEmployeeModel>.Insert(model);
                    }
                }

                //Thêm data vào ProjectUser
                for (int i = 0; i < grvProjectUser.RowCount; i++)
                {
                    long id = TextUtils.ToInt64(grvProjectUser.GetRowCellValue(i, colIDProjectUser));
                    ProjectUserModel projectUser = new ProjectUserModel();
                    if (id > 0)
                    {
                        projectUser = (ProjectUserModel)ProjectUserBO.Instance.FindByPK(id);
                    }
                    projectUser.ProjectID = project.ID;
                    projectUser.STT = TextUtils.ToInt(grvProjectUser.GetRowCellValue(i, colSTT));
                    projectUser.UserID = TextUtils.ToInt(grvProjectUser.GetRowCellValue(i, colUser));
                    projectUser.Mission = TextUtils.ToString(grvProjectUser.GetRowCellValue(i, colMission));
                    if (projectUser.ID > 0)
                    {
                        ProjectUserBO.Instance.Update(projectUser);
                        foreach (int item in lstIDDelete)
                        {
                            ProjectUserBO.Instance.Delete(item);
                        }
                    }
                    else
                    {
                        ProjectUserBO.Instance.Insert(projectUser);
                    }
                }

                //Add dữ liệu vào ProjectTypeLink
                AddDataToProjectTypeLink(tlProjectType.Nodes);


                //thêm vào DANH SÁCH CHI PHÍ
                //DataTable dt = TextUtils.Select("SELECT * FROM [RTC].[dbo].[ListCost]");
                //DataRow[] rows = dt.Select();
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    ProjectCostModel detailProjectCost = new ProjectCostModel();
                //    //DataTable dtProjectCost = TextUtils.Select($"SELECT * FROM [RTC].[dbo].[ProjectCost] where ProjectID={project.ID} and ListCostID={TextUtils.ToInt(rows[i]["ID"])}");
                //    if (dtProjectCost.Rows.Count > 0)
                //        detailProjectCost = (ProjectCostModel)ProjectCostBO.Instance.FindByPK(TextUtils.ToInt(dtProjectCost.Rows[0]["ID"]));
                //    detailProjectCost.STT = i + 1;
                //    detailProjectCost.ProjectID = project.ID;
                //    detailProjectCost.ListCostID = TextUtils.ToInt(rows[i]["ID"]);
                //    if (detailProjectCost.ID > 0)
                //    {
                //        ProjectCostBO.Instance.Update(detailProjectCost);
                //    }
                //    else
                //    {
                //        ProjectCostBO.Instance.Insert(detailProjectCost);
                //    }
                //}
                //DataTable dtProjectCost = TextUtils.Select($"SELECT top 1 * FROM [RTC].[dbo].[ProjectCost] where ProjectID={project.ID}");

                var projectCost = SQLHelper<ProjectCostModel>.FindByAttribute("ProjectID", project.ID);
                if (/*dtProjectCost.Rows.Count == 0*/ projectCost.Count <= 0)
                {
                    DataTable dt = TextUtils.LoadDataFromSP("spSaveProjectCost", "A", new string[] { "@ID" }, new object[] { project.ID });
                    dt.Select();
                }

                // Update lại ngày bắt đầu thực tế và dự kiến, ngày kết thúc thực tế và dự kiến vào follow dự án nếu có.
                UpdateFollowProject();

                UpdatePriority(project.ID);



                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
        }


        void UpdatePriority(int projectID)
        {
            var list = SQLHelper<ProjectPriorityLinkModel>.FindByAttribute("ProjectID", projectID);
            if (list.Count > 0)
            {
                SQLHelper<ProjectPriorityLinkModel>.DeleteListModel(list);
            }
            foreach (var item in listPriorities)
            {
                item.ProjectID = projectID;
                SQLHelper<ProjectPriorityLinkModel>.Insert(item);
            }

        }

        /// <summary>
        /// Update lại ngày bắt đầu thực tế và dự kiến, ngày kết thúc thực tế và dự kiến vào follow dự án nếu có.
        /// Lh Phúc 12/06/2024
        /// </summary>
        /// <param name="project"></param>
        private void UpdateFollowProject()
        {
            //int projectID = TextUtils.ToInt(project.ID);
            if (project.ID <= 0) return;

            var exp1 = new Expression("ProjectID", project.ID);
            var exp2 = new Expression("WarehouseID", 1);
            List<FollowProjectBaseModel> followProjectBase = SQLHelper<FollowProjectBaseModel>.FindByExpression(exp1.And(exp2));
            //List<FollowProjectBaseModel> followProjectBase1 = SQLHelper<FollowProjectBaseModel>.FindByAttribute("ProjectID", project.ID);

            FollowProjectBaseModel followProject = new FollowProjectBaseModel();
            if (followProjectBase.Count <= 0)
            {
                followProject.ProjectID = project.ID;
                ////dự kiến
                //followProject.ExpectedPlanDate = TextUtils.ToDate5(dtpExpectedPlanDate.EditValue);
                //followProject.ExpectedQuotationDate = TextUtils.ToDate5(dtpExpectedQuotationDate.EditValue);
                //followProject.ExpectedProjectEndDate = TextUtils.ToDate4(dtpExpectedProjectEndDate.EditValue);
                //followProject.ExpectedPODate = TextUtils.ToDate4(dtpExpectedPODate.EditValue);

                //// thực tế
                //followProject.RealityPlanDate = TextUtils.ToDate5(dtpRealityPlanDate.EditValue);
                //followProject.RealityProjectEndDate = TextUtils.ToDate4(dtpRealityProjectEndDate.EditValue);
                //followProject.RealityPODate = TextUtils.ToDate4(dtpRealityPODate.EditValue);
                //followProject.RealityProjectEndDate = TextUtils.ToDate4(dtpRealityProjectEndDate.EditValue);

                // dự kiến
                followProject.ExpectedPlanDate = TextUtils.ToDate5(dtpExpectedPlanDate.EditValue);
                followProject.ExpectedQuotationDate = TextUtils.ToDate5(dtpExpectedQuotationDate.EditValue);
                followProject.ExpectedPODate = TextUtils.ToDate4(dtpExpectedPODate.EditValue);
                followProject.ExpectedProjectEndDate = TextUtils.ToDate4(dtpExpectedProjectEndDate.EditValue);

                // thực tế
                followProject.RealityPlanDate = TextUtils.ToDate4(dtpRealityPlanDate.EditValue);
                followProject.RealityQuotationDate = TextUtils.ToDate4(dtpRealityQuotationDate.EditValue);
                followProject.RealityPODate = TextUtils.ToDate4(dtpRealityPODate.EditValue);
                followProject.RealityProjectEndDate = TextUtils.ToDate4(dtpRealityProjectEndDate.EditValue);

                followProject.FirmBaseID = TextUtils.ToInt(cboFirmBase.EditValue);
                followProject.ProjectTypeBaseID = TextUtils.ToInt(cboProjectTypeBase.EditValue);
                followProject.ProjectContactName = txtProjectContactName.Text.Trim();

                followProject.ProjectStartDate = project.CreatedDate;
                followProject.WarehouseID = 1;

                SQLHelper<FollowProjectBaseModel>.Insert(followProject);
            }
            else
            {
                foreach (FollowProjectBaseModel item in followProjectBase)
                {
                    followProject = item;
                    followProject.ProjectID = project.ID;

                    // dự kiến
                    followProject.ExpectedPlanDate = TextUtils.ToDate5(dtpExpectedPlanDate.EditValue);
                    followProject.ExpectedQuotationDate = TextUtils.ToDate5(dtpExpectedQuotationDate.EditValue);
                    followProject.ExpectedPODate = TextUtils.ToDate4(dtpExpectedPODate.EditValue);
                    followProject.ExpectedProjectEndDate = TextUtils.ToDate4(dtpExpectedProjectEndDate.EditValue);

                    // thực tế
                    followProject.RealityPlanDate = TextUtils.ToDate4(dtpRealityPlanDate.EditValue);
                    followProject.RealityQuotationDate = TextUtils.ToDate4(dtpRealityQuotationDate.EditValue);
                    followProject.RealityPODate = TextUtils.ToDate4(dtpRealityPODate.EditValue);
                    followProject.RealityProjectEndDate = TextUtils.ToDate4(dtpRealityProjectEndDate.EditValue);

                    followProject.FirmBaseID = TextUtils.ToInt(cboFirmBase.EditValue);
                    followProject.ProjectTypeBaseID = TextUtils.ToInt(cboProjectTypeBase.EditValue);
                    followProject.ProjectContactName = txtProjectContactName.Text.Trim();

                    followProject.ProjectStartDate = project.CreatedDate;
                    if (followProject.ID > 0)
                    {
                        SQLHelper<FollowProjectBaseModel>.Update(followProject);
                    }
                    else
                    {
                        followProject.WarehouseID = 1;
                        SQLHelper<FollowProjectBaseModel>.Insert(followProject);
                    }
                }

                //for (int i = 0; i < followProjectBase.Count; i++)
                //{

                //    int followProjectBaseID = TextUtils.ToInt(followProjectBase[i].ID);
                //    //FollowProjectBaseModel followModel = new FollowProjectBaseModel();

                //    if (followProjectBaseID > 0)
                //    {
                //        followProject = SQLHelper<FollowProjectBaseModel>.FindByID(followProjectBaseID);
                //    }

                //}
            }




        }

        private void AddDataToProjectTypeLink(TreeListNodes nodes)
        {
            int selected = 0;
            if (nodes == null || nodes.Count <= 0)
                return;

            foreach (TreeListNode node in nodes)
            {
                if (TextUtils.ToBoolean(tlProjectType.GetRowCellValue(node, colSelected)) == true)
                {
                    selected += 1;
                    //txtSeleted.Text = selected.ToString();
                }

                long id = TextUtils.ToInt64(tlProjectType.GetRowCellValue(node, colProjectTypeLinkID));
                ProjectTypeLinkModel projectTypeLinkModel = new ProjectTypeLinkModel();
                if (id > 0)
                {
                    projectTypeLinkModel = (ProjectTypeLinkModel)ProjectTypeLinkBO.Instance.FindByPK(id);
                }
                projectTypeLinkModel.ProjectID = project.ID;
                projectTypeLinkModel.LeaderID = TextUtils.ToInt(tlProjectType.GetRowCellValue(node, colLeader));
                projectTypeLinkModel.ProjectTypeID = TextUtils.ToInt(tlProjectType.GetRowCellValue(node, colProjectTypeID));
                projectTypeLinkModel.Selected = TextUtils.ToBoolean(tlProjectType.GetRowCellValue(node, colSelected));

                if (projectTypeLinkModel.ID > 0)
                {
                    ProjectTypeLinkBO.Instance.Update(projectTypeLinkModel);

                }
                else
                {
                    ProjectTypeLinkBO.Instance.Insert(projectTypeLinkModel);

                }

                AddDataToProjectTypeLink(node.Nodes);

            }
        }
        /// <summary>
        /// hàm ckeck lỗi
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            List<TreeListNode> listNodes = tlProjectType.GetNodeList();
            int value = 0;
            foreach (TreeListNode item in listNodes)
            {
                int select = TextUtils.ToInt(tlProjectType.GetRowCellValue(item, colSelected));
                if (select == 1)
                {
                    value += 1;
                }
            }
            DataTable dt;


            if (project.ID > 0)
            {
                dt = TextUtils.Select("select top 1 ProjectCode from Project where ProjectCode = '" + txtProjectCode.Text.Trim() + "' and ID <> " + project.ID);
            }
            else
            {
                dt = TextUtils.Select("select top 1 ProjectCode from Project where ProjectCode = '" + txtProjectCode.Text.Trim() + "'");
            }
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show($"Mã dự án [{txtProjectCode.Text.Trim()}] đã tồn tại, vui lòng kiểm tra lại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtProjectCode.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập Mã dự án !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtProjectName.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập Tên dự án !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (TextUtils.ToInt(cbCustomer.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (TextUtils.ToInt(cbUserSale.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Người phụ trách(Sale)!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (TextUtils.ToInt(cbUserTechnical.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Người phụ trách (Technical)!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (TextUtils.ToInt(cbPM.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn PM!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (TextUtils.ToInt(cboEndUser.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn End User!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (!TextUtils.ToDate4(dtpExpectedPlanDate.EditValue).HasValue)
            {
                MessageBox.Show("Vui lòng nhập Ngày gửi phương án!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (!TextUtils.ToDate4(dtpExpectedQuotationDate.EditValue).HasValue)
            {
                MessageBox.Show("Vui lòng nhập Ngày gửi báo giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            //if (!string.IsNullOrEmpty(TextUtils.ToString(dtpPlanDE.EditValue)) && !string.IsNullOrEmpty(TextUtils.ToString(dtpPlanDS.EditValue)))
            //{
            //    DateTime planDS = (DateTime)dtpPlanDS.EditValue;
            //    DateTime planDE = (DateTime)dtpPlanDE.EditValue;
            //    TimeSpan timeSpan = planDE - planDS;
            //    if (timeSpan.Days < 0)
            //    {
            //        MessageBox.Show("Ngày bắt đầu dự kiến không được nhỏ hơn ngày kết thúc dự kiến.\nVui lòng kiểm tra lại !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //        return false;
            //    }

            //}

            int type = cboTypeProject.SelectedIndex;

            if (value == 0 && type <= 1)
            {
                MessageBox.Show("Vui lòng chọn kiểu dự án !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }


            if (TextUtils.ToDecimal(txtPrio.Text.Trim()) < 0)
            {
                MessageBox.Show("Vui lòng nhập Mức ưu tiên!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }


            int projectStatusNew = TextUtils.ToInt(cboStatus.EditValue);
            if (!dateStatusLog.HasValue && projectStatusOld != projectStatusNew && project.ID > 0)
            {
                //int x = (Screen.PrimaryScreen.Bounds.Width / 2) - (flyoutPanel1.Width / 2);
                //int y = (Screen.PrimaryScreen.Bounds.Height / 2) - (flyoutPanel1.Height / 2) - 300;
                //flyoutPanel1.Options.Location = new System.Drawing.Point(x, y);
                flyoutPanel1.ShowPopup();

                MessageBox.Show($"Vui lòng chọn {label42.Text}!", "Thông báo");
                return false;
            }


            return true;
        }

        /// <summary>
        /// Check Seleted Project Type
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        private bool CheckSelectedProjectType(int selected)
        {
            if (selected == 0)
            {
                MessageBox.Show("Vui lòng chọn người phụ trách !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
        void loadProjectCOde()
        {
            btnEdit.Visible = true;
            DataRow[] rows;
            if (dtCustomer.Rows.Count <= 0) return;
            if (cbCustomer.Text.Trim() == "") return;
            rows = dtCustomer.Select($"ID ={cbCustomer.EditValue}");
            if (TextUtils.ToString(rows[0]["CustomerShortName"]) == "")
            {
                MessageBox.Show("Khách hàng đang không có tên kí hiệu. Xin vui lòng thêm thông tin tên kí hiệu!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtProjectCode.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
                cbContact.EditValue = 0;
                cbContact.Enabled = false;
                return;
            }

            //string year = TextUtils.ToString(DateTime.Now.Year).Substring(2, 2);
            //string customerShortName = TextUtils.ToString(rows[0]["CustomerShortName"]);
            //DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetProjectCode", new string[] { "@CustomerShortName", "@year" }, new object[] { customerShortName, year });
            //txtProjectCode.Text = TextUtils.ToString(dataSet.Tables[0].Rows[0]["NvarcharValue"]);




            if (rows.Length > 0)
            {
                cbContact.Enabled = true;
                int number = 0;
                string year = TextUtils.ToString(DateTime.Now.Year).Substring(2, 2);
                //string projectCode = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT TOP 1 ProjectCode FROM [dbo].[Project] Where Year(CreatedDate) = {DateTime.Now.Year} AND ProjectCode Like '%{TextUtils.ToString(rows[0]["CustomerShortName"])}.%' ORDER BY ProjectCode desc"));

                string prefixCode = TextUtils.ToString(rows[0]["CustomerShortName"]);
                if (cboTypeProject.SelectedIndex == 2) prefixCode = $"TM.{prefixCode}";
                else if (cboTypeProject.SelectedIndex == 3) prefixCode = $"F.{prefixCode}";


                var exp1 = new Expression("Year(CreatedDate)", DateTime.Now.Year);
                var exp2 = new Expression("ProjectCode", $"{prefixCode}.", "LIKE PREFIX");
                var listCodes = SQLHelper<ProjectModel>.FindByExpression(exp1.And(exp2))
                                                        .Select(x => new
                                                        {
                                                            STT = TextUtils.ToInt(x.ProjectCode.Split('.')[x.ProjectCode.Split('.').Length - 1]),
                                                            ProjectCode = x.ProjectCode,
                                                            CustomerID = x.CustomerID
                                                        }).OrderByDescending(x => x.STT).ToList();

                string projectCode = "";
                if (listCodes.Count > 0)
                {
                    projectCode = TextUtils.ToString(listCodes.FirstOrDefault().ProjectCode);
                }

                if (project.ID == 0)
                {
                    if (projectCode == "")
                    {
                        //txtProjectCode.Text = TextUtils.ToString(rows[0]["CustomerShortName"]) + "." + year + ".001";
                        txtProjectCode.Text = prefixCode + "." + year + ".001";
                        //return;
                    }
                    else
                    {
                        string count = "";
                        if (!projectCode.Contains("."))
                        {
                            count = TextUtils.ToString(TextUtils.ToInt(projectCode.Substring(projectCode.Length - 2)) + 1);
                        }
                        else
                        {
                            string[] arrCode = projectCode.Split('.');
                            //count = TextUtils.ToString(TextUtils.ToInt(arrCode[2]) + 1);
                            count = TextUtils.ToString(TextUtils.ToInt(arrCode[arrCode.Length - 1]) + 1);
                        }
                        for (int j = 0; count.Length < 3; j++)
                        {
                            count = "0" + count;
                        }
                        //txtProjectCode.Text = TextUtils.ToString(rows[0]["CustomerShortName"]) + "." + year + "." + count;
                        txtProjectCode.Text = prefixCode + "." + year + "." + count;
                    }

                    //if (cboTypeProject.SelectedIndex == 2)
                    //{
                    //    txtProjectCode.Text = $"TM.{txtProjectCode.Text}";
                    //}
                    //else if (cboTypeProject.SelectedIndex == 3)
                    //{
                    //    txtProjectCode.Text = $"F.{txtProjectCode.Text}";
                    //}
                }
            }
        }
        //string customer;
        private void cbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (cbCustomer.EditValue == null) return;
            //customer = TextUtils.ToString(dtCustomer.Select($"ID={cbCustomer.EditValue}")[0]["CustomerShortName"]);
            loadProjectCOde();
            if (TextUtils.ToInt(cboEndUser.EditValue) <= 0)
            {
                cboEndUser.EditValue = cbCustomer.EditValue;
            }

        }

        private void cbContact_EditValueChanged(object sender, EventArgs e)
        {
            if (dtcontact.Rows.Count == 0) return;
            DataRow[] row = dtcontact.Select("ID=" + cbContact.EditValue);
            if (row.Length == 0) return;
            txtPhone.Text = TextUtils.ToString(row[0]["ContactPhone"]);
            txtEmail.Text = TextUtils.ToString(row[0]["ContactEmail"]);
        }

        /// <summary>
        /// hàm sự kiện click column trên grvData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            //    if (info.Column != null && info.Column == colAdd)
            //    {
            //        btnAdd_Click(null, null);
            //    }
            //}
        }

        private void txtProjectCode_TextChanged(object sender, EventArgs e)
        {
            //if (grvData.RowCount <= 0) return;
            //string projectCode = txtProjectCode.Text.Trim();
            //int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colGroupFileID));
            //DataRow[] dtrows = dtGroupFile.Select("ID = " + ID);
            //for (int i = 0; i < grvData.RowCount; i++)
            //{
            //    string groupFileCode = TextUtils.ToString(grvData.GetRowCellValue(i, colGroupFileCode));
            //    string fileName = TextUtils.ToString(grvData.GetRowCellValue(i, colFileName));
            //    if (dtrows.Length > 0)
            //    {
            //        string newPath = $"\\" + $"\\192.168.1.2\\ftp\\DuAn\\" + projectCode + "\\" + groupFileCode + "\\" + fileName;
            //        grvData.SetRowCellValue(i, colPathShort, projectCode + "\\" + groupFileCode + "\\" + fileName);
            //        grvData.SetRowCellValue(i, colPathFull, newPath);
            //    }
            //}
        }

        private void btnProjectType_Click(object sender, EventArgs e)
        {
            frmProjectTypeDetail frm = new frmProjectTypeDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectTypeTree();
            }
        }

        private void txtShortName_TextChanged(object sender, EventArgs e)
        {
            //loadProjectCOde();
        }

        private void txtShortName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)Keys.Space;
        }

        /// <summary>
        /// Thêm dòng mới khi click button add Project User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvProjectUser_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvProjectUser.CalcHitInfo(new Point(e.X, e.Y));
                if (info.Column != null && info.Column == colSTT)
                {
                    MyLib.AddNewRow(grdProjectUser, grvProjectUser);
                }
            }
        }


        private void loadProjectUser()
        {
            DataTable dt = TextUtils.GetDataTableFromSP("spGetProjectUser", new string[] { "@ID" }, new object[] { project.ID });
            grdProjectUser.DataSource = dt;
            if (dt.Rows.Count == 0) return;
        }

        /// <summary>
        /// Click button Delete để xóa dòng Project user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteProjectUser_Click(object sender, EventArgs e)
        {
            if (grdProjectUser.DataSource == null) return;
            int strID = TextUtils.ToInt(grvProjectUser.GetFocusedRowCellValue(colIDProjectUser));

            string projectCode = txtProjectCode.Text;
            string userName = TextUtils.ToString(grvProjectUser.GetFocusedRowCellDisplayText(colUser));
            if (projectCode == "" || userName == "")
            {
                if (MessageBox.Show(String.Format($"Bạn có chắc chắn muốn xóa không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    grvProjectUser.DeleteSelectedRows();
                    lstIDDelete.Add(strID);
                }
            }
            else
            {
                if (MessageBox.Show(String.Format($"Bạn có chắc chắn muốn xóa '{userName}' khỏi dự án '{projectCode}' không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    grvProjectUser.DeleteSelectedRows();
                    lstIDDelete.Add(strID);
                }
            }

        }

        private void tlProjectType_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //var id = tlProjectType.GetFocusedRowCellValue(colTlID);
            //TreeListNode listChildNode = tlProjectType.FindNodeByKeyID(id);
            //TreeListNode parent = tlProjectType.FocusedNode.ParentNode;
            //bool select = TextUtils.ToBoolean(e.Node.GetValue(colTLSelected));
            //foreach (TreeListNode node in listChildNode.Nodes)
            //{

            //    if (select)
            //    {
            //        tlProjectType.SetRowCellValue(node, colTLSelected, 1);
            //    }
            //    else
            //    {
            //        tlProjectType.SetRowCellValue(node, colTLSelected, 0);
            //    }

            //}
        }


        private void tlProjectType_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {

        }

        private void tlProjectType_MouseEnter(object sender, EventArgs e)
        {

        }

        private void tlProjectType_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void cboProjectTypeTreeList_EditValueChanged(object sender, EventArgs e)
        {
            //int id = TextUtils.ToInt(cboProjectTypeTreeList.EditValue);
            //foreach (TreeListNode item in tlProjectType.GetNodeList())
            //{
            //    if (TextUtils.ToInt(tlProjectType.GetRowCellValue(item, colProjectTypeID)) == id)
            //    {
            //        tlProjectType.SetRowCellValue(item, colSelected, true);
            //    }
            //    else
            //    {
            //        tlProjectType.SetRowCellValue(item, colSelected, false);
            //    }
            //}
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmProjectDetail_SizeChanged(object sender, EventArgs e)
        {

            //int width = splitContainerControl1.Width;
            //splitContainerControl1.Panel2.MinSize = 600;
            //splitContainerControl1.Panel1.MinSize = width - 600;
        }

        private void btnClosePopup_Click(object sender, EventArgs e)
        {
            ResetdtProjectType();
            cbProjectTypeTreeList.ClosePopup();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboLeaderID_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void chkSelectedProjectType_CheckedChanged(object sender, EventArgs e)
        {
            if (tlProjectType.FocusedNode == null) return;
            var id = tlProjectType.GetFocusedRowCellValue(colTlID);
            TreeListNode listChildNode = tlProjectType.FindNodeByKeyID(id);
            TreeListNode parent = tlProjectType.FocusedNode.ParentNode;
            bool select = TextUtils.ToBoolean(tlProjectType.FocusedNode.GetValue(colTLSelected));
            tlProjectType.SetRowCellValue(tlProjectType.FocusedNode, colTLSelected, select ? 0 : 1);
            if (parent == null)
            {
                foreach (TreeListNode node in tlProjectType.FocusedNode.Nodes)
                    tlProjectType.SetRowCellValue(node, colTLSelected, select ? 0 : 1);
            }
            else
            {
                select = !select;
                if (select)
                {
                    if (parent != null)
                        foreach (TreeListNode item in parent.Nodes)
                        {
                            select = TextUtils.ToBoolean(item.GetValue(colTLSelected));
                            if (!select)
                            {
                                tlProjectType.SetRowCellValue(parent, colTLSelected, 0);
                                return;
                            }
                        }

                    tlProjectType.SetRowCellValue(parent, colTLSelected, 1);
                }
                else
                {
                    if (parent != null)
                        tlProjectType.SetRowCellValue(parent, colTLSelected, 0);
                }
            }
        }

        private void btnLeaderProject_Click(object sender, EventArgs e)
        {
            frmLeaderProject frm = new frmLeaderProject();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadLeader();
            }
        }

        private void btnAddBusinessField_Click(object sender, EventArgs e)
        {
            frmBusinessField frm = new frmBusinessField();
            frm.Text = "LĨNH VỰC DỰ ÁN";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBusinessField();
            }
        }
        void loadBusinessField()
        {
            List<BusinessFieldModel> list = SQLHelper<BusinessFieldModel>.FindAll().OrderByDescending(x => x.STT).ToList();

            cboBusinessField.Properties.ValueMember = "ID";
            cboBusinessField.Properties.DisplayMember = "Name";
            cboBusinessField.Properties.DataSource = list;

            cboBusinessField.EditValue = project.BusinessFieldID;
        }

        private void btnProjectPriority_Click(object sender, EventArgs e)
        {
            frmProjectPriority frm = new frmProjectPriority();
            frm.projectID = project.ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                decimal totalPriority = frm.getTotalPriority();
                txtPrio.Text = totalPriority.ToString();

                listPriorities = frm.listPriorities;
            }

        }

        private void dtpActualDE_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void frmProjectDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmProjectDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnProjectTypeLink_Click(object sender, EventArgs e)
        {
            frmProjectTypeLink frm = new frmProjectTypeLink();
            frm.cboProject.EditValue = project.ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //loadProject();
                //grvMaster.FocusedRowHandle = rowHandle;
                loadProjectTypeTree();
            }
        }

        private void btnAddStatus_Click(object sender, EventArgs e)
        {
            frmProjectAddStatus frm = new frmProjectAddStatus();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadProjectStatus();
            }
        }

        private void cboStatus_EditValueChanged(object sender, EventArgs e)
        {
            //flyoutPanel1.Tag = item.Tag;
            //flyoutPanel1.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Manual;

            ProjectStatusModel projectStatus = (ProjectStatusModel)cboStatus.GetSelectedDataRow() ?? new ProjectStatusModel();
            if (project.ProjectStatus == projectStatus.ID || project.ID <= 0)
            {
                flyoutPanel1.HidePopup();
                return;
            }
            //flyoutPanel1.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.TopLeft;
            //int x = cboStatus.Location.X;
            //int y = cboStatus.Location.Y + cboStatus.Height;
            //flyoutPanel1.Options.Location = new System.Drawing.Point(x, y);
            flyoutPanel1.ShowPopup();
        }

        private void flyoutPanel1_ButtonClick(object sender, FlyoutPanelButtonClickEventArgs e)
        {
            string tag = e.Button.Tag.ToString();
            switch (tag)
            {
                case "btnOK":
                    dateStatusLog = TextUtils.ToDate4(dtpDateStatusLog.EditValue);
                    if (!dateStatusLog.HasValue)
                    {
                        MessageBox.Show($"Vui lòng chọn {label42.Text}!", "Thông báo");
                        break;
                    }


                    flyoutPanel1.HidePopup();
                    dtpDateStatusLog.EditValue = null;

                    //TN.Binh update 03/10/25
                    if (project.ID <= 0) return;
                    FollowProjectBaseModel followProject = SQLHelper<FollowProjectBaseModel>.FindByAttribute("ProjectID", project.ID).OrderByDescending(x => x.ExpectedPlanDate).FirstOrDefault();

                    int statusID = TextUtils.ToInt(cboStatus.EditValue);
                    if (statusID == 6 || statusID == 9 && followProject.RealityProjectEndDate == null)
                    {
                        dtpRealityProjectEndDate.EditValue = dateStatusLog;
                    }
                    //endupdate


                    break;
                case "btnCancel":
                    // . . .
                    (sender as FlyoutPanel).HidePopup();
                    break;
            }
        }

        private void cboTypeProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadProjectCOde();
        }
    }
}
