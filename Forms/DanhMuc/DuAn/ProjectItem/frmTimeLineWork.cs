using BMS.Model;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
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
    public partial class frmTimeLineWork : _Forms
    {
        List<GridColumn> listCol = new List<GridColumn>();
        DataSet dataSet = new DataSet();

        public frmTimeLineWork()
        {
            InitializeComponent();
        }

        private void frmTimeLineWork_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+1).AddDays(-1);
            //dtpDateEnd.Value = dtpDateStart.Value.AddDays(3);
            loadDepartment();
            loadUserTeam();
            loadUser();

            loadData();
        }

        void loadDepartment()
        {
            List<DepartmentModel> listDept = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = listDept;
            cboDepartment.EditValue = Global.DepartmentID;
        }

        void loadUserTeam()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            List<UserTeamModel> listDept = SQLHelper<UserTeamModel>.FindAll().Where(x => x.DepartmentID == departmentID).ToList();
            cboTeam.Properties.ValueMember = "ID";
            cboTeam.Properties.DisplayMember = "Name";
            cboTeam.Properties.DataSource = listDept;
            cboTeam.EditValue = Global.UserTeamID;
        }

        void loadUser()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboUser.Properties.ValueMember = "UserID";
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.DataSource = dt;
        }

        void loadData()
        {
            using (WaitDialogForm wait = new WaitDialogForm("Loading data...", "Please wait"))
            {
                DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
                DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
                int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
                int userTeamID = TextUtils.ToInt(cboTeam.EditValue);
                int userID = TextUtils.ToInt(cboUser.EditValue);
                int showDetail = TextUtils.ToInt(chkViewDetail.Checked);
                string typeNumber = TextUtils.ToString(cboType.EditValue);

                addColumn();

                try
                {
                    DataTable dtData = TextUtils.LoadDataFromSP("spGetTimelineEmployeeWorks", "A"
                                          , new string[] { "@DateStart", "@DateEnd", "@DepartmentID", "@UserTeamID", "@UserID", "@IsShowDetail", "@TypeNumber" }
                                          , new object[] { dateStart, dateEnd, departmentID, userTeamID, userID, showDetail, typeNumber });
                    grdData.DataSource = dtData;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải dữ liệu.\nVui lòng chọn lại khoảng thời gian!","Thông báo");
                }

            }
        }


        void loadDetail()
        {
            if (grvData.FocusedColumn == colFullName || grvData.FocusedColumn == colTypeText)
            {
                return;
            }

            int userID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUserID));
            int type = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTypeNumber));
            DateTime date = TextUtils.ToDate5(grvData.FocusedColumn.FieldName);
            string typeText = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTypeText));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)).Replace("\n", "");


            barHeaderItem1.Caption = $"{typeText.ToUpper()} - {date.ToString("dd/MM/yyyy")}";

            DataTable dt = TextUtils.LoadDataFromSP("spGetTimelineEmployeeWorksDetail", "A"
                                           , new string[] { "@UserID", "@TypeNumber", "@Date", "@Code" }
                                           , new object[] { userID, type, date, code });

            if (dt.Rows.Count <= 0)
            {
                return;
            }
            string workcontent = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                workcontent += TextUtils.ToString(dt.Rows[i]["WorkContent"]) + "\r\n\n";
            }

            barEditItem1.AutoFillWidth = true;
            barEditItem2.EditValue = workcontent;

            barStaticItem1.Caption = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            popupMenu1.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
        }

        void addColumn(DataTable dt)
        {


            if (listCol.Count > 0)
            {
                foreach (GridColumn item in listCol)
                {
                    grvData.Columns.Remove(item);
                }
            }

            listCol.Clear();

            foreach (DataRow row in dt.Rows)
            {
                var value = TextUtils.ToDate5(row["AllDates"]);
                GridColumn col = new GridColumn();
                col.Visible = true;
                col.Caption = value.ToString("dd/MM/yyyy");
                col.FieldName = value.ToString("yyyy-MM-dd");
                col.ColumnEdit = repositoryItemMemoEdit1;
                col.Width = 100;
                col.BestFit();
                grvData.Columns.Add(col);
                listCol.Add(col);
            }

        }

        void addColumn()
        {
            DateTime dateStart = dtpDateStart.Value;
            DateTime dateEnd = dtpDateEnd.Value;

            grvData.Columns.Clear();

            grvData.BeginUpdate();

            //Add column default
            GridColumn colFullName = new GridColumn();
            colFullName.Visible = true;
            colFullName.Caption = "Họ tên";
            colFullName.FieldName = "FullName";
            colFullName.ColumnEdit = repositoryItemMemoEdit1;
            colFullName.Fixed = FixedStyle.Left;
            colFullName.Width = 200;

            GridColumn colTypeText = new GridColumn();
            colTypeText.Visible = true;
            colTypeText.Caption = "Loại";
            colTypeText.FieldName = "TypeText";
            colTypeText.ColumnEdit = repositoryItemMemoEdit1;
            colTypeText.Fixed = FixedStyle.Left;
            colTypeText.Width = 150;
            colTypeText.OptionsColumn.AllowSort = DefaultBoolean.False;

            grvData.Columns.Add(colFullName);
            grvData.Columns.Add(colTypeText);
            grvData.Columns.AddField("UserID");
            grvData.Columns.AddField("TypeNumber");

            //Add column by date
            while (dateStart <= dateEnd)
            {

                GridColumn col = new GridColumn();
                col.Visible = true;
                col.Caption = dateStart.ToString("dd/MM/yyyy");
                col.FieldName = dateStart.ToString("yyyy-MM-dd");
                col.ColumnEdit = repositoryItemMemoEdit1;
                col.Width = 120;
                col.OptionsFilter.AllowFilter = false;
                col.OptionsColumn.AllowSort = DefaultBoolean.False;
                col.BestFit();
                grvData.Columns.Add(col);
                listCol.Add(col);

                dateStart = dateStart.AddDays(+1);
            }
            grvData.EndUpdate();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadUserTeam();
            loadData();
        }

        private void cboTeam_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboUser_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void grvData_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName == "FullName")
            {
                int value1 = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle1, "UserID"));
                int value2 = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle2, "UserID"));
                e.Merge = (value1 == value2);
            }

            e.Handled = true;
            return;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.FileName = $"TimeLineCongViec_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {

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

                    compositeLink.ExportToXlsx(f.FileName, optionsEx);
                    Process.Start(f.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setValue(dataSet.Tables[0], dataSet.Tables[1]);
        }

        void setValue(DataTable dtData, DataTable dtCol)
        {
            Worksheet worksheet = spreadsheetControl1.Document.Worksheets[0];
            worksheet.Clear(worksheet.GetDataRange());

            spreadsheetControl1.BeginUpdate();

            using (WaitDialogForm wait = new WaitDialogForm("Loading data...", "Please wait"))
            {
                //Set header
                worksheet.Cells["A1"].SetValue("Tên nhân viên");
                worksheet.Cells["B1"].SetValue("Loại");

                for (int i = 0; i < dtCol.Rows.Count; i++)
                {
                    var value = TextUtils.ToDate5(dtCol.Rows[i]["AllDates"]);
                    worksheet.Columns[i + 2][0].SetValue(value.ToString("dd/MM/yyyy"));
                }

                CellRange firstCellHeader = worksheet.Columns[0][0];
                CellRange lastCellHeader = worksheet.Columns[dtCol.Rows.Count + 1][0];

                var rangeHeader = worksheet.Range[$"{firstCellHeader.GetReferenceA1()}:{lastCellHeader.GetReferenceA1()}"];
                rangeHeader.FillColor = Color.LightGray;
                rangeHeader.Font.Bold = true;
                rangeHeader.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                rangeHeader.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                rangeHeader.RowHeight = 150;

                //Set data
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    worksheet.Cells[$"A{i + 2}"].SetValue(TextUtils.ToString(dtData.Rows[i]["FullName"]));
                    worksheet.Cells[$"B{i + 2}"].SetValue(TextUtils.ToString(dtData.Rows[i]["TypeText"]));
                    for (int j = 0; j < dtCol.Rows.Count; j++)
                    {
                        DateTime fieldName = TextUtils.ToDate5(dtCol.Rows[j]["AllDates"]);
                        string cellValue = TextUtils.ToString(dtData.Rows[i][fieldName.ToString("yyyy-MM-dd")]);
                        worksheet.Columns[j + 2][i + 1].SetValue(cellValue);
                    }
                }

                CellRange lastCell = worksheet.Columns[dtCol.Rows.Count + 1][dtData.Rows.Count];
                var rangeData = worksheet.Range[$"A1:{lastCell.GetReferenceA1()}"];
                rangeData.Borders.SetAllBorders(Color.Black, BorderLineStyle.Thin);
                rangeData.ColumnWidth = 500;

                worksheet.GetDataRange().Alignment.WrapText = true;
                worksheet.GetDataRange().Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                worksheet.FreezePanes(0, 1);

                //Merge cell
                for (int i = 1; i < rangeData.RowCount; i++)
                {
                    CellRange cell1 = worksheet.Columns[0][i];
                    CellRange cell2 = worksheet.Columns[rangeData.RightColumnIndex][i];

                    var range = worksheet.Range[$"{cell1.GetReferenceA1()}:{cell2.GetReferenceA1()}"];
                    var rangeSameData = range.GroupBy(x => x.Value).Where(x => x.Count() > 1).ToList();
                    foreach (var item in rangeSameData)
                    {
                        var firstCellMerge = item.FirstOrDefault().GetReferenceA1();
                        var lastCellMerge = item.LastOrDefault().GetReferenceA1();

                        var rangeMerge = worksheet.Range[$"{firstCellMerge}:{lastCellMerge}"];
                        //rangeMerge.Merge(MergeCellsMode.ByRows);

                    }
                }

            }

            spreadsheetControl1.EndUpdate();
        }

        private void cboType_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void chkViewDetail_CheckedChanged(object sender, EventArgs e)
        {   
            loadData();
        }

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            loadDetail();
        }

        private void grdData_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                loadDetail();
            }
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle > 2)
            {
                int user1 = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle - 2, "UserID"));
                int user2 = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle - 1, "UserID"));
                int user3 = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, "UserID"));
            }
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
        }
    }
}
