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
    public partial class frmRegisterIdealSumarize : _Forms
    {
        public frmRegisterIdealSumarize()
        {
            InitializeComponent();
        }

        private void frmRegisterIdealSumarize_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";
            txtTotalPage.Text = "1";
            LoadDepartment();
            LoadRegisterIdeaType();
            LoadData();
        }
        private void LoadDepartment()
        {
            List<DepartmentModel> listDepart = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.DataSource = listDepart;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
        }
        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            LoadAuthor();
        }

        private void LoadAuthor()
        {
            int departMentID = TextUtils.ToInt(cboDepartment.EditValue);
            DataTable listAuthor = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status", "@DepartmentID" }, new object[] { 0, departMentID });
            cboEmployee.Properties.DataSource = listAuthor;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
        }

        private void LoadRegisterIdeaType()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseCatalog", "A", new string[] { "@CatalogType" }, new object[] { 2 });

            cboRegisterIdeaType.Properties.DataSource = dt;
            cboRegisterIdeaType.Properties.DisplayMember = "Name";
            cboRegisterIdeaType.Properties.ValueMember = "ID";
        }

        private void LoadData()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpToDate.Value.Year, dtpToDate.Value.Month, dtpToDate.Value.Day, 23, 59, 59);
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int authorID = TextUtils.ToInt(cboEmployee.EditValue);
            //int employeeID = Global.HeadOfDepartment;// up date
            //if (Global.DepartmentID == 1)
            //{
            //    employeeID = 0;
            //}

            DataTable dt = TextUtils.LoadDataFromSP("spGetRegisterIdea", "A",
                new string[] { "@EmployeeID", "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@FilterText", "@DepartmentID", "@AuthorID", "@RegisterTypeID" },
                new object[] { 0, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtFilterText.Text.Trim(), departmentID, authorID, TextUtils.ToInt(cboRegisterIdeaType.EditValue) });

            grdData.DataSource = dt;
            if (dt.Rows.Count <= 0) return;
            if (string.IsNullOrEmpty(TextUtils.ToString(dt.Rows[0]["TotalPage"])))
            {
                txtTotalPage.Text = "1";
            }
            else txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"TongHopTipTrick_{dtpFromDate.Value.ToString("ddMMyy")}_{dtpToDate.Value.ToString("ddMMyy")}.xlsx");

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

        private void btnOpenFolder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                string nameDepartment = TextUtils.ToString(grvData.GetFocusedRowCellValue(colRegisterTypeDepartmentName));
                string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colRegisterTypeCode));
                RegisterIdeaModel registerIdea = SQLHelper<RegisterIdeaModel>.FindByID(id);
                if (registerIdea == null) return;

                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathPaymentOrder").FirstOrDefault();
                string pathServer = $@"\\192.168.1.190\duan\Tip Trick\{registerIdea.DateRegister.Value.Year}";

                if (Global.DefaultFileName == "defaultonline.ini")
                {
                    pathServer.Replace("\\192.168.1.190", "\\113.190.234.64");
                }

                string pathPattern = $@"P {nameDepartment}\{code}";
                string pathUpload = Path.Combine(pathServer, pathPattern);
                string path = Path.Combine(pathServer, pathPattern);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnViewDetails_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            RegisterIdeaModel selectedIdea = SQLHelper<RegisterIdeaModel>.FindByID(id);
            if (selectedIdea.ID <= 0)
            {
                MessageBox.Show("Không tìm thấy đề tài TipTrick!", "Thông báo");
                return;
            }
            frmRegisterIdeaDetail frm = new frmRegisterIdeaDetail();
            frm.registerIdea = selectedIdea;
            frm.btnSaveCLose.Enabled = frm.btnSaveNew.Enabled = frm.btnUploadFile.Enabled = false;
            frm.grvFile.OptionsBehavior.Editable = false;
            frm.grvFile.OptionsBehavior.ReadOnly = true;
            frm.Show();
        }


        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            btnViewDetails_ItemClick(null, null);
        }

        private void xuấtExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnExportExcel_ItemClick(null, null);
        }

        private void btnTreeFolderContext_Click(object sender, EventArgs e)
        {
            btnOpenFolder_ItemClick(null, null);
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnViewDetails_ItemClick(null, null);
        }

        private void btnViewCourse_Click(object sender, EventArgs e)
        {
            string userName = Global.LoginName;
            string passwordHash = Global.AppPassword;
            int registerIdeaTypeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colRegisterIdeaTypeID));
            string htmlContent = $@"<!DOCTYPE html>
                                    <html lang=""en"">
                                    <head>
                                        <meta charset=""UTF-8"">
                                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                        <title>Form Example</title>
                                    </head>
                                    <body style=""height: 100vh; width: 100%; display: flex; justify-content:  center; align-items: center; margin: 0;padding: 0;"">

                                        <img src=""./loading.gif"" alt="""">
                                       <form action=""http://113.190.234.64:8087/Home/LoginToCourse"" method=""post"" id=""frmSubmitLink"" style=""display: none;"">
                                            <input type=""text"" name=""userName"" value=""{userName}""/>
                                            <input type=""password"" name=""passwordHash"" value=""{passwordHash}"" />
                                            <input type=""number"" name=""registerIdeaTypeID"" value=""{registerIdeaTypeID}""/>
                                            <button type=""submit"">Login</button>
                                        </form>
                                        <script>
                                           document.getElementById(""frmSubmitLink"").submit();
                                        </script>
                                    </body>
                                    </html>";

            string filePath = Path.Combine(Application.StartupPath, "login.html");
            File.WriteAllText(filePath, htmlContent);

            Process.Start(filePath);
        }
    }
}
