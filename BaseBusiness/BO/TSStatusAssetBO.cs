
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSStatusAssetBO : BaseBO
	{
		private TSStatusAssetFacade facade = TSStatusAssetFacade.Instance;
		protected static TSStatusAssetBO instance = new TSStatusAssetBO();

		protected TSStatusAssetBO()
		{
			this.baseFacade = facade;
		}

		public static TSStatusAssetBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	