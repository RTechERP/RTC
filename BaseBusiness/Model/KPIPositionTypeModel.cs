using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIPositionTypeModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? STT { get; set; } // int, null
        public string TypeCode { get; set; } // varchar(50), null
        public string TypeName { get; set; } // nvarchar(550), null
        public int? YearValue { get; set; } // int, null
        public int? QuaterValue { get; set; } // int, null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(50), null
        public string UpdatedBy { get; set; } // nvarchar(50), null
        public bool? IsDeleted { get; set; } // bit, null
        public int? ProjectTypeID { get; set; } // int, null
    }
    public enum KPIPositionTypeModel_Enum{
        ID,
        STT,
        TypeCode,
        TypeName,
        YearValue,
        QuaterValue,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        IsDeleted,
        ProjectTypeID,
        }
}
