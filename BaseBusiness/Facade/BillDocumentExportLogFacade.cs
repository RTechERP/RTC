
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillDocumentExportLogFacade : BaseFacade
	{
		protected static BillDocumentExportLogFacade instance = new BillDocumentExportLogFacade(new BillDocumentExportLogModel());
		protected BillDocumentExportLogFacade(BillDocumentExportLogModel model) : base(model)
		{
		}
		public static BillDocumentExportLogFacade Instance
		{
			get { return instance; }
		}
		protected BillDocumentExportLogFacade():base() 
		{ 
		} 
	
	}
}
	