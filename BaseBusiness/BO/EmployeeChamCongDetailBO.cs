
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeChamCongDetailBO : BaseBO
	{
		private EmployeeChamCongDetailFacade facade = EmployeeChamCongDetailFacade.Instance;
		protected static EmployeeChamCongDetailBO instance = new EmployeeChamCongDetailBO();

		protected EmployeeChamCongDetailBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeChamCongDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	