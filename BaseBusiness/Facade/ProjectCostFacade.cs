
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectCostFacade : BaseFacade
	{
		protected static ProjectCostFacade instance = new ProjectCostFacade(new ProjectCostModel());
		protected ProjectCostFacade(ProjectCostModel model) : base(model)
		{
		}
		public static ProjectCostFacade Instance
		{
			get { return instance; }
		}
		protected ProjectCostFacade():base() 
		{ 
		} 
	
	}
}
	