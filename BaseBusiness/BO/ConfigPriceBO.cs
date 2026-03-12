
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ConfigPriceBO : BaseBO
	{
		private ConfigPriceFacade facade = ConfigPriceFacade.Instance;
		protected static ConfigPriceBO instance = new ConfigPriceBO();

		protected ConfigPriceBO()
		{
			this.baseFacade = facade;
		}

		public static ConfigPriceBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	