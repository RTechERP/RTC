
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeNighShiftBO : BaseBO
	{
		private EmployeeNighShiftFacade facade = EmployeeNighShiftFacade.Instance;
		protected static EmployeeNighShiftBO instance = new EmployeeNighShiftBO();

		protected EmployeeNighShiftBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeNighShiftBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	