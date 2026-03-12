
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeFoodOrderFacade : BaseFacade
	{
		protected static EmployeeFoodOrderFacade instance = new EmployeeFoodOrderFacade(new EmployeeFoodOrderModel());
		protected EmployeeFoodOrderFacade(EmployeeFoodOrderModel model) : base(model)
		{
		}
		public static EmployeeFoodOrderFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeFoodOrderFacade():base() 
		{ 
		} 
	
	}
}
	