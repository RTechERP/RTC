
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class KPIErrorBO : BaseBO
	{
		private KPIErrorFacade facade = KPIErrorFacade.Instance;
		protected static KPIErrorBO instance = new KPIErrorBO();

		protected KPIErrorBO()
		{
			this.baseFacade = facade;
		}

		public static KPIErrorBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	