using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class HistoryMoneyPOModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? Number { get; set; } // int, null
        public decimal? Money { get; set; } // decimal(18,2), null
        public DateTime? MoneyDate { get; set; } // datetime, null
        public int? POKHID { get; set; } // int, null
        public string Note { get; set; } // nvarchar(250), null
        public int? ProjectID { get; set; } // int, null
        public int? ProductID { get; set; } // int, null
        public string InvoiceNo { get; set; } // nvarchar(50), null
        public string BankName { get; set; } // nvarchar(50), null
        public decimal? MoneyVAT { get; set; } // decimal(18,2), null
        public decimal? VAT { get; set; } // decimal(18,2), null
        public int? Type { get; set; } // int, null
        public int? POKHDetailID { get; set; } // int, null
        public bool? IsFilm { get; set; } // bit, null
        public bool? IsMergePO { get; set; } // bit, null
        public decimal? MoneyNotPaid { get; set; } // decimal(18,2), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
    }
    public enum HistoryMoneyPOModel_Enum
    {
        ID,
        Number,
        Money,
        MoneyDate,
        POKHID,
        Note,
        ProjectID,
        ProductID,
        InvoiceNo,
        BankName,
        MoneyVAT,
        VAT,
        Type,
        POKHDetailID,
        IsFilm,
        IsMergePO,
        MoneyNotPaid,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
    }
}
