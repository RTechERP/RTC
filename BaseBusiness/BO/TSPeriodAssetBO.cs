
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSPeriodAssetBO : BaseBO
	{
		private TSPeriodAssetFacade facade = TSPeriodAssetFacade.Instance;
		protected static TSPeriodAssetBO instance = new TSPeriodAssetBO();

		protected TSPeriodAssetBO()
		{
			this.baseFacade = facade;
		}

		public static TSPeriodAssetBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	