
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class sysdiagramsFacade : BaseFacade
	{
		protected static sysdiagramsFacade instance = new sysdiagramsFacade(new sysdiagramsModel());
		protected sysdiagramsFacade(sysdiagramsModel model) : base(model)
		{
		}
		public static sysdiagramsFacade Instance
		{
			get { return instance; }
		}
		protected sysdiagramsFacade():base() 
		{ 
		} 
	
	}
}
	