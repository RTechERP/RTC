
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSReportBrokenAssetBO : BaseBO
	{
		private TSReportBrokenAssetFacade facade = TSReportBrokenAssetFacade.Instance;
		protected static TSReportBrokenAssetBO instance = new TSReportBrokenAssetBO();

		protected TSReportBrokenAssetBO()
		{
			this.baseFacade = facade;
		}

		public static TSReportBrokenAssetBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	