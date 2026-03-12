
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CustomerBaseContactFacade : BaseFacade
	{
		protected static CustomerBaseContactFacade instance = new CustomerBaseContactFacade(new CustomerBaseContactModel());
		protected CustomerBaseContactFacade(CustomerBaseContactModel model) : base(model)
		{
		}
		public static CustomerBaseContactFacade Instance
		{
			get { return instance; }
		}
		protected CustomerBaseContactFacade():base() 
		{ 
		} 
	
	}
}
	