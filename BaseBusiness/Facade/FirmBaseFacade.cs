
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class FirmBaseFacade : BaseFacade
	{
		protected static FirmBaseFacade instance = new FirmBaseFacade(new FirmBaseModel());
		protected FirmBaseFacade(FirmBaseModel model) : base(model)
		{
		}
		public static FirmBaseFacade Instance
		{
			get { return instance; }
		}
		protected FirmBaseFacade():base() 
		{ 
		} 
	
	}
}
	