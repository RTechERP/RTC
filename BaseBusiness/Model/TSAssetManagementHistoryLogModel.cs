
using System;
namespace BMS.Model
{
	public partial class TSAssetManagementHistoryLogModel : BaseModel
	{
		public int ID {get; set;}
		
		public int TSAssetManagementHistoryID {get; set;}
		
		public DateTime? DateExpectedReturn {get; set;}
		
		public string Note {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	