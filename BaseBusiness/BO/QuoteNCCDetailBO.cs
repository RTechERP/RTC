
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class QuoteNCCDetailBO : BaseBO
	{
		private QuoteNCCDetailFacade facade = QuoteNCCDetailFacade.Instance;
		protected static QuoteNCCDetailBO instance = new QuoteNCCDetailBO();

		protected QuoteNCCDetailBO()
		{
			this.baseFacade = facade;
		}

		public static QuoteNCCDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	