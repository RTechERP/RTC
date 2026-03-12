using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIExamModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? KPISessionID { get; set; } // int, null
        public string ExamCode { get; set; } // varchar(150), null
        public string ExamName { get; set; } // nvarchar(250), null
        public bool? IsDeleted { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsActive { get; set; } // bit, null
        public DateTime? Deadline { get; set; } // datetime, null
    }
    public enum KPIExamModel_Enum{
        ID,
        KPISessionID,
        ExamCode,
        ExamName,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsActive,
        Deadline,
        }
}
