
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TrackingMarksFacade : BaseFacade
	{
		protected static TrackingMarksFacade instance = new TrackingMarksFacade(new TrackingMarksModel());
		protected TrackingMarksFacade(TrackingMarksModel model) : base(model)
		{
		}
		public static TrackingMarksFacade Instance
		{
			get { return instance; }
		}
		protected TrackingMarksFacade():base() 
		{ 
		} 
	
	}
}
	