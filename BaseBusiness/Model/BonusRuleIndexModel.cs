
using System;
namespace BMS.Model
{
	public partial class BonusRuleIndexModel : BaseModel
	{
		public int ID {get; set;}
		
		public int SaleUserTypeID {get; set;}
		
		public int GroupSalesID {get; set;}
		
		public decimal PercentBonus {get; set;}
		
		public decimal PercentBase {get; set;}
		
		public decimal PercentVision {get; set;}
		
		public decimal PercentBQMS {get; set;}
		
		public decimal PercentMachine {get; set;}
		
	}
}
	