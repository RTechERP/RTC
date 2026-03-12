using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ProductRTCModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? ProductGroupRTCID { get; set; } // int, null
        /// <summary>
        /// Mã thiết bị
        /// </summary>
        public string ProductCode { get; set; } // nvarchar(150), null
        /// <summary>
        /// Tên thiết bị
        /// </summary>
        public string ProductName { get; set; } // nvarchar(150), null
        /// <summary>
        /// Hãng
        /// </summary>
        public string Maker { get; set; } // nvarchar(150), null
        public int? UnitCountID { get; set; } // int, null
        /// <summary>
        /// Số lượng tổng
        /// </summary>
        public decimal? Number { get; set; } // decimal(18,0), null
        public string AddressBox { get; set; } // nvarchar(150), null
        public string Note { get; set; } // nvarchar(max), null
        /// <summary>
        /// Trạng thái sản phẩm (1: hiện có, 0: không có)
        /// </summary>
        public bool? StatusProduct { get; set; } // bit, null
        public DateTime? CreateDate { get; set; } // datetime, null
        public decimal? NumberInStore { get; set; } // decimal(18,0), null
        public string Serial { get; set; } // nvarchar(150), null
        public string SerialNumber { get; set; } // nvarchar(150), null
        public string PartNumber { get; set; } // nvarchar(150), null
        public string CreatedBy { get; set; } // nvarchar(50), null
        public string LocationImg { get; set; } // nvarchar(150), null
        public string ProductCodeRTC { get; set; } // nvarchar(150), null
        public bool? BorrowCustomer { get; set; } // bit, null
        public int? SLKiemKe { get; set; } // int, null
        public int? ProductLocationID { get; set; } // int, null
        public int? WarehouseID { get; set; } // int, null
        public string Resolution { get; set; } // nvarchar(150), null
        public string MonoColor { get; set; } // nvarchar(150), null
        public string SensorSize { get; set; } // nvarchar(150), null
        public string DataInterface { get; set; } // nvarchar(150), null
        public string LensMount { get; set; } // nvarchar(150), null
        public string ShutterMode { get; set; } // nvarchar(150), null
        public string PixelSize { get; set; } // nvarchar(150), null
        public string SensorSizeMax { get; set; } // nvarchar(150), null
        public string MOD { get; set; } // nvarchar(150), null
        public string FNo { get; set; } // nvarchar(150), null
        public string WD { get; set; } // nvarchar(150), null
        public string LampType { get; set; } // nvarchar(150), null
        public string LampColor { get; set; } // nvarchar(150), null
        public string LampPower { get; set; } // nvarchar(150), null
        public string LampWattage { get; set; } // nvarchar(150), null
        public bool? IsDelete { get; set; } // bit, null
        public string Magnification { get; set; } // nvarchar(max), null
        public string FocalLength { get; set; } // nvarchar(max), null
        public int? FirmID { get; set; } // int, null
        public string InputValue { get; set; } // nvarchar(150), null
        public string OutputValue { get; set; } // nvarchar(150), null
        public string CurrentIntensityMax { get; set; } // nvarchar(150), null
        /// <summary>
        /// 1: Đang giặt
        /// </summary>
        public int? Status { get; set; } // int, null
        public string Size { get; set; } // nvarchar(150), null
        public string CodeHCM { get; set; } // nvarchar(150), null
    }
    public enum ProductRTCModel_Enum
    {
        ID,
        ProductGroupRTCID,
        ProductCode,
        ProductName,
        Maker,
        UnitCountID,
        Number,
        AddressBox,
        Note,
        StatusProduct,
        CreateDate,
        NumberInStore,
        Serial,
        SerialNumber,
        PartNumber,
        CreatedBy,
        LocationImg,
        ProductCodeRTC,
        BorrowCustomer,
        SLKiemKe,
        ProductLocationID,
        WarehouseID,
        Resolution,
        MonoColor,
        SensorSize,
        DataInterface,
        LensMount,
        ShutterMode,
        PixelSize,
        SensorSizeMax,
        MOD,
        FNo,
        WD,
        LampType,
        LampColor,
        LampPower,
        LampWattage,
        IsDelete,
        Magnification,
        FocalLength,
        FirmID,
        InputValue,
        OutputValue,
        CurrentIntensityMax,
        Status,
        Size,
        CodeHCM
    }
}
