
using System;
namespace BMS.Model
{
	public partial class TSRepairAssetModel : BaseModel
	{
		public int ID {get; set;}
		
		public int AssetManagementID {get; set;}
		
		public DateTime? DateRepair {get; set;}
		
		public DateTime? DateEndRepair {get; set;}
		
		public DateTime? DateReuse {get; set;}
		
		public string Name {get; set;}
		
		public decimal ExpectedCost {get; set;}
		
		public decimal ActualCosts {get; set;}
		
		public string ContentRepair {get; set;}
		
		public string Reason {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	