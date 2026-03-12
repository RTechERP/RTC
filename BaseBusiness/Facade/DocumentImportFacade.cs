
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class DocumentImportFacade : BaseFacade
	{
		protected static DocumentImportFacade instance = new DocumentImportFacade(new DocumentImportModel());
		protected DocumentImportFacade(DocumentImportModel model) : base(model)
		{
		}
		public static DocumentImportFacade Instance
		{
			get { return instance; }
		}
		protected DocumentImportFacade():base() 
		{ 
		} 
	
	}
}
	