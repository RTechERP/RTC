
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class FilmManagementBO : BaseBO
	{
		private FilmManagementFacade facade = FilmManagementFacade.Instance;
		protected static FilmManagementBO instance = new FilmManagementBO();

		protected FilmManagementBO()
		{
			this.baseFacade = facade;
		}

		public static FilmManagementBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	