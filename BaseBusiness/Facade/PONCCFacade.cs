
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PONCCFacade : BaseFacade
	{
		protected static PONCCFacade instance = new PONCCFacade(new PONCCModel());
		protected PONCCFacade(PONCCModel model) : base(model)
		{
		}
		public static PONCCFacade Instance
		{
			get { return instance; }
		}
		protected PONCCFacade():base() 
		{ 
		} 
	
	}
}
	