
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CourseCatalogFacade : BaseFacade
	{
		protected static CourseCatalogFacade instance = new CourseCatalogFacade(new CourseCatalogModel());
		protected CourseCatalogFacade(CourseCatalogModel model) : base(model)
		{
		}
		public static CourseCatalogFacade Instance
		{
			get { return instance; }
		}
		protected CourseCatalogFacade():base() 
		{ 
		} 
	
	}
}
	