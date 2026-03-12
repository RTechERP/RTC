
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectPersonalPriotityBO : BaseBO
	{
		private ProjectPersonalPriotityFacade facade = ProjectPersonalPriotityFacade.Instance;
		protected static ProjectPersonalPriotityBO instance = new ProjectPersonalPriotityBO();

		protected ProjectPersonalPriotityBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectPersonalPriotityBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	