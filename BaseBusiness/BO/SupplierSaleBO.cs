
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class SupplierSaleBO : BaseBO
	{
		private SupplierSaleFacade facade = SupplierSaleFacade.Instance;
		protected static SupplierSaleBO instance = new SupplierSaleBO();

		protected SupplierSaleBO()
		{
			this.baseFacade = facade;
		}

		public static SupplierSaleBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	