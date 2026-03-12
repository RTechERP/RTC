
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PONCCDetailRequestBuyFacade : BaseFacade
	{
		protected static PONCCDetailRequestBuyFacade instance = new PONCCDetailRequestBuyFacade(new PONCCDetailRequestBuyModel());
		protected PONCCDetailRequestBuyFacade(PONCCDetailRequestBuyModel model) : base(model)
		{
		}
		public static PONCCDetailRequestBuyFacade Instance
		{
			get { return instance; }
		}
		protected PONCCDetailRequestBuyFacade():base() 
		{ 
		} 
	
	}
}
	