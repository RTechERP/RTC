
using System;
namespace BMS.Model
{
	public partial class TSTranferAssetModel : BaseModel
	{
		public int ID {get; set;}
		
		public int AssetManagementID {get; set;}
		
		public int DeliverID {get; set;}
		
		public int ReceiverID {get; set;}
		
		public int FromDepartmentID {get; set;}
		
		public int ToDepartmentID {get; set;}
		
		public int FromChucVuID {get; set;}
		
		public int ToChucVuID {get; set;}
		
		public string CodeReport {get; set;}
		
		public DateTime? TranferDate {get; set;}
		
		public string Reason {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public bool IsApproved {get; set;}
		
		public bool IsApproveAccountant {get; set;}
		
		public bool IsApprovedPersonalProperty {get; set;}
		
		public DateTime? DateApproveAccountant {get; set;}
		
		public DateTime? DateApprovedPersonalProperty {get; set;}
		
		public DateTime? DateApprovedHR {get; set;}
		
	}
}
	