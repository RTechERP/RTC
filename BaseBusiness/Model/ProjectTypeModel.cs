using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ProjectTypeModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string ProjectTypeCode { get; set; } // nvarchar(250), null
        public string ProjectTypeName { get; set; } // nvarchar(250), null
        public int? ParentID { get; set; } // int, null
        public string RootFolder { get; set; } // nvarchar(550), null
        public int? ApprovedTBPID { get; set; } // int, null
    }
    public enum ProjectTypeModel_Enum{
        ID,
        ProjectTypeCode,
        ProjectTypeName,
        ParentID,
        RootFolder,
        ApprovedTBPID,
        }
}
