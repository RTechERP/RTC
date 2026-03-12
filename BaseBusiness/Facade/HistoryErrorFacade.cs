
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class HistoryErrorFacade : BaseFacade
	{
		protected static HistoryErrorFacade instance = new HistoryErrorFacade(new HistoryErrorModel());
		protected HistoryErrorFacade(HistoryErrorModel model) : base(model)
		{
		}
		public static HistoryErrorFacade Instance
		{
			get { return instance; }
		}
		protected HistoryErrorFacade():base() 
		{ 
		} 
	
	}
}
	