
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class QuotationKHDetailFacade : BaseFacade
	{
		protected static QuotationKHDetailFacade instance = new QuotationKHDetailFacade(new QuotationKHDetailModel());
		protected QuotationKHDetailFacade(QuotationKHDetailModel model) : base(model)
		{
		}
		public static QuotationKHDetailFacade Instance
		{
			get { return instance; }
		}
		protected QuotationKHDetailFacade():base() 
		{ 
		} 
	
	}
}
	