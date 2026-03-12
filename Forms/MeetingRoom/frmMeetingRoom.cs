using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

using ClosedXML.Excel;
using BMS.Utils;

namespace BMS
{
    public partial class frmMeetingRoom : _Forms
    {
        public frmMeetingRoom()
        {
            InitializeComponent();
        }

        private void frmMeetingRoom_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            //dtpEndDate.Value = dtpStartDate.Value.AddDays(2);
            dtpEndDate.Value = dtpStartDate.Value;

            //if (Global.DepartmentCode == "HR" || Global.DepartmentCode == "KYTHUAT")
            //{
            //    dtpStartDate.Value = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            //    dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            //}

            //SplitPosition();
            LoadData();
        }


        private void LoadData()
        {


            DateTime dateStart = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetBookingRoom",
                new string[] { "@StartDate", "@EndDate" },
                new object[] { dateStart, dateEnd });

            LoadColTime();

            grdRoomOne.DataSource = ds.Tables[0];
            grdRoomTwo.DataSource = ds.Tables[1];
            grdRoomThree.DataSource = ds.Tables[2];

        }

        private void LoadColTime()
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 7, 30, 0);
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 17, 30, 0);

            //TimeSpan timeStep = TimeSpan.FromMinutes(30);
            //DateTime currentTime = start;

            int index = 2;
            TimeSpan spanStart = new TimeSpan(12, 30, 0);
            TimeSpan spanEnd = new TimeSpan(13, 00, 0);

            bandRoom1.Columns.Clear();
            bandRoom2.Columns.Clear();
            bandRoom3.Columns.Clear();

            //Add column default
            //BandedGridColumn colDate = new BandedGridColumn();
            //colDate.Caption = "Ngày";
            //colDate.Visible = true;
            //colDate.FieldName = "AllDate";
            //colDate.ColumnEdit = repositoryItemMemoEdit1;
            //colDate.MinWidth = 80;
            //colDate.Width = 80;
            //colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //colDate.DisplayFormat.FormatString = "dd/MM/yyyy";

            //BandedGridColumn colDayOfWeek = new BandedGridColumn();
            //colDayOfWeek.Caption = "Thứ";
            //colDayOfWeek.Visible = true;
            //colDayOfWeek.FieldName = "DayOfWeek";
            ////colDayOfWeek.ColumnEdit = repositoryItemMemoEdit1;
            //colDayOfWeek.MinWidth = 80;
            //colDayOfWeek.Width = 80;

            //BandedGridColumn colDate2 = new BandedGridColumn();
            //colDate2.Caption = "Ngày";
            //colDate2.Visible = true;
            //colDate2.FieldName = "AllDate";
            ////colDate2.ColumnEdit = repositoryItemMemoEdit1;
            //colDate2.MinWidth = 80;
            //colDate2.Width = 80;
            //colDate2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //colDate2.DisplayFormat.FormatString = "dd/MM/yyyy";


            //BandedGridColumn colDayOfWeek2 = new BandedGridColumn();
            //colDayOfWeek2.Caption = "Thứ";
            //colDayOfWeek2.Visible = true;
            //colDayOfWeek2.FieldName = "DayOfWeek";
            ////colDayOfWeek2.ColumnEdit = repositoryItemMemoEdit1;
            //colDayOfWeek2.MinWidth = 80;
            //colDayOfWeek2.Width = 80;

            //BandedGridColumn colDate3 = new BandedGridColumn();
            //colDate3.Caption = "Ngày";
            //colDate3.Visible = true;
            //colDate3.FieldName = "AllDate";
            ////colDate3.ColumnEdit = repositoryItemMemoEdit1;
            //colDate3.MinWidth = 80;
            //colDate3.Width = 80;
            //colDate3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //colDate3.DisplayFormat.FormatString = "dd/MM/yyyy";


            //BandedGridColumn colDayOfWeek3 = new BandedGridColumn();
            //colDayOfWeek3.Caption = "Thứ";
            //colDayOfWeek3.Visible = true;
            //colDayOfWeek3.FieldName = "DayOfWeek";
            ////colDayOfWeek3.ColumnEdit = repositoryItemMemoEdit1;
            //colDayOfWeek3.MinWidth = 80;
            //colDayOfWeek3.Width = 80;

            //gridBand1.Columns.Add(colDate);
            //gridBand1.Columns.Add(colDayOfWeek);

            //gridBand2.Columns.Add(colDate2);
            //gridBand2.Columns.Add(colDayOfWeek2);

            //gridBand3.Columns.Add(colDate3);
            //gridBand3.Columns.Add(colDayOfWeek3);

            repositoryItemMemoEdit1.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            while (start < end)
            {
                start = start.AddMinutes(30);
                if (start.TimeOfDay >= spanStart && start.TimeOfDay <= spanEnd)
                {
                    continue;
                }

                // Tạo mới cột cho mỗi bản ghi băng.
                BandedGridColumn timeColumn1 = new BandedGridColumn();
                timeColumn1.Caption = start.ToString("HH:mm");
                timeColumn1.Visible = true;
                //timeColumn1.VisibleIndex = index;
                timeColumn1.FieldName = start.ToString("HH:mm");
                timeColumn1.ColumnEdit = repositoryItemMemoEdit1;
                timeColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                //timeColumn1.DisplayFormat.FormatString = "#0";
                //timeColumn1.MinWidth = 80;
                timeColumn1.Width = 100;
                timeColumn1.OptionsColumn.FixedWidth = true;
                //timeColumn1.BestFit();
                bandRoom1.Columns.Add(timeColumn1);

                BandedGridColumn timeColumn2 = new BandedGridColumn();
                timeColumn2.Caption = start.ToString("HH:mm");
                timeColumn2.Visible = true;
                //timeColumn2.VisibleIndex = index;
                timeColumn2.FieldName = start.ToString("HH:mm");
                timeColumn2.ColumnEdit = repositoryItemMemoEdit1;
                //timeColumn2.MinWidth = 80;
                timeColumn2.Width = 100;
                timeColumn2.OptionsColumn.FixedWidth = true;
                //timeColumn2.BestFit();
                bandRoom2.Columns.Add(timeColumn2);

                BandedGridColumn timeColumn3 = new BandedGridColumn();
                timeColumn3.Caption = start.ToString("HH:mm");
                timeColumn3.Visible = true;
                //timeColumn2.VisibleIndex = index;
                timeColumn3.FieldName = start.ToString("HH:mm");
                timeColumn3.ColumnEdit = repositoryItemMemoEdit1;
                //timeColumn2.MinWidth = 80;
                timeColumn3.Width = 100;
                timeColumn3.OptionsColumn.FixedWidth = true;
                //timeColumn3.BestFit();
                bandRoom3.Columns.Add(timeColumn3);

                //currentTime = currentTime.Add(timeStep);
                index++;
            }

            // Auto-fit column widths for both grid views.
            //gridBand1.();
            //grvRoomTwo.BestFitColumns();


            //gridBand1.Width = ;
            //gridBand2.wit

        }

        private void SplitPosition()
        {
            //int middlePosition = splitContainerControl1.Height / 2;
            //splitContainerControl1.SplitterPosition = middlePosition;
        }
        private void frmMeetingRoom_Resize(object sender, EventArgs e)
        {
            SplitPosition();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmMeetingRoomDetail frm = new frmMeetingRoomDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }

        }


        private void grvRoomOne_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {


        }
        private void grvRoomOne_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.VisibleIndex > 0)
            {
                setColorCell(sender as GridView, e);
            }
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string data = TextUtils.ToString(grvRoomOne.GetFocusedValue());
            string data2 = TextUtils.ToString(grvRoomTwo.GetFocusedValue());
            string data3 = TextUtils.ToString(grvRoomThree.GetFocusedValue());
            GridView view = sender as GridView;
            var isFocus1 = grvRoomOne.IsFocusedView;
            var isFocus2 = grvRoomTwo.IsFocusedView;
            var isFocus3 = grvRoomThree.IsFocusedView;

            string[] values = new string[10];
            if (!string.IsNullOrEmpty(data) && isFocus1)
            {
                if (data.Contains("#"))
                {
                    values = data.Split('#')[1].Split('-');
                    int id = TextUtils.ToInt(values[0]);
                    int approved = TextUtils.ToInt(values[2]);
                    if (approved == 1)
                    {
                        MessageBox.Show($"Bạn không thể sửa đăng ký đã duyệt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    frmMeetingRoomDetail frm = new frmMeetingRoomDetail();
                    frm.ID = id;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
            if (!string.IsNullOrEmpty(data2) && isFocus2)
            {
                if (data2.Contains("#"))
                {
                    values = data2.Split('#')[1].Split('-');
                    int id = TextUtils.ToInt(values[0]);
                    int approved = TextUtils.ToInt(values[2]);

                    if (approved == 1)
                    {
                        MessageBox.Show($"Bạn không thể sửa đăng ký đã duyệt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    frmMeetingRoomDetail frm = new frmMeetingRoomDetail();
                    frm.ID = id;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }

            if (!string.IsNullOrEmpty(data3) && isFocus3)
            {
                if (data3.Contains("#"))
                {
                    values = data3.Split('#')[1].Split('-');
                    int id = TextUtils.ToInt(values[0]);
                    int approved = TextUtils.ToInt(values[2]);

                    if (approved == 1)
                    {
                        MessageBox.Show($"Bạn không thể sửa đăng ký đã duyệt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    frmMeetingRoomDetail frm = new frmMeetingRoomDetail();
                    frm.ID = id;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void grvRoomOne_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //string data = TextUtils.ToString(grvRoomOne.GetFocusedValue());
            //string data2 = TextUtils.ToString(grvRoomTwo.GetFocusedValue());
            var isFocus1 = grvRoomOne.IsFocusedView;
            var isFocus2 = grvRoomTwo.IsFocusedView;
            var isFocus3 = grvRoomThree.IsFocusedView;

            string[] values = new string[10];
            string fullName = "";
            if (isFocus1)
            {
                string data = TextUtils.ToString(grvRoomOne.GetFocusedValue()).Trim();
                if (!string.IsNullOrEmpty(data))
                {
                    values = data.Split('#')[1].Split('-');
                    fullName = data.Split('\n')[0];
                }
            }
            else if (isFocus2)
            {
                string data = TextUtils.ToString(grvRoomTwo.GetFocusedValue()).Trim();
                if (!string.IsNullOrEmpty(data))
                {
                    values = data.Split('#')[1].Split('-');
                    fullName = data.Split('\n')[0];
                }
            }
            else if (isFocus3)
            {
                string data = TextUtils.ToString(grvRoomThree.GetFocusedValue()).Trim();
                if (!string.IsNullOrEmpty(data))
                {
                    values = data.Split('#')[1].Split('-');
                    fullName = data.Split('\n')[0];
                }
            }
            else
            {
                return;
            }

            int id = TextUtils.ToInt(values[0]);
            int employeeID = TextUtils.ToInt(values[1]);
            int approved = TextUtils.ToInt(values[2]);
            

            if (id <= 0) return;
            if (approved == 1)
            {
                MessageBox.Show($"Bạn không thể xóa đăng ký đã duyệt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var exp1 = new Expression("Code", "N46");
            var exp2 = new Expression("UserID", Global.UserID);
            var list = SQLHelper<vUserGroupLinkModel>.FindByExpression(exp1.And(exp2));

            if (employeeID != Global.EmployeeID && !Global.IsAdmin && list.Count <= 0)
            {
                MessageBox.Show($"Bạn không thể xóa đăng ký của người khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string date = TextUtils.ToDate5(grvRoomOne.GetFocusedRowCellValue(colDate)).ToString("dd/MM/yyyy");
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa đăng ký ngày [{date}] Phòng họp 1 của [{fullName}]?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var model = SQLHelper<BookingRoomModel>.FindByID(id);
                UpdateBookingRoomLog(model, true);

                BookingRoomBO.Instance.Delete(id);
                LoadData();
            }

            //if (!string.IsNullOrEmpty(data) && isFocus1)
            //{

            //    if (data.Contains("#"))
            //    {

            //        values = data.Split('#')[1].Split('-');

            //        int id = TextUtils.ToInt(values[0]);
            //        int employeeID = TextUtils.ToInt(values[1]);
            //        int approved = TextUtils.ToInt(values[2]);

            //        if (approved == 1)
            //        {
            //            MessageBox.Show($"Bạn không thể xóa đăng ký đã duyệt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }

            //        if (employeeID == Global.EmployeeID || Global.IsAdmin)
            //        {
            //            string date = TextUtils.ToDate5(grvRoomOne.GetFocusedRowCellValue(colDate)).ToString("dd/MM/yyyy");
            //            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa đăng ký ngày [{date}] của Phòng họp 1?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //            if (result == DialogResult.Yes)
            //            {
            //                BookingRoomBO.Instance.Delete(id);
            //                LoadData();
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show($"Bạn không có quyền xóa đăng ký này!!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }

            //    }
            //}

            //if (!string.IsNullOrEmpty(data2) && isFocus2)
            //{
            //    if (data2.Contains("#"))
            //    {
            //        values = data2.Split('#')[1].Split('-');
            //        int id = TextUtils.ToInt(values[0]);
            //        int employeeID = TextUtils.ToInt(values[1]);
            //        int approved = TextUtils.ToInt(values[2]);

            //        if (approved == 1)
            //        {
            //            MessageBox.Show($"Bạn không thể xóa đăng ký đã duyệt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }
            //        if (employeeID == Global.EmployeeID || Global.IsAdmin)
            //        {
            //            string date = TextUtils.ToString(grvRoomOne.GetFocusedRowCellValue(colDate));
            //            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa đăng ký ngày [{date}] của Phòng họp 2?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //            if (result == DialogResult.Yes)
            //            {
            //                BookingRoomBO.Instance.Delete(id);
            //                LoadData();
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show($"Bạn không có quyền xóa đăng ký này!!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }
            //    }
            //}
        }



        private void grvRoomTwo_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.VisibleIndex > 0)
            {
                setColorCell(sender as GridView, e);

                //GridView view = sender as GridView;
                //string cellValue = TextUtils.ToString(view.GetRowCellValue(e.RowHandle, e.Column));

                //if (!string.IsNullOrEmpty(cellValue.Trim()) && cellValue.Contains("#"))
                //{
                //    var data = cellValue.Split('#')[1].Split('-');
                //    var eId = TextUtils.ToInt(data[1]);
                //    var approve = TextUtils.ToInt(data[2]);

                //    if (approve == 1)
                //    {
                //        e.Appearance.BackColor = Color.LimeGreen;
                //        e.Appearance.ForeColor = Color.Black;
                //    }
                //    else if (TextUtils.ToInt(eId) == Global.EmployeeID)
                //    {
                //        e.Appearance.BackColor = Color.LightYellow;
                //        e.Appearance.ForeColor = Color.Black;
                //    }
                //    else
                //    {
                //        e.Appearance.BackColor = Color.LightGray; // Set your desired background color
                //        e.Appearance.ForeColor = Color.Black;  // Set your desired font color
                //    }
                //}
            }
        }


        private void grvRoomTwo_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            grvRoomOne.OptionsPrint.AutoWidth = false;
            grvRoomTwo.OptionsPrint.AutoWidth = false;
            grvRoomThree.OptionsPrint.AutoWidth = false;
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = $"TheoDoiPhongHop_{dtpStartDate.Value.ToString("ddMMyy")}_{dtpEndDate.Value.ToString("ddMMyy")}.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    printableComponentLink1.Component = grdRoomOne;

                    PrintableComponentLink printableComponentLink2 = new PrintableComponentLink(printingSystem);
                    printableComponentLink2.Component = grdRoomTwo;

                    PrintableComponentLink printableComponentLink3 = new PrintableComponentLink(printingSystem);
                    printableComponentLink3.Component = grdRoomThree;


                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.AddRange(new object[] { printableComponentLink1, printableComponentLink2, printableComponentLink3 });

                    compositeLink.BreakSpace = 100;

                    compositeLink.ExportToXlsx(saveFileDialog.FileName);
                    Process.Start(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void ExportGridToWorksheet(GridControl gridControl, IXLWorksheet worksheet, int startRow)
        {
            BandedGridView gridView = (BandedGridView)gridControl.DefaultView;

            foreach (GridBand band in gridView.Bands)
            {
                var headerCell = worksheet.Cell(startRow, 1);
                headerCell.Value = band.Caption;
                worksheet.Range(startRow, 1, startRow, gridView.VisibleColumns.Count + 1).Merge();
                headerCell.Style.Font.Bold = true;

                startRow++;

                foreach (BandedGridColumn column in band.Columns)
                {
                    var columnCell = worksheet.Cell(startRow, column.VisibleIndex + 1);
                    columnCell.Value = column.Caption;
                }
                startRow++;

                for (int row = 0; row < gridView.RowCount; row++)
                {
                    int currentColumnIndex = 1;
                    for (int col = 0; col < gridView.VisibleColumns.Count; col++)
                    {
                        var cell = worksheet.Cell(startRow + row, currentColumnIndex);
                        cell.Value = gridView.GetRowCellDisplayText(row, gridView.VisibleColumns[col]);

                        if (col >= 2)
                        {
                            var cellValue = gridView.GetRowCellDisplayText(row, gridView.VisibleColumns[col]);
                            var compareCell = worksheet.Cell(startRow + row, currentColumnIndex - 1);

                            if (cellValue == gridView.GetRowCellDisplayText(row, gridView.VisibleColumns[col - 1]) && !string.IsNullOrEmpty(cellValue))
                            {
                                cell.Style.Fill.BackgroundColor = XLColor.LightYellow;
                                compareCell.Style.Fill.BackgroundColor = XLColor.LightYellow;
                            }
                        }

                        currentColumnIndex++;
                    }
                }
                startRow += gridView.RowCount;
            }

            // Tự động điều chỉnh kích thước của cột sau khi đã điền dữ liệu
        }


        private void grvRoomOne_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText.Contains("#"))
            {
                e.DisplayText = e.DisplayText.Split('#')[0];
            }
        }

        private void grvRoomTwo_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText.Contains("#"))
            {
                e.DisplayText = e.DisplayText.Split('#')[0];
            }
        }

        private void grvRoomThree_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText.Contains("#"))
            {
                e.DisplayText = e.DisplayText.Split('#')[0];
            }
        }

        private void Approved(int isApproved)
        {
            string approve = isApproved == 1 ? "duyệt" : "hủy duyệt";
            string name = "";
            string[] values = new string[10];

            var isFocus1 = grvRoomOne.IsFocusedView;
            var isFocus2 = grvRoomTwo.IsFocusedView;
            var isFocus3 = grvRoomThree.IsFocusedView;

            if (isFocus1)
            {
                string data = TextUtils.ToString(grvRoomOne.GetFocusedValue());
                if (data.Contains("#"))
                {
                    values = data.Split('#')[1].Split('-');
                    name = TextUtils.ToString(grvRoomOne.GetFocusedDisplayText());
                }
                else
                {
                    return;
                }
            }
            else if (isFocus2)
            {
                string data = TextUtils.ToString(grvRoomTwo.GetFocusedValue());
                if (data.Contains("#"))
                {
                    values = data.Split('#')[1].Split('-');
                    name = TextUtils.ToString(grvRoomTwo.GetFocusedDisplayText());
                }
                else
                {
                    return;
                }
            }
            else if (isFocus3)
            {
                string data = TextUtils.ToString(grvRoomThree.GetFocusedValue());
                if (data.Contains("#"))
                {
                    values = data.Split('#')[1].Split('-');
                    name = TextUtils.ToString(grvRoomThree.GetFocusedDisplayText());
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show($"Vui lòng chọn đăng ký muốn {approve}");
                return;
            }

            int id = TextUtils.ToInt(values[0]);
            BookingRoomModel model = (BookingRoomModel)BookingRoomBO.Instance.FindByPK(id);
            string date = model.DateRegister.Value.ToString("dd/MM/yyyy");
            string start = model.StartTime.Value.ToString("HH:mm");
            string end = model.EndTime.Value.ToString("HH:mm");

            if (isApproved == 1)
            {
                if (model.IsApproved == 1)
                {
                    MessageBox.Show($"Đăng ký ngày {date}\ntừ {start} đến {end}\nnhân viên [{name}] đã được duyệt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (model.IsApproved == 0)
                {
                    DialogResult result = MessageBox.Show($"Bạn có chắc muốn duyệt đăng ký ngày {date}\ntừ {start} đến {end}\nnhân viên [{name}]?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        model.IsApproved = 1;
                        BookingRoomBO.Instance.Update(model);
                        LoadData();
                    }
                }
            }
            else if (isApproved == 0)
            {
                if (model.IsApproved == 0)
                {
                    MessageBox.Show($"Đăng ký ngày {date}\ntừ {start} đến {end}\nnhân viên [{name}] chưa được duyệt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (model.IsApproved == 1)
                {
                    DialogResult result = MessageBox.Show($"Bạn có chắc muốn duyệt đăng ký ngày {date}\ntừ {start} đến {end}\nnhân viên [{name}]?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        UpdateBookingRoomLog(model);//ndNhat-25/02/2025

                        model.IsApproved = 0;
                        BookingRoomBO.Instance.Update(model);
                        LoadData();
                    }
                }
            }

        }

        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            Approved(1);
        }

        private void btnCancelApprove_Click(object sender, EventArgs e)
        {
            Approved(0);
        }

        void setColorCell(GridView view, RowCellStyleEventArgs e)
        {
            //GridView view = sender as GridView;
            string cellValue = TextUtils.ToString(view.GetRowCellValue(e.RowHandle, e.Column));

            if (!string.IsNullOrEmpty(cellValue.Trim()) && cellValue.Contains("#"))
            {
                var data = cellValue.Split('#')[1].Split('-');
                var id = TextUtils.ToInt(data[0]);
                var eId = TextUtils.ToInt(data[1]);
                var approve = TextUtils.ToInt(data[2]);

                if (approve == 1)
                {
                    e.Appearance.BackColor = Color.LimeGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
                else if (TextUtils.ToInt(eId) == Global.EmployeeID)
                {
                    e.Appearance.BackColor = Color.LightYellow;
                    e.Appearance.ForeColor = Color.Black;
                }
                else
                {
                    e.Appearance.BackColor = Color.LightGray; // Set your desired background color
                    e.Appearance.ForeColor = Color.Black;  // Set your desired font color
                }

            }
        }

        private void grdRoomTwo_Click(object sender, EventArgs e)
        {

        }

        private void grvRoomThree_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.VisibleIndex > 0)
            {
                setColorCell(sender as GridView, e);
            }
        }

        private void UpdateBookingRoomLog(BookingRoomModel updatedModel, bool isDeleted = false)
        {

            // Lấy dữ liệu cũ từ database
            var originalModel = SQLHelper<BookingRoomModel>.FindByID(updatedModel.ID);
            StringBuilder logContent = new StringBuilder();
            string oldEmpCode = SQLHelper<EmployeeModel>.FindByID(originalModel.EmployeeID).FullName;

            logContent.Append($"Nhân viên {Global.AppFullName} ");
            if (isDeleted)
            {
                logContent.Append($"đã xóa lịch đặt phòng họp của {oldEmpCode} " +
                    $"\ntừ {updatedModel.StartTime.Value.ToShortTimeString()} đến {updatedModel.EndTime.Value.ToShortTimeString()}\n " +
                    $"vào ngày {updatedModel.DateRegister.Value.ToShortDateString()}\n");
                BookingRoomLogModel logModel = new BookingRoomLogModel
                {
                    ContentLog = logContent.ToString(),
                    CreatedBy = Global.AppUserName,
                    CreatedDate = DateTime.Now,
                    DateLog = DateTime.Now,
                    UpdatedBy = Global.AppUserName,
                    UpdatedDate = DateTime.Now
                };
                SQLHelper<BookingRoomLogModel>.Insert(logModel);
            }
            else
            {
                if (originalModel != null)
                {
                    logContent.Append($"cập nhật lịch đặt phòng họp của {oldEmpCode} {Environment.NewLine}");
                    if (originalModel.IsApproved != updatedModel.IsApproved)
                    {
                        string oldStatus = originalModel.IsApproved == 1 ? "Đã duyệt" : "Chưa duyệt";
                        string newStatus = updatedModel.IsApproved == 1 ? "Đã duyệt" : "Chưa duyệt";
                        logContent.Append($"trạng thái từ {oldStatus} thành {newStatus},{Environment.NewLine}");
                    }
                    if (logContent.Length > 0)
                    {
                        logContent.Length -= 2;

                        BookingRoomLogModel logModel = new BookingRoomLogModel
                        {
                            ContentLog = logContent.ToString(),
                            CreatedBy = Global.AppUserName,
                            CreatedDate = DateTime.Now,
                            DateLog = DateTime.Now,
                            UpdatedBy = Global.AppUserName,
                            UpdatedDate = DateTime.Now
                        };
                        SQLHelper<BookingRoomLogModel>.Insert(logModel);
                    }
                }
            }
        }
    }
}