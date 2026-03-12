
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TrackingMarksSealBO : BaseBO
	{
		private TrackingMarksSealFacade facade = TrackingMarksSealFacade.Instance;
		protected static TrackingMarksSealBO instance = new TrackingMarksSealBO();

		protected TrackingMarksSealBO()
		{
			this.baseFacade = facade;
		}

		public static TrackingMarksSealBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	