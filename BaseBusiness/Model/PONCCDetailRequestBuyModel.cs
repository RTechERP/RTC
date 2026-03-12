
using System;
namespace BMS.Model
{
	public partial class PONCCDetailRequestBuyModel : BaseModel
	{
		public int ID {get; set;}
		
		public int PONCCDetailID {get; set;}
		
		public int ProjectPartlistPurchaseRequestID {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	