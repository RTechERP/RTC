
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RequestExportDetailFacade : BaseFacade
	{
		protected static RequestExportDetailFacade instance = new RequestExportDetailFacade(new RequestExportDetailModel());
		protected RequestExportDetailFacade(RequestExportDetailModel model) : base(model)
		{
		}
		public static RequestExportDetailFacade Instance
		{
			get { return instance; }
		}
		protected RequestExportDetailFacade():base() 
		{ 
		} 
	
	}
}
	