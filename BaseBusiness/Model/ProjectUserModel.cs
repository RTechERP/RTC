
using System;
namespace BMS.Model
{
	public partial class ProjectUserModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ProjectID {get; set;}
		
		public int UserID {get; set;}
		
		public string Mission {get; set;}
		
		public int STT {get; set;}
		
	}
}
	