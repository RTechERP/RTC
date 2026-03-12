
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class SALEFacade : BaseFacade
	{
		protected static SALEFacade instance = new SALEFacade(new SALEModel());
		protected SALEFacade(SALEModel model) : base(model)
		{
		}
		public static SALEFacade Instance
		{
			get { return instance; }
		}
		protected SALEFacade():base() 
		{ 
		} 
	
	}
}
	