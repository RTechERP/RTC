
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class GoalFacade : BaseFacade
	{
		protected static GoalFacade instance = new GoalFacade(new GoalModel());
		protected GoalFacade(GoalModel model) : base(model)
		{
		}
		public static GoalFacade Instance
		{
			get { return instance; }
		}
		protected GoalFacade():base() 
		{ 
		} 
	
	}
}
	