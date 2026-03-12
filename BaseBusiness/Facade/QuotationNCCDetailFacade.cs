
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class QuotationNCCDetailFacade : BaseFacade
	{
		protected static QuotationNCCDetailFacade instance = new QuotationNCCDetailFacade(new QuotationNCCDetailModel());
		protected QuotationNCCDetailFacade(QuotationNCCDetailModel model) : base(model)
		{
		}
		public static QuotationNCCDetailFacade Instance
		{
			get { return instance; }
		}
		protected QuotationNCCDetailFacade():base() 
		{ 
		} 
	
	}
}
	