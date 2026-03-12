
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TaxDepartmentBO : BaseBO
	{
		private TaxDepartmentFacade facade = TaxDepartmentFacade.Instance;
		protected static TaxDepartmentBO instance = new TaxDepartmentBO();

		protected TaxDepartmentBO()
		{
			this.baseFacade = facade;
		}

		public static TaxDepartmentBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	