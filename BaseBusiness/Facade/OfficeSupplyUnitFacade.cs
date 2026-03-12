
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class OfficeSupplyUnitFacade : BaseFacade
	{
		protected static OfficeSupplyUnitFacade instance = new OfficeSupplyUnitFacade(new OfficeSupplyUnitModel());
		protected OfficeSupplyUnitFacade(OfficeSupplyUnitModel model) : base(model)
		{
		}
		public static OfficeSupplyUnitFacade Instance
		{
			get { return instance; }
		}
		protected OfficeSupplyUnitFacade():base() 
		{ 
		} 
	
	}
}
	