
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeWFHFacade : BaseFacade
	{
		protected static EmployeeWFHFacade instance = new EmployeeWFHFacade(new EmployeeWFHModel());
		protected EmployeeWFHFacade(EmployeeWFHModel model) : base(model)
		{
		}
		public static EmployeeWFHFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeWFHFacade():base() 
		{ 
		} 
	
	}
}
	