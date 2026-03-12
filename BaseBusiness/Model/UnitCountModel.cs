
using System;
namespace BMS.Model
{
	public partial class UnitCountModel : BaseModel
	{
		public int ID {get; set;}
		
		public string UnitCode {get; set;}
		
		public string UnitName {get; set;}
		public string CreatedBy { get; set;}
		public DateTime? CreatedDate { get; set;}
		public string UpdatedBy { get; set;}
		public DateTime? UpdatedDate { get; set;}
		public bool IsDeleted { get; set;}
		
	}
}
	