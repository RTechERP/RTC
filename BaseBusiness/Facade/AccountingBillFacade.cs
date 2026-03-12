
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class AccountingBillFacade : BaseFacade
	{
		protected static AccountingBillFacade instance = new AccountingBillFacade(new AccountingBillModel());
		protected AccountingBillFacade(AccountingBillModel model) : base(model)
		{
		}
		public static AccountingBillFacade Instance
		{
			get { return instance; }
		}
		protected AccountingBillFacade():base() 
		{ 
		} 
	
	}
}
	