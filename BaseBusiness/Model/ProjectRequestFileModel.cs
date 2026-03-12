
using System;
namespace BMS.Model
{
	public partial class ProjectRequestFileModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ProjectRequestID {get; set;}
		
		public string FileNameOrigin { get; set;}
		public string FileName {get; set;}
		public string Extension { get; set;}
		
		public string OriginPath {get; set;}
		
		public string ServerPath {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	