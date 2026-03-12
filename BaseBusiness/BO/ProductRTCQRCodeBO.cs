
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProductRTCQRCodeBO : BaseBO
	{
		private ProductRTCQRCodeFacade facade = ProductRTCQRCodeFacade.Instance;
		protected static ProductRTCQRCodeBO instance = new ProductRTCQRCodeBO();

		protected ProductRTCQRCodeBO()
		{
			this.baseFacade = facade;
		}

		public static ProductRTCQRCodeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	