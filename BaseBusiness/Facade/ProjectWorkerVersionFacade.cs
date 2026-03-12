
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectWorkerVersionFacade : BaseFacade
	{
		protected static ProjectWorkerVersionFacade instance = new ProjectWorkerVersionFacade(new ProjectWorkerVersionModel());
		protected ProjectWorkerVersionFacade(ProjectWorkerVersionModel model) : base(model)
		{
		}
		public static ProjectWorkerVersionFacade Instance
		{
			get { return instance; }
		}
		protected ProjectWorkerVersionFacade():base() 
		{ 
		} 
	
	}
}
	