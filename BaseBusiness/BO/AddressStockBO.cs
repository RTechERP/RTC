
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class AddressStockBO : BaseBO
	{
		private AddressStockFacade facade = AddressStockFacade.Instance;
		protected static AddressStockBO instance = new AddressStockBO();

		protected AddressStockBO()
		{
			this.baseFacade = facade;
		}

		public static AddressStockBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	