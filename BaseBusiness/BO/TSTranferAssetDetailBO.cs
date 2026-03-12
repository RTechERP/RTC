
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSTranferAssetDetailBO : BaseBO
	{
		private TSTranferAssetDetailFacade facade = TSTranferAssetDetailFacade.Instance;
		protected static TSTranferAssetDetailBO instance = new TSTranferAssetDetailBO();

		protected TSTranferAssetDetailBO()
		{
			this.baseFacade = facade;
		}

		public static TSTranferAssetDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	