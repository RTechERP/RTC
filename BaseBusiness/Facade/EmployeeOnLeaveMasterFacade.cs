
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeOnLeaveMasterFacade : BaseFacade
	{
		protected static EmployeeOnLeaveMasterFacade instance = new EmployeeOnLeaveMasterFacade(new EmployeeOnLeaveMasterModel());
		protected EmployeeOnLeaveMasterFacade(EmployeeOnLeaveMasterModel model) : base(model)
		{
		}
		public static EmployeeOnLeaveMasterFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeOnLeaveMasterFacade():base() 
		{ 
		} 
	
	}
}
	