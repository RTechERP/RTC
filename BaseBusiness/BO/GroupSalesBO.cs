
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class GroupSalesBO : BaseBO
	{
		private GroupSalesFacade facade = GroupSalesFacade.Instance;
		protected static GroupSalesBO instance = new GroupSalesBO();

		protected GroupSalesBO()
		{
			this.baseFacade = facade;
		}

		public static GroupSalesBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	