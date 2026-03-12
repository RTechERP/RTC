
using System;
namespace BMS.Model
{
	public partial class UserTeamSaleModel : BaseModel
	{
		public int ID {get; set;}
		
		public int STT {get; set;}
		
		public string Name {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		public bool IsDeleted { get; set;}
		
	}
}
	