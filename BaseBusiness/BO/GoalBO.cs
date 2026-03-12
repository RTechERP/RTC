
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class GoalBO : BaseBO
	{
		private GoalFacade facade = GoalFacade.Instance;
		protected static GoalBO instance = new GoalBO();

		protected GoalBO()
		{
			this.baseFacade = facade;
		}

		public static GoalBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	