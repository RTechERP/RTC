
using System;
namespace BMS.Model
{
	public partial class EmployeeCurricularModel : BaseModel
	{
		public int ID {get; set;}
		
		public string CurricularCode {get; set;}
		
		public string CurricularName {get; set;}
		
		public int CurricularDay {get; set;}
		
		public int CurricularMonth {get; set;}
		
		public int CurricularYear {get; set;}
		
		public int EmployeeID {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		public string Note { get; set;}
		
	}
}
	