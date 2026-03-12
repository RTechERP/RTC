
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PONCCBO : BaseBO
	{
		private PONCCFacade facade = PONCCFacade.Instance;
		protected static PONCCBO instance = new PONCCBO();

		protected PONCCBO()
		{
			this.baseFacade = facade;
		}

		public static PONCCBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	