using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class BillExportDetailSerialNumberModulaLocationModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? BillExportDetailSerialNumberID { get; set; } // int, null
        public int? ModulaLocationDetailID { get; set; } // int, null
        public decimal? Quantity { get; set; } // decimal(18,2), null
        public bool? IsDeleted { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public int? BillExportTechDetailSerialID { get; set; } // int, null
    }
    public enum BillExportDetailSerialNumberModulaLocationModel_Enum{
        ID,
        BillExportDetailSerialNumberID,
        ModulaLocationDetailID,
        Quantity,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        BillExportTechDetailSerialID,
        }
}
