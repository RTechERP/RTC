
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeCurricularBO : BaseBO
	{
		private EmployeeCurricularFacade facade = EmployeeCurricularFacade.Instance;
		protected static EmployeeCurricularBO instance = new EmployeeCurricularBO();

		protected EmployeeCurricularBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeCurricularBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	