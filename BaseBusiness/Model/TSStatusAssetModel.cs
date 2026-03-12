
using System;
namespace BMS.Model
{
	public partial class TSStatusAssetModel : BaseModel
	{
		public int ID {get; set;}
		
		public string Status {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	