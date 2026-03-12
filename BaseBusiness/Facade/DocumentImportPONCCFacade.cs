
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class DocumentImportPONCCFacade : BaseFacade
	{
		protected static DocumentImportPONCCFacade instance = new DocumentImportPONCCFacade(new DocumentImportPONCCModel());
		protected DocumentImportPONCCFacade(DocumentImportPONCCModel model) : base(model)
		{
		}
		public static DocumentImportPONCCFacade Instance
		{
			get { return instance; }
		}
		protected DocumentImportPONCCFacade():base() 
		{ 
		} 
	
	}
}
	