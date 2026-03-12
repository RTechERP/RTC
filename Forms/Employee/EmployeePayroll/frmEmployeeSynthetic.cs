using BMS;
using DevExpress.Office.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
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
    public partial class frmEmployeeSynthetic : _Forms
    {
        public frmEmployeeSynthetic()
        {
            InitializeComponent();
        }



        private void frmEmployeeSynthetic_Load(object sender, EventArgs e)
        {
            dtpYear.Value = DateTime.Now.Year;
            dtpMonth.Value = DateTime.Now.Month;

            loadDepartment();
            loadEmployee();
            loadData();
        }

        void loadEmployee()
        {
            DataTable dt = TextUtils.Select("SELECT ID, Code,FullName FROM Employee WHERE Status <> 1");

            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }

        void loadDepartment()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Department");

            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = dt;
        }

        void loadData()
        {
            LoadWeekDay();
            DataTable dt = TextUtils.GetDataTableFromSP("spGetEmployeeSyntheticByMonth",
                new string[] { "@Month", "@Year", "@EmployeeID", "@DepartmentID" },
                new object[] { TextUtils.ToInt(dtpMonth.Value), TextUtils.ToInt(dtpYear.Value), TextUtils.ToInt(cboEmployee.EditValue), TextUtils.ToInt(cboDepartment.EditValue) });
            grdData.DataSource = dt;

            bandTitle.Caption = $"BẢNG TỔNG HỢP CHI TIẾT THÁNG  {dtpMonth.Value}/{dtpYear.Value}";

        }

        private void dtpMonth_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void dtpYear_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        void LoadWeekDay()
        {
            List<GridBand> countBand = bandTitle.Children.ToList();
            for (int i = 0; i < DateTime.DaysInMonth((int)dtpYear.Value, (int)dtpMonth.Value); i++)
            {
                countBand[i].AppearanceHeader.BackColor = SystemColors.Control;
                countBand[i].AppearanceHeader.ForeColor = Color.Black;
                //DateTime date = Convert.ToDateTime(dtpYear.Value.ToString() + "/" + dtpMonth.Value.ToString() + "/" + (i + 1));
                DateTime date = new DateTime((int)dtpYear.Value, (int)dtpMonth.Value, i + 1);
                countBand[i].Caption = date.ToString("ddd");

                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    countBand[i].AppearanceHeader.BackColor = Color.FromName("Tan");
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                //optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.SheetName = "Tổng hợp";
                //grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/TongHop_{dtpMonth.Value}-{dtpYear.Value}.xls";
                    grvData.ExportToXls(filepath, optionsEx);

                    Process.Start(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    //grvData.ExportToExcelOld($"{f.SelectedPath}/KeHoachDuAn_{cboProject.Text}.xls");
                }
                grvData.ClearSelection();
            }
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            e.DisplayText = TextUtils.ToString(e.Value).Replace("\n", "\n").Trim();
        }

        private void grvData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            try
            {
                if (e.Column.FieldName == "DepartmentName")
                {
                    object val1 = view.GetListSourceRowCellValue(e.ListSourceRowIndex1, "DepartmentID");
                    object val2 = view.GetListSourceRowCellValue(e.ListSourceRowIndex2, "DepartmentID");
                    e.Handled = true;
                    e.Result = System.Collections.Comparer.Default.Compare(val1, val2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void dtpMonth_ValueChanged_1(object sender, EventArgs e)
        {
            loadData();
        }

        private void dtpYear_ValueChanged_1(object sender, EventArgs e)
        {
            loadData();
        }
    }
}