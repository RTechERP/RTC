
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CurrencyFacade : BaseFacade
	{
		protected static CurrencyFacade instance = new CurrencyFacade(new CurrencyModel());
		protected CurrencyFacade(CurrencyModel model) : base(model)
		{
		}
		public static CurrencyFacade Instance
		{
			get { return instance; }
		}
		protected CurrencyFacade():base() 
		{ 
		} 
	
	}
}
	