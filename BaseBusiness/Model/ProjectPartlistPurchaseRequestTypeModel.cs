using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ProjectPartlistPurchaseRequestTypeModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string RequestTypeName { get; set; } // nvarchar(50), null
        public bool? IsIgnoreBGD { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(50), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(50), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string RequestTypeCode { get; set; } // datetime, null
    }
    public enum ProjectPartlistPurchaseRequestTypeModel_Enum{
        ID,
        RequestTypeName,
        IsIgnoreBGD,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        RequestTypeCode
    }
}
