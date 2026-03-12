using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmEmployeeLuckyNumber : _Forms
    {
        public frmEmployeeLuckyNumber()
        {
            InitializeComponent();
        }

        private void frmEmployeeLuckyNumber_Load(object sender, EventArgs e)
        {
            txtYear.Value = DateTime.Now.Year;

            LoadDepartment();
            LoadEmployee();
            LoadData();
        }

        void LoadData()
        {
            int year = TextUtils.ToInt(txtYear.Value);
            //int quarter = TextUtils.ToInt(txtQuarter.Value);
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);


            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
            {
                DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeLuckyNumber", "A",
                                                    new string[] { "@Year", "@DepartmentID", "@EmployeeID", "@Keyword" },
                                                    new object[] { year, departmentID, employeeID, txtKeyword.Text.Trim() });


                grdData.DataSource = dt;

            }
        }

        void LoadDepartment()
        {
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();

            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = list;

            cboDepartment.EditValue = Global.DepartmentID;
        }

        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"DanhSachBocTham_{txtYear.Value}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = f.FileName;

                XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
