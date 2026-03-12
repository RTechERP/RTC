
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ChangeLogStoreFacade : BaseFacade
	{
		protected static ChangeLogStoreFacade instance = new ChangeLogStoreFacade(new ChangeLogStoreModel());
		protected ChangeLogStoreFacade(ChangeLogStoreModel model) : base(model)
		{
		}
		public static ChangeLogStoreFacade Instance
		{
			get { return instance; }
		}
		protected ChangeLogStoreFacade():base() 
		{ 
		} 
	
	}
}
	