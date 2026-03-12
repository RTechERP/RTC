
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSRepairAssetBO : BaseBO
	{
		private TSRepairAssetFacade facade = TSRepairAssetFacade.Instance;
		protected static TSRepairAssetBO instance = new TSRepairAssetBO();

		protected TSRepairAssetBO()
		{
			this.baseFacade = facade;
		}

		public static TSRepairAssetBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	