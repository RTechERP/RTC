
using System;
namespace BMS.Model
{
	public partial class DocumentFileModel : BaseModel
	{
		public int ID {get; set;}
		
		public string FileName {get; set;}
		
		public string FilePath {get; set;}
		
		public int DocumentID {get; set;}
		
		public string FileNameOrigin {get; set;}
		
	}
}
	