
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class JobRequirementDetailBO : BaseBO
	{
		private JobRequirementDetailFacade facade = JobRequirementDetailFacade.Instance;
		protected static JobRequirementDetailBO instance = new JobRequirementDetailBO();

		protected JobRequirementDetailBO()
		{
			this.baseFacade = facade;
		}

		public static JobRequirementDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	