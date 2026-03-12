using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class CourseTypeModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string CourseTypeCode { get; set; } // varchar(150), null
        public string CourseTypeName { get; set; } // nvarchar(550), null
        public string CreatedBy { get; set; } // varchar(20), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // varchar(20), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public int? STT { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        /// <summary>
        /// 1: Học lần lượt, 0: ko cần học lần lượt
        /// </summary>
        public bool? IsLearnInTurn { get; set; } // bit, null
    }
    public enum CourseTypeModel_Enum{
        ID,
        CourseTypeCode,
        CourseTypeName,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        STT,
        IsDeleted,
        IsLearnInTurn,
        }
}
