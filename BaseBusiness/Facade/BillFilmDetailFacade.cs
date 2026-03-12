
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillFilmDetailFacade : BaseFacade
	{
		protected static BillFilmDetailFacade instance = new BillFilmDetailFacade(new BillFilmDetailModel());
		protected BillFilmDetailFacade(BillFilmDetailModel model) : base(model)
		{
		}
		public static BillFilmDetailFacade Instance
		{
			get { return instance; }
		}
		protected BillFilmDetailFacade():base() 
		{ 
		} 
	
	}
}
	