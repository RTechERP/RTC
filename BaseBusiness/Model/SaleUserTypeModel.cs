
using System;
namespace BMS.Model
{
	public partial class SaleUserTypeModel : BaseModel
	{
		public int ID {get; set;}
		
		public string SaleUserTypeCode {get; set;}
		
		public string SaleUserTypeName {get; set;}
		
		public decimal PercentBonus {get; set;}
		
	}
}
	