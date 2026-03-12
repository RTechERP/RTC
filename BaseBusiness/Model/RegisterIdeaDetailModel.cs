using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class RegisterIdeaDetailModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? RegisterIdeaID { get; set; } // int, null
        public int? STT { get; set; } // int, null
        public string Category { get; set; } // nvarchar(max), null
        public string Description { get; set; } // nvarchar(max), null
        public string Note { get; set; } // nvarchar(max), null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public DateTime? DateStart { get; set; } // datetime, null
        public DateTime? DateEnd { get; set; } // datetime, null
    }
    public enum RegisterIdeaDetailModel_Enum{
        ID,
        RegisterIdeaID,
        STT,
        Category,
        Description,
        Note,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        DateStart,
        DateEnd,
        }
}
