
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectCurrentSituationBO : BaseBO
	{
		private ProjectCurrentSituationFacade facade = ProjectCurrentSituationFacade.Instance;
		protected static ProjectCurrentSituationBO instance = new ProjectCurrentSituationBO();

		protected ProjectCurrentSituationBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectCurrentSituationBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	