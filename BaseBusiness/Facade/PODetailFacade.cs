
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PODetailFacade : BaseFacade
	{
		protected static PODetailFacade instance = new PODetailFacade(new PODetailModel());
		protected PODetailFacade(PODetailModel model) : base(model)
		{
		}
		public static PODetailFacade Instance
		{
			get { return instance; }
		}
		protected PODetailFacade():base() 
		{ 
		} 
	
	}
}
	