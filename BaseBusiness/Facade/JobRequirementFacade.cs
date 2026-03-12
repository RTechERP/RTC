
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class JobRequirementFacade : BaseFacade
	{
		protected static JobRequirementFacade instance = new JobRequirementFacade(new JobRequirementModel());
		protected JobRequirementFacade(JobRequirementModel model) : base(model)
		{
		}
		public static JobRequirementFacade Instance
		{
			get { return instance; }
		}
		protected JobRequirementFacade():base() 
		{ 
		} 
	
	}
}
	