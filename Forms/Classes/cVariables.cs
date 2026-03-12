using DevExpress.XtraSplashScreen;
using BMS.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Business;

namespace Forms.Classes
{
    public static class cGlobVar
    {
        public static bool LockEvents = true;
        public static bool Checkrow = true;
        public static SplashScreenManager RTCSplashScreenManager = new SplashScreenManager(null, typeof(WaitForm1), true, true);
        public delegate void SendData(string signal);
        public delegate void SendID(int ID);
        public delegate void SendListID(List<int> ID);

        public static object CourseLessonLog = new
        {
            Code = "Code",
            LessonTitle = "LessonTitle",
            LessonContent = "LessonContent",
            Duration = "Duration",
            VideoURL = "VideoURL",
            CourseID = "CourseID",
            UrlPDF = "UrlPDF",
            LessonCopyID = "LessonCopyID",
            NameCourse = "NameCourse",
            Instructor = "Instructor",
            CourseCatalogID = "CourseCatalogID",
            DeleteFlag = "DeleteFlag",
            FileCourseID = "FileCourseID",
            STT = "STT",
            IsPractice = "IsPractice",
            QuestionCount = "QuestionCount",
            QuestionDuration = "QuestionDuration",
            LeadTime = "LeadTime",
            CourseCopyID = "CourseCopyID",
            CourseTypeID = "CourseTypeID",
            EmployeeID = "EmployeeID"
        };
        
        public static object accountingContract = new
        {
            DateReceived = "Ngày trả hồ sơ gốc",
            QuantityDocument = "Số lượng hồ sơ",
            Company = "Công ty",
            ContractGroup = "Phân loại hợp đồng chính",
            AccountingContractTypeID = "Loại hợp đồng",
            CustomerID = "Khách hàng",
            SupplierSaleID = "Nhà cung cấp",
            ContractNumber = "Số HĐ/PL",
            ContractValue = "Giá trị hợp đồng",
            DateExpired = "Hiệu lực hợp đồng",
            DateIsApprovedGroup = "Ngày duyệt trên nhóm",
            EmployeeID = "Nhân viên phụ trách",
            ParentID = "Thuộc hợp đồng",
            ContractContent = "Nội dung hợp đồng",
            ContentPayment = "Nội dung thanh toán",
            IsReceivedContract = "Nhận chứng từ gốc",
            DateContract = "Ngày hợp đồng",
            Unit = "ĐVT"
        };

        public static object projectItem = new
        {
            TypeProjectItem = "Kiểu",
            UserID = "Người phụ trách",
            EmployeeIDRequest = "Người giao việc",
            PlanStartDate = "Ngày bắt đầu (DỰ KIẾN)",
            PlanEndDate = "Ngày kết thúc (DỰ KIẾN)",
            TotalDayPlan = "Số ngày",
            Mission = "Công việc",
        };

        public static object ponccDetails = new
        {
            /*ProductSaleID = "Mã sản phẩm (Sale)",
            UnitPrice = "Đơn giá",
            ActualDate = "Ngày về thực tế",
            QtyRequest = "Số lượng",
            VAT = "% VAT",
            VATMoney = "Tổng tiền VAT",
            ThanhTien = "Thành tiền",
            TotalPrice = "Tổng tiền",
            ExpectedDate = "Ngày về dự kiến",
            FeeShip = "Phí vận chuyển",
            Note = "Diễn giải",
            PriceSale = "Giá bán",
            CurrencyExchange = "Tổng tiền quy đổi (VNĐ)",
            Discount = "Chiết khấu",
            PriceHistory = "Giá lịch sử",
            ProductCodeOfSupplier = "Mã sản phẩm NCC",
            DiscountPercent = "% Chiết khấu",
            ProjectID = "Mã dự án",
            ProductRTCID = "Mã sản phẩm (KT)",
            BiddingPrice = "Giá chào thầu"*/

            STT = "STT",
            PONCCID = "PONCCID",
            ProductID = "Mã sản phẩm",
            Qty = "Qty",
            UnitPrice = "Đơn giá",
            IntoMoney = "IntoMoney",
            CodeBill = "Mã hoá đơn",
            NameBill = "Tên hoá đơn",
            RequestDate = "RequestDate",
            ActualDate = "Ngày về thực tế",
            RequestBuyRTCID = "RequestBuyRTCID",
            QtyRequest = "Số lượng",
            QtyReal = "QtyReal",
            Soluongcon = "Soluongcon",
            Price = "Price",
            VAT = "% VAT",
            VATMoney = "Tổng tiền VAT",
            ThanhTien = "Thành tiền",
            TotalPrice = "Tổng tiền",
            OrderDate = "OrderDate",
            ExpectedDate = "Ngày về dự kiến",
            FeeShip = "Phí vận chuyển",
            Note = "Diễn giải",
            PriceSale = "Giá bán",
            CurrencyExchange = "Tổng tiền quy đổi (VNĐ)",
            Discount = "Chiết khấu",
            ProfitRate = "ProfitRate",
            PriceHistory = "Giá lịch sử",
            ProductCodeOfSupplier = "Mã sản phẩm NCC",
            Status = "Status",
            ProjectPartlistPurchaseRequestID = "ProjectPartlistPurchaseRequestID",
            DiscountPercent = "% Chiết khấu",
            BiddingPrice = "Giá chào thầu",
            ProductSaleID = "Mã sản phẩm (Sale)",
            ProjectID = "Mã dự án",
            ProductRTCID = "Mã sản phẩm (KT)",
            ProjectName = "Tên dự án",
            DeadlineDelivery = "Deadline giao hàng",
            ProjectPartListID = "ProjectPartListID",
            IsBill = "IsBill",
            ProductType = "ProductType",
            DateReturnEstimated= "DateReturnEstimated",
            IsStock = "IsStock",
            UnitName = "UnitName",
            ParentProductCode = "ParentProductCode",
            IsPurchase = "IsPurchase",
        };

        public static class StoreProcedure
        {
            public static string spGetHoliday = "spGetHoliday";
            public static string spGetListProductImportExportByProjectID = "spGetListProductImportExportByProjectID";
            public static string spGetListProductImportExportByProjectID_New = "spGetListProductImportExportByProjectID_New";
            public static string spCalculateImport_New = "spCalculateImport_New";
            public static string spCalculateExport_New = "spCalculateExport_New";
            public static string usp_LoadBill_New = "usp_LoadBill_New";
            public static string spLoadDataXuatNote_New = "spLoadDataXuatNote_New";
            public static string spGetInventory = "spGetInventory";
            public static string spGetHistoryImportExportInventory = "spGetHistoryImportExportInventory";
            public static string spGetProductImportExportSale = "spGetProductImportExportSale";
            public static string spGetProductExportSale = "spGetProductExportSale";
            public static string spGetProductImportSale = "spGetProductImportSale";
        }

        public class Response
        {
            public int status { get; set; }
            public string message { get; set; }
        }


        public static int ProjectID;

        public class EmployeeDeclineApprove
        {
            public int ID { get; set; }
            public string TableName { get; set; }
        }

        public class DailyReportHCNSIT
        {
            public string FullName { get; set; }
            public string PositionName { get; set; }
            public DateTime? DateReport { get; set; }
            public string Content { get; set; }
            public string Results { get; set; }
            public string PlanNextDay { get; set; }
            public string Backlog { get; set; }
            public string Problem { get; set; }
            public string ProblemSolve { get; set; }
            public string Note { get; set; }
            public DateTime? CreatedDate { get; set; }
        }

        public class DailyReportHR
        {
            public int ID { get; set; }
            public int EmployeeID { get; set; }
            public DateTime? DateReport { get; set; }
            public int FilmManagementDetailID { get; set; }
            public int Quantity { get; set; }
            public decimal TimeActual { get; set; }
            public decimal PerformanceActual { get; set; }
            public decimal Percentage { get; set; }
            public decimal KmNumber { get; set; }
            public int TotalLate { get; set; }
            public decimal TotalTimeLate { get; set; }
            public string ReasonLate { get; set; }
            public string StatusVehicle { get; set; }
            public string Propose { get; set; }
            public DateTime? CreatedDate { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? UpdatedDate { get; set; }
            public string UpdatedBy { get; set; }
            public decimal PerformanceAVG { get; set; }
            public string WorkContent { get; set; }
            public string FilmName { get; set; }
            public string UnitName { get; set; }
            public string FullName { get; set; }
            public int ChucVuHDID { get; set; }
        }

        public class FileUpdate
        {
            public string Name { get; set; }
            public string Path { get; set; }
        }

        public class ReponseFileUpdate
        {
            public int Status { get; set; }
            public string NewVersion { get; set; }
            public List<FileUpdate> Data { get; set; }
        }
    }
}
