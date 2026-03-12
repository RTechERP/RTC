
using System;
namespace BMS.Model
{
	public partial class BillFilmModel : BaseModel
	{
		public int ID {get; set;}
		
		public string Code {get; set;}
		
		public DateTime? CreatDate {get; set;}
		
		public bool TypeBill {get; set;}
		
		public int SupplierID {get; set;}
		
		public int CustomerID {get; set;}
		
		public int UserID {get; set;}
		
		public int StockID {get; set;}
		
		public string Description {get; set;}
		
	}
}
	