
using System;
namespace BMS.Model
{
	public partial class AuditLogModel : BaseModel
	{
		public long ID {get; set;}
		
		public string TableName {get; set;}
		
		public int Action {get; set;}
		
		public string UserName {get; set;}
		
		public DateTime? ActionDate {get; set;}
		
		public string Note {get; set;}
		
	}
}
	