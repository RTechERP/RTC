
using System;
namespace BMS.Model
{
	public partial class HistoryErrorModel : BaseModel
	{
		public long ID {get; set;}
		
		public long ProductHistoryID {get; set;}
		
		public string DescriptionError {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public DateTime? UpdateDate {get; set;}
		
	}
}
	