
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class QuoteNCCFacade : BaseFacade
	{
		protected static QuoteNCCFacade instance = new QuoteNCCFacade(new QuoteNCCModel());
		protected QuoteNCCFacade(QuoteNCCModel model) : base(model)
		{
		}
		public static QuoteNCCFacade Instance
		{
			get { return instance; }
		}
		protected QuoteNCCFacade():base() 
		{ 
		} 
	
	}
}
	