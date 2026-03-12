
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class WorkPlanDetailBO : BaseBO
	{
		private WorkPlanDetailFacade facade = WorkPlanDetailFacade.Instance;
		protected static WorkPlanDetailBO instance = new WorkPlanDetailBO();

		protected WorkPlanDetailBO()
		{
			this.baseFacade = facade;
		}

		public static WorkPlanDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	