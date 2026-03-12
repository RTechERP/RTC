
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class FilmManagementDetailBO : BaseBO
	{
		private FilmManagementDetailFacade facade = FilmManagementDetailFacade.Instance;
		protected static FilmManagementDetailBO instance = new FilmManagementDetailBO();

		protected FilmManagementDetailBO()
		{
			this.baseFacade = facade;
		}

		public static FilmManagementDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	