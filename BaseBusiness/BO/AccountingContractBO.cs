
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class AccountingContractBO : BaseBO
	{
		private AccountingContractFacade facade = AccountingContractFacade.Instance;
		protected static AccountingContractBO instance = new AccountingContractBO();

		protected AccountingContractBO()
		{
			this.baseFacade = facade;
		}

		public static AccountingContractBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	