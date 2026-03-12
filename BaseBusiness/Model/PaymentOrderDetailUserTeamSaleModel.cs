
using System;
namespace BMS.Model
{
	public partial class PaymentOrderDetailUserTeamSaleModel : BaseModel
	{
		public int ID {get; set;}
		
		public int PaymentOrderDetailID {get; set;}
		
		public int UserTeamSaleID {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	