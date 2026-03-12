
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeCollectMoneyBO : BaseBO
	{
		private EmployeeCollectMoneyFacade facade = EmployeeCollectMoneyFacade.Instance;
		protected static EmployeeCollectMoneyBO instance = new EmployeeCollectMoneyBO();

		protected EmployeeCollectMoneyBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeCollectMoneyBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	