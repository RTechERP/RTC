
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RequestImportDetailFacade : BaseFacade
	{
		protected static RequestImportDetailFacade instance = new RequestImportDetailFacade(new RequestImportDetailModel());
		protected RequestImportDetailFacade(RequestImportDetailModel model) : base(model)
		{
		}
		public static RequestImportDetailFacade Instance
		{
			get { return instance; }
		}
		protected RequestImportDetailFacade():base() 
		{ 
		} 
	
	}
}
	