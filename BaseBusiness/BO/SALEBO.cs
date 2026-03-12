
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class SALEBO : BaseBO
	{
		private SALEFacade facade = SALEFacade.Instance;
		protected static SALEBO instance = new SALEBO();

		protected SALEBO()
		{
			this.baseFacade = facade;
		}

		public static SALEBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	