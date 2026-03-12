
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillDocumentImportTechnicalLogFacade : BaseFacade
	{
		protected static BillDocumentImportTechnicalLogFacade instance = new BillDocumentImportTechnicalLogFacade(new BillDocumentImportTechnicalLogModel());
		protected BillDocumentImportTechnicalLogFacade(BillDocumentImportTechnicalLogModel model) : base(model)
		{
		}
		public static BillDocumentImportTechnicalLogFacade Instance
		{
			get { return instance; }
		}
		protected BillDocumentImportTechnicalLogFacade():base() 
		{ 
		} 
	
	}
}
	