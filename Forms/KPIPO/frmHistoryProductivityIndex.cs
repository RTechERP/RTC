using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using Forms.Classes;
using Forms.KPI_PO;
using System;
using System.Collections;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace BMS
{
    public partial class frmHistoryProductivityIndex : _Forms
    {
        int warehouseID = 0;

        DataTable dtKPIAddmin;
        public List<int> LstMainGroupCount = new List<int>(); // số lượng maingroup
        DevExpress.XtraGrid.Views.Grid.GridView grvKPIStaff;
        public decimal colACCP;

        public frmHistoryProductivityIndex(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }
        int quy;
        ucKPIStaff ucKPI;
        ucKPIAdmin ucKPIAddmin;
        private void frmProductivityIndex_Load(object sender, EventArgs e)
        {
            this.Text += $" - {this.Tag}";
            try
            {
                cGlobVar.LockEvents = true;
                LoadCbNote();
                checktime();
                loaduser();
                cbUser.EditValue = Global.UserID;
                nbrYear.Value = DateTime.Now.Year;
                loadPositon();

                loadReportIndex();
                cbQuy.SelectedIndex = quy;
                btnExcel.Enabled = false;

            }
            finally
            {
                cGlobVar.LockEvents = false;
            }

        }

        void LoadCbNote()
        {
            DataTable dt = TextUtils.Select("Select MainIndex,ID From [MainIndex]");
            cbNote.Properties.ValueMember = "ID";
            cbNote.Properties.DisplayMember = "MainIndex";
            cbNote.Properties.DataSource = dt;
        }
        void loadPositon()
        {
            DataTable dt = TextUtils.Select("Select * From [SaleUserType]");
            cbPosition.DisplayMember = "SaleUserTypeName";
            cbPosition.ValueMember = "ID";
            cbPosition.DataSource = dt;
        }
        void loadReportIndex()
        {
            if (cbUser.EditValue == null) return;

            DataSet ds = TextUtils.LoadDataSetFromSP("spGetPerformanceKPI", 
                                                    new string[] { "@quy", "@year", "@userID", "@WarehouseID" }, 
                                                    new object[] { cbQuy.SelectedIndex + 1, nbrYear.Value, TextUtils.ToInt(cbUser.EditValue),warehouseID });
            grdReport.DataSource = ds.Tables[0];
            grdPerformance.DataSource = ds.Tables[1];
        }

        decimal thang0 = 0, thang1 = 0, index = 0, thang2 = 0;
        decimal avg0 = 0;
        decimal sumPer;
        void caculPerformance()
        {

            if (Position == cConsts.Staff || Position == cConsts.LeaderTeam || Position == cConsts.Leader)

                sumPer = TextUtils.ToDecimal(ucKPI.sumACCP);
            else if (Position == cConsts.Admin)
                sumPer = (decimal)1.0;
            else if (Position == cConsts.Marketting)
            {
                sumPer = TextUtils.ToDecimal(ucKPIAddmin.SumActual);
                grvPerformance.SetRowCellValue(0, colPerformance, sumPer);
                tinhheso();
                return;
            }
            for (int i = 0; i < grvReport.RowCount; i++)
            {
                thang0 = TextUtils.ToDecimal(grvReport.GetRowCellValue(i, colMonth0));
                thang1 = TextUtils.ToDecimal(grvReport.GetRowCellValue(i, colMonth1));
                thang2 = TextUtils.ToDecimal(grvReport.GetRowCellValue(i, colMonth2));
                index = TextUtils.ToDecimal(grvReport.GetRowCellValue(i, colPercent));
                avg0 = (thang0 + thang1 + thang2) * index / 3;
                sumPer += avg0;
            }
            grvPerformance.SetRowCellValue(0, colPerformance, sumPer);
            tinhheso();


        }
        string month;
        void checktime()
        {

            if (0 < DateTime.Now.Month && DateTime.Now.Month < 4)
            {
                month = "1,2,3";
                quy = 0;
            }
            if (3 < DateTime.Now.Month && DateTime.Now.Month < 7)
            {
                month = "4,5,6";
                quy = 1;
            }
            if (6 < DateTime.Now.Month && DateTime.Now.Month < 10)
            {
                month = "7,8,9";
                quy = 2;
            }
            if (9 < DateTime.Now.Month && DateTime.Now.Month < 13)
            {
                month = "10,11,12";
                quy = 3;
            }
        }
        decimal tongG = 0, tongR = 0, tongA = 0;
        void loaduser()
        {
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetEmployeeManager", new string[] { "@group" }, new object[] { TextUtils.ToInt(0) });
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = ds.Tables[2];
        }
        void loadMainIndex()
        {
            try
            {
                if (ucKPI != null)
                {
                    colMonth0.Caption = "Tháng " + ucKPI.dtMonth.Rows[0]["MonthReport"];
                    colMonth1.Caption = "Tháng " + ucKPI.dtMonth.Rows[1]["MonthReport"];
                    colMonth2.Caption = "Tháng " + ucKPI.dtMonth.Rows[2]["MonthReport"];
                }
                else if (ucKPIAddmin != null)
                {
                    colMonth0.Caption = "Tháng " + ucKPIAddmin.dtMonth.Rows[0]["MonthReport"];
                    colMonth1.Caption = "Tháng " + ucKPIAddmin.dtMonth.Rows[1]["MonthReport"];
                    colMonth2.Caption = "Tháng " + ucKPIAddmin.dtMonth.Rows[2]["MonthReport"];
                }
            }
            catch (Exception ex)
            { }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmProductivityIndex frm = new frmProductivityIndex();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //loadMainIndex();
            }
        }

        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadReportIndex();
            caculPerformance();


        }

        private void frmHistoryProductivityIndex_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void nbrYear_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tính toán..."))
            {
                loadReportIndex();
                caculPerformance();
            }
        }

        void SaveKpi()
        {
            if (Position == cConsts.Marketting)
            {
                AdminMarketingDetailModel adm = new AdminMarketingDetailModel();
                for (int i = 0; i < ucKPIAddmin.dtadmin.Rows.Count; i++)
                {
                    if (TextUtils.ToInt(ucKPIAddmin.dtadmin.Rows[i]["ID"]) > 0)
                        adm = (AdminMarketingDetailModel)AdminMarketingDetailBO.Instance.FindByPK(TextUtils.ToInt(ucKPIAddmin.dtadmin.Rows[i]["ID"]));
                    adm.Month1 = TextUtils.ToInt(ucKPIAddmin.dtadmin.Rows[i]["Month1"]);
                    adm.Month2 = TextUtils.ToInt(ucKPIAddmin.dtadmin.Rows[i]["Month2"]);
                    adm.Month3 = TextUtils.ToInt(ucKPIAddmin.dtadmin.Rows[i]["Month3"]);
                    adm.KPIID = TextUtils.ToInt(ucKPIAddmin.dtadmin.Rows[i]["IDMaster"]);
                    adm.Quy = quy;
                    adm.UserID = TextUtils.ToInt(cbUser.EditValue);
                    adm.Year = DateTime.Now.Year;
                    adm.WarehouseID = warehouseID; //Lt.Anh update 16/01/2024
                    if (adm.ID > 0)
                        AdminMarketingDetailBO.Instance.Update(adm);
                    else
                        AdminMarketingDetailBO.Instance.Insert(adm);
                }
            }

            ReportIndexModel report = new ReportIndexModel();
            for (int i = 0; i < grvReport.RowCount; i++)
            {
                int IDre = TextUtils.ToInt(grvReport.GetRowCellValue(i, colIDre));
                if (IDre > 0)
                    report = (ReportIndexModel)ReportIndexBO.Instance.FindByPK(IDre);
                report.Month0 = TextUtils.ToInt(grvReport.GetRowCellValue(i, colMonth0));
                report.Month1 = TextUtils.ToInt(grvReport.GetRowCellValue(i, colMonth1));
                report.Month2 = TextUtils.ToInt(grvReport.GetRowCellValue(i, colMonth2));
                report.Quy = quy;
                report.Year = TextUtils.ToInt(nbrYear.Value);
                report.UserID = TextUtils.ToInt(cbUser.EditValue);
                report.KPIID = TextUtils.ToInt(grvReport.GetRowCellValue(i, colKpiID));
                report.WarehouseID = warehouseID; //Lt.Anh update 16/01/2024
                if (IDre > 0)
                    ReportIndexBO.Instance.Update(report);
                else
                    ReportIndexBO.Instance.Insert(report);
            }

            SalesPerformanceRankingModel sale = new SalesPerformanceRankingModel();
            int ID = TextUtils.ToInt(grvPerformance.GetRowCellValue(0, colIDBot));
            if (ID > 0)
                sale = (SalesPerformanceRankingModel)SalesPerformanceRankingBO.Instance.FindByPK(ID);
            sale.Performance = TextUtils.ToDecimal(grvPerformance.GetRowCellValue(0, colPerformance));
            sale.Coefficient = TextUtils.ToDecimal(grvPerformance.GetRowCellValue(0, colCoefficient));
            if (Position == cConsts.Staff || Position == cConsts.LeaderTeam || Position == cConsts.Leader || Position == cConsts.Admin)
            {
                sale.NewAccountQty = ucKPI.NewAccountQty;
                sale.TotalSale = ucKPI.TotalSale;
            }
            else if (Position == cConsts.Marketting)
            {
                sale.TotalSale = ucKPIAddmin.TotalSale;
            }

            sale.UserID = TextUtils.ToInt(cbUser.EditValue);
            sale.Quy = quy;
            sale.Year = DateTime.Now.Year;
            sale.WarehouseID = warehouseID; //Lt.Anh update 16/01/2024
            if (ID > 0)
            {
                SalesPerformanceRankingBO.Instance.Update(sale);
            }
            else
                SalesPerformanceRankingBO.Instance.Insert(sale);
            MessageBox.Show("Đã cất thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            grvReport.FocusedRowHandle = -1;
            if (quy == cbQuy.SelectedIndex && nbrYear.Value == DateTime.Now.Year)
                SaveKpi();
            loadReportIndex();
        }

        private void grvReport_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colMonth0 || e.Column == colMonth1 || e.Column == colMonth2)
            {
                caculPerformance();
            }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            frmChooseReport frm = new frmChooseReport();
            frm.ShowDialog();
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            GroupSalesUserModel model = new GroupSalesUserModel();
            model = (GroupSalesUserModel)GroupSalesUserBO.Instance.FindByCode("UserID", TextUtils.ToString(cbUser.EditValue));
            model.Note = TextUtils.ToString(cbNote.EditValue);
            GroupSalesUserBO.Instance.Update(model);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmEditKPI frm = new frmEditKPI();
            frm.user = TextUtils.ToInt(cbUser.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadReportIndex();
                loadMainIndex();
                caculPerformance();
            }
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grdReport_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSALE frm = new frmSALE();
            frm.user = TextUtils.ToInt(cbUser.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadMainIndex();
                loadReportIndex();
                caculPerformance();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            caculPerformance();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (grvKPIStaff.RowCount <= 0) return;
            string fileName = cbUser.Text.Trim() + "_" + cbQuy.Text.Trim() + "_" + nbrYear.Value;
            string filePath = TextUtils.ExportExcelReturnFilePath(grvKPIStaff, fileName);
            if (string.IsNullOrEmpty(filePath)) return;

            Excel.Application app = default(Excel.Application);
            Excel.Workbook workBoook = default(Excel.Workbook);
            Excel.Worksheet workSheet = default(Excel.Worksheet);
            try
            {
                app = new Excel.Application();
                app.Workbooks.Open(filePath);
                workBoook = app.Workbooks[1];
                workSheet = (Excel.Worksheet)workBoook.Worksheets[1];
                app.DisplayAlerts = false;

                int number = grvKPIStaff.RowCount + LstMainGroupCount.Count + 3;
                int numberTotal;
                if (grvReport.RowCount > grvPerformance.RowCount) numberTotal = number + grvReport.RowCount;
                else numberTotal = number + grvPerformance.RowCount;
                for (int j = number; j <= numberTotal; j++)
                {
                    int k = j - number - 1;
                    if (j == number)
                    {
                        workSheet.Cells[j - 1, 1] = "Các chỉ số về báo cáo";
                        workSheet.Cells[j - 1, 7] = "Hệ số tính thường";
                        for (int m = 1; m <= 9; m++)
                        {
                            if (m == 6) continue;
                            workSheet.Cells[j - 1, m].Style.Font.Size = 14;
                            workSheet.Cells[j - 1, m].Interior.Color = Excel.XlRgbColor.rgbLightGray; // màu
                            workSheet.Cells[j - 1, m].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter; // căn giữa
                        }

                        workSheet.Cells[j, 1] = "KPI";
                        workSheet.Cells[j, 2] = "Rule đạt điểm";
                        workSheet.Cells[j, 1].Interior.Color = workSheet.Cells[j, 2].Interior.Color = Excel.XlRgbColor.rgbGold; // màu
                        workSheet.Cells[j, 3] = "Tháng 1";
                        workSheet.Cells[j, 3].Interior.Color = workSheet.Cells[j, 2].Interior.Color = Excel.XlRgbColor.rgbBeige; // màu 
                        workSheet.Cells[j, 4] = "Tháng 2";
                        workSheet.Cells[j, 4].Interior.Color = workSheet.Cells[j, 2].Interior.Color = Excel.XlRgbColor.rgbCadetBlue; // màu 
                        workSheet.Cells[j, 5] = "Tháng 3";
                        workSheet.Cells[j, 5].Interior.Color = workSheet.Cells[j, 2].Interior.Color = Excel.XlRgbColor.rgbLightGreen; // màu 

                        workSheet.Cells[j, 7] = "Performance";
                        workSheet.Cells[j, 8] = "Hệ số tính thưởng";
                        workSheet.Cells[j, 9] = "Chức vụ";
                    }
                    else
                    {
                        workSheet.Cells[j, 1] = TextUtils.ToString(grvReport.GetRowCellValue(k, colKPI));
                        workSheet.Cells[j, 2] = TextUtils.ToString(grvReport.GetRowCellValue(k, colNote));
                        workSheet.Cells[j, 3] = TextUtils.ToString(grvReport.GetRowCellValue(k, colMonth0));
                        workSheet.Cells[j, 4] = TextUtils.ToString(grvReport.GetRowCellValue(k, colMonth1));
                        workSheet.Cells[j, 5] = TextUtils.ToString(grvReport.GetRowCellValue(k, colMonth2));

                        workSheet.Cells[j, 7] = TextUtils.ToString(grvPerformance.GetRowCellValue(k, colPerformance));
                        workSheet.Cells[j, 8] = TextUtils.ToString(grvPerformance.GetRowCellValue(k, colCoefficient));
                        workSheet.Cells[j, 9] = TextUtils.ToString(grvPerformance.GetRowCellValue(k, colPosition));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                app.ActiveWorkbook.Save();
                app.Workbooks.Close();
                app.Quit();

                Process.Start(filePath);
            }
        }

        private void cbUser_EditValueChanged_1(object sender, EventArgs e)
        {
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tính toán..."))
            {
                try
                {
                    // phân quyền admin
                    //DataTable dt = TextUtils.Select($"Select * From [GroupSalesUser] WHERE UserID = {cbUser.EditValue} AND( SaleUserTypeID = 1 OR SaleUserTypeID = 2 OR SaleUserTypeID = 6 OR SaleUserTypeID = 7 OR SaleUserTypeID = 8 )");
                    //if (dt.Rows.Count > 0) btnSave.Enabled = toolStripButton1.Enabled = btnEditMainIndex.Enabled = true;
                    //else btnSave.Enabled = toolStripButton1.Enabled = btnEditMainIndex.Enabled = false;

                    btnEdit.Enabled = true;
                    btnSettings.Enabled = true;
                    btnReload.Enabled = true;
                    tableLayoutPanel1.Controls.Clear();
                    DataTable note = TextUtils.Select($"Select Note,GroupSalesID From [GroupSalesUser] where UserID = {cbUser.EditValue}");
                    cbNote.EditValue = note.Rows[0]["Note"];
                    if (cGlobVar.LockEvents) return;
                    Position = TextUtils.ToString(TextUtils.ExcuteScalar($"Select SaleUserTypeCode from GroupSalesUser g Inner join SaleUserType s on g.SaleUserTypeID=s.ID where UserID={ cbUser.EditValue}"));
                    if (Position == cConsts.Staff || Position == cConsts.LeaderTeam || Position == cConsts.Leader || Position == cConsts.Admin)
                    {
                        ucKPI = new ucKPIStaff();
                        btnNote.Enabled = true;
                        ucKPI.cbUser = cbUser;
                        ucKPI.cbMonth = cbQuy;
                        ucKPI.nbrYear = nbrYear;
                        ucKPI.Position = Position;
                        ucKPI.cbNote = cbNote;
                        ucKPI.Groupsale = TextUtils.ToInt(note.Rows[0]["GroupSalesID"]);
                        ucKPI.ckHide = ckHide;
                        ucKPI.btnNote = btnNote;
                        ucKPI.Dock = DockStyle.Fill;
                        ucKPI.btnReload = btnReload;
                        ucKPI.btnSave = btnSave;
                        ucKPI.cbQuy = cbQuy;


                        // todo: 13022022
                        tableLayoutPanel1.Controls.Add(ucKPI);
                        grvKPIStaff = ucKPI.grvKPIStaff;
                        //LstMainGroupCount = ucKPI.LstMainGroupCount;
                    }
                    else if (Position == cConsts.Marketting)
                    {
                        btnNote.Enabled = false;
                        ucKPIAddmin = new ucKPIAdmin();
                        ucKPIAddmin.Dock = DockStyle.Fill;
                        ucKPIAddmin.cbUser = cbUser;
                        ucKPIAddmin.nbrYear = nbrYear;
                        ucKPIAddmin.cbMonth = cbQuy;
                        ucKPIAddmin.Send += ReLoadData;
                        tableLayoutPanel1.Controls.Add(ucKPIAddmin);
                        // dtKPIAddmin = ucKPIAddmin.dtKPIAdmin;

                    }
                    if (Position == cConsts.Admin)
                    {
                        btnNote.Enabled = false;
                    }
                    loadMainIndex();
                    loadReportIndex();
                    caculPerformance();
                    btnExcel.Enabled = true;
                }
                catch { }
            }
        }

        private void btnEditMainIndex_Click(object sender, EventArgs e)
        {
            DataTable dt = TextUtils.Select($"Select g.MainIndexID from GroupSalesUser gu inner join GroupSales g on g.ID=gu.GroupSalesID where gu.UserID={cbUser.EditValue}");
            frmEditPercent frm = new frmEditPercent();
            if (dt.Rows.Count > 0)
            {
                frm.user = TextUtils.ToInt(cbUser.EditValue);
                frm.main = TextUtils.ToString(dt.Rows[0]["MainIndexID"]);
            }
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadMainIndex();
                loadReportIndex();
                caculPerformance();
            }
        }
        string Position;
        void tinhheso()
        {
            decimal per = TextUtils.ToDecimal(grvPerformance.GetRowCellValue(0, colPerformance));
            DataTable dt = TextUtils.Select($"Select b.* From [dbo].[BonusRule] b Inner join [dbo].[GroupSalesUser] g on g.SaleUserTypeID = b.SaleUserTypeID and g.GroupSalesID = b.GroupSaleID where g.UserID ={cbUser.EditValue} ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (TextUtils.ToInt(dt.Rows[i]["CompareMAX"]))
                {
                    case 0:
                        if (per < (decimal)dt.Rows[i]["Max"])
                        {
                            grvPerformance.SetRowCellValue(0, colCoefficient, (decimal)dt.Rows[i]["Value"]);
                            return;
                        }
                        break;
                    case 1:
                        if (per <= (decimal)dt.Rows[i]["Max"])
                        {
                            grvPerformance.SetRowCellValue(0, colCoefficient, (decimal)dt.Rows[i]["Value"]);
                            return;
                        }
                        break;
                    default:
                        break;
                }


            }
        }
        private void ReLoadData(string signal)
        {
            loadReportIndex();
            caculPerformance();
        }
    }
}
