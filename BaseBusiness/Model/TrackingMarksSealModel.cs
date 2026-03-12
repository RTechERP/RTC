
using System;
namespace BMS.Model
{
	public partial class TrackingMarksSealModel : BaseModel
	{
		public int ID {get; set;}
		
		public int TrackingMartkID {get; set;}
		
		public int SealID {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	