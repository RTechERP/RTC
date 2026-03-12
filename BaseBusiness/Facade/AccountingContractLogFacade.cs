
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class AccountingContractLogFacade : BaseFacade
	{
		protected static AccountingContractLogFacade instance = new AccountingContractLogFacade(new AccountingContractLogModel());
		protected AccountingContractLogFacade(AccountingContractLogModel model) : base(model)
		{
		}
		public static AccountingContractLogFacade Instance
		{
			get { return instance; }
		}
		protected AccountingContractLogFacade():base() 
		{ 
		} 
	
	}
}
	