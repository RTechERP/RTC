using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmHangMucCongViecNew : _Forms
    {
        DataSet dataSet = new DataSet();
        List<GridBand> listBand = new List<GridBand>();
        List<BandedGridColumn> listCol = new List<BandedGridColumn>();
        public DateTime createdDate = DateTime.Now;
        public frmHangMucCongViecNew()
        {
            InitializeComponent();
        }

        private void frmHangMucCongViecNew_Load(object sender, System.EventArgs e)
        {

            DateTime dateStart = DateTime.Now.AddDays(-7);
            DateTime dateEnd = DateTime.Now.AddDays(+7);

            dtpDateStart.Value = dateStart;
            dtpDateEnd.Value = dateEnd;

            cboStatus.SelectedIndex = 1;

            LoadEmployee();
            LoadDepartment();
            LoadTeam();
            LoadData();
        }


        private void LoadData()
        {
            //DateTime dateStart = dtpDateStart.Value;
            //DateTime dateEnd = dtpDateEnd.Value;
            //Lib.LockEvents = true;
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);

            int departmentId = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeId = TextUtils.ToInt(cboEmployee.EditValue);
            int teamId = TextUtils.ToInt(cbTeam.EditValue);
            int status = cboStatus.SelectedIndex - 1;

            grvData.OptionsView.AllowCellMerge = false;
            grvData.CellMerge -= new CellMergeEventHandler(grvData_CellMerge_1);
            grdData.DataSource = null;

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu..."))
            {
                if (listCol.Count > 0)
                {
                    foreach (var item in listCol)
                    {
                        grvData.Columns.Remove(item);
                    }

                    listCol.Clear();
                }

                if (listBand.Count > 0)
                {
                    bandNote.Children.Clear();
                    listBand.Clear();
                }

                //Stopwatch stopwatch = new Stopwatch();
                //stopwatch.Start();
                dataSet = TextUtils.LoadDataSetFromSP("sp_GetHangMucCongViec",
                                                   new string[] { "@DateStart", "@DateEnd", "@EmployeeID", "@TeamID", "@DepartmentID", "@Status" },
                                                   new object[] { dateStart, dateEnd, employeeId, teamId, departmentId, status });

                AddBand();

                DataTable data = dataSet.Tables[0];
                grdData.DataSource = data;

                //var summarys = grvData.Columns[colTotalDays.FieldName].Summary;
                //if (summarys.Count > 0)
                //{
                //    grvData.Columns[colTotalDays.FieldName].Summary.Clear();
                //}

                //var sumTotalDay = data.AsEnumerable()
                //  .GroupBy(row => row.Field<string>("TypeText"))
                //  .Select(g => new
                //  {
                //      TypeText = g.Key,
                //      TotalDay = g.Sum(row => row.Field<decimal?>("TotalDay"))
                //  });

                //var itemPlans = sumTotalDay.FirstOrDefault(item => item.TypeText == "Plan"); //.TotalDay;
                //var itemActuals = sumTotalDay.FirstOrDefault(item => item.TypeText == "Actual");//.TotalDay;

                //decimal sumPlan = itemPlans == null ? 0 : TextUtils.ToDecimal(itemPlans.TotalDay);
                //decimal sumActual = itemActuals == null ? 0 : TextUtils.ToDecimal(itemActuals.TotalDay);


                //grvData.Columns[colTotalDays.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colTotalDays.FieldName, $"Plan = {sumPlan}"));
                //grvData.Columns[colTotalDays.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colTotalDays.FieldName, $"Actual = {sumActual}"));

                //DataTable dt = dataSet.Tables[1];

                //string dateS = "";
                //string dateE = "";
                //if (dt.Rows.Count >= 0)
                //{
                //    dateS = string.IsNullOrEmpty(TextUtils.ToString(dt.Rows[0]["AllDates"])) ? "" : TextUtils.ToString(dt.Rows[0]["AllDates"]).Substring(0, 5);
                //    dateE = string.IsNullOrEmpty(TextUtils.ToString(dt.Rows[dt.Rows.Count - 1]["AllDates"])) ? "" : TextUtils.ToString(dt.Rows[dt.Rows.Count - 1]["AllDates"]).Substring(0, 5);
                //}

                //stopwatch.Stop();
                //MessageBox.Show(stopwatch.ElapsedMilliseconds.ToString(), "Get data");

                //Stopwatch stopwatch1 = new Stopwatch();
                //stopwatch1.Start();
                //addConlumn();

                //stopwatch1.Stop();
                //MessageBox.Show(stopwatch1.ElapsedMilliseconds.ToString(), "Add column");

                grvData.CellMerge += new CellMergeEventHandler(grvData_CellMerge_1);
                grvData.OptionsView.AllowCellMerge = true;

                grvData.OptionsBehavior.AutoExpandAllGroups = true;


                //Lib.LockEvents = false;
                //Stopwatch stopwatch2 = new Stopwatch();
                //stopwatch2.Start();
                //AddBand();
                //stopwatch2.Stop();
                //MessageBox.Show(stopwatch2.ElapsedMilliseconds.ToString(), "Setting");
            }


        }
        private void AddBand()
        {

            //GridBand bandMonth = new GridBand();
            DataTable dtMonth = dataSet.Tables[2];
            DataTable dtAllDate = dataSet.Tables[1];


            for (int i = 0; i < dtMonth.Rows.Count; i++)
            {

                // Thêm 1 band để hiển thị tháng năm 
                GridBand bandMonth = new GridBand();

                string month = TextUtils.ToString(dtMonth.Rows[i]["monthDate"]);
                string year = TextUtils.ToString(dtMonth.Rows[i]["yearDate"]);

                if (string.IsNullOrEmpty(month) || string.IsNullOrEmpty(year)) continue;

                bandMonth.Caption = "Tháng " + month + "/" + year;
                bandMonth.Name = month;
                bandMonth.MinWidth = 120;
                bandMonth.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                bandMonth.AppearanceHeader.Options.UseForeColor = true;
                bandMonth.AppearanceHeader.Options.UseFont = true;
                bandMonth.RowCount = 2;

                bandNote.Children.Add(bandMonth);
                listBand.Add(bandMonth);

                // hiển thị từng ngày trong khoảng thời gian từ bắt đầu đến kết thúc 
                var dataDates = dtAllDate.Select($"YearAllDates = {year} AND MonthAllDates = {month}");
                AddColumn(dataDates, bandMonth);
            }
        }


        void AddColumn(DataRow[] dataRows, GridBand gridBand)
        {
            foreach (DataRow row in dataRows)
            {
                string allDates = TextUtils.ToString(row["AllDates"]);
                if (string.IsNullOrWhiteSpace(allDates)) continue;

                //DateTime date = DateTime.Parse(TextUtils.ToString(dtAllDate.Rows[j]["AllDates"]));
                DateTime date = TextUtils.ToDate5(allDates);
                BandedGridColumn col = new BandedGridColumn();

                col.Visible = true;
                col.Caption = date.ToString("dd");

                col.Name = "col" + date.ToString("ddMMyy");
                col.FieldName = date.ToString("dd/MM/yyyy");
                col.Tag = date.ToString("dd/MM/yyyy");
                col.OptionsColumn.AllowMove = false;
                col.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                col.AppearanceHeader.Options.UseFont = true;
                col.AppearanceHeader.Options.UseForeColor = true;
                col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                col.AppearanceHeader.Options.UseTextOptions = true;
                col.MinWidth = 24;
                col.Width = 24;
                col.OptionsColumn.FixedWidth = true;
                col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                col.OptionsColumn.AllowSize = false;
                col.OptionsFilter.AllowFilter = false;
                col.OptionsFilter.AllowAutoFilter = false;



                gridBand.Columns.Add(col);
                listCol.Add(col);
            }
        }

        #region Load Combo
        void LoadDepartment()
        {
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();

            cboDepartment.Properties.DataSource = list;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";

            cboDepartment.EditValue = Global.DepartmentID;
        }


        void LoadTeam()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            List<UserTeamModel> list = SQLHelper<UserTeamModel>.FindByAttribute($"DepartmentID ", departmentID);

            cbTeam.Properties.DataSource = list;
            cbTeam.Properties.DisplayMember = "Name";
            cbTeam.Properties.ValueMember = "ID";

            UserTeamModel team = SQLHelper<UserTeamModel>.FindByAttribute("LeaderID", Global.EmployeeID).FirstOrDefault();
            if (team == null) return;
            cbTeam.EditValue = team.ID;
        }

        void LoadEmployee()
        {
            List<EmployeeModel> list = SQLHelper<EmployeeModel>.FindAll().Where(x => x.Status != 1).ToList() ;
            cboEmployee.Properties.DataSource = list;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";

            //cboEmployee.EditValue = Global.EmployeeID;
        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            LoadData();
            grvData.FocusedRowHandle = rowHandle;

        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle; // lấy ra dòng hiện tại đang focuse
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            if (ID == 0) return;
            // get ra id của nhân viên trong hạng mục công việc
            EmployeeModel model = (EmployeeModel)EmployeeBO.Instance.FindByPK(ID);

            // gán sang form hạng mục công việc của nhân viên đó 

            //frmHangMucCongViecDetail frm = new frmHangMucCongViecDetail();
            //frm.employeeModel = model;
            //if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    LoadData();
            //    grvData.FocusedRowHandle = focusedRowHandle;
            //}
        }

        private void grvData_CellMerge_1(object sender, CellMergeEventArgs e)
        {
            //if (Lib.LockEvents)
            //{
            //    return;
            //}
            if (e.Column == colFullName || e.Column == colProjectFullName || e.Column == colCode || e.Column == colMission)
            {
                string value1 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                string value2 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = (value1 == value2);
                e.Handled = true;
                return;
            }
            else
            {
                e.Handled = true;
                return;
            }
        }

        private void grvData_RowCellStyle_1(object sender, RowCellStyleEventArgs e)
        {
            return;
            if (e.RowHandle >= 0)
            {
                //decimal itemLate = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colItemLate));
                decimal itemLate = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colItemLateActual));
                if (e.Column == colType)
                {
                    if (TextUtils.ToString(e.CellValue) == "Plan")
                    {
                        e.Appearance.BackColor = Color.Aqua;
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                }
                if (e.Column == colCode)
                {
                    DataRow Row = grvData.GetDataRow(e.RowHandle); // lấy ra dòng 
                    //if (Lib.ToInt(Row["ItemLate"]) != 0) // nếu dòng đó có ItemLate != 0 => tô màu 
                    if (TextUtils.ToInt(Row["ItemLateActual"]) != 0) // nếu dòng đó có ItemLate != 0 => tô màu 
                    {
                        e.Appearance.BackColor = Color.DarkRed;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
                if (e.Column == colStartDate || e.Column == colTotalDays || e.Column == colEndDate || e.Column == colType)
                {
                    return;
                }

                string strDS = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colStartDate));
                string strDE = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colEndDate));

                DateTime? ds = null;
                DateTime? de = null;
                ds = string.IsNullOrEmpty(strDS) == true ? ds : DateTime.Parse(strDS);
                de = string.IsNullOrEmpty(strDE) == true ? de : DateTime.Parse(strDE);

                if (!DateTime.TryParse(e.Column.FieldName, out DateTime dateCol))
                {
                    return;
                }
                string dayOfWeek = dateCol.DayOfWeek.ToString();
                if (dayOfWeek == "Sunday")
                {
                    e.Appearance.BackColor = Color.FromArgb(224, 224, 224);
                }

                if (dateCol >= ds && dateCol <= de)
                {
                    if (dayOfWeek == "Sunday")
                    {
                        if (TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colType)) == "Plan")
                        {
                            e.Appearance.BackColor = Color.FromArgb(224, 224, 224);
                            e.Appearance.BackColor2 = Color.Aqua;
                        }
                        else
                        {

                            e.Appearance.BackColor = Color.FromArgb(224, 224, 224);
                            e.Appearance.BackColor2 = Color.Yellow;
                        }
                    }
                    else
                    {
                        if (TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colType)) == "Plan")
                        {
                            e.Appearance.BackColor = Color.Aqua;
                        }
                        else
                        {
                            if (e.Column == colProjectFullName && itemLate != 0)
                            {
                                e.Appearance.BackColor = Color.FromArgb(224, 224, 224);
                                e.Appearance.BackColor2 = Color.DarkRed;
                            }
                            else
                            {
                                e.Appearance.BackColor = Color.Yellow;
                            }
                        }
                    }

                    if (dateCol.Date == DateTime.Now.Date)
                    {
                        e.Appearance.BackColor = Color.Orange;
                        //e.Appearance.BackColor2 = Color.Aqua;
                    }
                }


                //check bôi mầu thực tế
                DateTime? actualDateStart = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colActualDateStart));
                DateTime? actualDateEnd = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colActualDateEnd));

                if (actualDateStart.HasValue)
                {
                    if (e.Column.FieldName == actualDateStart.Value.ToString("dd/MM/yyyy"))
                    {
                        e.Appearance.BackColor = Color.LimeGreen;
                    }
                }

                if (actualDateEnd.HasValue)
                {
                    if (e.Column.FieldName == actualDateEnd.Value.ToString("dd/MM/yyyy"))
                    {
                        e.Appearance.BackColor = Color.DeepPink;
                    }
                }

                if (!actualDateStart.HasValue && !actualDateEnd.HasValue)
                {
                    if (e.Column.FieldName == DateTime.Now.ToString("dd/MM/yyyy"))
                    {
                        e.Appearance.BackColor = Color.Red;
                    }
                }


            }
        }

        #region Sự kiện combo 
        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            LoadTeam();
        }
        private void cbTeam_EditValueChanged(object sender, EventArgs e)
        {

            int teamId = TextUtils.ToInt(cbTeam.EditValue);
            if (teamId == 0)
            {
                LoadEmployee();
            }
            else
            {
                //DataTable dt = TextUtils.Select($"Select e.* from Employee As e Join Users As u on u.ID = e.UserID Join UserTeamLink as utl on utl.UserID = u.ID Join UserTeam as ut on ut.ID = utl.UserTeamID WHERE ut.ID = {teamId}");

                List<EmployeeModel> list = SQLHelper<EmployeeModel>.ProcedureToList("spGetEmployeeByTeamID", new string[] { "@TeamID" }, new object[] { teamId });
                cboEmployee.Properties.DataSource = list;
                cboEmployee.Properties.DisplayMember = "FullName";
                cboEmployee.Properties.ValueMember = "ID";
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
            //addConlumn();
        }

        #endregion

        private void btnExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {

                string filepath = Path.Combine(f.SelectedPath, $"TimlineCongViec_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx");
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                //PrintableComponentLink printableComponentLink2 = new PrintableComponentLink(printingSystem);
                //printableComponentLink2.Component = grdData;

                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);
                    //compositeLink.Links.Add(printableComponentLink2);

                    //compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            //AddColumn();
        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //return;
            //if (Lib.LockEvents) return;
            //if (e.RowHandle < 0) return;
            //try
            //{
            //if (Lib.LockEvents) return;
            ////return;
            //if (e.RowHandle < 0) return;

            //DateTime? startDate = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colStartDate));
            //DateTime? endDate = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colEndDate));

            //DateTime? actualDateStart = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colActualDateStart));
            //DateTime? actualDateEnd = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colActualDateEnd));

            //bool isBandInfo = gridBand1.Columns.Contains((BandedGridColumn)e.Column);
            //if (isBandInfo) return;

            //DateTime? dateCol = TextUtils.ToDate4(e.Column.FieldName);
            //if (!dateCol.HasValue) return;
            //if (!startDate.HasValue || !endDate.HasValue) return;

            ////Check chủ nhật
            //bool isSunday = dateCol.Value.DayOfWeek == DayOfWeek.Sunday;
            //if (isSunday)
            //{
            //    //e.Appearance.BackColor = Color.Aqua;
            //    e.Appearance.BackColor = Color.FromArgb(224, 224, 224);
            //}


            //if (dateCol.Value.Date >= startDate.Value.Date &&
            //    dateCol.Value.Date <= endDate.Value.Date)
            //{
            //    e.Appearance.BackColor = Color.Aqua;
            //    if (isSunday)
            //    {
            //        //e.Appearance.BackColor = Color.Aqua;
            //        e.Appearance.BackColor2 = Color.FromArgb(224, 224, 224);
            //    }
            //}


            //if (actualDateStart.HasValue && e.Column.FieldName == actualDateStart.Value.ToString("dd/MM/yyyy"))
            //{
            //    e.Appearance.BackColor = Color.LimeGreen;
            //    if (isSunday)
            //    {
            //        //e.Appearance.BackColor = Color.Aqua;
            //        e.Appearance.BackColor2 = Color.FromArgb(224, 224, 224);
            //    }
            //}

            //if (actualDateEnd.HasValue && e.Column.FieldName == actualDateEnd.Value.ToString("dd/MM/yyyy"))
            //{

            //    if (actualDateEnd.Value.Date <= endDate.Value.Date)
            //    {
            //        e.Appearance.BackColor = Color.Pink;
            //        if (isSunday)
            //        {
            //            //e.Appearance.BackColor = Color.Aqua;
            //            e.Appearance.BackColor2 = Color.FromArgb(224, 224, 224);
            //        }
            //    }
            //    else
            //    {
            //        e.Appearance.BackColor = Color.DarkRed;
            //        if (isSunday)
            //        {
            //            //e.Appearance.BackColor = Color.Aqua;
            //            e.Appearance.BackColor2 = Color.FromArgb(224, 224, 224);
            //        }
            //    }
            //}

            //if (!actualDateStart.HasValue && !actualDateEnd.HasValue)
            //{
            //    if (DateTime.Now.Date >= endDate.Value.Date && e.Column.FieldName == endDate.Value.ToString("dd/MM/yyyy"))
            //    {
            //        e.Appearance.BackColor = Color.DarkRed;
            //        if (isSunday)
            //        {
            //            //e.Appearance.BackColor = Color.Aqua;
            //            e.Appearance.BackColor2 = Color.FromArgb(224, 224, 224);
            //        }
            //    }
            //}
            //}
            //finally
            //{
            //    Lib.LockEvents = false;
            //}
        }



        private void grvData_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            Lib.LockEvents = false;
            try
            {
                if (Lib.LockEvents) return;
                //return;
                if (e.RowHandle < 0) return;

                DateTime? startDate = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colStartDate));
                DateTime? endDate = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colEndDate));

                DateTime? actualDateStart = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colActualDateStart));
                DateTime? actualDateEnd = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colActualDateEnd));

                bool isBandInfo = gridBand1.Columns.Contains((BandedGridColumn)e.Column);
                if (isBandInfo) return;

                DateTime? dateCol = TextUtils.ToDate4(e.Column.FieldName);
                if (!dateCol.HasValue) return;
                if (!startDate.HasValue || !endDate.HasValue) return;

                //Check chủ nhật
                bool isSunday = dateCol.Value.DayOfWeek == DayOfWeek.Sunday;
                if (isSunday)
                {
                    //e.Appearance.BackColor = Color.Aqua;
                    e.Appearance.BackColor = Color.FromArgb(224, 224, 224);
                }


                if (dateCol.Value.Date >= startDate.Value.Date &&
                    dateCol.Value.Date <= endDate.Value.Date)
                {
                    e.Appearance.BackColor = Color.Aqua;
                    if (isSunday)
                    {
                        //e.Appearance.BackColor = Color.Aqua;
                        e.Appearance.BackColor2 = Color.FromArgb(224, 224, 224);
                    }
                }


                if (actualDateStart.HasValue && e.Column.FieldName == actualDateStart.Value.ToString("dd/MM/yyyy"))
                {
                    e.Appearance.BackColor = Color.LimeGreen;
                    if (isSunday)
                    {
                        //e.Appearance.BackColor = Color.Aqua;
                        e.Appearance.BackColor2 = Color.FromArgb(224, 224, 224);
                    }
                }

                if (actualDateEnd.HasValue && e.Column.FieldName == actualDateEnd.Value.ToString("dd/MM/yyyy"))
                {

                    if (actualDateEnd.Value.Date <= endDate.Value.Date)
                    {
                        e.Appearance.BackColor = Color.Pink;
                        if (isSunday)
                        {
                            //e.Appearance.BackColor = Color.Aqua;
                            e.Appearance.BackColor2 = Color.FromArgb(224, 224, 224);
                        }
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.DarkRed;
                        if (isSunday)
                        {
                            //e.Appearance.BackColor = Color.Aqua;
                            e.Appearance.BackColor2 = Color.FromArgb(224, 224, 224);
                        }
                    }
                }

                if (!actualDateStart.HasValue && !actualDateEnd.HasValue)
                {
                    if (DateTime.Now.Date >= endDate.Value.Date && e.Column.FieldName == endDate.Value.ToString("dd/MM/yyyy"))
                    {
                        e.Appearance.BackColor = Color.DarkRed;
                        if (isSunday)
                        {
                            //e.Appearance.BackColor = Color.Aqua;
                            e.Appearance.BackColor2 = Color.FromArgb(224, 224, 224);
                        }
                    }
                }
            }
            finally
            {
                Lib.LockEvents = false;
            }
        }
    }
}
