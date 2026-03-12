
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillExportDocumentFacade : BaseFacade
	{
		protected static BillExportDocumentFacade instance = new BillExportDocumentFacade(new BillExportDocumentModel());
		protected BillExportDocumentFacade(BillExportDocumentModel model) : base(model)
		{
		}
		public static BillExportDocumentFacade Instance
		{
			get { return instance; }
		}
		protected BillExportDocumentFacade():base() 
		{ 
		} 
	
	}
}
	