
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class WorkPlanBO : BaseBO
	{
		private WorkPlanFacade facade = WorkPlanFacade.Instance;
		protected static WorkPlanBO instance = new WorkPlanBO();

		protected WorkPlanBO()
		{
			this.baseFacade = facade;
		}

		public static WorkPlanBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	