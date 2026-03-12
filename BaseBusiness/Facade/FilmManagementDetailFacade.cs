
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class FilmManagementDetailFacade : BaseFacade
	{
		protected static FilmManagementDetailFacade instance = new FilmManagementDetailFacade(new FilmManagementDetailModel());
		protected FilmManagementDetailFacade(FilmManagementDetailModel model) : base(model)
		{
		}
		public static FilmManagementDetailFacade Instance
		{
			get { return instance; }
		}
		protected FilmManagementDetailFacade():base() 
		{ 
		} 
	
	}
}
	