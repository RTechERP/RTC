
using System;
namespace BMS.Model
{
	public partial class PercentMainIndexUserModel : BaseModel
	{
		public int ID {get; set;}
		
		public decimal PercentIndex {get; set;}
		
		public int UserID {get; set;}
		
		public int MainIndexID {get; set;}
		
		public int Quy {get; set;}
		
		public int Year {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	