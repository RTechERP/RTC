
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillFilmDetailBO : BaseBO
	{
		private BillFilmDetailFacade facade = BillFilmDetailFacade.Instance;
		protected static BillFilmDetailBO instance = new BillFilmDetailBO();

		protected BillFilmDetailBO()
		{
			this.baseFacade = facade;
		}

		public static BillFilmDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	