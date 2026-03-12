
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class KPIDetailUserFacade : BaseFacade
	{
		protected static KPIDetailUserFacade instance = new KPIDetailUserFacade(new KPIDetailUserModel());
		protected KPIDetailUserFacade(KPIDetailUserModel model) : base(model)
		{
		}
		public static KPIDetailUserFacade Instance
		{
			get { return instance; }
		}
		protected KPIDetailUserFacade():base() 
		{ 
		} 
	
	}
}
	