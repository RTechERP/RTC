
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProductKhachHangBO : BaseBO
	{
		private ProductKhachHangFacade facade = ProductKhachHangFacade.Instance;
		protected static ProductKhachHangBO instance = new ProductKhachHangBO();

		protected ProductKhachHangBO()
		{
			this.baseFacade = facade;
		}

		public static ProductKhachHangBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	