
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillSaleBO : BaseBO
	{
		private BillSaleFacade facade = BillSaleFacade.Instance;
		protected static BillSaleBO instance = new BillSaleBO();

		protected BillSaleBO()
		{
			this.baseFacade = facade;
		}

		public static BillSaleBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	