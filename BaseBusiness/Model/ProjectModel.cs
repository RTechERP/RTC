
using System;
namespace BMS.Model
{
	public partial class ProjectModel : BaseModel
	{
		public int ID {get; set;}
		
		public int CustomerID {get; set;}
		
		public string ProjectCode {get; set;}
		
		public string ProjectName {get; set;}
		
		public string ProjectShortName {get; set;}
		
		public int ProjectStatus {get; set;}
		
		public int UserID {get; set;}
		
		public int UserTechnicalID {get; set;}
		
		public string Note {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public bool IsApproved {get; set;}
		
		public int ContactID {get; set;}
		
		public string PO {get; set;}
		
		public int ProjectType {get; set;}
		
		public int ListCostID {get; set;}
		  
		public DateTime? PlanDateStart {get; set;}
		
		public DateTime? PlanDateEnd {get; set;}
		
		public DateTime? ActualDateStart {get; set;}
		
		public DateTime? ActualDateEnd { get; set;}
        public string EU { get; set; }
        public int ProjectManager { get; set; }
        public string CurrentState { get; set; }
        public decimal Priotity { get; set; }
        public DateTime? PODate { get; set; }
        public int EndUser { get; set; }
        public int BusinessFieldID { get; set; }
        public int TypeProject { get; set; }



    }
}
	