
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class KPIErrorEmployeeFileFacade : BaseFacade
	{
		protected static KPIErrorEmployeeFileFacade instance = new KPIErrorEmployeeFileFacade(new KPIErrorEmployeeFileModel());
		protected KPIErrorEmployeeFileFacade(KPIErrorEmployeeFileModel model) : base(model)
		{
		}
		public static KPIErrorEmployeeFileFacade Instance
		{
			get { return instance; }
		}
		protected KPIErrorEmployeeFileFacade():base() 
		{ 
		} 
	
	}
}
	