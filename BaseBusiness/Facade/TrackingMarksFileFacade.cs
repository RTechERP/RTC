
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TrackingMarksFileFacade : BaseFacade
	{
		protected static TrackingMarksFileFacade instance = new TrackingMarksFileFacade(new TrackingMarksFileModel());
		protected TrackingMarksFileFacade(TrackingMarksFileModel model) : base(model)
		{
		}
		public static TrackingMarksFileFacade Instance
		{
			get { return instance; }
		}
		protected TrackingMarksFileFacade():base() 
		{ 
		} 
	
	}
}
	