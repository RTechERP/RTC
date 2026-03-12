
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class AccountingContractTypeBO : BaseBO
	{
		private AccountingContractTypeFacade facade = AccountingContractTypeFacade.Instance;
		protected static AccountingContractTypeBO instance = new AccountingContractTypeBO();

		protected AccountingContractTypeBO()
		{
			this.baseFacade = facade;
		}

		public static AccountingContractTypeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	