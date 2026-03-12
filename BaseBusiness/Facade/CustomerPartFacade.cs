
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CustomerPartFacade : BaseFacade
	{
		protected static CustomerPartFacade instance = new CustomerPartFacade(new CustomerPartModel());
		protected CustomerPartFacade(CustomerPartModel model) : base(model)
		{
		}
		public static CustomerPartFacade Instance
		{
			get { return instance; }
		}
		protected CustomerPartFacade():base() 
		{ 
		} 
	
	}
}
	