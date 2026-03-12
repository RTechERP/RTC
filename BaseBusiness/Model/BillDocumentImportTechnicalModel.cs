
using System;
namespace BMS.Model
{
	public partial class BillDocumentImportTechnicalModel : BaseModel
	{
		public int ID {get; set;}
		
		public int BillImportTechnicalID {get; set;}
		
		public int DocumentImportID {get; set;}
		
		public int Status {get; set;}
		
		public DateTime? LogDate {get; set;}
		
		public string Note {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
        public int? StatusPurchase { get; set; } // int, null
        public string ReasonCancel { get; set; } // string, null
        public int? EmployeeReceiveID { get; set; } // int, null
        public DateTime? DateReceive { get; set; }

    }
}
	