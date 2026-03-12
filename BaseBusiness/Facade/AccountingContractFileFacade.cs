
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class AccountingContractFileFacade : BaseFacade
	{
		protected static AccountingContractFileFacade instance = new AccountingContractFileFacade(new AccountingContractFileModel());
		protected AccountingContractFileFacade(AccountingContractFileModel model) : base(model)
		{
		}
		public static AccountingContractFileFacade Instance
		{
			get { return instance; }
		}
		protected AccountingContractFileFacade():base() 
		{ 
		} 
	
	}
}
	