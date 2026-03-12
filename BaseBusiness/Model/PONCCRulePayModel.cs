
using System;
namespace BMS.Model
{
	public partial class PONCCRulePayModel : BaseModel
	{
		public int ID {get; set;}
		
		public int PONCCID {get; set;}
		
		public int RulePayID {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	