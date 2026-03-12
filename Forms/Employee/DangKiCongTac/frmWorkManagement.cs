
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
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
    public partial class frmWorkManagement : _Forms
    {
        public frmWorkManagement()
        {
            InitializeComponent();
        }

        private void frmWorkManagement_Load(object sender, EventArgs e)
        {
            //var request = WebRequest.Create("http://192.168.1.2:8083/api/Upload/BillVehicle/1_3006232146.jpg");
            ////request.Timeout = 1000;
            //var response = request.GetResponse();
            //var stream = response.GetResponseStream();


            txtMonth.Value = DateTime.Now.Month;
            txtYear.Value = DateTime.Now.Year;

            loadDepartment();
            loadEmployee();
            loadData();
            //loadDataVehicleEarly();
        }

        private void loadData()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);

            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeBussinessByMonth", "A"
                , new string[] { "@Month", "@Year", "@DepartmentID", "@EmployeeID", "@Keyword" }
                , new object[] { TextUtils.ToInt(txtMonth.Value), TextUtils.ToInt(txtYear.Value), departmentID, employeeID, txtKeyword.Text.Trim() });
                grdData.DataSource = dt;

                bandTitle.Caption = $"BẢNG CHẤM CÔNG ĐI CÔNG TÁC THÁNG {txtMonth.Value}";
                loadWeekDays();

                loadDataVehicleEarly();
            }
        }


        void loadDataVehicleEarly()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);

            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetEmployeeBussinessVehicle"
                                           , new string[] { "@Month", "@Year", "@DepartmentID", "@EmployeeID", "@Keyword" }
                                           , new object[] { TextUtils.ToInt(txtMonth.Value), TextUtils.ToInt(txtYear.Value), departmentID, employeeID, txtKeyword.Text.Trim() });


            DataTable dtEarly = dataSet.Tables[0];
            DataTable dtVehicle = dataSet.Tables[1];

            grdDataEarly.DataSource = dtEarly;
            grdDataVehicle.DataSource = dtVehicle;

            bandTitleEarly.Caption = $"BẢNG CHI TIẾT DI CHUYỂN TRƯỚC 7H15 T{txtMonth.Value}.{txtYear.Value}";
            bandTitleVehicle.Caption = $"TIỀN XE ĐI CÔNG TÁC/ CHI PHÍ PHÁT SINH CỦA NHÂN VIÊN THÁNG {txtMonth.Value}.{txtYear.Value}";

        }


        void loadDepartment()
        {
            List<DepartmentModel> listDepartment = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = listDepartment;
        }

        void loadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
            //loadDataVehicleEarly();

        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"BaoCaoCongTac_T{txtMonth.Text}_{txtYear.Value}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"BaoCaoCongTac_T{txtMonth.Text}_{txtYear.Value}.xlsx");
                string filepath = f.FileName;
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                PrintableComponentLink printableComponentLink2 = new PrintableComponentLink(printingSystem);
                printableComponentLink2.Component = grdDataEarly;

                PrintableComponentLink printableComponentLink3 = new PrintableComponentLink(printingSystem);
                printableComponentLink3.Component = grdDataVehicle;

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);
                        compositeLink.Links.Add(printableComponentLink2);
                        compositeLink.Links.Add(printableComponentLink3);

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
            e.SheetName = e.Index == 0 ? $"Công tác" : (e.Index == 1 ? $"Đi làm sớm" : $"Tiền xe đi CT");
        }

        private void loadWeekDays()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetDayOfWeek", "A"
                , new string[] { "@Month", "@Year" }
                , new object[] { TextUtils.ToInt(txtMonth.Value), TextUtils.ToInt(txtYear.Value) });
            List<GridBand> listBand = bandTitle.Children.ToList();

            foreach (var item in listBand)
            {
                string value = TextUtils.ToString(dt.Rows[0][$"D{item.Index + 1}"]);
                string caption = value.Substring(0, value.LastIndexOf(";"));
                int status = TextUtils.ToInt(value.Substring(value.LastIndexOf(";") + 1));
                item.Caption = caption;
                item.OptionsBand.AllowMove = false;

                item.AppearanceHeader.BackColor = SystemColors.Control;
                item.AppearanceHeader.ForeColor = Color.Black;
                if (status == 1 || status == 7)
                {
                    item.AppearanceHeader.BackColor = Color.FromName("Tan");
                    //item.AppearanceHeader.BackColor = ColorTranslator.FromHtml("#EEECE1");
                }
            }
        }

        private void txtMonth_ValueChanged(object sender, EventArgs e)
        {
            loadData();
            //loadDataVehicleEarly();
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            loadData();
            //loadDataVehicleEarly();
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
            //loadDataVehicleEarly();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
            //loadDataVehicleEarly();
        }

        private void grvDataVehicle_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            //return;
            GridView view = sender as GridView;
            string stt = TextUtils.ToString(view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), "STT"));
            string fileName = TextUtils.ToString(view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), "BillImage"));
            //fileName = "https://i.pinimg.com/736x/3f/8c/ed/3f8cedb77c672df8c4507383d79e202e.jpg";
            if (e.Column == colImageShow && e.IsGetData)
            {
                if (!string.IsNullOrEmpty(fileName) && !fileName.Contains("NoImageUpdate"))
                {
                    try
                    {
                        var request = WebRequest.Create("http://113.190.234.64:8083/api/Upload/BillVehicle/" + fileName);
                        //var request = WebRequest.Create(fileName);
                        var response = request.GetResponse();
                        var stream = response.GetResponseStream();

                        e.Value = Image.FromStream(stream);
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }

        private void grvData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {

        }



        //async Task<Image> GetImage(string fileName)
        //{
        //    Image image = null;
        //    if (string.IsNullOrEmpty(fileName))
        //    {
        //        var request = HttpWebRequest.CreateHttp("http://192.168.1.2:8083/api/Upload/BillVehicle/" + fileName);
        //        var response = request.GetRequestStreamAsync();
        //        var stream = await response.GetAwaiter();

        //        image = Image.FromStream(stream.GetResponseStream());
        //    }
        //    return image;
        //}


    }
}
