
using System;
namespace BMS.Model
{
	public partial class RequestBuyRTCModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ParentID {get; set;}
		
		public int POKHDetailID {get; set;}
		
		public int NguoiYeuCauID {get; set;}
		
		public int PhongBanID {get; set;}
		
		public DateTime? NgayNhanYeuCau {get; set;}
		
		public int ProjectID {get; set;}
		
		public int ProductID {get; set;}
		
		public int PONCCID {get; set;}
		
		public DateTime? NgayYeuCauGiao {get; set;}
		
		public string Unit {get; set;}
		
		public int Qty {get; set;}
		
		public int QtyReal {get; set;}
		
		public decimal DonGiaNhap {get; set;}
		
		public string ProductCode_ {get; set;}
		
		public string ProductName_ {get; set;}
		
		public string GuestCode_ {get; set;}
		
		public decimal Vat {get; set;}
		
		public string MaSPMua {get; set;}
		
		public string TenSPMua {get; set;}
		
		public decimal ThanhTien {get; set;}
		
		public int SupplierID {get; set;}
		
		public string CongNo {get; set;}
		
		public DateTime? HanTT {get; set;}
		
		public bool TinhTrangTT {get; set;}
		
		public DateTime? NgayDatHang {get; set;}
		
		public DateTime? NgayDuKienVe {get; set;}
		
		public DateTime? NgayVeThucTe {get; set;}
		
		public int TinhTrangDonHang {get; set;}
		
		public string HoaDon {get; set;}
		
		public string GhiChu {get; set;}
		
		public bool IsApproved_Level1 {get; set;}
		
		public bool IsApproved_Level2 {get; set;}
		
		public bool IsApproved_Level3 {get; set;}
		
		public int IsApproved_Level1_PeopleID {get; set;}
		
		public int IsApproved_Level2_PeopleID {get; set;}
		
		public int IsApproved_Level3_PeopleID {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
        public decimal PriceSale { get; set; }

    }
}
	