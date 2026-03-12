
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillExportAcountantDetailFacade : BaseFacade
	{
		protected static BillExportAcountantDetailFacade instance = new BillExportAcountantDetailFacade(new BillExportAcountantDetailModel());
		protected BillExportAcountantDetailFacade(BillExportAcountantDetailModel model) : base(model)
		{
		}
		public static BillExportAcountantDetailFacade Instance
		{
			get { return instance; }
		}
		protected BillExportAcountantDetailFacade():base() 
		{ 
		} 
	
	}
}
	