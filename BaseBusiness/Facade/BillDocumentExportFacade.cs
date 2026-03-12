
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillDocumentExportFacade : BaseFacade
	{
		protected static BillDocumentExportFacade instance = new BillDocumentExportFacade(new BillDocumentExportModel());
		protected BillDocumentExportFacade(BillDocumentExportModel model) : base(model)
		{
		}
		public static BillDocumentExportFacade Instance
		{
			get { return instance; }
		}
		protected BillDocumentExportFacade():base() 
		{ 
		} 
	
	}
}
	