
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BonusRuleIndexFacade : BaseFacade
	{
		protected static BonusRuleIndexFacade instance = new BonusRuleIndexFacade(new BonusRuleIndexModel());
		protected BonusRuleIndexFacade(BonusRuleIndexModel model) : base(model)
		{
		}
		public static BonusRuleIndexFacade Instance
		{
			get { return instance; }
		}
		protected BonusRuleIndexFacade():base() 
		{ 
		} 
	
	}
}
	