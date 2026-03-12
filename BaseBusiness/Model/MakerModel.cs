using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class MakerModel : BaseModel
    {
        public int ID { get; set; } // int
        public int STT { get; set; } // int
        public string CodeMaker { get; set; } // varchar(50)
        public string NameMaker { get; set; } // nvarchar(550)
        public DateTime? CreatedDate { get; set; } // datetime
        public DateTime? UpdatedDate { get; set; } // datetime
        public string CreatedBy { get; set; } // nvarchar(150)
        public string UpdatedBy { get; set; } // nvarchar(150)
    }
    public enum MakerModel_Enum{
        ID,
        CodeMaker,
        NameMaker,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        }
}
