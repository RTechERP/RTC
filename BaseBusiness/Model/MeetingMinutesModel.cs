using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class MeetingMinutesModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? ProjectID { get; set; } // int, null
        public int? MeetingTypeID { get; set; } // int, null
        public DateTime? DateStart { get; set; } // datetime, null
        public DateTime? DateEnd { get; set; } // datetime, null
        public string Title { get; set; } // nvarchar(550), null
        public string Place { get; set; } // nvarchar(550), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public bool? IsDeleted { get; set; } // bit, null
        public int? CreatorID { get; set; } // int, null
    }
    public enum MeetingMinutesModel_Enum{
        ID,
        ProjectID,
        MeetingTypeID,
        DateStart,
        DateEnd,
        Title,
        Place,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        IsDeleted,
        CreatorID,
        }
}
