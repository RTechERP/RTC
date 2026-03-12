
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeWorkingProcessFacade : BaseFacade
	{
		protected static EmployeeWorkingProcessFacade instance = new EmployeeWorkingProcessFacade(new EmployeeWorkingProcessModel());
		protected EmployeeWorkingProcessFacade(EmployeeWorkingProcessModel model) : base(model)
		{
		}
		public static EmployeeWorkingProcessFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeWorkingProcessFacade():base() 
		{ 
		} 
	
	}
}
	