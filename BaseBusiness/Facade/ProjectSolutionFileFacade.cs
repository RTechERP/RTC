
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectSolutionFileFacade : BaseFacade
	{
		protected static ProjectSolutionFileFacade instance = new ProjectSolutionFileFacade(new ProjectSolutionFileModel());
		protected ProjectSolutionFileFacade(ProjectSolutionFileModel model) : base(model)
		{
		}
		public static ProjectSolutionFileFacade Instance
		{
			get { return instance; }
		}
		protected ProjectSolutionFileFacade():base() 
		{ 
		} 
	
	}
}
	