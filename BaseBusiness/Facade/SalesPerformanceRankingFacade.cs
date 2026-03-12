
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class SalesPerformanceRankingFacade : BaseFacade
	{
		protected static SalesPerformanceRankingFacade instance = new SalesPerformanceRankingFacade(new SalesPerformanceRankingModel());
		protected SalesPerformanceRankingFacade(SalesPerformanceRankingModel model) : base(model)
		{
		}
		public static SalesPerformanceRankingFacade Instance
		{
			get { return instance; }
		}
		protected SalesPerformanceRankingFacade():base() 
		{ 
		} 
	
	}
}
	