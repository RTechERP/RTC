
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class UnitCountKTFacade : BaseFacade
	{
		protected static UnitCountKTFacade instance = new UnitCountKTFacade(new UnitCountKTModel());
		protected UnitCountKTFacade(UnitCountKTModel model) : base(model)
		{
		}
		public static UnitCountKTFacade Instance
		{
			get { return instance; }
		}
		protected UnitCountKTFacade():base() 
		{ 
		} 
	
	}
}
	