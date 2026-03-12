
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class JobRequirementApprovedBO : BaseBO
	{
		private JobRequirementApprovedFacade facade = JobRequirementApprovedFacade.Instance;
		protected static JobRequirementApprovedBO instance = new JobRequirementApprovedBO();

		protected JobRequirementApprovedBO()
		{
			this.baseFacade = facade;
		}

		public static JobRequirementApprovedBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	