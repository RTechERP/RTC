
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class VehicleManagementBO : BaseBO
	{
		private VehicleManagementFacade facade = VehicleManagementFacade.Instance;
		protected static VehicleManagementBO instance = new VehicleManagementBO();

		protected VehicleManagementBO()
		{
			this.baseFacade = facade;
		}

		public static VehicleManagementBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	