
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectFileBO : BaseBO
	{
		private ProjectFileFacade facade = ProjectFileFacade.Instance;
		protected static ProjectFileBO instance = new ProjectFileBO();

		protected ProjectFileBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectFileBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	