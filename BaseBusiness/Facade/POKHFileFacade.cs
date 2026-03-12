
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class POKHFileFacade : BaseFacade
	{
		protected static POKHFileFacade instance = new POKHFileFacade(new POKHFileModel());
		protected POKHFileFacade(POKHFileModel model) : base(model)
		{
		}
		public static POKHFileFacade Instance
		{
			get { return instance; }
		}
		protected POKHFileFacade():base() 
		{ 
		} 
	
	}
}
	