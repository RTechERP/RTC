
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class FirmBO : BaseBO
	{
		private FirmFacade facade = FirmFacade.Instance;
		protected static FirmBO instance = new FirmBO();

		protected FirmBO()
		{
			this.baseFacade = facade;
		}

		public static FirmBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	