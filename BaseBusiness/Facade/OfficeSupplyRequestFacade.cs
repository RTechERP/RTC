
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class OfficeSupplyRequestFacade : BaseFacade
	{
		protected static OfficeSupplyRequestFacade instance = new OfficeSupplyRequestFacade(new OfficeSupplyRequestModel());
		protected OfficeSupplyRequestFacade(OfficeSupplyRequestModel model) : base(model)
		{
		}
		public static OfficeSupplyRequestFacade Instance
		{
			get { return instance; }
		}
		protected OfficeSupplyRequestFacade():base() 
		{ 
		} 
	
	}
}
	