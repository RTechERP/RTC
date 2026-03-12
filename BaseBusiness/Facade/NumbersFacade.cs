
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class NumbersFacade : BaseFacade
	{
		protected static NumbersFacade instance = new NumbersFacade(new NumbersModel());
		protected NumbersFacade(NumbersModel model) : base(model)
		{
		}
		public static NumbersFacade Instance
		{
			get { return instance; }
		}
		protected NumbersFacade():base() 
		{ 
		} 
	
	}
}
	