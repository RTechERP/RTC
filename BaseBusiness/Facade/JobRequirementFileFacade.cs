
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class JobRequirementFileFacade : BaseFacade
	{
		protected static JobRequirementFileFacade instance = new JobRequirementFileFacade(new JobRequirementFileModel());
		protected JobRequirementFileFacade(JobRequirementFileModel model) : base(model)
		{
		}
		public static JobRequirementFileFacade Instance
		{
			get { return instance; }
		}
		protected JobRequirementFileFacade():base() 
		{ 
		} 
	
	}
}
	