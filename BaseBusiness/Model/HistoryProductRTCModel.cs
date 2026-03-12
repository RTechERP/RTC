
using System;
namespace BMS.Model
{
	public partial class HistoryProductRTCModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ProductRTCID {get; set;}
		
		public DateTime? DateBorrow {get; set;}
		
		public DateTime? DateReturnExpected {get; set;}
		
		public int PeopleID {get; set;}
		
		public string Project {get; set;}
		
		public DateTime? DateReturn {get; set;}
		
		public string Note {get; set;}
		
		public int Status {get; set;}
		
		public decimal NumberBorrow {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public bool AdminConfirm {get; set;}
        public int BillExportTechnicalID { get; set; }
        public int ProductRTCQRCodeID { get; set; }
        public string ProductRTCQRCode { get; set; }
		public int WarehouseID { get; set; }
        public bool IsDelete { get; set; }
        public int StatusPerson { get; set; }
    }
}
	