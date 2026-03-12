using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBusiness.DTO
{
    public class spGetProjectPartlistPurchaseRequest_New_Khanh
    {
        public int ID { get; set; }
        public decimal TotalBillImport { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ProjectFullName { get; set; }
        public string TT { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int StatusRequest { get; set; }
        public string StatusRequestText { get; set; }
        public string FullName { get; set; }
        public DateTime? DateRequest { get; set; }
        public DateTime? DateReturnExpected { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? DeadlineDelivery { get; set; }
        public DateTime? DateReturnActual { get; set; }
        public DateTime? DateReceive { get; set; }
        public string Note { get; set; }
        public string NameNCC { get; set; }
        public int ProjectPartListID { get; set; }
        public int SupplierSaleID { get; set; }
        public string UnitMoney { get; set; }
        public bool IsApprovedTBP { get; set; }
        public bool IsApprovedBGD { get; set; }
        public int ApprovedTBP { get; set; }
        public string ApprovedTBPName { get; set; }
        public int ApprovedBGD { get; set; }
        public string ApprovedBGDName { get; set; }
        public DateTime? DateApprovedTBP { get; set; }
        public DateTime? DateApprovedBGD { get; set; }
        public int ProductGroupID { get; set; }
        public int ProductSaleID { get; set; }
        public string ProductNewCode { get; set; }
        public bool IsImport { get; set; }
        public int CurrencyID { get; set; }
        public decimal CurrencyRate { get; set; }
        public decimal TotalPriceExchange { get; set; }
        public string LeadTime { get; set; }
        public decimal UnitFactoryExportPrice { get; set; }
        public decimal UnitImportPrice { get; set; }
        public decimal TotalImportPrice { get; set; }
        public bool IsRequestApproved { get; set; }
        public string Manufacturer { get; set; }
        public string ReasonCancel { get; set; }
        public int ProjectID { get; set; }
        public string UpdatedName { get; set; }
        public decimal VAT { get; set; }
        public decimal TotaMoneyVAT { get; set; }
        public int TotalDayLeadTime { get; set; }
        public int PONCCID { get; set; }
        public string BillCode { get; set; }
        public decimal UnitPricePOKH { get; set; }
        public bool IsCommercialProduct { get; set; }
        public bool IsDeleted { get; set; }
        public string Model { get; set; }
        public int JobRequirementID { get; set; }
        public int CustomerID { get; set; }
        public string ProjectTypeName { get; set; }
        public string CustomerName { get; set; }
        public string PONumber { get; set; }
        public string GuestCode { get; set; }
        public string CustomerCode { get; set; }
        public string POKHCode { get; set; }
        public string StatusPOKHText { get; set; }
        public string SpecialCode { get; set; }
        public decimal HistoryPrice { get; set; }
        public decimal TotalPriceHistory { get; set; }
        public bool IsTechBought { get; set; }
        public string IsApprovedBGDText { get; set; }
        public int ProductGroupRTCID { get; set; }
        public int ProductRTCID { get; set; }
        public string ProductNewCodeSale { get; set; }
        public string NotePartlist { get; set; }
        public int TicketType { get; set; }
        public string TicketTypeText { get; set; }
        public DateTime? DateReturnEstimated { get; set; }
        public bool IsStock { get; set; }
        public string ProductCodeRTC { get; set; }
        public decimal TotalBillImportCount { get; set; }
        public int ProjectPartlistPurchaseRequestTypeID { get; set; }
        public int UnitCountID { get; set; }
        public string NoteMarketing { get; set; }
        public string UnitName { get; set; }
        public string FullNamePriceRequest { get; set; }
        public string UnitNameNew { get; set; }
        public string UnitPricetext { get; set; }
        public int POKHDetailID { get; set; }
        public int InventoryProjectID { get; set; }
        public decimal TargetPrice { get; set; }
        public int DuplicateID { get; set; }
        public decimal OriginQuantity { get; set; }
        public string TotalHN { get; set; }
        public string TotalHCM { get; set; }
        public string TotalBN { get; set; }
        public string TotalHP { get; set; }
        public string TotalBH { get; set; }
        public string TotalDP { get; set; }
        public string ParentProductCode { get; set; }
        public string ParentProductCodePO { get; set; }
    }
}
