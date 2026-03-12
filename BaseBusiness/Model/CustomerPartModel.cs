
using System;
namespace BMS.Model
{
	public partial class CustomerPartModel : BaseModel
	{
		public int ID {get; set;}
		
		public string PartName {get; set;}
		
		public string PartCode {get; set;}
		
		public int CustomerID {get; set;}
		
		public int STT {get; set;}
		
	}
}
	