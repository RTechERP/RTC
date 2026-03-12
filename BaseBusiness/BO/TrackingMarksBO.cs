
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TrackingMarksBO : BaseBO
	{
		private TrackingMarksFacade facade = TrackingMarksFacade.Instance;
		protected static TrackingMarksBO instance = new TrackingMarksBO();

		protected TrackingMarksBO()
		{
			this.baseFacade = facade;
		}

		public static TrackingMarksBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	