
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PriceCheckResultBO : BaseBO
	{
		private PriceCheckResultFacade facade = PriceCheckResultFacade.Instance;
		protected static PriceCheckResultBO instance = new PriceCheckResultBO();

		protected PriceCheckResultBO()
		{
			this.baseFacade = facade;
		}

		public static PriceCheckResultBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	