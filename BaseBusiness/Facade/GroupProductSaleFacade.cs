
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class GroupProductSaleFacade : BaseFacade
	{
		protected static GroupProductSaleFacade instance = new GroupProductSaleFacade(new GroupProductSaleModel());
		protected GroupProductSaleFacade(GroupProductSaleModel model) : base(model)
		{
		}
		public static GroupProductSaleFacade Instance
		{
			get { return instance; }
		}
		protected GroupProductSaleFacade():base() 
		{ 
		} 
	
	}
}
	