using BMS;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmJobRequirementSumarize : _Forms
    {
        public DateTime DateStart;
        public DateTime DateEnd;
        public string Request = "";
        public int EmployeeId = 0;
        public int Step = 0;
        public int DepartmentId = 0;
        public frmJobRequirementSumarize()
        {
            InitializeComponent();
        }
        private void frmJobRequirementSumarize_Load(object sender, EventArgs e)
        {
           
            GetStep();
            GetAllEmployee();
            LoadDepartment();
            LoadDetails();
            GetAll();
        }
        private void LoadDetails()
        {
            dtpDateStart.Value = DateStart;
            dtpDateEnd.Value = DateEnd;
            txtKeyword.Text = Request;
            cboStep.SelectedValue = Step;
            cboDepartment.EditValue = DepartmentId;
            cboEmployee.EditValue = EmployeeId;

        }
        private void  GetAll()
        {
            DateTime dateStart = dtpDateStart.Value;
            DateTime dateEnd = dtpDateEnd.Value;
            string request = TextUtils.ToString(txtKeyword.Text).Trim();
            int employeeId = TextUtils.ToInt(cboEmployee.EditValue);
            int departmentId = TextUtils.ToInt(cboDepartment.EditValue);
            int step = TextUtils.ToInt(cboStep.SelectedValue);
            DataTable data = TextUtils.LoadDataFromSP("spGetSumarizeJobrequirement", "A",
                                                        new string[] { "@DateStart", "@DateEnd", "@Request", "@EmployeeId", "@Step", "@DepartmentId" },
                                                        new object[] { dateStart, dateEnd, request, employeeId, step, departmentId });
            grdData.DataSource = data;
        }
        private void GetStep()
        {

            List<object> listStep = new List<object>()
            {
                new {Step = 0,StepName = "Tất cả"},
                new {Step = 1,StepName = "NV đề nghị"},
                new {Step = 2,StepName = "TBP xác nhận"},
                new {Step = 3,StepName = "HR check hồ sơ"},
                new {Step = 4,StepName = "TBP HR xác nhận"},
                new {Step = 5,StepName = "Ban giám đốc xác nhận"},
                new {Step = 6,StepName = "Phòng mua hàng hoặc P.HCNS triển khai"}
            };
            cboStep.DataSource = listStep;
            cboStep.ValueMember = "Step";
            cboStep.DisplayMember = "StepName";
        }
        void LoadDepartment()
        {
            //DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { -1 });
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = list;

            if (Global.DepartmentCode == "GD" || Global.DepartmentCode == "KT" || Global.DepartmentCode == "HR" || Global.IsAdmin || Global.LoginName == "TrangLT" || Global.LoginName == "NV0058") //Nếu là BGĐ hoặc phòng kế toán hoặc nhận sự hoặc Lê Thị Trang
            {
                cboDepartment.EditValue = 0;
                cboDepartment.Enabled = true;
            }
            else
            {
                cboDepartment.EditValue = Global.DepartmentID;
                cboDepartment.Enabled = false;
            }
        }
        private void GetAllEmployee()
        {
            var data = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { -1 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = data;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            GetAll();
        }

        private void cboStep_SelectedValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void dtpDateEnd_ValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"TongHopYCCV_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"TongHopYCCV_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx");
                string filepath = f.FileName;

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

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.HighPriority = true;
            }
        }
    }
}
