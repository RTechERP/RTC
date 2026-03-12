
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillDocumentImportFacade : BaseFacade
	{
		protected static BillDocumentImportFacade instance = new BillDocumentImportFacade(new BillDocumentImportModel());
		protected BillDocumentImportFacade(BillDocumentImportModel model) : base(model)
		{
		}
		public static BillDocumentImportFacade Instance
		{
			get { return instance; }
		}
		protected BillDocumentImportFacade():base() 
		{ 
		} 
	
	}
}
	