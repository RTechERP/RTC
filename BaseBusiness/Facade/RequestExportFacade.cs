
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RequestExportFacade : BaseFacade
	{
		protected static RequestExportFacade instance = new RequestExportFacade(new RequestExportModel());
		protected RequestExportFacade(RequestExportModel model) : base(model)
		{
		}
		public static RequestExportFacade Instance
		{
			get { return instance; }
		}
		protected RequestExportFacade():base() 
		{ 
		} 
	
	}
}
	