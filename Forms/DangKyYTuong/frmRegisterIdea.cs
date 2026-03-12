using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmRegisterIdea : _Forms
    {


        public frmRegisterIdea()
        {
            InitializeComponent();
        }
        private void frmRegisterIdea_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";
            txtTotalPage.Text = "1";
            LoadDepartment();
            LoadAuthor();
            LoadData();
            LoadFile();
            LoadIdeaDetail();
            LoadRegisterIdeaType();
            if (Global.DepartmentID == 1)
            {
                cboDepartment.Enabled = true; 
                cboEmployee.Enabled = true;
            }
            else if (Global.HeadOfDepartment == Global.EmployeeID)
            {
                cboEmployee.Enabled = true;
            }
        }

        private void LoadFile()
        {
            int registerIdeaID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            List<RegisterIdeaFileModel> listFile = SQLHelper<RegisterIdeaFileModel>.FindByAttribute("RegisterIdeaID", registerIdeaID);
            grdFile.DataSource = listFile;

        }

        private void LoadRegisterIdeaType()
        {
            //DataTable dt = TextUtils.LoadDataFromSP("spGetRegisterIdeaType", "A", new string[] { }, new object[] { });


            //var exp1 = new Expression(RegisterIdeaTypeModel_Enum.IsDeleted.ToString(), 0);
            //var list = SQLHelper<RegisterIdeaTypeModel>.FindByAttribute(RegisterIdeaTypeModel_Enum.IsDeleted.ToString(), 0).OrderBy(x=>x.STT).ToList();

            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseCatalog", "A", new string[] { "@CatalogType" }, new object[] { 2 });

            cboRegisterIdeaType.Properties.DataSource = dt;
            cboRegisterIdeaType.Properties.DisplayMember = "Name";
            cboRegisterIdeaType.Properties.ValueMember = "ID";
        }

        private void LoadIdeaDetail()
        {
            int registerIdeaID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            List<RegisterIdeaDetailModel> listIdeaDetail = SQLHelper<RegisterIdeaDetailModel>.FindByAttribute("RegisterIdeaID", registerIdeaID);
            grdDetail.DataSource = listIdeaDetail;
        }

        private void LoadAuthor()
        {
            //List<EmployeeModel> listAuthor = SQLHelper<EmployeeModel>.FindAll();

            DataTable listAuthor = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.DataSource = listAuthor;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.EditValue = Global.EmployeeID;
        }

        private void LoadDepartment()
        {
            List<DepartmentModel> listDepart = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.DataSource = listDepart;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.EditValue = Global.DepartmentID;
        }

        private void LoadData()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int authorID = TextUtils.ToInt(cboEmployee.EditValue);
            int employeeID = Global.HeadOfDepartment;// up date
            if (Global.DepartmentID == 1)
            {
                employeeID = 0;
            }

            DataTable dt = TextUtils.LoadDataFromSP("spGetRegisterIdea", "A", 
                new string[] { "@EmployeeID", "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@FilterText", "@DepartmentID", "@AuthorID","@RegisterTypeID" },
                new object[] { employeeID, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtFilterText.Text, departmentID, authorID , TextUtils.ToInt(cboRegisterIdeaType.EditValue)});

            grdData.DataSource = dt;
            if (dt.Rows.Count <= 0) return;
            if (string.IsNullOrEmpty(TextUtils.ToString(dt.Rows[0]["TotalPage"])))
            {
                txtTotalPage.Text = "1";
            }
            else txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
            
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadData();

        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadData();

        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadData();


        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            LoadData();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmRegisterIdeaDetail frm = new frmRegisterIdeaDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            RegisterIdeaModel selectedIdea = SQLHelper<RegisterIdeaModel>.FindByID(id);
            frmRegisterIdeaDetail frm = new frmRegisterIdeaDetail();
            frm.registerIdea = selectedIdea;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var focusedRowHandle = grvData.FocusedRowHandle;
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                string employeeName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));
                DateTime dateRegister = TextUtils.ToDate(grvData.GetFocusedRowCellValue(colDateRegister).ToString());
                if (MessageBox.Show($"Bạn có muốn xóa ý tưởng do {employeeName} đăng kí ngày {dateRegister} hay không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
                    bool isApprovedTBP = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTBP));

                    if (isApproved || isApprovedTBP)
                    {
                        MessageBox.Show($"Ý tưởng đã được duyệt. Không thể xóa!", "Thông báo");
                        return;
                    }
                    RegisterIdeaModel deleteIdea = SQLHelper<RegisterIdeaModel>.FindByID(ID);
                    deleteIdea.IsDeleted = true;
                    SQLHelper<RegisterIdeaModel>.Update(deleteIdea);
                    //RegisterIdeaBO.Instance.Delete(ID);
                    grvData.DeleteSelectedRows();
                    //grvData.FocusedRowHandle = focusedRowHandle;

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}");
            }
        }

        private void btnScore_Click(object sender, EventArgs e)
        {
            int registerIdeaID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            RegisterIdeaModel registerIdea = SQLHelper<RegisterIdeaModel>.FindByID(registerIdeaID);
            EmployeeModel author = SQLHelper<EmployeeModel>.FindByID(TextUtils.ToInt(registerIdea.EmployeeID));
            if (author.ID <= 0) return;
            DepartmentModel ideaDepartment = SQLHelper<DepartmentModel>.FindByID(TextUtils.ToInt(author.DepartmentID));

            if (!Global.IsAdmin)
            {
                if (registerIdea.EmployeeID == Global.EmployeeID && ideaDepartment.HeadofDepartment != Global.HeadOfDepartment)
                {
                    MessageBox.Show($"Bạn không thể tự chấm điểm cho ý tưởng của mình!", "Thông báo");
                    return;
                }
            }
            frmRegisterIdeaScore frm = new frmRegisterIdeaScore();
            frm.registerIdea = registerIdea;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            EmployeeModel em = SQLHelper<EmployeeModel>.FindByID(TextUtils.ToInt(cboEmployee.EditValue));
            cboDepartment.EditValue = em.DepartmentID;
            LoadData();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadFile();
            LoadIdeaDetail();
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            RegisterIdeaModel selectedIdea = SQLHelper<RegisterIdeaModel>.FindByID(id);
            frmRegisterIdeaDetail frm = new frmRegisterIdeaDetail();
            frm.registerIdea = selectedIdea;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                LoadIdeaDetail();
                LoadFile();
            }
        }

        private void grdFile_Click(object sender, EventArgs e)
        {

        }

        private void btnDownloadFile_Click(object sender, EventArgs e)
        {
            int focusedIdeaID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (focusedIdeaID <= 0) return;
            RegisterIdeaModel focusedIdea = SQLHelper<RegisterIdeaModel>.FindByID(focusedIdeaID);
            string pathPattern = $@"DemoUploadFile/NĂM {focusedIdea.DateRegister.Value.Year}/THÁNG {focusedIdea.DateRegister.Value.ToString("MM.yyyy")}";
            //      string pathPattern = $@"{project.CreatedDate.Value.Year}/{project.ProjectCode.Trim()}/THIETKE.Co/{solution.CodeSolution.Trim()}/2D/GC/DH";


            int[] rowSelecteds = grvFile.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn file muốn tải!", "Thông báo");
                return;
            }
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string fileName = "";
                try
                {
                    foreach (int row in rowSelecteds)
                    {
                        // productCode = TextUtils.ToString(grvData.GetRowCellValue(row, colProductCode));
                        fileName = TextUtils.ToString(grvFile.GetRowCellValue(row, colFileName));
                        if (string.IsNullOrEmpty(fileName)) continue;
                        string pathDowload = Path.Combine(fbd.SelectedPath, $"{fileName}");
                        string url = $"http://113.190.234.64:8083/api/demo/{pathPattern}/{fileName}";

                        WebClient webClient = new WebClient();
                        webClient.DownloadFile(url, pathDowload);
                        Process.Start(pathDowload);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"File [{fileName}] không tồn tại!\n{ex.Message}", "Thông báo");
                }
            }
            //private void btnDownloadFile_Click(object sender, EventArgs e)
            //{
            //    int projectId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));

            //    ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectId);
            //    if (project == null) return;
            //    if (!project.CreatedDate.HasValue) return;

            //    int projectPartlistId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectPartListID));

            //    ProjectSolutionModel solution = SQLHelper<ProjectSolutionModel>.ProcedureToList("spGetProjectSolutionByProjectPartListID",
            //                                        new string[] { "@ProjectPartListID" }, new object[] { projectPartlistId }).FirstOrDefault();
            //    if (solution == null) return;
            //    if (string.IsNullOrEmpty(solution.CodeSolution)) return;
            //    string pathPattern = $@"{project.CreatedDate.Value.Year}/{project.ProjectCode.Trim()}/THIETKE.Co/{solution.CodeSolution.Trim()}/2D/GC/DH";

            //    int[] rowSelecteds = grvData.GetSelectedRows();
            //    if (rowSelecteds.Length <= 0)
            //    {
            //        MessageBox.Show("Vui lòng chọn sản phẩm muốn tải file!", "Thông báo");
            //        return;
            //    }
            //    FolderBrowserDialog fbd = new FolderBrowserDialog();
            //    if (fbd.ShowDialog() == DialogResult.OK)
            //    {
            //        string productCode = "";
            //        try
            //        {
            //            foreach (int row in rowSelecteds)
            //            {
            //                productCode = TextUtils.ToString(grvData.GetRowCellValue(row, colProductCode));
            //                if (string.IsNullOrEmpty(productCode)) continue;
            //                string pathDowload = Path.Combine(fbd.SelectedPath, $"{productCode}.pdf");
            //                string url = $"http://113.190.234.64:8083/api/project/{pathPattern}/{productCode}.pdf";

            //                WebClient webClient = new WebClient();
            //                webClient.DownloadFile(url, pathDowload);
            //                Process.Start(pathDowload);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show($"File [{productCode}.pdf] không tồn tại!\n{ex.Message}", "Thông báo");
            //        }
            //    }
        }

        private void tảiFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDownloadFile_Click(null, null);
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            frmRegisterIdeaTypeSynthetic frm = new frmRegisterIdeaTypeSynthetic();
            frm.ShowDialog();
            if(frm.DialogResult == DialogResult.OK)
            {
                LoadRegisterIdeaType();
            }
        }


        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtFilterText_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}