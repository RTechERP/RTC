
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectStatusBO : BaseBO
	{
		private ProjectStatusFacade facade = ProjectStatusFacade.Instance;
		protected static ProjectStatusBO instance = new ProjectStatusBO();

		protected ProjectStatusBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectStatusBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	