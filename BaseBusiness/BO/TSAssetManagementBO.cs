
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSAssetManagementBO : BaseBO
	{
		private TSAssetManagementFacade facade = TSAssetManagementFacade.Instance;
		protected static TSAssetManagementBO instance = new TSAssetManagementBO();

		protected TSAssetManagementBO()
		{
			this.baseFacade = facade;
		}

		public static TSAssetManagementBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	