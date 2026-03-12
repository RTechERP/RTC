using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class TaxCompanyModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string Code { get; set; } // nvarchar(50), null
        public string Name { get; set; } // nvarchar(200), null
        public string FullName { get; set; } // nvarchar(500), null
        public string CreatedBy { get; set; } // nvarchar(50), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(50), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsDeleted { get; set; } // bit, null
        public string TaxCode { get; set; } // varchar(50), null
        public string Address { get; set; } // nvarchar(550), null
        public string PhoneNumber { get; set; } // varchar(50), null
        public string Director { get; set; } // nvarchar(250), null
        public string Position { get; set; } // nvarchar(250), null
        public string BuyerEnglish { get; set; } // nvarchar(550), null
        public string AddressBuyerEnglish { get; set; } // nvarchar(max), null
        public string LegalRepresentativeEnglish { get; set; } // nvarchar(550), null
        public string BuyerVietnamese { get; set; } // nvarchar(550), null
        public string AddressBuyerVienamese { get; set; } // nvarchar(max), null
        public string TaxVietnamese { get; set; } // nvarchar(550), null
    }
    public enum TaxCompanyModel_Enum{
        ID,
        Code,
        Name,
        FullName,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsDeleted,
        TaxCode,
        Address,
        PhoneNumber,
        Director,
        Position,
        BuyerEnglish,
        AddressBuyerEnglish,
        LegalRepresentativeEnglish,
        BuyerVietnamese,
        AddressBuyerVienamese,
        TaxVietnamese,
        }
}
