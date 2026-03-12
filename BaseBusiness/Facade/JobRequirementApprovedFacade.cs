
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class JobRequirementApprovedFacade : BaseFacade
	{
		protected static JobRequirementApprovedFacade instance = new JobRequirementApprovedFacade(new JobRequirementApprovedModel());
		protected JobRequirementApprovedFacade(JobRequirementApprovedModel model) : base(model)
		{
		}
		public static JobRequirementApprovedFacade Instance
		{
			get { return instance; }
		}
		protected JobRequirementApprovedFacade():base() 
		{ 
		} 
	
	}
}
	