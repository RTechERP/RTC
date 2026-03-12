
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class QuotationTermLinkFacade : BaseFacade
	{
		protected static QuotationTermLinkFacade instance = new QuotationTermLinkFacade(new QuotationTermLinkModel());
		protected QuotationTermLinkFacade(QuotationTermLinkModel model) : base(model)
		{
		}
		public static QuotationTermLinkFacade Instance
		{
			get { return instance; }
		}
		protected QuotationTermLinkFacade():base() 
		{ 
		} 
	
	}
}
	