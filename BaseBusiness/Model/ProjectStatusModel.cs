
using System;
namespace BMS.Model
{
	public partial class ProjectStatusModel : BaseModel
	{
		public int ID {get; set;}
		
		public string StatusName {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		public int STT { get; set;}
		
	}
}
	