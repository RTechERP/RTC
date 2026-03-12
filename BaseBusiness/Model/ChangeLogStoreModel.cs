
using System;
namespace BMS.Model
{
	public partial class ChangeLogStoreModel : BaseModel
	{
		public int LogId {get; set;}
		
		public string DatabaseName {get; set;}
		
		public string EventType {get; set;}
		
		public string ObjectName {get; set;}
		
		public string ObjectType {get; set;}
		
		public string SqlCommand {get; set;}
		
		public DateTime? EventDate {get; set;}
		
		public string LoginName {get; set;}
		
	}
}
	