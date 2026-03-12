
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeePurchaseFacade : BaseFacade
	{
		protected static EmployeePurchaseFacade instance = new EmployeePurchaseFacade(new EmployeePurchaseModel());
		protected EmployeePurchaseFacade(EmployeePurchaseModel model) : base(model)
		{
		}
		public static EmployeePurchaseFacade Instance
		{
			get { return instance; }
		}
		protected EmployeePurchaseFacade():base() 
		{ 
		} 
	
	}
}
	