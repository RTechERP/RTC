using BMS.Model;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmKPIErrorEmployeeSummaryMax : _Forms
    {
        public int departmentID = 0;
        public string deName;
        List<GridColumn> listCols = new List<GridColumn>();
        public frmKPIErrorEmployeeSummaryMax()
        {
            InitializeComponent();
        }

        private void frmKPIErrorEmployeeSummaryMax_Load(object sender, EventArgs e)
        {
            this.Text += " - " + deName;
            DateTime dateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
            dtpDateStart.Value = dateNow.AddMonths(-2);
            dtpDateEnd.Value = dateNow.AddMonths(+1).AddDays(-1);

            LoadDepartment();
            LoadEmployee();
            LoadKPIErrorType();

            LoadData();
        }


        void LoadData()
        {
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            int departmenID = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            int kpiErrorTypeID = TextUtils.ToInt(cboKPIErrorType.EditValue);

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu..."))
            {
                AddColumn(dateStart, dateEnd);


                DataTable dt = TextUtils.LoadDataFromSP("spGetKPIErrorEmployeeSummaryMax", "A",
                                        new string[] { "@DateStart", "@DateEnd", "@DepartmentID", "@EmployeeID", "@KPIErrorTypeID" },
                                        new object[] { dateStart, dateEnd, departmenID, employeeID, kpiErrorTypeID });

                grdData.DataSource = dt;


                //grvData.Columns[$"{DateTime.Now.ToString("MMyyyy")}"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

                //grvData.GroupSummary.Clear();

                //GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                //item1.FieldName = $"{DateTime.Now.ToString("MMyyyy")}";
                //item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //item1.DisplayFormat = "{0}";
                //item1.ShowInGroupColumnFooter = grvData.Columns[item1.FieldName];
                //grvData.GroupSummary.Add(item1);

                //GridGroupSummaryItem item2 = new GridGroupSummaryItem();
                //item2.FieldName = $"{DateTime.Now.AddMonths(-1).ToString("MMyyyy")}";
                //item2.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //item2.DisplayFormat = "{0}";
                //item2.ShowInGroupColumnFooter = grvData.Columns[item2.FieldName];
                //grvData.GroupSummary.Add(item2);

                //GridGroupSummaryItem item3 = new GridGroupSummaryItem();
                //item3.FieldName = $"{DateTime.Now.AddMonths(-2).ToString("MMyyyy")}";
                //item3.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //item3.DisplayFormat = "{0}";
                //item3.ShowInGroupColumnFooter = grvData.Columns[item3.FieldName];
                //grvData.GroupSummary.Add(item3);

                //grvData.OptionsBehavior.AlignGroupSummaryInGroupRow = DefaultBoolean.True;
                //grvData.OptionsBehavior.AutoExpandAllGroups = false;

                grvData.OptionsMenu.ShowGroupSortSummaryItems = true;
                GridSummaryItem summaryItemMaxOrderSum = grvData.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, $"{DateTime.Now.ToString("MMyyyy")}", null, "{}");
                GridColumn firstGroupColumn = grvData.SortInfo[1].Column;
                //GridColumn firstGroupColumn = grvData.Columns[item1.FieldName];
                GroupSummarySortInfo[] groupSummaryToSort = { new GroupSummarySortInfo(summaryItemMaxOrderSum, firstGroupColumn, ColumnSortOrder.Descending) };
                grvData.GroupSummarySortInfo.ClearAndAddRange(groupSummaryToSort);

                //GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                //item1.FieldName = $"{DateTime.Now.ToString("MMyyyy")}";
                //item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //item1.DisplayFormat = "Total {0:n0}";
                //item1.ShowInGroupColumnFooter = grvData.Columns[item1.FieldName];
                //grvData.GroupSummary.Add(item1);

            }
            grvData.ExpandGroupLevel(0);
        }


        void AddColumn(DateTime dateStart, DateTime dateEnd)
        {
            dateStart = new DateTime(dateStart.Year, dateStart.Month, 1);
            dateEnd = new DateTime(dateEnd.Year, dateEnd.Month, 1);
            if (listCols.Count > 0)
            {
                foreach (var item in listCols)
                {
                    grvData.Columns.Remove(item);
                }
                listCols.Clear();
            }

            grvData.GroupSummary.Clear();

            while (dateStart <= dateEnd)
            {
                GridColumn col = new GridColumn();
                col.Visible = true;
                col.FieldName = dateStart.ToString("MMyyyy");
                col.Caption = dateStart.ToString("yyyy / MM");
                col.Width = 100;
                col.OptionsColumn.AllowMerge = DefaultBoolean.False;
                col.Summary.Add(DevExpress.Data.SummaryItemType.Sum);
                grvData.Columns.Add(col);


                GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                item1.FieldName = col.FieldName;
                item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                item1.DisplayFormat = "{0}";
                item1.ShowInGroupColumnFooter = grvData.Columns[col.FieldName];
                grvData.GroupSummary.Add(item1);

                dateStart = dateStart.AddMonths(+1);
                listCols.Add(col);
            }

            grvData.AutoFillColumn = colContent;
        }

        void LoadDepartment()
        {
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();

            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = list;

            cboDepartment.EditValue = departmentID;
        }

        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }

        void LoadKPIErrorType()
        {
            List<KPIErrorTypeModel> list = SQLHelper<KPIErrorTypeModel>.FindAll();

            cboKPIErrorType.Properties.ValueMember = "ID";
            cboKPIErrorType.Properties.DisplayMember = "Name";
            cboKPIErrorType.Properties.DataSource = list;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"TongHopNhanVienNhieuLoi_T{dtpDateStart.Value.ToString("MMyyyy")}-T{dtpDateEnd.Value.ToString("MMyyyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"DanhSachTongHopLoiCuaNhanVien_Thang{txtMonth.Value}Nam{txtYear.Value}.xlsx");
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

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }
    }
}
