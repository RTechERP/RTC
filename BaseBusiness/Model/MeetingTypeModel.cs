using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class MeetingTypeModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        /// <summary>
        /// 1: Nội bộ; 2: Khách hàng
        /// </summary>
        public int? GroupID { get; set; } // int, null
        public string TypeCode { get; set; } // nvarchar(150), null
        public string TypeName { get; set; } // nvarchar(250), null
        public string TypeContent { get; set; } // nvarchar(max), null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsDelete { get; set; } // bit, null
    }
    public enum MeetingTypeModel_Enum{
        ID,
        GroupID,
        TypeCode,
        TypeName,
        TypeContent,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsDelete,
        }
}
