
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class JobRequirementDetailFacade : BaseFacade
	{
		protected static JobRequirementDetailFacade instance = new JobRequirementDetailFacade(new JobRequirementDetailModel());
		protected JobRequirementDetailFacade(JobRequirementDetailModel model) : base(model)
		{
		}
		public static JobRequirementDetailFacade Instance
		{
			get { return instance; }
		}
		protected JobRequirementDetailFacade():base() 
		{ 
		} 
	
	}
}
	