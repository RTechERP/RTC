
using System;
namespace BMS.Model
{
	public partial class TSReportBrokenAssetModel : BaseModel
	{
		public int ID {get; set;}
		
		public int AssetManagementID {get; set;}
		
		public DateTime? DateReportBroken {get; set;}
		
		public string Reason {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	