using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmDailyReportSaleDetail : _Forms
    {
        public int warehouseID = 0;

        int projectStatusOld = 0;
        DateTime? dateStatusLog = null;

        public DailyReportSaleModel dailyReportSaleModel = new DailyReportSaleModel();
        //frmDailyReportSale frm = new frmDailyReportSale(warehouseID);

        public frmDailyReportSaleDetail(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }
        /// <summary>
        /// load dữ liệu lên khi load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDailyReportSaleDetail_Load(object sender, EventArgs e)
        {
            WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByID(warehouseID);
            this.Text += $" - {warehouse.WarehouseCode}";

            loadGroup();
            loadCustomer();
            loadUsers();
            LoadProject();
            loadFirmBase();
            loadProjectTypeBase();
            //CheckAndSetSaveButtonVisibility();
            //SetCurrentUser();

            LoadProjectStatus();
            loadDailyReportSaleDetail();
        }

        private void loadFirmBase()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM FirmBase");
            cbFirmBase.Properties.DisplayMember = "FirmName";
            cbFirmBase.Properties.ValueMember = "ID";
            cbFirmBase.Properties.DataSource = dt;
        }
        private void loadProjectTypeBase()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM ProjectTypeBase");
            cbProjectTypeBase.Properties.DisplayMember = "ProjectTypeName";
            cbProjectTypeBase.Properties.ValueMember = "ID";
            cbProjectTypeBase.Properties.DataSource = dt;
        }

        void loadGroup()
        {
            //DataTable dt = TextUtils.Select($"Select * From MainIndex where ID in (15,16,17,18,19,20,21,29)");

            //List<MainIndexModel> list = SQLHelper<MainIndexModel>.ProcedureToList("spGetMainIndex", new string[] { }, new object[] { });

            // =================================================== PQ.CHien - UPDATE - 03/04/2025=====================================================
            List<MainIndexModel> list = SQLHelper<MainIndexModel>.ProcedureToList("spGetMainIndex", new string[] { "@Type" }, new object[] { 2 });
            cbGroup.Properties.ValueMember = "ID";
            cbGroup.Properties.DisplayMember = "MainIndex";
            cbGroup.Properties.DataSource = list;
        }


        private void LoadProjectStatus()
        {
            //DataTable dt = TextUtils.Select("SELECT * FROM ProjectStatus");
            List<ProjectStatusModel> list = SQLHelper<ProjectStatusModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboProjectStatus.Properties.DisplayMember = "StatusName";
            cboProjectStatus.Properties.ValueMember = "ID";
            cboProjectStatus.Properties.DataSource = list;
        }
        #region Methods
        /// <summary>
        /// load DailyReportSaleDetail
        /// </summary>
        private void loadDailyReportSaleDetail()
        {
            if (dailyReportSaleModel.ID > 0)
            {
                cbUsers.Focus();
                cbUsers.EditValue = dailyReportSaleModel.UserID;
                if (dailyReportSaleModel.DateEnd != null)
                {
                    dtpDateEnd.Value = dailyReportSaleModel.DateEnd.Value;
                }
                if (dailyReportSaleModel.DateStart != null)
                {
                    dtpDateStart.Value = dailyReportSaleModel.DateStart.Value;
                }
                ckbBigAccount.Checked = dailyReportSaleModel.BigAccount;
                cbCustomer.EditValue = dailyReportSaleModel.CustomerID;
                txtContent.Text = dailyReportSaleModel.Content;
                txtResult.Text = dailyReportSaleModel.Result;
                txtProblemBacklog.Text = dailyReportSaleModel.ProblemBacklog;
                txtPlanNext.Text = dailyReportSaleModel.PlanNext;
                txtNote.Text = dailyReportSaleModel.Note;
                cbGroup.EditValue = dailyReportSaleModel.GroupType;
                cbContact.EditValue = dailyReportSaleModel.ContacID;
                cbPart.EditValue = dailyReportSaleModel.EndUser;
                txtProductOfCustomer.Text = dailyReportSaleModel.ProductOfCustomer;
                txtRequestOfCustomer.Text = dailyReportSaleModel.RequestOfCustomer;
                cboProject.EditValue = dailyReportSaleModel.ProjectID;

                cbFirmBase.EditValue = dailyReportSaleModel.FirmBaseID;
                cbProjectTypeBase.EditValue = dailyReportSaleModel.ProjectTypeBaseID;

                chkSaleOpportunity.Checked = dailyReportSaleModel.SaleOpportunity;
            }
            else
            {
                cbUsers.EditValue = Global.UserID;
            }

            btnSave.Enabled = btnSaveNew.Enabled = CheckRole(TextUtils.ToInt(cbUsers.EditValue));
            cbUsers.Enabled = (Global.IsAdminSale || Global.IsAdmin);
        }

        /// <summary>
        /// load khách hàng
        /// </summary>
        private void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM Customer where IsDeleted <> 1");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }


        /// <summary>
        /// load người phụ trách
        /// </summary>
        /// 
        private void SetCurrentUser()
        {
            // Lấy thông tin người đăng nhập

            int salePersonID = Global.UserID;


            cbUsers.EditValue = salePersonID;


        }
        public void loadUsers()
        {
            //DataTable dt = TextUtils.Select("SELECT * FROM Users");
            //List<EmployeeModel> list = SQLHelper<EmployeeModel>.ProcedureToList("spGetEmployee", new string[] { "@Status" }, new object[] { 0 });
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cbUsers.Properties.DisplayMember = "FullName";
            cbUsers.Properties.ValueMember = "UserID";
            cbUsers.Properties.DataSource = dt;

            cbUsers.EditValue = Global.UserID;
        }

        /// <summary>
        /// Load danh sách dự án lên combo
        /// </summary>
        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DataSource = list;
        }

        /// <summary>
        /// Update follow dự án
        /// </summary>
        void UpdateFollowProject()
        {
            ProjectModel project = (ProjectModel)cboProject.GetSelectedDataRow();
            if (project != null)
            {
                //FollowProjectBaseModel follow = new FollowProjectBaseModel()
                //{
                //    ProjectID = project.ID,
                //    UserID = TextUtils.ToInt(cbUsers.EditValue),
                //    CustomerBaseID = TextUtils.ToInt(cbCustomer.EditValue),
                //    EndUserID = project.EndUser,
                //    ProjectStatusBaseID = project.ProjectStatus,

                //    ImplementationDate = dtpDateStart.Value,
                //    ExpectedDate = dtpDateEnd.Value,
                //    WorkDone = txtContent.Text.Trim(),
                //    Results = txtResult.Text.Trim(),
                //    ProblemBacklog = txtProblemBacklog.Text.Trim(),
                //    WorkWillDo = txtPlanNext.Text.Trim(),
                //};

                FollowProjectBaseModel follow = SQLHelper<FollowProjectBaseModel>.FindByAttribute("ProjectID", project.ID).OrderByDescending(x => x.ID).FirstOrDefault();
                follow = follow ?? new FollowProjectBaseModel();

                follow.ProjectID = project.ID;
                follow.CustomerBaseID = TextUtils.ToInt(cbCustomer.EditValue);
                follow.EndUserID = project.EndUser;
                follow.ProjectStatusBaseID = project.ProjectStatus;
                follow.ProjectStartDate = project.CreatedDate;
                follow.WarehouseID = warehouseID;

                // Em sửa ở đây nhớ!
                follow.FirmBaseID = dailyReportSaleModel.FirmBaseID;
                follow.ProjectTypeBaseID = dailyReportSaleModel.ProjectTypeBaseID;

                follow.ProjectStatusBaseID = TextUtils.ToInt(cboProjectStatus.EditValue);

                if (follow.ID <= 0)
                {
                    follow.ID = SQLHelper<FollowProjectBaseModel>.Insert(follow).ID;
                }
                else
                {
                    SQLHelper<FollowProjectBaseModel>.Update(follow);
                }

                //if (follow == null || follow.ID <= 0)
                //{
                //    follow = new FollowProjectBaseModel();
                //    follow.ProjectID = project.ID;
                //    follow.CustomerBaseID = TextUtils.ToInt(cbCustomer.EditValue);
                //    follow.EndUserID = project.EndUser;
                //    follow.ProjectStatusBaseID = project.ProjectStatus;
                //    follow.ProjectStartDate = project.CreatedDate;
                //    follow.WarehouseID = warehouseID;

                //    // Em sửa ở đây nhớ!
                //    follow.FirmBaseID = dailyReportSaleModel.FirmBaseID;
                //    follow.ProjectTypeBaseID = dailyReportSaleModel.ProjectTypeBaseID;

                //    follow.ProjectStatusBaseID = TextUtils.ToInt(cboProjectStatus.EditValue);
                //    follow.ID = (int)FollowProjectBaseBO.Instance.Insert(follow);
                //}
                FollowProjectBaseDetailModel detail = new FollowProjectBaseDetailModel()
                {
                    FollowProjectBaseID = follow.ID,
                    ProjectID = project.ID,
                    UserID = TextUtils.ToInt(cbUsers.EditValue),
                    ImplementationDate = dtpDateStart.Value,
                    ExpectedDate = dtpDateEnd.Value,
                    WorkDone = txtContent.Text.Trim(),
                    WorkWillDo = txtPlanNext.Text.Trim(),
                    Results = txtResult.Text.Trim(),
                    ProblemBacklog = txtProblemBacklog.Text.Trim(),
                };

                SQLHelper<FollowProjectBaseDetailModel>.Insert(detail);
            }
        }


        void UpdateProject()
        {
            int id = TextUtils.ToInt(cboProject.EditValue);
            ProjectModel project = SQLHelper<ProjectModel>.FindByID(id);
            if (project.ID > 0)
            {
                project.ProjectStatus = TextUtils.ToInt(cboProjectStatus.EditValue);
                SQLHelper<ProjectModel>.Update(project);


                if (projectStatusOld != project.ProjectStatus)
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
            }
        }


        void LoadProductCustomer()
        {
            int employeeID = TextUtils.ToInt(cbUsers.EditValue);
            int projectID = TextUtils.ToInt(cboProject.EditValue);

            var exp1 = new Expression("UserID", employeeID);
            var exp2 = new Expression("ProjectID", projectID);

            DailyReportSaleModel reportSale = SQLHelper<DailyReportSaleModel>.FindByExpression(exp1.And(exp2)).OrderByDescending(x => x.ID).FirstOrDefault();
            reportSale = reportSale ?? new DailyReportSaleModel();
            txtProductOfCustomer.Text = reportSale.ProductOfCustomer;

            FollowProjectBaseModel followProject = SQLHelper<FollowProjectBaseModel>.FindByExpression(exp2).OrderByDescending(x => x.ID).FirstOrDefault();
            followProject = followProject ?? new FollowProjectBaseModel();
            cbFirmBase.EditValue = followProject.FirmBaseID;
            cbProjectTypeBase.EditValue = followProject.ProjectTypeBaseID;
        }

        #endregion

        #region Buttons Events
        /// <summary>
        /// click button save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        //private void btnSave_Click(object sender, EventArgs e)
        //{

        //    if (saveData())this.DialogResult = DialogResult.OK;
        //}

        private void CheckAndSetSaveButtonVisibility()
        {
            int salePersonID = TextUtils.ToInt(cbUsers.EditValue);

            // kiểm tra quyền 
            if (!CheckRole(salePersonID))
            {
                btnSave.Visible = false;
                btnSaveNew.Visible = false;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
                //int salePersonID = TextUtils.ToInt(cbUsers.EditValue);
                //if (frm.CheckRole(salePersonID))
                //{
                //    //MessageBox.Show("Lưu thông tin thành công!");
                //    this.DialogResult = DialogResult.OK;
                //}
                //else
                //{
                //    MessageBox.Show("Bạn không có quyền sửa báo cáo của nhân viên khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
        }



        /// <summary>
        /// click button sve new
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                //MessageBox.Show("Cất thành công !", "Thông báo");
                ckbBigAccount.Checked = false;
                txtContent.Clear();
                dailyReportSaleModel = new DailyReportSaleModel();
            }

            //int salePersonID = TextUtils.ToInt(cbUsers.EditValue);
            //if (frm.CheckRole(salePersonID))
            //{
            //    if (saveData())
            //    {
            //        //MessageBox.Show("Cất thành công !", "Thông báo");
            //        ckbBigAccount.Checked = false;
            //        txtContent.Clear();
            //        dailyReportSaleModel = new DailyReportSaleModel();
            //    }

            //    //cbCustomer.Text = "";
            //    //cbUsers.Text = "";
            //    //cbContact.Text = "";
            //    //txtContent.Clear();
            //    //txtProblemBacklog.Clear();
            //    //txtPlanNext.Clear();
            //    //txtResult.Clear();
            //    //txtNote.Clear();
            //}
            //else
            //{
            //    MessageBox.Show("Bạn không có quyền chỉnh sửa thông tin do bạn không phải Sale phụ trách!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        #endregion
        /// <summary>
        /// hàm save
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {
            this.Focus();
            if (!ValidateForm()) return false;
            dailyReportSaleModel.UserID = TextUtils.ToInt(cbUsers.EditValue);
            dailyReportSaleModel.DateEnd = dtpDateEnd.Value;
            dailyReportSaleModel.BigAccount = ckbBigAccount.Checked;
            if (ckbBigAccount.Checked == true)
            {
                dailyReportSaleModel.BigAccount = true;
            }
            else
            {
                dailyReportSaleModel.BigAccount = false;
            }
            dailyReportSaleModel.CustomerID = TextUtils.ToInt(cbCustomer.EditValue);
            dailyReportSaleModel.ContacID = TextUtils.ToInt(cbContact.EditValue);
            dailyReportSaleModel.Content = txtContent.Text.Trim();
            dailyReportSaleModel.Result = txtResult.Text.Trim();
            dailyReportSaleModel.ProblemBacklog = txtProblemBacklog.Text.Trim();
            dailyReportSaleModel.PlanNext = txtPlanNext.Text.Trim();
            dailyReportSaleModel.Note = txtNote.Text.Trim();
            dailyReportSaleModel.GroupType = TextUtils.ToInt(cbGroup.EditValue);
            dailyReportSaleModel.Month = dtpDateEnd.Value.Month;
            dailyReportSaleModel.Year = DateTime.Now.Year;
            dailyReportSaleModel.EndUser = TextUtils.ToInt(cbPart.EditValue);
            dailyReportSaleModel.DateStart = dtpDateStart.Value;
            dailyReportSaleModel.RequestOfCustomer = txtRequestOfCustomer.Text.Trim();
            dailyReportSaleModel.ProductOfCustomer = txtProductOfCustomer.Text.Trim();

            dailyReportSaleModel.ProjectID = TextUtils.ToInt(cboProject.EditValue);

            // Em sửa ở đây nhớ!
            dailyReportSaleModel.FirmBaseID = TextUtils.ToInt(cbFirmBase.EditValue);
            dailyReportSaleModel.ProjectTypeBaseID = TextUtils.ToInt(cbProjectTypeBase.EditValue);

            dailyReportSaleModel.SaleOpportunity = chkSaleOpportunity.Checked;
            dailyReportSaleModel.WarehouseID = warehouseID;

            if (dailyReportSaleModel.ID > 0)
            {
                DailyReportSaleBO.Instance.Update(dailyReportSaleModel);
            }
            else
            {
                DailyReportSaleBO.Instance.Insert(dailyReportSaleModel);
            }

            UpdateFollowProject();
            UpdateProject();
            return true;
        }

        private void frmDailyReportSaleDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// check lỗi
        /// </summary>
        /// <returns></returns>
        bool ValidateForm()
        {

            if (TextUtils.ToInt(cbUsers.EditValue) <= 0)
            {
                MessageBox.Show("Xin vui lòng nhập Người phụ trách.", TextUtils.Caption);
                return false;
            }

            if (TextUtils.ToInt(cbCustomer.EditValue) <= 0)
            {
                MessageBox.Show("Xin vui lòng nhập Khách hàng.", TextUtils.Caption);
                return false;
            }

            if (TextUtils.ToInt(cbGroup.EditValue) <= 0)
            {
                MessageBox.Show("Xin vui lòng nhập Loại nhóm!", TextUtils.Caption);
                return false;
            }

            if (string.IsNullOrEmpty(txtContent.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Việc đã làm!", TextUtils.Caption);
                return false;
            }

            if (string.IsNullOrEmpty(txtResult.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Kết quả!", TextUtils.Caption);
                return false;
            }

            if (string.IsNullOrEmpty(txtPlanNext.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Kế hoạch ngày tiếp theo!", TextUtils.Caption);
                return false;
            }

            if (string.IsNullOrEmpty(txtProductOfCustomer.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Sản phẩm của KH!", TextUtils.Caption);
                return false;
            }

            if (TextUtils.ToInt(cbFirmBase.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Hãng!", TextUtils.Caption);
                return false;
            }

            if (TextUtils.ToInt(cbProjectTypeBase.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Loại dự án!", TextUtils.Caption);
                return false;
            }

            if (TextUtils.ToInt(cbContact.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Liên hệ!", TextUtils.Caption);
                return false;
            }

            if (TextUtils.ToInt(cboProjectStatus.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Trạng thái dự án!", TextUtils.Caption);
                return false;
            }


            int projectStatusNew = TextUtils.ToInt(cboProjectStatus.EditValue);
            if (!dateStatusLog.HasValue && projectStatusOld != projectStatusNew)
            {
                flyoutPanel1.ShowPopup();

                MessageBox.Show($"Vui lòng chọn {label27.Text}!", "Thông báo");
                return false;
            }

            //DataTable dt;
            //if (dailyReportSaleModel.ID > 0)
            //{
            //    dt = TextUtils.Select("select top 1 CustomerID from DailyReportSale where CustomerID = '" + cbCustomer.EditValue + "' and ID <> " + dailyReportSaleModel.ID);
            //}
            //else
            //{
            //    dt = TextUtils.Select("select top 1 CustomerID from DailyReportSale where CustomerID = '" + cbCustomer.EditValue + "'");
            //}
            return true;
        }
        void loadContact()
        {
            DataTable dt = TextUtils.Select($"SELECT ContactPhone,ContactEmail,ContactName,ID FROM dbo.CustomerContact where CustomerID={cbCustomer.EditValue}");
            cbContact.Properties.DisplayMember = "ContactName";
            cbContact.Properties.ValueMember = "ID";
            cbContact.Properties.DataSource = dt;
        }
        private void cbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            string a = "";
            loadContact();
            loadCbPart();
        }
        void loadCbPart()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetCustomerPart", "A", new string[] { "@ID" }, new object[] { TextUtils.ToInt(cbCustomer.EditValue) });
            cbPart.Properties.DisplayMember = "PartCode";
            cbPart.Properties.ValueMember = "ID";
            cbPart.Properties.DataSource = dt;
        }
        private void btnPart_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(cbCustomer.EditValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmCustomerPart frm = new frmCustomerPart();
            frm.IDCutomer = TextUtils.ToInt(cbCustomer.EditValue);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCbPart();
            }
        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(cbCustomer.EditValue);
            if (ID == 0) return;
            CustomerModel model = (CustomerModel)CustomerBO.Instance.FindByPK(ID);
            //frmCustomerDetail frm = new frmCustomerDetail();
            frmCustomerDetailNew frm = new frmCustomerDetailNew(warehouseID);
            frm.customer = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadContact();
            }
        }

        private void frmDailyReportSaleDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            ProjectModel projectSelected = (ProjectModel)cboProject.GetSelectedDataRow();
            if (projectSelected != null)
            {
                cbCustomer.EditValue = projectSelected.CustomerID;
                cboProjectStatus.EditValue = projectSelected.ProjectStatus;

                projectStatusOld = projectSelected.ProjectStatus;
            }

            LoadProductCustomer();
        }

        bool CheckRole(int salePersonID)
        {
            if (Global.IsAdmin || Global.IsAdminSale)
            {
                return true;
            }

            return salePersonID == Global.UserID;
        }

        private void btnFirmBase_Click(object sender, EventArgs e)
        {
            frmFirmBaseDetail frm = new frmFirmBaseDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadFirmBase();
            }
        }

        private void btnProjectTypeBase_Click(object sender, EventArgs e)
        {
            frmProjectTypeBaseDetail frm = new frmProjectTypeBaseDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectTypeBase();
            }
        }

        private void cbUsers_EditValueChanged(object sender, EventArgs e)
        {
            LoadProductCustomer();
        }

        private void btnAddProjectStatus_Click(object sender, EventArgs e)
        {
            frmProjectAddStatus frm = new frmProjectAddStatus();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadProjectStatus();
            }
        }

        private void cboProjectStatus_EditValueChanged(object sender, EventArgs e)
        {
            ProjectModel projectSelected = (ProjectModel)cboProject.GetSelectedDataRow() ?? new ProjectModel();
            ProjectStatusModel projectStatus = (ProjectStatusModel)cboProjectStatus.GetSelectedDataRow() ?? new ProjectStatusModel();
            if (projectSelected.ProjectStatus == projectStatus.ID || projectStatus.ID <= 0 || projectSelected.ID <= 0)
            {
                flyoutPanel1.HidePopup();
                return;
            }
            flyoutPanel1.ShowPopup();
        }


        private void flyoutPanel1_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            string tag = e.Button.Tag.ToString();
            switch (tag)
            {
                case "btnOK":
                    dateStatusLog = TextUtils.ToDate4(dtpDateStatusLog.EditValue);
                    if (!dateStatusLog.HasValue)
                    {
                        MessageBox.Show($"Vui lòng chọn {label27.Text}!", "Thông báo");
                        break;
                    }

                    flyoutPanel1.HidePopup();
                    dtpDateStatusLog.EditValue = null;

                    break;
                case "btnCancel":
                    // . . .
                    (sender as FlyoutPanel).HidePopup();
                    break;
            }
        }
    }
}
