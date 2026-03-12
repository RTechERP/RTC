
using System;
namespace BMS.Model
{
	public partial class ProductRTCQRCodeModel : BaseModel
	{
		public int ID {get; set;}
		
		public int Status {get; set;}
		
		public int ProductRTCID {get; set;}
		
		public string ProductQRCode {get; set;}
		
		public string Serial {get; set;}
		
		public string SerialNumber {get; set;}
		
		public string PartNumber {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		public int WarehouseID { get; set; }
		public int ModulaLocationDetailID { get; set; }
	}
}
	