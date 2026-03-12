
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class AccountingContractFileBO : BaseBO
	{
		private AccountingContractFileFacade facade = AccountingContractFileFacade.Instance;
		protected static AccountingContractFileBO instance = new AccountingContractFileBO();

		protected AccountingContractFileBO()
		{
			this.baseFacade = facade;
		}

		public static AccountingContractFileBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	