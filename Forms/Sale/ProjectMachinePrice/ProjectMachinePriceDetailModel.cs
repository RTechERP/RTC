using System;
namespace BMS.Model
{
    public partial class ProjectMachinePriceDetailModel: BaseModel
    {
        public int ID { get; set; }
        public int? ProjectMachinePriceID { get; set; }
        public int? ProjectVersionID { get; set; }
        public int? STT { get; set; }
        public string CodeGroup { get; set; }
        public string NameGroup { get; set; }
        public string ContentPrice { get; set; }
        public decimal? AmountSpent { get; set; }
        public string DependentObject { get; set; }
        public decimal? EstimateCost { get; set; }
        public decimal? Coefficient { get; set; }
        public string Note { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
