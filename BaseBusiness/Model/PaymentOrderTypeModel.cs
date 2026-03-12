
using System;
namespace BMS.Model
{
	public partial class PaymentOrderTypeModel : BaseModel
	{
		public int ID {get; set;}
		
		public int STT {get; set;}
		
		public string TypeCode {get; set;}
		
		public string TypeName {get; set;}
		
		public bool IsIgnoreHR {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public bool IsDelete {get; set;}
		
		public bool IsSpecialOrder {get; set;}
		
	}
}
	