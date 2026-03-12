
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectStatusBaseFacade : BaseFacade
	{
		protected static ProjectStatusBaseFacade instance = new ProjectStatusBaseFacade(new ProjectStatusBaseModel());
		protected ProjectStatusBaseFacade(ProjectStatusBaseModel model) : base(model)
		{
		}
		public static ProjectStatusBaseFacade Instance
		{
			get { return instance; }
		}
		protected ProjectStatusBaseFacade():base() 
		{ 
		} 
	
	}
}
	