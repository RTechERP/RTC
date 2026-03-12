using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmEmployeeAttendance : _Forms
    {
        public frmEmployeeAttendance()
        {
            InitializeComponent();
        }
        private void frmEmployeeAttendance_Load(object sender, EventArgs e)
        {


            DateTime firstDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dtpDateStart.Value = firstDate;
            dtpDateEnd.Value = firstDate.AddMonths(+1).AddDays(-1);

            loadCboDepartment();
            loadCboEmployee();
            loadData();
        }

        private void loadData()
        {
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //grvData.BeginDataUpdate();
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);

            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);

            using (WaitDialogForm fWait = new WaitDialogForm("Đang tải dữ liệu", "Vui lòng đợi..."))
            {
                DataTable dt = TextUtils.GetDataTableFromSP("spGetEmployeeAttendance",
                                                new string[] { "@DepartmentID", "@EmployeeID", "@FindText", "@DateStart", "@DateEnd" },
                                                new object[] { departmentID, employeeID, txtSearch.Text, dateStart, dateEnd });
                grdData.DataSource = dt;
            }
            //grvData.EndDataUpdate();
            //grvData.Invalidate();
            //stopwatch.Stop();
            //MessageBox.Show(stopwatch.ElapsedMilliseconds.ToString());
        }

        void loadCboDepartment()
        {
            List<DepartmentModel> listDepartment = SQLHelper<DepartmentModel>.SqlToList("SELECT ID, Name FROM dbo.Department");

            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = listDepartment;
        }

        void loadCboEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //MyLib.ShowWaitForm();
            //Thread.Sleep(10000);
            //MyLib.CloseWaitForm();


            loadData();
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmInputExcelEmployeeAttendance frm = new frmInputExcelEmployeeAttendance();
            frm.dtpDateStart.Value = dtpDateStart.Value;
            frm.dtpDateEnd.Value = dtpDateEnd.Value;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            if (e.RowHandle < 0 ||
                (e.Column != colCheckIn &&
                e.Column != colCheckOut &&
                e.Column != colIsLateRegister &&
                e.Column != colIsEarlyRegister))
                return;

            bool IsOnleave = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colOnLeave));
            bool IsBussiness = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colBussiness));
            bool IsNoFinger = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colNoFingerprint));
            bool IsWfh = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colWFH));

            if (!IsOnleave && !IsBussiness && !IsNoFinger && !IsWfh)
            {
                if (e.Column == colCheckIn || e.Column == colCheckOut)
                {
                    int holidayDay = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colHolidayDay));
                    bool IsOverLate = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsOverLate));
                    bool IsOverEarly = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsOverEarly));
                    if (e.Column == colCheckIn)
                    {
                        bool isLate = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsLate));

                        if (IsOverLate && holidayDay == 0)
                        {
                            e.Appearance.BackColor = Color.Yellow;
                            e.Appearance.ForeColor = Color.Black;
                        }
                        else
                        {
                            if (isLate && holidayDay == 0)
                            {
                                e.Appearance.BackColor = Color.Red;
                                e.Appearance.ForeColor = Color.White;
                            }
                        }
                    }
                    else
                    {
                        bool isEarly = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsEarly));

                        if (IsOverEarly && holidayDay == 0)
                        {
                            e.Appearance.BackColor = Color.Yellow;
                            e.Appearance.ForeColor = Color.Black;
                        }
                        else
                        {
                            if (isEarly && holidayDay == 0)
                            {
                                e.Appearance.BackColor = Color.Red;
                                e.Appearance.ForeColor = Color.White;
                            }
                        }
                    }
                }
            }

            if (e.Column == colIsLateRegister)
            {
                int type = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colTypeLate));
                if (type == 4)
                {
                    e.Appearance.BackColor = Color.LimeGreen;
                }
            }
            else if (e.Column == colIsEarlyRegister)
            {
                int type = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colTypeEarly));
                if (type == 3)
                {
                    e.Appearance.BackColor = Color.LimeGreen;
                }
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

        private void btnExcel_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"DanhSachVanTay_{dtpDateStart.Value.ToString("ddMMyyyy")}_{dtpDateEnd.Value.ToString("ddMMyyyy")}.xlsx");

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();
                //printingSystem.ExportOptions.Xlsx.TextExportMode = TextExportMode.Value;

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                grvData.OptionsPrint.AutoWidth = false;
                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    //compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;
                    //optionsEx.CustomizeCell += OptionsEx_CustomizeCell;

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

        private void grvData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {

            //grvData.SortedColumns[0].

            GridView view = sender as GridView;
            if (view == null) return;
            try
            {
                if (e.Column != colDepartmentName)
                {
                    return;
                }
                if (e.Column.FieldName == "DepartmentName")
                {
                    int val1 = TextUtils.ToInt(view.GetListSourceRowCellValue(e.ListSourceRowIndex1, "DepartmentSTT"));
                    int val2 = TextUtils.ToInt(view.GetListSourceRowCellValue(e.ListSourceRowIndex2, "DepartmentSTT"));

                    e.Handled = true;
                    e.Result = System.Collections.Comparer.Default.Compare(val1, val2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void grvData_CustomDrawGroupRowCell(object sender, DevExpress.XtraGrid.Views.Base.RowGroupRowCellEventArgs e)
        {
            //e.Cache.DrawRectangle(e.Bounds.X,e.Bounds.Y,e.Bounds.Width,e.Bounds.Height, Color.FromArgb(1, 240, 240, 240),0);  
            //e.Handled = true;
            ////var r = e.Bounds;
            //r.Inflate(0, 0);
            //e.Cache.DrawRectangle(e.Cache.GetPen(Color.FromArgb(1, 201, 210, 210)), r);
            //e.Handled = true;
        }

        private void grvData_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            //GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            //GridView view = sender as GridView;
            //view.OptionsView.AllowHtmlDrawGroups = true;
            //e.Appearance.BackColor = Color.FromArgb(1, 240, 240, 240);
            //e.Cache.DrawRectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height, Color.FromArgb(1, 240, 240, 240), 1);
            ////info.GroupText = info.Column.Caption + ": <color=Black>" + info.GroupValueText + "</color> ";
            ////info.GroupText += "<color=LightSteelBlue>" + view.GetGroupSummaryText(e.RowHandle) + "</color> ";
            //e.Handled = true;
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column == colIsLateRegister || e.Column == colIsEarlyRegister || e.Column ==colOvertime ||e.Column == colBussiness || e.Column == colNoFingerprint || e.Column == colOnLeave || e.Column == colWFH || e.Column == colCurricular || e.Column == colNoFingerprintReal)
            //{
            //    e.DisplayText = TextUtils.ToBoolean(e.Value) ? "x" : "";
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int[] rowSelecteds = grvData.GetSelectedRows();
                if (rowSelecteds.Length <= 0)
                {
                    MessageBox.Show("Vui lòng chọn vân tay muốn xóa!", "Thông báo");
                    return;
                }

                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xóa danh sách vân tay đã chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.No) return;

                using (WaitDialogForm fWait = new WaitDialogForm("Đang xóa dữ liệu", "Vui lòng đợi..."))
                {
                    foreach (int row in rowSelecteds)
                    {
                        int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                        if (id <= 0) continue;
                        //if (idDeletes.Contains(id)) continue;
                        //idDeletes.Add(id);

                        SQLHelper<EmployeeAttendanceModel>.DeleteModelByID(id);
                        //grvData.DeleteRow(row);
                    }
                    loadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
    }
}