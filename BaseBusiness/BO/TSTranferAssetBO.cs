
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSTranferAssetBO : BaseBO
	{
		private TSTranferAssetFacade facade = TSTranferAssetFacade.Instance;
		protected static TSTranferAssetBO instance = new TSTranferAssetBO();

		protected TSTranferAssetBO()
		{
			this.baseFacade = facade;
		}

		public static TSTranferAssetBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	