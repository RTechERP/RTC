
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ViewDetailKPIPurchaseBO : BaseBO
	{
		private ViewDetailKPIPurchaseFacade facade = ViewDetailKPIPurchaseFacade.Instance;
		protected static ViewDetailKPIPurchaseBO instance = new ViewDetailKPIPurchaseBO();

		protected ViewDetailKPIPurchaseBO()
		{
			this.baseFacade = facade;
		}

		public static ViewDetailKPIPurchaseBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	