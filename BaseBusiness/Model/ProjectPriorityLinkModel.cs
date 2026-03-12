
using System;
namespace BMS.Model
{
	public partial class ProjectPriorityLinkModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ProjectID {get; set;}
		
		public int ProjectPriorityID {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	