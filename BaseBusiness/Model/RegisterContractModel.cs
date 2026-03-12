using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class RegisterContractModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? EmployeeID { get; set; } // int, null
        public int? EmployeeReciveID { get; set; } // int, null
        public int? TaxCompanyID { get; set; } // int, null
        public DateTime? RegistedDate { get; set; } // datetime, null
        public int? DocumentTypeID { get; set; } // int, null
        public string DocumentName { get; set; } // nvarchar(max), null
        public int? DocumentQuantity { get; set; } // int, null
        /// <summary>
        /// 1: Sao y, 2: Gốc, 3: Treo
        /// </summary>
        public int? ContractTypeID { get; set; } // int, null
        public string ReasonCancel { get; set; } // nvarchar(max), null
        /// <summary>
        /// 0: Chưa nhận,1: Đã nhận, 2: Hủy
        /// </summary>
        public int? Status { get; set; } // int, null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public bool? IsDeleted { get; set; } // bit, null
        public DateTime? DateApproved { get; set; } // datetime, null
        public bool? IsScan { get; set; } // bit, null
        public string FolderPath { get; set; } // nvarchar(550), null
    }
    public enum RegisterContractModel_Enum{
        ID,
        EmployeeID,
        EmployeeReciveID,
        TaxCompanyID,
        RegistedDate,
        DocumentTypeID,
        DocumentName,
        DocumentQuantity,
        ContractTypeID,
        ReasonCancel,
        Status,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        IsDeleted,
        DateApproved,
        IsScan,
        FolderPath,
        }
}
