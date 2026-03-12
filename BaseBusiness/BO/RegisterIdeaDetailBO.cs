
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RegisterIdeaDetailBO : BaseBO
	{
		private RegisterIdeaDetailFacade facade = RegisterIdeaDetailFacade.Instance;
		protected static RegisterIdeaDetailBO instance = new RegisterIdeaDetailBO();

		protected RegisterIdeaDetailBO()
		{
			this.baseFacade = facade;
		}

		public static RegisterIdeaDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	