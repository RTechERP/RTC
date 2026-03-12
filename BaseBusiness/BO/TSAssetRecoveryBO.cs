
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSAssetRecoveryBO : BaseBO
	{
		private TSAssetRecoveryFacade facade = TSAssetRecoveryFacade.Instance;
		protected static TSAssetRecoveryBO instance = new TSAssetRecoveryBO();

		protected TSAssetRecoveryBO()
		{
			this.baseFacade = facade;
		}

		public static TSAssetRecoveryBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	