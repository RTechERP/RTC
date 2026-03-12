using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class DocumentModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string Code { get; set; } // varchar(50), null
        public int? STT { get; set; } // int, null
        public string NameDocument { get; set; } // nvarchar(200), null
        public DateTime? DatePromulgate { get; set; } // datetime, null
        public DateTime? DateEffective { get; set; } // datetime, null
        public int? DocumentTypeID { get; set; } // int, null
        public int? DepartmentID { get; set; } // int, null
        /// <summary>
        /// 1: Phòng HR; 2: Admin Sale
        /// </summary>
        public int? GroupType { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(50), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(50), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public int? SignedEmployeeID { get; set; } // int, null
        public string AffectedScope { get; set; } // nvarchar(550), null
        public bool? IsOnWeb { get; set; } // bit, null
        public bool? IsPromulgated { get; set; } // bit, null
    }
    public enum DocumentModel_Enum
    {
        ID,
        Code,
        STT,
        NameDocument,
        DatePromulgate,
        DateEffective,
        DocumentTypeID,
        DepartmentID,
        GroupType,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        SignedEmployeeID,
        AffectedScope,
        IsOnWeb,
        IsPromulgated,
    }
}
