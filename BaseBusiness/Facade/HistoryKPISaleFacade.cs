
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class HistoryKPISaleFacade : BaseFacade
	{
		protected static HistoryKPISaleFacade instance = new HistoryKPISaleFacade(new HistoryKPISaleModel());
		protected HistoryKPISaleFacade(HistoryKPISaleModel model) : base(model)
		{
		}
		public static HistoryKPISaleFacade Instance
		{
			get { return instance; }
		}
		protected HistoryKPISaleFacade():base() 
		{ 
		} 
	
	}
}
	