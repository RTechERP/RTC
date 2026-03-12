
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class QuotationDetailFacade : BaseFacade
	{
		protected static QuotationDetailFacade instance = new QuotationDetailFacade(new QuotationDetailModel());
		protected QuotationDetailFacade(QuotationDetailModel model) : base(model)
		{
		}
		public static QuotationDetailFacade Instance
		{
			get { return instance; }
		}
		protected QuotationDetailFacade():base() 
		{ 
		} 
	
	}
}
	