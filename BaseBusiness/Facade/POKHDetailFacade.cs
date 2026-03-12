
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class POKHDetailFacade : BaseFacade
	{
		protected static POKHDetailFacade instance = new POKHDetailFacade(new POKHDetailModel());
		protected POKHDetailFacade(POKHDetailModel model) : base(model)
		{
		}
		public static POKHDetailFacade Instance
		{
			get { return instance; }
		}
		protected POKHDetailFacade():base() 
		{ 
		} 
	
	}
}
	