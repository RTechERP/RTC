
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PriceCheckFacade : BaseFacade
	{
		protected static PriceCheckFacade instance = new PriceCheckFacade(new PriceCheckModel());
		protected PriceCheckFacade(PriceCheckModel model) : base(model)
		{
		}
		public static PriceCheckFacade Instance
		{
			get { return instance; }
		}
		protected PriceCheckFacade():base() 
		{ 
		} 
	
	}
}
	