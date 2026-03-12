
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeePayrollBonusDeuctionBO : BaseBO
	{
		private EmployeePayrollBonusDeuctionFacade facade = EmployeePayrollBonusDeuctionFacade.Instance;
		protected static EmployeePayrollBonusDeuctionBO instance = new EmployeePayrollBonusDeuctionBO();

		protected EmployeePayrollBonusDeuctionBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeePayrollBonusDeuctionBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	