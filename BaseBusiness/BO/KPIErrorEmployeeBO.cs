
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class KPIErrorEmployeeBO : BaseBO
	{
		private KPIErrorEmployeeFacade facade = KPIErrorEmployeeFacade.Instance;
		protected static KPIErrorEmployeeBO instance = new KPIErrorEmployeeBO();

		protected KPIErrorEmployeeBO()
		{
			this.baseFacade = facade;
		}

		public static KPIErrorEmployeeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	