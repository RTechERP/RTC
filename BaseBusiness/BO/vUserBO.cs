
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class vUserBO : BaseBO
	{
		private vUserFacade facade = vUserFacade.Instance;
		protected static vUserBO instance = new vUserBO();

		protected vUserBO()
		{
			this.baseFacade = facade;
		}

		public static vUserBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	