using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmSummaryKPIErrorEmployee : _Forms
    {
        public int departmentID = 0;
        public string deName;
        public frmSummaryKPIErrorEmployee()
        {
            InitializeComponent();
        }

        private void frmSummaryKPIErrorEmployee_Load(object sender, EventArgs e)
        {
            this.Text += " - " + deName;
            txtYear.Value = txtYearChart.Value = txtYearError.Value = DateTime.Now.Year;
            txtMonth.Value = txtMonthChart.Value = txtMonthError.Value = DateTime.Now.Month;
            LoadDepartment();

            LoadKPIErrorType();
            LoadKPIError();
            LoadEmployee();
            //LoadData();

            LoadAllData_TH();
        }
        #region LOAD DATA
        void LoadData()
        {

            //Load tab tổng hợp lỗi vi phạm
            LoadAllData_TH();
            //Load tab thống kê lỗi vi phạm
            LoadData_TK();
            //Load tab biểu đồ lỗi vi phạm
            LoadData_BD();
        }
        //LinhTN update 13/11/2024 - add
        void LoadAllData_TH()
        {
            LoadData_TH();
            LoadDataFile();

        }
        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            //grdDataEmployee.DataSource = null;
            LoadData_TK();
        }
        private void txtMonth_ValueChanged(object sender, EventArgs e)
        {
            colMonth.Caption = $"Tháng {txtMonth.Value}";
            //grdDataEmployee.DataSource = null;
            LoadData_TK();
        }
        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        //LinhTN update 13/11/2024 - add
        void LoadKPIErrorType()
        {
            var dt = SQLHelper<KPIErrorTypeModel>.FindByAttribute("IsDelete", 0);
            cboKPIErrorType_TH.Properties.DisplayMember = "Code";
            cboKPIErrorType_TH.Properties.ValueMember = "ID";
            cboKPIErrorType_TH.Properties.DataSource = dt;

            cboKPIErrorType_TK.Properties.DisplayMember = "Code";
            cboKPIErrorType_TK.Properties.ValueMember = "ID";
            cboKPIErrorType_TK.Properties.DataSource = dt;

            cboKPIErrorType_BD.Properties.DisplayMember = "Code";
            cboKPIErrorType_BD.Properties.ValueMember = "ID";
            cboKPIErrorType_BD.Properties.DataSource = dt;
        }
        void LoadKPIError()
        {
            //LinhTN update 13/11/2024 - add @TypeID
            DataTable dt = TextUtils.LoadDataFromSP("spGetKPIError", "A", new string[] { "@TypeID" }, new object[] { TextUtils.ToInt(cboKPIErrorType_TH.EditValue) });
            cboKPIError.Properties.DisplayMember = "Code";
            cboKPIError.Properties.ValueMember = "ID";
            cboKPIError.Properties.DataSource = dt;
        }
        void LoadEmployee()
        {
            //DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeForKPIError", "A", new string[] { }, new object[] { });
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DataSource = dt;
        }
        void LoadDepartment()
        {
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();

            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = list;

            cboDepartment_TK.Properties.ValueMember = "ID";
            cboDepartment_TK.Properties.DisplayMember = "Name";
            cboDepartment_TK.Properties.DataSource = list;

            cboDepartment_BD.Properties.ValueMember = "ID";
            cboDepartment_BD.Properties.DisplayMember = "Name";
            cboDepartment_BD.Properties.DataSource = list;

            cboDepartment.EditValue = departmentID;
            cboDepartment_TK.EditValue = departmentID;
            cboDepartment_BD.EditValue = departmentID;
        }

        #endregion
        #region TỔNG HỢP LỖI CỦA NHÂN VIÊN
        void LoadData_TH()
        {
            int year = (int)txtYearError.Value;
            int month = (int)txtMonthError.Value;
            int kpiErrorID = TextUtils.ToInt(cboKPIError.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            //int typeID = TextUtils.ToInt(cboKPIErrorType_TH.EditValue);
            departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            //LinhTN update 13/11/2024 - thêm @TypeID
            DataSet ds1 = TextUtils.LoadDataSetFromSP("spGetSummaryKPIErrorEmployee",
                                        new string[] { "@Month", "@Year", "@KPIErrorID", "@EmployeeID", "@Keyword", "@TypeID", "@DepartmentID" },
                                        new object[] { month, year, kpiErrorID, employeeID, txtKeyword.Text.Trim(), 1, departmentID });
            grdData.DataSource = ds1.Tables[0];


            DataSet ds2 = TextUtils.LoadDataSetFromSP("spGetSummaryKPIErrorEmployee",
                                       new string[] { "@Month", "@Year", "@KPIErrorID", "@EmployeeID", "@Keyword", "@TypeID", "@DepartmentID" },
                                       new object[] { month, year, kpiErrorID, employeeID, txtKeyword.Text.Trim(), 3, departmentID });
            gridControl1.DataSource = ds2.Tables[0];

            DataSet ds3 = TextUtils.LoadDataSetFromSP("spGetSummaryKPIErrorEmployee",
                                       new string[] { "@Month", "@Year", "@KPIErrorID", "@EmployeeID", "@Keyword", "@TypeID", "@DepartmentID" },
                                       new object[] { month, year, kpiErrorID, employeeID, txtKeyword.Text.Trim(), 9, departmentID });
            gridControl2.DataSource = ds3.Tables[0];
        }

        private void cboKPIError_EditValueChanged(object sender, EventArgs e)
        {
            //LoadData_TH();
            //LoadDataFile();
            LoadAllData_TH();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            //LoadData_TH();
            //LoadDataFile();
            LoadAllData_TH();
        }
        //LinhTN update 13/11/2024 - add
        private void cboKPIErrorType_TH_EditValueChanged(object sender, EventArgs e)
        {
            LoadKPIError();
            LoadAllData_TH();
        }
        private void txtMonthError_ValueChanged(object sender, EventArgs e)
        {
            //LoadData_TH();
            //LoadDataFile();
            LoadAllData_TH();
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            //LoadData_TH();
            //LoadDataFile();
            LoadAllData_TH();
        }


        void LoadDataFile()
        {
            //LinhTN update 13/11/2024 - add @TypeID, update lại txtMonth -> txtMonthError và txtYear -> txtYearError
            departmentID = TextUtils.ToInt(cboDepartment.EditValue);

            DataSet ds = TextUtils.LoadDataSetFromSP("spGetSummaryKPIErrorEmployee",
                new string[] { "@Month", "@Year", "@KPIErrorID", "@EmployeeID", "@Keyword", "@TypeID", "@DepartmentID" },
                new object[] { txtMonthError.Value, txtYearError.Value, TextUtils.ToInt(cboKPIError.EditValue), TextUtils.ToInt(cboEmployee.EditValue), txtKeyword.Text.Trim(), TextUtils.ToInt(cboKPIErrorType_TH.EditValue), departmentID });
            grdDataFile.DataSource = ds.Tables[1];
            //LoadImage();
        }
        private void btnShowImage_Click(object sender, EventArgs e)
        {
            List<KPIErrorEmployeeFileModel> files = new List<KPIErrorEmployeeFileModel>();
            int[] rowSelecteds = grvDataFile.GetSelectedRows();
            if (rowSelecteds.Length > 0)
            {
                foreach (int rowIndex in rowSelecteds)
                {
                    int fileID = TextUtils.ToInt(grvDataFile.GetRowCellValue(rowIndex, colFileID));
                    if (fileID != 0)
                    {
                        KPIErrorEmployeeFileModel file = SQLHelper<KPIErrorEmployeeFileModel>.FindByID(fileID);
                        files.Add(file);
                    }
                }
            }
            else
            {
                MessageBox.Show("Chọn file ảnh cần xem!", "Thông báo");
                return;
            }

            frmKPIErrorEmployeeDetailImages frm = new frmKPIErrorEmployeeDetailImages();
            frm.files = files;
            frm.Text = $"DANH SÁCH ẢNH CÁC LỖI VI PHẠM KPI - THÁNG {txtMonth.Value} NĂM {txtYear.Value}";
            frm.Show();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    LoadData_TH();
            //    LoadDataFile();
            //}
        }
        //void LoadImage()
        //{
        //    try
        //    {
        //        int ID = TextUtils.ToInt(grvDataFile.GetFocusedRowCellValue(colFileID));
        //        if (ID == 0)
        //        {
        //            pictureBox.Image = null;
        //            return;
        //        }
        //        KPIErrorEmployeeFileModel model = SQLHelper<KPIErrorEmployeeFileModel>.FindByID(ID);
        //        //string url = $"http://113.190.234.64:8083/api/kpi/{item.ServerPath}/{item.FileName}";
        //        string url = $"{model.OriginPath}\\{model.FileName}";
        //        var request = WebRequest.Create(url);
        //        var response = request.GetResponse();
        //        var stream = response.GetResponseStream();

        //        pictureBox.Image = Image.FromStream(stream);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Thông báo");
        //    }
        //}

        void LoadImage()
        {
            try
            {
                int ID = TextUtils.ToInt(grvDataFile.GetFocusedRowCellValue(colFileID));
                if (ID == 0)
                {
                    pictureBox.Image = null;
                    return;
                }
                KPIErrorEmployeeFileModel model = SQLHelper<KPIErrorEmployeeFileModel>.FindByID(ID);
                //DateTime? errorDate = TextUtils.ToDate4(grvDataFile.GetFocusedRowCellValue(colErrorDateText));
                DateTime? errorDate = TextUtils.ToDate4(grvDataFile.GetFocusedRowCellValue(colErrorDateFile));
                if (!errorDate.HasValue) return;

                string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colFileName));


                //string dateFolder = !errorDate.HasValue ? "" : $"{errorDate:dd.MM.yyyy}";
                //string pathPatern = $@"/AnhViPham/{dateFolder}";
                string pathPattern = $@"{errorDate.Value.Year}\T{errorDate.Value.Month}\N{errorDate.Value.ToString("dd.MM.yyyy")}"; //LinhTN update 04/11/2024
                string url = $"http://113.190.234.64:8083/api/kpi/{pathPattern}/{fileName}";
                //string url = $"{model.OriginPath}\\{model.FileName}";
                var request = WebRequest.Create(url);
                var response = request.GetResponse();
                var stream = response.GetResponseStream();

                pictureBox.Image = Image.FromStream(stream);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Thông báo");
                pictureBox.Image = null;
            }
        }

        private void grvDataFile_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadImage();
        }

        private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData_TH();
                LoadDataFile();
            }
        }
        private void btnExportExcel_Click(object sender, EventArgs e)
        {

            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"DanhSachTongHopLoiCuaNhanVien_Thang{txtMonth.Value}Nam{txtYear.Value}.xlsx";
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
        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            string codeNoColor = "L1.2"; //Báo cáoc cv muộn
            if (e.Column == colTotalError)
            {
                string code = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, "Code"));
                int errorNumber = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colTotalError));
                if (errorNumber >= 2 && code != codeNoColor)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 74);
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            if (e.Column == colCoefficient)
            {
                int coefficient = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colCoefficient));
                if (coefficient >= 2)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 165, 0);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }
        #endregion
        #region THỐNG KÊ LỖI VI PHẠM
        void LoadData_TK()
        {
            int month = (int)txtMonth.Value;
            int year = (int)txtYear.Value;
            int typeID = TextUtils.ToInt(cboKPIErrorType_TK.EditValue);
            departmentID = TextUtils.ToInt(cboDepartment_TK.EditValue);

            //LinhTN update 13/11/2024 - add @TypeID
            DataTable dt = TextUtils.LoadDataFromSP("spGetSummaryKPIError", "A",
                new string[] { "@Month", "@Year", "@Keyword", "@TypeID", "@DepartmentID" },
                new object[] { month, year, txtKeyword_TK.Text.Trim(), typeID, departmentID });
            grdData_TK.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                colWeek1.Caption = $"Tuần 1 {dt.Rows[0]["WeekText1"]}";
                colWeek2.Caption = $"Tuần 2 {dt.Rows[0]["WeekText2"]}";
                colWeek3.Caption = $"Tuần 3 {dt.Rows[0]["WeekText3"]}";
                colWeek4.Caption = $"Tuần 4 {dt.Rows[0]["WeekText4"]}";
                colWeek5.Caption = $"Tuần 5 {dt.Rows[0]["WeekText5"]}";
                colWeek6.Caption = $"Tuần 6 {dt.Rows[0]["WeekText6"]}";
            }

        }

        //LinhTN update 13/11/2024 - add
        private void cboKPIErrorType_TK_EditValueChanged(object sender, EventArgs e)
        {
            LoadData_TK();
        }
        private void btnFind_TK_Click(object sender, EventArgs e)
        {
            LoadData_TK();
        }

        private void txtKeyword_TK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadData_TK();
        }
        private void grvData_TK_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridColumn[] weekColumns = { colWeek1, colWeek2, colWeek3, colWeek4, colWeek5, colWeek6, colMonth };

            if (!weekColumns.Contains(e.Column)) return;

            int kpiErrorTypeID = TextUtils.ToInt(grvData_TK.GetRowCellValue(e.RowHandle, colKPIErrorTypeID));

            if (kpiErrorTypeID > 1) return;

            int cellValue = TextUtils.ToInt(grvData_TK.GetRowCellValue(e.RowHandle, e.Column));

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
            int month = (int)txtMonthChart.Value;
            int year = (int)txtYearChart.Value;
            int typeID = TextUtils.ToInt(cboKPIErrorType_BD.EditValue);
            departmentID = TextUtils.ToInt(cboDepartment_BD.EditValue);

            //LinhTN update 13/11/2024 - add @TypeID
            DataTable dt = TextUtils.LoadDataFromSP("spGetSummaryKPIError", "A",
                                                    new string[] { "@Month", "@Year", "@Keyword", "@TypeID", "@DepartmentID" },
                                                    new object[] { month, year, "", typeID, departmentID });
            chartKPIErrorInMonth.DataSource = dt;

            chartKPIErrorInMonth.Titles.Clear();
            chartKPIErrorInMonth.Titles.Add(new ChartTitle
            {
                Text = $"THỐNG KÊ LỖI VI PHẠM THÁNG {txtMonthChart.Value} NĂM {txtYearChart.Value}", //LinhTN update 13/11/2024
                Font = new Font("Tahoma", 14, FontStyle.Bold),
                TextColor = Color.Orange
            });
        }
        private void chartKPIErrorInMonth_MouseClick(object sender, MouseEventArgs e)
        {
            int kpiErrorID = 0;
            int weekIndex = 0;
            ChartHitInfo hit = chartKPIErrorInMonth.CalcHitInfo(e.X, e.Y);

            SeriesPoint seriesPoint = hit.SeriesPoint;
            if (seriesPoint != null)
            {

                DataRowView dataRowView = (DataRowView)seriesPoint.Tag;
                DataRow dataRow = dataRowView.Row;
                kpiErrorID = TextUtils.ToInt(dataRow["ID"]);
            }

            Series series = (Series)hit.Series;
            if (series != null)
            {
                weekIndex = TextUtils.ToInt(series.Tag);
            }

            //LinhTN update 13/11/2024 - add
            KPIErrorModel kpiError = SQLHelper<KPIErrorModel>.FindByID(kpiErrorID);
            //lblGroupWeek.Text = $"Tuần: {weekIndex} - {kpiError.Content}";
            string content = string.IsNullOrWhiteSpace(kpiError.Content) ? "" : $" - {kpiError.Code}: {kpiError.Content}";
            label23.Text = $"Tuần: {weekIndex}{content}";
            grvDataEmployee.OptionsView.AllowCellMerge = false;
            grvDataEmployee.CellMerge -= new CellMergeEventHandler(grvDataEmployee_CellMerge);
            //

            int deparmentID = TextUtils.ToInt(cboDepartment_BD.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetSummaryEmployeeKPIError", "A",
                                                new string[] { "@Month", "@Year", "@KPIErrorID", "@WeekIndex", "@DepartmentID" },
                                                new object[] { txtMonthChart.Value, txtYearChart.Value, kpiErrorID, weekIndex, deparmentID });
            grdDataEmployee.DataSource = dt;

            //LinhTN update 13/11/2024 - add
            grvDataEmployee.CellMerge += new CellMergeEventHandler(grvDataEmployee_CellMerge);
            grvDataEmployee.OptionsView.AllowCellMerge = true;
            //
        }
        private void grvDataEmployee_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }
        private void btnExportExcel_TK_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"DanhSachThongKeLoiViPham_Thang{txtMonth.Value}Nam{txtYear.Value}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"DanhSachThongKeLoiViPham_Thang{txtMonth.Value}Nam{txtYear.Value}.xlsx");
                string filepath = f.FileName;

                XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();
                grvData_TK.OptionsPrint.AutoWidth = false;
                grvData_TK.OptionsPrint.ExpandAllDetails = false;
                grvData_TK.OptionsPrint.PrintDetails = true;
                grvData_TK.OptionsPrint.UsePrintStyles = true;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;

                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData_TK;

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
        private void btnFindChart_Click(object sender, EventArgs e)
        {
            LoadData_BD();
        }
        //LinhTN update 13/11/2024 - add
        private void cboKPIErrorType_BD_EditValueChanged(object sender, EventArgs e)
        {
            LoadData_BD();
        }
        //LinhTN update 13/11/2024 - add
        private void grvDataEmployee_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column == colTotalError_BD || e.Column == colDayError_BD)
            {
                string value1 = TextUtils.ToString(grvDataEmployee.GetRowCellValue(e.RowHandle1, colTotalError_BD));
                string value2 = TextUtils.ToString(grvDataEmployee.GetRowCellValue(e.RowHandle2, colTotalError_BD));

                string value3 = TextUtils.ToString(grvDataEmployee.GetRowCellValue(e.RowHandle1, colDayError_BD));
                string value4 = TextUtils.ToString(grvDataEmployee.GetRowCellValue(e.RowHandle2, colDayError_BD));
                e.Merge = (value1 == value2 && value3 == value4);

            }

            e.Handled = true;
            return;
            //e.Handled = true;
        }
        //LinhTN update 13/11/2024 - add
        void LoadImageEmployee()
        {
            try
            {
                int ID = TextUtils.ToInt(grvDataEmployee.GetFocusedRowCellValue(colFileImageID));
                if (ID == 0)
                {
                    pictureBoxEmployee.Image = null;
                    return;
                }
                KPIErrorEmployeeFileModel model = SQLHelper<KPIErrorEmployeeFileModel>.FindByID(ID);
                DateTime? errorDate = TextUtils.ToDate4(grvDataEmployee.GetFocusedRowCellValue(colErrorDate_BD));
                if (!errorDate.HasValue) return;

                string fileName = TextUtils.ToString(grvDataEmployee.GetFocusedRowCellValue(colFileImageName));


                //string dateFolder = !errorDate.HasValue ? "" : $"{errorDate:dd.MM.yyyy}";
                //string pathPatern = $@"/AnhViPham/{dateFolder}";
                string pathPattern = $@"{errorDate.Value.Year}\T{errorDate.Value.Month}\N{errorDate.Value.ToString("dd.MM.yyyy")}"; //LinhTN update 04/11/2024
                string url = $"http://113.190.234.64:8083/api/kpi/{pathPattern}/{fileName}";
                //string url = $"{model.OriginPath}\\{model.FileName}";
                var request = WebRequest.Create(url);
                var response = request.GetResponse();
                var stream = response.GetResponseStream();

                pictureBoxEmployee.Image = Image.FromStream(stream);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Thông báo");
                pictureBoxEmployee.Image = null;
            }
        }
        //LinhTN update 13/11/2024 - add
        private void grvDataEmployee_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadImageEmployee();
        }
        //LinhTN update 13/11/2024 - add
        private void btnShowImageEmployee_Click(object sender, EventArgs e)
        {
            List<KPIErrorEmployeeFileModel> files = new List<KPIErrorEmployeeFileModel>();


            int employeeIDSelected = TextUtils.ToInt(grvDataEmployee.GetRowCellValue(grvDataEmployee.FocusedRowHandle, colEmployeeID));
            for (int i = 0; i < grvDataEmployee.RowCount; i++)
            {
                int employeeID = TextUtils.ToInt(grvDataEmployee.GetRowCellValue(i, colEmployeeID));

                if (employeeIDSelected != employeeID) continue;
                int fileID = TextUtils.ToInt(grvDataEmployee.GetRowCellValue(i, colFileImageID));
                if (fileID != 0)
                {
                    KPIErrorEmployeeFileModel file = SQLHelper<KPIErrorEmployeeFileModel>.FindByID(fileID);
                    files.Add(file);
                }
            }

            if (files.Count <= 0) return;

            frmKPIErrorEmployeeDetailImages frm = new frmKPIErrorEmployeeDetailImages();
            frm.files = files;
            frm.Text = $"DANH SÁCH ẢNH CÁC LỖI VI PHẠM KPI - THÁNG {txtMonthChart.Value} NĂM {txtYearChart.Value}";
            frm.Show();
        }

        //LinhTN update 13/11/2024 - add
        private void txtMonthChart_ValueChanged(object sender, EventArgs e)
        {
            LoadData_BD();
            grdDataEmployee.DataSource = null;
        }
        #endregion


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"TongHopViPhamLoi_{DateTime.Now.ToString("ddMMyyyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"TongHopViPhamLoi_{DateTime.Now.ToString("ddMMyyyy")}.xlsx");
                string filepath = f.FileName;
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                PrintableComponentLink printableComponentLink2 = new PrintableComponentLink(printingSystem);
                printableComponentLink2.Component = grdData_TK;

                //PrintableComponentLink printableComponentLink3 = new PrintableComponentLink(printingSystem);
                //printableComponentLink3.Component = grdData_TK;

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);
                        compositeLink.Links.Add(printableComponentLink2);
                        //compositeLink.Links.Add(printableComponentLink3);

                        compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

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

        private void PrintingSystem_XlSheetCreated(object sender, XlSheetCreatedEventArgs e)
        {
            e.SheetName = e.Index == 0 ? $"Công tác" : (e.Index == 1 ? $"Đi làm sớm" : $"");

            if (e.Index == 0)
            {
                e.SheetName = $"LỖI CỦA NHÂN VIÊN";
            }
            else if (e.Index == 1)
            {
                e.SheetName = $"THỐNG KÊ";
            }
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //LoadDataFile();
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