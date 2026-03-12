using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class OrganizationalChartModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? TaxCompanyID { get; set; } // int, null
        public int? DepartmentID { get; set; } // int, null
        public string Code { get; set; } // varchar(50), null
        public string Name { get; set; } // nvarchar(550), null
        public int? ParentID { get; set; } // int, null
        public int? IsDeleted { get; set; } // int, null
        /// <summary>
        /// ID leader
        /// </summary>
        public int? EmployeeID { get; set; } // int, null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public int? STT { get; set; } // nvarchar(150), null
    }
    public enum OrganizationalChartModel_Enum
    {
        ID,
        TaxCompanyID,
        DepartmentID,
        Code,
        Name,
        ParentID,
        IsDeleted,
        EmployeeID,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        STT
    }
}
