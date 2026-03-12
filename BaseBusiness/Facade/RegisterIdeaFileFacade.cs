
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RegisterIdeaFileFacade : BaseFacade
	{
		protected static RegisterIdeaFileFacade instance = new RegisterIdeaFileFacade(new RegisterIdeaFileModel());
		protected RegisterIdeaFileFacade(RegisterIdeaFileModel model) : base(model)
		{
		}
		public static RegisterIdeaFileFacade Instance
		{
			get { return instance; }
		}
		protected RegisterIdeaFileFacade():base() 
		{ 
		} 
	
	}
}
	