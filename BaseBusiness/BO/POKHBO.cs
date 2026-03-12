
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class POKHBO : BaseBO
	{
		private POKHFacade facade = POKHFacade.Instance;
		protected static POKHBO instance = new POKHBO();

		protected POKHBO()
		{
			this.baseFacade = facade;
		}

		public static POKHBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	