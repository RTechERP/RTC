
using System;
namespace BMS.Model
{
	public partial class ProjectItemProblemModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ProjectItemID {get; set;}
		
		public string ContentProblem {get; set;}
		
		public string Note {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	