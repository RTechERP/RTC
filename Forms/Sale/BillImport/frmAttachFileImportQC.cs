using BMS.Business;
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
    public partial class frmAttachFileImportQC : _Forms
    {
        public DataRow FocusedRow { get; set; }
        public List<FileInfo> ListFileUpload = new List<FileInfo>();
        public List<BillImportQCDetailFilesModel> ListFiles = new List<BillImportQCDetailFilesModel>();
        public List<BillImportQCDetailFilesModel> ListFileViews = new List<BillImportQCDetailFilesModel>();
        public List<BillImportQCDetailFilesModel> ListFileDelete = new List<BillImportQCDetailFilesModel>();
        int _fileType = 0; // 1: Pur checksheet, 2: Tech report
        public frmAttachFileImportQC()
        {
            InitializeComponent();
        }
        public frmAttachFileImportQC(int fileType)
        {
            InitializeComponent();
            _fileType = fileType;
        }

        private void frmAttachFileImportQC_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnCloseSave_Click(object sender, EventArgs e)
        {
            //UploadFile();
            //RemoveFile();
            this.DialogResult = DialogResult.OK;
        }
        void loadData()
        {
            if (FocusedRow == null) return;
            //int billImportQCDetailID = TextUtils.ToInt(FocusedRow["ID"]);
            //if (billImportQCDetailID > 0)
            //{
            //    ListFiles = SQLHelper<BillImportQCDetailFilesModel>.FindByAttribute("BillImportQCDetailID", billImportQCDetailID);
            //}

            
            LoadFile(ListFileViews);
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
                    BillImportQCDetailFilesModel fileRequest = new BillImportQCDetailFilesModel()
                    {
                        FileName = fileInfo.Name,
                        OriginPath = fileInfo.DirectoryName,
                        FileType = _fileType
                    };

                    ListFiles.Insert(0, fileRequest);
                    ListFileViews.Insert(0, fileRequest);
                    ListFileUpload.Add(fileInfo);
                }
                LoadFile(ListFileViews);
            }
        }
        void LoadFile(List<BillImportQCDetailFilesModel> listFiles)
        {
            grdFileData.DataSource = null;
            var files = listFiles.Where(x => x.FileType == _fileType).ToList();

            grdFileData.DataSource = files;
            grvFileData.RefreshData();
        }


     
        private void btnDeleteFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int id = TextUtils.ToInt(grvFileData.GetFocusedRowCellValue("ID"));
            string fileName = TextUtils.ToString(grvFileData.GetFocusedRowCellValue("FileName"));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file đính kèm [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvFileData.DeleteSelectedRows();
                //if (id <= 0) return;
                BillImportQCDetailFilesModel file = SQLHelper<BillImportQCDetailFilesModel>.FindByID(id);
                ListFileDelete.Add(file);

                var fileRemove = ListFileViews.FirstOrDefault(x => x.ID == file.ID);
                if (fileRemove != null) ListFileViews.Remove(fileRemove);
            }
        }
    }
}