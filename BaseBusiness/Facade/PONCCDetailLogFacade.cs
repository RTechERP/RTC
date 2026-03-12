
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PONCCDetailLogFacade : BaseFacade
	{
		protected static PONCCDetailLogFacade instance = new PONCCDetailLogFacade(new PONCCDetailLogModel());
		protected PONCCDetailLogFacade(PONCCDetailLogModel model) : base(model)
		{
		}
		public static PONCCDetailLogFacade Instance
		{
			get { return instance; }
		}
		protected PONCCDetailLogFacade():base() 
		{ 
		} 
	
	}
}
	