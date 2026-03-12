
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class POKHFacade : BaseFacade
	{
		protected static POKHFacade instance = new POKHFacade(new POKHModel());
		protected POKHFacade(POKHModel model) : base(model)
		{
		}
		public static POKHFacade Instance
		{
			get { return instance; }
		}
		protected POKHFacade():base() 
		{ 
		} 
	
	}
}
	