
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class SealRegulationsBO : BaseBO
	{
		private SealRegulationsFacade facade = SealRegulationsFacade.Instance;
		protected static SealRegulationsBO instance = new SealRegulationsBO();

		protected SealRegulationsBO()
		{
			this.baseFacade = facade;
		}

		public static SealRegulationsBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	