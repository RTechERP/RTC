
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class KPIDetailUserBO : BaseBO
	{
		private KPIDetailUserFacade facade = KPIDetailUserFacade.Instance;
		protected static KPIDetailUserBO instance = new KPIDetailUserBO();

		protected KPIDetailUserBO()
		{
			this.baseFacade = facade;
		}

		public static KPIDetailUserBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	