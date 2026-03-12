
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class VehicleCategoryFacade : BaseFacade
	{
		protected static VehicleCategoryFacade instance = new VehicleCategoryFacade(new VehicleCategoryModel());
		protected VehicleCategoryFacade(VehicleCategoryModel model) : base(model)
		{
		}
		public static VehicleCategoryFacade Instance
		{
			get { return instance; }
		}
		protected VehicleCategoryFacade():base() 
		{ 
		} 
	
	}
}
	