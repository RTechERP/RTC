
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSAssetManagementHistoryBO : BaseBO
	{
		private TSAssetManagementHistoryFacade facade = TSAssetManagementHistoryFacade.Instance;
		protected static TSAssetManagementHistoryBO instance = new TSAssetManagementHistoryBO();

		protected TSAssetManagementHistoryBO()
		{
			this.baseFacade = facade;
		}

		public static TSAssetManagementHistoryBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	