
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSSourceAssetBO : BaseBO
	{
		private TSSourceAssetFacade facade = TSSourceAssetFacade.Instance;
		protected static TSSourceAssetBO instance = new TSSourceAssetBO();

		protected TSSourceAssetBO()
		{
			this.baseFacade = facade;
		}

		public static TSSourceAssetBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	