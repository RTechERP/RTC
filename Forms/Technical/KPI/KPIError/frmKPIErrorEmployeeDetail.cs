using BMS.Business;
using BMS.Model;
using BMS.Utils;
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
    public partial class frmKPIErrorEmployeeDetail : _Forms
    {
        public KPIErrorEmployeeModel model = new KPIErrorEmployeeModel();

        List<KPIErrorEmployeeFileModel> listFiles = new List<KPIErrorEmployeeFileModel>();
        List<KPIErrorEmployeeFileModel> listFileDelete = new List<KPIErrorEmployeeFileModel>();
        List<FileInfo> listFileUpload = new List<FileInfo>();

        string pathServer = @"\\192.168.1.190\Technical\ANH LOI 5S- ADMIN KT";
        public int departmentID = 0;
        public frmKPIErrorEmployeeDetail()
        {
            InitializeComponent();
        }

        private void frmKPIErrorEmployeeDetail_Load(object sender, EventArgs e)
        {
            LoadKPIError();
            LoadEmployee();
            LoadData();
        }
        void LoadKPIError()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetKPIError", "A", new string[] { "@DepartmentID" }, new object[] { departmentID});
            cboKPIError.Properties.DisplayMember = "Code";
            cboKPIError.Properties.ValueMember = "ID";
            cboKPIError.Properties.DataSource = dt;
        }
        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }
        void LoadData()
        {
            cboKPIError.EditValue = model.KPIErrorID;
            cboEmployee.EditValue = model.EmployeeID;
            txtErrorNumber.Value = model.ErrorNumber == 0 ? 1 : model.ErrorNumber;
            dtpErrorDate.Value = model.ErrorDate == null ? DateTime.Now : (DateTime)model.ErrorDate;
            txtNote.Text = model.Note == null ? model.Note : model.Note;

            if (model.TotalMoney != 0) //LinhTN update 09/08/2024
            {
                txtMonney.EditValue = model.TotalMoney;
            }


            if (model.ID > 0)
            {
                //Load ds file đính kèm
                listFiles = SQLHelper<KPIErrorEmployeeFileModel>.FindByAttribute("KPIErrorEmployeeID", model.ID);
                grdData.DataSource = listFiles;
            }
            else
            {
                grdData.DataSource = null;
            }
        }

        string codeError = "L2";
        private void cboKPIError_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            DataRowView dataRow = (DataRowView)lookUpEdit.GetSelectedDataRow();
            if (dataRow != null)
            {
                string code = TextUtils.ToString(dataRow.Row.Field<string>("Code"));
                txtMonney.ReadOnly = code.Trim().ToLower() != codeError.Trim().ToLower();

                txtQuantity.Text = TextUtils.ToString(dataRow.Row.Field<int>("Quantity"));
                txtMonney.Text = TextUtils.ToString(dataRow.Row.Field<decimal>("Monney"));
                txtErrorName.Text = dataRow.Row.Field<string>("Name");
                txtErrorContent.Text = dataRow.Row.Field<string>("Content");
            }
            else
            {
                txtQuantity.Text = "";
                txtMonney.Text = "";
                txtErrorName.Text = "";
                txtErrorContent.Text = "";
            }

            
        }
        bool CheckValidate()
        {
            if (TextUtils.ToInt(cboKPIError.EditValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn Mã lỗi vi phạm!", "Thông báo");
                return false;
            }
            if (TextUtils.ToInt(cboEmployee.EditValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn Nhân viên vi phạm!", "Thông báo");
                return false;
            }

            //Check trùng - start
            //var startDate = dtpErrorDate.Value.ToString("yyyy/MM/dd 00:00");
            //var endDate = dtpErrorDate.Value.ToString("yyyy/MM/dd 23:59");

            //var ex1 = new Expression("ID", model.ID, "<>");
            //var ex2 = new Expression("EmployeeID", TextUtils.ToInt(cboEmployee.EditValue));
            //var ex3 = new Expression("KPIErrorID", TextUtils.ToInt(cboKPIError.EditValue));
            //var ex4 = new Expression("ErrorDate", startDate, ">=");
            //var ex5 = new Expression("ErrorDate", endDate, "<=");
            ////var exist = SQLHelper<KPIErrorEmployeeModel>.FindByExpression(ex1.And(ex2).And(ex3).And(ex4).And(ex5));
            //var ex6 = new Expression("IsDelete", 0); //LinhTN update 09/08/2024
            //var exist = SQLHelper<KPIErrorEmployeeModel>.FindByExpression(ex1.And(ex2).And(ex3).And(ex4).And(ex5).And(ex6));

            //if (exist.Count > 0)
            //{
            //    MessageBox.Show($"Đã tồn tại nhân viên [{cboEmployee.Text}] vi phạm mã lỗi [{cboKPIError.Text}] vào ngày [{dtpErrorDate.Value:dd/MM/yyyy}]!", "Thông báo");
            //    return false;
            //}
            //Check trùng - end
            return true;
        }

        bool Save()
        {
            try
            {
                if (!CheckValidate()) return false;
                model.KPIErrorID = TextUtils.ToInt(cboKPIError.EditValue);
                model.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
                model.ErrorNumber = TextUtils.ToInt(txtErrorNumber.Value);
                model.ErrorDate = dtpErrorDate.Value;
                model.Note = txtNote.Text.Trim();
                model.TotalMoney = TextUtils.ToDecimal(txtMonney.EditValue);

                if (model.ID > 0)
                {
                    SQLHelper<KPIErrorEmployeeModel>.Update(model);
                }
                else
                {
                    model.ID = SQLHelper<KPIErrorEmployeeModel>.Insert(model).ID;
                }
                UploadFile(model.ID);
                RemoveFile();


                listFileUpload.Clear();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveAndReset_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                model = new KPIErrorEmployeeModel();
                LoadData();
            }
        }
        private void frmKPIErrorEmployeeDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        void LoadFile(List<KPIErrorEmployeeFileModel> listFiles)
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
                    KPIErrorEmployeeFileModel fileRequest = new KPIErrorEmployeeFileModel()
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
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            string fileName = TextUtils.ToString(grvData.GetFocusedRowCellValue("FileName"));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file đính kèm [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                if (id <= 0) return;
                KPIErrorEmployeeFileModel file = SQLHelper<KPIErrorEmployeeFileModel>.FindByID(id);
                listFileDelete.Add(file);
            }
        }
        public async void UploadFile(int ID)
        {
            try
            {
                if (listFileUpload.Count <= 0) return;

                KPIErrorEmployeeModel model = SQLHelper<KPIErrorEmployeeModel>.FindByID(ID);
                if (model == null) return;
                //ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathKPIErrorEmployeeFile").FirstOrDefault();
                //config = config ?? new ConfigSystemModel();

                //string pathServer = @"\\192.168.1.190\Common\08. SOFTWARES\LeTheAnh\DemoUploadFile";
                //string pathServer = TextUtils.ToString(config.KeyValue).Trim();
                //string pathServer = "\\192.168.1.190\\Technical\\ANH LOI 5S- ADMIN KT";

                if (string.IsNullOrWhiteSpace(pathServer))
                {
                    MessageBox.Show("Vui lòng chọn đường dẫn trên server!", "Thông báo");
                    return;
                }

                //string dateFolder = !model.ErrorDate.HasValue ? "" : $"{model.ErrorDate:dd.MM.yyyy}";
                //dateFolder = "";
                //string pathPattern = $@"AnhViPham\{dateFolder}";

                string pathPattern = $@"{model.ErrorDate.Value.Year}\T{model.ErrorDate.Value.Month}\N{model.ErrorDate.Value.ToString("dd.MM.yyyy")}"; //LinhTN update 04/11/2024
                string pathUpload = Path.Combine(pathServer, pathPattern);

                var client = new HttpClient();

                List<KPIErrorEmployeeFileModel> listFiles = new List<KPIErrorEmployeeFileModel>();
                foreach (var file in listFileUpload.ToList())
                {
                    if (file.Length < 0) continue;
                    KPIErrorEmployeeFileModel modelFile = new KPIErrorEmployeeFileModel();
                    modelFile.KPIErrorEmployeeID = ID;
                    modelFile.FileName = file.Name;
                    modelFile.OriginPath = file.DirectoryName;
                    modelFile.ServerPath = pathUpload;


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
                        SQLHelper<KPIErrorEmployeeFileModel>.Insert(modelFile);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        public async void RemoveFile()
        {
            if (listFileDelete.Count <= 0) return;
            if (!model.ErrorDate.HasValue) return;

            var client = new HttpClient();
            foreach (var item in listFileDelete)
            {
                string pathPattern = $@"{model.ErrorDate.Value.Year}\T{model.ErrorDate.Value.Month}\N{model.ErrorDate.Value.ToString("dd.MM.yyyy")}";
                string pathUpload = Path.Combine(pathServer, pathPattern, item.FileName);

                if (File.Exists(pathUpload))
                {
                    string url = $@"http://113.190.234.64:8083/api/Home/removefile?path={pathUpload}";
                    var result = await client.GetAsync(url);

                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                    }
                }
                
                SQLHelper<KPIErrorEmployeeFileModel>.Delete(item);
            }
        }
    }
}