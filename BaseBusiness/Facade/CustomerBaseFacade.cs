
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CustomerBaseFacade : BaseFacade
	{
		protected static CustomerBaseFacade instance = new CustomerBaseFacade(new CustomerBaseModel());
		protected CustomerBaseFacade(CustomerBaseModel model) : base(model)
		{
		}
		public static CustomerBaseFacade Instance
		{
			get { return instance; }
		}
		protected CustomerBaseFacade():base() 
		{ 
		} 
	
	}
}
	