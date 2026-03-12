
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillDocumentImportTechnicalFacade : BaseFacade
	{
		protected static BillDocumentImportTechnicalFacade instance = new BillDocumentImportTechnicalFacade(new BillDocumentImportTechnicalModel());
		protected BillDocumentImportTechnicalFacade(BillDocumentImportTechnicalModel model) : base(model)
		{
		}
		public static BillDocumentImportTechnicalFacade Instance
		{
			get { return instance; }
		}
		protected BillDocumentImportTechnicalFacade():base() 
		{ 
		} 
	
	}
}
	