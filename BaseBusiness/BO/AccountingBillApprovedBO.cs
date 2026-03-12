
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class AccountingBillApprovedBO : BaseBO
	{
		private AccountingBillApprovedFacade facade = AccountingBillApprovedFacade.Instance;
		protected static AccountingBillApprovedBO instance = new AccountingBillApprovedBO();

		protected AccountingBillApprovedBO()
		{
			this.baseFacade = facade;
		}

		public static AccountingBillApprovedBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	