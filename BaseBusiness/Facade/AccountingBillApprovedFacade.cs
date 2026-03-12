
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class AccountingBillApprovedFacade : BaseFacade
	{
		protected static AccountingBillApprovedFacade instance = new AccountingBillApprovedFacade(new AccountingBillApprovedModel());
		protected AccountingBillApprovedFacade(AccountingBillApprovedModel model) : base(model)
		{
		}
		public static AccountingBillApprovedFacade Instance
		{
			get { return instance; }
		}
		protected AccountingBillApprovedFacade():base() 
		{ 
		} 
	
	}
}
	