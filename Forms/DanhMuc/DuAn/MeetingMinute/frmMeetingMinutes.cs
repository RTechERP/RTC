using BMS.Model;
//using DevExpress.DXCore.Controls.Utils;
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
    public partial class frmMeetingMinutes : _Forms
    {
        public ProjectModel project = new ProjectModel();
        public frmMeetingMinutes()
        {
            InitializeComponent();
        }

        private void frmMeetingMinutes_Load(object sender, EventArgs e)
        {
            //PQ.Chiến - UPDATE - 15/07/2025
            grdFile.ContextMenuStrip = contextMenuStrip2;
            this.Text = $"BIÊN BẢN CUỘC HỌP - {project.ProjectCode}";
            //END
            DateTime currentDate = DateTime.Now;
            dpkDateStart.Value = new DateTime(currentDate.Year, currentDate.Month, 1);
            dpkDateEnd.Value = dpkDateStart.Value.AddMonths(1).AddDays(-1);
            LoadGroupID();
            LoadMeetingType();
            LoadEmployee();
            LoadProject();
            LoadData();

            LoadUserTeam(); //vtn Update
            LoadProjectProblem(); //vtn Update

            //PQ.Chiến - UPDATE - 15/07/2025
            if (project.ID != 0)
            {
                cboProjectType.Enabled = false;
                grvData.Columns["ProjectCode"].Visible = false;
                grvData.Columns["ProjectName"].Visible = false;
            }
            //END
        }
        private void LoadGroupID()
        {
            List<object> lst = new List<object>()
            {
                new {ID = 1, GroupName = "Nội bộ"},
                new {ID = 2, GroupName = "Khách hàng"}
            };
            cboGroupType.DataSource = lst;
            cboGroupType.ValueMember = "ID";
            cboGroupType.DisplayMember = "GroupName";
        }
        private void LoadMeetingType()
        {
            List<MeetingTypeModel> lst = SQLHelper<MeetingTypeModel>.FindByAttribute("ISNULL(IsDelete,0)", 0);
            cboMeetingType.Properties.DataSource = lst;
            cboMeetingType.Properties.ValueMember = "ID";
            cboMeetingType.Properties.DisplayMember = "TypeName";
        }
        private void LoadData()
        {
            DateTime dateStart = new DateTime(dpkDateStart.Value.Year, dpkDateStart.Value.Month, dpkDateStart.Value.Day, 00, 00, 00);
            DateTime dateEnd = new DateTime(dpkDateEnd.Value.Year, dpkDateEnd.Value.Month, dpkDateEnd.Value.Day, 23, 59, 59);
            int meetingTypeID = TextUtils.ToInt(cboMeetingType.EditValue);
            string keyWords = txtKeywords.Text.Trim();
            //PQ.Chiến - UPDATE - 15/07/2025 - Thêm ProjectID vào store
            DataTable dt = TextUtils.LoadDataFromSP("spGetMeetingMinutes", "lmkTable", new string[] { "@DateStart", "@DateEnd", "@MeetingTypeID", "@Keywords", "@ProjectID"}
                                                                         , new object[] { dateStart, dateEnd, meetingTypeID, keyWords, project.ID});
            //END
            grdData.DataSource = dt;
            LoadDetails();
        }
        private void LoadDetails() //vtn Update
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetMeetingMinutesDetailsByID", new string[] { "@MeetingMinutesID" }, new object[] { id });
            grdContent.DataSource = ds.Tables[1];
            grdAttendance.DataSource = ds.Tables[2];
            grdDetailCustomer.DataSource = ds.Tables[0];
            grdCustomer.DataSource = ds.Tables[3];
            ////PQ.Chiến - UPDATE - 15/07/2025
            grdFile.DataSource = ds.Tables[4];
            //END
        }


        private void LoadUserTeam() //vtn Update
        {
            List<UserTeamModel> ut = SQLHelper<UserTeamModel>.FindAll();
            cboTeam.DisplayMember = "Name";
            cboTeam.ValueMember = "ID";
            cboTeam.DataSource = ut;
        }

        private void LoadProjectProblem() //vtn Update
        {
            List<ProjectHistoryProblemModel> lst = SQLHelper<ProjectHistoryProblemModel>.FindAll();
            cboProjectProblem.DataSource = lst;
            cboProjectProblem.ValueMember = "ID";
            cboProjectProblem.DisplayMember = "ContentError";

            cboProblemCus.DataSource = lst;
            cboProblemCus.ValueMember = "ID";
            cboProblemCus.DisplayMember = "ContentError";
        }


        private void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.DataSource = dt;
            cboEmployee.ValueMember = "ID";
            cboEmployee.DisplayMember = "Code";

            cboEmployeeAttendance.DataSource = dt;
            cboEmployeeAttendance.ValueMember = "ID";
            cboEmployeeAttendance.DisplayMember = "Code";
        }
        private void LoadProject()
        {
            List<ProjectModel> lst = SQLHelper<ProjectModel>.FindAll();
            cboProject.DataSource = lst;
            cboProject.DisplayMember = "ProjectCode";
            cboProject.ValueMember = "ID";

            //PQ.Chien - UPDATE - 15/07/2025
            cboProjectType.Properties.DataSource = lst;
            cboProjectType.Properties.ValueMember = "ID";
            cboProjectType.Properties.DisplayMember = "ProjectCode";
            cboProjectType.EditValue = project.ID;
            //END
        }
        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string title = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTitle));
            MeetingMinutesModel model = SQLHelper<MeetingMinutesModel>.FindByID(id);
            if (model.ID <= 0)
            {
                MessageBox.Show($"Không tìm thấy biên bản họp [{title}]", "Thông báo");
                return;
            }
            frmMeetingMinutesDetails frm = new frmMeetingMinutesDetails();
            frm.meetingMinutes = model;
            //ndnhat update 09/08/2025
            frm.ReloadMeetingType = () =>
            {
                LoadMeetingType();
            };
            //end ndnhat update 09/08/2025
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            frmMeetingMinutesDetails frm = new frmMeetingMinutesDetails();
            //PQ.Chiến - UPDATE - 15/07/2025
            frm.meetingMinutes.ProjectID = project.ID;
            //END
            //ndnhat update 09/08/2025
            frm.ReloadMeetingType = () =>
            {
                LoadMeetingType();
            };
            //end ndnhat update 09/08/2025
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_ItemClick(null, null);
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetails();
            int checkGroupId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colMeetingTypeID));
            var mtType = SQLHelper<MeetingTypeModel>.FindByID(checkGroupId);
            if (mtType != null)
            {

                if (mtType.GroupID == 1)
                {
                    splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                    splitContainerControl2.IsSplitterFixed = true;
                }
                else
                {
                    splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                    splitContainerControl2.IsSplitterFixed = false;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetRowCellValue(rowHandle, grvData.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grvContent_KeyDown(object sender, KeyEventArgs e)
        {
            int rowHandle = grvContent.FocusedRowHandle;
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvContent.GetRowCellValue(rowHandle, grvContent.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grvAttendance_KeyDown(object sender, KeyEventArgs e)
        {
            int rowHandle = grvAttendance.FocusedRowHandle;
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvAttendance.GetRowCellValue(rowHandle, grvAttendance.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) //vtn Update
        {
            int checkGroupId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colMeetingTypeID));
            var mtType = SQLHelper<MeetingTypeModel>.FindByID(checkGroupId);

            int rowHandle = grvData.FocusedRowHandle;
            int masterID = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, colID));
            if (masterID == 0)
            {
                MessageBox.Show("Không tìm thấy biên bản cuộc họp!", "Thông báo");
                return;
            }
            string path = "";
            //FolderBrowserDialog fbd = new FolderBrowserDialog();

            string projectCode = TextUtils.ToString(grvData.GetRowCellValue(rowHandle, colProjectCode));
            DateTime dateStart = TextUtils.ToDate5(grvData.GetRowCellValue(rowHandle, colDateStart));
            DateTime dateEnd = TextUtils.ToDate5(grvData.GetRowCellValue(rowHandle, colDateEnd));

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel Files|*.xlsx";
            //dialog.FileName = $"DanhSachThietBi_{DateTime.Now.ToString("ddMMyy")}.xls";
            dialog.FileName = $"BienBanCuocHop_{projectCode}_{dateStart.ToString("ddMMyyyy_HHmmss")}.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {
                    //path = fbd.SelectedPath;
                    //path = dialog.FileName;

                    string fileSourceName = "Mau_ExportMeetingMinutes.xlsx";
                    //string code = $"BienBanCuocHop_{projectCode}_{dateStart.ToString("ddMMyyyy_HHmm")}";
                    string sourcePath = Application.StartupPath + "\\" + fileSourceName;
                    //string currentPath = path + "\\" + code + ".xlsx";
                    string currentPath = dialog.FileName;
                    try
                    {
                        File.Copy(sourcePath, currentPath, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    Microsoft.Office.Interop.Excel.Application app = default(Microsoft.Office.Interop.Excel.Application);
                    Microsoft.Office.Interop.Excel.Workbook workBoook = default(Microsoft.Office.Interop.Excel.Workbook);
                    Microsoft.Office.Interop.Excel.Worksheet workSheet = default(Microsoft.Office.Interop.Excel.Worksheet);

                    try
                    {
                        string time = $"{dateStart.ToString("HH:mm")} - {dateEnd.ToString("HH:mm")}";
                        string date = $"Date: {dateStart.ToString("dd/MM/yyyy")}";
                        string title = TextUtils.ToString(grvData.GetRowCellValue(rowHandle, colTitle));
                        string place = TextUtils.ToString(grvData.GetRowCellValue(rowHandle, colPlace));

                        app = new Microsoft.Office.Interop.Excel.Application();
                        app.DisplayAlerts = false;

                        app.Workbooks.Open(currentPath);

                        workBoook = app.Workbooks[1];
                        workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBoook.Worksheets[1];

                        workSheet.Cells[6, 2] = time;
                        workSheet.Cells[6, 4] = date;
                        workSheet.Cells[6, 8] = place;
                        workSheet.Cells[7, 2] = title;


                        for (int i = grvContent.RowCount - 1; i >= 0; i--)
                        {
                            workSheet.Cells[14, 1] = TextUtils.ToString(grvContent.GetRowCellValue(i, colDetailSTT));
                            workSheet.Cells[14, 2] = TextUtils.ToString(grvContent.GetRowCellValue(i, colDetailDetailContent));
                            workSheet.Cells[14, 8] = TextUtils.ToString(grvContent.GetRowCellValue(i, colDetailResult));
                            workSheet.Cells[14, 9] = TextUtils.ToString(grvContent.GetRowCellValue(i, colDetailCustomerName));

                            DateTime? detailPlan = TextUtils.ToDate4(grvContent.GetRowCellValue(i, colDetailPlan));
                            string datePlan = detailPlan.HasValue ? detailPlan.Value.ToString("dd/MM/yyyy") : "";
                            //workSheet.Cells[14, 10] = detailPlan.HasValue ? detailPlan.Value.ToString("dd/MM/yyyy") : "";
                            workSheet.Cells[14, 10] = (detailPlan.HasValue ? detailPlan.Value.ToString("dd/MM/yyyy") : "");
                            workSheet.Cells[14, 11] = TextUtils.ToString(grvContent.GetRowCellValue(i, colDetailNote));
                            //workSheet.Cells[14, 11] = TextUtils.ToBoolean(grvContent.GetRowCellValue(i, colIsProblem)) ? "Phát sinh" : "";
                            ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[14]).Insert();
                            MergeExcel(workSheet, "B14", "G14");
                        }

                        if (mtType != null)
                        {
                            if (mtType.GroupID == 2)
                            {
                                for (int i = grvDetailCustomer.RowCount - 1; i >= 0; i--)
                                {
                                    workSheet.Cells[14, 1] = TextUtils.ToString(grvDetailCustomer.GetRowCellValue(i, colDetailSttCus));
                                    workSheet.Cells[14, 2] = TextUtils.ToString(grvDetailCustomer.GetRowCellValue(i, colContentCusDe));
                                    workSheet.Cells[14, 8] = TextUtils.ToString(grvDetailCustomer.GetRowCellValue(i, colResultCusDe));
                                    workSheet.Cells[14, 9] = TextUtils.ToString(grvDetailCustomer.GetRowCellValue(i, colNameCusDe));

                                    DateTime? planCusDe = TextUtils.ToDate4(grvDetailCustomer.GetRowCellValue(i, colPlanCusDe));
                                    string datePlan = planCusDe.HasValue ? planCusDe.Value.ToString("dd/MM/yyyy") : "";
                                    //workSheet.Cells[14, 10] = planCusDe.HasValue ? planCusDe.Value.ToString("dd/MM/yyyy") : "";
                                    workSheet.Cells[14, 10] = (planCusDe.HasValue ? planCusDe.Value.ToString("dd/MM/yyyy") : "");
                                    workSheet.Cells[14, 11] = TextUtils.ToString(grvDetailCustomer.GetRowCellValue(i, colNoteCusDe));
                                    //workSheet.Cells[14, 11] = TextUtils.ToBoolean(grvDetailCustomer.GetRowCellValue(i, colIsProblemDe)) ? "Phát sinh" : "";
                                    ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[14]).Insert();
                                    MergeExcel(workSheet, "B14", "G14");
                                }
                            }
                        }

                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[13]).Delete();
                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[13]).Delete();

                        for (int i = grvAttendance.RowCount - 1; i >= 0; i--)
                        {
                            workSheet.Cells[10, 2] = TextUtils.ToString(grvAttendance.GetRowCellValue(i, colAttendanceCustomerName));
                            workSheet.Cells[10, 4] = TextUtils.ToString(grvAttendance.GetRowCellValue(i, colAttendacePhoneNum));
                            ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[10]).Insert();
                            MergeExcel(workSheet, "B10", "C10");
                            MergeExcel(workSheet, "F10", "G10");
                        }

                        if (mtType != null)
                        {
                            if (mtType.GroupID == 2)
                            {
                                for (int i = grvCustomer.RowCount - 1; i >= 0; i--)
                                {
                                    workSheet.Cells[10, 2] = TextUtils.ToString(grvCustomer.GetRowCellValue(i, colNameCus));
                                    workSheet.Cells[10, 4] = TextUtils.ToString(grvCustomer.GetRowCellValue(i, colSdtCus));
                                    ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[10]).Insert();
                                    MergeExcel(workSheet, "B10", "C10");
                                    MergeExcel(workSheet, "F10", "G10");
                                }
                            }
                        }

                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[9]).Delete();
                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[9]).Delete();


                        Process.Start(currentPath);
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
                }
            }
            else
            {
                return;
            }


        }
        static void MergeExcel(Microsoft.Office.Interop.Excel.Worksheet workSheet, string startRow, string endRow)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Range range = workSheet.Range[startRow, endRow];
                range.Merge();
            }
            catch { }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, colID));
            string title = TextUtils.ToString(grvData.GetRowCellValue(rowHandle, colTitle));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xóa Biên bản cuộc họp [{title}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.No) return;

            Dictionary<string, object> newDict = new Dictionary<string, object>()
            {
                {MeetingMinutesModel_Enum.IsDeleted.ToString(), true },
                {MeetingMinutesModel_Enum.UpdatedBy.ToString(), Global.AppUserName },
                {MeetingMinutesModel_Enum.UpdatedDate.ToString(), DateTime.Now }
            };
            SQLHelper<MeetingMinutesModel>.UpdateFieldsByID(newDict, id);
            LoadData();
        }

        private void btnViewHistoryProblem_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
            if (ID <= 0) return;
            ProjectModel model = SQLHelper<ProjectModel>.FindByID(ID);
            frmHistoryProjectProblem frm = new frmHistoryProjectProblem();
            frm.project = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        ////PQ.Chiến - UPDATE - 15/07/2025
        private void xemFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = TextUtils.ToString(grvFile.GetFocusedRowCellValue(colServerPath));
                string fileName = TextUtils.ToString(grvFile.GetFocusedRowCellValue(colFileName));

                Process.Start(Path.Combine(path, fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tảiFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int projectID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
                //ProjectModel prj = (ProjectModel)cboProject.GetSelectedDataRow();
                ProjectModel prj = SQLHelper<ProjectModel>.FindByID(projectID);
                if (prj == null || !prj.CreatedDate.HasValue) return;

                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", "BienBanCuocHop");

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }

                string pathPattern = $@"{prj.CreatedDate.Value.Year}/{prj.ProjectCode}/TaiLieuChung/BienBanCuocHop";

                string fileName = TextUtils.ToString(grvFile.GetFocusedRowCellValue(colFileName));
                string folderDownload = Path.Combine(pathDownload, fileName);
                string url = $"http://113.190.234.64:8083/api/bienbancuochop/{pathPattern}/{fileName}";

                if (File.Exists(folderDownload))
                {
                    folderDownload = Path.Combine(pathDownload, $"{Path.GetFileNameWithoutExtension(fileName)}_{DateTime.Now.ToString("HHmmss")}.{Path.GetExtension(fileName)}");
                }


                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, folderDownload);
                Process.Start(folderDownload);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }
        //END
    }
}
