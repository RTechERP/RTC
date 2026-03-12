using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class HandoverMinutesDetailModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? HandoverMinutesID { get; set; } // int, null
        public int? STT { get; set; } // int, null
        public int? POKHID { get; set; } // int, null
        public int? ProductSaleID { get; set; } // int, null
        public decimal? Quantity { get; set; } // decimal(18,2), null
        /// <summary>
        /// 1: Hàng mới
        /// </summary>
        public int? ProductStatus { get; set; } // int, null
        public string Guarantee { get; set; } // nvarchar(50), null
        /// <summary>
        /// 1: đã nhận đủ; 2: thiếu
        /// </summary>
        public int? DeliveryStatus { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public int? POKHDetailID { get; set; } // int, null
    }
    public enum HandoverMinutesDetailModel_Enum{
        ID,
        HandoverMinutesID,
        STT,
        POKHID,
        ProductSaleID,
        Quantity,
        ProductStatus,
        Guarantee,
        DeliveryStatus,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        POKHDetailID,
        }
}
