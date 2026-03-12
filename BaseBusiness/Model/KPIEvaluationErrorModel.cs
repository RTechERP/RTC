using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIEvaluationErrorModel : BaseModel
    {
        public int ID { get; set; } // int
        public int? KPIEvaluationID { get; set; } // int
        public int? KPIErrorID { get; set; } // int
        public string CreatedBy { get; set; } // nvarchar(150)
        public DateTime? CreatedDate { get; set; } // datetime
        public string UpdatedBy { get; set; } // nvarchar(150)
        public DateTime? UpdatedDate { get; set; } // datetime
    }
    public enum KPIEvaluationErrorModel_Enum{
        ID,
        KPIEvaluationID,
        KPIErrorID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
