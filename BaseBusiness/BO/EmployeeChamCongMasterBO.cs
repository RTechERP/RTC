
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeChamCongMasterBO : BaseBO
	{
		private EmployeeChamCongMasterFacade facade = EmployeeChamCongMasterFacade.Instance;
		protected static EmployeeChamCongMasterBO instance = new EmployeeChamCongMasterBO();

		protected EmployeeChamCongMasterBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeChamCongMasterBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	