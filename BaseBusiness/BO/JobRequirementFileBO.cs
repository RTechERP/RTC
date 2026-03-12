
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class JobRequirementFileBO : BaseBO
	{
		private JobRequirementFileFacade facade = JobRequirementFileFacade.Instance;
		protected static JobRequirementFileBO instance = new JobRequirementFileBO();

		protected JobRequirementFileBO()
		{
			this.baseFacade = facade;
		}

		public static JobRequirementFileBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	