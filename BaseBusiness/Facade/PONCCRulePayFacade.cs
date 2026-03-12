
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PONCCRulePayFacade : BaseFacade
	{
		protected static PONCCRulePayFacade instance = new PONCCRulePayFacade(new PONCCRulePayModel());
		protected PONCCRulePayFacade(PONCCRulePayModel model) : base(model)
		{
		}
		public static PONCCRulePayFacade Instance
		{
			get { return instance; }
		}
		protected PONCCRulePayFacade():base() 
		{ 
		} 
	
	}
}
	