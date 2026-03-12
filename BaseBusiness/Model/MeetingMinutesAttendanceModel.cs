using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class MeetingMinutesAttendanceModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? MeetingMinutesID { get; set; } // int, null
        public int? EmployeeID { get; set; } // int, null
        public string Section { get; set; } // nvarchar(250), null
        public string CustomerName { get; set; } // nvarchar(250), null
        public string PhoneNumber { get; set; } // nvarchar(250), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public bool? IsDeleted { get; set; } // bit, null
        public bool? IsEmployee { get; set; } // bit, null
        public string FullName { get; set; } // bit, null
        public int UserTeamID { get; set; } // bit, null
        public string EmailCustomer { get; set; } // bit, null
        public string AddressCustomer { get; set; } // bit, null
    }
    public enum MeetingMinutesAttendanceModel_Enum
    {
        ID,
        MeetingMinutesID,
        EmployeeID,
        Section,
        CustomerName,
        PhoneNumber,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        IsDeleted,
        IsEmployee,
        FullName,
        UserTeamID,
        EmailCustomer,
        AddressCustomer,
    }
}
