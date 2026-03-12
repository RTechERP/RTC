
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TrackingMarksSealFacade : BaseFacade
	{
		protected static TrackingMarksSealFacade instance = new TrackingMarksSealFacade(new TrackingMarksSealModel());
		protected TrackingMarksSealFacade(TrackingMarksSealModel model) : base(model)
		{
		}
		public static TrackingMarksSealFacade Instance
		{
			get { return instance; }
		}
		protected TrackingMarksSealFacade():base() 
		{ 
		} 
	
	}
}
	