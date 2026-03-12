
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class POKHDetailMoneyFacade : BaseFacade
	{
		protected static POKHDetailMoneyFacade instance = new POKHDetailMoneyFacade(new POKHDetailMoneyModel());
		protected POKHDetailMoneyFacade(POKHDetailMoneyModel model) : base(model)
		{
		}
		public static POKHDetailMoneyFacade Instance
		{
			get { return instance; }
		}
		protected POKHDetailMoneyFacade():base() 
		{ 
		} 
	
	}
}
	