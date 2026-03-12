
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class DocumentExportFacade : BaseFacade
	{
		protected static DocumentExportFacade instance = new DocumentExportFacade(new DocumentExportModel());
		protected DocumentExportFacade(DocumentExportModel model) : base(model)
		{
		}
		public static DocumentExportFacade Instance
		{
			get { return instance; }
		}
		protected DocumentExportFacade():base() 
		{ 
		} 
	
	}
}
	