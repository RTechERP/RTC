
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RegisterOTBO : BaseBO
	{
		private RegisterOTFacade facade = RegisterOTFacade.Instance;
		protected static RegisterOTBO instance = new RegisterOTBO();

		protected RegisterOTBO()
		{
			this.baseFacade = facade;
		}

		public static RegisterOTBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	