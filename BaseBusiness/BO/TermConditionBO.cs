
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TermConditionBO : BaseBO
	{
		private TermConditionFacade facade = TermConditionFacade.Instance;
		protected static TermConditionBO instance = new TermConditionBO();

		protected TermConditionBO()
		{
			this.baseFacade = facade;
		}

		public static TermConditionBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	