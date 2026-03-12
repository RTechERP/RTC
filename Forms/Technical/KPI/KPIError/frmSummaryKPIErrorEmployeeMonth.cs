using DevExpress.XtraGrid.Columns;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;

namespace BMS
{
    public partial class frmSummaryKPIErrorEmployeeMonth : _Forms
    {
        public int departmentID = 0;
        public string deName;
        public frmSummaryKPIErrorEmployeeMonth()
        {
            InitializeComponent();
        }

        private void frmSummaryKPIErrorEmployeeMonth_Load(object sender, EventArgs e)
        {
            this.Text += " - " + deName;
            LoadDepartment();
            LoadKPIErrorType();
            LoadData();

            dtpStartDate_TK.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpEndDate_TK.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddSeconds(-1);
            LoadData_TK();

            dtpStartDate_BD.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpEndDate_BD.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddSeconds(-1);
            LoadData_BD();

            
        }
        #region THỐNG KÊ LỖI VI PHẠM
        void LoadData_TK()
        {
            if (dtpStartDate_TK.Value.Year != dtpEndDate_TK.Value.Year)
            {
                MessageBox.Show("Ngày bắt đầu và ngày kết thúc phải trong cùng 1 năm!", "Thông báo");
                return;
            }

            int startMonth = dtpStartDate_TK.Value.Month;
            int endMonth = dtpEndDate_TK.Value.Month;

            gridBand1.Caption = $"THỐNG KÊ TỪ THÁNG {dtpStartDate_TK.Value.Month} ĐẾN THÁNG {dtpEndDate_TK.Value.Month} NĂM {dtpEndDate_TK.Value.Year}";
            gridBand1.Columns.Clear();
            for (int i = startMonth; i <= endMonth; i++)
            {
                BandedGridColumn col = new BandedGridColumn();
                col.Caption = $"Tháng {i}";
                col.Visible = true;
                col.FieldName = $"Month{i}";
                col.Width = 70;
                col.OptionsColumn.FixedWidth = true;
                col.ColumnEdit = repositoryItemMemoEdit1;
                col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                col.SummaryItem.FieldName = $"Month{i}";
                gridBand1.Columns.Add(col);
            }

            DateTime dateStart = new DateTime(dtpStartDate_TK.Value.Year, dtpStartDate_TK.Value.Month, dtpStartDate_TK.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpEndDate_TK.Value.Year, dtpEndDate_TK.Value.Month, dtpEndDate_TK.Value.Day, 23, 59, 59);

            int typeID = TextUtils.ToInt(cboKPIErrorType_TK.EditValue);//LinhTN update 13/11/2024 -add @TypeID
            int departmentID = TextUtils.ToInt(cboDepartment_TK.EditValue);//LinhTN update 13/11/2024 -add @TypeID


            DataTable dt = TextUtils.LoadDataFromSP("spGetSummaryKPIErrorMonth", "A",
                                                    new string[] { "@StartDate", "@EndDate", "@Keyword", "@TypeID", "@DepartmentID" },
                                                    new object[] { dateStart, dateEnd, txtKeyword_TK.Text.Trim(), typeID, departmentID });
            grdData_TK.DataSource = dt;
        }

        private void btnFind_TK_Click(object sender, EventArgs e)
        {
            LoadData_TK();
        }

        private void txtKeyword_TK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadData_TK();
        }
        private void dtpStartDate_TK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadData_TK();
        }

        private void dtpEndDate_TK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadData_TK();
        }
        private void grvData_TK_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int startMonth = dtpStartDate_TK.Value.Month;
            int endMonth = dtpEndDate_TK.Value.Month;
            int cellValue = 0;

            if (!e.Column.ToString().Contains("Tháng")) return;
            for (int i = startMonth; i <= endMonth; i++)
            {
                if (e.Column.ToString() == $"Tháng {i}")
                {
                    cellValue = TextUtils.ToInt(grvData_TK.GetRowCellValue(e.RowHandle, e.Column));
                }
            }

            if (cellValue >= 10)
            {
                e.Appearance.BackColor = Color.FromArgb(239, 31, 62);
                e.Appearance.ForeColor = Color.White;
            }
            else if (cellValue >= 5)
            {
                e.Appearance.BackColor = Color.FromArgb(255, 255, 74);
                e.Appearance.ForeColor = Color.Black;
            }
        }
        #endregion

        #region BIỂU ĐỒ THỐNG KÊ
        void LoadData_BD()
        {
            if (dtpStartDate_BD.Value.Year != dtpEndDate_BD.Value.Year)
            {
                MessageBox.Show("Ngày bắt đầu và ngày kết thúc phải trong cùng 1 năm!", "Thông báo");
                return;
            }

            DateTime dateStart = new DateTime(dtpStartDate_BD.Value.Year, dtpStartDate_BD.Value.Month, dtpStartDate_BD.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpEndDate_BD.Value.Year, dtpEndDate_BD.Value.Month, dtpEndDate_BD.Value.Day, 23, 59, 59);

            int typeID = TextUtils.ToInt(cboKPIErrorType_BD.EditValue); //LinhTN update 13/11/2024 -add @TypeID
            int departmentID = TextUtils.ToInt(cboDepartment_BD.EditValue); //LinhTN update 13/11/2024 -add @TypeID

            DataTable dt = TextUtils.LoadDataFromSP("spGetSummaryKPIErrorMonth", "A",
                                                    new string[] { "@StartDate", "@EndDate", "@TypeID", "@DepartmentID" },
                                                    new object[] { dateStart, dateEnd, typeID, departmentID });

            chartKPIErrorInMonth.DataSource = null;
            chartKPIErrorInMonth.Series.Clear();

            int startMonth = dtpStartDate_BD.Value.Month;
            int endMonth = dtpEndDate_BD.Value.Month;
            for (int i = startMonth; i <= endMonth; i++)
            {
                if (dt.Columns.Contains($"Month{i}"))
                {
                    Series series = new Series($"Tháng {i}", ViewType.Bar);
                    series.ArgumentDataMember = "Code";
                    series.ValueDataMembers.AddRange($"Month{i}");

                    series.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
                    series.ToolTipPointPattern = "{A}: {V} lỗi \nNội dung: {Content}";

                    chartKPIErrorInMonth.Series.Add(series);
                }
            }

            chartKPIErrorInMonth.DataSource = dt;

            XYDiagram diagram = chartKPIErrorInMonth.Diagram as XYDiagram;
            if (diagram != null)
            {
                diagram.AxisY.NumericScaleOptions.GridSpacing = 1;
            }

            chartKPIErrorInMonth.Titles.Clear();
            chartKPIErrorInMonth.Titles.Add(new ChartTitle
            {
                Text = $"THỐNG KÊ LỖI VI PHẠM TỪ THÁNG {dtpStartDate_BD.Value.Month} ĐẾN THÁNG {dtpEndDate_BD.Value.Month} NĂM {dtpEndDate_BD.Value.Year}",
                Font = new Font("Tahoma", 14, FontStyle.Bold),
                TextColor = Color.Orange
            });

            chartKPIErrorInMonth.Legend.MarkerMode = LegendMarkerMode.CheckBoxAndMarker;
            chartKPIErrorInMonth.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chartKPIErrorInMonth.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            chartKPIErrorInMonth.Legend.Direction = LegendDirection.LeftToRight;
        }


        //LinhTN update 13/11/2024 - add
        void LoadKPIErrorType()
        {
            var dt = SQLHelper<KPIErrorTypeModel>.FindByAttribute("IsDelete", 0);
            cboKPIErrorType_TK.Properties.DisplayMember = "Code";
            cboKPIErrorType_TK.Properties.ValueMember = "ID";
            cboKPIErrorType_TK.Properties.DataSource = dt;

            cboKPIErrorType_BD.Properties.DisplayMember = "Code";
            cboKPIErrorType_BD.Properties.ValueMember = "ID";
            cboKPIErrorType_BD.Properties.DataSource = dt;
        }

        void LoadDepartment()
        {
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();

            cboDepartment_TK.Properties.ValueMember = "ID";
            cboDepartment_TK.Properties.DisplayMember = "Name";
            cboDepartment_TK.Properties.DataSource = list;

            cboDepartment_BD.Properties.ValueMember = "ID";
            cboDepartment_BD.Properties.DisplayMember = "Name";
            cboDepartment_BD.Properties.DataSource = list;

            cboDepartment_TK.EditValue = departmentID;
            cboDepartment_BD.EditValue = departmentID;
        }

        void LoadData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetKPIError", "A", new string[] { "@Keyword", "@DepartmentID" }, new object[] { "", departmentID });
            grdData.DataSource = dt;
        }
        private void dtpStartDate_BD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadData_BD();
        }

        private void dtpEndDate_BD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadData_BD();
        }
        #endregion

        private void btnFind_BD_Click(object sender, EventArgs e)
        {
            LoadData_BD();
        }

        private void grdData_TK_Click(object sender, EventArgs e)
        {

        }

        private void grvData_TK_KeyDown(object sender, KeyEventArgs e)
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