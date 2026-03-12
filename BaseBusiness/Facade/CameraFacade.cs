
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CameraFacade : BaseFacade
	{
		protected static CameraFacade instance = new CameraFacade(new CameraModel());
		protected CameraFacade(CameraModel model) : base(model)
		{
		}
		public static CameraFacade Instance
		{
			get { return instance; }
		}
		protected CameraFacade():base() 
		{ 
		} 
	
	}
}
	