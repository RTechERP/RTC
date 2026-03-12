
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class KPIDetailFacade : BaseFacade
	{
		protected static KPIDetailFacade instance = new KPIDetailFacade(new KPIDetailModel());
		protected KPIDetailFacade(KPIDetailModel model) : base(model)
		{
		}
		public static KPIDetailFacade Instance
		{
			get { return instance; }
		}
		protected KPIDetailFacade():base() 
		{ 
		} 
	
	}
}
	