
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeBussinessVehicleFacade : BaseFacade
	{
		protected static EmployeeBussinessVehicleFacade instance = new EmployeeBussinessVehicleFacade(new EmployeeBussinessVehicleModel());
		protected EmployeeBussinessVehicleFacade(EmployeeBussinessVehicleModel model) : base(model)
		{
		}
		public static EmployeeBussinessVehicleFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeBussinessVehicleFacade():base() 
		{ 
		} 
	
	}
}
	