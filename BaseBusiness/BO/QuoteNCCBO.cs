
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class QuoteNCCBO : BaseBO
	{
		private QuoteNCCFacade facade = QuoteNCCFacade.Instance;
		protected static QuoteNCCBO instance = new QuoteNCCBO();

		protected QuoteNCCBO()
		{
			this.baseFacade = facade;
		}

		public static QuoteNCCBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	