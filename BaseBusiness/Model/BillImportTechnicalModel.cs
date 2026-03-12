using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class BillImportTechnicalModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string BillCode { get; set; } // varchar(100), null
        public DateTime? CreatDate { get; set; } // datetime, null
        public string Deliver { get; set; } // nvarchar(150), null
        public string Receiver { get; set; } // nvarchar(150), null
        public bool? Status { get; set; } // bit, null
        public string Suplier { get; set; } // nvarchar(500), null
        public bool? BillType { get; set; } // bit, null
        public string WarehouseType { get; set; } // nvarchar(100), null
        public int? DeliverID { get; set; } // int, null
        public int? ReceiverID { get; set; } // int, null
        public int? SuplierID { get; set; } // int, null
        public int? GroupTypeID { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string Image { get; set; } // nvarchar(100), null
        public int? WarehouseID { get; set; } // int, null
        public int? SupplierSaleID { get; set; } // int, null
        public int? BillTypeNew { get; set; } // int, null
        public int? IsBorrowSupplier { get; set; } // int, null
        public int? CustomerID { get; set; } // int, null
        public int? BillDocumentImportType { get; set; } // int, null
        public DateTime? DateRequestImport { get; set; } // datetime, null
        public int? RulePayID { get; set; } // int, null
        public bool? IsNormalize { get; set; } // bit, null
        public int? ApproverID { get; set; } // int, null
    }
    public enum BillImportTechnicalModel_Enum
    {
        ID,
        BillCode,
        CreatDate,
        Deliver,
        Receiver,
        Status,
        Suplier,
        BillType,
        WarehouseType,
        DeliverID,
        ReceiverID,
        SuplierID,
        GroupTypeID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        Image,
        WarehouseID,
        SupplierSaleID,
        BillTypeNew,
        IsBorrowSupplier,
        CustomerID,
        BillDocumentImportType,
        DateRequestImport,
        RulePayID,
        IsNormalize,
        ApproverID,
    }
}
