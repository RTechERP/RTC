
using System;
namespace BMS.Model
{
	public partial class TrackingMarksFileModel : BaseModel
	{
		public int ID {get; set;}
		
		public int TrackingMarksID {get; set;}
		
		public string FileName {get; set;}
		
		public string OriginPath {get; set;}
		
		public string ServerPath {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	