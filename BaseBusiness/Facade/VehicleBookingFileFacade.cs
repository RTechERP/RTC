
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class VehicleBookingFileFacade : BaseFacade
	{
		protected static VehicleBookingFileFacade instance = new VehicleBookingFileFacade(new VehicleBookingFileModel());
		protected VehicleBookingFileFacade(VehicleBookingFileModel model) : base(model)
		{
		}
		public static VehicleBookingFileFacade Instance
		{
			get { return instance; }
		}
		protected VehicleBookingFileFacade():base() 
		{ 
		} 
	
	}
}
	