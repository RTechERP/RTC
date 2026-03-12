
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillImportTechDetailSerialFacade : BaseFacade
	{
		protected static BillImportTechDetailSerialFacade instance = new BillImportTechDetailSerialFacade(new BillImportTechDetailSerialModel());
		protected BillImportTechDetailSerialFacade(BillImportTechDetailSerialModel model) : base(model)
		{
		}
		public static BillImportTechDetailSerialFacade Instance
		{
			get { return instance; }
		}
		protected BillImportTechDetailSerialFacade():base() 
		{ 
		} 
	
	}
}
	