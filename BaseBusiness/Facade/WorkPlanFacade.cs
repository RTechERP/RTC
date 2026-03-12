
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class WorkPlanFacade : BaseFacade
	{
		protected static WorkPlanFacade instance = new WorkPlanFacade(new WorkPlanModel());
		protected WorkPlanFacade(WorkPlanModel model) : base(model)
		{
		}
		public static WorkPlanFacade Instance
		{
			get { return instance; }
		}
		protected WorkPlanFacade():base() 
		{ 
		} 
	
	}
}
	