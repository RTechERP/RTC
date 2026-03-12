
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RulePayFacade : BaseFacade
	{
		protected static RulePayFacade instance = new RulePayFacade(new RulePayModel());
		protected RulePayFacade(RulePayModel model) : base(model)
		{
		}
		public static RulePayFacade Instance
		{
			get { return instance; }
		}
		protected RulePayFacade():base() 
		{ 
		} 
	
	}
}
	