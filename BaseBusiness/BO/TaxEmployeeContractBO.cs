
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TaxEmployeeContractBO : BaseBO
	{
		private TaxEmployeeContractFacade facade = TaxEmployeeContractFacade.Instance;
		protected static TaxEmployeeContractBO instance = new TaxEmployeeContractBO();

		protected TaxEmployeeContractBO()
		{
			this.baseFacade = facade;
		}

		public static TaxEmployeeContractBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	