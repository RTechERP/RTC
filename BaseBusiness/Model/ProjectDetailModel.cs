
using System;
namespace BMS.Model
{
	public partial class ProjectDetailModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ProjectID {get; set;}
		
		public int STT {get; set;}
		
		public int GroupFileID {get; set;}
		
		public string FileName {get; set;}
		
		public string PathShort {get; set;}
		
		public string PathFull {get; set;}
		
		public string Note {get; set;}
		
	}
}
	