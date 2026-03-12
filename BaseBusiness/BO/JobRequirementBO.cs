
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class JobRequirementBO : BaseBO
	{
		private JobRequirementFacade facade = JobRequirementFacade.Instance;
		protected static JobRequirementBO instance = new JobRequirementBO();

		protected JobRequirementBO()
		{
			this.baseFacade = facade;
		}

		public static JobRequirementBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	