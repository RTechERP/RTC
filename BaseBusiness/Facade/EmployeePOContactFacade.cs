
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeePOContactFacade : BaseFacade
	{
		protected static EmployeePOContactFacade instance = new EmployeePOContactFacade(new EmployeePOContactModel());
		protected EmployeePOContactFacade(EmployeePOContactModel model) : base(model)
		{
		}
		public static EmployeePOContactFacade Instance
		{
			get { return instance; }
		}
		protected EmployeePOContactFacade():base() 
		{ 
		} 
	
	}
}
	