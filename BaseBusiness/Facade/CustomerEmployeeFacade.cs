
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CustomerEmployeeFacade : BaseFacade
	{
		protected static CustomerEmployeeFacade instance = new CustomerEmployeeFacade(new CustomerEmployeeModel());
		protected CustomerEmployeeFacade(CustomerEmployeeModel model) : base(model)
		{
		}
		public static CustomerEmployeeFacade Instance
		{
			get { return instance; }
		}
		protected CustomerEmployeeFacade():base() 
		{ 
		} 
	
	}
}
	