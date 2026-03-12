
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TermConditionFacade : BaseFacade
	{
		protected static TermConditionFacade instance = new TermConditionFacade(new TermConditionModel());
		protected TermConditionFacade(TermConditionModel model) : base(model)
		{
		}
		public static TermConditionFacade Instance
		{
			get { return instance; }
		}
		protected TermConditionFacade():base() 
		{ 
		} 
	
	}
}
	