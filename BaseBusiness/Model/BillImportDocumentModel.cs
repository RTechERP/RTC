
using System;
namespace BMS.Model
{
	public partial class BillImportDocumentModel : BaseModel
	{
		public int ID {get; set;}
		
		public int BillImportID {get; set;}
		
		public int DocumentType {get; set;}
		
		public DateTime? DateReceiver {get; set;}
		
		public string Reason {get; set;}
		
		public bool StatusDocument {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	