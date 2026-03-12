
using System;
namespace BMS.Model
{
	public partial class ProjectCostModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ProjectID {get; set;}
		
		public int ListCostID {get; set;}
		
		public decimal Money {get; set;}
		
	}
}
	