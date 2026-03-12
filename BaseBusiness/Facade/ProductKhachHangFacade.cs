
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProductKhachHangFacade : BaseFacade
	{
		protected static ProductKhachHangFacade instance = new ProductKhachHangFacade(new ProductKhachHangModel());
		protected ProductKhachHangFacade(ProductKhachHangModel model) : base(model)
		{
		}
		public static ProductKhachHangFacade Instance
		{
			get { return instance; }
		}
		protected ProductKhachHangFacade():base() 
		{ 
		} 
	
	}
}
	