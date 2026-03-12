
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class DocumentFileFacade : BaseFacade
	{
		protected static DocumentFileFacade instance = new DocumentFileFacade(new DocumentFileModel());
		protected DocumentFileFacade(DocumentFileModel model) : base(model)
		{
		}
		public static DocumentFileFacade Instance
		{
			get { return instance; }
		}
		protected DocumentFileFacade():base() 
		{ 
		} 
	
	}
}
	