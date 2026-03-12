
using System;
namespace BMS.Model
{
	public partial class ProjectStatusDetailModel : BaseModel
	{
		public int ID {get; set;}
		
		public int STT {get; set;}
		
		public int ProjectID {get; set;}
		
		public int ProjectStatusID {get; set;}
		
		public DateTime? EstimatedStartDate {get; set;}
		
		public DateTime? EstimatedEndDate {get; set;}
		
		public DateTime? ActualStartDate {get; set;}
		
		public DateTime? ActualEndDate {get; set;}
		
		public bool Selected {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	