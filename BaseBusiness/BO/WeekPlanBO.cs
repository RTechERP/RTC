
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class WeekPlanBO : BaseBO
	{
		private WeekPlanFacade facade = WeekPlanFacade.Instance;
		protected static WeekPlanBO instance = new WeekPlanBO();

		protected WeekPlanBO()
		{
			this.baseFacade = facade;
		}

		public static WeekPlanBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	