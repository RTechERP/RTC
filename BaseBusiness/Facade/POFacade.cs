
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class POFacade : BaseFacade
	{
		protected static POFacade instance = new POFacade(new POModel());
		protected POFacade(POModel model) : base(model)
		{
		}
		public static POFacade Instance
		{
			get { return instance; }
		}
		protected POFacade():base() 
		{ 
		} 
	
	}
}
	