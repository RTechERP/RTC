
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class VehicleManagementFacade : BaseFacade
	{
		protected static VehicleManagementFacade instance = new VehicleManagementFacade(new VehicleManagementModel());
		protected VehicleManagementFacade(VehicleManagementModel model) : base(model)
		{
		}
		public static VehicleManagementFacade Instance
		{
			get { return instance; }
		}
		protected VehicleManagementFacade():base() 
		{ 
		} 
	
	}
}
	