
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CustomerContactFacade : BaseFacade
	{
		protected static CustomerContactFacade instance = new CustomerContactFacade(new CustomerContactModel());
		protected CustomerContactFacade(CustomerContactModel model) : base(model)
		{
		}
		public static CustomerContactFacade Instance
		{
			get { return instance; }
		}
		protected CustomerContactFacade():base() 
		{ 
		} 
	
	}
}
	