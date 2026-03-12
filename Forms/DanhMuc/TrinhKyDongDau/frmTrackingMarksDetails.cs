using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmTrackingMarksDetails : _Forms
    {
        public TrackingMarksModel trackingMark = new TrackingMarksModel();
        List<FileInfo> listFileUpload = new List<FileInfo>();
        List<TrackingMarksFileModel> listFileDelete = new List<TrackingMarksFileModel>();
        List<TrackingMarksFileModel> listFiles = new List<TrackingMarksFileModel>();
        public frmTrackingMarksDetails()
        {
            InitializeComponent();
        }

        private void frmTrackingMarksDetails_Load(object sender, EventArgs e)
        {
            LoadDepartment();
            LoadEmployee();
            LoadTypeDocument();
            LoadDetails();
        }
        private void LoadTypeDocument()
        {
            List<DocumentTypeModel> lstTD = new List<DocumentTypeModel>();
            lstTD = SQLHelper<DocumentTypeModel>.FindAll();
            cboDocumentType.DataSource = lstTD;
            cboDocumentType.DisplayMember = "Name";
            cboDocumentType.ValueMember = "ID";
        }
        private void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.DataSource = dt;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";

            // Người ký chính
            cboEmployeeSign.Properties.DataSource = dt;
            cboEmployeeSign.Properties.DisplayMember = "FullName";
            cboEmployeeSign.Properties.ValueMember = "ID";
        }
        private void LoadDepartment()
        {
            List<DepartmentModel> lstD = new List<DepartmentModel>();
            lstD = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.DataSource = lstD;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
        }
        private void LoadDetails()
        {
            DataSet tables = TextUtils.LoadDataSetFromSP("spGetTrackingMarksDetails", new string[] { "@TrackingMarksID" }, new object[] { trackingMark.ID });

            grdSealRegulation.DataSource = tables.Tables[0];
            grdTaxCompany.DataSource = tables.Tables[1];
            if (trackingMark.ID > 0)
            {
                listFiles = SQLHelper<TrackingMarksFileModel>.FindByAttribute("TrackingMarksID", trackingMark.ID);
                grdFileData.DataSource = listFiles;
                cboEmployee.EditValue = trackingMark.EmployeeID;
                EmployeeModel emp = SQLHelper<EmployeeModel>.FindByID(trackingMark.EmployeeID);
                cboDepartment.EditValue = emp.DepartmentID;
                dtpRegisterDate.Value = trackingMark.RegisterDate ?? DateTime.Now;
                cboEmployeeSign.EditValue = trackingMark.EmployeeSignID;
                cboDocumentType.SelectedValue = trackingMark.DocumentTypeID;
                txtDocumentName.Text = trackingMark.DocumentName;
                txtDocumentQuantity.Text = TextUtils.ToString(trackingMark.DocumentQuantity);
                txtDocumentTotalPage.Text = TextUtils.ToString(trackingMark.DocumentTotalPage);
                dtpDeadline.EditValue = trackingMark.Deadline;
                chkIsUrgent.Checked = trackingMark.IsUrgent;
            }
            else
            {
                cboEmployee.EditValue = Global.EmployeeID;
                cboDepartment.EditValue = Global.DepartmentID;
            }
        }

        void LoadFile(List<TrackingMarksFileModel> listFiles)
        {
            grdFileData.DataSource = listFiles;
            grvFileData.RefreshData();
        }
        private void Reset()
        {
            trackingMark = new TrackingMarksModel();
            listFileUpload = new List<FileInfo>();
            listFileDelete = new List<TrackingMarksFileModel>();
            listFiles = new List<TrackingMarksFileModel>();

            dtpRegisterDate.Value = DateTime.Now;
            cboEmployee.EditValue = Global.EmployeeID;
            cboDocumentType.SelectedValue = Global.DepartmentID;
            txtDocumentName.Clear();
            txtDocumentQuantity.Reset();
            txtDocumentTotalPage.Reset();
            cboEmployeeSign.EditValue = 0;

            for (int i = 0; i < grvSealRegulation.RowCount; i++)
            {
                grvSealRegulation.SetRowCellValue(i, colSealIsCheck, false);
            }

            for (int i = 0; i < grvTaxCompany.RowCount; i++)
            {
                grvTaxCompany.SetRowCellValue(i, colTaxIsCheck, false);
            }
        }
        private bool SaveData()
        {
            bool validate = CheckValidate();
            if (validate)
            {
                TrackingMarksModel newModel = SQLHelper<TrackingMarksModel>.FindByID(trackingMark.ID) ?? new TrackingMarksModel();

                newModel.RegisterDate = dtpRegisterDate.Value;
                newModel.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
                newModel.DocumentTypeID = TextUtils.ToInt(cboDocumentType.SelectedValue);
                newModel.DocumentName = txtDocumentName.Text.Trim();
                newModel.DocumentQuantity = TextUtils.ToInt(txtDocumentQuantity.Text);
                newModel.DocumentTotalPage = TextUtils.ToInt(txtDocumentTotalPage.Text);
                newModel.EmployeeSignID = TextUtils.ToInt(cboEmployeeSign.EditValue);
                newModel.Deadline = TextUtils.ToDate4(dtpDeadline.EditValue);
                newModel.IsUrgent = newModel.Deadline.HasValue;

                if (newModel.ID > 0)
                {
                    SQLHelper<TrackingMarksModel>.Update(newModel);
                }
                else
                {
                    newModel.ID = SQLHelper<TrackingMarksModel>.Insert(newModel).ID;
                }

                SQLHelper<TrackingMarksSealModel>.DeleteByAttribute("TrackingMartkID", newModel.ID);
                for (int i = 0; i < grvSealRegulation.RowCount; i++)
                {
                    bool isCheck = TextUtils.ToBoolean(grvSealRegulation.GetRowCellValue(i, colSealIsCheck));
                    if (isCheck)
                    {
                        TrackingMarksSealModel newSeal = new TrackingMarksSealModel()
                        {
                            SealID = TextUtils.ToInt(grvSealRegulation.GetRowCellValue(i, colSealID)),
                            TrackingMartkID = newModel.ID
                        };
                        SQLHelper<TrackingMarksSealModel>.Insert(newSeal);
                    }
                }

                SQLHelper<TrackingMarksTaxCompanyModel>.DeleteByAttribute("TrackingMartkID", newModel.ID);
                for (int i = 0; i < grvTaxCompany.RowCount; i++)
                {
                    bool isCheck = TextUtils.ToBoolean(grvTaxCompany.GetRowCellValue(i, colTaxIsCheck));
                    if (isCheck)
                    {
                        TrackingMarksTaxCompanyModel newSeal = new TrackingMarksTaxCompanyModel()
                        {
                            TaxCompanyID = TextUtils.ToInt(grvTaxCompany.GetRowCellValue(i, colTaxID)),
                            TrackingMartkID = newModel.ID
                        };
                        SQLHelper<TrackingMarksTaxCompanyModel>.Insert(newSeal);
                    }
                }
                UploadFile(newModel.ID);
                RemoveFile();
            }
            return validate;
        }



        private bool CheckValidate()
        {
            grvSealRegulation.FocusedRowHandle = -1;
            grvTaxCompany.FocusedRowHandle = -1;

            int EmployeeId = TextUtils.ToInt(cboEmployee.EditValue);
            DateTime RegisterDate = dtpRegisterDate.Value;
            int EmployeeSign = TextUtils.ToInt(cboEmployeeSign.EditValue);
            int DocumentTypeID = TextUtils.ToInt(cboDocumentType.SelectedValue);
            string DocumentName = txtDocumentName.Text;
            int DocumentQuantity = TextUtils.ToInt(txtDocumentQuantity.EditValue);
            int DocumentTotalPage = TextUtils.ToInt(txtDocumentTotalPage.EditValue);

            if (trackingMark.ID > 0 && trackingMark.Status > 0)
            {
                MessageBox.Show("Phiếu theo dõi sổ đóng dấu đã được duyệt! Không thể sửa!!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (EmployeeId <= 0)
            {
                MessageBox.Show("Vui lòng nhập [Tên nhân viên]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (RegisterDate == null)
            {
                MessageBox.Show("Vui lòng nhập [Ngày đăng ký]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //if (EmployeeSign <= 0)
            //{
            //    MessageBox.Show("Vui lòng nhập [Người ký chính]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            if (DocumentTypeID <= 0)
            {
                MessageBox.Show("Vui lòng nhập [Loại văn bản]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(DocumentName) || DocumentName.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập [Tên văn bản]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (DocumentQuantity <= 0)
            {
                MessageBox.Show("Vui lòng nhập [Số lượng bản]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //if (DocumentTotalPage <= 0)
            //{
            //    MessageBox.Show("Vui lòng nhập [Số tờ/bản]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            int countTax = 0;
            for (int i = 0; i < grvTaxCompany.RowCount; i++)
            {
                if (TextUtils.ToBoolean(grvTaxCompany.GetRowCellValue(i, colTaxIsCheck)))
                {
                    countTax++;
                }
            }
            if (countTax <= 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 [Công ty]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int countSeal = 0;
            for (int i = 0; i < grvSealRegulation.RowCount; i++)
            {
                if (TextUtils.ToBoolean(grvSealRegulation.GetRowCellValue(i, colSealIsCheck)))
                {
                    countSeal++;
                }
            }
            if (countSeal <= 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 [Dấu]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //if (grvFileData.RowCount <= 0)
            //{
            //    MessageBox.Show("Vui lòng chọn ít nhất 1 file đính kèm!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}


            DateTime? deadline = TextUtils.ToDate4(dtpDeadline.EditValue);
            if (chkIsUrgent.Checked && !deadline.HasValue)
            {
                MessageBox.Show("Vui lòng nhập Hạn đóng dấu!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    TrackingMarksFileModel fileRequest = new TrackingMarksFileModel()
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

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int id = TextUtils.ToInt(grvFileData.GetFocusedRowCellValue("ID"));
            string fileName = TextUtils.ToString(grvFileData.GetFocusedRowCellValue("FileName"));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file đính kèm [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvFileData.DeleteSelectedRows();
                if (id <= 0) return;
                TrackingMarksFileModel file = SQLHelper<TrackingMarksFileModel>.FindByID(id);
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
                    SQLHelper<TrackingMarksFileModel>.Delete(item);
                }
            }
            listFileDelete = new List<TrackingMarksFileModel>();
        }
        public async void UploadFile(int Id)
        {
            grvFileData.FocusedRowHandle = -1;
            try
            {
                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "TrackingMarks").FirstOrDefault();
                if (config == null || string.IsNullOrEmpty(config.KeyValue))
                {
                    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                    return;
                }

                TrackingMarksModel trackingMarks = SQLHelper<TrackingMarksModel>.FindByID(Id);
                if (trackingMarks == null || trackingMarks.ID <= 0) return;
                if (trackingMarks.EmployeeID != Global.EmployeeID && !Global.IsAdmin) return;

                string pathServer = config.KeyValue.Trim();
                string pathPattern = $@"NĂM {trackingMarks.RegisterDate.Value.Year}\THÁNG {trackingMarks.RegisterDate.Value.ToString("MM.yyyy")}\{trackingMarks.RegisterDate.Value.ToString("dd.MM.yyyy")}";
                string pathUpload = Path.Combine(pathServer, pathPattern);

                var client = new HttpClient();

                List<TrackingMarksFileModel> listFiles = new List<TrackingMarksFileModel>();
                foreach (var file in listFileUpload)
                {
                    TrackingMarksFileModel fileOrder = new TrackingMarksFileModel();
                    fileOrder.TrackingMarksID = trackingMark.ID;
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
                        SQLHelper<TrackingMarksFileModel>.Insert(fileOrder);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                Reset();
                MessageBox.Show("Lưu thành công!", "Thông báo");
            }
        }

        private void frmTrackingMarksDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void chkIsUrgent_CheckedChanged(object sender, EventArgs e)
        {
            labelControl9.Visible = chkIsUrgent.Checked;
        }
    }
}
