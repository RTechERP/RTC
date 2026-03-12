using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIPositionModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string PositionCode { get; set; } // nvarchar(250), null
        public string PositionName { get; set; } // nvarchar(250), null
        public bool? IsDeleted { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public int? STT { get; set; } // int, null
        public int? TypePosition { get; set; } // int, null
        public int? KPISessionID { get; set; } // int, null
        public int? KPIPositionTypeID { get; set; } // int, null
    }
    public enum KPIPositionModel_Enum{
        ID,
        PositionCode,
        PositionName,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        STT,
        TypePosition,
        KPISessionID,
        KPIPositionTypeID,
        }
}
