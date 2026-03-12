
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class SealRegulationsFacade : BaseFacade
	{
		protected static SealRegulationsFacade instance = new SealRegulationsFacade(new SealRegulationsModel());
		protected SealRegulationsFacade(SealRegulationsModel model) : base(model)
		{
		}
		public static SealRegulationsFacade Instance
		{
			get { return instance; }
		}
		protected SealRegulationsFacade():base() 
		{ 
		} 
	
	}
}
	