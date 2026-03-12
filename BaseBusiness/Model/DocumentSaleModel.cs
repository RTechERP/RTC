
using System;
namespace BMS.Model
{
	public partial class DocumentSaleModel : BaseModel
	{
		public int ID {get; set;}
		
		public int BillID {get; set;}
		
		public string FileName {get; set;}
		
		public string FilePath {get; set;}
		
		public string FileNameOrigin {get; set;}
		
		public int BillType {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	