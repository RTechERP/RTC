using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml.Office;
using Forms.DanhMuc.JobRequirement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmJobRequirementCheckPrice : _Forms
    {
        List<FileInfo> listFileUpload = new List<FileInfo>();
        List<JobRequirementFileModel> listFiles = new List<JobRequirementFileModel>();
        List<JobRequirementFileModel> listFileDelete = new List<JobRequirementFileModel>();
        public JobRequirementModel model = new JobRequirementModel();
        List<JobRequirementDetailModel> dataDefault = new List<JobRequirementDetailModel>();
        public bool isCopy = false;
        public int jobRequirementID = 0;
        public frmJobRequirementCheckPrice()
        {
            InitializeComponent();
        }

        private void frmJobRequirementDetails_Load(object sender, EventArgs e)
        {
            GetAllDeparment();
            GetAllTBP();
            SetDefaultRow();
            grvData.SetRowCellValue(6, colDescription, dtpDeadlineRequest.Text);
            grvData.RefreshData();
            GetAllEmployee();
            if (model.ID > 0) LoadData();
            LoadDataCheckPrice();
        }
        private void LoadData()
        {
            cboEmployee.EditValue = model.EmployeeID;
            dtpDateOrder.Value = model.DateRequest.HasValue ? model.DateRequest.Value : DateTime.Now;
            dtpDeadlineRequest.Value = model.DeadlineRequest.HasValue ? model.DeadlineRequest.Value : DateTime.Now;
            cboTBP.EditValue = model.ApprovedTBPID;
            cboCoordinationDepartment.EditValue = model.CoordinationDepartmentID;
            cboRequiredDepartment.EditValue = model.RequiredDepartmentID;


            listFiles = SQLHelper<JobRequirementFileModel>.FindByAttribute("JobRequirementID", model.ID);
            grdFileData.DataSource = listFiles;
            grdFileData.Refresh();

            dataDefault = SQLHelper<JobRequirementDetailModel>.FindByAttribute("JobRequirementID", model.ID);
            grdData.DataSource = dataDefault;
            grvData.RefreshData();
        }
        private void LoadDataCheckPrice()
        {
            try
            {
                Dictionary<string, string> suffixCaptions = new Dictionary<string, string>
                {
                    { "OfferPrice", "Giá chào/LS" },
                    { "PurchasePrice", "Đơn giá" },
                    { "ShippingFee", "Phí VC" },
                    { "TotalAmount", "Tổng tiền" },
                    { "LeadTime", "LeadTime" },
                    { "VAT", "VAT" },
                    { "Supplier", "Nhà cung cấp" },
                    { "Status", "Trạng thái" },
                };
                List<string> hiddenSuffixes = new List<string>
                {
                    "ID",
                };
                DataTable dt = TextUtils.LoadDataFromSP(
                    "spGetJobRequirementCheckPrice",
                    "A",
                    new string[] { "@JobRequirementID" },
                    new object[] { jobRequirementID });

                //for (int i = bandNCC.Children.Count - 1; i >= 0; i--)
                //{
                //    if (bandNCC.Children[i].Tag != null && bandNCC.Children[i].Tag.ToString() == "Dynamic")
                //    {
                //        bandNCC.Children.RemoveAt(i);
                //    }
                //}

                //for (int i = bandNCC.Children.Count - 1; i >= 0; i--)
                //{
                //    var col = bandNCC.Children[i];
                //    if (col.FieldName.StartsWith("Cps"))
                //    {
                //        bandNCC.Children.Remove(col);
                //    }
                //}

                for (int i = bandNCC.Children.Count - 1; i >= 0; i--)
                {
                    var band = bandNCC.Children[i];

                    for (int j = band.Columns.Count - 1; j >= 0; j--)
                    {
                        var bandCol = band.Columns[j];
                        if (bandCol != null && !string.IsNullOrEmpty(bandCol.FieldName) && bandCol.FieldName.StartsWith("Cps"))
                        {
                            grvCheckPrice.Columns.Remove(bandCol);
                        }
                    }

                    if (band.Tag != null && band.Tag.ToString() == "Dynamic")
                    {
                        bandNCC.Children.RemoveAt(i);
                    }
                }


                grdCheckPrice.DataSource = dt;

                //// Tìm các cột động (Cps1_, Cps2_, ...)
                var cpsColumns = dt.Columns.Cast<DataColumn>()
                                    .Where(c => c.ColumnName.StartsWith("Cps"))
                                    .Select(c => c.ColumnName)
                                    .ToList();

                var groups = cpsColumns.GroupBy(c => c.Split('_')[0]);

                foreach (var g in groups.OrderBy(x => x.Key))
                {
                    //Tạo band
                    string bandIndex = g.Key.Substring(3);
                    string bandCaption = $"Nhà cung cấp {bandIndex}";

                    //GridBand cpsBand = grvCheckPrice.Bands.AddBand(bandCaption);
                    GridBand cpsBand = bandNCC.Children.AddBand(bandCaption);

                    cpsBand.AppearanceHeader.Font = new Font("Tahoma", 9F, FontStyle.Bold);
                    cpsBand.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
                    cpsBand.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    cpsBand.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;

                    cpsBand.Tag = "Dynamic"; // đánh dấu band là dynamic

                    foreach (var colName in g)
                    {
                        if (!grvCheckPrice.Columns.Contains(grvCheckPrice.Columns[colName]))
                        {
                            var gridCol = grvCheckPrice.Columns.AddField(colName);
                            gridCol.Visible = true;

                            //Đổi caption từ fieldName sang tên 
                            var parts = colName.Split('_');
                            string suffix = parts.Length > 1 ? parts[1] : colName;

                            if (suffixCaptions.ContainsKey(suffix))
                                gridCol.Caption = suffixCaptions[suffix];
                            else
                                gridCol.Caption = suffix;

                            gridCol.Visible = !hiddenSuffixes.Contains(suffix);

                            gridCol.AppearanceHeader.Font = new Font("Tahoma", 9F, FontStyle.Bold);
                            gridCol.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
                            gridCol.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                            gridCol.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;

                            cpsBand.Columns.Add(gridCol);
                        }
                    }
                }

                grvCheckPrice.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void SetDefaultRow()
        {
            dataDefault = new List<JobRequirementDetailModel>();
            dataDefault.Add(new JobRequirementDetailModel() { STT = 1, Category = "Nội dung yêu cầu" });
            dataDefault.Add(new JobRequirementDetailModel() { STT = 2, Category = "Người yêu cầu" });
            dataDefault.Add(new JobRequirementDetailModel() { STT = 3, Category = "Lý do" });
            dataDefault.Add(new JobRequirementDetailModel() { STT = 4, Category = "Số lượng" });
            dataDefault.Add(new JobRequirementDetailModel() { STT = 5, Category = "Chất lượng" });
            dataDefault.Add(new JobRequirementDetailModel() { STT = 6, Category = "Địa điểm" });
            dataDefault.Add(new JobRequirementDetailModel() { STT = 7, Category = "Thời gian hoàn thành đề nghị" });
            grdData.DataSource = dataDefault;
        }
        private void GetAllEmployee()
        {
            var data = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = data;
            cboEmployee.EditValue = Global.EmployeeID;
        }
        private void GetAllDeparment()
        {
            var data = SQLHelper<DepartmentModel>.FindAll();
            cboCoordinationDepartment.Properties.DataSource = data;
            cboCoordinationDepartment.Properties.ValueMember = "ID";
            cboCoordinationDepartment.Properties.DisplayMember = "Name";

            cboRequiredDepartment.Properties.DataSource = data;
            cboRequiredDepartment.Properties.ValueMember = "ID";
            cboRequiredDepartment.Properties.DisplayMember = "Name";


            cboDepartment.Properties.DataSource = data;
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.EditValue = Global.DepartmentID;
        }

        private void GetAllTBP()
        {
            var exp = new Expression("Type", 3);
            List<EmployeeApproveModel> list = SQLHelper<EmployeeApproveModel>.FindByExpression(exp);
            cboTBP.Properties.DataSource = list;
            cboTBP.Properties.ValueMember = "EmployeeID";
            cboTBP.Properties.DisplayMember = "FullName";
        }
        void LoadFile(List<JobRequirementFileModel> listFiles)
        {
            grdFileData.DataSource = listFiles;
            grvFileData.RefreshData();
        }
        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    JobRequirementFileModel fileRequest = new JobRequirementFileModel()
                    {
                        FileName = fileInfo.Name,
                        OriginPath = fileInfo.DirectoryName
                    };

                    listFiles.Insert(0, fileRequest);
                    listFileUpload.Add(fileInfo);
                }
                LoadFile(listFiles);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvFileData.GetFocusedRowCellValue("ID"));
            string fileName = TextUtils.ToString(grvFileData.GetFocusedRowCellValue("FileName"));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file đính kèm [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvFileData.DeleteSelectedRows();
                if (id <= 0) return;
                JobRequirementFileModel file = SQLHelper<JobRequirementFileModel>.FindByID(id);
                listFileDelete.Add(file);
            }
        }
        public async void RemoveFile()
        {
            if (listFileDelete.Count <= 0) return;
            var url = $"http://113.190.234.64:8083/api/Home/removefile?path=";
            //var url = $"http://localhost:8390/api/Home/removefile?path=";
            var client = new HttpClient();
            foreach (var item in listFileDelete)
            {
                url += $@"{item.ServerPath}\{item.FileName}";
                var result = await client.GetAsync(url);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    SQLHelper<JobRequirementFileModel>.Delete(item);
                }
            }
            listFileDelete = new List<JobRequirementFileModel>();
        }
        public async void UploadFile(int Id)
        {
            try
            {
                if (listFileUpload.Count <= 0) return;

                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathJobRequirement").FirstOrDefault();
                if (config == null || string.IsNullOrEmpty(config.KeyValue))
                {
                    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                    return;
                }

                JobRequirementModel order = SQLHelper<JobRequirementModel>.FindByID(Id);
                if (order == null || order.ID <= 0) return;
                //if (order.EmployeeID != Global.EmployeeID) return;

                string pathServer = config.KeyValue.Trim();
                string pathPattern = $@"NĂM {order.DateRequest.Value.Year}\YÊU CẦU CÔNG VIỆC\THÁNG {order.DateRequest.Value.ToString("MM.yyyy")}\{order.DateRequest.Value.ToString("dd.MM.yyyy")}\{order.NumberRequest}";
                string pathUpload = Path.Combine(pathServer, pathPattern);

                var client = new HttpClient();

                List<JobRequirementFileModel> listFiles = new List<JobRequirementFileModel>();
                foreach (var file in listFileUpload)
                {
                    JobRequirementFileModel fileOrder = new JobRequirementFileModel();
                    fileOrder.JobRequirementID = order.ID;
                    fileOrder.FileName = file.Name;
                    fileOrder.OriginPath = file.DirectoryName;
                    fileOrder.ServerPath = pathUpload;

                    if (file.Length < 0) continue;

                    var fileStream = new FileStream(file.FullName, FileMode.Open);
                    byte[] bytes = new byte[file.Length];
                    fileStream.Read(bytes, 0, (int)file.Length);
                    var byteArrayContent = new ByteArrayContent(bytes);

                    MultipartFormDataContent content = new MultipartFormDataContent();
                    content.Add(byteArrayContent, "file", file.Name);

                    var url = $"http://113.190.234.64:8083/api/Home/uploadfile?path={pathUpload}";
                    var result = await client.PostAsync(url, content);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SQLHelper<JobRequirementFileModel>.Insert(fileOrder);
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void grvData_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //int rowFocus = grvData.FocusedRowHandle;
            //string oldValue = dtpDeadlineRequest.Text;
            //if (rowFocus == 6 && e.Column == colDescription)
            //{
            //    grvData.SetFocusedRowCellValue(colDescription, oldValue);
            //    grvData.FocusedRowHandle = -1;
            //    MessageBox.Show("Thay đổi [Ngày hoàn thành] để thay đổi [Diễn giải - Thời gian hoàn thành đề nghị]!");
            //}
        }

        private void dtpDeadlineReques_ValueChanged(object sender, EventArgs e)
        {
            grvData.SetRowCellValue(6, colDescription, dtpDeadlineRequest.Text);
        }

        private bool ValidatedYCCH()
        {
            grvData.FocusedRowHandle = -1;
            grvFileData.FocusedRowHandle = -1;
            if (TextUtils.ToInt(cboTBP.EditValue) <= 0)
            {
                MessageBox.Show("Hãy chọn [Trưởng bộ phận duyệt]!");
                return false;
            }
            //if (TextUtils.ToInt(cboCoordinationDepartment.EditValue) <= 0)
            //{
            //    MessageBox.Show("Hãy chọn [Bộ phận phối hợp]!");
            //    return false;
            //}
            if (TextUtils.ToInt(cboRequiredDepartment.EditValue) <= 0)
            {
                MessageBox.Show("Hãy chọn [Bộ phận được yêu cầu]!");
                return false;
            }

            //if (string.IsNullOrWhiteSpace(TextUtils.ToString(dtpDeadlineRequest.Text).Trim()))
            //{
            //    MessageBox.Show("Hãy chọn [Thời gian hoàn thành]!");
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(TextUtils.ToString(dtpDateOrder.Text).Trim()))
            //{
            //    MessageBox.Show("Hãy chọn [Ngày yêu cầu]!");
            //    return false;
            //}

            //DateTime dateOrder = TextUtils.ToDate(dtpDateOrder.Text);
            //DateTime deadlineReques = TextUtils.ToDate(dtpDeadlineRequest.Text);
            if (dtpDateOrder.Value.Date > dtpDeadlineRequest.Value.Date)
            {
                MessageBox.Show("[Thời gian hoàn thành] phải > [Ngày yêu cầu]");
                return false;
            }

            return true;
        }
        private void Reset()
        {
            SetDefaultRow();
            grdData.Refresh();
            listFileUpload = new List<FileInfo>();
            listFiles = new List<JobRequirementFileModel>();
            listFileDelete = new List<JobRequirementFileModel>();
            cboCoordinationDepartment.EditValue = null;
            cboRequiredDepartment.EditValue = null;
            cboTBP.EditValue = null;
            dtpDateOrder.Value = DateTime.Now;
            dtpDeadlineRequest.Value = DateTime.Now;
        }
        private bool Save()
        {
            bool isCreated = false;
            if (!ValidatedYCCH()) return false;
            model.DateRequest = dtpDateOrder.Value;
            model.DeadlineRequest = dtpDeadlineRequest.Value;
            model.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            model.CoordinationDepartmentID = TextUtils.ToInt(cboCoordinationDepartment.EditValue);
            model.RequiredDepartmentID = TextUtils.ToInt(cboRequiredDepartment.EditValue);
            model.ApprovedTBPID = TextUtils.ToInt(cboTBP.EditValue);

            if (model.ID > 0)
            {
                //JobRequirementBO.Instance.Update(model);
                SQLHelper<JobRequirementModel>.Update(model);
            }
            else
            {
                isCreated = true;

                int currentYear = DateTime.Now.Year;
                //List<JobRequirementModel> list = SQLHelper<JobRequirementModel>.SqlToList($"SELECT * FROM JobRequirements WHERE YEAR(DateRequest) = {currentYear}");
                List<JobRequirementModel> list = SQLHelper<JobRequirementModel>.FindByAttribute("YEAR(DateRequest)", currentYear);
                model.NumberRequest = $"{list.Count + 1}.{currentYear}.PYC-RTC";
                //model.ID = TextUtils.ToInt(JobRequirementBO.Instance.Insert(model));
                model.ID = SQLHelper<JobRequirementModel>.Insert(model).ID;
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                JobRequirementDetailModel detail = new JobRequirementDetailModel();
                if (id > 0)
                {
                    detail = SQLHelper<JobRequirementDetailModel>.FindByID(id);
                }

                detail.JobRequirementID = model.ID;
                detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                detail.Category = TextUtils.ToString(grvData.GetRowCellValue(i, colCategory));
                detail.Description = TextUtils.ToString(grvData.GetRowCellValue(i, colDescription));
                detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                detail.Target = TextUtils.ToString(grvData.GetRowCellValue(i, colTarget));

                if (detail.ID > 0)
                {
                    //JobRequirementDetailBO.Instance.Update(detail);
                    SQLHelper<JobRequirementDetailModel>.Update(detail);
                }
                else
                {
                    //detail.ID = TextUtils.ToInt(JobRequirementDetailBO.Instance.Insert(detail));
                    detail.ID = SQLHelper<JobRequirementDetailModel>.Insert(detail).ID;
                }
            }


            UploadFile(model.ID);
            RemoveFile();
            CreateApprove(TextUtils.ToInt(cboTBP.EditValue), model);


            //Add Notify
            string textNotify = $"Yêu cầu duyệt\n" +
                                $"Số yêu cầu: {model.NumberRequest}\n" +
                                $"Thời gian cần hoàn thành: {model.DeadlineRequest.Value.ToString("dd/MM/yyyy")}";

            TextUtils.AddNotify("YÊU CẦU CÔNG VIỆC", textNotify, TextUtils.ToInt(model.ApprovedTBPID));
            return true;
        }



        public void CreateApprove(int approvedTBPID, JobRequirementModel model)
        {
            string _typeHanhChinh = "2,3,4,5,6,7";
            string _typeHR = "1,4,5";
            string[] stepNames = new string[] { "", "Tạo phiếu yêu cầu công việc", "TBP xác nhận", "HR check yêu cầu", "TBP HR xác nhận", "BGĐ xác nhận" };

            try
            {
                var list = SQLHelper<JobRequirementApprovedModel>.FindAll().Where(x => x.JobRequirementID == model.ID);
                foreach (var item in list)
                {
                    SQLHelper<JobRequirementApprovedModel>.Delete(item);
                }

                List<JobRequirementApprovedModel> listLog = new List<JobRequirementApprovedModel>();
                var departments = SQLHelper<DepartmentModel>.FindAll();

                int headOfHR = departments.FirstOrDefault(x => x.Code == "HR") == null ? 0 : TextUtils.ToInt(departments.FirstOrDefault(x => x.Code == "HR").HeadofDepartment);
                int headOfKT = departments.FirstOrDefault(x => x.Code == "KT") == null ? 0 : TextUtils.ToInt(departments.FirstOrDefault(x => x.Code == "KT").HeadofDepartment);

                var typeHanhChinh = _typeHanhChinh.Split(',');
                var typeHR = _typeHR.Split(',');

                listLog.Add(new JobRequirementApprovedModel() { JobRequirementID = model.ID, Step = 1, StepName = stepNames[1], DateApproved = DateTime.Now, IsApproved = 1, ApprovedID = Global.EmployeeID, ApprovedActualID = Global.EmployeeID, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                listLog.Add(new JobRequirementApprovedModel() { JobRequirementID = model.ID, Step = 2, StepName = stepNames[2], DateApproved = null, IsApproved = 0, ApprovedID = approvedTBPID, ApprovedActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                listLog.Add(new JobRequirementApprovedModel() { JobRequirementID = model.ID, Step = 3, StepName = stepNames[3], DateApproved = null, IsApproved = 0, ApprovedID = headOfHR, ApprovedActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                listLog.Add(new JobRequirementApprovedModel() { JobRequirementID = model.ID, Step = 4, StepName = stepNames[4], DateApproved = null, IsApproved = 0, ApprovedID = headOfHR, ApprovedActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                listLog.Add(new JobRequirementApprovedModel() { JobRequirementID = model.ID, Step = 5, StepName = stepNames[5], DateApproved = null, IsApproved = 0, ApprovedID = 1, ApprovedActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });

                //Check nếu là TBP là người tạo đề nghị
                var tbpApproved = SQLHelper<EmployeeApproveModel>.FindAll().Where(x => x.EmployeeID == Global.EmployeeID && x.Type == 3).ToList();
                if (tbpApproved.Count > 0)
                {
                    var logTBP = listLog.FirstOrDefault(x => x.Step == 2);
                    if (logTBP != null)
                    {
                        logTBP.IsApproved = 1;
                        logTBP.ApprovedID = Global.EmployeeID;
                        logTBP.ApprovedActualID = Global.EmployeeID;
                        logTBP.DateApproved = DateTime.Now;
                    }
                }
                foreach (JobRequirementApprovedModel item in listLog)
                {
                    SQLHelper<JobRequirementApprovedModel>.Insert(item);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void frmJobRequirementDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmJobRequirementDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (grvData.FocusedColumn == colDescription && grvData.FocusedRowHandle == 3)
            {
                string value = TextUtils.ToString(e.Value);
                if (string.IsNullOrWhiteSpace(value)) return;
                string pattern = @"^-?\d+(\.\d+)?$";

                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(value))
                {
                    grvData.BeginUpdate();
                    e.Valid = false;
                    e.ErrorText = "Vui lòng chỉ nhập số!";
                    grvData.EndUpdate();
                }

            }
            //else if (grvData.FocusedColumn == colDescription && grvData.FocusedRowHandle == 6)
            //{
            //    if (rowFocus == 6 && e.Column == colDescription)
            //    {
            //        grvData.SetFocusedRowCellValue(colDescription, oldValue);
            //        grvData.FocusedRowHandle = -1;
            //        MessageBox.Show("Thay đổi [Ngày hoàn thành] để thay đổi [Diễn giải - Thời gian hoàn thành đề nghị]!");
            //    }
            //}
        }

        private void grvData_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (grvData.FocusedColumn == colDescription && grvData.FocusedRowHandle == 6) e.Cancel = true;

        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            frmJobRequirementCheckPriceDetail frm = new frmJobRequirementCheckPriceDetail();
            var exp1 = new Expression("JobRequirementID", jobRequirementID);
            JobRequirementCheckPriceModel model = SQLHelper<JobRequirementCheckPriceModel>.FindByExpression(exp1).FirstOrDefault() ?? new JobRequirementCheckPriceModel();
            frm.jobRequirementID = jobRequirementID;
            frm.jobRequirementCheckPriceModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataCheckPrice();
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"CheckGia_{DateTime.Now.ToString("ddMMyy")}.xlsx";

            if (f.ShowDialog() == DialogResult.OK)
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {
                    //string filepath = Path.Combine(f.SelectedPath, $"");
                    string filepath = f.FileName;

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    printableComponentLink1.Component = grdCheckPrice;

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
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            var dataRow = (DataRowView)cboEmployee.GetSelectedDataRow();
            if (dataRow != null)
            {
                cboDepartment.EditValue = dataRow["DepartmentID"];
            }


            grvData.SetRowCellValue(1, colDescription, cboEmployee.Text);
        }
    }

}
