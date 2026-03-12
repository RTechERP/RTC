using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
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
using Word = Microsoft.Office.Interop.Word;

namespace BMS
{
    public partial class frmEmployeeContract : _Forms
    {
        public int employeeID;
        public frmEmployeeContract()
        {
            InitializeComponent();
        }

        private void frmEmployeeContract_Load(object sender, EventArgs e)
        {
            loadEmployee();
            loadLoaiHDLD();
            loadData();
        }

        private void loadData()
        {
            grdData.DataSource = TextUtils.LoadDataFromSP("spGetEmployeeContract", "A",
                new string[] { "@EmployeeID", "@LoaiHDLDID", "@FilterText" },
                new object[] { TextUtils.ToInt(cboEmployee.EditValue), TextUtils.ToInt(cboLoaiHDLD.EditValue), txtFilterText.Text.Trim() });
        }
        private void loadEmployee()
        {
            cboEmployee.Properties.DataSource = SQLHelper<EmployeeModel>.SqlToList("SELECT ID,Code,FullName FROM dbo.Employee");
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.EditValue = employeeID;
        }
        private void loadLoaiHDLD()
        {
            cboLoaiHDLD.Properties.DataSource = SQLHelper<EmployeeLoaiHDLDModel>.SqlToList("SELECT ID,Code,Name FROM dbo.EmployeeLoaiHDLD");
            cboLoaiHDLD.Properties.ValueMember = "ID";
            cboLoaiHDLD.Properties.DisplayMember = "Name";
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmEmployeeContractDetail frm = new frmEmployeeContractDetail();
            frm.employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int contractID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            frmEmployeeContractDetail frm = new frmEmployeeContractDetail();
            frm.contractID = contractID;
            frm.employeeID = employeeID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string contractNumber = TextUtils.ToString(grvData.GetFocusedRowCellValue(colContractNumber));
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa hợp đồng [{contractNumber}] này không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                EmployeeContractModel model = (EmployeeContractModel)EmployeeContractBO.Instance.FindByPK(id);
                model.IsDelete = true;
                EmployeeContractBO.Instance.Update(model);
                loadData();

                //Update lại loại hợp đồng hiện tại
                var exp1 = new Expression("EmployeeID", employeeID);
                var exp2 = new Expression("IsDelete", 1, "<>");
                List<EmployeeContractModel> list = SQLHelper<EmployeeContractModel>.FindByExpression(exp1.And(exp2)).OrderByDescending(x => x.CreatedDate).ToList();
                if (list.Count > 0)
                {
                    int currentContract = list.FirstOrDefault() == null ? 0 : list.FirstOrDefault().EmployeeLoaiHDLDID;
                    EmployeeModel employee = SQLHelper<EmployeeModel>.FindByID(employeeID);
                    employee.LoaiHDLDID = currentContract;
                    SQLHelper<EmployeeModel>.Update(employee);
                }
            }
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboLoaiHDLD_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsDelete)))
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;

            }
        }

        private void frmEmployeeContract_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnPrintContract_Click(object sender, EventArgs e)
        {
            int idEmployee = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            if (idEmployee == 0)
            {
                MessageBox.Show("Vui lòng chọn HDLD bạn muốn xuất file Word!", TextUtils.Caption, MessageBoxButtons.OK);
                return;
            }
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeContractWord", "A",
                new string[] { "@ID" },
                new object[] { TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID)) });
            //string contractNumber = TextUtils.ToString(grvData.GetFocusedRowCellValue(colContractNumber));
            string contractNumber = dt.Rows[0]["ContractNumber"].ToString();
            List<(string findText, string replaceText)> findReplacePairs = new List<(string, string)>
            {
                ("#ContractNumber", contractNumber),
                ("#DateContract", dt.Rows[0]["DateContract"].ToString()),
                ("#FullName", dt.Rows[0]["FullName"].ToString()),
                ("#DateOfBirth", dt.Rows[0]["BirthOfDate"].ToString()),
                ("#CCCD_CMND", dt.Rows[0]["SoCMTND"].ToString()),
                ("#IssuedBy", dt.Rows[0]["NoiCap"].ToString()),
                ("#Address", dt.Rows[0]["DcThuongTru"].ToString()),
                ("#PhoneNumber", dt.Rows[0]["SDTCaNhan"].ToString()),
                ("#Sex", dt.Rows[0]["Sex"].ToString()),
                ("#Nationality", dt.Rows[0]["QuocTich"].ToString()),
                ("#DateRange", dt.Rows[0]["NgayCap"].ToString()),
                ("#ContractType", dt.Rows[0]["LoaiHDLD"].ToString()),
                ("#ContractDuration", dt.Rows[0]["ContractDuration"].ToString()),
                ("#Position", dt.Rows[0]["Position"].ToString()),
                ("#Department", dt.Rows[0]["DepartmentName"].ToString()),
                ("#Salary ", dt.Rows[0]["MucDongBHXHHienTai"].ToString()),
                ("#NotificationDate", dt.Rows[0]["NotificationDate"].ToString()),
                //Thông tin công ty
                //("#CompanyNameHeader", dt.Rows[0]["CompanyNameHeader"].ToString()),
                ("#CompanyNameHeader", dt.Rows[0]["CompanyName"].ToString().Replace("\n","\r\n").ToUpper()),
                ("#COMPANYCODE", dt.Rows[0]["COMPANYCODE"].ToString()),
                ("#CompanyName", dt.Rows[0]["CompanyName"].ToString()),
                ("#UPPERCOMPANYNAME", dt.Rows[0]["CompanyName"].ToString().ToUpper()),
                ("#TaxCodeCom", dt.Rows[0]["TaxCodeCom"].ToString()),
                ("#AddCom", dt.Rows[0]["AddressCom"].ToString()),
                ("#PhoneCom", dt.Rows[0]["PhoneNumberCom"].ToString()),
                ("#DirectorCom", dt.Rows[0]["DirectorCom"].ToString()),
                ("#PosCom", dt.Rows[0]["PositionCom"].ToString()),

            };
            string fileName = contractNumber;
            fileName = fileName.Replace("/", "");
            int idHDLD = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeLoaiHDLDID));
            if (idHDLD == 0)
            {
                MessageBox.Show("Chưa có HDLD!", TextUtils.Caption, MessageBoxButtons.OK);
                return;
            }

            string fileOldName = "(Mau)_HDLD.doc";
            if (idHDLD == 1) fileOldName = "(Mau)_HDTV_Company.doc";
            if (idHDLD == 4) fileName += "_12T";


            OpenAndEditWordDocument(Path.Combine(Application.StartupPath, fileOldName), findReplacePairs, fileName);

            //if (idHDLD == 1) // HĐTV
            //{
            //    ////OpenAndEditWordDocument(@"D:\RTC\Code\Old\RTCApp03052024\RTC\TaiLieu\(Mau)_HDTV.doc", findReplacePairs, fileName);
            //    //OpenAndEditWordDocument(Path.Combine(Application.StartupPath, "(Mau)_HDTV_Company.doc"), findReplacePairs, fileName);

            //}
            //else 
            //if (idHDLD == 4)// HĐLĐ 12T
            //{
            //    //OpenAndEditWordDocument(Path.Combine(Application.StartupPath, "(Mau)_HDLD.doc"), findReplacePairs, fileName + "_12T");

            //    fileName += "_12T";
            //}
            //else//Không xác định thời hạn
            //{
            //    //OpenAndEditWordDocument(Path.Combine(Application.StartupPath, "(Mau)_HDLD.doc"), findReplacePairs, fileName);
            //}



        }

        private void OpenAndEditWordDocument(string filePath, List<(string findText, string replaceText)> findReplacePairs, string fileName)
        {
            Word.Application wordApp = new Word.Application();
            Word.Document doc = null;
            string savedFilePath = "";

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Word Documents|*.docx";
                //saveFileDialog.Title = "Chọn file lưu HDLD";
                saveFileDialog.FileName = fileName;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {

                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo hợp đồng..."))
                    {
                        wordApp.Visible = false;
                        doc = wordApp.Documents.Open(filePath);

                        foreach (var pair in findReplacePairs)
                        {
                            //if (pair.findText == "#CompanyNameHeader")
                            //{
                            //    string textForWord = pair.replaceText.Replace("\n", "\r\n"); // xuống đoạn
                            //    TextUtils.FindReplaceAnywhere(wordApp, pair.findText, textForWord.ToUpper());
                            //}
                            TextUtils.FindReplaceAnywhere(wordApp, pair.findText, pair.replaceText);
                        }


                        savedFilePath = saveFileDialog.FileName;

                        if (File.Exists(savedFilePath))
                        {
                            bool isFileOpen = false;
                            try
                            {
                                using (FileStream fs = new FileStream(savedFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                                {
                                }
                            }
                            catch (IOException)
                            {
                                isFileOpen = true;
                            }

                            if (isFileOpen)
                            {
                                MessageBox.Show($"File [{fileName}.docx] đang mở. Vui lòng đóng file trước khi lưu.", TextUtils.Caption, MessageBoxButtons.OK);
                                return;
                            }
                        }
                        doc.SaveAs2(savedFilePath, Word.WdSaveFormat.wdFormatXMLDocument);
                        Process.Start(savedFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close(false);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                }

                if (wordApp != null)
                {
                    wordApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                }
            }
        }

        private void btnPrintContractContext_Click(object sender, EventArgs e)
        {
            btnPrintContract_Click(null, null);
        }
    }
}