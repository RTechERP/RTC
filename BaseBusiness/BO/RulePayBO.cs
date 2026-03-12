
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RulePayBO : BaseBO
	{
		private RulePayFacade facade = RulePayFacade.Instance;
		protected static RulePayBO instance = new RulePayBO();

		protected RulePayBO()
		{
			this.baseFacade = facade;
		}

		public static RulePayBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	