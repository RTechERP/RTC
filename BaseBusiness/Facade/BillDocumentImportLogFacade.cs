
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillDocumentImportLogFacade : BaseFacade
	{
		protected static BillDocumentImportLogFacade instance = new BillDocumentImportLogFacade(new BillDocumentImportLogModel());
		protected BillDocumentImportLogFacade(BillDocumentImportLogModel model) : base(model)
		{
		}
		public static BillDocumentImportLogFacade Instance
		{
			get { return instance; }
		}
		protected BillDocumentImportLogFacade():base() 
		{ 
		} 
	
	}
}
	