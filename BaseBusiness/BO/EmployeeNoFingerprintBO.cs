
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeNoFingerprintBO : BaseBO
	{
		private EmployeeNoFingerprintFacade facade = EmployeeNoFingerprintFacade.Instance;
		protected static EmployeeNoFingerprintBO instance = new EmployeeNoFingerprintBO();

		protected EmployeeNoFingerprintBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeNoFingerprintBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	