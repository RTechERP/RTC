
using System;
namespace BMS.Model
{
	public partial class EmployeeFoodOrderModel : BaseModel
	{
		public int ID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public int Quantity {get; set;}
		
		public DateTime? DateOrder {get; set;}
		
		public string Note {get; set;}
		
		public bool IsApproved {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		public bool IsDeleted { get; set;}

		/// <summary>
		/// 1: VP HN
		/// 2: Xưởng Đan Phượng
		/// </summary>
		public int Location { get; set;}
		
	}
}
	