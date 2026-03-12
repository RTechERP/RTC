
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BusinessFieldLinkFacade : BaseFacade
	{
		protected static BusinessFieldLinkFacade instance = new BusinessFieldLinkFacade(new BusinessFieldLinkModel());
		protected BusinessFieldLinkFacade(BusinessFieldLinkModel model) : base(model)
		{
		}
		public static BusinessFieldLinkFacade Instance
		{
			get { return instance; }
		}
		protected BusinessFieldLinkFacade():base() 
		{ 
		} 
	
	}
}
	