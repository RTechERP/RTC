
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class KPIDetailBO : BaseBO
	{
		private KPIDetailFacade facade = KPIDetailFacade.Instance;
		protected static KPIDetailBO instance = new KPIDetailBO();

		protected KPIDetailBO()
		{
			this.baseFacade = facade;
		}

		public static KPIDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	