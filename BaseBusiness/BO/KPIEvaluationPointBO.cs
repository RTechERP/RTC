
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class KPIEvaluationPointBO : BaseBO
	{
		private KPIEvaluationPointFacade facade = KPIEvaluationPointFacade.Instance;
		protected static KPIEvaluationPointBO instance = new KPIEvaluationPointBO();

		protected KPIEvaluationPointBO()
		{
			this.baseFacade = facade;
		}

		public static KPIEvaluationPointBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	