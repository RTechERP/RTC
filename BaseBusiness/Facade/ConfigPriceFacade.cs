
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ConfigPriceFacade : BaseFacade
	{
		protected static ConfigPriceFacade instance = new ConfigPriceFacade(new ConfigPriceModel());
		protected ConfigPriceFacade(ConfigPriceModel model) : base(model)
		{
		}
		public static ConfigPriceFacade Instance
		{
			get { return instance; }
		}
		protected ConfigPriceFacade():base() 
		{ 
		} 
	
	}
}
	