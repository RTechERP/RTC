
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class __EFMigrationsHistoryFacade : BaseFacade
	{
		protected static __EFMigrationsHistoryFacade instance = new __EFMigrationsHistoryFacade(new __EFMigrationsHistoryModel());
		protected __EFMigrationsHistoryFacade(__EFMigrationsHistoryModel model) : base(model)
		{
		}
		public static __EFMigrationsHistoryFacade Instance
		{
			get { return instance; }
		}
		protected __EFMigrationsHistoryFacade():base() 
		{ 
		} 
	
	}
}
	