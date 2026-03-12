
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectStatusDetailFacade : BaseFacade
	{
		protected static ProjectStatusDetailFacade instance = new ProjectStatusDetailFacade(new ProjectStatusDetailModel());
		protected ProjectStatusDetailFacade(ProjectStatusDetailModel model) : base(model)
		{
		}
		public static ProjectStatusDetailFacade Instance
		{
			get { return instance; }
		}
		protected ProjectStatusDetailFacade():base() 
		{ 
		} 
	
	}
}
	