
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillImportDocumentFacade : BaseFacade
	{
		protected static BillImportDocumentFacade instance = new BillImportDocumentFacade(new BillImportDocumentModel());
		protected BillImportDocumentFacade(BillImportDocumentModel model) : base(model)
		{
		}
		public static BillImportDocumentFacade Instance
		{
			get { return instance; }
		}
		protected BillImportDocumentFacade():base() 
		{ 
		} 
	
	}
}
	