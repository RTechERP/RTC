
using System;
namespace BMS.Model
{
	public partial class ProductCheckHistoryDetailModel : BaseModel
	{
		public long ID {get; set;}
		
		public int ProductStepID {get; set;}
		
		public int ProductWorkingID {get; set;}
		
		public string QRCode {get; set;}
		
		public string OrderCode {get; set;}
		
		public string ProductOrder {get; set;}
		
		public int WorkerID {get; set;}
		
		public string WorkerCode {get; set;}
		
		public string StandardValue {get; set;}
		
		public string RealValue {get; set;}
		
		public int ValueType {get; set;}
		
		public string EditValue1 {get; set;}
		
		public string EditValue2 {get; set;}
		
		public int StatusResult {get; set;}
		
		public bool IsConnected {get; set;}
		
		public int Comport {get; set;}
		
		public int ProductID {get; set;}
		
		public string PackageNumber {get; set;}
		
		public string QtyInPackage {get; set;}
		
		public string Approved {get; set;}
		
		public string Monitor {get; set;}
		
		public DateTime? DateLR {get; set;}
		
		public string EditContent {get; set;}
		
		public DateTime? EditDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	