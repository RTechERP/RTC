
using System;
namespace BMS.Model
{
	public partial class BillExportLogModel : BaseModel
	{
		public int ID {get; set;}
		
		public int BillExportID {get; set;}
		
		public bool StatusBill {get; set;}
		
		public DateTime? DateStatus {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	