using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class HandoverMinutesModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string Code { get; set; } // varchar(50), null
        public DateTime? DateMinutes { get; set; } // datetime, null
        public int? CustomerID { get; set; } // int, null
        public string CustomerAddress { get; set; } // nvarchar(550), null
        public string CustomerContact { get; set; } // nvarchar(550), null
        public string CustomerPhone { get; set; } // varchar(50), null
        /// <summary>
        /// thủ kho (Lấy từ ID Employee)
        /// </summary>
        public int AdminWarehouseID { get; set; } // int, not null
        public int? EmployeeID { get; set; } // int, null
        /// <summary>
        /// Người nhận
        /// </summary>
        public string Receiver { get; set; } // nvarchar(550), null
        public bool? IsDeleted { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string ReceiverPhone { get; set; } // varchar(50), null
    }
    public enum HandoverMinutesModel_Enum{
        AdminWarehouseID,
        ID,
        Code,
        DateMinutes,
        CustomerID,
        CustomerAddress,
        CustomerContact,
        CustomerPhone,
        EmployeeID,
        Receiver,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        ReceiverPhone,
        }
}
