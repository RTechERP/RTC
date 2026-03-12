
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RegisterIdeaScoreBO : BaseBO
	{
		private RegisterIdeaScoreFacade facade = RegisterIdeaScoreFacade.Instance;
		protected static RegisterIdeaScoreBO instance = new RegisterIdeaScoreBO();

		protected RegisterIdeaScoreBO()
		{
			this.baseFacade = facade;
		}

		public static RegisterIdeaScoreBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	