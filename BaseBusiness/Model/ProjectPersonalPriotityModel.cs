
using System;
namespace BMS.Model
{
	public partial class ProjectPersonalPriotityModel : BaseModel
	{
		public int ID {get; set;}
		
		public int UserID {get; set;}
		
		public int ProjectID {get; set;}
		
		public int Priotity {get; set;}
		
	}
}
	