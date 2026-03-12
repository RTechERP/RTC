using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ModulaLocationDetailModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? STT { get; set; } // int, not null
        public int? ModulaLocationID { get; set; } // int, null
        public string Code { get; set; } // varchar(50), null
        public string Name { get; set; } // nvarchar(150), null
        /// <summary>
        /// Đơn vị chiều rộng theo mm
        /// </summary>
        public int? Width { get; set; } // int, null
        /// <summary>
        /// Đơn vị chiều dài theo mm
        /// </summary>
        public int? Height { get; set; } // int, null
        public int? AxisX { get; set; } // int, null
        public int? AxisY { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null

        public (double? X, double? Y) GetCenter() => (AxisX + Width / 2.0, AxisY + Height / 2.0);
    }
    public enum ModulaLocationDetailModel_Enum
    {
        STT,
        ID,
        ModulaLocationID,
        Code,
        Name,
        Width,
        Height,
        AxisX,
        AxisY,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
    }
}
