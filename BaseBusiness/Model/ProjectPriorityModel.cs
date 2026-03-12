
using System;
namespace BMS.Model
{
	public partial class ProjectPriorityModel : BaseModel
	{
		public int ID {get; set;}
		
		public string Code {get; set;}
		
		public string ProjectCheckpoint {get; set;}
		
		public decimal Rate {get; set;}
		
		public int Score {get; set;}
		
		public decimal Priority {get; set;}
		
		public int ParentID {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	