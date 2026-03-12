
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeePurchaseBO : BaseBO
	{
		private EmployeePurchaseFacade facade = EmployeePurchaseFacade.Instance;
		protected static EmployeePurchaseBO instance = new EmployeePurchaseBO();

		protected EmployeePurchaseBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeePurchaseBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	