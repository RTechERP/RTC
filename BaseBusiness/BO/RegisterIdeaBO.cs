
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RegisterIdeaBO : BaseBO
	{
		private RegisterIdeaFacade facade = RegisterIdeaFacade.Instance;
		protected static RegisterIdeaBO instance = new RegisterIdeaBO();

		protected RegisterIdeaBO()
		{
			this.baseFacade = facade;
		}

		public static RegisterIdeaBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	