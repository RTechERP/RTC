
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillDocumentExportTechnicalFacade : BaseFacade
	{
		protected static BillDocumentExportTechnicalFacade instance = new BillDocumentExportTechnicalFacade(new BillDocumentExportTechnicalModel());
		protected BillDocumentExportTechnicalFacade(BillDocumentExportTechnicalModel model) : base(model)
		{
		}
		public static BillDocumentExportTechnicalFacade Instance
		{
			get { return instance; }
		}
		protected BillDocumentExportTechnicalFacade():base() 
		{ 
		} 
	
	}
}
	