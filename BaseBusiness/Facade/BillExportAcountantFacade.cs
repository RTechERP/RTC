
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillExportAcountantFacade : BaseFacade
	{
		protected static BillExportAcountantFacade instance = new BillExportAcountantFacade(new BillExportAcountantModel());
		protected BillExportAcountantFacade(BillExportAcountantModel model) : base(model)
		{
		}
		public static BillExportAcountantFacade Instance
		{
			get { return instance; }
		}
		protected BillExportAcountantFacade():base() 
		{ 
		} 
	
	}
}
	