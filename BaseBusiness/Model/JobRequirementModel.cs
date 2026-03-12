using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class JobRequirementModel : BaseModel
    {
        public int ID { get; set; }
        public string NumberRequest { get; set; }
        public DateTime? DateRequest { get; set; }
        public DateTime? DeadlineRequest { get; set; }
        public int? EmployeeID { get; set; }
        public int? CoordinationDepartmentID { get; set; }
        public int? RequiredDepartmentID { get; set; }
        public bool? IsApprovedTBP { get; set; }
        public DateTime? DateApprovedTBP { get; set; }
        public int? ApprovedTBPID { get; set; }
        public bool? IsApprovedHR { get; set; }
        public DateTime? DateApprovedHR { get; set; }
        public int? ApprovedHRID { get; set; }
        public bool? IsApprovedBGD { get; set; }
        public DateTime? DateApprovedBGD { get; set; }
        public int? ApprovedBGDID { get; set; }
        public string EvaluateCompletion { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsRequestBuy { get; set; }
        public int? Status { get; set; }
        public string Note { get; set; }
        public bool? IsRequestBGDApproved { get; set; }
        public bool? IsRequestPriceQuote { get; set; }
    }
    public enum JobRequirementModel_Enum
    {
        ID,
        NumberRequest,
        DateRequest,
        DeadlineRequest,
        EmployeeID,
        CoordinationDepartmentID,
        RequiredDepartmentID,
        IsApprovedTBP,
        DateApprovedTBP,
        ApprovedTBPID,
        IsApprovedHR,
        DateApprovedHR,
        ApprovedHRID,
        IsApprovedBGD,
        DateApprovedBGD,
        ApprovedBGDID,
        EvaluateCompletion,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsRequestBuy,
        Status,
        Note,
        IsRequestBGDApproved,
        IsRequestPriceQuote
    }
}
