
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class GroupProductSaleBO : BaseBO
	{
		private GroupProductSaleFacade facade = GroupProductSaleFacade.Instance;
		protected static GroupProductSaleBO instance = new GroupProductSaleBO();

		protected GroupProductSaleBO()
		{
			this.baseFacade = facade;
		}

		public static GroupProductSaleBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	