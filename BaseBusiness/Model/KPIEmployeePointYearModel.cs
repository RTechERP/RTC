using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIEmployeePointYearModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? YearValue { get; set; } // int, null
        public int? EmployeeID { get; set; } // int, null
        public decimal? PointPercentYear { get; set; } // decimal(18,2), null
        public int? IsApproveYear { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum KPIEmployeePointYearModel_Enum{
        ID,
        YearValue,
        EmployeeID,
        PointPercentYear,
        IsApproveYear,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
