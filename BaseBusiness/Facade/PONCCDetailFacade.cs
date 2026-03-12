
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PONCCDetailFacade : BaseFacade
	{
		protected static PONCCDetailFacade instance = new PONCCDetailFacade(new PONCCDetailModel());
		protected PONCCDetailFacade(PONCCDetailModel model) : base(model)
		{
		}
		public static PONCCDetailFacade Instance
		{
			get { return instance; }
		}
		protected PONCCDetailFacade():base() 
		{ 
		} 
	
	}
}
	