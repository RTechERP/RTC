using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
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

namespace BMS
{
    public partial class frmProjectSurvey : _Forms
    {
        public frmProjectSurvey()
        {
            InitializeComponent();
        }

        private void frmProjectSurvey_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(2);
            LoadProject();
            LoadEmployee();

            LoadData();
        }

        void LoadData()
        {
            //var list = SQLHelper<ProjectSurveyModel>.FindAll();

            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            int employeeRequestID = TextUtils.ToInt(cboEmployeeSale.EditValue);
            int employeeTechID = TextUtils.ToInt(cboEmployeeTechnical.EditValue);
            string keyword = txtKeyword.Text.Trim();
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectSurvey", "A",
                                                    new string[] { "@DateStart", "@DateEnd", "@ProjectID", "@EmployeeRequestID", "@EmployeeTechID", "@Keyword" },
                                                    new object[] { dateStart, dateEnd, projectID, employeeRequestID, employeeTechID, keyword });


            grdData.DataSource = dt;
        }


        void LoadProject()
        {
            var list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.CreatedDate).ToList();
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.DataSource = list;
        }


        void LoadCustomer()
        {
            //var exp1 = new Expression("IsDeleted", 1, "<>");
            //var list = SQLHelper<CustomerModel>.FindByExpression(exp1);
            //cboCustomer.Properties.ValueMember = "ID";
            //cboCustomer.Properties.DisplayMember = "CustomerName";
            //cboCustomer.Properties.DataSource = list;
        }


        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });

            //Load người Phụ trách Sale
            cboEmployeeSale.Properties.ValueMember = "ID";
            cboEmployeeSale.Properties.DisplayMember = "FullName";
            cboEmployeeSale.Properties.DataSource = dt;

            //Load người  Phụ trách Kỹ thuật
            cboEmployeeTechnical.Properties.ValueMember = "ID";
            cboEmployeeTechnical.Properties.DisplayMember = "FullName";
            cboEmployeeTechnical.Properties.DataSource = dt;
        }


        void ApprovedUrgent(bool isApproved)
        {
            string isApprovedText = isApproved ? "duyệt" : "hủy duyệt";
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isApprovedText} gấp yêu cầu khảo sát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
                var myDict = new Dictionary<string, object>()
                {
                    {ProjectSurveyModel_Enum.IsApprovedUrgent.ToString(),isApproved},
                    {ProjectSurveyModel_Enum.ApprovedUrgentID.ToString(),Global.EmployeeID},
                    {ProjectSurveyModel_Enum.UpdatedBy.ToString(),Global.AppCodeName},
                    {ProjectSurveyModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                };

                SQLHelper<ProjectSurveyModel>.UpdateFieldsByID(myDict, id);
                LoadData();
            }
        }


        void Approved(int status)
        {
            string statusText = status == 1 ? "duyệt" : "hủy duyệt";

            int leaderID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colLeaderID));
            string leaderName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullNameLeaderTBP));
            if (Global.EmployeeID != leaderID)
            {
                MessageBox.Show($"Bạn không thể {statusText} khảo sát của Leader [{leaderName}]!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {statusText} yêu cầu khảo sát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectSurveyDetailID));
                int surveyID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                ProjectSurveyModel projectSurvey = SQLHelper<ProjectSurveyModel>.FindByID(surveyID);
                ProjectSurveyDetailModel projectSurveyDetail = SQLHelper<ProjectSurveyDetailModel>.FindByID(id);

                frmProjectSurveyApproved frm = new frmProjectSurveyApproved(status);
                frm.survey = projectSurvey;
                frm.surveyDetail = projectSurveyDetail;
                frm.Text = $"{statusText} yêu cầu".ToUpper();
                frm.dateSurvey = TextUtils.ToDate4(grvData.GetFocusedRowCellValue(colDateEnd)); //NTA B - update 091025
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var myDict = new Dictionary<string, object>()
                    {
                        {ProjectSurveyDetailModel_Enum.Status.ToString(),status},
                        {ProjectSurveyDetailModel_Enum.EmployeeID.ToString(),frm.surveyDetail.EmployeeID},
                        {ProjectSurveyDetailModel_Enum.DateSurvey.ToString(),frm.surveyDetail.DateSurvey},
                        {ProjectSurveyDetailModel_Enum.ReasonCancel.ToString(),frm.surveyDetail.ReasonCancel},
                        {ProjectSurveyDetailModel_Enum.UpdatedBy.ToString(),Global.AppCodeName},
                        {ProjectSurveyDetailModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                        {ProjectSurveyDetailModel_Enum.SurveySession.ToString(),frm.surveyDetail.SurveySession},
                    };

                    SQLHelper<ProjectSurveyDetailModel>.UpdateFieldsByID(myDict, id);
                    LoadData();
                }

            }
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmProjectSurveyDetail frm = new frmProjectSurveyDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.totalRecords.Count > 0) LoadData();
            }
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            ProjectSurveyModel projectSurvey = SQLHelper<ProjectSurveyModel>.FindByID(id);
            frmProjectSurveyDetail frm = new frmProjectSurveyDetail();
            frm.projectSurvey = projectSurvey;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.totalRecords.Count > 0) LoadData();
            }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;

            var listDetails = SQLHelper<ProjectSurveyDetailModel>.FindByAttribute(ProjectSurveyDetailModel_Enum.ProjectSurveyID.ToString(), id);
            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("EmployeeID"));
            if (employeeID != Global.EmployeeID && !Global.IsAdmin)
            {
                MessageBox.Show("Bạn không thể xóa yêu cầu khảo sát của người khác?", "Thông báo");
                return;
            }

            bool isConfirm = listDetails.Any(x => x.Status == 1);
            if (isConfirm && !Global.IsAdmin)
            {
                MessageBox.Show("Bạn không thể xóa yêu cầu khảo sát vì Leader Kỹ thuật đã xác nhận?", "Thông báo");
                return;
            }


            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xóa yêu cầu khảo sát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                var myDict = new Dictionary<string, object>()
                {
                    {ProjectSurveyModel_Enum.IsDeleted.ToString(),true},
                    {ProjectSurveyModel_Enum.UpdatedBy.ToString(),Global.AppCodeName},
                    {ProjectSurveyModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                };

                SQLHelper<ProjectSurveyModel>.UpdateFieldsByID(myDict, id);
                LoadData();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnApprovedUrgent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ApprovedUrgent(true);
        }

        private void btnUnApprovedUrgent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ApprovedUrgent(false);
        }

        private void btnApprovedRequest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Approved(1);
        }

        private void btnUnApprovedRequest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Approved(2);
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"YeuCauKhaoSat{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"BaoCaoCongTac_T{txtMonth.Text}_{txtYear.Value}.xlsx");
                string filepath = f.FileName;
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);
                        Process.Start(filepath);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_ItemClick(null, null);
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void btnOpenFolder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int projectID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ProjectID"));
                ProjectModel projectModel = SQLHelper<ProjectModel>.FindByID(projectID);
                if (projectModel.ID <= 0 || !projectModel.CreatedDate.HasValue) return;
                //ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "ProjectSurvey").FirstOrDefault();
                string pathLocation = Global.PathLocationProject;
                if (Global.IsOnline)
                {
                    //pathLocation = @"\\rtctechnologydata.ddns.net\DUAN\Projects\";
                    pathLocation = @"\\113.190.234.64\DUAN\Projects\";
                }

                try
                {
                    Directory.CreateDirectory(pathLocation);
                }
                catch (Exception)
                {
                    pathLocation = @"\\113.190.234.64\DUAN\Projects\";
                }
               
                string pathPattern = $@"{projectModel.CreatedDate.Value.Year}\{projectModel.ProjectCode}\TaiLieuChung\ThongTinKhaoSat";

                string path = Path.Combine(pathLocation, pathPattern);
                Process.Start(path);

                try
                {
                    Process.Start(path);
                }
                catch
                {
                    pathPattern = $@"{projectModel.CreatedDate.Value.Year}\{projectModel.ProjectCode}\TaiLieuChung\ThongTinKhaoSat";
                    path = Path.Combine(pathLocation, pathPattern);
                    Process.Start(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnProjectSurveyResult_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            int prjSurveyDetailID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectSurveyDetailID));
            bool isPower = Global.EmployeeID == empID || Global.IsAdmin;
            if (!isPower)
            {
                MessageBox.Show("Bạn không được nhập kết quả khảo sát của người khác!", "Thông báo");
                return;
            }

            ProjectSurveyDetailModel detail = SQLHelper<ProjectSurveyDetailModel>.FindByID(prjSurveyDetailID);
            if (detail.ID <= 0)
            {
                MessageBox.Show("Không tìm thấy nội dung khảo sát!", "Thông báo");
                return;
            }
            frmProjectSurveyContentResult frm = new frmProjectSurveyContentResult();
            frm.prjSurveyDetail = detail;
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
