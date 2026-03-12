
using System;
namespace BMS.Model
{
	public partial class TaxEmployeeContractModel : BaseModel
	{
		public int ID {get; set;}
		
		public int STT {get; set;}
		
		public int TaxEmployeeID {get; set;}
		
		public int EmployeeLoaiHDLDID {get; set;}
		
		public DateTime? DateStart {get; set;}
		
		public DateTime? DateEnd {get; set;}
		
		public string ContractNumber {get; set;}
		
		public int StatusSign {get; set;}
		
		public DateTime? DateSign {get; set;}
		
		public bool IsDelete {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public int EmployeeID {get; set;}
		
	}
}
	