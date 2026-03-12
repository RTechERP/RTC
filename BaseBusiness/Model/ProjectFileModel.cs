
using System;
namespace BMS.Model
{
	public partial class ProjectFileModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ProjectID {get; set;}
		
		public string FileName {get; set;}
		
		public string FileType {get; set;}
		
		public decimal Size {get; set;}
		
		public string OriginPath {get; set;}
		
		public string Note {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public int FileTypeFolder {get; set;}
        public string PathServer { get; set; }

    }
}
	