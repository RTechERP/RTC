
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillDocumentExportTechnicalLogFacade : BaseFacade
	{
		protected static BillDocumentExportTechnicalLogFacade instance = new BillDocumentExportTechnicalLogFacade(new BillDocumentExportTechnicalLogModel());
		protected BillDocumentExportTechnicalLogFacade(BillDocumentExportTechnicalLogModel model) : base(model)
		{
		}
		public static BillDocumentExportTechnicalLogFacade Instance
		{
			get { return instance; }
		}
		protected BillDocumentExportTechnicalLogFacade():base() 
		{ 
		} 
	
	}
}
	