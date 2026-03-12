
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class AccountingBillBO : BaseBO
	{
		private AccountingBillFacade facade = AccountingBillFacade.Instance;
		protected static AccountingBillBO instance = new AccountingBillBO();

		protected AccountingBillBO()
		{
			this.baseFacade = facade;
		}

		public static AccountingBillBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	