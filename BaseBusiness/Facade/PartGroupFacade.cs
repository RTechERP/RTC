
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PartGroupFacade : BaseFacade
	{
		protected static PartGroupFacade instance = new PartGroupFacade(new PartGroupModel());
		protected PartGroupFacade(PartGroupModel model) : base(model)
		{
		}
		public static PartGroupFacade Instance
		{
			get { return instance; }
		}
		protected PartGroupFacade():base() 
		{ 
		} 
	
	}
}
	