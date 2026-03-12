
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class OfficeSupplyRequestsDetailFacade : BaseFacade
	{
		protected static OfficeSupplyRequestsDetailFacade instance = new OfficeSupplyRequestsDetailFacade(new OfficeSupplyRequestsDetailModel());
		protected OfficeSupplyRequestsDetailFacade(OfficeSupplyRequestsDetailModel model) : base(model)
		{
		}
		public static OfficeSupplyRequestsDetailFacade Instance
		{
			get { return instance; }
		}
		protected OfficeSupplyRequestsDetailFacade():base() 
		{ 
		} 
	
	}
}
	