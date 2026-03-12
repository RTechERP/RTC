
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TaxEmployeePositionBO : BaseBO
	{
		private TaxEmployeePositionFacade facade = TaxEmployeePositionFacade.Instance;
		protected static TaxEmployeePositionBO instance = new TaxEmployeePositionBO();

		protected TaxEmployeePositionBO()
		{
			this.baseFacade = facade;
		}

		public static TaxEmployeePositionBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	