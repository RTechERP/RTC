using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
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
    public partial class frmTimeSheetOT : _Forms
    {
        public frmTimeSheetOT()
        {
            InitializeComponent();
        }

        private void frmTimeSheetOT_Load(object sender, EventArgs e)
        {
            txtMonth.Value = DateTime.Now.Month;
            txtYear.Value = DateTime.Now.Year;

            loadDepartment();
            loadEmployee();
            loadData();
        }

        private void loadData()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);

            using (WaitDialogForm wait = new WaitDialogForm("Loading data...", "Please wait"))
            {
                DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeOvertimeByMonth", "A"
                , new string[] { "@Month", "@Year", "@DepartmentID", "@EmployeeID", "@Keyword" }
                , new object[] { TextUtils.ToInt(txtMonth.Value), TextUtils.ToInt(txtYear.Value), departmentID, employeeID, txtKeyword.Text.Trim() });
                grdData.DataSource = dt;

                loadWeekDays();

                bandTitle.Caption = $"BẢNG CHẤM CÔNG LÀM THÊM THÁNG {txtMonth.Value}";
            }
        }

        void loadDepartment()
        {
            List<DepartmentModel> listDepartment = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = listDepartment;
        }

        void loadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee","A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"BaoCaoLamThem_T{txtMonth.Text}_{txtYear.Value}.xlsx");
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;


                grvData.OptionsPrint.AutoWidth = false;
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

        private void loadWeekDays()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetDayOfWeek", "A"
                , new string[] { "@Month", "@Year" }
                , new object[] { TextUtils.ToInt(txtMonth.Value), TextUtils.ToInt(txtYear.Value) });
            List<GridBand> listBand = bandTitle.Children.ToList();

            foreach (var item in listBand)
            {
                string value = TextUtils.ToString(dt.Rows[0][$"D{item.Index + 1}"]);
                string caption = value.Substring(0, value.LastIndexOf(";"));
                int status = TextUtils.ToInt(value.Substring(value.LastIndexOf(";") + 1));
                item.Caption = caption;
                item.OptionsBand.AllowMove = false;

                item.AppearanceHeader.BackColor = SystemColors.Control;
                item.AppearanceHeader.ForeColor = Color.Black;
                if (status == 1 || status == 7)
                {
                    item.AppearanceHeader.BackColor = Color.FromName("Tan");
                    //item.AppearanceHeader.BackColor = ColorTranslator.FromHtml("#EEECE1");
                }
            }
        }

        private void txtMonth_ValueChanged(object sender, EventArgs e)
        {
            //lblMonth.Text = TextUtils.ToString(txtMonth.Value);
            loadData();
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void grvData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {

        }
    }
}
