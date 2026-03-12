
using System;
namespace BMS.Model
{
	public partial class PONCCDetailLogModel : BaseModel
	{
		public int ID {get; set;}
		
		public DateTime? DateLog {get; set;}
		
		public string ContentLog {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	