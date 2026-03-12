
using System;
namespace BMS.Model
{
	public partial class UserTeamModel : BaseModel
	{
		public int ID {get; set;}
		
		public string Name {get; set;}
		
		public int DepartmentID {get; set;}
		
		public int LeaderID {get; set;}
		
		public int ParentID {get; set;}
		
		public int ProjectTypeID {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	