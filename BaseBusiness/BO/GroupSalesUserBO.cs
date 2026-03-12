
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class GroupSalesUserBO : BaseBO
	{
		private GroupSalesUserFacade facade = GroupSalesUserFacade.Instance;
		protected static GroupSalesUserBO instance = new GroupSalesUserBO();

		protected GroupSalesUserBO()
		{
			this.baseFacade = facade;
		}

		public static GroupSalesUserBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	