
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProductRTCQRCodeFacade : BaseFacade
	{
		protected static ProductRTCQRCodeFacade instance = new ProductRTCQRCodeFacade(new ProductRTCQRCodeModel());
		protected ProductRTCQRCodeFacade(ProductRTCQRCodeModel model) : base(model)
		{
		}
		public static ProductRTCQRCodeFacade Instance
		{
			get { return instance; }
		}
		protected ProductRTCQRCodeFacade():base() 
		{ 
		} 
	
	}
}
	