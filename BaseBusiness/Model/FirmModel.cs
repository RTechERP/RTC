using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class FirmModel : BaseModel
    {
        public int ID { get; set; } // int
        public string FirmCode { get; set; } // nvarchar(250)
        public string FirmName { get; set; } // nvarchar(250)
        /// <summary>
        /// 1: Hãng kho Sale; 2: Hãng kho Demo
        /// </summary>
        public int? FirmType { get; set; } // int
        public string CreatedBy { get; set; } // nvarchar(150)
        public DateTime? CreatedDate { get; set; } // datetime
        public string UpdatedBy { get; set; } // nvarchar(150)
        public DateTime? UpdatedDate { get; set; } // datetime
        public bool? IsDelete { get; set; } // datetime
    }
    public enum FirmModel_Enum
    {
        ID,
        FirmCode,
        FirmName,
        FirmType,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsDelete,
    }
}
