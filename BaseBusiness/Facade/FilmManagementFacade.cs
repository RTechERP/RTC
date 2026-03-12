
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class FilmManagementFacade : BaseFacade
	{
		protected static FilmManagementFacade instance = new FilmManagementFacade(new FilmManagementModel());
		protected FilmManagementFacade(FilmManagementModel model) : base(model)
		{
		}
		public static FilmManagementFacade Instance
		{
			get { return instance; }
		}
		protected FilmManagementFacade():base() 
		{ 
		} 
	
	}
}
	