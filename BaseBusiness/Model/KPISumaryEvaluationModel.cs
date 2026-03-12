using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPISumaryEvaluationModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? EmployeeID { get; set; } // int, null
        public int? KPIExamID { get; set; } // int, null
        public int? SpecializationType { get; set; } // int, null
        public decimal? EmployeePoint { get; set; } // decimal(18,2), null
        public decimal? TBPPoint { get; set; } // decimal(18,2), null
        public decimal? BGDPoint { get; set; } // decimal(18,2), null
    }
    public enum KPISumaryEvaluationModel_Enum{
        ID,
        EmployeeID,
        KPIExamID,
        SpecializationType,
        EmployeePoint,
        TBPPoint,
        BGDPoint,
        }
}
