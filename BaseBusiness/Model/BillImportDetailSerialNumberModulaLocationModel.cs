using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class BillImportDetailSerialNumberModulaLocationModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? BillImportDetailSerialNumberID { get; set; } // int, null
        public int? ModulaLocationDetailID { get; set; } // int, null
        public decimal? Quantity { get; set; } // decimal(18,2), null
        public bool? IsDeleted { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public int? BillImportTechDetailSerialID { get; set; } // int, null
    }
    public enum BillImportDetailSerialNumberModulaLocationModel_Enum{
        ID,
        BillImportDetailSerialNumberID,
        ModulaLocationDetailID,
        Quantity,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        BillImportTechDetailSerialID,
        }
}
