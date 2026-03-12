
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectItemLogFacade : BaseFacade
	{
		protected static ProjectItemLogFacade instance = new ProjectItemLogFacade(new ProjectItemLogModel());
		protected ProjectItemLogFacade(ProjectItemLogModel model) : base(model)
		{
		}
		public static ProjectItemLogFacade Instance
		{
			get { return instance; }
		}
		protected ProjectItemLogFacade():base() 
		{ 
		} 
	
	}
}
	