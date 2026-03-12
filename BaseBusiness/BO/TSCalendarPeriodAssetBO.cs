
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TSCalendarPeriodAssetBO : BaseBO
	{
		private TSCalendarPeriodAssetFacade facade = TSCalendarPeriodAssetFacade.Instance;
		protected static TSCalendarPeriodAssetBO instance = new TSCalendarPeriodAssetBO();

		protected TSCalendarPeriodAssetBO()
		{
			this.baseFacade = facade;
		}

		public static TSCalendarPeriodAssetBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	