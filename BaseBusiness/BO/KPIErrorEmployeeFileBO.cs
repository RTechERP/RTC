
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class KPIErrorEmployeeFileBO : BaseBO
	{
		private KPIErrorEmployeeFileFacade facade = KPIErrorEmployeeFileFacade.Instance;
		protected static KPIErrorEmployeeFileBO instance = new KPIErrorEmployeeFileBO();

		protected KPIErrorEmployeeFileBO()
		{
			this.baseFacade = facade;
		}

		public static KPIErrorEmployeeFileBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	