
using System;
namespace BMS.Model
{
	public partial class BillExportTechDetailSerialModel : BaseModel
	{
		public int ID {get; set;}
		
		public int STT {get; set;}
		
		public int BillExportTechDetailID {get; set;}
		
		public string SerialNumber {get; set;}
		public int WarehouseID { get; set;}

		
	}
}
	