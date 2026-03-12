
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class OfficeSupplyRequestsFacade : BaseFacade
	{
		protected static OfficeSupplyRequestsFacade instance = new OfficeSupplyRequestsFacade(new OfficeSupplyRequestsModel());
		protected OfficeSupplyRequestsFacade(OfficeSupplyRequestsModel model) : base(model)
		{
		}
		public static OfficeSupplyRequestsFacade Instance
		{
			get { return instance; }
		}
		protected OfficeSupplyRequestsFacade():base() 
		{ 
		} 
	
	}
}
	