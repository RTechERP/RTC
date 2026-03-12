
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSAssetAllocationDetailBO : BaseBO
	{
		private TSAssetAllocationDetailFacade facade = TSAssetAllocationDetailFacade.Instance;
		protected static TSAssetAllocationDetailBO instance = new TSAssetAllocationDetailBO();

		protected TSAssetAllocationDetailBO()
		{
			this.baseFacade = facade;
		}

		public static TSAssetAllocationDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	