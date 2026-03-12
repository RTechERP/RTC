
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillFilmFacade : BaseFacade
	{
		protected static BillFilmFacade instance = new BillFilmFacade(new BillFilmModel());
		protected BillFilmFacade(BillFilmModel model) : base(model)
		{
		}
		public static BillFilmFacade Instance
		{
			get { return instance; }
		}
		protected BillFilmFacade():base() 
		{ 
		} 
	
	}
}
	