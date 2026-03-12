
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectTreeFolderBO : BaseBO
	{
		private ProjectTreeFolderFacade facade = ProjectTreeFolderFacade.Instance;
		protected static ProjectTreeFolderBO instance = new ProjectTreeFolderBO();

		protected ProjectTreeFolderBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectTreeFolderBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	