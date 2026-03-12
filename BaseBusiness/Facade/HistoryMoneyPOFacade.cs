
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class HistoryMoneyPOFacade : BaseFacade
	{
		protected static HistoryMoneyPOFacade instance = new HistoryMoneyPOFacade(new HistoryMoneyPOModel());
		protected HistoryMoneyPOFacade(HistoryMoneyPOModel model) : base(model)
		{
		}
		public static HistoryMoneyPOFacade Instance
		{
			get { return instance; }
		}
		protected HistoryMoneyPOFacade():base() 
		{ 
		} 
	
	}
}
	