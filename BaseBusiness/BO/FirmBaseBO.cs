
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class FirmBaseBO : BaseBO
	{
		private FirmBaseFacade facade = FirmBaseFacade.Instance;
		protected static FirmBaseBO instance = new FirmBaseBO();

		protected FirmBaseBO()
		{
			this.baseFacade = facade;
		}

		public static FirmBaseBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	