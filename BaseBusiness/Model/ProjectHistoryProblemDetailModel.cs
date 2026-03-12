
using System;
namespace BMS.Model
{
	public partial class ProjectHistoryProblemDetailModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ProjectHistoryProblemID {get; set;}
		
		public int STT {get; set;}
		
		public string Description {get; set;}
		
		public int Status {get; set;}
		
		public string Note {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	