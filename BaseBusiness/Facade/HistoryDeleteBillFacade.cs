
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class HistoryDeleteBillFacade : BaseFacade
	{
		protected static HistoryDeleteBillFacade instance = new HistoryDeleteBillFacade(new HistoryDeleteBillModel());
		protected HistoryDeleteBillFacade(HistoryDeleteBillModel model) : base(model)
		{
		}
		public static HistoryDeleteBillFacade Instance
		{
			get { return instance; }
		}
		protected HistoryDeleteBillFacade():base() 
		{ 
		} 
	
	}
}
	