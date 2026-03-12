
using System;
namespace BMS.Model
{
	public partial class ActionHistoryModel : BaseModel
	{
		public int ID {get; set;}
		
		public int UserID {get; set;}
		
		public string UserName {get; set;}
		
		public string Action {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	