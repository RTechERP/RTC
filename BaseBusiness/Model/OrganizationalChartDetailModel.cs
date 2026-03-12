using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class OrganizationalChartDetailModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? OrganizationalChartID { get; set; } // int, null
        public int? EmployeeID { get; set; } // int, null
        public int? IsDeleted { get; set; } // int, null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
    }
    public enum OrganizationalChartDetailModel_Enum{
        ID,
        OrganizationalChartID,
        EmployeeID,
        IsDeleted,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        }
}
