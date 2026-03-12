
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class AccountingContractLogBO : BaseBO
	{
		private AccountingContractLogFacade facade = AccountingContractLogFacade.Instance;
		protected static AccountingContractLogBO instance = new AccountingContractLogBO();

		protected AccountingContractLogBO()
		{
			this.baseFacade = facade;
		}

		public static AccountingContractLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	