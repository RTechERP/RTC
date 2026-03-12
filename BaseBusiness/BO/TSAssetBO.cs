
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSAssetBO : BaseBO
	{
		private TSAssetFacade facade = TSAssetFacade.Instance;
		protected static TSAssetBO instance = new TSAssetBO();

		protected TSAssetBO()
		{
			this.baseFacade = facade;
		}

		public static TSAssetBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	