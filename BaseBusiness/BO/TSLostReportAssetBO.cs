
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSLostReportAssetBO : BaseBO
	{
		private TSLostReportAssetFacade facade = TSLostReportAssetFacade.Instance;
		protected static TSLostReportAssetBO instance = new TSLostReportAssetBO();

		protected TSLostReportAssetBO()
		{
			this.baseFacade = facade;
		}

		public static TSLostReportAssetBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	