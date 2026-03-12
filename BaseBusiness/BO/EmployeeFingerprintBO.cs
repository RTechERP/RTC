
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeFingerprintBO : BaseBO
	{
		private EmployeeFingerprintFacade facade = EmployeeFingerprintFacade.Instance;
		protected static EmployeeFingerprintBO instance = new EmployeeFingerprintBO();

		protected EmployeeFingerprintBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeFingerprintBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	