
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class WeekPlanFacade : BaseFacade
	{
		protected static WeekPlanFacade instance = new WeekPlanFacade(new WeekPlanModel());
		protected WeekPlanFacade(WeekPlanModel model) : base(model)
		{
		}
		public static WeekPlanFacade Instance
		{
			get { return instance; }
		}
		protected WeekPlanFacade():base() 
		{ 
		} 
	
	}
}
	