
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeBussinessVehicleBO : BaseBO
	{
		private EmployeeBussinessVehicleFacade facade = EmployeeBussinessVehicleFacade.Instance;
		protected static EmployeeBussinessVehicleBO instance = new EmployeeBussinessVehicleBO();

		protected EmployeeBussinessVehicleBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeBussinessVehicleBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	