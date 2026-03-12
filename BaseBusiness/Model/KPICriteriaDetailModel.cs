using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPICriteriaDetailModel : BaseModel
    {
        public int ID { get; set; } // int
        public int? KPICriteriaID { get; set; } // int
        public int? STT { get; set; } // int
        public int? Point { get; set; } // int
        public int? PointPercent { get; set; } // int
        public string CriteriaContent { get; set; } // nvarchar(max)
        public string CreatedBy { get; set; } // varchar(20)
        public DateTime? CreatedDate { get; set; } // datetime
        public string UpdatedBy { get; set; } // varchar(20)
        public DateTime? UpdatedDate { get; set; } // datetime
    }
    public enum KPICriteriaDetailModel_Enum{
        ID,
        KPICriteriaID,
        STT,
        Point,
        PointPercent,
        CriteriaContent,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
