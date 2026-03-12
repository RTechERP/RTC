
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BonusRuleIndexBO : BaseBO
	{
		private BonusRuleIndexFacade facade = BonusRuleIndexFacade.Instance;
		protected static BonusRuleIndexBO instance = new BonusRuleIndexBO();

		protected BonusRuleIndexBO()
		{
			this.baseFacade = facade;
		}

		public static BonusRuleIndexBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	