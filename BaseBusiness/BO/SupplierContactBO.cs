
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class SupplierContactBO : BaseBO
	{
		private SupplierContactFacade facade = SupplierContactFacade.Instance;
		protected static SupplierContactBO instance = new SupplierContactBO();

		protected SupplierContactBO()
		{
			this.baseFacade = facade;
		}

		public static SupplierContactBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	