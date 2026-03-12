using BMS.Business;
using BMS.Model;
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
    public partial class frmFollowProjectBaseDetail : _Forms
    {
        int warehouseID = 0;
        public FollowProjectBaseModel followProjectBase = new FollowProjectBaseModel();
        public ProjectModel project = new ProjectModel();

        int projectStatusOld = 0;
        DateTime? dateStatusLog = null;
        public frmFollowProjectBaseDetail(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }
        /// <summary>
        /// load dữ liệu lên khi load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFollowProjectBaseDetail_Load(object sender, EventArgs e)
        {
            // cbUser.EditValue = Global.UserID;

            // phân quyền admin
            //DataTable dt = TextUtils.Select($"Select * From [GroupSalesUser] WHERE UserID = {cbUser.EditValue} AND (SaleUserTypeID = 1 OR SaleUserTypeID = 6 OR SaleUserTypeID = 7 OR SaleUserTypeID = 8)");
            //if (dt.Rows.Count > 0) cbUser.Enabled = true;
            //else cbUser.Enabled = false;

            WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByID(warehouseID);
            this.Text += $" - {warehouse.WarehouseCode}";

            loadProject();
            loadCustomer();
            loadUser();
            loadProjectStatus();
            loadProjectTypeBase();
            loadFirmBase();
            LoadPM();
            loadFollowProjectBaseDetail();

            splitContainerControl1.SplitterPosition = splitContainerControl1.Width / 2;
        }

        #region Methods
        /// <summary>
        /// load dự án
        /// </summary>
        /// 
        private void loadProject()
        {
            DataTable dt = TextUtils.Select("SELECT *,ProjectCode +'_'+ProjectName as ProjectFullName FROM Project");
            cbProject.Properties.DisplayMember = "ProjectFullName";
            cbProject.Properties.ValueMember = "ID";
            cbProject.Properties.DataSource = dt;

            //cboEndUser.Properties.DisplayMember = "ProjectName";
            //cboEndUser.Properties.ValueMember = "ID";
            //cboEndUser.Properties.DataSource = dt;
        }


        void LoadPM()
        {
            DataTable dt = TextUtils.GetTable("spGetEmployeeForProject");
            cboPM.Properties.DataSource = dt;
            cboPM.Properties.DisplayMember = "FullName";
            cboPM.Properties.ValueMember = "EmployeeID";
        }

        /// <summary>
        /// load khách hàng
        /// </summary>
        private void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM Customer WHERE IsDeleted <> 1");
            cbCustomer.Properties.DisplayMember = cboEndUser.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = cboEndUser.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = cboEndUser.Properties.DataSource = dt;
        }

        /// <summary>
        /// load sale phụ trách
        /// </summary>
        private void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM Users");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;
        }

        /// <summary>
        /// load trạng thái 
        /// </summary>
        private void loadProjectStatus()
        {
            //DataTable dt = TextUtils.Select("SELECT * FROM ProjectStatus");
            List<ProjectStatusModel> list = SQLHelper<ProjectStatusModel>.FindAll().OrderBy(x => x.STT).ToList();
            cbProjectStatus.Properties.DisplayMember = "StatusName";
            cbProjectStatus.Properties.ValueMember = "ID";
            cbProjectStatus.Properties.DataSource = list;
        }

        /// <summary>
        /// load hãng
        /// </summary>
        private void loadFirmBase()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM FirmBase");
            cbFirmBase.Properties.DisplayMember = "FirmName";
            cbFirmBase.Properties.ValueMember = "ID";
            cbFirmBase.Properties.DataSource = dt;
        }

        /// <summary>
        /// load trạng thái 
        /// </summary>
        private void loadProjectTypeBase()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM ProjectTypeBase");
            cbProjectTypeBase.Properties.DisplayMember = "ProjectTypeName";
            cbProjectTypeBase.Properties.ValueMember = "ID";
            cbProjectTypeBase.Properties.DataSource = dt;
        }

        private void loadFollowProjectBaseDetail()
        {
            if (followProjectBase.ID > 0)
            {
                //follow
                cbProject.EditValue = followProjectBase.ProjectID;
                // cbUser.EditValue = project.UserID;
                //cbProjectStatus.EditValue = followProjectBase.ProjectStatusBaseID;
                cbCustomer.EditValue = followProjectBase.CustomerBaseID;
                cbFirmBase.EditValue = followProjectBase.FirmBaseID;
                cbProjectTypeBase.EditValue = followProjectBase.ProjectTypeBaseID;
                cboEndUser.EditValue = followProjectBase.EndUserID;

                txtProjectStartDate.EditValue = TextUtils.ToDate4(followProjectBase.ProjectStartDate);
                //txtImplementationDate.EditValue = TextUtils.ToDate4(followProjectBase.ImplementationDate);
                //txtExpectedDate.EditValue = TextUtils.ToDate4(followProjectBase.ExpectedDate);
                txtWorkDone.Text = followProjectBase.WorkDone;
                txtWorkWillDo.Text = followProjectBase.WorkWillDo;
                txtPossibilityPO.Text = followProjectBase.PossibilityPO;
                // dự kiến
                txtExpectedPlanDate.EditValue = TextUtils.ToDate4(followProjectBase.ExpectedPlanDate);
                txtExpectedQuotationDate.EditValue = TextUtils.ToDate4(followProjectBase.ExpectedQuotationDate);
                txtExpectedPODate.EditValue = TextUtils.ToDate4(followProjectBase.ExpectedPODate);
                txtExpectedProjectEndDate.EditValue = TextUtils.ToDate4(followProjectBase.ExpectedProjectEndDate);
                // thực tế
                txtRealityPlanDate.EditValue = TextUtils.ToDate4(followProjectBase.RealityPlanDate);
                txtRealityQuotationDate.EditValue = TextUtils.ToDate4(followProjectBase.RealityQuotationDate);
                txtRealityPODate.EditValue = TextUtils.ToDate4(followProjectBase.RealityPODate);
                txtRealityProjectEndDate.EditValue = TextUtils.ToDate4(followProjectBase.RealityProjectEndDate);
                //follow 
                txtTotalWithoutVAT.EditValue = followProjectBase.TotalWithoutVAT;
                txtProjectContactName.Text = followProjectBase.ProjectContactName;
                txtNote.Text = followProjectBase.Note;

                //PM
                dtpDateDonePM.EditValue = followProjectBase.DateDonePM;
                dtpDateWillDoPM.EditValue = followProjectBase.DateWillDoPM;
                txtWorkDonePM.Text = followProjectBase.WorkDonePM;
                txtWorkWillDoPM.Text = followProjectBase.WorkWillDoPM;

                //Sale
                dtpDateDoneSale.EditValue = followProjectBase.DateDoneSale;
                dtpDateWillDoSale.EditValue = followProjectBase.DateWillDoSale;

                LoadDetail();
            }
        }

        void LoadDetail()
        {
            //DateTime dateStart = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            //DateTime dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            int projectID = TextUtils.ToInt(cbProject.EditValue);

            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetFollowProjectBaseDetail"
                                                            , new string[] { "@FollowProjectBaseID", "@ProjectID" }
                                                            , new object[] { followProjectBase.ID, projectID });
            DataTable dtSale = dataSet.Tables[0];
            DataTable dtPM = dataSet.Tables[1];

            grdDataSale.DataSource = dtSale;
            grdDataPM.DataSource = dtPM;
        }

        #endregion
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData()) this.DialogResult = DialogResult.OK;
        }
        bool saveData()
        {
            projectStatusOld = followProjectBase.ProjectStatusBaseID;
            if (!ValidateForm()) return false;
            try
            {
                followProjectBase.ProjectID = TextUtils.ToInt(cbProject.EditValue);
                followProjectBase.UserID = TextUtils.ToInt(cbUser.EditValue);
                followProjectBase.ProjectStatusBaseID = TextUtils.ToInt(cbProjectStatus.EditValue);

                //project.ProjectStatus = TextUtils.ToInt(cbProjectStatus.EditValue);

                followProjectBase.CustomerBaseID = TextUtils.ToInt(cbCustomer.EditValue);
                followProjectBase.FirmBaseID = TextUtils.ToInt(cbFirmBase.EditValue);
                followProjectBase.ProjectTypeBaseID = TextUtils.ToInt(cbProjectTypeBase.EditValue);
                followProjectBase.EndUserID = TextUtils.ToInt(cboEndUser.EditValue);
                followProjectBase.ProjectStartDate = TextUtils.ToDate4(txtProjectStartDate.EditValue);

                //followProjectBase.ImplementationDate = TextUtils.ToDate4(txtImplementationDate.EditValue);
                //followProjectBase.ExpectedDate = TextUtils.ToDate4(txtExpectedDate.EditValue);

                followProjectBase.PossibilityPO = txtPossibilityPO.Text.Trim();

                // dự kiến
                followProjectBase.ExpectedPlanDate = TextUtils.ToDate4(txtExpectedPlanDate.EditValue);
                followProjectBase.ExpectedQuotationDate = TextUtils.ToDate4(txtExpectedQuotationDate.EditValue);
                followProjectBase.ExpectedPODate = TextUtils.ToDate4(txtExpectedPODate.EditValue);
                followProjectBase.ExpectedProjectEndDate = TextUtils.ToDate4(txtExpectedProjectEndDate.EditValue);

                // thực tế
                followProjectBase.RealityPlanDate = TextUtils.ToDate4(txtRealityPlanDate.EditValue);
                followProjectBase.RealityQuotationDate = TextUtils.ToDate4(txtRealityQuotationDate.EditValue);
                followProjectBase.RealityPODate = TextUtils.ToDate4(txtRealityPODate.EditValue);
                followProjectBase.RealityProjectEndDate = TextUtils.ToDate4(txtRealityProjectEndDate.EditValue);

                //follow 
                followProjectBase.TotalWithoutVAT = TextUtils.ToDecimal(txtTotalWithoutVAT.EditValue);
                followProjectBase.ProjectContactName = txtProjectContactName.Text.Trim();
                followProjectBase.Note = txtNote.Text.Trim();

                followProjectBase.WarehouseID = warehouseID;

                ////PM Báo cáo
                //followProjectBase.DateDonePM = TextUtils.ToDate4(dtpDateDonePM.EditValue);
                //followProjectBase.DateWillDoPM = TextUtils.ToDate4(dtpDateWillDoPM.EditValue);
                //followProjectBase.WorkDonePM = txtWorkDonePM.Text.Trim();
                //followProjectBase.WorkWillDoPM = txtWorkWillDoPM.Text.Trim();

                ////Sale báo cáo
                //followProjectBase.DateDoneSale = TextUtils.ToDate4(dtpDateDoneSale.EditValue);
                //followProjectBase.DateWillDoSale = TextUtils.ToDate4(dtpDateWillDoSale.EditValue);
                //followProjectBase.WorkDone = txtWorkDone.Text.Trim();
                //followProjectBase.WorkWillDo = txtWorkWillDo.Text.Trim();

                //project.ID = TextUtils.ToInt(cbProject.EditValue);
                //if (project.ID == followProjectBase.ProjectID)
                //{

                //}
                if (followProjectBase.ID > 0)
                {
                    FollowProjectBaseBO.Instance.Update(followProjectBase);
                    //MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    FollowProjectBaseBO.Instance.Insert(followProjectBase);
                    //MessageBox.Show("Thêm mới thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                string updateQuery = $"UPDATE Project SET ProjectStatus = {followProjectBase.ProjectStatusBaseID},UpdatedBy = '{Global.LoginName}',UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE ID = {followProjectBase.ProjectID}";
                TextUtils.ExcuteSQL(updateQuery);
                //project.ProjectStatus = followProjectBase.ProjectStatusBaseID;

                //Update lịch sử trạng thái
                if (projectStatusOld != followProjectBase.ProjectStatusBaseID)
                {

                    ProjectStatusLogModel statusLog = new ProjectStatusLogModel()
                    {
                        ProjectID = followProjectBase.ProjectID,
                        ProjectStatusID = followProjectBase.ProjectStatusBaseID,
                        EmployeeID = Global.EmployeeID,
                        DateLog = dateStatusLog.Value
                    };

                    SQLHelper<ProjectStatusLogModel>.Insert(statusLog);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                cbProject.Text = "";
                cbUser.Text = "";
                cbProjectStatus.Text = "";
                cbCustomer.Text = "";
                cbFirmBase.Text = "";
                cbProjectTypeBase.Text = "";
                txtProjectStartDate.Text = "";
                //txtImplementationDate.Text = "";
                //txtExpectedDate.Text = "";
                txtWorkDone.Clear();
                txtWorkWillDo.Clear();
                txtPossibilityPO.Clear();

                txtExpectedPlanDate.Text = "";
                txtExpectedQuotationDate.Text = "";
                txtExpectedPODate.Text = "";
                txtExpectedProjectEndDate.Text = "";

                txtRealityPlanDate.Text = "";
                txtRealityQuotationDate.Text = "";
                txtRealityPODate.Text = "";
                txtRealityProjectEndDate.Text = "";

                txtTotalWithoutVAT.EditValue = 0;
                txtProjectContactName.Clear();
                txtNote.Clear();
                followProjectBase = new FollowProjectBaseModel();
            }
            
        }

        private void frmFirmDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// check lỗi
        /// </summary>
        /// <returns></returns>
        /// 

        bool ValidateForm()
        {
            if (string.IsNullOrEmpty(cbProject.Text))
            {
                MessageBox.Show("Vui lòng chọn Dự án!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            //if (string.IsNullOrEmpty(cbUser.Text))
            //{
            //    MessageBox.Show("Vui lòng chọn người dùng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            if (string.IsNullOrEmpty(cbProjectStatus.Text))
            {
                MessageBox.Show("Vui lòng chọn Trạng thái dự án!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (TextUtils.ToInt(cbCustomer.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrEmpty(cbFirmBase.Text))
            {
                MessageBox.Show("Vui lòng chọn Hãng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrEmpty(cbProjectTypeBase.Text))
            {
                MessageBox.Show("Vui lòng chọn Loại dự án!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            int projectStatusNew = TextUtils.ToInt(cbProjectStatus.EditValue);
            if (!dateStatusLog.HasValue && projectStatusOld != projectStatusNew)
            {
                flyoutPanel1.ShowPopup();

                MessageBox.Show($"Vui lòng chọn {label6.Text}!", "Thông báo");
                return false;
            }

            //if (string.IsNullOrEmpty(txtImplementationDate.Text))
            //{
            //    MessageBox.Show("Vui lòng chọn Ngày thực hiện gần nhất!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            //if (string.IsNullOrEmpty(txtExpectedDate.Text))
            //{
            //    MessageBox.Show("Vui lòng chọn Ngày dự kiến thực hiện!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}


            //if (!TextUtils.ToDate4(dtpDateDonePM.EditValue).HasValue)
            //{
            //    MessageBox.Show("Vui lòng nhập Ngày đã làm (PM báo cáo)!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            //if (!TextUtils.ToDate4(dtpDateWillDoPM.EditValue).HasValue)
            //{
            //    MessageBox.Show("Vui lòng nhập Ngày sẽ làm (PM báo cáo)!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            //if (string.IsNullOrEmpty(txtWorkDonePM.Text.Trim()))
            //{
            //    MessageBox.Show("Vui lòng nhập Việc đã làm (PM báo cáo)!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            //if (string.IsNullOrEmpty(txtWorkWillDoPM.Text.Trim()))
            //{
            //    MessageBox.Show("Vui lòng nhập Việc sẽ làm (PM báo cáo)!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            //DataTable dt;
            //if (followProjectBase.ID > 0)
            //{
            //    dt = TextUtils.Select("select top 1 ProjectName from FollowProjectBase where ProjectName = '" + txtProjectName.Text.Trim() + "' and ID <> " + followProjectBase.ID);
            //}
            //else
            //{
            //    dt = TextUtils.Select("select top 1 ProjectName from FollowProjectBase where ProjectName = '" + txtProjectName.Text.Trim() + "'");
            //}
            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        MessageBox.Show("Mã đã tồn tại, vui lòng kiểm tra lại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return false;
            //    }
            //}

            return true;
        }





        private void txtTotalWithoutVAT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnProjectStatusBase_Click(object sender, EventArgs e)
        {
            frmProjectStatusBaseDetail frm = new frmProjectStatusBaseDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectStatus();
            }
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

        private void cbProject_EditValueChanged(object sender, EventArgs e)
        {
            int selectedProjectID = TextUtils.ToInt(cbProject.EditValue);

            ProjectModel project = SQLHelper<ProjectModel>.FindByID(selectedProjectID);
            cbCustomer.EditValue = project.CustomerID;
            cboEndUser.EditValue = project.EndUser;
            cbProjectStatus.EditValue = project.ProjectStatus;
            txtProjectStartDate.EditValue = project.CreatedDate;
            cbUser.EditValue = project.UserID;
            cboPM.EditValue = project.ProjectManager;


            //DataTable dtProject = TextUtils.Select($"SELECT * FROM Project WHERE ID = {selectedProjectID}");

            //if (dtProject != null && dtProject.Rows.Count > 0)
            //{
            //    int customerID = TextUtils.ToInt(dtProject.Rows[0]["CustomerID"]);
            //    int StatusID = TextUtils.ToInt(dtProject.Rows[0]["ProjectStatus"]);
            //    int EndUser = TextUtils.ToInt(dtProject.Rows[0]["EndUser"]);
            //    int UserID = TextUtils.ToInt(dtProject.Rows[0]["UserID"]);
            //    // Đổ dữ liệu cho cbCustomerBase


            //    cbCustomer.EditValue = customerID;

            //    // Set giá trị cho cboEndUser

            //    cboEndUser.EditValue = EndUser;


            //    // Set giá trị cho Status

            //    cbProjectStatus.EditValue = StatusID;

            //    // Set Thời gian bắt đầu
            //    txtProjectStartDate.EditValue = DateTime.Now;


            //    cbUser.EditValue = UserID;
            //}
        }

        private void frmFollowProjectBaseDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmFollowProjectBaseDetail_SizeChanged(object sender, EventArgs e)
        {
            //splitContainerControl1.SplitterPosition = splitContainerControl1.Width / 3;
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
                        MessageBox.Show($"Vui lòng chọn {label6.Text}!", "Thông báo");
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

        private void cbProjectStatus_EditValueChanged(object sender, EventArgs e)
        {
            ProjectStatusModel projectStatus = (ProjectStatusModel)cbProjectStatus.GetSelectedDataRow() ?? new ProjectStatusModel();
            if (followProjectBase.ProjectStatusBaseID == projectStatus.ID || projectStatus.ID <= 0)
            {
                flyoutPanel1.HidePopup();
                return;
            }
            flyoutPanel1.ShowPopup();
        }
    }
}
