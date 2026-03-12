
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectTreeFolderFacade : BaseFacade
	{
		protected static ProjectTreeFolderFacade instance = new ProjectTreeFolderFacade(new ProjectTreeFolderModel());
		protected ProjectTreeFolderFacade(ProjectTreeFolderModel model) : base(model)
		{
		}
		public static ProjectTreeFolderFacade Instance
		{
			get { return instance; }
		}
		protected ProjectTreeFolderFacade():base() 
		{ 
		} 
	
	}
}
	