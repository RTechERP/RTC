
using System;
namespace BMS.Model
{
	public partial class RequestExportDetailModel : BaseModel
	{
		public int ID {get; set;}
		
		public int RequestID {get; set;}
		
		public int ProductID {get; set;}
		
		public int POKHID {get; set;}
		
		public int Qty {get; set;}
		
		public string Warehouse {get; set;}
		
		public string Project {get; set;}
		
		public string Note {get; set;}
		
	}
}
	