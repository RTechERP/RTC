
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class MainIndexBO : BaseBO
	{
		private MainIndexFacade facade = MainIndexFacade.Instance;
		protected static MainIndexBO instance = new MainIndexBO();

		protected MainIndexBO()
		{
			this.baseFacade = facade;
		}

		public static MainIndexBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	