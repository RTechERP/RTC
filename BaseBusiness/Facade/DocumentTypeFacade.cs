
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class DocumentTypeFacade : BaseFacade
	{
		protected static DocumentTypeFacade instance = new DocumentTypeFacade(new DocumentTypeModel());
		protected DocumentTypeFacade(DocumentTypeModel model) : base(model)
		{
		}
		public static DocumentTypeFacade Instance
		{
			get { return instance; }
		}
		protected DocumentTypeFacade():base() 
		{ 
		} 
	
	}
}
	