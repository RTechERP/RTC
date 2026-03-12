
using System;
namespace BMS.Model
{
	public partial class HistoryProductRTCLogModel : BaseModel
	{
		public int ID {get; set;}
		
		public int HistoryProductRTCID {get; set;}
		
		public DateTime? DateReturnExpected { get; set;}
		
		public string Note {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	