
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CurrencyBO : BaseBO
	{
		private CurrencyFacade facade = CurrencyFacade.Instance;
		protected static CurrencyBO instance = new CurrencyBO();

		protected CurrencyBO()
		{
			this.baseFacade = facade;
		}

		public static CurrencyBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	