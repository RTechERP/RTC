
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TrackingMarksFileBO : BaseBO
	{
		private TrackingMarksFileFacade facade = TrackingMarksFileFacade.Instance;
		protected static TrackingMarksFileBO instance = new TrackingMarksFileBO();

		protected TrackingMarksFileBO()
		{
			this.baseFacade = facade;
		}

		public static TrackingMarksFileBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	