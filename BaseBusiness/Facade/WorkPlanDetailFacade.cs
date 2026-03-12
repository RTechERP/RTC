
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class WorkPlanDetailFacade : BaseFacade
	{
		protected static WorkPlanDetailFacade instance = new WorkPlanDetailFacade(new WorkPlanDetailModel());
		protected WorkPlanDetailFacade(WorkPlanDetailModel model) : base(model)
		{
		}
		public static WorkPlanDetailFacade Instance
		{
			get { return instance; }
		}
		protected WorkPlanDetailFacade():base() 
		{ 
		} 
	
	}
}
	