
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeVehicleBussinessFacade : BaseFacade
	{
		protected static EmployeeVehicleBussinessFacade instance = new EmployeeVehicleBussinessFacade(new EmployeeVehicleBussinessModel());
		protected EmployeeVehicleBussinessFacade(EmployeeVehicleBussinessModel model) : base(model)
		{
		}
		public static EmployeeVehicleBussinessFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeVehicleBussinessFacade():base() 
		{ 
		} 
	
	}
}
	