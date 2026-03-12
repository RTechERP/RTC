
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PriceCheckResultFacade : BaseFacade
	{
		protected static PriceCheckResultFacade instance = new PriceCheckResultFacade(new PriceCheckResultModel());
		protected PriceCheckResultFacade(PriceCheckResultModel model) : base(model)
		{
		}
		public static PriceCheckResultFacade Instance
		{
			get { return instance; }
		}
		protected PriceCheckResultFacade():base() 
		{ 
		} 
	
	}
}
	