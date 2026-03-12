using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class TSAssetManagementModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public bool? IsAllocation { get; set; } // bit, null
        public int? StatusID { get; set; } // int, null
        public int? DepartmentID { get; set; } // int, null
        /// <summary>
        /// Trưởng phòng quản lý tài sản
        /// </summary>
        public int? EmployeeID { get; set; } // int, null
        public int? TSAssetID { get; set; } // int, null
        public string TSAssetCode { get; set; } // nvarchar(max), null
        public string TSAssetName { get; set; } // nvarchar(max), null
        /// <summary>
        /// Nguồn gốc tài sản
        /// </summary>
        public int? SourceID { get; set; } // int, null
        public string Seri { get; set; } // nvarchar(max), null
        /// <summary>
        /// quy cách tài sản
        /// </summary>
        public string SpecificationsAsset { get; set; } // nvarchar(max), null
        /// <summary>
        /// nhà cung cấp tài sản
        /// </summary>
        public int? SupplierID { get; set; } // int, null
        public DateTime? DateBuy { get; set; } // datetime, null
        /// <summary>
        /// Thời gian bảo hành
        /// </summary>
        public decimal? Insurance { get; set; } // decimal(18,2), null
        /// <summary>
        /// Thời gian áp dụng
        /// </summary>
        public DateTime? DateEffect { get; set; } // datetime, null
        public string Status { get; set; } // nvarchar(max), null
        public string Note { get; set; } // nvarchar(500), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(50), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(50), null
        public int? UnitID { get; set; } // int, null
        public string TSCodeNCC { get; set; } // varchar(550), null
        public int? STT { get; set; } // int, null
        /// <summary>
        /// 1: Chưa active; 2: Đã active; 3: Crack
        /// </summary>
        public int? OfficeActiveStatus { get; set; } // int, null
        /// <summary>
        /// 1: Chưa active; 2: Đã active; 3: Crack
        /// </summary>
        public int? WindowActiveStatus { get; set; } // int, null
        public string Model { get; set; } // int, null
    }
    public enum TSAssetManagementModel_Enum{
        ID,
        IsAllocation,
        StatusID,
        DepartmentID,
        EmployeeID,
        TSAssetID,
        TSAssetCode,
        TSAssetName,
        SourceID,
        Seri,
        SpecificationsAsset,
        SupplierID,
        DateBuy,
        Insurance,
        DateEffect,
        Status,
        Note,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        UnitID,
        TSCodeNCC,
        STT,
        OfficeActiveStatus,
        WindowActiveStatus,
        Model
    }
}
