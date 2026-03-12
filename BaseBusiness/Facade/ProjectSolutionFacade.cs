
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectSolutionFacade : BaseFacade
	{
		protected static ProjectSolutionFacade instance = new ProjectSolutionFacade(new ProjectSolutionModel());
		protected ProjectSolutionFacade(ProjectSolutionModel model) : base(model)
		{
		}
		public static ProjectSolutionFacade Instance
		{
			get { return instance; }
		}
		protected ProjectSolutionFacade():base() 
		{ 
		} 
	
	}
}
	