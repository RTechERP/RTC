
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PODetailBO : BaseBO
	{
		private PODetailFacade facade = PODetailFacade.Instance;
		protected static PODetailBO instance = new PODetailBO();

		protected PODetailBO()
		{
			this.baseFacade = facade;
		}

		public static PODetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	