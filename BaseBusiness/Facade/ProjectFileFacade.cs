
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectFileFacade : BaseFacade
	{
		protected static ProjectFileFacade instance = new ProjectFileFacade(new ProjectFileModel());
		protected ProjectFileFacade(ProjectFileModel model) : base(model)
		{
		}
		public static ProjectFileFacade Instance
		{
			get { return instance; }
		}
		protected ProjectFileFacade():base() 
		{ 
		} 
	
	}
}
	