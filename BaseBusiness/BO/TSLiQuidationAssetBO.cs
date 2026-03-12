
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSLiQuidationAssetBO : BaseBO
	{
		private TSLiQuidationAssetFacade facade = TSLiQuidationAssetFacade.Instance;
		protected static TSLiQuidationAssetBO instance = new TSLiQuidationAssetBO();

		protected TSLiQuidationAssetBO()
		{
			this.baseFacade = facade;
		}

		public static TSLiQuidationAssetBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	