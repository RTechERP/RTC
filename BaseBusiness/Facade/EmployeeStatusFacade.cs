
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeStatusFacade : BaseFacade
	{
		protected static EmployeeStatusFacade instance = new EmployeeStatusFacade(new EmployeeStatusModel());
		protected EmployeeStatusFacade(EmployeeStatusModel model) : base(model)
		{
		}
		public static EmployeeStatusFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeStatusFacade():base() 
		{ 
		} 
	
	}
}
	