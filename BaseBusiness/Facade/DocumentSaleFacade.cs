
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class DocumentSaleFacade : BaseFacade
	{
		protected static DocumentSaleFacade instance = new DocumentSaleFacade(new DocumentSaleModel());
		protected DocumentSaleFacade(DocumentSaleModel model) : base(model)
		{
		}
		public static DocumentSaleFacade Instance
		{
			get { return instance; }
		}
		protected DocumentSaleFacade():base() 
		{ 
		} 
	
	}
}
	