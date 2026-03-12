
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class NotifyBO : BaseBO
	{
		private NotifyFacade facade = NotifyFacade.Instance;
		protected static NotifyBO instance = new NotifyBO();

		protected NotifyBO()
		{
			this.baseFacade = facade;
		}

		public static NotifyBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	