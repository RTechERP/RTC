
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PercentMainIndexUserBO : BaseBO
	{
		private PercentMainIndexUserFacade facade = PercentMainIndexUserFacade.Instance;
		protected static PercentMainIndexUserBO instance = new PercentMainIndexUserBO();

		protected PercentMainIndexUserBO()
		{
			this.baseFacade = facade;
		}

		public static PercentMainIndexUserBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	