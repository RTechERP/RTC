
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillExportTechnicalLogFacade : BaseFacade
	{
		protected static BillExportTechnicalLogFacade instance = new BillExportTechnicalLogFacade(new BillExportTechnicalLogModel());
		protected BillExportTechnicalLogFacade(BillExportTechnicalLogModel model) : base(model)
		{
		}
		public static BillExportTechnicalLogFacade Instance
		{
			get { return instance; }
		}
		protected BillExportTechnicalLogFacade():base() 
		{ 
		} 
	
	}
}
	