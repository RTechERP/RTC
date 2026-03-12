using BaseBusiness.DTO;
using BMS;
using BMS.Model;
using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
namespace Forms.Sale.HandoverMinutes
{
    //----------Ninh Duy Nhật 12/02/2025----------------
    public partial class frmHandoverMinutes : _Forms
    {
        List<HandoverMinutesModel> _handoverMinutes = new List<HandoverMinutesModel>();
        int handoverMinutesID;
        
        public frmHandoverMinutes()
        {
            DateTime currentDate = DateTime.Now;
            InitializeComponent();
            dpkDateStart.Value = new DateTime(currentDate.Year, currentDate.Month, 1);
            dpkDateEnd.Value = dpkDateStart.Value.AddMonths(1).AddDays(-1);
            LoadHandoverMinutes();
            
        }

        private void frmHandoverMinutes_Load(object sender, EventArgs e)
        {
            LoadHandoverMinutes();
            //LoadHandoverMinutesDetails();
   
        }

        private void LoadHandoverMinutes()
        {
            DateTime dateStart = new DateTime(dpkDateStart.Value.Year, dpkDateStart.Value.Month, dpkDateStart.Value.Day, 00, 00, 00);
            DateTime dateEnd = new DateTime(dpkDateEnd.Value.Year, dpkDateEnd.Value.Month, dpkDateEnd.Value.Day, 23, 59, 59);
            string keyWords = TextUtils.ToString(txtKeywords.Text.Trim());
            //DataTable dt = TextUtils.GetTable("spGetAllHandoverMinutes");
            DataTable dt = TextUtils.LoadDataFromSP("spGetAllHandoverMinutes", "HandoverMinutes", new string[] { "@DateStart", "@DateEnd", "@KeyWords" }, new object[] { dateStart, dateEnd, keyWords });
            grdHandoverMinutes.DataSource = null;  // Đặt về null trước khi gán mới
            grdHandoverMinutes.DataSource = dt;
            grdHandoverMinutes.RefreshDataSource();

        }
        private void LoadHandoverMinutesDetails()
        {
             handoverMinutesID = TextUtils.ToInt(grvHandoverMinutes.GetFocusedRowCellValue("ID"));
            DataTable dt = TextUtils.LoadDataFromSP("spGetHanoverMinutesDetail", "HandoverMinutesDetail", new string[] { "@HandoverMinutesID" }, new object[] { handoverMinutesID });
            grdhandoverMinutesDetail.DataSource = dt;
        }

        public void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmHandoverMinutesDetail frm = new frmHandoverMinutesDetail();
            frm.SaveEvent += SaveEventCallBack;
            frm.Show();
            
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int id = TextUtils.ToInt(grvHandoverMinutes.GetFocusedRowCellValue(colID));
            HandoverMinutesModel model = SQLHelper<HandoverMinutesModel>.FindByID(id);
            if (model.ID <= 0)
            {
                MessageBox.Show($"Không tìm thấy biên bản bàn giao!", "Thông báo");
                return;
            }

            frmHandoverMinutesDetail frm = new frmHandoverMinutesDetail();
            frm.HandoverMinutesModel = model;
            frm.SaveEvent += SaveEventCallBack;
            frm.Show();
        }


        private void grvHandoverMinutes_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadHandoverMinutesDetails();
        }
        Task SaveEventCallBack(HandoverMinutesModel arg)
        {
            this.Invoke(new Action(() =>
            {
                HandoverMinutesModel existingModel = _handoverMinutes.FirstOrDefault(p => p.ID == arg.ID);
                if (existingModel == null)
                {
                    _handoverMinutes.Add(arg);
                }
                else
                {
                    existingModel.DateMinutes = arg.DateMinutes;
                    existingModel.CustomerID = arg.CustomerID;
                    existingModel.CustomerAddress = arg.CustomerAddress;
                    existingModel.CustomerContact = arg.CustomerContact;
                    existingModel.CustomerPhone = arg.CustomerPhone;
                    existingModel.EmployeeID = arg.EmployeeID;
                    existingModel.Receiver = arg.Receiver;
                    existingModel.ReceiverPhone = arg.ReceiverPhone;
                    existingModel.AdminWarehouseID = arg.AdminWarehouseID;
                }

                LoadHandoverMinutes();
            }));
            return Task.CompletedTask;
        }


        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int handoverMinutesModelID = TextUtils.ToInt(grvHandoverMinutes.GetFocusedRowCellValue("ID"));

            if (handoverMinutesModelID <= 0) 
            {
                TextUtils.ShowError("hãy chọn biên bản bàn giao để xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc là muốn xóa biên bản bàn giao này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Dictionary<string, object> param = new Dictionary<string, object>
                    {
                        { "IsDeleted", 1 }  
                    };

                    
                    SQLHelper<HandoverMinutesModel>.UpdateFieldsByID(param, handoverMinutesModelID);
                    
                    MessageBox.Show("Đã xóa biên bản bàn giao được chọn!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    TextUtils.ShowError(ex.Message);
                }
            }
            grdHandoverMinutes.RefreshDataSource();
            LoadHandoverMinutes(); 
        }
        private string GetProductStatusDescription(int status)
        {
            switch (status)
            {
                case 1:
                    return "Mới";
                case 2:
                    return "Cũ";
                default:
                    return "Không xác định";
            }
        }

        private string GetDeliveryStatusDescription(int status)
        {
            switch (status)
            {
                case 1:
                    return "Nhận đủ";
                case 2:
                    return "Thiếu";
                default:
                    return "Không xác định";
            }
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<string> collumnsNeed = new List<string>
            {
                "Code", "DateMinutes", "CustomerName", "CustomerAddress",
                "CustomerContact", "CustomerPhone", "FullName", "DepartmentName", "EmailCaNhan", "SDTCaNhan","AdminWarehouseName","Receiver","ReceiverPhone"
            };
            //đường dẫn mẫu excel
            string templatePath = @"C:\RTC\Project\RTC\TrainingProject\RTC\BMS\bin\Debug\BBBGTemplate.xlsx";

            // Khởi tạo ứng dụng Excel
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;

            Excel.Workbook workbook = excelApp.Workbooks.Open(templatePath);
            Excel.Worksheet sheet = workbook.Sheets[1]; // Chọn sheet đầu tiên

            try
            {
                // Mở SaveFileDialog để người dùng chọn đường dẫn và tên file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveFileDialog.Title = $"Lưu {this.Text}";
                saveFileDialog.FileName = $"{TextUtils.ToString(grvHandoverMinutes.GetFocusedRowCellValue(collumnsNeed[0]))}_{DateTime.Now.ToString("yy-MM-dd")}.xlsx"; // Tên mặc định của file

                // Kiểm tra xem người dùng có chọn file không
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string savePath = saveFileDialog.FileName; // Đường dẫn file mà người dùng chọn
                    string employeeFullname = TextUtils.ToString(grvHandoverMinutes.GetFocusedRowCellValue(collumnsNeed[6]));
                    DateTime dateMinutes = TextUtils.ToDate4(grvHandoverMinutes.GetFocusedRowCellValue(collumnsNeed[1])) ?? DateTime.Now;
                    string formattedDate = $"Hà Nội, ngày {dateMinutes:dd}, tháng {dateMinutes:MM}, năm {dateMinutes:yyyy}";
                    sheet.Cells[14, 2] = formattedDate; // B14
                    sheet.Cells[16, 3] = TextUtils.ToString(grvHandoverMinutes.GetFocusedRowCellValue(collumnsNeed[2]));// C16
                    sheet.Cells[17, 3] = TextUtils.ToString(grvHandoverMinutes.GetFocusedRowCellValue(collumnsNeed[3]));// C17
                    sheet.Cells[18, 3] = TextUtils.ToString(grvHandoverMinutes.GetFocusedRowCellValue(collumnsNeed[4])); // C18
                    sheet.Cells[19, 3] = "'" + TextUtils.ToString(grvHandoverMinutes.GetFocusedRowCellValue(collumnsNeed[5]));// C19
                    sheet.Cells[16, 9] = employeeFullname; // I16
                    sheet.Cells[17, 9] = TextUtils.ToString(grvHandoverMinutes.GetFocusedRowCellValue(collumnsNeed[7])); // I17
                    sheet.Cells[18, 9] = TextUtils.ToString(grvHandoverMinutes.GetFocusedRowCellValue(collumnsNeed[8])); // I18
                    sheet.Cells[19, 9] = "'" + TextUtils.ToString(grvHandoverMinutes.GetFocusedRowCellValue(collumnsNeed[9])); // I19

                    // Lấy dữ liệu trực tiếp từ GridControl
                    var gridView = grvHandoverMinutesDetail;
                    int rowCount = gridView.DataRowCount;

                    // Nếu không có dữ liệu, thoát sớm
                    if (rowCount == 0)
                    {
                        MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int startRow = 22; // Bắt đầu từ dòng 22
                    int startColumn = 3; // Cột C (Excel)

                    // Chỉ lấy các cột cần thiết
                    List<string> allowedColumns = new List<string>
                    {
                        "POCode", "ProductName", "ProductCode", "Maker",
                        "Quantity", "Unit", "ProductStatus", "Guarantee", "DeliveryStatus"
                    };

                    // Lọc danh sách cột dựa trên danh sách 
                    List<GridColumn> selectedColumns = grvHandoverMinutesDetail.Columns
                        .Where(col => allowedColumns.Contains(col.FieldName))
                        .ToList();

                    // Nếu có nhiều dòng dữ liệu thì chèn thêm dòng trống
                    if (rowCount > 1)
                    {
                        for (int i = 1; i < rowCount; i++)
                        {
                            Excel.Range range = sheet.Rows[startRow + i];
                            range.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Excel.XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
                        }
                    }

                    // Ghi dữ liệu vào Excel (điền STT trước)
                    for (int i = 0; i < rowCount; i++)
                    {
                        sheet.Cells[startRow + i, startColumn - 1] = i + 1; // Cột B: STT

                        for (int j = 0; j < selectedColumns.Count; j++)
                        {
                            string fieldName = selectedColumns[j].FieldName;
                            string cellValue = gridView.GetRowCellValue(i, fieldName)?.ToString();

                            // Chuyển đổi ProductStatus và DeliveryStatus
                            if (fieldName == "ProductStatus")
                            {
                                sheet.Cells[startRow + i, startColumn + j] = GetProductStatusDescription(int.Parse(cellValue));
                            }
                            else if (fieldName == "DeliveryStatus")
                            {
                                sheet.Cells[startRow + i, startColumn + j] = GetDeliveryStatusDescription(int.Parse(cellValue));
                            }
                            else
                            {
                                sheet.Cells[startRow + i, startColumn + j] = cellValue;
                            }

                            // Căn chỉnh text và số
                            Excel.Range cell = sheet.Cells[startRow + i, startColumn + j];
                            if (!double.TryParse(cellValue, out _))
                            {
                                cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft; // Căn trái cho text
                            }

                            cell.Font.Bold = false;
                        }
                    }

                    // Xác định vị trí dòng cuối cùng sau khi nhập dữ liệu
                    int finalRow = startRow + rowCount; // Dòng tiếp theo sau dữ liệu
                    sheet.Cells[finalRow + 8, 4] = TextUtils.ToString(grvHandoverMinutes.GetFocusedRowCellValue(collumnsNeed[10]));
                    sheet.Cells[finalRow + 8, 6] = employeeFullname;
                    string receiver = TextUtils.ToString(grvHandoverMinutes.GetFocusedRowCellValue(collumnsNeed[11])) + "-" + TextUtils.ToString(grvHandoverMinutes.GetFocusedRowCellValue(collumnsNeed[12]));
                    sheet.Cells[finalRow + 8, 11] = receiver;

                    Excel.Range rangeToDelete = sheet.Rows[finalRow];
                    rangeToDelete.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);

                    // Lưu file ngay lập tức
                    workbook.SaveAs(savePath);

                    // Đóng Workbook và Excel
                    workbook.Close(false);
                    excelApp.Quit();

                    // Giải phóng bộ nhớ
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                    // Hiển thị thông báo và mở file ngay sau khi nhấn OK
                    MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = savePath,
                        UseShellExecute = true // Mở bằng ứng dụng mặc định
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
           LoadHandoverMinutes();
        }

        private void grvHandoverMinutesDetail_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            // Kiểm tra nếu là cột ProductStatus
            if (e.Column.FieldName == "ProductStatus")
            {
                if (e.Value != null)
                {
                    int productStatus = Convert.ToInt32(e.Value);
                    e.DisplayText = productStatus == 1 ? "Hàng mới" : (productStatus == 2 ? "Hàng cũ" : string.Empty);
                }
            }

            // Kiểm tra nếu là cột DeliveryStatus
            if (e.Column.FieldName == "DeliveryStatus")
            {
                if (e.Value != null)
                {
                    int deliveryStatus = Convert.ToInt32(e.Value);
                    e.DisplayText = deliveryStatus == 1 ? "Nhận đủ" : (deliveryStatus == 2 ? "Thiếu" : string.Empty);
                }
            }
        }

        private void frmHandoverMinutes_Load_1(object sender, EventArgs e)
        {

        }
    }

}