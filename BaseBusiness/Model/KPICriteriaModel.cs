using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPICriteriaModel : BaseModel
    {
        public int ID { get; set; } // int
        public int? STT { get; set; } // int
        public string CriteriaCode { get; set; } // nvarchar(150)
        public string CriteriaName { get; set; } // nvarchar(250)
        public int? KPICriteriaQuater { get; set; } // int
        public int? KPICriteriaYear { get; set; } // int
        public bool? IsDeleted { get; set; } // bit
        public string CreatedBy { get; set; } // varchar(20)
        public DateTime? CreatedDate { get; set; } // datetime
        public string UpdatedBy { get; set; } // varchar(20)
        public DateTime? UpdatedDate { get; set; } // datetime
    }
    public enum KPICriteriaModel_Enum{
        ID,
        STT,
        CriteriaCode,
        CriteriaName,
        KPICriteriaQuater,
        KPICriteriaYear,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
