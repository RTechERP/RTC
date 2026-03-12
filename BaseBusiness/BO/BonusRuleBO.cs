
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BonusRuleBO : BaseBO
	{
		private BonusRuleFacade facade = BonusRuleFacade.Instance;
		protected static BonusRuleBO instance = new BonusRuleBO();

		protected BonusRuleBO()
		{
			this.baseFacade = facade;
		}

		public static BonusRuleBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	