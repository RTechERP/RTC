
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSAllocationEvictionAssetBO : BaseBO
	{
		private TSAllocationEvictionAssetFacade facade = TSAllocationEvictionAssetFacade.Instance;
		protected static TSAllocationEvictionAssetBO instance = new TSAllocationEvictionAssetBO();

		protected TSAllocationEvictionAssetBO()
		{
			this.baseFacade = facade;
		}

		public static TSAllocationEvictionAssetBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	