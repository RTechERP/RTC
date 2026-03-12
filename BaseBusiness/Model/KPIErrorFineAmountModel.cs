using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIErrorFineAmountModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? KPIErrorID { get; set; } // int, null
        public int? QuantityError { get; set; } // int, null
        public decimal? TotalMoneyError { get; set; } // decimal(18,0), null
        public string Note { get; set; } // nvarchar(max), null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum KPIErrorFineAmountModel_Enum{
        ID,
        KPIErrorID,
        QuantityError,
        TotalMoneyError,
        Note,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
