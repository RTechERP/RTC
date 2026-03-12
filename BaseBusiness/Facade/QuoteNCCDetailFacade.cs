
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class QuoteNCCDetailFacade : BaseFacade
	{
		protected static QuoteNCCDetailFacade instance = new QuoteNCCDetailFacade(new QuoteNCCDetailModel());
		protected QuoteNCCDetailFacade(QuoteNCCDetailModel model) : base(model)
		{
		}
		public static QuoteNCCDetailFacade Instance
		{
			get { return instance; }
		}
		protected QuoteNCCDetailFacade():base() 
		{ 
		} 
	
	}
}
	