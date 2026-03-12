using BMS.Model;
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
    public partial class frmProjectRequest : _Forms
    {
        public frmProjectRequest()
        {
            InitializeComponent();
        }

        private void frmProjectRequest_Load(object sender, EventArgs e)
        {
            LoadProject();
            LoadDataRequest();
            LoadProjectRequestFile();
            LoadProjectSolutionFile();
        }

        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.DataSource = list;
        }

        void LoadDataRequest()
        {
            int projectId = TextUtils.ToInt(cboProject.EditValue);
            string keyword = txtKeyword.Text.Trim();
            List<ProjectRequestModel> list = SQLHelper<ProjectRequestModel>.ProcedureToList("spGetProjectRequest",
                                                                            new string[] { "@ProjectID", "@Keyword" }, new object[] { projectId, keyword });
            grdDataRequest.DataSource = list;
        }

        void LoadDataSolution()
        {
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            int projectRequestId = TextUtils.ToInt(grvDataRequest.GetFocusedRowCellValue("ID"));
            List<ProjectSolutionModel> list = SQLHelper<ProjectSolutionModel>.ProcedureToList("spGetProjectSolution", 
                                                new string[] { "@ProjectID","@ProjectRequestID" }, 
                                                new object[] { projectID,projectRequestId });
            grdDataSolution.DataSource = list;
        }


        void LoadProjectRequestFile()
        {
            int projectRequestId = TextUtils.ToInt(grvDataRequest.GetFocusedRowCellValue("ID"));
            List<ProjectRequestFileModel> listFiles = SQLHelper<ProjectRequestFileModel>.FindByAttribute("ProjectRequestID", projectRequestId);
            grdDataRequestFile.DataSource = listFiles;
        }

        void LoadProjectSolutionFile()
        {
            int projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            List<ProjectSolutionFileModel> listFiles = SQLHelper<ProjectSolutionFileModel>.FindByAttribute("ProjectSolutionID", projectSolutionId);
            grdDataSolutionFile.DataSource = listFiles;
        }


        void ApprovedSolution(bool isApproved, int status)
        {
            int rowHandle = grvDataSolution.FocusedRowHandle;

            string isApprovedText = isApproved ? "duyệt" : "huỷ duyệt";
            string statusText = status == 1 ? "báo giá" : "PO";

            int id = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            string code = TextUtils.ToString(grvDataSolution.GetFocusedRowCellValue(colCodeSolution));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isApprovedText} {statusText} của giải pháp [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ProjectSolutionModel solution = SQLHelper<ProjectSolutionModel>.FindByID(id);
                if (solution == null) return;

                if (status == 1)
                {
                    solution.IsApprovedPrice = isApproved;
                    solution.EmployeeApprovedPriceID = Global.EmployeeID;
                }
                else if (status == 2)
                {
                    solution.IsApprovedPO = isApproved;
                    solution.EmployeeApprovedPOID = Global.EmployeeID;
                }


                SQLHelper<ProjectSolutionModel>.Update(solution);
                LoadDataSolution();
                grvDataSolution.FocusedRowHandle = rowHandle;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDataRequest();
            LoadDataSolution();
            LoadProjectRequestFile();
            LoadProjectSolutionFile();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProjectRequestDetail frm = new frmProjectRequestDetail();
            frm.projectId = TextUtils.ToInt(cboProject.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnSearch_Click(null, null);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowHandle = grvDataRequest.FocusedRowHandle;
            int id = TextUtils.ToInt(grvDataRequest.GetFocusedRowCellValue("ID"));
            if (id <= 0)
            {
                return;
            }

            frmProjectRequestDetail frm = new frmProjectRequestDetail();
            frm.projectRequest = SQLHelper<ProjectRequestModel>.FindByID(id);
            frm.projectId = TextUtils.ToInt(cboProject.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //LoadDataRequest();
                //grvDataRequest.FocusedRowHandle = rowHandle;

                btnSearch_Click(null, null);
                grvDataRequest.FocusedRowHandle = rowHandle;
            }
        }

        private void btnAddSolution_Click(object sender, EventArgs e)
        {
            frmProjectSolutionDetail frm = new frmProjectSolutionDetail();
            frm.projectId = TextUtils.ToInt(cboProject.EditValue);
            frm.projectRequestId = TextUtils.ToInt(grvDataRequest.GetFocusedRowCellValue("ID"));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataRequest();
                LoadDataSolution();
                LoadProjectSolutionFile();
                //btnSearch_Click(null, null);
            }
        }

        private void btnEditSolution_Click(object sender, EventArgs e)
        {
            int rowHandleR = grvDataRequest.FocusedRowHandle;
            int rowHandleS = grvDataSolution.FocusedRowHandle;
            int id = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            if (id <= 0)
            {
                return;
            }

            frmProjectSolutionDetail frm = new frmProjectSolutionDetail();
            frm.projectSolution = SQLHelper<ProjectSolutionModel>.FindByID(id);
            frm.projectId = TextUtils.ToInt(cboProject.EditValue);
            frm.projectRequestId = TextUtils.ToInt(grvDataRequest.GetFocusedRowCellValue("ID"));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataRequest();
                LoadDataSolution();
                LoadProjectSolutionFile();
                grvDataRequest.FocusedRowHandle = rowHandleR;
                grvDataSolution.FocusedRowHandle = rowHandleS;
            }
        }

        private void grvDataRequest_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //LoadDataSolution();
            LoadDataSolution();
            LoadProjectRequestFile();

            //btnSearch_Click(null, null);
        }

        private void grvDataRequest_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);

        }

        private void grvDataSolution_DoubleClick(object sender, EventArgs e)
        {
            btnEditSolution_Click(null, null);
        }

        private void btnEditRequestContext_Click(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnAddSolutionContext_Click(object sender, EventArgs e)
        {
            btnAddSolution_Click(null, null);

        }

        private void grvDataRequestFile_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", "YeuCauDuAn");

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }

                int id = TextUtils.ToInt(grvDataRequestFile.GetFocusedRowCellValue("ID"));
                string extension = TextUtils.ToString(grvDataRequestFile.GetFocusedRowCellValue("Extension"));
                string fileName = TextUtils.ToString(grvDataRequestFile.GetFocusedRowCellValue("FileNameOrigin"));
                string folderDownload = Path.Combine(pathDownload, fileName);

                int projectId = TextUtils.ToInt(cboProject.EditValue);
                ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectId);
                if (project == null) return;
                string pathPatern = $@"{project.CreatedDate.Value.Year}/{project.ProjectCode}/TaiLieuChung/YeuCauDuAn(REV02)";

                //string url = $"http://113.190.234.64:8083/api/paymentorder/{pathPattern}/{id}{extension}";
                //string url = $"http://113.190.234.64:8083/api/demo/DemoUploadFile/{id}{extension}";
                string url = $"http://113.190.234.64:8083/api/project/{pathPatern}/{id}{extension}";

                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, folderDownload);
                Process.Start(folderDownload);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grvDataSolution_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadProjectSolutionFile();
        }

        private void grvDataSolutionFile_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", "GiaiPhapDuAn");

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }

                int id = TextUtils.ToInt(grvDataSolutionFile.GetFocusedRowCellValue("ID"));
                string extension = TextUtils.ToString(grvDataSolutionFile.GetFocusedRowCellValue("Extension"));
                string fileName = TextUtils.ToString(grvDataSolutionFile.GetFocusedRowCellValue("FileNameOrigin"));
                string folderDownload = Path.Combine(pathDownload, fileName);

                int projectId = TextUtils.ToInt(cboProject.EditValue);
                ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectId);
                if (project == null) return;
                string pathPatern = $@"{project.CreatedDate.Value.Year}/{project.ProjectCode}/TaiLieuChung/GiaiPhap";

                //string url = $"http://113.190.234.64:8083/api/paymentorder/{pathPattern}/{id}{extension}";
                //string url = $"http://113.190.234.64:8083/api/demo/DemoUploadFile/Solution/{id}{extension}";
                string url = $"http://113.190.234.64:8083/api/project/{pathPatern}/{id}{extension}";


                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, folderDownload);
                Process.Start(folderDownload);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnIsApprovedPriceContext_Click(object sender, EventArgs e)
        {
            //int rowHandle = grvDataSolution.FocusedRowHandle;
            //int id = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            //if (id <= 0) return;
            //string code = TextUtils.ToString(grvDataSolution.GetFocusedRowCellValue(colCodeSolution));
            //DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn duyệt báo giá của giải pháp [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialog == DialogResult.Yes)
            //{
            //    ProjectSolutionModel solution = SQLHelper<ProjectSolutionModel>.FindByID(id);
            //    if (solution == null) return;
            //    solution.IsApprovedPrice = true;
            //    solution.EmployeeApprovedPriceID = Global.EmployeeID;

            //    SQLHelper<ProjectSolutionModel>.Update(solution);
            //    LoadDataSolution();
            //    grvDataSolution.FocusedRowHandle = rowHandle;
            //}

            ApprovedSolution(true, 1);
        }

        private void btnIsApprovedPOContext_Click(object sender, EventArgs e)
        {
            //int rowHandle = grvDataSolution.FocusedRowHandle;
            //int id = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            //if (id <= 0) return;
            //string code = TextUtils.ToString(grvDataSolution.GetFocusedRowCellValue(colCodeSolution));
            //DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn duyệt PO của giải pháp [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialog == DialogResult.Yes)
            //{
            //    ProjectSolutionModel solution = SQLHelper<ProjectSolutionModel>.FindByID(id);
            //    if (solution == null) return;
            //    solution.IsApprovedPO = true;
            //    solution.EmployeeApprovedPOID = Global.EmployeeID;

            //    SQLHelper<ProjectSolutionModel>.Update(solution);
            //    LoadDataSolution();
            //    grvDataSolution.FocusedRowHandle = rowHandle;
            //}

            ApprovedSolution(true, 1);
        }

        private void btnDeleteRequest_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvDataRequest.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;

            string code = TextUtils.ToString(grvDataRequest.GetFocusedRowCellValue(colCodeRequest));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá Yêu cầu mã [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ProjectRequestModel request = SQLHelper<ProjectRequestModel>.FindByID(id);
                request.IsDeleted = true;
                grvDataRequest.DeleteSelectedRows();
            }
        }

        private void btnDeleteSolution_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;

            string code = TextUtils.ToString(grvDataSolution.GetFocusedRowCellValue(colCodeRequest));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá Giải pháp mã [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                frmPaymentOrderUnApprove frm = new frmPaymentOrderUnApprove();
                frm.Text = $"XOÁ GIẢI PHÁP - {code}";
                frm.label1.Text = "Lý do xoá";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ProjectSolutionModel request = SQLHelper<ProjectSolutionModel>.FindByID(id);
                    request.IsDeleted = true;
                    grvDataRequest.DeleteSelectedRows();

                    SQLHelper<ProjectSolutionModel>.Update(request);

                    var versions = SQLHelper<ProjectPartListVersionModel>.FindByAttribute("ProjectSolutionID", id);
                    foreach (var item in versions)
                    {
                        string sql = $"UPDATE dbo.ProjectPartList SET IsDeleted = 1," +
                                    $"ReasonDeleted = N'{frm.txtReasonCancel.Text.Trim()}'," +
                                    $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                                    $"UpdatedBy = '{Global.LoginName}' " +
                                    $"WHERE ProjectPartListVersionID = {item.ID}";

                        TextUtils.ExcuteSQL(sql);
                    }


                    LoadDataSolution();
                }



            }
        }
    }
}
