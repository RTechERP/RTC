
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSAssetManagementHistoryLogBO : BaseBO
	{
		private TSAssetManagementHistoryLogFacade facade = TSAssetManagementHistoryLogFacade.Instance;
		protected static TSAssetManagementHistoryLogBO instance = new TSAssetManagementHistoryLogBO();

		protected TSAssetManagementHistoryLogBO()
		{
			this.baseFacade = facade;
		}

		public static TSAssetManagementHistoryLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	