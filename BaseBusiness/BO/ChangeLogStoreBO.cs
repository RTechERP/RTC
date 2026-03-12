
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ChangeLogStoreBO : BaseBO
	{
		private ChangeLogStoreFacade facade = ChangeLogStoreFacade.Instance;
		protected static ChangeLogStoreBO instance = new ChangeLogStoreBO();

		protected ChangeLogStoreBO()
		{
			this.baseFacade = facade;
		}

		public static ChangeLogStoreBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	