
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeErrorFacade : BaseFacade
	{
		protected static EmployeeErrorFacade instance = new EmployeeErrorFacade(new EmployeeErrorModel());
		protected EmployeeErrorFacade(EmployeeErrorModel model) : base(model)
		{
		}
		public static EmployeeErrorFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeErrorFacade():base() 
		{ 
		} 
	
	}
}
	