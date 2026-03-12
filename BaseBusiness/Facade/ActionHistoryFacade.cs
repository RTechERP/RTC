
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ActionHistoryFacade : BaseFacade
	{
		protected static ActionHistoryFacade instance = new ActionHistoryFacade(new ActionHistoryModel());
		protected ActionHistoryFacade(ActionHistoryModel model) : base(model)
		{
		}
		public static ActionHistoryFacade Instance
		{
			get { return instance; }
		}
		protected ActionHistoryFacade():base() 
		{ 
		} 
	
	}
}
	