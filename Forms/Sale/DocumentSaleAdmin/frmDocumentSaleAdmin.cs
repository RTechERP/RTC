using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BMS;
using BMS.Model;
using BMS.Business;
using System.Net;
//using Forms.QuanLyVanBan;
//using Forms.Sale.DocumentSaleAdmin;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection.Emit;
using Newtonsoft.Json.Linq;

namespace BMS
{
    public partial class frmDocumentSaleAdmin : _Forms
    {
        private List<string> listFilename = new List<string>();
        public frmDocumentSaleAdmin()
        {
            InitializeComponent();
        }

        void LoadDepartment()
        {
            List<DepartmentModel> listDepart = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboDepartment.Properties.DataSource = listDepart;
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";

            cboDepartment.EditValue = Global.DepartmentID;
        }

        void loadData()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetDocumentSaleAdmin", "A", new string[] { "@GroupType", "@DepartmentID" }, new object[] { 2, departmentID });
            grdDocument.DataSource = dt;
        }

        private void frmDocumentSaleAdmin_Load(object sender, EventArgs e)
        {
            LoadDepartment();
            loadData();
        }
        void dataDocumentFile()
        {
            int ID = TextUtils.ToInt(grvDocument.GetFocusedRowCellValue(colID));
            if (ID == 0) return;

            List<DocumentFileModel> listDocFile = SQLHelper<DocumentFileModel>.FindByAttribute("DocumentID", ID);
            grdDocumentFile.DataSource = listDocFile;
        }

        private void grvDocument_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dataDocumentFile();
        }
        void SaveEventCallBack()
        {
            loadData();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmDocumentSaleAdminDetail frm = new frmDocumentSaleAdminDetail();
            frm.Group = new DocumentModel();
            //frm.Group.DepartmentID = 3;
            frm.SaveEvent += SaveEventCallBack;
            frm.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvDocument.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                TextUtils.ShowError("Vui lòng chọn một dòng để sửa!");
                return;
            }
            DocumentModel model = SQLHelper<DocumentModel>.FindByID(id);
            if (model == null)
            {
                TextUtils.ShowError("Vui lòng chọn một dòng để sửa!");
                return;
            }
            frmDocumentSaleAdminDetail frm = new frmDocumentSaleAdminDetail();
            frm.Group = model;
            frm.SaveEvent += SaveEventCallBack;
            frm.Show();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvDocument.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                TextUtils.ShowError("Vui lòng chọn một dòng để xóa!");
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //DocumentModel model = SQLHelper<DocumentModel>.FindByID(id);
                var myDict = new Dictionary<string, object>()
                {
                    {DocumentModel_Enum.IsDeleted.ToString(),true },
                    {DocumentModel_Enum.UpdatedBy.ToString(),Global.AppUserName },
                    {DocumentModel_Enum.UpdatedDate.ToString(),DateTime.Now }
                };

                SQLHelper<DocumentModel>.UpdateFieldsByID(myDict, id);
                loadData();
            }
            else
            {
                return;
            }

        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            UploadFile();
            //OpenFileDialog o = new OpenFileDialog { Multiselect = true };
            //if (o.ShowDialog() != DialogResult.OK) return;

            //foreach (string filePath in o.FileNames)
            //{
            //    try
            //    {
            //        string directory = Path.GetDirectoryName(filePath);
            //        string name = Path.GetFileNameWithoutExtension(filePath);
            //        string extension = Path.GetExtension(filePath);
            //        string newFileName = $"{name}_{DateTime.Now:ddMMyyHHmm}{extension}";
            //        string newFilePath = Path.Combine(directory, newFileName);

            //        File.Copy(filePath, newFilePath);

            //        if (DocumentFileBO.Instance.FindByCode("FileName", newFileName) != null)
            //        {
            //            MessageBox.Show($"Tên File {name} đã tồn tại trên hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            continue;
            //        }

            //        DocumentFileModel modelFile = new DocumentFileModel
            //        {
            //            FileName = newFileName,
            //            FileNameOrigin = name + extension,
            //            FilePath = newFilePath,
            //            DocumentID = TextUtils.ToInt(grvDocument.GetFocusedRowCellValue(colID))
            //        };

            //        DocumentFileBO.Instance.Insert(modelFile);
            //        listFilename.Add(newFilePath);

            //        // Gửi lên server
            //        bool isLastFile = filePath == o.FileNames.Last();
            //        UploadFile(newFilePath, isLastFile);

            //        // Load lại dữ liệu
            //        dataDocumentFile();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }
        public async void UploadFile()
        {
            try
            {
                OpenFileDialog o = new OpenFileDialog { Multiselect = true };
                if (o.ShowDialog() != DialogResult.OK) return;

                var config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathDocumenSale").FirstOrDefault();
                if (config == null || string.IsNullOrEmpty(config.KeyValue))
                {
                    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                    return;
                }

                int id = TextUtils.ToInt(grvDocument.GetFocusedRowCellValue(colID));
                //var doc = SQLHelper<DocumentModel>.FindByID(id);
                //if (doc == null) return;
                if (id <= 0) return;

                string documentType = TextUtils.ToString(grvDocument.GetFocusedRowCellValue(colDocumnentTypeName));
                string departmentCode = TextUtils.ToString(grvDocument.GetFocusedRowCellValue(colDepartmentCode));
                if (!string.IsNullOrWhiteSpace(departmentCode)) departmentCode += @"\";


                string pathServer = config.KeyValue.Trim();
                string uploadPath = Path.Combine(pathServer, $@"{departmentCode}{documentType}");

                int totalSuccess = 0;
                foreach (string file in o.FileNames)
                {

                    FileInfo fileInfo = new FileInfo(file);
                    string fileName = fileInfo.Name;


                    //DocumentFileModel fileModel = SQLHelper<DocumentFileModel>.FindByExpression(new Utils.Expression("DocumentID", id).And(new Utils.Expression("FileName", fileName))).FirstOrDefault() ?? new DocumentFileModel();

                    string filePath = Path.Combine(uploadPath, fileInfo.Name);
                    if (File.Exists(filePath))
                    {
                        fileName = $"{Path.GetFileNameWithoutExtension(fileInfo.Name)}_{DateTime.Now.ToString("ddMMyyHHmmss")}{fileInfo.Extension}";
                    }

                    using (var client = new HttpClient())
                    {
                        using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                        {
                            var content = new MultipartFormDataContent
                            {
                                { new StreamContent(fileStream), "file", fileName}
                            };

                            string url = $"http://113.190.234.64:8083/api/Home/uploadfile?path={uploadPath}";
                            var response = await client.PostAsync(url, content);
                            string contentresponse = await response.Content.ReadAsStringAsync();

                            JObject json = JObject.Parse(contentresponse);
                            int status = TextUtils.ToInt(json["status"]);
                            if (status == 1)
                            {
                                DocumentFileModel modelFile = new DocumentFileModel
                                {
                                    FileName = fileName,
                                    FileNameOrigin = fileInfo.Name,
                                    FilePath = fileInfo.FullName,
                                    DocumentID = id
                                };
                                SQLHelper<DocumentFileModel>.Insert(modelFile);
                                totalSuccess++;

                            }
                            else MessageBox.Show($"{TextUtils.ToString(json["message"])}\r\n[{fileName}]", "Thông báo");

                        }
                    }
                }


                if (totalSuccess == o.FileNames.Length)
                {
                    MessageBox.Show($"Upload file thành công!", "Thông báo");
                }
                dataDocumentFile();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            int[] RowIndex = grvDocumentFile.GetSelectedRows();
            if (RowIndex.Count() < 1)
            {
                MessageBox.Show("Vui lòng chọn để xóa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa những file vừa chọn ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {


                for (int i = 0; i < RowIndex.Length; i++)
                {
                    int id = TextUtils.ToInt(grvDocumentFile.GetRowCellValue(RowIndex[i], colIDFileName));
                    DocumentFileBO.Instance.Delete(id);
                }
                dataDocumentFile();
            }
        }

        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string documentType = TextUtils.ToString(grvDocument.GetFocusedRowCellValue(colDocumnentTypeName));
                string departmentCode = TextUtils.ToString(grvDocument.GetFocusedRowCellValue(colDepartmentCode));

                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", $"FORM_BIEU_CHUNG/{departmentCode}/{documentType}");

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }


                //string pathServer = config.KeyValue.Trim();
                //string pathPattern = Path.Combine($"FORM_ADMIN_SALE/{documentType}");
                string pathPattern = Path.Combine($"{departmentCode}/{documentType}");
                int[] selectedRows = grvDocumentFile.GetSelectedRows();
                foreach (int row in selectedRows)
                {
                    string fileName = TextUtils.ToString(grvDocumentFile.GetRowCellValue(row, colFileName));
                    string folderDownload = Path.Combine(pathDownload, fileName);
                    string url = $"http://113.190.234.64:8083/api/formadminsale/{pathPattern}/{fileName}";
                    //url = $"http://localhost:8390/api/formadminsale/{pathPattern}/{fileName}";

                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(url, folderDownload);
                }

                Process.Start(pathDownload);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void grvDocument_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }
    }
}