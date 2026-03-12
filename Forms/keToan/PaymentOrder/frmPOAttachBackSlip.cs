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
    public partial class frmPOAttachBackSlip : _Forms
    {
        public int PaymentOrderID { get; set; } = 0;
        List<FileInfo> listFileUpload = new List<FileInfo>();
        List<PaymentOrderFileBankSlipModel> listFiles = new List<PaymentOrderFileBankSlipModel>();
        List<PaymentOrderFileBankSlipModel> listFileDelete = new List<PaymentOrderFileBankSlipModel>();

        public frmPOAttachBackSlip()
        {
            InitializeComponent();
        }

        private void frmPOAttachBackSlip_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            listFiles = SQLHelper<PaymentOrderFileBankSlipModel>.FindByAttribute("PaymentOrderID", PaymentOrderID);
            LoadFile(listFiles);
        }
        private void btnCloseSave_Click(object sender, EventArgs e)
        {
            UploadFile();
            RemoveFile();
            this.DialogResult = DialogResult.OK;
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    PaymentOrderFileBankSlipModel fileRequest = new PaymentOrderFileBankSlipModel()
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

        void LoadFile(List<PaymentOrderFileBankSlipModel> listFiles)
        {
            grdFileData.DataSource = listFiles;
            grvFileData.RefreshData();
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
                    SQLHelper<PaymentOrderFileBankSlipModel>.Delete(item);
                }
            }
        }
        public async void UploadFile()
        {
            try
            {
                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathPaymentOrder").FirstOrDefault();
                if (config == null || string.IsNullOrEmpty(config.KeyValue))
                {
                    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                    return;
                }

                PaymentOrderModel order = SQLHelper<PaymentOrderModel>.FindByID(PaymentOrderID);
                if (order == null || order.ID <= 0) return;
                //if (order.EmployeeID != Global.EmployeeID && !Global.IsAdmin) return;

                string pathServer = config.KeyValue.Trim();
                string pathPattern = $@"NĂM {order.DateOrder.Value.Year}\ĐỀ NGHỊ THANH TOÁN\THÁNG {order.DateOrder.Value.ToString("MM.yyyy")}\{order.DateOrder.Value.ToString("dd.MM.yyyy")}\{order.Code}";
                string pathUpload = Path.Combine(pathServer, pathPattern);
                //pathUpload = "\\\\192.168.1.190\\Common\\08. SOFTWARES\\LeTheAnh\\DemoUploadFile";

                var client = new HttpClient();

                List<PaymentOrderFileBankSlipModel> listFiles = new List<PaymentOrderFileBankSlipModel>();
                foreach (var file in listFileUpload)
                {
                    PaymentOrderFileBankSlipModel fileOrder = new PaymentOrderFileBankSlipModel();
                    fileOrder.PaymentOrderID = order.ID;
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
                        SQLHelper<PaymentOrderFileBankSlipModel>.Insert(fileOrder);
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnDeleteFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int id = TextUtils.ToInt(grvFileData.GetFocusedRowCellValue("ID"));
            string fileName = TextUtils.ToString(grvFileData.GetFocusedRowCellValue("FileName"));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file đính kèm [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvFileData.DeleteSelectedRows();
                if (id <= 0) return;
                PaymentOrderFileBankSlipModel file = SQLHelper<PaymentOrderFileBankSlipModel>.FindByID(id);
                listFileDelete.Add(file);
            }
        }
    }
}