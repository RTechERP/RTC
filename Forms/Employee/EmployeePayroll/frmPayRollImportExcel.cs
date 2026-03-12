using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
namespace BMS
{
    public partial class frmPayRollImportExcel : _Forms
    {
        DateTime start;
        DataSet ds;
        //public int IDMaster;
        public int payrollID;

        public frmPayRollImportExcel()
        {
            InitializeComponent();
        }

        #region Lưu dữ liệu

        private void SaveData()
        {
            //if (grvData.RowCount <= 0) return;

            int stt = 0;
            for (int i = 0; i < grvData.RowCount; i++)
            {
                try
                {
                    //int rowHandle = i;
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));

                    stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F3")); // STT
                    if (stt <= 0) continue;
                    string employeeCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")).Trim();

                    EmployeeModel employee = SQLHelper<EmployeeModel>.FindByAttribute("Code", employeeCode).FirstOrDefault() ?? new EmployeeModel();

                    //var EmpolyeeEntryDate = SQLHelper<EmployeeModel>.FindByID(EmployeeID);
                    //int ID = ep.ID;

                    if (employee.ID <= 0) continue;

                    decimal basicSalary = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F8"));// Lương cơ bản tham chiếu

                    var ex1 = new Expression("PayrollID", payrollID);
                    var ex2 = new Expression("EmployeeID", employee.ID);
                    var ex3 = new Expression("BasicSalary", basicSalary);
                    EmployeePayrollDetailModel ep = SQLHelper<EmployeePayrollDetailModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault();
                    ep = ep ?? new EmployeePayrollDetailModel();

                    ep.PayrollID = payrollID;
                    ep.EmployeeID = employee.ID;
                    ep.IsPublish = TextUtils.ToBoolean(grvData.GetRowCellValue(i, "F1")); // công bố
                    ep.Sign = TextUtils.ToBoolean(grvData.GetRowCellValue(i, "F2")); // kí nhận

                    //ep.EntryDate = EmpolyeeEntryDate.NgayBatDauThuViec;
                    ep.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, "F3")); // STT
                    ep.BasicSalary = basicSalary; // Lương cơ bản tham chiếu
                    ep.TotalMerit = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F9")); // Công
                    ep.TotalSalaryByDay = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F10")); // Lương
                    ep.SalaryOneHour = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F11")); // Tiền công /h

                    ep.OT_Hour_WD = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F12")); // Số h ngày thường
                    ep.OT_Money_WD = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F13")); // Thành tiền ngày thường
                    ep.OT_Hour_WK = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F14")); // số giờ cuối tuần 
                    ep.OT_Money_WK = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F15")); // Thành tiền cuối tuần
                    ep.OT_Hour_HD = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F16")); // số giờ ngày lễ
                    ep.OT_Money_HD = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F17")); // Thành tiền ngày lễ
                                                                                             //ep.OT_TotalSalary = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F18")); // tổng tiền làm thêm

                    //ep.ReferenceIndustry = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F19")); // phụ cấp chuyên cần tham chiếu
                    ep.RealIndustry = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F20")); // phụ cấp chuyên cần thực lĩnh 
                    ep.AllowanceMeal = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F21")); // phụ cấp ăn cơm
                    ep.Allowance_OT_Early = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F22")); // phụ cấp đi làm trước 7h15
                                                                                                    //ep.TotalAllowance = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F23")); // tổng phụ cấp

                    ep.BussinessMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F24")); // tiền công tác phí
                    ep.NightShiftMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F25")); // tiền công làm đêm
                    ep.CostVehicleBussiness = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F26")); // chi phí phương tiện
                    ep.Bonus = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F27")); // thưởng kpi/doanh số
                    ep.Other = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F28")); // khác
                                                                                       //ep.TotalBonus = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F29")); // tổng cộng
                    ep.RealSalary = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F30")); // Tổng thu nhập

                    //ep.SocialInsurance = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F31")); // mức đóng
                    ep.Insurances = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F32")); // thu BHXH
                    ep.UnionFees = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F33")); // phí công đoàn

                    ep.AdvancePayment = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F34")); // tạm ứng lương
                    ep.DepartmentalFees = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F35")); // thu hộ phòng ban
                    ep.ParkingMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F36")); // Gửi xe ô tô
                    ep.Punish5S = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F37")); // phạt 5s
                    ep.MealUse = TextUtils.ToInt(grvData.GetRowCellValue(i, "F38")); // cơm ca
                    ep.OtherDeduction = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F39")); // chí phí khác phải trừ

                    ep.ActualAmountReceived = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F41")); // thực lĩnh
                    ep.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F42")).Trim(); // ghi chú

                    if (ep.ID > 0)
                    {
                        SQLHelper<EmployeePayrollDetailModel>.Update(ep);
                    }
                    else
                    {
                        ep.ID = SQLHelper<EmployeePayrollDetailModel>.Insert(ep).ID;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi lưu dữ liệu tại dòng [{i + 1}] - STT: {stt}\n{ex.ToString()}", "Thông báo");
                    continue;
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                progressBar1.Minimum = 1;
                if (grvData.RowCount <= 0)
                {
                    MessageBox.Show(String.Format("Bạn chưa chọn đường dẫn file hoặc tên sheet. Vui lòng chọn và thử lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    progressBar1.Maximum = grvData.RowCount;
                    txtRate.Text = "";
                    start = DateTime.Now;
                    enableControl(false);
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            //if (SaveData())
            //{
            //    MessageBox.Show("Dữ liệu đã được lưu!", "Thông báo", MessageBoxButtons.OK);
            //}
        }

        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }

        #endregion

        #region Mẫu excel
        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo("BANG_LUONG.xlsx");
                if (fi.Exists)
                {
                    System.Diagnostics.Process.Start("BANG_LUONG.xlsx");
                }
                else
                {
                    MessageBox.Show("file doesn't exist", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        #endregion

        #region Chọn đường dẫn excel
        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            grvData.Columns.Clear();
            txtRate.Text = "";
            progressBar1.Value = 0;
            OpenFileDialog ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnBrowse.Text = ofd.FileName;


                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
                {
                    var stream = new FileStream(btnBrowse.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    var sw = new Stopwatch();
                    sw.Start();


                    IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

                    var openTiming = sw.ElapsedMilliseconds;

                    ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        UseColumnDataType = false,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = false
                        }
                    });

                    var tablenames = GetTablenames(ds.Tables);

                    cboSheet.DataSource = tablenames;

                    if (tablenames.Count > 0) cboSheet.SelectedIndex = 0;

                    btnSave.Enabled = true;
                    var tablename = cboSheet.SelectedItem.ToString();

                    grdData.DataSource = ds; // dataset
                    grdData.DataMember = tablename;

                }

            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                try
                {

                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
                    {
                        var stream = new FileStream(btnBrowse.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                        var sw = new Stopwatch();
                        sw.Start();

                        IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

                        var openTiming = sw.ElapsedMilliseconds;

                        ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            UseColumnDataType = false,
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = false
                            }
                        });

                        var tablenames = GetTablenames(ds.Tables);

                        cboSheet.DataSource = tablenames;

                        if (tablenames.Count > 0)
                            cboSheet.SelectedIndex = 0;

                        btnSave.Enabled = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    btnBrowse.Text = ofd.FileName;
                    cboSheet.DataSource = null;
                    cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);

                    cboSheet_SelectionChangeCommitted(null, null);
                }
            }
            AdjustColumnWidths();
        }

        private void AdjustColumnWidths()
        {
            // Không cho chỉnh sửa
            //grvData.Columns[0].OptionsColumn.AllowEdit = false;
            //grvData.Columns[1].OptionsColumn.AllowEdit = false;
            //grvData.Columns[2].OptionsColumn.AllowEdit = false;
            //grvData.Columns[3].OptionsColumn.AllowEdit = false;
            //grvData.Columns[4].OptionsColumn.AllowEdit = false;
            //grvData.Columns[5].OptionsColumn.AllowEdit = false;

            DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            dateEdit.Mask.EditMask = "dd/MM/yyyy";
            dateEdit.Mask.UseMaskAsDisplayFormat = true;

            // Gán sự kiện CustomRowCellEdit cho GridView
            grvData.CustomRowCellEdit += grvData_CustomRowCellEdit;

            grvData.OptionsView.ColumnAutoWidth = false;
            grvData.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll;

            foreach (DevExpress.XtraGrid.Columns.GridColumn column in grvData.Columns)
            {
                column.Width = 150;
            }
        }

        private static IList<string> GetTablenames(DataTableCollection tables)
        {
            var tableList = new List<string>();
            foreach (var table in tables)
            {
                if (table.ToString().Contains('.')) continue;
                tableList.Add(table.ToString());
            }

            return tableList;
        }

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            grvData.Columns.Clear();
            try
            {
                var tablename = cboSheet.SelectedItem.ToString();

                grdData.DataSource = ds; // dataset
                grdData.DataMember = tablename;
            }
            catch (Exception ex)
            {
                TextUtils.ShowError(ex);
                grdData.DataSource = null;
            }

            if (grdData.DataSource == null)
            {
                try
                {
                    DataTable dt = TextUtils.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

                    grdData.DataSource = dt;
                    grvData.PopulateColumns();
                    grvData.BestFitColumns();
                    grdData.Focus();
                }
                catch (Exception ex)
                {
                    TextUtils.ShowError(ex);
                    grdData.DataSource = null;
                }
            }
        }
        #endregion

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void grvData_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {

        }

        private void frmPayRollImportExcel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SaveData();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                enableControl(true);
                return;
            };

            MessageBox.Show($"Cập nhật thành công!\n{start.ToString()} - {DateTime.Now.ToString()}", "Thông báo");
            enableControl(true);
            this.Close();
        }

        private void frmPayRollImportExcel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnBrowse_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void frmPayRollImportExcel_Load(object sender, EventArgs e)
        {

        }
    }
}
