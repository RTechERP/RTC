
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillFilmBO : BaseBO
	{
		private BillFilmFacade facade = BillFilmFacade.Instance;
		protected static BillFilmBO instance = new BillFilmBO();

		protected BillFilmBO()
		{
			this.baseFacade = facade;
		}

		public static BillFilmBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	