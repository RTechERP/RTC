
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class AccountingContractFacade : BaseFacade
	{
		protected static AccountingContractFacade instance = new AccountingContractFacade(new AccountingContractModel());
		protected AccountingContractFacade(AccountingContractModel model) : base(model)
		{
		}
		public static AccountingContractFacade Instance
		{
			get { return instance; }
		}
		protected AccountingContractFacade():base() 
		{ 
		} 
	
	}
}
	