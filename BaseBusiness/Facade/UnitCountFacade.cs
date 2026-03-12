
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class UnitCountFacade : BaseFacade
	{
		protected static UnitCountFacade instance = new UnitCountFacade(new UnitCountModel());
		protected UnitCountFacade(UnitCountModel model) : base(model)
		{
		}
		public static UnitCountFacade Instance
		{
			get { return instance; }
		}
		protected UnitCountFacade():base() 
		{ 
		} 
	
	}
}
	