
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class AccountingContractTypeFacade : BaseFacade
	{
		protected static AccountingContractTypeFacade instance = new AccountingContractTypeFacade(new AccountingContractTypeModel());
		protected AccountingContractTypeFacade(AccountingContractTypeModel model) : base(model)
		{
		}
		public static AccountingContractTypeFacade Instance
		{
			get { return instance; }
		}
		protected AccountingContractTypeFacade():base() 
		{ 
		} 
	
	}
}
	