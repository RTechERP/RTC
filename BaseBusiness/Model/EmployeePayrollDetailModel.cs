using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class EmployeePayrollDetailModel : BaseModel
    {
        public int ID { get; set; } // int
        public int PayrollID { get; set; } // int
        public int STT { get; set; } // int
        public int EmployeeID { get; set; } // int
        public decimal BasicSalary { get; set; } // decimal(18,2)
        /// <summary>
        /// Tổng công tiêu chuẩn
        /// </summary>
        public decimal TotalWorkday { get; set; } // decimal(18,2)
        /// <summary>
        /// Tổng công được tính (Bao gồm công thực tế và phép)
        /// </summary>
        public decimal TotalMerit { get; set; } // decimal(18,2)
        /// <summary>
        /// Tổng lương theo ngày công 
        /// </summary>
        public decimal TotalSalaryByDay { get; set; } // decimal(18,2)
        /// <summary>
        /// Tính tiền công 1h
        /// </summary>
        public decimal SalaryOneHour { get; set; } // decimal(18,2)
        /// <summary>
        ///  Số giờ làm thêm ngày thường  
        /// </summary>
        public decimal OT_Hour_WD { get; set; } // decimal(18,2)
        /// <summary>
        /// Số tiền làm thêm giờ (Số giờ * 1,5 * Tiền công 1h)
        /// </summary>
        public decimal OT_Money_WD { get; set; } // decimal(18,2)
        /// <summary>
        /// Số giờ làm thêm ngày thứ 7, CN
        /// </summary>
        public decimal OT_Hour_WK { get; set; } // decimal(18,2)
        /// <summary>
        /// Số tiền làm thêm giờ chiều T7, ngày CN (Số giờ * 2 * Công 1h)
        /// </summary>
        public decimal OT_Money_WK { get; set; } // decimal(18,2)
        /// <summary>
        /// Số giờ làm thêm ngày Lễ Tết
        /// </summary>
        public decimal OT_Hour_HD { get; set; } // decimal(18,2)
        /// <summary>
        /// Số tiền làm thêm giờ ngày Lễ Tết (Số giờ * 3 * Công 1h)
        /// </summary>
        public decimal OT_Money_HD { get; set; } // decimal(18,2)
        /// <summary>
        /// Phụ cấp chuyên cần thực nhận
        /// </summary>
        public decimal RealIndustry { get; set; } // decimal(18,2)
        /// <summary>
        /// Phụ cấp cơm ca
        /// </summary>
        public decimal AllowanceMeal { get; set; } // decimal(18,2)
        /// <summary>
        /// Phụ cấp làm thêm trước 7H15 
        /// </summary>
        public decimal Allowance_OT_Early { get; set; } // decimal(18,2)
        /// <summary>
        /// Tiền công tác phí
        /// </summary>
        public decimal BussinessMoney { get; set; } // decimal(18,2)
        /// <summary>
        /// Tiền công làm đêm
        /// </summary>
        public decimal NightShiftMoney { get; set; } // decimal(18,2)
        /// <summary>
        /// Chi phí phương tiện công tác
        /// </summary>
        public decimal CostVehicleBussiness { get; set; } // decimal(18,2)
        /// <summary>
        /// "Thưởng KPIs/doanh số"
        /// </summary>
        public decimal Bonus { get; set; } // decimal(18,2)
        /// <summary>
        /// khoản công khác
        /// </summary>
        public decimal Other { get; set; } // decimal(18,2)
        /// <summary>
        /// "Tổng thu nhập thực tế"
        /// </summary>
        public decimal RealSalary { get; set; } // decimal(18,2)
        /// <summary>
        /// BHXH, BHYT, BHTN (10,5%)
        /// </summary>
        public decimal Insurances { get; set; } // decimal(18,2)
        /// <summary>
        /// Công đoàn (1% * lương đóng bảo hiểm)
        /// </summary>
        public decimal UnionFees { get; set; } // decimal(18,2)
        /// <summary>
        /// Ứng trước lương
        /// </summary>
        public decimal AdvancePayment { get; set; } // decimal(18,2)
        /// <summary>
        /// Thu hộ phòng ban
        /// </summary>
        public decimal DepartmentalFees { get; set; } // decimal(18,2)
        /// <summary>
        /// Tiền gửi xe ô tô
        /// </summary>
        public decimal ParkingMoney { get; set; } // decimal(18,2)
        /// <summary>
        /// Phạt 5S
        /// </summary>
        public decimal Punish5S { get; set; } // decimal(18,2)
        /// <summary>
        /// Các khoản phải trừ khác
        /// </summary>
        public decimal OtherDeduction { get; set; } // decimal(18,2)
        /// <summary>
        /// Thực lĩnh
        /// </summary>
        public decimal ActualAmountReceived { get; set; } // decimal(18,2)
        /// <summary>
        /// Ký
        /// </summary>
        public bool Sign { get; set; } // bit
        /// <summary>
        /// Nhận tiền mặt(true là có, false là không)
        /// </summary>
        public bool GetCash { get; set; } // bit
        /// <summary>
        /// 1: hiển thị trên web cho nhân viên xem; 0: Không show trên web
        /// </summary>
        public bool IsPublish { get; set; } // bit
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; } // nvarchar(1500)
        public DateTime CreatedDate { get; set; } // datetime
        public string CreatedBy { get; set; } // nvarchar(150)
        public DateTime UpdatedDate { get; set; } // datetime
        public string UpdatedBy { get; set; } // nvarchar(150)
        public int MealUse { get; set; } // int
    }
    public enum EmployeePayrollDetailModel_Enum{
        ID,
        PayrollID,
        STT,
        EmployeeID,
        BasicSalary,
        TotalWorkday,
        TotalMerit,
        TotalSalaryByDay,
        SalaryOneHour,
        OT_Hour_WD,
        OT_Money_WD,
        OT_Hour_WK,
        OT_Money_WK,
        OT_Hour_HD,
        OT_Money_HD,
        RealIndustry,
        AllowanceMeal,
        Allowance_OT_Early,
        BussinessMoney,
        NightShiftMoney,
        CostVehicleBussiness,
        Bonus,
        Other,
        RealSalary,
        Insurances,
        UnionFees,
        AdvancePayment,
        DepartmentalFees,
        ParkingMoney,
        Punish5S,
        OtherDeduction,
        ActualAmountReceived,
        Sign,
        GetCash,
        IsPublish,
        Note,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        MealUse,
        }
}
