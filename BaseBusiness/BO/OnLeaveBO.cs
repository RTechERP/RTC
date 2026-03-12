
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class OnLeaveBO : BaseBO
	{
		private OnLeaveFacade facade = OnLeaveFacade.Instance;
		protected static OnLeaveBO instance = new OnLeaveBO();

		protected OnLeaveBO()
		{
			this.baseFacade = facade;
		}

		public static OnLeaveBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	