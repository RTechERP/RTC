using BMS.Model;
using BMS.Utils;
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
    public partial class frmKpiSyntheticYears : _Forms
    {
        private int txt;

        public frmKpiSyntheticYears()
        {
            InitializeComponent();
        }

        private void frmKpiSyntheticYears_Load(object sender, EventArgs e)
        {
            txtYears.Value = DateTime.Now.Year;

            LoadDepartment();
            LoadEmployee();
            LoadData();
        }

        void LoadDepartment()
        {
            var list = SQLHelper<DepartmentModel>.FindAll().ToList();
            cboDepartMent.Properties.ValueMember = "ID";
            cboDepartMent.Properties.DisplayMember = "Name";
            cboDepartMent.Properties.DataSource = list;

            cboDepartMent.EditValue = Global.DepartmentID;


            
        }

        void LoadEmployee()
        {
            var list = SQLHelper<EmployeeModel>.FindAll().ToList();
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = list;

            cboEmployee.EditValue = Global.EmployeeID;

            var exp1 = new Expression("UserID", Global.UserID);
            var exp2 = new Expression("Code", "N38");
            var vUserGroupLinks = SQLHelper<vUserGroupLinkModel>.FindByExpression(exp1.And(exp2));
            cboEmployee.Enabled = vUserGroupLinks.Count > 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string keyword = txtKeywords.Text.Trim();
            int year = TextUtils.ToInt(txtYears.Value);
            int emID = TextUtils.ToInt(cboEmployee.EditValue);
            int deID = TextUtils.ToInt(cboDepartMent.EditValue);

            DataTable dt = TextUtils.LoadDataFromSP("spGetSyntheticKPI", "A",
                new string[] { "@Year", "@DepartmentID", "@EmployeeID", "@Keyword" },
                new object[] { year, deID, emID, keyword });
            grdMain.DataSource = dt;

        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DateTime dti = DateTime.Now;
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            //f.FileName = $"TongHopKPINam_{dti.Day}{dti.Month}{dti.Year}.xlsx";
            f.FileName = $"TongHopKPINam_{txtYears.Value}_{DateTime.Now.ToString("HHmmss")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = f.FileName;

                //XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();
                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdMain;

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);
                        Process.Start(filepath);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int year = TextUtils.ToInt(txtYears.Value);
            grvMain.CloseEditor();

            for (int i = 0; i < grvMain.RowCount; i++)
            {
                int employeeID = TextUtils.ToInt(grvMain.GetRowCellValue(i, colEmployeeID));
                var exp1 = new Expression(KPIEmployeePointYearModel_Enum.EmployeeID, employeeID);
                var exp2 = new Expression(KPIEmployeePointYearModel_Enum.YearValue, year);
                var exp3 = new Expression(KPIEmployeePointYearModel_Enum.IsDeleted, 0);

                var pointYear = SQLHelper<KPIEmployeePointYearModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault() ?? new KPIEmployeePointYearModel();

                pointYear.EmployeeID = employeeID;
                pointYear.YearValue = year;
                pointYear.PointPercentYear = TextUtils.ToDecimal(grvMain.GetRowCellValue(i, colPointPercentYear));

                if (pointYear.ID > 0)
                {
                    SQLHelper<KPIEmployeePointYearModel>.Update(pointYear);
                }
                else
                {
                    SQLHelper<KPIEmployeePointYearModel>.Insert(pointYear);
                }

            }

            LoadData();
        }

        private void btnApproved_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Approved(1);
        }


        void Approved(int isApproveYear)
        {
            int year = TextUtils.ToInt(txtYears.Value);
            //grvMain.CloseEditor();

            int[] selectedRows = grvMain.GetSelectedRows();

            foreach (var row in selectedRows)
            {
                int id = TextUtils.ToInt(grvMain.GetRowCellValue(row, colKPIEmployeePointYearID));
                //var exp1 = new Expression(KPIEmployeePointYearModel_Enum.EmployeeID, employeeID);
                //var exp2 = new Expression(KPIEmployeePointYearModel_Enum.YearValue, year);
                //var exp3 = new Expression(KPIEmployeePointYearModel_Enum.IsDeleted, 0);

                var pointYear = SQLHelper<KPIEmployeePointYearModel>.FindByID(id);
                pointYear.IsApproveYear = isApproveYear;

                if (pointYear.ID > 0)
                {
                    SQLHelper<KPIEmployeePointYearModel>.Update(pointYear);
                }
                else
                {
                    SQLHelper<KPIEmployeePointYearModel>.Insert(pointYear);
                }

            }

            LoadData();
        }

        private void btnUnApproved_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Approved(0);
        }
    }

}
