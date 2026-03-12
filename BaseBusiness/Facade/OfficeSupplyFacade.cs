
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class OfficeSupplyFacade : BaseFacade
	{
		protected static OfficeSupplyFacade instance = new OfficeSupplyFacade(new OfficeSupplyModel());
		protected OfficeSupplyFacade(OfficeSupplyModel model) : base(model)
		{
		}
		public static OfficeSupplyFacade Instance
		{
			get { return instance; }
		}
		protected OfficeSupplyFacade():base() 
		{ 
		} 
	
	}
}
	