using BMS.Model;
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
    public partial class frmAccountingContractFile : _Forms
    {
        public frmAccountingContractFile()
        {
            InitializeComponent();
        }

        private void frmAccountingContractFile_Load(object sender, EventArgs e)
        {
            LoadContract();
            LoadData();
        }

        void LoadContract()
        {
            List<AccountingContractModel> list = SQLHelper<AccountingContractModel>.FindAll().Where(x => x.IsDelete == false).OrderByDescending(x => x.DateInput).ToList();
            list.Insert(0, new AccountingContractModel() { ID = -1, ContractNumber = "--Tất cả--" });
            cboContract.Properties.ValueMember = "ID";
            cboContract.Properties.DisplayMember = "ContractNumber";
            cboContract.Properties.DataSource = list;
        }

        void LoadData()
        {
            int accountingContractID = TextUtils.ToInt(cboContract.EditValue) <= 0 ? 0 : TextUtils.ToInt(cboContract.EditValue);
            List<AccountingContractFileDTO> list = SQLHelper<AccountingContractFileDTO>.ProcedureToList("spGetAccountingContractFile"
                                                                                        , new string[] { "@AccountingContractID" }
                                                                                        , new object[] { accountingContractID });
            grdData.DataSource = list;
        }

        private void cboContract_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"DanhSachFileHopDong_{DateTime.Now.ToString("ddMMyy")}.xlsx");

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

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

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                int id  = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colContractID));
                if (id <= 0)
                {
                    return;
                }
                string path = TextUtils.ToString(grvData.GetFocusedRowCellValue(colServerPath));
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                
                int contractID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colContractID));
                if (contractID <= 0)
                {
                    return;
                }

                bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
                string numberContract = TextUtils.ToString(grvData.GetFocusedRowCellValue(colContractNumber));
                if (isApproved)
                {
                    MessageBox.Show($"Hợp đồng [{numberContract}] đã được duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo");
                    return;
                }
                //AccountingContractModel contract = SQLHelper<AccountingContractModel>.FindByID(contractID);

                //string pathUpload = @"\\192.168.1.190\Common\08. SOFTWARES\LeTheAnh\DemoContractAccounting";
                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathAccounting").FirstOrDefault();
                string pathUpload = config != null ? $@"{config.KeyValue}" : "";

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var listSelected = dialog.FileNames;

                    AccountingContractModel contractModel = SQLHelper<AccountingContractModel>.ProcedureToList("spGetAccountingContractParent", new string[] { "@ID" }, new object[] { contractID }).FirstOrDefault();

                    string company = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCompanyName)).ToUpper();
                    string group = TextUtils.ToString(grvData.GetFocusedRowCellValue(colContractGroupText)).ToUpper();
                    string custormer = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCustomerOrSupplier));
                    string contractNumber = contractModel != null ? $@"\{contractModel.ContractNumber}" : "";
                    string destFilename = $@"{company}\{group}\{custormer}{contractNumber}";

                    destFilename = Path.Combine(pathUpload, destFilename);
                    if (!Directory.Exists(destFilename))
                    {
                        Directory.CreateDirectory(destFilename);
                    }

                    foreach (var file in listSelected)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        File.Copy(fileInfo.FullName, Path.Combine(destFilename, fileInfo.Name), true);

                        AccountingContractFileModel contractFile = new AccountingContractFileModel();
                        contractFile.AccountingContractID = contractID;
                        contractFile.FileName = fileInfo.Name;
                        contractFile.OriginPath = fileInfo.DirectoryName;
                        contractFile.ServerPath = destFilename;
                        SQLHelper<AccountingContractFileModel>.Insert(contractFile);

                        //string folderUpload = project.CreatedDate.Value.Year + "/" + project.ProjectCode;
                        //string folder = projectFile.FileTypeFolder == 1 ? $@"{folderUpload}/Video" : $@"{folderUpload}/Image";
                        //projectFile.PathServer = "ftp://192.168.1.2:22/" + folder;

                        //var fileExist = listFile.Where(x => x.FileName == fileInfo.Name).ToList();

                        //var row = dt.Select($"FileName = '{fileInfo.Name}'");
                        //if (row.Length > 0)
                        //{
                        //    listFileExist.Add(projectFile);
                        //}
                        //else
                        //{
                        //    listFileInsert.Insert(0, projectFile);
                        //}

                    }
                    LoadData();
                    //saveData(listFileInsert, listFileExist);
                    //loadData();
                    //MessageBox.Show("Upload thành công!", "Thông báo");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {

        }

        private void btnViewFile_Click(object sender, EventArgs e)
        {
            string fileName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFileName));
            string filePath = TextUtils.ToString(grvData.GetFocusedRowCellValue(colServerPath));
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }
            Process.Start(Path.Combine(filePath, fileName));
        }
    }
}
