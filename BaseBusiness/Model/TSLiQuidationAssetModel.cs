
using System;
namespace BMS.Model
{
	public partial class TSLiQuidationAssetModel : BaseModel
	{
		public int ID {get; set;}
		
		public int AssetManagementID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public bool IsApproved {get; set;}
		
		public DateTime? DateSuggest {get; set;}
		
		public DateTime? DateLiquidation {get; set;}
		
		public string Reason {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	