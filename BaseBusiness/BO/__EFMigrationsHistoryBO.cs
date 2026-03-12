
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class __EFMigrationsHistoryBO : BaseBO
	{
		private __EFMigrationsHistoryFacade facade = __EFMigrationsHistoryFacade.Instance;
		protected static __EFMigrationsHistoryBO instance = new __EFMigrationsHistoryBO();

		protected __EFMigrationsHistoryBO()
		{
			this.baseFacade = facade;
		}

		public static __EFMigrationsHistoryBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	