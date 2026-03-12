
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class GroupSalesFacade : BaseFacade
	{
		protected static GroupSalesFacade instance = new GroupSalesFacade(new GroupSalesModel());
		protected GroupSalesFacade(GroupSalesModel model) : base(model)
		{
		}
		public static GroupSalesFacade Instance
		{
			get { return instance; }
		}
		protected GroupSalesFacade():base() 
		{ 
		} 
	
	}
}
	