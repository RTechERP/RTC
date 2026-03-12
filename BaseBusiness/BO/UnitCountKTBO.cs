
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class UnitCountKTBO : BaseBO
	{
		private UnitCountKTFacade facade = UnitCountKTFacade.Instance;
		protected static UnitCountKTBO instance = new UnitCountKTBO();

		protected UnitCountKTBO()
		{
			this.baseFacade = facade;
		}

		public static UnitCountKTBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	