using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ActivityLogsModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public DateTime LogTime { get; set; } // datetime, not null
        public int? UserID { get; set; } // int, null
        public string Application { get; set; } // nvarchar(50), null
        public string FormName { get; set; } // nvarchar(100), null
        public string Action { get; set; } // nvarchar(200), null
        public string Details { get; set; } // nvarchar(max), null
        public int? EmployeeID { get; set; } // int, null
        public string ControlName { get; set; } // int, null
    }
    public enum ActivityLogsModel_Enum{
        ID,
        LogTime,
        UserID,
        Application,
        FormName,
        Action,
        Details,
        EmployeeID,
        ControlName
    }
}
