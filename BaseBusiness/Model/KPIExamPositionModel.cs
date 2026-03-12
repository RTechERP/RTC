using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIExamPositionModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? KPIExamID { get; set; } // int, null
        public int? KPIPositionID { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsDeleted { get; set; } // bit, null
    }
    public enum KPIExamPositionModel_Enum{
        ID,
        KPIExamID,
        KPIPositionID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsDeleted,
        }
}
