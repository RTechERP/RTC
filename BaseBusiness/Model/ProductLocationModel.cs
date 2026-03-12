using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ProductLocationModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string LocationCode { get; set; } // varchar(20), null
        public string OldLocationName { get; set; } // nvarchar(250), null
        public string LocationName { get; set; } // nvarchar(250), null
        public int? WarehouseID { get; set; } // int, null
        public decimal? CoordinatesX { get; set; } // decimal(18,2), null
        public decimal? CoordinatesY { get; set; } // decimal(18,2), null
        public int? STT { get; set; } // int, null
        /// <summary>
        /// 1: Tủ mũ & quần áo; 2: Tủ giày
        /// </summary>
        public int? LocationType { get; set; } // int, null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(50), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(50), null
    }
    public enum ProductLocationModel_Enum{
        ID,
        LocationCode,
        OldLocationName,
        LocationName,
        WarehouseID,
        CoordinatesX,
        CoordinatesY,
        STT,
        LocationType,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        }
}
