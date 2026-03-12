
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeEarlyLateFacade : BaseFacade
	{
		protected static EmployeeEarlyLateFacade instance = new EmployeeEarlyLateFacade(new EmployeeEarlyLateModel());
		protected EmployeeEarlyLateFacade(EmployeeEarlyLateModel model) : base(model)
		{
		}
		public static EmployeeEarlyLateFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeEarlyLateFacade():base() 
		{ 
		} 
	
	}
}
	