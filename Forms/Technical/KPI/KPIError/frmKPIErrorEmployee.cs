using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Utils;
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
using BMS.Business;
using DevExpress.Utils.Extensions;

namespace BMS
{
    public partial class frmKPIErrorEmployee : _Forms
    {
        public int departmentID = 0;
        public string deName;
        public frmKPIErrorEmployee()
        {
            InitializeComponent();
        }

        private void frmKPIErrorEmployee_Load(object sender, EventArgs e)
        {
            this.Text += " - " + deName;
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEndDate.Value = dtpStartDate.Value.AddMonths(+1).AddSeconds(-1);

            LoadKPIErrorType();

            LoadKPIError();
            LoadEmployee();
            LoadDepartMent();
            LoadData();
        }

        private void LoadDepartMent()
        {
            List<DepartmentModel> lst = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboDepartMent.Properties.DataSource = lst;
            cboDepartMent.Properties.ValueMember = "ID";
            cboDepartMent.Properties.DisplayMember = "Name";

            cboDepartMent.EditValue = departmentID;
        }

        void LoadKPIError()
        {
            //DataTable dt = TextUtils.LoadDataFromSP("spGetKPIError", "A", new string[] { }, new object[] { });

            //LinhTN update 13/11/2024 - add @TypeID
            int typeID = TextUtils.ToInt(cboKPIErrorType.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetKPIError", "A", new string[] { "@TypeID" }, new object[] { typeID });
            cboKPIError.Properties.DisplayMember = "Code";
            cboKPIError.Properties.ValueMember = "ID";
            cboKPIError.Properties.DataSource = dt;
        }
        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DataSource = dt;
        }

        //LinhTN update 13/11/2024 - add
        void LoadKPIErrorType()
        {
            var dt = SQLHelper<KPIErrorTypeModel>.FindByAttribute("IsDelete", 0);
            cboKPIErrorType.Properties.DisplayMember = "Code";
            cboKPIErrorType.Properties.ValueMember = "ID";
            cboKPIErrorType.Properties.DataSource = dt;

        }

        void LoadData()
        {
            int kpiErrorID = TextUtils.ToInt(cboKPIError.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);

            int typeID = TextUtils.ToInt(cboKPIErrorType.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetKPIErrorEmployee", "A",
                new string[] { "@StartDate", "@EndDate", "@KPIErrorID", "@EmployeeID", "@Keyword", "@TypeID", "@DepartmentID" },
                new object[] { dtpStartDate.Value, dtpEndDate.Value, kpiErrorID, employeeID, txtKeyword.Text.Trim(), typeID, TextUtils.ToInt(cboDepartMent.EditValue) });
            grdData.DataSource = dt;
        }

        private void cboKPIError_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmKPIErrorEmployeeDetail frm = new frmKPIErrorEmployeeDetail();
            frm.departmentID = departmentID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            KPIErrorEmployeeModel model = SQLHelper<KPIErrorEmployeeModel>.FindByID(ID);
            frmKPIErrorEmployeeDetail frm = new frmKPIErrorEmployeeDetail();
            frm.departmentID = departmentID;
            frm.model = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int[] selectedRows = grvData.GetSelectedRows();

            if (selectedRows.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn lỗi muốn xóa!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xóa danh sách lỗi vi phạm đã chọn không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {



                foreach (int row in selectedRows)
                {
                    int ID = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    string employeeName = TextUtils.ToString(grvData.GetRowCellValue(row, colEmployee));
                    if (ID == 0) continue;
                    //KPIErrorEmployeeModel model = SQLHelper<KPIErrorEmployeeModel>.FindByID(ID);
                    //if ( )
                    {
                        //model.IsDelete = true;
                        //SQLHelper<KPIErrorEmployeeModel>.Update(model);


                        var myDict = new Dictionary<string, object>()
                        {
                            {"IsDelete",true },
                            {"UpdatedBy",Global.AppUserName },
                            {"UpdatedDate",DateTime.Now },
                        };

                        SQLHelper<KPIErrorEmployeeModel>.UpdateFieldsByID(myDict, ID);

                    }
                }
                LoadData();
            }

        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        void LoadDataFile()
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            List<KPIErrorEmployeeFileModel> files = SQLHelper<KPIErrorEmployeeFileModel>.FindByAttribute("KPIErrorEmployeeID", ID);
            grdDataFile.DataSource = files;
            LoadImage();
        }
        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataFile();
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
                    KPIErrorEmployeeFileModel file = SQLHelper<KPIErrorEmployeeFileModel>.FindByID(fileID);
                    files.Add(file);
                }
            }
            else
            {
                MessageBox.Show("Chọn file ảnh cần xem!", "Thông báo");
                return;
            }
            frmKPIErrorEmployeeDetailImages frm = new frmKPIErrorEmployeeDetailImages();
            frm.files = files;

            string errorDateValue = TextUtils.ToString(grvData.GetFocusedRowCellValue(colErrorDate));
            DateTime errorDate = TextUtils.ToDate(errorDateValue);
            string errorEmployee = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));
            frm.Text = $"DANH SÁCH ẢNH CÁC LỖI VI PHẠM KPI - NGÀY VI PHẠM: {errorDate:dd/MM/yyyy} - NHÂN VIÊN VI PHẠM: {errorEmployee.ToUpper()}";
            frm.Show();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    //LoadData();
            //}
        }

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
                DateTime? errorDate = TextUtils.ToDate4(grvData.GetFocusedRowCellValue(colErrorDate));
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

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                colImagesLink.Visible = true;
                //FolderBrowserDialog f = new FolderBrowserDialog();
                SaveFileDialog f = new SaveFileDialog();
                f.Filter = "Excel Files|*.xlsx";
                f.FileName = $"DanhSachNhanVienViPhamLoiKPI_TuNgay{dtpStartDate.Value:ddMMyy}DenNgay{dtpEndDate.Value:ddMMyy}.xlsx";
                if (f.ShowDialog() == DialogResult.OK)
                {
                    //string filepath = Path.Combine(f.SelectedPath, $"DanhSachNhanVienViPhamLoiKPI_TuNgay{dtpStartDate.Value:ddMMyy}DenNgay{dtpEndDate.Value:ddMMyy}.xlsx");
                    string filepath = f.FileName;

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    grvData.OptionsPrint.AutoWidth = false;
                    grvData.OptionsPrint.ExpandAllDetails = false;
                    grvData.OptionsPrint.PrintDetails = true;
                    grvData.OptionsPrint.UsePrintStyles = true;
                    //optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;

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
            catch (Exception)
            {
                throw;
            }
            finally
            {
                colImagesLink.Visible = false;
            }
        }

        private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadData();
        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmKPIErrorEmployeeImportExcel frm = new frmKPIErrorEmployeeImportExcel();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void cboKPIErrorType_EditValueChanged(object sender, EventArgs e)
        {
            LoadKPIError();
            LoadData();
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

        private void btnAutoAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Đang cập nhật lỗi báo cáo", "Vui lòng đợi"))
                {
                    //var userTeams = TextUtils.Select("Select ID from UserTeam");
                    var userTeams = SQLHelper<UserTeamModel>.FindAll();
                    string idList = string.Join(";", userTeams.Select(x => x.ID).ToList());

                    DateTime dateStart = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
                    DateTime dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
                    DateTime dateEndNow = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 23, 59, 59);

                    dateEnd = dateEndNow.Date < dateEnd.Date ? dateEndNow : dateEnd;

                    var ds = TextUtils.LoadDataSetFromSP("spExportToExcelDRT",
                        new string[] { "DateStart", "DateEnd", "TeamID" },
                        new object[] { dateStart, dateEnd, idList });
                    var dt = ds.Tables[1];
                    //var lst = new List<KPIErrorEmployeeModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        var newError = new KPIErrorEmployeeModel()
                        {
                            KPIErrorID = TextUtils.ToString(row["DayValue"]).ToLower() == "xm" ? 3 : 1,
                            EmployeeID = TextUtils.ToInt(row["EmployeeID"]),
                            ErrorDate = TextUtils.ToDate4(row["AllDates"]),
                            ErrorNumber = 1,
                            Note = "",
                            TotalMoney = 10000.00M
                        };
                        //lst.Add(newError);

                        var findByErrorID = new Expression("KPIErrorID", newError.KPIErrorID);
                        var findByEmployeeID = new Expression("EmployeeID", newError.EmployeeID);
                        //var findByErrorDate = new Expression("ErrorDate", newError.ErrorDate.Value);
                        var findByIsDeleted = new Expression("IsDelete", 0);

                        var findByErrorDateYear = new Expression("YEAR(ErrorDate)", newError.ErrorDate.Value.Year);
                        var findByErrorDateMonth = new Expression("MONTH(ErrorDate)", newError.ErrorDate.Value.Month);
                        var findByErrorDateDay = new Expression("DAY(ErrorDate)", newError.ErrorDate.Value.Day);

                        var existed = SQLHelper<KPIErrorEmployeeModel>.FindByExpression(findByErrorID.And(findByEmployeeID).And(findByErrorDateYear.And(findByErrorDateMonth.And(findByErrorDateDay))).And(findByIsDeleted));
                        if (existed.Count == 0) SQLHelper<KPIErrorEmployeeModel>.Insert(newError);
                        //SQLHelper<KPIErrorEmployeeModel>.Insert(newError);
                    }
                    //var t = lst[5000];
                }
                MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}