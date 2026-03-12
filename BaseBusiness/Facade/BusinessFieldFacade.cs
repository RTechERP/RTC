
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BusinessFieldFacade : BaseFacade
	{
		protected static BusinessFieldFacade instance = new BusinessFieldFacade(new BusinessFieldModel());
		protected BusinessFieldFacade(BusinessFieldModel model) : base(model)
		{
		}
		public static BusinessFieldFacade Instance
		{
			get { return instance; }
		}
		protected BusinessFieldFacade():base() 
		{ 
		} 
	
	}
}
	