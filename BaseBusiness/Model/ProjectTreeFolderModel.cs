
using System;
namespace BMS.Model
{
	public partial class ProjectTreeFolderModel : BaseModel
	{
		public int ID {get; set;}
		public string FolderName {get; set;}
		public int ParentID {get; set;}
        public int ProjectTypeID { get; set; }

    }
}
	