
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeFingerprintMasterBO : BaseBO
	{
		private EmployeeFingerprintMasterFacade facade = EmployeeFingerprintMasterFacade.Instance;
		protected static EmployeeFingerprintMasterBO instance = new EmployeeFingerprintMasterBO();

		protected EmployeeFingerprintMasterBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeFingerprintMasterBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	