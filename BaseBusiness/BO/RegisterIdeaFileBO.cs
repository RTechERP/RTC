
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RegisterIdeaFileBO : BaseBO
	{
		private RegisterIdeaFileFacade facade = RegisterIdeaFileFacade.Instance;
		protected static RegisterIdeaFileBO instance = new RegisterIdeaFileBO();

		protected RegisterIdeaFileBO()
		{
			this.baseFacade = facade;
		}

		public static RegisterIdeaFileBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	