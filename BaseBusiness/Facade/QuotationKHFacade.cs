
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class QuotationKHFacade : BaseFacade
	{
		protected static QuotationKHFacade instance = new QuotationKHFacade(new QuotationKHModel());
		protected QuotationKHFacade(QuotationKHModel model) : base(model)
		{
		}
		public static QuotationKHFacade Instance
		{
			get { return instance; }
		}
		protected QuotationKHFacade():base() 
		{ 
		} 
	
	}
}
	