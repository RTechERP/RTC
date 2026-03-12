
using System;
namespace BMS.Model
{
	public partial class AccountingBillApprovedModel : BaseModel
	{
		public int ID {get; set;}
		
		public int AccountingBillID {get; set;}
		
		public int DocumentImportID {get; set;}
		
		public int Status {get; set;}
		
		public string Note {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	