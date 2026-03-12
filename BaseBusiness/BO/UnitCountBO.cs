
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class UnitCountBO : BaseBO
	{
		private UnitCountFacade facade = UnitCountFacade.Instance;
		protected static UnitCountBO instance = new UnitCountBO();

		protected UnitCountBO()
		{
			this.baseFacade = facade;
		}

		public static UnitCountBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	