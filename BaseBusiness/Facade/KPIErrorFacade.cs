
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class KPIErrorFacade : BaseFacade
	{
		protected static KPIErrorFacade instance = new KPIErrorFacade(new KPIErrorModel());
		protected KPIErrorFacade(KPIErrorModel model) : base(model)
		{
		}
		public static KPIErrorFacade Instance
		{
			get { return instance; }
		}
		protected KPIErrorFacade():base() 
		{ 
		} 
	
	}
}
	