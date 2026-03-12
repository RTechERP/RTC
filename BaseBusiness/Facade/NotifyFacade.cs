
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class NotifyFacade : BaseFacade
	{
		protected static NotifyFacade instance = new NotifyFacade(new NotifyModel());
		protected NotifyFacade(NotifyModel model) : base(model)
		{
		}
		public static NotifyFacade Instance
		{
			get { return instance; }
		}
		protected NotifyFacade():base() 
		{ 
		} 
	
	}
}
	