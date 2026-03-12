
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PriceCheckBO : BaseBO
	{
		private PriceCheckFacade facade = PriceCheckFacade.Instance;
		protected static PriceCheckBO instance = new PriceCheckBO();

		protected PriceCheckBO()
		{
			this.baseFacade = facade;
		}

		public static PriceCheckBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	