
using System;
namespace BMS.Model
{
	public partial class FilmManagementModel : BaseModel
	{
		public int ID {get; set;}
		
		public int STT {get; set;}
		
		public string Code {get; set;}
		
		public string Name {get; set;}
		public bool RequestResult { get; set;}
		public bool IsDeleted { get; set;}
		public DateTime? CreatedDate {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	