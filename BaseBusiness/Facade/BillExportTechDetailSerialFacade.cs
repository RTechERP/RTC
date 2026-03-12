
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillExportTechDetailSerialFacade : BaseFacade
	{
		protected static BillExportTechDetailSerialFacade instance = new BillExportTechDetailSerialFacade(new BillExportTechDetailSerialModel());
		protected BillExportTechDetailSerialFacade(BillExportTechDetailSerialModel model) : base(model)
		{
		}
		public static BillExportTechDetailSerialFacade Instance
		{
			get { return instance; }
		}
		protected BillExportTechDetailSerialFacade():base() 
		{ 
		} 
	
	}
}
	