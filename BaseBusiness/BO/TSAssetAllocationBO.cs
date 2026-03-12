
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSAssetAllocationBO : BaseBO
	{
		private TSAssetAllocationFacade facade = TSAssetAllocationFacade.Instance;
		protected static TSAssetAllocationBO instance = new TSAssetAllocationBO();

		protected TSAssetAllocationBO()
		{
			this.baseFacade = facade;
		}

		public static TSAssetAllocationBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	