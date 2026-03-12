
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class QuotationFacade : BaseFacade
	{
		protected static QuotationFacade instance = new QuotationFacade(new QuotationModel());
		protected QuotationFacade(QuotationModel model) : base(model)
		{
		}
		public static QuotationFacade Instance
		{
			get { return instance; }
		}
		protected QuotationFacade():base() 
		{ 
		} 
	
	}
}
	