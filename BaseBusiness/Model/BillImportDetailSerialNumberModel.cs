
using System;
namespace BMS.Model
{
	public partial class BillImportDetailSerialNumberModel : BaseModel
	{
		public int ID {get; set;}
		
		public int BillImportDetailID {get; set;}
		
		public int STT {get; set;}
		
		public string SerialNumber {get; set;}
		
		public string SerialNumberRTC {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	