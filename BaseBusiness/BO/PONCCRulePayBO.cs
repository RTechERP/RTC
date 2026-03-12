
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PONCCRulePayBO : BaseBO
	{
		private PONCCRulePayFacade facade = PONCCRulePayFacade.Instance;
		protected static PONCCRulePayBO instance = new PONCCRulePayBO();

		protected PONCCRulePayBO()
		{
			this.baseFacade = facade;
		}

		public static PONCCRulePayBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	