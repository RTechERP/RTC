using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIEvaluationModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string EvaluationCode { get; set; } // nvarchar(250), null
        public string Note { get; set; } // nvarchar(max), null
        public bool? IsDeleted { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public int? DepartmentID { get; set; } // int, null
    }
    public enum KPIEvaluationModel_Enum{
        ID,
        EvaluationCode,
        Note,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        DepartmentID,
        }
}
