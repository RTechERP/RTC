
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class DocumentFacade : BaseFacade
	{
		protected static DocumentFacade instance = new DocumentFacade(new DocumentModel());
		protected DocumentFacade(DocumentModel model) : base(model)
		{
		}
		public static DocumentFacade Instance
		{
			get { return instance; }
		}
		protected DocumentFacade():base() 
		{ 
		} 
	
	}
}
	