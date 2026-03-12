
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PONCCHistoryFacade : BaseFacade
	{
		protected static PONCCHistoryFacade instance = new PONCCHistoryFacade(new PONCCHistoryModel());
		protected PONCCHistoryFacade(PONCCHistoryModel model) : base(model)
		{
		}
		public static PONCCHistoryFacade Instance
		{
			get { return instance; }
		}
		protected PONCCHistoryFacade():base() 
		{ 
		} 
	
	}
}
	