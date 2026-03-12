
using System;
namespace BMS.Model
{
	public partial class FollowProjectBaseModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ProjectID {get; set;}
		
		public int UserID {get; set;}
		
		public int CustomerBaseID {get; set;}
		
		public int EndUserID {get; set;}
		
		public int ProjectStatusBaseID {get; set;}
		
		public DateTime? ProjectStartDate {get; set;}
		
		public int ProjectTypeBaseID {get; set;}
		
		public int FirmBaseID {get; set;}
		
		public string WorkDone {get; set;}
		
		public DateTime? ImplementationDate {get; set;}
		
		public string WorkWillDo {get; set;}
		
		public DateTime? ExpectedDate {get; set;}
		
		public string PossibilityPO {get; set;}
		
		public string Fail {get; set;}
		
		public DateTime? ExpectedPlanDate {get; set;}
		
		public DateTime? ExpectedQuotationDate {get; set;}
		
		public DateTime? ExpectedPODate {get; set;}
		
		public DateTime? ExpectedProjectEndDate {get; set;}
		
		public DateTime? RealityPlanDate {get; set;}
		
		public DateTime? RealityQuotationDate {get; set;}
		
		public DateTime? RealityPODate {get; set;}
		
		public DateTime? RealityProjectEndDate {get; set;}
		
		public decimal TotalWithoutVAT {get; set;}
		
		public string ProjectContactName {get; set;}
		
		public string Note {get; set;}
		
		public string WorkDonePM {get; set;}
		
		public string WorkWillDoPM {get; set;}
		
		public DateTime? DateDonePM {get; set;}
		
		public DateTime? DateWillDoPM {get; set;}
		
		public DateTime? DateDoneSale {get; set;}
		
		public DateTime? DateWillDoSale {get; set;}
		
		public string Results {get; set;}
		
		public string ProblemBacklog {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
        public int WarehouseID { get; set; }

    }
}
	