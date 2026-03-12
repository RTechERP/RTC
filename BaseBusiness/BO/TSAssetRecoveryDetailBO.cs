
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSAssetRecoveryDetailBO : BaseBO
	{
		private TSAssetRecoveryDetailFacade facade = TSAssetRecoveryDetailFacade.Instance;
		protected static TSAssetRecoveryDetailBO instance = new TSAssetRecoveryDetailBO();

		protected TSAssetRecoveryDetailBO()
		{
			this.baseFacade = facade;
		}

		public static TSAssetRecoveryDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	