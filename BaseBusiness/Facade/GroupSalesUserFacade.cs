
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class GroupSalesUserFacade : BaseFacade
	{
		protected static GroupSalesUserFacade instance = new GroupSalesUserFacade(new GroupSalesUserModel());
		protected GroupSalesUserFacade(GroupSalesUserModel model) : base(model)
		{
		}
		public static GroupSalesUserFacade Instance
		{
			get { return instance; }
		}
		protected GroupSalesUserFacade():base() 
		{ 
		} 
	
	}
}
	