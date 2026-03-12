
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RequestImportFacade : BaseFacade
	{
		protected static RequestImportFacade instance = new RequestImportFacade(new RequestImportModel());
		protected RequestImportFacade(RequestImportModel model) : base(model)
		{
		}
		public static RequestImportFacade Instance
		{
			get { return instance; }
		}
		protected RequestImportFacade():base() 
		{ 
		} 
	
	}
}
	