using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ProjectPartlistPurchaseRequestNoteModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? ProjectPartlistPurchaseRequestID { get; set; } // int, null
        public string Note { get; set; } // nvarchar(max), null
        public string CreatedBy { get; set; } // nvarchar(50), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(50), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum ProjectPartlistPurchaseRequestNoteModel_Enum{
        ID,
        ProjectPartlistPurchaseRequestID,
        Note,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
