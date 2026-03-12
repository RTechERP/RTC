
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectFacade : BaseFacade
	{
		protected static ProjectFacade instance = new ProjectFacade(new ProjectModel());
		protected ProjectFacade(ProjectModel model) : base(model)
		{
		}
		public static ProjectFacade Instance
		{
			get { return instance; }
		}
		protected ProjectFacade():base() 
		{ 
		} 
	
	}
}
	