
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class FirmFacade : BaseFacade
	{
		protected static FirmFacade instance = new FirmFacade(new FirmModel());
		protected FirmFacade(FirmModel model) : base(model)
		{
		}
		public static FirmFacade Instance
		{
			get { return instance; }
		}
		protected FirmFacade():base() 
		{ 
		} 
	
	}
}
	