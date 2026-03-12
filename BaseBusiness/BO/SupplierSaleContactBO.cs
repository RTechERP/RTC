
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class SupplierSaleContactBO : BaseBO
	{
		private SupplierSaleContactFacade facade = SupplierSaleContactFacade.Instance;
		protected static SupplierSaleContactBO instance = new SupplierSaleContactBO();

		protected SupplierSaleContactBO()
		{
			this.baseFacade = facade;
		}

		public static SupplierSaleContactBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	