
using System;
namespace BMS.Model
{
	public partial class SupplierSaleContactModel : BaseModel
	{
		public int ID {get; set;}
		
		public int SupplierID {get; set;}
		
		public string SupplierName {get; set;}
		
		public string SupplierPhone {get; set;}
		
		public string SupplierEmail {get; set;}
		
		public string Describe {get; set;}
		
	}
}
	