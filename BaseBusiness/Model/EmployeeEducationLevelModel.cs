using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class EmployeeEducationLevelModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? EmployeeID { get; set; } // int, null
        public int? STT { get; set; } // int, null
        public string SchoolName { get; set; } // nvarchar(550), null
        /// <summary>
        /// 1:Đại học (ĐH); 2: Cao đẳng (CĐ); 3: Trung cấp (TC)
        /// </summary>
        public int? RankType { get; set; } // int, null
        /// <summary>
        /// Loại hình đào tạo: 1. Chính quy; 2. Liên thông
        /// </summary>
        public int? TrainType { get; set; } // int, null
        public string Major { get; set; } // nvarchar(550), null
        public int? YearGraduate { get; set; } // int, null
        /// <summary>
        /// Xếp loại (1:Giỏi; 2: Khá, 3: Trung bình)
        /// </summary>
        public int? Classification { get; set; } // int, null
        public string CreatedBy { get; set; } // varchar(20), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // varchar(20), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum EmployeeEducationLevelModel_Enum
    {
        ID,
        EmployeeID,
        STT,
        SchoolName,
        RankType,
        TrainType,
        Major,
        YearGraduate,
        Classification,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
    }
}
