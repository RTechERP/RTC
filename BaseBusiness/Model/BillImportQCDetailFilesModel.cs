using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class BillImportQCDetailFilesModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? BillImportQCDetailID { get; set; } // int, null
        public string FileName { get; set; } // nvarchar(550), null
        public string OriginPath { get; set; } // nvarchar(max), null
        public string ServerPath { get; set; } // nvarchar(max), null
        /// <summary>
        /// 1: Pur checksheet,2: Tech report
        /// </summary>
        public int? FileType { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum BillImportQCDetailFilesModel_Enum{
        ID,
        BillImportQCDetailID,
        FileName,
        OriginPath,
        ServerPath,
        FileType,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
