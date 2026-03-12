
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectPartListVersionBO : BaseBO
	{
		private ProjectPartListVersionFacade facade = ProjectPartListVersionFacade.Instance;
		protected static ProjectPartListVersionBO instance = new ProjectPartListVersionBO();

		protected ProjectPartListVersionBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectPartListVersionBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	