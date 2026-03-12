using BMS.Model;
using DevExpress.XtraEditors;
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
    public partial class frmUploadFileImages : _Forms
    {
        List<FileInfo> listFileUpload = new List<FileInfo>();
        List<VehicleBookingFileModel> listFiles = new List<VehicleBookingFileModel>();

        List<VehicleBookingFileModel> lstFileDelete = new List<VehicleBookingFileModel>();
        public int stt { get; set; }
        public frmUploadFileImages()
        {
            InitializeComponent();
        }

        private void frmUploadFileImages_Load(object sender, EventArgs e)
        {
            //Load ds file ảnh đính kèm
            listFiles = SQLHelper<VehicleBookingFileModel>.FindByAttribute("STT", stt);
            grdData.DataSource = listFiles;
        }
        void LoadFile(List<VehicleBookingFileModel> listFiles)
        {
            grdData.DataSource = listFiles;
            grvData.RefreshData();
        }
        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Chọn file ảnh";
            dialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    VehicleBookingFileModel fileRequest = new VehicleBookingFileModel()
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
        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            if (grvData.RowCount > 0)
            {
                var focusedRowHandle = grvData.FocusedRowHandle;
                grvData.DeleteSelectedRows();
                grvData.FocusedRowHandle = focusedRowHandle;

                lstFileDelete.Add(SQLHelper<VehicleBookingFileModel>.FindByID(TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"))));
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathVehicleBooking").FirstOrDefault();
                //if (config == null || string.IsNullOrEmpty(config.KeyValue))
                //{
                //    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                //    return;
                //}               
                //string pathServer = config.KeyValue.Trim();
                //string pathServer = @"\\192.168.1.190\Common\08. SOFTWARES\LeTheAnh\Image";
                string pathServer = @"\\192.168.1.190\Common\11. HCNS\DatXe";
                string pathPattern = $@"DANGKYDATXENGAY{DateTime.Now.ToString("dd.MM.yyyy")}";
                string pathUpload = Path.Combine(pathServer, pathPattern);

                var client = new HttpClient();

                List<VehicleBookingFileModel> listFiles = new List<VehicleBookingFileModel>();
                foreach (var file in listFileUpload)
                {
                    VehicleBookingFileModel fileVehicleBooking = new VehicleBookingFileModel();
                    fileVehicleBooking.FileName = file.Name;
                    fileVehicleBooking.OriginPath = file.DirectoryName;
                    fileVehicleBooking.ServerPath = pathUpload;
                    fileVehicleBooking.STT = stt;

                    if (file.Length < 0) continue;

                    //using var fileStream = file.OpenReadStream();
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
                        SQLHelper<VehicleBookingFileModel>.Insert(fileVehicleBooking);
                    }
                }
                if(lstFileDelete.Count > 0)
                {
                    SQLHelper<VehicleBookingFileModel>.DeleteListModel(lstFileDelete);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}