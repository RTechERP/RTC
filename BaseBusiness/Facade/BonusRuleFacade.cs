
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BonusRuleFacade : BaseFacade
	{
		protected static BonusRuleFacade instance = new BonusRuleFacade(new BonusRuleModel());
		protected BonusRuleFacade(BonusRuleModel model) : base(model)
		{
		}
		public static BonusRuleFacade Instance
		{
			get { return instance; }
		}
		protected BonusRuleFacade():base() 
		{ 
		} 
	
	}
}
	