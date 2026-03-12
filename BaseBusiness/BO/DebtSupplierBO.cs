
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class DebtSupplierBO : BaseBO
	{
		private DebtSupplierFacade facade = DebtSupplierFacade.Instance;
		protected static DebtSupplierBO instance = new DebtSupplierBO();

		protected DebtSupplierBO()
		{
			this.baseFacade = facade;
		}

		public static DebtSupplierBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	