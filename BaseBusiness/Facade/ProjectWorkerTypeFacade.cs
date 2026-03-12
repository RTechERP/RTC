
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectWorkerTypeFacade : BaseFacade
	{
		protected static ProjectWorkerTypeFacade instance = new ProjectWorkerTypeFacade(new ProjectWorkerTypeModel());
		protected ProjectWorkerTypeFacade(ProjectWorkerTypeModel model) : base(model)
		{
		}
		public static ProjectWorkerTypeFacade Instance
		{
			get { return instance; }
		}
		protected ProjectWorkerTypeFacade():base() 
		{ 
		} 
	
	}
}
	