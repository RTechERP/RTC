
using System;
namespace BMS.Model
{
	public partial class TSAssetModel : BaseModel
	{
		public int ID {get; set;}
		
		public string AssetCode {get; set;}
		
		public string AssetType {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	