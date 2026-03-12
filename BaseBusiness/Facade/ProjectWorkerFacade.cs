
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectWorkerFacade : BaseFacade
	{
		protected static ProjectWorkerFacade instance = new ProjectWorkerFacade(new ProjectWorkerModel());
		protected ProjectWorkerFacade(ProjectWorkerModel model) : base(model)
		{
		}
		public static ProjectWorkerFacade Instance
		{
			get { return instance; }
		}
		protected ProjectWorkerFacade():base() 
		{ 
		} 
	
	}
}
	