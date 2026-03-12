
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CourseCatalogBO : BaseBO
	{
		private CourseCatalogFacade facade = CourseCatalogFacade.Instance;
		protected static CourseCatalogBO instance = new CourseCatalogBO();

		protected CourseCatalogBO()
		{
			this.baseFacade = facade;
		}

		public static CourseCatalogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	