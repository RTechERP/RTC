
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class NumbersBO : BaseBO
	{
		private NumbersFacade facade = NumbersFacade.Instance;
		protected static NumbersBO instance = new NumbersBO();

		protected NumbersBO()
		{
			this.baseFacade = facade;
		}

		public static NumbersBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	