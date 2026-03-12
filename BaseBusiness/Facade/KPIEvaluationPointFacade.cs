
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class KPIEvaluationPointFacade : BaseFacade
	{
		protected static KPIEvaluationPointFacade instance = new KPIEvaluationPointFacade(new KPIEvaluationPointModel());
		protected KPIEvaluationPointFacade(KPIEvaluationPointModel model) : base(model)
		{
		}
		public static KPIEvaluationPointFacade Instance
		{
			get { return instance; }
		}
		protected KPIEvaluationPointFacade():base() 
		{ 
		} 
	
	}
}
	