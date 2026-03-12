
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeVehicleBussinessBO : BaseBO
	{
		private EmployeeVehicleBussinessFacade facade = EmployeeVehicleBussinessFacade.Instance;
		protected static EmployeeVehicleBussinessBO instance = new EmployeeVehicleBussinessBO();

		protected EmployeeVehicleBussinessBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeVehicleBussinessBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	