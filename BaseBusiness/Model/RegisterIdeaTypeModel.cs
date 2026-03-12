using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class RegisterIdeaTypeModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? STT { get; set; } // int, null
        public string RegisterTypeName { get; set; } // nvarchar(max), null
        public string Note { get; set; } // nvarchar(max), null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsDeleted { get; set; } // bit, null
        public string RegisterTypeCode { get; set; } // nvarchar(100), null
    }
    public enum RegisterIdeaTypeModel_Enum{
        ID,
        STT,
        RegisterTypeName,
        Note,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsDeleted,
        RegisterTypeCode,
        }
}
