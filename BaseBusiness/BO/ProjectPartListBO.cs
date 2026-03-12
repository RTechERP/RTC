
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectPartListBO : BaseBO
	{
		private ProjectPartListFacade facade = ProjectPartListFacade.Instance;
		protected static ProjectPartListBO instance = new ProjectPartListBO();

		protected ProjectPartListBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectPartListBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	