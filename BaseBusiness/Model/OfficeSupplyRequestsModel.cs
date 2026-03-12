using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class OfficeSupplyRequestsModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? EmployeeIDRequest { get; set; } // int, null
        public DateTime? DateRequest { get; set; } // datetime, null
        public string CreatedBy { get; set; } // varchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // varchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsApproved { get; set; } // bit, null
        public int? ApprovedID { get; set; } // int, null
        public DateTime? DateApproved { get; set; } // datetime, null
        public int? DepartmentID { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        public bool? IsAdminApproved { get; set; } // bit, null
        public int? AdminApprovedID { get; set; } // int, null
        public DateTime? DateAdminApproved { get; set; } // datetime, null
    }
    public enum OfficeSupplyRequestsModel_Enum{
        ID,
        EmployeeIDRequest,
        DateRequest,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsApproved,
        ApprovedID,
        DateApproved,
        DepartmentID,
        IsDeleted,
        IsAdminApproved,
        AdminApprovedID,
        DateAdminApproved,
        }
}
