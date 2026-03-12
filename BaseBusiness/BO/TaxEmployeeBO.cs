
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TaxEmployeeBO : BaseBO
	{
		private TaxEmployeeFacade facade = TaxEmployeeFacade.Instance;
		protected static TaxEmployeeBO instance = new TaxEmployeeBO();

		protected TaxEmployeeBO()
		{
			this.baseFacade = facade;
		}

		public static TaxEmployeeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	