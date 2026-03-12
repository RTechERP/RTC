using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Forms.DanhMuc.JobRequirement;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmJobRequirement : _Forms
    {
        int approvedTBPID = 0;
        public frmJobRequirement(bool tbp)
        {
            InitializeComponent();

            approvedTBPID = tbp ? Global.EmployeeID : 0;

            btnApproveTBP_New.Visible = tbp;
            btnUnApproveTBP_New.Visible = tbp;
            toolStripSeparator14.Visible = tbp;

            toolStripSeparator11.Visible = !tbp;
            toolStripSeparator12.Visible = !tbp;
            toolStripSeparator13.Visible = !tbp;
            toolStripSeparator8.Visible = !tbp;
            toolStripSeparator1.Visible = !tbp;
            toolStripSeparator10.Visible = !tbp;
            toolStripSeparator5.Visible = !tbp;
            btnAdd.Visible = !tbp;
            btnEdit.Visible = !tbp;
            btnDelete.Visible = !tbp;
            btnTBP.Visible = !tbp;
            btnHR.Visible = !tbp;
            btnBGĐ.Visible = !tbp;
            btnBPPH.Visible = !tbp;
            btnSumaryRequestBuy.Visible = !tbp;
            btnPrint.Visible = !tbp;
        }
        private void frmJobRequirement_Load(object sender, EventArgs e)
        {

            //btnViewPriceRequest.Visible = Global.IsAdmin;
            DateTime curentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateStart.Value = curentDate;
            dtpDateEnd.Value = curentDate.AddMonths(+1).AddDays(-1);
            GetStep();
            GetAllEmployee();
            LoadDepartment();
            GetAll();
        }
        private void GetStep()
        {

            List<object> listStep = new List<object>()
            {
                new {Step = 0,StepName = "Tất cả"},
                new {Step = 1,StepName = "NV đề nghị"},
                new {Step = 2,StepName = "TBP xác nhận"},
                new {Step = 3,StepName = "HR check hồ sơ"},
                new {Step = 4,StepName = "TBP HR xác nhận"},
                new {Step = 5,StepName = "Ban giám đốc xác nhận"},
                new {Step = 6,StepName = "Phòng mua hàng hoặc P.HCNS triển khai"}
            };
            cboStep.DataSource = listStep;
            cboStep.ValueMember = "Step";
            cboStep.DisplayMember = "StepName";
            cboStep.SelectedValue = approvedTBPID <= 0 ? 0 : 1;
        }
        void LoadDepartment()
        {
            //DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { -1 });
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = list;

            //if (Global.DepartmentCode == "GD" || Global.DepartmentCode == "KT" || Global.DepartmentCode == "HR" || Global.IsAdmin || Global.LoginName == "TrangLT" || Global.LoginName == "NV0058") //Nếu là BGĐ hoặc phòng kế toán hoặc nhận sự hoặc Lê Thị Trang
            //{
            //    cboDepartment.EditValue = 0;
            //    cboDepartment.Enabled = true;
            //}
            //else
            //{
            //    cboDepartment.EditValue = Global.DepartmentID;
            //    cboDepartment.Enabled = false;
            //}
        }
        private void GetAllEmployee()
        {
            var data = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = data;

            if (approvedTBPID <= 0) cboEmployee.EditValue = Global.EmployeeID;


        }
        private void GetAll()
        {
            DateTime dateStart = dtpDateStart.Value;
            DateTime dateEnd = dtpDateEnd.Value;
            string request = TextUtils.ToString(txtKeyword.Text).Trim();
            int employeeId = TextUtils.ToInt(cboEmployee.EditValue);
            int departmentId = TextUtils.ToInt(cboDepartment.EditValue);
            int step = TextUtils.ToInt(cboStep.SelectedValue);
            DataTable data = TextUtils.LoadDataFromSP("spGetJobRequirement", "A",
                                                        new string[] { "@DateStart", "@DateEnd", "@Request", "@EmployeeId", "@Step", "@DepartmentId", "@ApprovedTBPID" },
                                                        new object[] { dateStart, dateEnd, request, employeeId, step, departmentId, approvedTBPID });

            grdData.DataSource = data;

            GetDetails();
        }
        private void GetDetails()
        {
            int jobRequirementID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            DataSet data = TextUtils.LoadDataSetFromSP("spGetJobRequirementDetail",
                                                    new string[] { "@JobRequirementID" },
                                                    new object[] { jobRequirementID });

            DataTable dtDetails = data.Tables[0];
            DataTable dtApproved = data.Tables[1];
            DataTable dtFiles = data.Tables[2];

            grdDetails.DataSource = dtDetails;
            grdFile.DataSource = dtFiles;
            grdApproved.DataSource = dtApproved;

        }
        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            GetAll();
        }

        private void cboStep_SelectedValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void dtpDateEnd_ValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void dtpDateStart_ValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GetDetails();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmJobRequirementDetails frm = new frmJobRequirementDetails();

            if (frm.ShowDialog() == DialogResult.OK) btnFind_Click(null, null);
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowIndex = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colID));
            if (id <= 0) return;
            frmJobRequirementDetails frm = new frmJobRequirementDetails();
            JobRequirementModel dataJR = SQLHelper<JobRequirementModel>.FindByID(id);
            //if (dataJR == null)
            //{
            //    //MessageBox.Show($"Không tìm thấy yêu cầu công việc [{dataJR.NumberRequest}]!");
            //    return;
            //}

            bool isApproved = SQLHelper<JobRequirementApprovedModel>.FindByAttribute("JobRequirementID", id).Any(x => x.IsApproved == 1 && x.Step != 1);
            //if (isApproved)
            //{
            //    MessageBox.Show($"Yêu cầu công việc [{dataJR.NumberRequest}] đã được duyệt! Không thể sửa!");
            //    return;
            //}

            frm.model = dataJR;
            if (!Global.IsAdmin)
            {
                if (isApproved)
                {
                    frm.btnSave.Enabled = frm.btnSaveNew.Enabled = false;
                }
                else if (dataJR.EmployeeID != Global.EmployeeID)
                {
                    frm.btnSave.Enabled = frm.btnSaveNew.Enabled = false;
                }
            }
            //frm.btnSave.Enabled = frm.btnSaveNew.Enabled = isApproved || dataJR.EmployeeID != Global.EmployeeID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GetAll();
                grvData.FocusedRowHandle = rowIndex;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {


            //return;
            //frmJobRequirementDetails frm = new frmJobRequirementDetails();
            //int id = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colID));
            //frm.model = SQLHelper<JobRequirementModel>.FindByID(id);
            //frm.isCopy = true;

            //if (frm.ShowDialog() == DialogResult.OK) btnFind_Click(null, null);


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int rowIndex = grvData.FocusedRowHandle;
            string numberRequest = grvData.GetRowCellValue(rowIndex, colNumberRequest).ToString();

            int id = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex, colID));
            if (id <= 0) return;
            JobRequirementModel dataJR = SQLHelper<JobRequirementModel>.FindByID(id);
            bool isApproved = SQLHelper<JobRequirementApprovedModel>.FindByAttribute("JobRequirementID", dataJR.ID).Any(x => x.IsApproved == 1 && x.Step != 1);
            if (isApproved)
            {
                MessageBox.Show($"Yêu cầu công việc [{dataJR.NumberRequest}] đã được duyệt! Không thể xóa!", "Thông báo");
                return;
            }
            else if (dataJR.EmployeeID != Global.EmployeeID)
            {
                MessageBox.Show($"Bạn không thể xoá yêu cầu công việc của người khác!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có muốn xóa yêu cầu công việc [{numberRequest}] hay không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {

                dataJR.IsDeleted = true;
                JobRequirementBO.Instance.Update(dataJR);
                btnFind_Click(null, null);
                //grvData_FocusedRowChanged(null, null);
                //grvData.FocusedRowHandle = rowIndex;
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }



        void Approve(int status, /*ToolStripMenuItem button,*/ object sender)
        {
            string[] buttonTBPActions = new string[] { "btnApproveTBP_New", "btnUnApproveTBP_New" }; //LinhTN update 17/08/2024
            string[] stepNames = new string[] { "", "Tạo phiếu yêu cầu công việc", "TBP xác nhận", "HR check yêu cầu", "TBP HR xác nhận", "BGĐ xác nhận" };
            int rowHandle = grvData.FocusedRowHandle;

            int actionStep = 0;
            //string statusText = button.Text;
            string statusText = ""; //LinhTN update 17/08/2024

            int id = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colID));
            if (id <= 0)
            {
                return;
            }

            int step = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colStep));
            JobRequirementModel jobRequire = SQLHelper<JobRequirementModel>.FindByID(id);
            //Update action step

            //LinhTN update 17/08/2024 - start
            string dropdownButtonName = "";
            string dropdownButtonText = "";
            string buttonName = "";
            string buttonText = "";
            if (sender.GetType().Name == "ToolStripMenuItem")
            {
                ToolStripMenuItem button = (ToolStripMenuItem)sender;
                ToolStripItem dropdownButton = (ToolStripItem)button.OwnerItem;
                dropdownButtonName = dropdownButton.Name;
                dropdownButtonText = dropdownButton.Text;
                buttonName = button.Name;
                buttonText = button.Text;
                statusText = button.Text;
            }
            else
            {
                ToolStripButton toolStripButton = (ToolStripButton)sender;
                if (buttonTBPActions.Contains(toolStripButton.Name))
                {
                    dropdownButtonName = "btnTBP";
                    statusText = toolStripButton.Text;
                }
            }
            //LinhTN update 17/08/2024 - end

            //ToolStripItem dropdownButton = (ToolStripItem)button.OwnerItem;

            if (dropdownButtonName == "btnTBP") actionStep = 2;
            else if (dropdownButtonName == "btnHR")
            {
                if (buttonName == "btnApproveDocumentHR" || buttonName == "btnUnApproveDocumentHR") actionStep = 3;
                if (buttonName == "btnApproveHR" || buttonName == "btnUnApproveHR") actionStep = 4;
                if (buttonName == "btnSuccessApproved") actionStep = 5;

            }
            else if (dropdownButtonName == "btnBGĐ") actionStep = 5;


            string numberRequest = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNumberRequest));
            if (actionStep == 0)
            {
                MessageBox.Show($"Yêu cầu công việc [{numberRequest}] không cần {dropdownButtonText.Trim().ToLower()} {buttonText.Trim().ToLower()}!");
                return;
            }

            int isApproved = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colIsApproved));

            //if (!Global.IsAdmin)
            //{
            //}

            if (actionStep == step)
            {
                if (status == 1 && isApproved == 2)
                {
                    MessageBox.Show($"Bạn không thể {statusText}!", "Thông báo");
                    return;
                }
                else if (status == 1 && isApproved == 1)
                {
                    MessageBox.Show($"Đề nghị [{jobRequire.NumberRequest}] đã được duyệt!", "Thông báo");
                    return;
                }

            }
            else if (actionStep < step)
            {
                if (status != 1)
                {
                    MessageBox.Show($"Vui lòng huỷ duyệt ở bước [{stepNames[actionStep + 1]}] !", "Thông báo");
                    return;
                }
            }
            else
            {

                if (isApproved != 1)
                {
                    MessageBox.Show($"Bạn không thể {statusText}!", "Thông báo");
                    return;
                }
                else if ((actionStep - step) == 1) //Ở ngay bước sau
                {
                    if (status == 1 && isApproved == 2)
                    {
                        MessageBox.Show("Bạn không thể huỷ duyệt!", "Thông báo");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show($"Vui lòng duyệt ở bước [{stepNames[actionStep - 1]}]!", "Thông báo");
                    return;
                }
            }


            //Check có phải TBP hay ko
            if (dropdownButtonName == "btnTBP")
            {
                if (jobRequire.ApprovedTBPID != Global.EmployeeID && !Global.IsAdmin)
                {
                    MessageBox.Show($"Bạn không phải Trưởng bộ phận được yêu cầu duyệt!", "Thông báo");
                    return;
                }
            }

            //Get quy trình duyệt của phiếu
            var exp1 = new Expression("JobRequirementID", id);
            var exp2 = new Expression("Step", actionStep);
            JobRequirementApprovedModel log = SQLHelper<JobRequirementApprovedModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
            if (status == 2)
            {
                frmJobRequirementUnApproved frm = new frmJobRequirementUnApproved(false);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(frm.txtReasonCancel.Text.Trim())) return;

                    //log.StepName = $"{dropdownButton.Text} {button.Text.ToLower()}";
                    log.DateApproved = DateTime.Now;
                    log.IsApproved = status;
                    log.ApprovedActualID = Global.EmployeeID;
                    log.ReasonCancel = frm.txtReasonCancel.Text.Trim();
                    log.ContentLog += $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}: {Global.AppFullName} {buttonText.ToLower().Trim()}\n";

                    SQLHelper<JobRequirementApprovedModel>.Update(log);

                    //Add Notify
                    string textNotify = $"Yêu cầu công việc đã bị huỷ\n" +
                                        $"Số yêu cầu: { jobRequire.NumberRequest}\n" +
                                        $"Thời gian cần hoàn thành: { jobRequire.DeadlineRequest.Value.ToString("dd/MM/yyyy")}";

                    TextUtils.AddNotify("YÊU CẦU CÔNG VIỆC", textNotify, TextUtils.ToInt(jobRequire.EmployeeID));
                }

                //string path = @"\\192.168.1.190\Common\08. SOFTWARES\LeTheAnh\DemoContractAccounting";
                //DirectoryInfo di = new DirectoryInfo(path);
                //di.Attributes = FileAttributes.Hidden;

                //Ẩn thư mục khi huỷ duyệt
                //GetFolderPayment(id, false);
            }

            else
            {

                DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {statusText.ToLower()} yêu cầu công việc [{numberRequest}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    log.DateApproved = DateTime.Now;
                    log.IsApproved = status;
                    log.ApprovedActualID = Global.EmployeeID;
                    log.ReasonCancel = "";
                    log.ContentLog += $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}: {Global.AppFullName} {buttonText.ToLower()}\n";

                    SQLHelper<JobRequirementApprovedModel>.Update(log);

                    string fullName = TextUtils.ToString(grvData.GetFocusedRowCellValue("EmployeeName"));

                    if (buttonName == "btnApproveTBP")
                    {
                        SendMail(fullName, jobRequire, "btnApproveTBP");
                        //Add Notify
                        string textNotify = $"Yêu cầu duyệt\n" +
                                            $"Số yêu cầu: {jobRequire.NumberRequest}\n" +
                                            $"Thời gian cần hoàn thành: { jobRequire.DeadlineRequest.Value.ToString("dd/MM/yyyy")}";

                        TextUtils.AddNotify("YÊU CẦU CÔNG VIỆC", textNotify, 6);
                    }
                    else if (buttonName == "btnRequestBGDApproved")
                    {
                        SendMail(fullName, jobRequire, "btnRequestBGDApproved");

                        //Add Notify
                        string textNotify = $"Yêu cầu duyệt\n" +
                                            $"Số yêu cầu: { jobRequire.NumberRequest}\n" +
                                            $"Thời gian cần hoàn thành: { jobRequire.DeadlineRequest.Value.ToString("dd/MM/yyyy")}";

                        TextUtils.AddNotify("YÊU CẦU CÔNG VIỆC", textNotify, 0, 1);
                    }
                    else if (buttonName == "btnApproveBGĐ")
                    {
                        //Add Notify
                        string textNotify = $"BGĐ đã duyệt\n" +
                                            $"Số yêu cầu: { jobRequire.NumberRequest}\n" +
                                            $"Thời gian cần hoàn thành: { jobRequire.DeadlineRequest.Value.ToString("dd/MM/yyyy")}";

                        TextUtils.AddNotify("YÊU CẦU CÔNG VIỆC", textNotify, 0, 6);
                    }


                }

            }

            //Add log
            JobRequirementLogModel logModel = new JobRequirementLogModel()
            {
                JobRequirementID = jobRequire.ID,
                EmployeeID = Global.EmployeeID,
                DateLog = DateTime.Now,
                LogContent = $"{Global.AppFullName} đã {statusText}"
            };
            SQLHelper<JobRequirementLogModel>.Insert(logModel);
            btnFind_Click(null, null);
            grvData.FocusedRowHandle = rowHandle;
        }

        void RequestBGDApproved(bool isRequest)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, "ID"));
            if (id <= 0) return;
            string isRequestText = isRequest ? "yêu cầu" : "huỷ yêu cầu";
            string numberRequest = TextUtils.ToString(grvData.GetRowCellValue(rowHandle, colNumberRequest));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isRequestText} BGĐ duyệt yêu cầu công việc [{numberRequest}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                int isRequestValue = isRequest ? 1 : 0;
                var myDict = new Dictionary<string, object>()
                {
                    { JobRequirementModel_Enum.IsRequestBGDApproved.ToString(),isRequest},
                    { JobRequirementModel_Enum.UpdatedBy.ToString(),Global.LoginName},
                    { JobRequirementModel_Enum.UpdatedDate.ToString(),DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},
                };

                SQLHelper<JobRequirementModel>.UpdateFieldsByID(myDict, id);

                btnFind_Click(null, null);
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void btnApproveTBP_Click(object sender, EventArgs e)
        {
            Approve(1, sender);
        }
        private void btnApproveDocumentHR_Click(object sender, EventArgs e)
        {
            Approve(1, sender);
        }
        private void btnApproveHR_Click(object sender, EventArgs e)
        {
            Approve(1, sender);
        }
        private void btnApproveBGĐ_Click(object sender, EventArgs e)
        {
            Approve(1, sender);
        }

        private void btnUnApproveTBP_Click(object sender, EventArgs e)
        {
            Approve(2, sender as ToolStripMenuItem);
        }
        private void btnUnApproveDocumentHR_Click(object sender, EventArgs e)
        {
            Approve(2, sender);
        }
        private void btnUnApproveHR_Click(object sender, EventArgs e)
        {
            Approve(2, sender);
        }
        private void btnUnApproveBGĐ_Click(object sender, EventArgs e)
        {
            Approve(2, sender);
        }

        private void grvApproved_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    if (e.Column == colAprIsApproved)
            //    {
            //        if (TextUtils.ToInt(e.CellValue) == 1)
            //        {
            //            e.Appearance.BackColor = Color.Yellow;
            //            e.Appearance.ForeColor = Color.Black;
            //        }
            //        else if (TextUtils.ToInt(e.CellValue) == 2)
            //        {
            //            e.Appearance.BackColor = Color.Green;
            //            e.Appearance.ForeColor = Color.White;

            //        }
            //    }
            //}
        }

        private void grvApproved_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            int isApproved = TextUtils.ToInt(grvApproved.GetRowCellValue(e.RowHandle, colAprIsApproved));
            if (isApproved == 2)
            {
                e.Appearance.BackColor = System.Drawing.Color.Red;
                e.Appearance.ForeColor = System.Drawing.Color.White;
            }
            else if (isApproved == 1)
            {
                e.Appearance.BackColor = System.Drawing.Color.LightGreen;
                e.Appearance.ForeColor = System.Drawing.Color.Black;

            }
            e.HighPriority = true;
        }

        //LinhTN update 01/07/2024
        private void BPPH(int stt)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            var jobRequirement = SQLHelper<JobRequirementModel>.FindByID(ID);

            var ex1 = new Expression("JobRequirementID", ID);
            var ex2 = new Expression("Step", 5); //Bước BGĐ xác nhận
            var ex3 = new Expression("IsApproved", 1); //BGĐ xác nhận đã duyệt
            var jobRequirementApproved = SQLHelper<JobRequirementApprovedModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault();
            if (jobRequirementApproved == null)
            {
                MessageBox.Show($"Yêu cầu công việc [{jobRequirement.NumberRequest}] cần được BGĐ duyệt!", "Thông báo");
                return;
            }

            if (jobRequirement.Status == 1)
            {
                MessageBox.Show($"Yêu cầu công việc [{jobRequirement.NumberRequest}] đã hoàn thành!", "Thông báo");
                return;
            }
            if (stt == 1)
            {
                DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xác nhận hoàn thành yêu cầu công việc  [{jobRequirement.NumberRequest}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    //jobRequirement.IsRequestBuy = false; //Không yêu cầu mua
                    jobRequirement.Status = 1; //Hoàn thành công việc
                    JobRequirementBO.Instance.Update(jobRequirement);


                    //Add notify
                    string textNotify = $"Yêu cầu công việc đã hoàn thành\n" +
                                           $"Số yêu cầu: { jobRequirement.NumberRequest}\n" +
                                           $"Thời gian cần hoàn thành: { jobRequirement.DeadlineRequest.Value.ToString("dd/MM/yyyy")}";

                    TextUtils.AddNotify("YÊU CẦU CÔNG VIỆC", textNotify, TextUtils.ToInt(jobRequirement.EmployeeID));
                }
            }
            else if (stt == 2)// VTN update 18225
            {
                //if (jobRequirement.IsRequestBuy)
                //{
                //    DialogResult dialog = MessageBox.Show($"Yêu cầu công việc [{jobRequirement.NumberRequest}] đã có yêu cầu mua, bạn có muốn thêm yêu cầu?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    if (dialog == DialogResult.Yes)
                //    {
                //        frmProjectPartlistPurchaseRequestDetail frm = new frmProjectPartlistPurchaseRequestDetail();
                //        frm.jobRequirementID = ID;
                //        frm.employeeRequestID = jobRequirement.EmployeeID;
                //        if (frm.ShowDialog() == DialogResult.OK)
                //        {
                //            jobRequirement.IsRequestBuy = true; //Yêu cầu mua
                //            jobRequirement.Status = 2; //Chưa hoàn thành
                //            JobRequirementBO.Instance.Update(jobRequirement);
                //        }
                //    }
                //}
                //else
                {
                    frmProjectPartlistPurchaseRequestDetail frm = new frmProjectPartlistPurchaseRequestDetail();
                    frm.jobRequirement = SQLHelper<JobRequirementModel>.FindByID(ID);
                    //frm.jobRequirementID = ID;
                    //frm.employeeRequestID = jobRequirement.EmployeeID;

                    var exp1 = new Expression("JobRequirementID", ID);
                    var exp2 = new Expression("STT", 4);
                    JobRequirementDetailModel detail = SQLHelper<JobRequirementDetailModel>.FindByExpression(ex1.And(exp2)).FirstOrDefault() ?? new JobRequirementDetailModel();
                    frm.txtQuantity.Value = TextUtils.ToDecimal(detail.Description);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        jobRequirement.IsRequestBuy = true; //Yêu cầu mua
                        jobRequirement.Status = 2; //Chưa hoàn thành
                        JobRequirementBO.Instance.Update(jobRequirement);
                    }
                }
            }
            else // VTN update 18225
            {
                var exp2 = new Expression("STT", 4);
                JobRequirementDetailModel detail = SQLHelper<JobRequirementDetailModel>.FindByExpression(ex1.And(exp2)).FirstOrDefault() ?? new JobRequirementDetailModel();

                string noteJobRequirement = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNote));
                frmProjectPartlistPriceRequestDetailNew frm = new frmProjectPartlistPriceRequestDetailNew(jobRequirement.ID,3); //Hàng HR
                //frm.isJobRequirement = true;
                frm.noteJobRequirement = noteJobRequirement;
                frm.qty = TextUtils.ToInt(detail.Description);

                if (frm.ShowDialog() == DialogResult.OK)
                {

                    jobRequirement.IsRequestPriceQuote = true; //Yêu cầu báo giá 
                    jobRequirement.Status = 2; //Chưa hoàn thành
                    //JobRequirementBO.Instance.Update(jobRequirement);
                    SQLHelper<JobRequirementModel>.Update(jobRequirement);
                }
            }
            GetAll();
        }
        //LinhTN update 01/07/2024


        void DownloadFile(string pathDownload, string pathPattern, int rowHandle)
        {
            string fileName = TextUtils.ToString(grvFile.GetRowCellValue(rowHandle, "FileName"));
            string folderDownload = Path.Combine(pathDownload, fileName);
            string url = $"http://113.190.234.64:8083/api/jobrequirement/{pathPattern}/{fileName}";

            WebClient webClient = new WebClient();
            webClient.DownloadFile(url, folderDownload);
            Process.Start(folderDownload);
        }
        private void btnPersonnelBPPH_Click(object sender, EventArgs e)
        {
            BPPH(1);
        }
        //LinhTN update 01/07/2024
        private void btnPurchaseRequestBPPH_Click(object sender, EventArgs e)
        {
            BPPH(2);
        }

        private void btnSumaryRequestBuy_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            frmJobRequeirementFurchaseRequest frm = new frmJobRequeirementFurchaseRequest();
            //frm.listJobRequirementID = listJobRequirementID;
            frm.jobRequirementID = id;
            frm.ShowDialog();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"YeuCauCongViec_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx";

            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"YeuCauCongViec_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx");
                string filepath = f.FileName;

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;
                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            frmJobRequirementSumarize frm = new frmJobRequirementSumarize();
            frm.DateEnd = dtpDateEnd.Value;
            frm.DateStart = dtpDateStart.Value;
            frm.Step = TextUtils.ToInt(cboStep.SelectedValue);
            frm.Request = txtKeyword.Text.Trim();
            frm.DepartmentId = TextUtils.ToInt(cboDepartment.EditValue);
            frm.EmployeeId = TextUtils.ToInt(cboEmployee.EditValue);
            frm.Show();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var dataRow = grvData.GetDataRow(grvData.FocusedRowHandle);
            frmJobRequirementPrint frm = new frmJobRequirementPrint();
            frm.data = dataRow;
            frm.Show();
        }

        private void btnViewAttachFile_Click(object sender, EventArgs e)
        {
            try
            {
                string path = TextUtils.ToString(grvFile.GetFocusedRowCellValue("ServerPath"));
                string fileName = TextUtils.ToString(grvFile.GetFocusedRowCellValue("FileName"));

                Process.Start(Path.Combine(path, fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDownloadAttachFile_Click(object sender, EventArgs e)
        {
            try
            {
                //string pathDownload = Path.Combine(KnownFolders.Downloads.Path, "DeNghiThanhToan");
                //string pathDownload = Path.Combine(Application.StartupPath, "DeNghiThanhToan");
                DateTime dateOrder = TextUtils.ToDate5(grvData.GetFocusedRowCellValue("DateRequest"));
                string numberRequest = TextUtils.ToString(grvData.GetFocusedRowCellValue("NumberRequest"));

                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", "YeuCauCongViec", numberRequest);

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }


                //string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
                string pathPattern = $@"NĂM {dateOrder.Year}/YÊU CẦU CÔNG VIỆC/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{numberRequest}";

                int[] selectedRows = grvFile.GetSelectedRows();
                if (selectedRows.Length <= 0)
                {
                    DownloadFile(pathDownload, pathPattern, grvFile.FocusedRowHandle);
                }
                else
                {
                    foreach (int row in selectedRows)
                    {
                        DownloadFile(pathDownload, pathPattern, row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnAddNote_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;

            string note = TextUtils.ToString(grvData.GetFocusedRowCellValue("Note"));
            frmJobRequirementUnApproved frm = new frmJobRequirementUnApproved(true);
            frm.txtReasonCancel.Text = note;
            if (frm.ShowDialog() == DialogResult.OK)
            {

                //var myDict = new Dictionary<string, object>
                //                            {
                //                                { "Note", frm.txtReasonCancel.Text.Trim() }
                //                            };

                if (string.IsNullOrWhiteSpace(frm.txtReasonCancel.Text.Trim())) return;
                //JobRequirementModel job = SQLHelper<JobRequirementModel>.FindByID(id);
                //job.Note = $"\n{Global.AppFullName} - {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}: \n{frm.txtReasonCancel.Text.Trim()}".Trim();
                //job.Note = $"{frm.txtReasonCancel.Text.Trim()}";

                //SQLHelper<JobRequirementModel>.UpdateFieldsByID(myDict, id);
                //SQLHelper<JobRequirementModel>.Update(job);


                JobRequirementCommentModel jobComment = new JobRequirementCommentModel();
                jobComment.JobRequirementID = id;
                jobComment.DateComment = DateTime.Now;
                jobComment.EmployeeID = Global.EmployeeID;
                jobComment.CommentContent = frm.txtReasonCancel.Text.Trim();
                SQLHelper<JobRequirementCommentModel>.Insert(jobComment);

                btnFind_Click(null, null);
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = System.Drawing.Color.LightYellow;
                e.HighPriority = true;
            }
        }


        void SendMail(string fullName, JobRequirementModel jobRequirement, string action)
        {

            if (jobRequirement.ID <= 0) return;
            EmployeeSendEmailModel sendEmail = new EmployeeSendEmailModel();

            //EmployeeModel employee = SQLHelper<EmployeeModel>.FindByID(jobRequirement.ApprovedTBPID);
            List<JobRequirementDetailModel> detail = SQLHelper<JobRequirementDetailModel>.FindByAttribute("JobRequirementID", jobRequirement.ID);

            JobRequirementDetailModel contents = detail.Where(x => x.STT == 1).FirstOrDefault() ?? new JobRequirementDetailModel();
            JobRequirementDetailModel reason = detail.Where(x => x.STT == 3).FirstOrDefault() ?? new JobRequirementDetailModel();
            JobRequirementDetailModel deadline = detail.Where(x => x.STT == 7).FirstOrDefault() ?? new JobRequirementDetailModel();

            string emailTo = "";
            string title = "";
            if (action == "btnApproveTBP")
            {
                emailTo = "hanhchinh@rtc.edu.vn";
                title = "Dear phòng Hành chính nhân sự";
            }
            else if (action == "btnRequestBGDApproved")
            {
                emailTo = "dept_manager@rtc.edu.vn";
                title = "Dear Ban Giám Đốc";
            }

            if (Global.DebugFlag) emailTo = "lethe.anh@rtc.edu.vn";

            sendEmail.Subject = $"YÊU CẦU CÔNG VIỆC - {fullName.ToUpper()} - {DateTime.Now.ToString("dd/MM/yyyy")}";
            sendEmail.EmailTo = emailTo;
            sendEmail.EmailCC = $"";
            sendEmail.Body = $@"<div> <p style=""font-weight: bold; color: red;"">[NO REPLY]</p> <p> {title} </p ></div >
                       <div style = ""margin-top: 30px;"">
                        <p> Anh/chị cho em đăng ký phiếu yêu cầu công việc</p>
                        <p> Nội dung: {contents.Description}</p>
                        <p> Lý do: {reason.Description}</p>
                        <p> Thời gian cần hoàn thành: {deadline.Description}</p>
                        <p> Anh / chị duyệt giúp em với ạ.Em cảm ơn! </p>
                       </div>
                       <div style = ""margin-top: 30px;"">
                        <p> Thanks </p>
                        <p> {fullName}</p>
                       </div>";

            sendEmail.StatusSend = 1;
            sendEmail.EmployeeID = Global.EmployeeID; //Người gửi
            //sendEmail.Receiver = jobRequirement.ApprovedTBPID; //Người nhận
            //sendEmailRepo.Create(e);

            SQLHelper<EmployeeSendEmailModel>.Insert(sendEmail);
        }

        private void btnNoteContext_Click(object sender, EventArgs e)
        {
            btnAddNote_Click(null, null);
        }

        private void btnRequestBGDApproved_Click(object sender, EventArgs e)
        {
            RequestBGDApproved(true);
        }

        private void btnUnRequestBGDApproved_Click(object sender, EventArgs e)
        {
            RequestBGDApproved(false);
        }


        private void btnSuccessApproved_Click(object sender, EventArgs e)
        {
            btnApproveBGĐ_Click(sender, e);
        }

        private void grdData_Click(object sender, EventArgs e)
        {


        }

        private void btnApproveTBP_New_Click(object sender, EventArgs e)
        {
            Approve(1, sender);
        }

        private void btnUnApproveTBP_New_Click(object sender, EventArgs e)
        {
            Approve(2, sender);
        }

        private void btnRequestPriceQuote_Click(object sender, EventArgs e)
        {
            BPPH(3);
        }

        private void btnViewPriceRequest_Click(object sender, EventArgs e)
        {
            frmProjectPartlistPriceRequestNew frm = new frmProjectPartlistPriceRequestNew(3);
            frm.jobRequirementID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID)); ;
            frm.Show();
        }

        private void btnJobRequirementCheckPrice_Click(object sender, EventArgs e)
        {
            int rowIndex = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colID));
            if (id <= 0) return;
            frmJobRequirementCheckPrice frm = new frmJobRequirementCheckPrice();
            JobRequirementModel dataJR = SQLHelper<JobRequirementModel>.FindByID(id);
            //if (dataJR == null)
            //{
            //    //MessageBox.Show($"Không tìm thấy yêu cầu công việc [{dataJR.NumberRequest}]!");
            //    return;
            //}

            bool isApproved = SQLHelper<JobRequirementApprovedModel>.FindByAttribute("JobRequirementID", id).Any(x => x.IsApproved == 1 && x.Step != 1);
            //if (isApproved)
            //{
            //    MessageBox.Show($"Yêu cầu công việc [{dataJR.NumberRequest}] đã được duyệt! Không thể sửa!");
            //    return;
            //}
            if (id <= 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần check giá", "Thông báo");
                return;
            }
            frm.model = dataJR;
            frm.jobRequirementID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            //frm.btnSave.Enabled = frm.btnSaveNew.Enabled = isApproved || dataJR.EmployeeID != Global.EmployeeID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GetAll();
                grvData.FocusedRowHandle = rowIndex;
            }
        }

        private void btnCheckPriceSummary_Click(object sender, EventArgs e)
        {
            frmJobRequirementCheckPriceSummary frm = new frmJobRequirementCheckPriceSummary();
            frm.Show();
        }
    }
}
