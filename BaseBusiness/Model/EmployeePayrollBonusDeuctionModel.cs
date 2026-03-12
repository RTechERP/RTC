
using System;
namespace BMS.Model
{
    public partial class EmployeePayrollBonusDeuctionModel : BaseModel
    {
        public int ID { get; set; }

        public int EmployeeID { get; set; }

        public int YearValue { get; set; }

        public int MonthValue { get; set; }

        public decimal KPIBonus { get; set; }

        public decimal OtherBonus { get; set; }

        public decimal ParkingMoney { get; set; }

        public decimal Punish5S { get; set; }

        public decimal OtherDeduction { get; set; }

        public decimal BHXH { get; set; }

        public decimal SalaryAdvance { get; set; }

        public string Note { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
        public decimal Insurances { get; set; }
        public decimal TotalWorkDay { get; set; }

    }
}
