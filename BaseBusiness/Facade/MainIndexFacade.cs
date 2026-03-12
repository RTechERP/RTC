
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class MainIndexFacade : BaseFacade
	{
		protected static MainIndexFacade instance = new MainIndexFacade(new MainIndexModel());
		protected MainIndexFacade(MainIndexModel model) : base(model)
		{
		}
		public static MainIndexFacade Instance
		{
			get { return instance; }
		}
		protected MainIndexFacade():base() 
		{ 
		} 
	
	}
}
	