
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class SalesPerformanceRankingBO : BaseBO
	{
		private SalesPerformanceRankingFacade facade = SalesPerformanceRankingFacade.Instance;
		protected static SalesPerformanceRankingBO instance = new SalesPerformanceRankingBO();

		protected SalesPerformanceRankingBO()
		{
			this.baseFacade = facade;
		}

		public static SalesPerformanceRankingBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	