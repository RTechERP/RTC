
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PartGroupBO : BaseBO
	{
		private PartGroupFacade facade = PartGroupFacade.Instance;
		protected static PartGroupBO instance = new PartGroupBO();

		protected PartGroupBO()
		{
			this.baseFacade = facade;
		}

		public static PartGroupBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	