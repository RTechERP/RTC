
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class POBO : BaseBO
	{
		private POFacade facade = POFacade.Instance;
		protected static POBO instance = new POBO();

		protected POBO()
		{
			this.baseFacade = facade;
		}

		public static POBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	