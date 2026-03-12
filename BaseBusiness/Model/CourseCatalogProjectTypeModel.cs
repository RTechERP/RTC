using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class CourseCatalogProjectTypeModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? CourseCatalogID { get; set; } // int, null
        public int? ProjectTypeID { get; set; } // int, null
        public string CreatedBy { get; set; } // varchar(50), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // varchar(50), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum CourseCatalogProjectTypeModel_Enum{
        ID,
        CourseCatalogID,
        ProjectTypeID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
