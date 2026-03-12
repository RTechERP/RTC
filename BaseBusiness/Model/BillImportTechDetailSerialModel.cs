
using System;
namespace BMS.Model
{
	public partial class BillImportTechDetailSerialModel : BaseModel
	{
		public int ID {get; set;}
		
		public int STT {get; set;}
		
		public int BillImportTechDetailID {get; set;}
		
		public string SerialNumber {get; set;}
		public int WarehouseID { get; set;}
		
	}
}
	