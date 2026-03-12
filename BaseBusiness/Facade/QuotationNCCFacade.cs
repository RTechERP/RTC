
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class QuotationNCCFacade : BaseFacade
	{
		protected static QuotationNCCFacade instance = new QuotationNCCFacade(new QuotationNCCModel());
		protected QuotationNCCFacade(QuotationNCCModel model) : base(model)
		{
		}
		public static QuotationNCCFacade Instance
		{
			get { return instance; }
		}
		protected QuotationNCCFacade():base() 
		{ 
		} 
	
	}
}
	